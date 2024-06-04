using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.CacheManager;
using System;
using System.Web.UI.HtmlControls;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class PressAgencyHRUC : UserControl
    {
        public event EventHandler RequestSave_PressAgency;

        #region Events

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestSave_PressAgency != null && string.IsNullOrWhiteSpace(hidPressAgencyID.Value))
                {
                    RequestSave_PressAgency(null, null);
                    return;
                }

                string url = string.Format("/UI/PopupPages/PressAgencyHRs/Edit.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidPressAgencyID.Value).GetValueOrDefault(0), 0 }), btnReloadAppendix.ClientID);
                UIUtility.OpenPopupWindow(this.Page, url, 1300, 700);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnReloadAppendix_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));

                var page = Request.Params["P"];
                string url = string.Format("Default.aspx?ID={0}&P={1}", hidPressAgencyID.Value, string.IsNullOrWhiteSpace(page) ? "1" : page);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptHR_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptHR_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDelete:
                        var pressAgencyHRID = Utility.GetNullableInt(e.CommandArgument.ToString());

                        PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHR);
                        param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = pressAgencyHRID };
                        MainController.Provider.Execute(param);

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
            btnAddNew.Visible = isEdit.GetValueOrDefault(false);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHR_ByPressAgencyID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = 4
            };
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
            MainController.Provider.Execute(param);

            rptHR.DataSource = param.ListPressAgencyHR;
            rptHR.DataBind();
        }

        #endregion

        #region Private Methods

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHR item = rptItem.DataItem as agency_PressAgencyHR;

            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
            btnDelete.CommandName = SMX.ActionDelete;
            btnDelete.CommandArgument = Utility.GetString(item.PressAgencyHRID);

            HyperLink lnkEdit = rptItem.FindControl("lnkEdit") as HyperLink;
            string urlEdit = UIUtility.BuildHyperlinkWithPopup("<i style=\"font-size: 16px\" class=\"fas fa-pencil-alt\"></i>", string.Format("~/UI/PopupPages/PressAgencyHRs/Edit.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidPressAgencyID.Value).GetValueOrDefault(0), item.PressAgencyHRID.GetValueOrDefault(0) }), btnReloadAppendix.ClientID), 1300, 700);
            lnkEdit.Text = urlEdit;

            lnkEdit.Visible = btnDelete.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/PressAgencyHRs/Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidPressAgencyID.Value).GetValueOrDefault(0), item.PressAgencyHRID.GetValueOrDefault(0) }), btnReloadAppendix.ClientID), 1300, 700);
            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            divLink.Attributes.Add("onclick", url);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrPosition", item.Position);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrFullName", item.FullName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAge", item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrDOB", Utility.GetDateString(item.DOB));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrEmail", item.Email);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrMobile", item.Mobile);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAddress", item.Address);

            Label ltrAttitude = rptItem.FindControl("ltrAttitude") as Label;
            ltrAttitude.Text = Utility.GetDictionaryValue(SMX.Attitude.dicDesc, item.Attitude);
            switch (item.Attitude)
            {
                case SMX.Attitude.TichCuc:
                    ltrAttitude.Attributes["class"] = "positive";
                    break;
                case SMX.Attitude.TieuCuc:
                    ltrAttitude.Attributes["class"] = "negative";
                    break;
                case SMX.Attitude.TrungLap:
                    ltrAttitude.Attributes["class"] = "medium";
                    break;
            }

            HtmlImage img = rptItem.FindControl("img") as HtmlImage;
            BindDataImage(img, item.Attachment);
        }

        private void BindDataImage(HtmlImage imgUI, adm_Attachment imgData)
        {
            if (imgData != null)
            {
                imgUI.Alt = imgData.Description;
                imgUI.Src = GetImageURL(imgData);
            }
            else
                imgUI.Src = SMX.DefaultImage;
        }

        private string GetImageURL(adm_Attachment image)
        {
            string url = SMX.DefaultImage;

            if (image != null && image.FileContent != null)
            {
                string imageFileName = string.Format("{0}_{1}", image.AttachmentID, image.FileName);
                string imageFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository", "ECM");
                if (!System.IO.Directory.Exists(imageFilePath))
                    System.IO.Directory.CreateDirectory(imageFilePath);
                imageFilePath = System.IO.Path.Combine(imageFilePath, imageFileName);
                if (!System.IO.File.Exists(imageFilePath))
                    System.IO.File.WriteAllBytes(imageFilePath, image.FileContent);
                url = "~/Repository/ECM/" + imageFileName;
            }

            return ResolveUrl(url);
        }

        #endregion
    }
}