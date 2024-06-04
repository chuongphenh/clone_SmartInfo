using System;
using System.Web.UI;
using SM.SmartInfo.Utils;
using SM.SmartInfo.BIZ;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Collections.Generic;
using System.Web;
using System.IO;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Drawing;
using System.Globalization;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class PostsUC : UserControl
    {
        public event EventHandler RequestSave_SingleNews;

        public event EventHandler Save_SingleNews;

        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);

                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidCampaignID.Value));
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
                var item = GetDataFromPopup();

                NewsParam param = new NewsParam(FunctionType.News.SaveSingleNews);

                param.SingleNews = item;

                param.ListAttachment = ViewState["ListImages"] as List<adm_Attachment>;

                MainController.Provider.Execute(param);

                popEdit.Hide();

                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidCampaignID.Value));

                if (Save_SingleNews != null)
                    Save_SingleNews(null, null);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestSave_SingleNews != null && string.IsNullOrWhiteSpace(hidNewsID.Value))
                {
                    RequestSave_SingleNews(null, null);
                    return;
                }

                if(string.IsNullOrEmpty(hidCampaignID.Value))
                {
                    ucErr.ShowError("Bấm lưu & tiếp tục trước khi thêm mới");
                }

                ClearPopup();
                popEdit.Show();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);

                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidCampaignID.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void popEdit_PopupClosed(object sender, EventArgs e)
        {
            popEdit.Hide();
        }

        protected void rptSingleNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptSingleNews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                var item = GetCurrentRowData(e.Item);

                switch (e.CommandName)
                {
                    case SMX.ActionEdit:
                        BindObjectToPopup(item);
                        popEdit.Show();
                        break;
                    case SMX.ActionDelete:
                        DeleteItem(item);
                        if (Save_SingleNews != null)
                            Save_SingleNews(null, null);
                        break;
                }

                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidCampaignID.Value));
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

        public void BindData(int? newsID, bool? isEdit, int? campaignID = null)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidNewsID.Value = Utility.GetString(newsID);
            hidCampaignID.Value = Utility.GetString(campaignID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = thAddNew.Visible = isEdit.GetValueOrDefault(false);
            tdPaging.ColSpan = isEdit.GetValueOrDefault(false) ? 6 : 1;

            NewsParam param = new NewsParam(FunctionType.News.GetListSingleNewsByNewsIDAndCampaignID);
            param.NewsID = newsID;
            param.CampaignNewsID = campaignID;

            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };

            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptSingleNews.DataSource = param.ListSingleNews;
            rptSingleNews.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidSingleNewsID.Value = txtTitle.Text = string.Empty;
            dpkPostedDate.SelectedDate = DateTime.Now;
            txtMediaChanel.Text = string.Empty;
            txtContent.Text = string.Empty;
            txtLink.Text = string.Empty;    
            uploadedImages = null;
            divImage.Visible = false;
            rptImage.DataSource = null;
            rptImage.DataBind();
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            SingleNews item = rptItem.DataItem as SingleNews;

            HiddenField hidCNewsID = rptItem.FindControl("hidSingleNewsID") as HiddenField;
            hidCNewsID.Value = Utility.GetString(item.Id);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrTitle", item.Title);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrPostedDate", item.PostedDate);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrChanel", item.Chanel);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrContent", item.Summary);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrLink", item.Link);

            NewsParam param = new NewsParam(FunctionType.CampaignNews.getCountUploadedDocument);
            param.attRefID = item.Id;
            param.attRefType = SMX.AttachmentRefType.SingleNews;
            MainController.Provider.Execute(param);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrAttachments", param.attCount);

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private SingleNews GetCurrentRowData(RepeaterItem rptItem)
        {
            SingleNews result = new SingleNews();

            HiddenField hidCNewsID = rptItem.FindControl("hidSingleNewsID") as HiddenField;
            Literal ltrTitle = rptItem.FindControl("ltrTitle") as Literal;
            Literal ltrPostedDate = rptItem.FindControl("ltrPostedDate") as Literal;
            Literal ltrChanel = rptItem.FindControl("ltrChanel") as Literal;
            Literal ltrContent = rptItem.FindControl("ltrContent") as Literal;
            Literal ltrLink = rptItem.FindControl("ltrLink") as Literal;

            result.Id = Utility.GetNullableInt(hidCNewsID.Value);

            result.Title = ltrTitle.Text;
            string format = "dd/MM/yyyy";
            result.PostedDate = DateTime.ParseExact(ltrPostedDate.Text, format, CultureInfo.InvariantCulture);
            result.Chanel = ltrChanel.Text;
            result.Summary = ltrContent.Text;
            result.Link = ltrLink.Text;
            
            return result;
        }

        private void BindObjectToPopup(SingleNews item)
        {
            hidSingleNewsID.Value = Utility.GetString(item.Id);
            txtTitle.Text = item.Title;
            dpkPostedDate.SelectedDate = (DateTime)item.PostedDate;
            txtContent.Text = item.Summary;
            txtLink.Text = item.Link;
            txtMediaChanel.Text = item.Chanel;
            LoadDataImages();
        }

        private SingleNews GetDataFromPopup()
        {
            SingleNews result = new SingleNews();

            result.Id = Utility.GetNullableInt(hidSingleNewsID.Value);
            result.NewsId = Utility.GetNullableInt(hidNewsID.Value);
            result.Title = txtTitle.Text;
            result.PostedDate = dpkPostedDate.SelectedDate;
            result.Summary = txtContent.Text;
            result.Link = txtLink.Text;
            result.Chanel = txtMediaChanel.Text;
            if (!string.IsNullOrEmpty(hidCampaignID.Value))
            {
                result.CampaignId = Utility.GetNullableInt(hidCampaignID.Value);
            }

            List<adm_Attachment> ListImages = new List<adm_Attachment>();

            foreach (HttpPostedFile postedFile in uploadedImages.PostedFiles)
            {
                if (postedFile.ContentLength > 0)
                {
                    FileInfo fi = new FileInfo(postedFile.FileName);
                    string ext = fi.Extension;
                    if (!SMX.AcceptFiles.lstCommonDocument.Exists(c => c.Equals(ext)))
                        throw new SMXException(Messages.AcceptFiles.CommonDocument);

                    adm_Attachment attachment = new adm_Attachment();
                    attachment.FileName = postedFile.FileName;
                    attachment.RefType = 14;
                    attachment.CreatedBy = Profiles.MyProfile.UserName;
                    attachment.CreatedDTG = DateTime.Now;
                    attachment.FileSize = postedFile.ContentLength;
                    attachment.DisplayName = postedFile.FileName;
                    attachment.ContentType = postedFile.ContentType;
                    attachment.RefID = Utility.GetNullableInt(hidSingleNewsID.Value);
                    byte[] fileContent = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(fileContent, 0, postedFile.ContentLength);
                    attachment.FileContent = fileContent;

                    ListImages.Add(attachment);
                }
            }

            ViewState["ListImages"] = ListImages;

            return result;
        }

        private void DeleteItem(SingleNews item)
        {
            NewsParam param = new NewsParam(FunctionType.News.DeleteSingleNews);
            param.SingleNewsId = item.Id;
            MainController.Provider.Execute(param);
        }

        #endregion

        private void LoadDataImages()
        {
            var param = new NewsParam(FunctionType.News.LoadDataImagesSingleNews);
            param.SingleNewsId = Utility.GetNullableInt(hidSingleNewsID.Value);
            MainController.Provider.Execute(param);

            divImage.Visible = param.ListAttachment != null && param.ListAttachment.Count > 0;
            rptImage.DataSource = param.ListAttachment;
            rptImage.DataBind();
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
                return;
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
                return;
            }
        }

        private void DeleteImage(int? attID)
        {
            var param = new AttachmentParam(FunctionType.CommonList.Attachment.DeleteDocument);
            param.adm_Attachment = new adm_Attachment() { AttachmentID = attID };
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

        /*protected void btnPreview_Click(object sender, EventArgs e)
        {
            List<adm_Attachment> ListImages = new List<adm_Attachment>();

            foreach (HttpPostedFile postedFile in uploadedImages.PostedFiles)
            {
                if (postedFile.ContentLength > 0)
                {
                    FileInfo fi = new FileInfo(postedFile.FileName);
                    string ext = fi.Extension;
                    if (!SMX.AcceptFiles.lstCommonDocument.Exists(c => c.Equals(ext)))
                        throw new SMXException(Messages.AcceptFiles.CommonDocument);

                    adm_Attachment attachment = new adm_Attachment();
                    attachment.FileName = postedFile.FileName;
                    attachment.RefType = 14;
                    attachment.CreatedBy = Profiles.MyProfile.UserName;
                    attachment.CreatedDTG = DateTime.Now;
                    attachment.FileSize = postedFile.ContentLength;
                    attachment.DisplayName = postedFile.FileName;
                    attachment.ContentType = postedFile.ContentType;
                    attachment.RefID = Utility.GetNullableInt(hidNewsID.Value);

                    ListImages.Add(attachment);
                }
            }

            ViewState["ListImagesPreview"] = ListImages;

            LoadDataImages();
        }*/

        private void UploadFiles(HttpPostedFile fileImage)
        {
            MemoryStream ms = new MemoryStream(fileImage.ContentLength);
            fileImage.InputStream.CopyTo(ms);

            adm_Attachment item = new adm_Attachment();
            item.FileName = fileImage.FileName;
            item.DisplayName = fileImage.FileName;
            item.FileSize = fileImage.ContentLength;
            item.ContentType = fileImage.ContentType;
            item.Description = "";
            item.RefID = Utility.GetNullableInt(hidNewsID.Value);
            item.RefType = 14;
            item.FileContent = ms.ToArray();

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private string GetDataUri(HttpPostedFile file)
        {
            Bitmap bitmap;
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] byteImage = ms.ToArray();

                using (MemoryStream ms2 = new MemoryStream(byteImage))
                {
                    bitmap = new Bitmap(ms2);
                }
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return "data:image/png;base64," + base64String;
            }
        }
    }
}