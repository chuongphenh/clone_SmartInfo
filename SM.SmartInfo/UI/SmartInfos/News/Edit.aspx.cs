using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Web;
using System.IO;
using System.Web.DynamicData;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class Edit : BasePage
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
                    ucNews.Visible = false;
                    SetupForm();
                    LoadDataImages();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateItem(true);
                PushNotiNews(Convert.ToInt32(hidId.Value));
                Response.Redirect(string.Format(PageURL.Display, hidId.Value));
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        private void PushNotiNews(int newsID)
        {
            SharedComponent.Entities.News item = GetData();
            ntf_Notification itemNoti = new ntf_Notification();
            NotificationParam pushNotiPram = new NotificationParam(FunctionType.Notification.PushNotification);
            itemNoti.NotificationID = newsID;
            itemNoti.Content = item?.Name;
            itemNoti.Type = SMX.CommentRefType.News;
            pushNotiPram.Notification = itemNoti;
            pushNotiPram.Type = 2;
            pushNotiPram.TypeNoti = "TinTucSuVuMoi";
            MainController.Provider.Execute(pushNotiPram);
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format(PageURL.Display, GetIntIdParam()));
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

        protected void btnSaveContinues_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateItem(false);

                ShowMessage("Lưu thành công");
                LoadData();
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

        private void SetupForm()
        {
            hidId.Value = Utility.GetString(GetIntIdParam());
            
            ucCatalogNewsSelectorTree.LoadData();

            UIUtility.BindDicToDropDownList(ddlCategory, SMX.NewsCategory.dicDesc);
            
        }

        private void LoadData()
        {
            var param = new NewsParam(FunctionType.News.LoadDataNews);
            param.NewsID = Utility.GetNullableInt(hidId.Value);
            MainController.Provider.Execute(param);

            

            BindObjectToForm(param.News);

            rptImage.DataSource = param.ListAttachment;
            rptImage.DataBind();

            ucCampaignNews.SetupForm();
            ucCampaignNews.BindData(param.NewsID, true);

            ucNews.SetupForm();
            ucNews.BindData(param.NewsID, true);

            ucSpecificResults.SetupForm(param.NewsID);
            ucSpecificResults.BindData(param.NewsID, true);

            ucComment.SetupForm();
            ucComment.LoadData(Utility.GetNullableInt(hidId.Value), SMX.CommentRefType.News, true);
            IsSingleCamp();
            if (!(Profiles.MyProfile.UserName == param.News.CreatedBy || Profiles.MyProfile.ListFixedPermissionCode.Contains(SMX.FixedBusinessPermissionCode.CG)))
                Response.Redirect(PageURL.ErrorPage);
        }

        private int? UpdateItem(bool isSaveComplete)
        {
            //Binding object
            SharedComponent.Entities.News item = GetData();

            if (!string.IsNullOrEmpty(item.Hastag))
            {
                NewsParam hastagPram = new NewsParam(FunctionType.News.IsNameExists);
                hastagPram.Hastag = item.Hastag;
                MainController.Provider.Execute(hastagPram);

                if (!hastagPram.IsNameExists)
                {
                    NewsParam hastagSave = new NewsParam(FunctionType.News.CreateHastag);
                    hastagSave.Hastag = item.Hastag;
                    MainController.Provider.Execute(hastagSave);
                }
            }

            //Add
            NewsParam param = new NewsParam(FunctionType.News.UpdateNews);
            param.News = item;
            param.IsSaveComplete = isSaveComplete;
            MainController.Provider.Execute(param);

            return item?.NewsID;
        }
       
        private SharedComponent.Entities.News GetData()
        {
            SharedComponent.Entities.News item = new SharedComponent.Entities.News();

            item.NewsID = Utility.GetNullableInt(hidId.Value);
            item.PostingFromDTG = dpkPostingFromDTG.SelectedDate;
            item.PostingToDTG = dpkPostingToDTG.SelectedDate;
            item.CatalogID = Utility.GetNullableInt(ucCatalogNewsSelectorTree.SelectedValue);
            item.Name = txtName.Text;
            item.NumberOfPublish = (int?)numNumberOfPublish.Value;
            item.Content = txtContent.Text;
            item.Category = Utility.GetNullableInt(ddlCategory.SelectedValue);
            item.DisplayOrder = (int?)numDisplayOrder.Value;
            item.Hastag = txtHastag.Text;
            return item;
        }

        private void BindObjectToForm(SharedComponent.Entities.News item)
        {
            dpkPostingFromDTG.SelectedDate = item.PostingFromDTG;
            dpkPostingToDTG.SelectedDate = item.PostingToDTG;
            ucCatalogNewsSelectorTree.SelectedValue = Utility.GetString(item.CatalogID);
            txtName.Text = item.Name;
            numNumberOfPublish.Value = item.NumberOfPublish;
            txtContent.Text = item.Content;
            ddlCategory.SelectedValue = Utility.GetString(item.Category);
            numDisplayOrder.Value = item.DisplayOrder;
            txtHastag.Text = item.Hastag;
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

            SetupForm();
            LoadDataImages();
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this      , FunctionCode.EDIT },
                };
            }
        }

        /*protected void btnTrigger_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(hidEditable.Value) && Convert.ToBoolean(hidEditable.Value) == true)
            {
                var newsId = UpdateItem(false);

                foreach (HttpPostedFile postedFile in filesUpload.PostedFiles)
                {
                    if (postedFile.ContentLength > 0)
                    {
                        FileInfo fi = new FileInfo(postedFile.FileName);
                        string ext = fi.Extension;
                        if (!SMX.AcceptFiles.lstCommonDocument.Exists(c => c.Equals(ext)))
                            throw new SMXException(Messages.AcceptFiles.CommonDocument);

                        adm_Attachment attachment = new adm_Attachment();
                        attachment.FileName = postedFile.FileName;
                        attachment.RefType = SMX.AttachmentRefType.News;
                        attachment.CreatedBy = Profiles.MyProfile.UserName;
                        attachment.CreatedDTG = DateTime.Now;
                        attachment.FileSize = postedFile.ContentLength;
                        attachment.DisplayName = postedFile.FileName;
                        attachment.ContentType = postedFile.ContentType;
                        attachment.RefID = newsId;
                        byte[] fileContent = new byte[postedFile.ContentLength];
                        postedFile.InputStream.Read(fileContent, 0, postedFile.ContentLength);
                        attachment.FileContent = fileContent;

                        AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
                        param.adm_Attachment = attachment;
                        MainController.Provider.Execute(param);
                    }
                }
                hidEditable.Value = "false";

                SetupForm();

                LoadDataImages();
            }
        }*/
    }
}