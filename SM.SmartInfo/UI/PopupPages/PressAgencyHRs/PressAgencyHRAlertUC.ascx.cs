using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using static SM.SmartInfo.SharedComponent.Constants.SMX;
using DocumentFormat.OpenXml.Drawing.Charts;
//using System.Globalization;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using SM.SmartInfo.CacheManager;
using System.Globalization;
using SM.SmartInfo.DAO;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class PressAgencyHRAlertUC : UserControl
    {
        public event EventHandler RequestSave_PressAgencyHR;

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetDataFromPopup();
                PressAgencyParam param1 = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyHR_ByID);
                param1.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value) };
                MainController.Provider.Execute(param1);

                var hrPosition = param1.PressAgencyHR.Position;
                var hrName = param1.PressAgencyHR.FullName;



                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyHRAlert);
                param.PressAgencyHRAlert = item;
                MainController.Provider.Execute(param);
                LogManager.WebLogger.LogError("Tạo lịch nhắc nhở: " + item.TypeDate);
                if (item.TypeDate == null) item.TypeDate = 1;
                LogManager.WebLogger.LogError("Tạo lịch nhắc nhở: " + item.TypeDate);
                var typeDate = item.TypeDate == 1 ? "Dương lịch" : "Âm lịch";



                //var newNTF = new ntf_Notification()
                //{
                //    lunarDay = item.TypeDate == 1 ? lunarCalendar.GetDayOfMonth(DateTime.Now) : item.lunarDay,
                //    lunarMonth = item.TypeDate == 1 ? lunarCalendar.GetMonth(DateTime.Now) : item.lunarMonth,
                //    lunarYear = item.TypeDate == 1 ? lunarCalendar.GetYear(DateTime.Now) : item.lunarYear,
                //    //DoDTG = item.TypeDate == 1 ? item.AlertDTG : ConvertSolarLunar.solarConverter(Convert.ToDateTime(item.AlertDTG)),
                //    DoDTG = item.AlertDTG,
                //    Content = item.TypeDate == 1 ? (string.IsNullOrEmpty(item.Content) ? "Sinh nhật" : item.Content) + " của ông/bà" + $" {hrName} " + $"({typeDate})" :
                //    (string.IsNullOrEmpty(item.Content) ? "Ngày giỗ" : item.Content) + " của ông/bà" + $" {hrName} " + $"({item.lunarDay}/{item.lunarMonth}/{item.lunarYear} - {typeDate})",
                //    Type = item.TypeDate == 1 ? 1 : 5,
                //    Note = hrPosition,
                //    Comment = null,
                //    AlertID = param.PressAgencyHRAlert.PressAgencyHRAlertID,
                //    isDeleted = 0,
                //    CreatedBy = Profiles.MyProfile.UserName
                //};

                //NotificationParam ntfParam = new NotificationParam(FunctionType.Notification.AddOrUpdateItem);
                //ntfParam.Notification = newNTF;
                //MainController.Provider.Execute(ntfParam);

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidPressAgencyHRID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestSave_PressAgencyHR != null && (string.IsNullOrWhiteSpace(hidPressAgencyHRID.Value) || Utility.GetNullableInt(hidPressAgencyHRID.Value) == 0))
                {
                    RequestSave_PressAgencyHR(null, null);
                    return;
                }

                ClearPopup();
                UIUtility.BindDicToDropDownList(dataTypeDate, TypeDate.dicDesc, false);
                popEdit.Show();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void popEdit_PopupClosed(object sender, EventArgs e)
        {
            popEdit.Hide();
        }

        protected void rptHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeater(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                var item = GetCurrentRowData(e.Item);

                switch (e.CommandName)
                {
                    case SMX.ActionEdit:
                        BindObjectToPopup(item);
                        popEdit.Show();
                        break;
                    case SMX.ActionDelete:
                        DeleteItem(item);
                        BindData(Utility.GetNullableInt(hidPressAgencyHRID.Value), Utility.GetNullableBool(hidIsEdit.Value));
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void BindData(int? pressAgencyHRID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyHRID.Value = Utility.GetString(pressAgencyHRID);
            btnAddNew.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHRAlert_ByPressAgencyHRID);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = pressAgencyHRID };
            MainController.Provider.Execute(param);

            rptHistory.DataSource = param.ListPressAgencyHRAlert;
            rptHistory.DataBind();

        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidPressAgencyHRAlertID.Value = txtContent.Text = string.Empty;
            dpkAlertDTG.SelectedDate = null;

        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHRAlert item = rptItem.DataItem as agency_PressAgencyHRAlert;

            HiddenField hidPressAgencyHRAlertID = rptItem.FindControl("hidPressAgencyHRAlertID") as HiddenField;
            HiddenField hidTypeDate = rptItem.FindControl("fieldTypeDate") as HiddenField;

            hidPressAgencyHRAlertID.Value = Utility.GetString(item.PressAgencyHRAlertID);
            hidTypeDate.Value = Utility.GetString(item.TypeDate);

            string smg = "";
            string strDate = "";
            if (item.TypeDate == 2)
            {
                int[] lunarDate = VietNamCalendar.convertSolar2Lunar(item.AlertDTG.Value.Day, item.AlertDTG.Value.Month, item.AlertDTG.Value.Year, 7.0);
                smg += "Âm Lịch";
                strDate = $"{ lunarDate[0]}/{ lunarDate[1]}/{lunarDate[2]}";
            }
            else
            {
                smg += "Dương Lịch";
                strDate = Utility.GetDateString(item.AlertDTG);

            }

            UIUtility.SetRepeaterItemIText(rptItem, "ltrContent", item.Content);
            UIUtility.SetRepeaterItemIText(rptItem, "lrtDate", smg.ToString());
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAlertDTG", strDate);
            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
            UIUtility.BindDicToDropDownList(dataTypeDate, TypeDate.dicDesc, false);
            btnEdit.Visible = btnDelete.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            DatePicker dpkAlertDTG = rptItem.FindControl("dpkAlertDTG") as DatePicker;
            dpkAlertDTG.SelectedDate = item.AlertDTG;

            btnEdit.CommandName = ActionEdit;
            btnDelete.CommandName = ActionDelete;
        }


        private agency_PressAgencyHRAlert GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_PressAgencyHRAlert result = new agency_PressAgencyHRAlert();
            ChineseLunisolarCalendar lunarCalendar = new ChineseLunisolarCalendar();
            HiddenField hidPressAgencyHRAlertID = rptItem.FindControl("hidPressAgencyHRAlertID") as HiddenField;
            Literal ltrContent = rptItem.FindControl("ltrContent") as Literal;
            DatePicker dpkAlertDTG = rptItem.FindControl("dpkAlertDTG") as DatePicker;
            HiddenField drdTypeDate = rptItem.FindControl("fieldTypeDate") as HiddenField;
            result.PressAgencyHRAlertID = Utility.GetNullableInt(hidPressAgencyHRAlertID.Value);
            result.Content = ltrContent.Text;
            result.AlertDTG = dpkAlertDTG.SelectedDate;
            result.lunarYear = lunarCalendar.GetYear((DateTime)dpkAlertDTG.SelectedDate);
            result.lunarMonth = lunarCalendar.GetMonth((DateTime)dpkAlertDTG.SelectedDate);
            result.lunarDay = lunarCalendar.GetDayOfMonth((DateTime)dpkAlertDTG.SelectedDate);
            result.TypeDate = int.Parse(drdTypeDate.Value);

            return result;
        }

        private void BindObjectToPopup(agency_PressAgencyHRAlert item)
        {
            hidPressAgencyHRAlertID.Value = Utility.GetString(item.PressAgencyHRAlertID);
            txtContent.Text = item.Content;
            dpkAlertDTG.SelectedDate = item.AlertDTG.GetValueOrDefault();
            //if (item.TypeDate == 2)
            //{
            //    DateTime alertDTG = item.AlertDTG.GetValueOrDefault();
            //    dpkAlertDTG.SelectedDate = alertDTG;
            //}
            //else dpkAlertDTG.SelectedDate = item.AlertDTG;

            dataTypeDate.SelectedValue = item.TypeDate.ToString();
        }

        private agency_PressAgencyHRAlert GetDataFromPopup()
        {
            agency_PressAgencyHRAlert result = new agency_PressAgencyHRAlert();

            result.PressAgencyHRAlertID = Utility.GetNullableInt(hidPressAgencyHRAlertID.Value);
            result.PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
            result.Content = txtContent.Text;

            DateTime alertDTG = dpkAlertDTG.SelectedDate.GetValueOrDefault();
            if (int.Parse(dataTypeDate.SelectedValue) == 2)
            {
                int[] lunarDate = VietNamCalendar.convertSolar2Lunar(alertDTG.Day, alertDTG.Month, alertDTG.Year, 7.0);
                result.lunarYear = lunarDate[2];
                result.lunarMonth = lunarDate[1];
                result.lunarDay = lunarDate[0];
            }
            result.AlertDTG = alertDTG;
            result.TypeDate = int.Parse(dataTypeDate.SelectedValue);
            return result;
        }

        private void DeleteItem(agency_PressAgencyHRAlert item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHRAlert);
            param.PressAgencyHRAlert = item;
            MainController.Provider.Execute(param);

            NotificationParam ntfParam = new NotificationParam(FunctionType.Notification.DeleteNotificationByHRAlertID);
            ntfParam.AlertID = item.PressAgencyHRAlertID.Value;
            MainController.Provider.Execute(ntfParam);
        }

        #endregion
    }
}