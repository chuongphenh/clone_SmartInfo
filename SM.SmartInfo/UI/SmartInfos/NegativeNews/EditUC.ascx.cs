using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using static SM.SmartInfo.UI.SmartInfos.NegativeNews.Default;
using System.IO;

namespace SM.SmartInfo.UI.SmartInfos.NegativeNews
{
    public partial class EditUC : BaseUserControl
    {
        public event EventHandler RequestExit;

        public delegate void SaveContinue(int? newsID);

        public event SaveContinue RequestSaveContinue;

        public delegate void RequestPermission(RequestPermissionArgs param);

        public event RequestPermission RequestItemPermission;

        public int? NewsID
        {
            get { return Utility.GetNullableInt(hidNewsID.Value); }
            set { hidNewsID.Value = Utility.GetString(value); }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //dpkHourIncurredDTG.SelectedTime = new TimeSpan(0, 0,0); // Đặt giá trị mặc định là 08:30:00 AM
                //ucNewsResearched.RequestSave_NegativeNews += RequestSave_NegativeNews;
                //ucListNagativeNews.RequestSave_NegativeNews += RequestSave_NegativeNews;
            }
            catch (SMXException ex)
            {
                LogManager.WebLogger.LogError("NegativeNews Page Error: ", ex);
                ucErr.ShowError(ex);
            }
        }
        private void PushNotiNegativeNews(SharedComponent.Entities.News itemGetNews)
        {
            ntf_Notification itemNoti = new ntf_Notification();
            NotificationParam pushNotiPram = new NotificationParam(FunctionType.Notification.PushNotification);
            itemNoti.NotificationID = itemGetNews?.NewsID ?? 0;
            itemNoti.Content = itemGetNews.Name;
            itemNoti.Type = SMX.CommentRefType.NegativeNews;
            pushNotiPram.Notification = itemNoti;
            pushNotiPram.TypeNoti = "TinTucSuVuMoi";
            MainController.Provider.Execute(pushNotiPram);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool checkUpdate = true;
                var functionType = FunctionType.NegativeNew.UpdateItem;
                var itemGetNews = GetData();
                if (NewsID == null)
                {
                    functionType = FunctionType.NegativeNew.AddNewItem;
                    checkUpdate = false;
                }
                NewsParam param = new NewsParam(functionType);
                param.News = itemGetNews;
                param.IsSaveComplete = true;
                MainController.Provider.Execute(param);
               // if(!checkUpdate)
                    PushNotiNegativeNews(param.News);
                //NewsID = param.News.NewsID;

                if (RequestExit != null)
                    RequestExit(null, null);
            }
            catch (SMXException ex)
            {
                LogManager.WebLogger.LogError("NegativeNews Save Error: ", ex);
                ucErr.ShowError(ex);
            }
        }

        protected void btnSaveContinues_Click(object sender, EventArgs e)
        {
            try
            {
                var functionType = FunctionType.NegativeNew.UpdateItem;
                if (NewsID == null)
                    functionType = FunctionType.NegativeNew.AddNewItem;

                NewsParam param = new NewsParam(functionType);
                param.News = GetData();
                param.IsSaveComplete = false;
                MainController.Provider.Execute(param);

                NewsID = param.News.NewsID;

                ShowMessage("Lưu thành công");

                if (RequestSaveContinue != null)
                    RequestSaveContinue(NewsID);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestExit != null)
                    RequestExit(null, null);
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
                if (NewsID != null)
                    popUpload.Show();
                else
                    RequestSave_NegativeNews(null, null);
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

        private void RequestSave_NegativeNews(object sender, EventArgs e)
        {
            try
            {
                throw new SMXException("Vui lòng bấm Lưu trước khi thực hiện thao tác này.");
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
            //ucNewsResearched.RequestSave_NegativeNews += RequestSave_NegativeNews;
            //ucListNagativeNews.RequestSave_NegativeNews += RequestSave_NegativeNews;

            UIUtility.BindDicToDropDownList(ddlClassification, SMX.News.Classification.dicClassification);
            UIUtility.BindDicToDropDownList(ddlNegativeType, SMX.News.NegativeNews.dicType);
            UIUtility.BindDicToDropDownList(ddlStatus, SMX.News.Status.dicDesc);
        }

        public void BindData(SharedComponent.Entities.News news)
        {
            NewsID = news.NewsID;

            BindObject2Form(news);

            ucComment.SetupForm();
            ucComment.LoadData(news.NewsID, SMX.CommentRefType.NegativeNews, true);
            ucComment.Visible = divComment.Visible = NewsID != null && NewsID != 0;

            RequestButtonPermisstion(news);
        }

        public SharedComponent.Entities.News GetData()
        {
            SharedComponent.Entities.News item = new SharedComponent.Entities.News();
            DateTime? incurredDTGTime;
            item.NewsID = NewsID;
            item.Name = txtName.Text;

            item.Classification = Utility.GetNullableInt(ddlClassification.SelectedValue);
            item.NegativeType = Utility.GetNullableInt(ddlNegativeType.SelectedValue);
            item.Status = Utility.GetNullableInt(ddlStatus.SelectedValue);
            //item.RatedLevel = txtRatedLevel.Text;
            item.PressAgency = txtPressAgency.Text;
            //item.Resolution = txtResolution.Text;
            //item.ResolutionContent = txtResolutionContent.Text;
            item.Concluded = txtConcluded.Text;
            if (dpkIncurredDTG.SelectedDate != null && dpkHourIncurredDTG.SelectedHour != null)
            {
                incurredDTGTime = dpkIncurredDTG.SelectedDate.Value.AddHours(dpkHourIncurredDTG.SelectedHour.Value);
                if (dpkIncurredDTG.SelectedDate != null && dpkHourIncurredDTG.SelectedHour != null && dpkHourIncurredDTG.SelectedMinute != null)
                    incurredDTGTime = dpkIncurredDTG.SelectedDate.Value.AddHours(dpkHourIncurredDTG.SelectedHour.Value).AddMinutes(dpkHourIncurredDTG.SelectedMinute.Value);
            }
            else
                incurredDTGTime = dpkIncurredDTG.SelectedDate;
            item.IncurredDTG = incurredDTGTime;
            //item.CreatedDTG = dpkIncurredDTG.SelectedDate + new TimeSpan(dpkHourIncurredDTG.SelectedHour ?? 0, dpkHourIncurredDTG.SelectedMinute ?? 0, 0);
            //item.CreatedDTG = dpkIncurredDTG.SelectedDate + new TimeSpan(dpkHourIncurredDTG.SelectedHour ?? 0, dpkHourIncurredDTG.SelectedMinute ?? 0, 0);


            return item;
        }

        #endregion

        #region Private Methods

        private void RequestButtonPermisstion(SharedComponent.Entities.News news)
        {
            if (RequestItemPermission != null)
            {
                var param = new RequestPermissionArgs();
                RequestItemPermission(param);

                if (news.NewsID != null)
                {
                    if (!(param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT) && (Profiles.MyProfile.UserName == news.CreatedBy || Profiles.MyProfile.ListFixedPermissionCode.Contains(SMX.FixedBusinessPermissionCode.CG))))
                        Response.Redirect(PageURL.ErrorPage);
                }
                else
                {
                    if (!(param.lstRight.Exists(x => x.FunctionCode == FunctionCode.ADD)))
                        Response.Redirect(PageURL.ErrorPage);
                }
            }
        }

        private void BindObject2Form(SharedComponent.Entities.News news)
        {
            //ucListNagativeNews.SetupForm();

            if (news != null)
            {
                txtName.Text = news.Name;
                dpkIncurredDTG.SelectedDate = news.IncurredDTG;
                if (news.IncurredDTG.HasValue)
                {
                    TimeSpan? selectedTime = new TimeSpan(news.IncurredDTG.Value.Hour, news.IncurredDTG.Value.Minute, 0);
                    dpkHourIncurredDTG.SelectedTime = selectedTime;
                }
                ddlClassification.SelectedValue = Utility.GetString(news.Classification);
                ddlStatus.SelectedValue = Utility.GetString(news.Status);
                ddlNegativeType.SelectedValue = Utility.GetString(news.NegativeType);

                //switch (news.NegativeType)
                //{
                //    case SMX.News.NegativeNews.ChuaPhatSinh:
                //        lblTitlePressAgency.Text = "Cơ quan báo chí liên hệ <span class='star'>*</span>";
                //        break;
                //    case SMX.News.NegativeNews.DaPhatSinh:
                //        lblTitlePressAgency.Text = "Các báo đăng tải <span class='star'>*</span>";
                //        break;
                //}

                //txtRatedLevel.Text = news.RatedLevel;
                txtPressAgency.Text = news.PressAgency;
                //txtResolution.Text = news.Resolution;
                //txtResolutionContent.Text = news.ResolutionContent;
                txtConcluded.Text = news.Concluded;

                if (news.ListAttachment != null && news.ListAttachment.Count > 0)
                {
                    divImage.Visible = true;

                    rptImage.DataSource = news.ListAttachment;
                    rptImage.DataBind();
                }
                else
                    divImage.Visible = false;

                //ucNewsResearched.BindData(news.NewsID, true);
                //ucListNagativeNews.BindData(true, news.NewsID);
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
            item.RefID = NewsID;
            item.RefType = SMX.AttachmentRefType.NegativeNews;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void LoadDataImages()
        {
            var param = new NewsParam(FunctionType.NegativeNew.LoadDataImages);
            param.NewsID = NewsID;
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

        #endregion
    }
}