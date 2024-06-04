using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.Service.Reporting.Engine;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class Display : BasePage
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                (this.Master as MasterPages.Common.SmartInfo).Search += News_Search;
                ucCampaignNews.Save_CampaignNews += ucCampaignNews_Save_CampaignNews;

                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        protected void IsSingleCamp()
        {
            var param = new NewsParam(FunctionType.News.IsSingleCamp);
            param.NewsID = Utility.GetNullableInt(hidId.Value);
            MainController.Provider.Execute(param);
            if (param.IsSingleCamp == true)
            {
                tuyenBai_tinLe.SelectedValue = "1";
                ucCampaignNews.Visible = false;
                ucNews.Visible = true;
            }
            else
            {
                tuyenBai_tinLe.SelectedValue = "0";
                ucCampaignNews.Visible = true;
                ucNews.Visible = false;
            }
        }
        protected void tuyenBai_tinLe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tuyenBai_tinLe.SelectedValue == "0")
            {
                ucCampaignNews.Visible = true;
                ucNews.Visible = false;
            }
            else if (tuyenBai_tinLe.SelectedValue == "1")
            {
                ucCampaignNews.Visible = false;
                ucNews.Visible = true;
            }
        }
        private void News_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, SMX.TypeSearch.News));
        }

        private void ucCampaignNews_Save_CampaignNews(object sender, EventArgs e)
        {
            ucSpecificResults.SetupForm(Utility.GetNullableInt(hidId.Value));
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int itemID = base.GetIntIdParam();
                Response.Redirect(string.Format(PageURL.Edit, itemID));
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int itemID = base.GetIntIdParam();

                var param = new NewsParam(FunctionType.News.DeleteNewsAndPositiveNewsAndCampaignNews);
                param.NewsID = itemID;
                MainController.Provider.Execute(param);

                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    {"NewsID", hidId.Value},
                };

                Export(SMX.TemplateExcel.ExcelExport_News, param);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
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
                ShowError(ex);
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
                ShowError(ex);
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
                ShowError(ex);
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

        protected void ucSpecificResults_Complete(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion

        #region Private Methods

        public void SetupForm()
        {
            hidId.Value = Utility.GetString(GetIntIdParam());
        }

        private void LoadData()
        {
            int itemID = base.GetIntIdParam();

            var param = new NewsParam(FunctionType.News.LoadDataNews);
            param.NewsID = itemID;
            MainController.Provider.Execute(param);

            BindObjectToForm(param.News);

            spanEdit.Visible = spanDelete.Visible = spanUpload.Visible = (Profiles.MyProfile.UserName == param.News.CreatedBy || Profiles.MyProfile.ListFixedPermissionCode.Contains(SMX.FixedBusinessPermissionCode.CG));

            if (param.ListAttachment != null && param.ListAttachment.Count > 0)
            {
                rptImage.DataSource = param.ListAttachment;
                rptImage.DataBind();
            }
            else
                divImage.Visible = false;

            ucCampaignNews.SetupForm();
            ucCampaignNews.BindData(param.NewsID, spanEdit.Visible && btnEdit.Visible);

            ucNews.SetupForm();
            ucNews.BindData(param.NewsID, spanEdit.Visible && btnEdit.Visible);

            ucSpecificResults.SetupForm(param.NewsID);
            ucSpecificResults.BindData(param.NewsID, spanEdit.Visible && btnEdit.Visible);

            ucSideBar.LoadData();

            ucComment.SetupForm();
            ucComment.LoadData(itemID, SMX.CommentRefType.News, true);
            IsSingleCamp();
        }

        public void BindObjectToForm(SharedComponent.Entities.News item)
        {
            ltrPostingFromDTG.Text = Utility.GetDateString(item.PostingFromDTG);
            ltrPostingToDTG.Text = Utility.GetDateString(item.PostingToDTG);
            ltrCatalogID.Text = item.CatalogName;
            ltrCategory.Text = Utility.GetDictionaryValue(SMX.NewsCategory.dicDesc, item.Category);
            ltrDisplayOrder.Text = Utility.GetString(item.DisplayOrder);
            ltrName.Text = item.Name;
            ltrContent.Text = UIUtility.ConvertBreakLine2Html(item.Content);
            ltrNumberOfPublish.Text = Utility.GetString(item.NumberOfPublish);
            txtHastag.Text = item.Hastag;
        }

        private void BindImageToRepeaterItem(RepeaterItem rptItem)
        {
            adm_Attachment att = rptItem.DataItem as adm_Attachment;

            LinkButton btnDeleteImage = rptItem.FindControl("btnDeleteImage") as LinkButton;
            btnDeleteImage.CommandArgument = Utility.GetString(att.AttachmentID);
            btnDeleteImage.CommandName = SMX.ActionDelete;
            btnDeleteImage.Visible = spanEdit.Visible && btnEdit.Visible;

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { att?.AttachmentID ?? 0 })), 1000, 600);
            HtmlGenericControl divViewDetailImage = rptItem.FindControl("divViewDetailImage") as HtmlGenericControl;
            divViewDetailImage.Attributes.Add("onclick", url);

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

        private void UploadFiles()
        {
            adm_Attachment item = new adm_Attachment();
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.Description = txtDescription.Text;
            item.RefID = Utility.GetNullableInt(hidId.Value);
            item.RefType = SMX.AttachmentRefType.News;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void LoadDataImages()
        {
            var param = new NewsParam(FunctionType.News.LoadDataImagesNews);
            param.NewsID = Utility.GetNullableInt(hidId.Value);
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

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this                  , FunctionCode.DISPLAY },
                    { btnEdit               , FunctionCode.EDIT },
                    { btnShowPopupUpload    , FunctionCode.EDIT },
                    { btnDelete             , FunctionCode.DELETE },
                };
            }
        }
    }
}