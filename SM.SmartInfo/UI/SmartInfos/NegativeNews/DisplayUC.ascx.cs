using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using SM.SmartInfo.Service.Reporting.Engine;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using static SM.SmartInfo.UI.SmartInfos.NegativeNews.Default;
using System.IO;

namespace SM.SmartInfo.UI.SmartInfos.NegativeNews
{
    public partial class DisplayUC : BaseUserControl
    {
        public event EventHandler RequestEdit;

        public event EventHandler RequestFinish;

        public delegate void RequestPermission(RequestPermissionArgs param);

        public event RequestPermission RequestItemPermission;

        #region Events

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestEdit != null)
                    RequestEdit(null, null);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                int? itemID = Utility.GetNullableInt(hidNewsID.Value);

                if (itemID != null)
                {
                    var param = new NewsParam(FunctionType.NegativeNew.UpdateStatusHoanThanh);
                    param.News = new SharedComponent.Entities.News() { NewsID = itemID };
                    MainController.Provider.Execute(param);

                    ShowMessage("Hoàn thành thành công");

                    if (RequestFinish != null)
                        RequestFinish(null, null);
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int? itemID = Utility.GetNullableInt(hidNewsID.Value);

                if (itemID != null)
                {
                    var param = new NewsParam(FunctionType.NegativeNew.DeleteItem);
                    param.NewsID = itemID;
                    MainController.Provider.Execute(param);
                }

                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    {"NewsID", hidNewsID.Value},
                };

                Export(SMX.TemplateExcel.ExcelExport_NegativeNews, param);
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

        #region Public Methods

        public void BindData(SharedComponent.Entities.News news)
        {
            hidNewsID.Value = Utility.GetString(news.NewsID);

            BindObject2Form(news);

            ucComment.SetupForm();
            ucComment.LoadData(news.NewsID, SMX.CommentRefType.NegativeNews, true);

            RequestButtonPermisstion(news);
        }

        #endregion

        #region Private Methods

        private void RequestButtonPermisstion(SharedComponent.Entities.News news)
        {
            if (RequestItemPermission != null)
            {
                var param = new RequestPermissionArgs();
                RequestItemPermission(param);

                if (!param.lstRight.Exists(x => x.FunctionCode == FunctionCode.DISPLAY))
                    Response.Redirect(PageURL.ErrorPage);

                btnEdit.Visible
                    = btnShowPopupUpload.Visible
                    = param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT) && (Profiles.MyProfile.UserName == news.CreatedBy || Profiles.MyProfile.ListFixedPermissionCode.Contains(SMX.FixedBusinessPermissionCode.CG));

                btnFinish.Visible = news.Status != SMX.News.Status.HoanThanh && btnEdit.Visible;

                btnDelete.Visible = param.lstRight.Exists(x => x.FunctionCode == FunctionCode.DELETE) && (Profiles.MyProfile.UserName == news.CreatedBy || Profiles.MyProfile.ListFixedPermissionCode.Contains(SMX.FixedBusinessPermissionCode.CG));

                foreach (RepeaterItem rptItem in rptImage.Items)
                {
                    LinkButton btnDeleteImage = rptItem.FindControl("btnDeleteImage") as LinkButton;
                    btnDeleteImage.Visible = param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT) && (Profiles.MyProfile.UserName == news.CreatedBy || Profiles.MyProfile.ListFixedPermissionCode.Contains(SMX.FixedBusinessPermissionCode.CG));
                }
            }
        }

        private void BindObject2Form(SharedComponent.Entities.News news)
        {
            //ucListNagativeNews.SetupForm();

            if (news != null && news.NewsID.HasValue)
            {
                ltrName.Text = news.Name;
                ltrNegativeType.Text = Utility.GetDictionaryValue(SMX.News.NegativeNews.dicType, news.NegativeType);
                ltrIncurredDTG.Text = Utility.GetDateTimeString(news.IncurredDTG, "HH:mm - dd/MM/yyyy");
                ltrClassification.Text = Utility.GetDictionaryValue(SMX.News.Classification.dicClassification, news.Classification);

                switch (news.NegativeType)
                {
                    case SMX.News.NegativeNews.ChuaPhatSinh:
                        ltrNegativeType.Attributes.Add("class", "chua-phat-sinh");
                        //lblTitlePressAgency.Text = "Cơ quan báo chí liên hệ";
                        break;
                    case SMX.News.NegativeNews.DaPhatSinh:
                        ltrNegativeType.Attributes.Add("class", "da-phat-sinh");
                        //lblTitlePressAgency.Text = "Các báo đăng tải";
                        break;
                }

                switch (news.Classification)
                {
                    case SMX.News.Classification.QuanTrong:
                        spanClassification.Attributes.Add("class", "quan-trong");
                        break;
                    case SMX.News.Classification.TrungBinh:
                        spanClassification.Attributes.Add("class", "trung-binh");
                        break;
                    case SMX.News.Classification.BinhThuong:
                        spanClassification.Attributes.Add("class", "binh-thuong");
                        break;
                }

                ltrStatus.Text = Utils.Utility.GetDictionaryValue(SMX.News.Status.dicDesc, news.Status);

                if (news.Status == SMX.News.Status.HoanThanh)
                {
                    spanStatus.Attributes.Add("class", "done");
                    iStatus.Attributes.Add("class", "fa fa-check fa-negative-news-done");
                }
                else
                {
                    spanStatus.Attributes.Add("class", "inprogress");
                    iStatus.Attributes.Add("class", "fa fa-arrow-right fa-negative-news-inprogress");
                }

                //ltrRatedLevel.Text = UIUtility.ConvertBreakLine2Html(news.RatedLevel);
                ltrPressAgency.Text = UIUtility.ConvertBreakLine2Html(news.PressAgency);
                //ltrResolution.Text = UIUtility.ConvertBreakLine2Html(news.Resolution);
                //ltrResolutionContent.Text = UIUtility.ConvertBreakLine2Html(news.ResolutionContent);
                ltrConcluded.Text = UIUtility.ConvertBreakLine2Html(news.Concluded);

                if (news.ListAttachment != null && news.ListAttachment.Count > 0)
                {
                    divImage.Visible = true;

                    rptImage.DataSource = news.ListAttachment;
                    rptImage.DataBind();
                }
                else
                    divImage.Visible = false;

                //ucNewsResearched.BindData(news.NewsID, btnEdit.Visible);
                //ucListNagativeNews.BindData(btnEdit.Visible, news.NewsID);
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
            string file = Path.GetExtension(img.Src);   
            if (file == ".pdf")
            {
                img.Src = "../../../Images/iconpdf.png";
                img.Style.Add("object-fit", "contain");
            }
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

        private void UploadFiles()
        {
            adm_Attachment item = new adm_Attachment();
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.Description = txtDescription.Text;
            item.RefID = Utility.GetNullableInt(hidNewsID.Value);
            item.RefType = SMX.AttachmentRefType.NegativeNews;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void LoadDataImages()
        {
            var param = new NewsParam(FunctionType.NegativeNew.LoadDataImages);
            param.NewsID = Utility.GetNullableInt(hidNewsID.Value);
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

        private void Export(string xmlFile, Dictionary<string, object> param, string ouputFileName = "")
        {
            ReportingEngine engine = new ReportingEngine(xmlFile);
            engine.SetParameters(param);
            var fileContent = engine.Render();

            if (string.IsNullOrWhiteSpace(ouputFileName))
                ouputFileName = engine.SaveAsFileName + ".xlsx";

            UIUtilities.ExportHelper.PushToDownload(fileContent, SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_XLSX, ouputFileName);
        }

        #endregion
    }
}