using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SoftMart.Core.Security.Entity;
using System.Collections.Generic;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class Display : BasePagePopup
    {
        public List<IFunctionRight> lstRight { get; set; }
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                uc_RequestItemPermission();
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

        private void uc_RequestItemPermission()
        {
            try
            {
                lstRight = GetPagePermission();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        protected void btnPopupUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFiles();
                popUpload.Hide();
                LoadDataImages();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnShowPopupUpload_Click(object sender, EventArgs e)
        {
            try
            {
                popUpload.Show();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptImage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindImageToRepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptImage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDelete:
                        var attID = Utility.GetNullableInt(e.CommandArgument.ToString());
                        DeleteImage(attID);
                        LoadDataImages();
                        break;
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        #endregion

        #region Biz
        private void SetupForm()
        {
            string key = UIUtility.GetParamId();
            int? empID = 0;
            int[] arrRefParam;

            Utility.Decrypt(key, out empID, out arrRefParam);

            if (empID != CacheManager.Profiles.MyProfile.EmployeeID && arrRefParam.Length != 2)
                Response.Redirect(PageURL.ErrorPage);

            hidPressAgencyID.Value = Utility.GetString(arrRefParam[0]);
            hidPressAgencyHRID.Value = Utility.GetString(arrRefParam[1]);

            lnkEdit.NavigateUrl = string.Format("Edit.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidPressAgencyID.Value).GetValueOrDefault(0), Utility.GetNullableInt(hidPressAgencyHRID.Value).GetValueOrDefault(0) }), GetParamCallbackButton());
        }

        private void LoadData()
        {
            if (!lstRight.Exists(x => x.FunctionCode == FunctionCode.DISPLAY))
            {
                Response.Redirect(PageURL.ErrorPage);
                return;
            }
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyHR_ByID);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value) };
            MainController.Provider.Execute(param);

            lblPressAgencyName.Text = string.IsNullOrWhiteSpace(param.PressAgencyHR?.PressAgencyName) ? "" : "- " + param.PressAgencyHR.PressAgencyName.ToUpper();
            lblHRTypeName.Text = string.IsNullOrWhiteSpace(param.PressAgencyHR?.PressAgencyTypeString) ? "" : "- " + param.PressAgencyHR.PressAgencyTypeString.ToUpper();

            BindObjectToForm(param.PressAgencyHR);

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { param.Attachment?.AttachmentID ?? 0 })), 1000, 600);
            divViewDetailImage.Attributes.Add("onclick", url);

            BindDataImage(img, param.Attachment);

            var pressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);

            ucPressAgencyHRHistory.BindData(pressAgencyHRID, false);

            ucPressAgencyHRAlert.BindData(pressAgencyHRID, false);

            ucPressAgencyHRRelatives.BindData(pressAgencyHRID, false);

            ucPressAgencyHR.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), param.PressAgencyHR.Attitude, param.PressAgencyHR.PressAgencyHRID, false);

            LoadDataImages();

            ucComment.SetupForm();
            ucComment.LoadData(pressAgencyHRID, SMX.CommentRefType.PressAgencyHR, true);
        }

        private void BindObjectToForm(agency_PressAgencyHR item)
        {
            if (item != null)
            {
                ltrFullName.Text = item.FullName;
                ltrPosition.Text = item.Position;
                ltrAttitude.Text = ltrAttitude1.Text = Utility.GetDictionaryValue(SMX.Attitude.dicDesc, item.Attitude);

                switch (item.Attitude)
                {
                    case SMX.Attitude.TichCuc:
                        ltrAttitude.Attributes["class"] = ltrAttitude1.Attributes["class"] = "positive";
                        break;
                    case SMX.Attitude.TieuCuc:
                        ltrAttitude.Attributes["class"] = ltrAttitude1.Attributes["class"] = "negative";
                        break;
                    case SMX.Attitude.TrungLap:
                        ltrAttitude.Attributes["class"] = ltrAttitude1.Attributes["class"] = "medium";
                        break;
                }

                ltrAge.Text = item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty;
                ltrDOB.Text = Utility.GetDateString(item.DOB);
                ltrMobile.Text = item.Mobile;
                ltrEmail.Text = item.Email;
                ltrAddress.Text = item.Address;
                ltrHobby.Text = item.Hobby;
                ltrRelatedInformation.Text = UIUtility.ConvertBreakLine2Html(item.RelatedInformation);
            }
        }

        private void BindImageToRepeaterItem(RepeaterItem rptItem)
        {
            adm_Attachment att = rptItem.DataItem as adm_Attachment;

            LinkButton btnDeleteImage = rptItem.FindControl("btnDeleteImage") as LinkButton;
            btnDeleteImage.CommandArgument = Utility.GetString(att.AttachmentID);
            btnDeleteImage.CommandName = SMX.ActionDelete;

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { att?.AttachmentID ?? 0 })), 1000, 600);
            HtmlGenericControl divViewDetailImage = rptItem.FindControl("divViewDetailImage") as HtmlGenericControl;
            divViewDetailImage.Attributes.Add("onclick", url);

            HtmlImage img = rptItem.FindControl("img") as HtmlImage;
            BindDataImage(img, att);
        }

        private void UploadFiles()
        {
            adm_Attachment item = new adm_Attachment();
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.Description = txtDescription.Text;
            item.RefID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
            item.RefType = SMX.AttachmentRefType.PressAgencyHROtherImage;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void LoadDataImages()
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.LoadDataOtherImage_PressAgencyHR);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value) };
            MainController.Provider.Execute(param);

            divImage.Visible = param.ListAttachment != null && param.ListAttachment.Count > 0;
            rptImage.DataSource = param.ListAttachment;
            rptImage.DataBind();
        }

        private void DeleteImage(int? attID)
        {
            var param = new AttachmentParam(FunctionType.CommonList.Attachment.DeleteDocument);
            param.adm_Attachment = new adm_Attachment() { AttachmentID = attID };
            MainController.Provider.Execute(param);
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