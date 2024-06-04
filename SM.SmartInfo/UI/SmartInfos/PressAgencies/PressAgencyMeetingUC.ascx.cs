using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class PressAgencyMeetingUC : UserControl
    {
        public event EventHandler RequestSave_PressAgency;

        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetDataFromPopup();

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyMeeting);
                param.PressAgencyMeeting = item;
                MainController.Provider.Execute(param);

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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
                if (RequestSave_PressAgency != null && string.IsNullOrWhiteSpace(hidPressAgencyID.Value))
                {
                    RequestSave_PressAgency(null, null);
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
                    case SMX.ActionUpload:
                        string url = string.Format("/UI/PopupPages/ListAttachments/Edit.aspx?ID={0}", Utils.Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { item.PressAgencyMeetingID.GetValueOrDefault(0), SMX.AttachmentRefType.PressAgencyMeeting }));
                        UIUtility.OpenPopupWindow(this.Page, url);
                        break;
                    case SMX.ActionEdit:
                        BindObjectToPopup(item);
                        popEdit.Show();
                        break;
                    case SMX.ActionDelete:
                        DeleteItem(item);
                        BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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

        public void SetupForm()
        {
            hidPage.Value = "1";
        }

        public void BindData(int? pressAgencyID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyID.Value = Utility.GetString(pressAgencyID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = btnAddNew.Visible = footerEdit.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyMeeting_ByPressAgencyID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniTen
            };
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptHistory.DataSource = param.ListPressAgencyMeeting;
            rptHistory.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidPressAgencyMeetingID.Value = txtPartner.Text = txtLocation.Text = txtContent.Text = string.Empty;
            dpkMeetDTG.SelectedDate = null;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyMeeting item = rptItem.DataItem as agency_PressAgencyMeeting;

            HiddenField hidPressAgencyMeetingID = rptItem.FindControl("hidPressAgencyMeetingID") as HiddenField;
            hidPressAgencyMeetingID.Value = Utility.GetString(item.PressAgencyMeetingID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrContractNo", item.ContractNo);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrContractDTG", Utility.GetDateString(item.ContractDTG));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrPartner", item.Partner);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrLocation", item.Location);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrContent", UIUtility.ConvertBreakLine2Html(item.Content));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrMeetDTG", Utility.GetDateString(item.MeetDTG));

            DatePicker dpkContractDTG = rptItem.FindControl("dpkContractDTG") as DatePicker;
            dpkContractDTG.SelectedDate = item.ContractDTG;

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            DatePicker dpkMeetDTG = rptItem.FindControl("dpkMeetDTG") as DatePicker;
            dpkMeetDTG.SelectedDate = item.MeetDTG;

            LinkButton btnUpload = rptItem.FindControl("btnUpload") as LinkButton;
            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnUpload.CommandName = SMX.ActionUpload;
            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private agency_PressAgencyMeeting GetCurrentRowData(RepeaterItem rptItem)
        {
            agency_PressAgencyMeeting result = new agency_PressAgencyMeeting();

            HiddenField hidPressAgencyMeetingID = rptItem.FindControl("hidPressAgencyMeetingID") as HiddenField;
            Literal ltrContractNo = rptItem.FindControl("ltrContractNo") as Literal;
            DatePicker dpkContractDTG = rptItem.FindControl("dpkContractDTG") as DatePicker;
            Literal ltrPartner = rptItem.FindControl("ltrPartner") as Literal;
            Literal ltrLocation = rptItem.FindControl("ltrLocation") as Literal;
            Literal ltrContent = rptItem.FindControl("ltrContent") as Literal;
            DatePicker dpkMeetDTG = rptItem.FindControl("dpkMeetDTG") as DatePicker;

            result.PressAgencyMeetingID = Utility.GetNullableInt(hidPressAgencyMeetingID.Value);
            result.ContractNo = ltrContractNo.Text;
            result.ContractDTG = dpkContractDTG.SelectedDate;
            result.Partner = ltrPartner.Text;
            result.Location = ltrLocation.Text;
            result.Content = UIUtility.ConvertHtml2BreakLine(ltrContent.Text);
            result.MeetDTG = dpkMeetDTG.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(agency_PressAgencyMeeting item)
        {
            hidPressAgencyMeetingID.Value = Utility.GetString(item.PressAgencyMeetingID);
            txtContractNo.Text = item.ContractNo;
            dpkContractDTG.SelectedDate = item.ContractDTG;
            txtPartner.Text = item.Partner;
            txtPartner.Text = item.Partner;
            txtLocation.Text = item.Location;
            txtContent.Text = item.Content;
            dpkMeetDTG.SelectedDate = item.MeetDTG;
        }

        private agency_PressAgencyMeeting GetDataFromPopup()
        {
            agency_PressAgencyMeeting result = new agency_PressAgencyMeeting();

            result.PressAgencyMeetingID = Utility.GetNullableInt(hidPressAgencyMeetingID.Value);
            result.PressAgencyID = Utility.GetNullableInt(hidPressAgencyID.Value);
            result.ContractNo = txtContractNo.Text;
            result.ContractDTG = dpkContractDTG.SelectedDate;
            result.Partner = txtPartner.Text;
            result.Location = txtLocation.Text;
            result.Content = txtContent.Text;
            result.MeetDTG = dpkMeetDTG.SelectedDate;

            return result;
        }

        private void DeleteItem(agency_PressAgencyMeeting item)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyMeeting);
            param.PressAgencyMeeting = item;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}