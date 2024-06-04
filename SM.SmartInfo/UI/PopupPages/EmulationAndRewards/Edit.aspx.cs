using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.PopupPages.EmulationAndRewards
{
    public partial class Edit : BasePagePopup
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        #endregion

        #region Biz
        private void SetupForm()
        {
            if (Profiles.MyProfile == null)
            {
                string oldPage = Request.Url.PathAndQuery;
                string newPage = string.Format(PageURL.LoginPageWithReturn, Server.UrlEncode(oldPage));
                Response.Redirect(newPage);
            }
            string key = UIUtility.GetParamId();
            int? empID = 0;
            int[] arrRefParam;

            Utility.Decrypt(key, out empID, out arrRefParam);

            if (empID != Profiles.MyProfile.EmployeeID && arrRefParam.Length != 1)
                Response.Redirect(PageURL.ErrorPage);

            hidSubjectID.Value = Utility.GetString(arrRefParam[0]);
            lnkExit.NavigateUrl = string.Format("Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidSubjectID.Value).GetValueOrDefault(0) }));
        }

        private void LoadData()
        {
            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListEmulationAndRewardHistory);
            param.er_EmulationAndRewardHistory = new er_EmulationAndRewardHistory()
            {
                EmulationAndRewardSubjectID = Utility.GetNullableInt(hidSubjectID.Value),
            };
            MainController.Provider.Execute(param);

            BindSubject2Form(param.er_EmulationAndRewardSubject);

            grdData.DataSource = param.ListEmulationAndRewardHistory;
            grdData.DataBind();

            grdAtt.DataSource = param.ListAttachment;
            grdAtt.DataBind();
        }

        private void BindSubject2Form(er_EmulationAndRewardSubject item)
        {
            ltrCode.Text = item.Code;
            txtName.Text = item.Name;
            txtUnit.Text = item.Unit;
            txtEmail.Text = item.Email;
            txtMobile.Text = item.Mobile;
        }

        #endregion

        protected void grdData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    er_EmulationAndRewardHistory item = e.Item.DataItem as er_EmulationAndRewardHistory;

                    UIUtility.SetGridItemIText(e.Item, "ltrTitle", item.Title);
                    UIUtility.SetGridItemIText(e.Item, "ltrEmulationAndRewardUnit", item.EmulationAndRewardUnit);
                    UIUtility.SetGridItemIText(e.Item, "ltrRewardedDTG", Utility.GetDateString(item.RewardedDTG));
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdAtt_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    adm_Attachment item = e.Item.DataItem as adm_Attachment;

                    UIUtility.SetGridItemHidden(e.Item, "hidAttachmentID", item.AttachmentID);

                    UIUtility.SetGridItemIText(e.Item, "ltrDocumentName", item.DocumentName);

                    string docName = string.IsNullOrWhiteSpace(item.Description) ? item.FileName : item.Description;
                    UIUtility.SetGridItemIText(e.Item, "ltrName", docName);

                    UIUtility.SetGridItemIText(e.Item, "ltrCreatedDTG", Utility.GetDateTimeString(item.CreatedDTG));
                    UIUtility.SetGridItemIText(e.Item, "ltrCreatedBy", item.FullNameCreateBy);

                    if (item.AttachmentID.HasValue)
                    {
                        int? empID = Profiles.MyProfile.EmployeeID;
                        var urlDownLoad = string.Format(PageURL.DownloadDocument, Utility.Encrypt(empID, item.AttachmentID));
                        urlDownLoad = UIUtility.BuildHyperlinkWithPopup(
                            "<i class=\"fas fa-cloud-download-alt\" aria-hidden=\"true\" style=\"color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;\" title=\"Tải về\"></i>", urlDownLoad);
                        UIUtility.SetGridItemIText(e.Item, "lblDownLoad", urlDownLoad);
                    }
                }
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
                er_EmulationAndRewardSubject sub = new er_EmulationAndRewardSubject()
                {
                    EmulationAndRewardSubjectID = Utility.GetNullableInt(hidSubjectID.Value),
                    Name = txtName.Text,
                    Unit = txtUnit.Text,
                    Email = txtEmail.Text,
                    Mobile = txtMobile.Text,
                };

                EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.SaveEmulationAndRewardSubject);
                param.er_EmulationAndRewardSubject = sub;
                MainController.Provider.Execute(param);

                ClickParentButton(GetParamCallbackButton(), string.Format("Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidSubjectID.Value).GetValueOrDefault(0) }), GetParamCallbackButton()));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
    }
}