using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Security.Policy;
using System.Windows;
using System.Web.UI;
using System.IO;

namespace SM.SmartInfo.UI.PopupPages.ViewImages
{
    public partial class Display : BasePagePopup
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

            if (empID != Profiles.MyProfile.EmployeeID && arrRefParam.Length != 2)
                Response.Redirect(PageURL.ErrorPage);

            hidAttID.Value = Utility.GetString(arrRefParam[0]);
            hidNewsID.Value = Request.QueryString["NewsID"];
        }

        private void LoadData()
        {
            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.GetAttachmentByID);
            param.AttachmentID = Utility.GetNullableInt(hidAttID.Value);
            MainController.Provider.Execute(param);
            ViewState["CurrentImage"] = param.adm_Attachment;
            BindObjectToForm(param.adm_Attachment);
        }

        private void BindObjectToForm(adm_Attachment att)
        {
            if (att != null)
            {
                img.Alt = att.Description;
                var trimUrl = GetImageURL(att);
                string file = Path.GetExtension(trimUrl);
                if (file == ".pdf")
                {
                    pdfViewer.Src = GetImageURL(att);
                    pdfViewer.Visible = true; // Hiển thị <iframe>
                    img.Visible = false;     // Ẩn <img>
                }
                else
                {
                    img.Src = GetImageURL(att);
                    pdfViewer.Visible = false; // Ẩn <iframe>
                    img.Visible = true;       // Hiển thị <img>
                }

                lblDescription.Text = att.Description;
                if (att.CreatedDTG != null)
                {
                    lblCreatedDTG.Text = Convert.ToDateTime(att.CreatedDTG).ToString("dd-MM-yyyy");
                }
            }
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

        protected void btnTrigger_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(hidCroppedImage.Value) && ViewState["CurrentImage"] != null)
            {
                var currentImage = ViewState["CurrentImage"] as adm_Attachment;
                var imgInfo = hidCroppedImage.Value;
                var fileType = imgInfo.Split(';')[0].Split(':')[1];
                byte[] fileContent = Convert.FromBase64String(imgInfo.Split(',')[1]);
                var fileSize = fileContent.Length;
                var croppedImage = new adm_Attachment()
                {
                    AttachmentID = currentImage.AttachmentID,
                    FileName = currentImage.FileName,
                    RefType = SMX.AttachmentRefType.News,
                    CreatedBy = Profiles.MyProfile.UserName,
                    CreatedDTG = DateTime.Now,
                    FileSize = fileSize,
                    DisplayName = currentImage.FileName,
                    ContentType = fileType,
                    RefID = currentImage.RefID,
                    FileContent = fileContent,
                };

                AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Replace);
                param.adm_Attachment = croppedImage;
                MainController.Provider.Execute(param);

                hidAttID.Value = param.AttachmentID.ToString();
                LoadData();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "MyScript", "reloadEditPage();", true);
        }
    }
}