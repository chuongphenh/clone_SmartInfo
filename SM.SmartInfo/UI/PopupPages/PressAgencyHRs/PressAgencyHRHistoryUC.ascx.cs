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

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class PressAgencyHRHistoryUC : UserControl
    {
        public event EventHandler RequestSave_PressAgencyHR;

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetDataFromPopup();

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyHRHistory);
                param.PressAgencyHRHistory = item;
                MainController.Provider.Execute(param);

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

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHRHistory_ByPressAgencyHRID);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = pressAgencyHRID };
            MainController.Provider.Execute(param);

            rptHistory.DataSource = param.ListPressAgencyHRHistory;
            rptHistory.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidPressAgencyHRHistoryID.Value = txtContent.Text = string.Empty;
            dpkMeetedDTG.SelectedDate = null;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHRHistory item = rptItem.DataItem as agency_PressAgencyHRHistory;

            HiddenField hidPressAgencyHRHistoryID = rptItem.FindControl("hidPressAgencyHRHistoryID") as HiddenField;
            hidPressAgencyHRHistoryID.Value = Utility.GetString(item.PressAgencyHRHistoryID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrContent", item.Content);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrMeetedDTG", Utility.GetDateString(item.MeetedDTG));
            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
            btnEdit.Visible = btnDelete.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            DatePicker dpkMeetedDTG = rptItem.FindControl("dpkMeetedDTG") as DatePicker;
            dpkMeetedDTG.SelectedDate = item.MeetedDTG;



            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private agency_PressAgencyHRHistory GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_PressAgencyHRHistory result = new agency_PressAgencyHRHistory();

            HiddenField hidPressAgencyHRHistoryID = rptItem.FindControl("hidPressAgencyHRHistoryID") as HiddenField;
            Literal ltrContent = rptItem.FindControl("ltrContent") as Literal;
            DatePicker dpkMeetedDTG = rptItem.FindControl("dpkMeetedDTG") as DatePicker;

            result.PressAgencyHRHistoryID = Utility.GetNullableInt(hidPressAgencyHRHistoryID.Value);
            result.Content = ltrContent.Text;
            result.MeetedDTG = dpkMeetedDTG.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(agency_PressAgencyHRHistory item)
        {
            hidPressAgencyHRHistoryID.Value = Utility.GetString(item.PressAgencyHRHistoryID);
            txtContent.Text = item.Content;
            dpkMeetedDTG.SelectedDate = item.MeetedDTG;
        }

        private agency_PressAgencyHRHistory GetDataFromPopup()
        {
            agency_PressAgencyHRHistory result = new agency_PressAgencyHRHistory();

            result.PressAgencyHRHistoryID = Utility.GetNullableInt(hidPressAgencyHRHistoryID.Value);
            result.PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
            result.Content = txtContent.Text;
            result.MeetedDTG = dpkMeetedDTG.SelectedDate;

            return result;
        }

        private void DeleteItem(agency_PressAgencyHRHistory item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHRHistory);
            param.PressAgencyHRHistory = item;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}