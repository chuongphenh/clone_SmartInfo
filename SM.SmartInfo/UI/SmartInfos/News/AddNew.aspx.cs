using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.IO;
using System.Web;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class AddNew : BasePage
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ucCampaignNews.RequestSave_News += RequestSave_News;
                ucNews.RequestSave_SingleNews += NewsUC_RequestSave_SingleNews;
                ucSpecificResults.RequestSave_News += RequestSave_News;

                if (!IsPostBack)
                {
                    ucNews.Visible = false;
                    SetupForm();
                }
                ucCampaignNews.Visible = true;
                ucNews.Visible = false;
            }
            catch (SMXException ex)
            {
                ShowError(ex);
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
        private void NewsUC_RequestSave_SingleNews(object sender, EventArgs e)
        {
            try
            {
                throw new SMXException("Vui lòng bấm Lưu trước khi thực hiện thao tác này");
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private void RequestSave_News(object sender, EventArgs e)
        {
            try
            {
                throw new SMXException("Vui lòng bấm Lưu trước khi thực hiện thao tác này");
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var newsID = UpdateItem(true);
                PushNotiNews(newsID);
                Response.Redirect(string.Format(PageURL.Display, newsID));
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        private void PushNotiNews(int? newsID)
        {
            SharedComponent.Entities.News item = GetData();
            ntf_Notification itemNoti = new ntf_Notification();
            NotificationParam pushNotiPram = new NotificationParam(FunctionType.Notification.PushNotification);
            itemNoti.NotificationID = newsID ?? 0;
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
                Response.Redirect(PageURL.Default);
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
                LogManager.Pentest.LogDebug("Save continues");
                var newsID = UpdateItem(false);
                LogManager.Pentest.LogDebug("Lưu thành công");
                ShowMessage("Lưu thành công");
                Response.Redirect(string.Format(PageURL.Edit, newsID));
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
                throw new SMXException("Vui lòng bấm Lưu trước khi thực hiện thao tác này");
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        #endregion

        #region Private Methods

        private void SetupForm()
        {
            ucCatalogNewsSelectorTree.LoadData();

            ucCampaignNews.SetupForm();
            ucCampaignNews.BindData(null, true);

            ucNews.SetupForm();
            ucNews.BindData(null, true);

            ucSpecificResults.SetupForm(null);
            ucSpecificResults.BindData(null, true);

            UIUtility.BindDicToDropDownList(ddlCategory, SMX.NewsCategory.dicDesc);
        }

        public int? UpdateItem(bool isSaveComplete)
        {
            LogManager.Pentest.LogDebug("bat dau UpdateItem");
            //Binding object
            SharedComponent.Entities.News item = GetData();
            LogManager.Pentest.LogDebug("get date  News item");

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
            NewsParam param = new NewsParam(FunctionType.News.AddNewNews);
            param.News = item;
            param.IsSaveComplete = isSaveComplete;
            MainController.Provider.Execute(param);

            return param.News?.NewsID;
        }

        private SharedComponent.Entities.News GetData()
        {
            SharedComponent.Entities.News item = new SharedComponent.Entities.News();

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

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this      , FunctionCode.ADD },
                };
            }
        }

        /*[System.Web.Services.WebMethod]
        public static List<string> SearchHastag(string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                List<string> searchResults = new List<string>();

                NewsParam param = new NewsParam(FunctionType.News.GetListHastag);
                param.Hastag = searchValue;
                MainController.Provider.Execute(param);

                foreach(var item in param.ListHastag)
                {
                    searchResults.Add(item.ToString());
                }

                return searchResults;
            }
            return null;
        }*/


        /*protected void btnTrigger_Click(object sender, EventArgs e)
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
            Response.Redirect(string.Format(PageURL.Edit, newsId));
        }*/
       
    }
}