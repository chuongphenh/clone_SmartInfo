using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class OtherImageUC : UserControl
    {
        public event EventHandler RequestSave_PressAgency;

        #region Events

        protected void btnPopupUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFiles();
                popUpload.Hide();
                BindData(Utility.GetNullableInt(hidPressAgencyID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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
                if (RequestSave_PressAgency != null && string.IsNullOrWhiteSpace(hidPressAgencyID.Value))
                {
                    RequestSave_PressAgency(null, null);
                    return;
                }

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
                    BindObjectToRepeater(e.Item);
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

        }

        public void BindData(int? pressAgencyID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyID.Value = Utility.GetString(pressAgencyID);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListOtherImage_ByPressAgencyID);
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
            MainController.Provider.Execute(param);

            rptImage.DataSource = param.ListOtherImage;
            rptImage.DataBind();
        }

        #endregion

        #region Private Methods

        private void UploadFiles()
        {
            adm_Attachment item = new adm_Attachment();
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.Description = txtDescription.Text;
            item.RefID = Utility.GetNullableInt(hidPressAgencyID.Value);
            item.RefType = SMX.AttachmentRefType.PressAgencyOtherImage;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            adm_Attachment att = rptItem.DataItem as adm_Attachment;

            LinkButton btnDeleteImage = rptItem.FindControl("btnDeleteImage") as LinkButton;
            btnDeleteImage.CommandArgument = Utility.GetString(att.AttachmentID);
            btnDeleteImage.CommandName = SMX.ActionDelete;

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { att?.AttachmentID ?? 0 })), 1000, 600);
            HtmlGenericControl divViewDetailImage = rptItem.FindControl("divViewDetailImage") as HtmlGenericControl;
            divViewDetailImage.Attributes.Add("onclick", url);

            Label lblDescription = rptItem.FindControl("lblDescription") as Label;
            lblDescription.Text = att.Description;

            HtmlImage img = rptItem.FindControl("img") as HtmlImage;
            BindDataImage(img, att);
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

        private void DeleteImage(int? attID)
        {
            var param = new AttachmentParam(FunctionType.CommonList.Attachment.DeleteDocument);
            param.adm_Attachment = new adm_Attachment() { AttachmentID = attID };
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}