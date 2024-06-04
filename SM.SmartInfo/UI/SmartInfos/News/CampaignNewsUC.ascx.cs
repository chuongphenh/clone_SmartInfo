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
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Documents;
using System.Collections.Generic;
using System.IO;
using System.Web;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Linq;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class CampaignNewsUC : System.Web.UI.UserControl
    {
        public event EventHandler RequestSave_News;

        public event EventHandler Save_CampaignNews;

        public List<adm_Attachment> listNewUploadedAtt = new List<adm_Attachment>();

        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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
                if (!string.IsNullOrEmpty(hidCampaignNewsID.Value))
                {
                    NewsParam param = new NewsParam(FunctionType.CampaignNews.SaveCampaignNews);
                    param.CampaignNews = GetDataFromPopup();
                    param.ListAttachment = listNewUploadedAtt;
                    if (!string.IsNullOrEmpty(hidCampaignNewsID.Value))
                    {
                        param.CampaignNews.CampaignNewsID = Utility.GetNullableInt(hidCampaignNewsID.Value);
                    }

                    MainController.Provider.Execute(param);

                    NewsParam param1 = new NewsParam(FunctionType.News.SetIsSingleCamp);
                    param1.NewsID = Utility.GetNullableInt(hidNewsID.Value);
                    param1.IsSingleCamp = false;
                    MainController.Provider.Execute(param1);
                }
                else
                {
                    var campaignID = UpdateItem(false);

                    hidCampaignNewsID.Value = campaignID.ToString();

                    NewsParam param = new NewsParam(FunctionType.News.AddDocumentCampaignNews);
                    param.ListAttachment = listNewUploadedAtt;
                    param.CampaignNewsID = campaignID;
                    MainController.Provider.Execute(param);

                    NewsParam param1 = new NewsParam(FunctionType.News.SetIsSingleCamp);
                    param1.NewsID = Utility.GetNullableInt(hidNewsID.Value);
                    param1.IsSingleCamp = false;
                    MainController.Provider.Execute(param1);
                }

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value));

                if (Save_CampaignNews != null)
                    Save_CampaignNews(null, null);
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
                if (RequestSave_News != null && string.IsNullOrWhiteSpace(hidNewsID.Value))
                {
                    RequestSave_News(null, null);
                    return;
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
                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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

        protected void rptCampaignNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptCampaignNews_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                        if (Save_CampaignNews != null)
                            Save_CampaignNews(null, null);
                        break;
                }

                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value));
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

        public void BindData(int? newsID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidNewsID.Value = Utility.GetString(newsID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = thAddNew.Visible = isEdit.GetValueOrDefault(false);
            tdPaging.ColSpan = isEdit.GetValueOrDefault(false) ? 4 : 1;

            PostsUC.SetupForm();
            PostsUC.BindData(Utility.GetNullableInt(hidNewsID.Value), true, Utility.GetNullableInt(hidCampaignNewsID.Value));

            NewsParam param = new NewsParam(FunctionType.CampaignNews.GetListCampaignNewsByNewsID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };
            param.NewsID = newsID;
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptCampaignNews.DataSource = param.ListCampaignNews;
            rptCampaignNews.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidCampaignNewsID.Value = txtCampaign.Text = string.Empty;
            txtNumberOfNews.Value = null;
            dpkPostedDate.SelectedDate = DateTime.Now;
            rptDoc.DataSource = null;
            rptDoc.DataBind();
            divDoc.Visible = false;
            ViewState["ListExistingAtt"] = null;
            PostsUC.SetupForm();
            PostsUC.BindData(null, null);
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            CampaignNews item = rptItem.DataItem as CampaignNews;

            HiddenField hidCNewsID = rptItem.FindControl("hidCampaignNewsID") as HiddenField;
            hidCNewsID.Value = Utility.GetString(item.CampaignNewsID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrCampaign", item.Campaign);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrNumberOfNews", Utility.GetString(item.NumberOfNews));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrPostedDate", item.PostedDTG);

            NewsParam param = new NewsParam(FunctionType.CampaignNews.getCountUploadedDocument);
            param.attRefID = item.CampaignNewsID;
            param.attRefType = SMX.AttachmentRefType.CampaignNews;
            MainController.Provider.Execute(param);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrNumOfFilesUploaded", param.attCount);

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private CampaignNews GetCurrentRowData(RepeaterItem rptItem)
        {
            CampaignNews result = new CampaignNews();

            HiddenField hidCNewsID = rptItem.FindControl("hidCampaignNewsID") as HiddenField;
            Literal ltrCampaign = rptItem.FindControl("ltrCampaign") as Literal;
            Literal ltrNumberOfNews = rptItem.FindControl("ltrNumberOfNews") as Literal;
            Literal ltrPostedDate = rptItem.FindControl("ltrPostedDate") as Literal;

            result.CampaignNewsID = Utility.GetNullableInt(hidCNewsID.Value);

            result.Campaign = ltrCampaign.Text;
            result.NumberOfNews = Utility.GetNullableInt(ltrNumberOfNews.Text);

            if (!string.IsNullOrEmpty(ltrPostedDate.Text))
            {
                string format = "dd/MM/yyyy";

                result.PostedDTG = DateTime.ParseExact(ltrPostedDate.Text, format, CultureInfo.InvariantCulture);
            }
            else
            {
                result.PostedDTG = null;
            }
            

            return result;
        }

        private void BindObjectToPopup(CampaignNews item)
        {
            hidCampaignNewsID.Value = Utility.GetString(item.CampaignNewsID);
            txtCampaign.Text = item.Campaign;
            txtNumberOfNews.Value = (double?)item.NumberOfNews;
            dpkPostedDate.SelectedDate = item.PostedDTG;
            txtNumberOfNews.Text = item.NumberOfNews.ToString();
            LoadDataDoc();

            PostsUC.SetupForm();
            PostsUC.BindData(Utility.GetNullableInt(hidNewsID.Value), true, Utility.GetNullableInt(hidCampaignNewsID.Value));
        }

        private CampaignNews GetDataFromPopup()
        {
            CampaignNews result = new CampaignNews();

            result.CampaignNewsID = Utility.GetNullableInt(hidCampaignNewsID.Value);
            result.NewsID = Utility.GetNullableInt(hidNewsID.Value);
            result.Campaign = txtCampaign.Text;
            result.NumberOfNews = (int?)txtNumberOfNews.Value;
            result.PostedDTG = dpkPostedDate.SelectedDate;

            foreach (HttpPostedFile postedFile in fupDocument.PostedFiles)
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
                    byte[] fileContent = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(fileContent, 0, postedFile.ContentLength);
                    attachment.FileContent = fileContent;

                    listNewUploadedAtt.Add(attachment);
                }
            }

            return result;
        }

        private void DeleteItem(CampaignNews item)
        {
            NewsParam param = new NewsParam(FunctionType.CampaignNews.DeleteCampaignNews);
            param.CampaignNewsID = item.CampaignNewsID;
            param.NewsID = Utility.GetNullableInt(hidNewsID.Value);
            MainController.Provider.Execute(param);
        }

        #endregion

        protected void btnSaveAndContinue_Click(object sender, EventArgs e)
        {
            try
            {

                var campaignID = UpdateItem(false);

                hidCampaignNewsID.Value = campaignID.ToString();


                if (ViewState["ListExistingAtt"] as List<adm_Attachment> == null)
                {
                    ViewState["ListExistingAtt"] = new List<adm_Attachment>();
                }

                NewsParam param = new NewsParam(FunctionType.News.AddDocumentCampaignNews);
                param.ListAttachment = listNewUploadedAtt.Concat((ViewState["ListExistingAtt"] as List<adm_Attachment>)).ToList();
                param.CampaignNewsID = Utility.GetNullableInt(hidCampaignNewsID.Value);
                MainController.Provider.Execute(param);

                LoadDataDoc();

                PostsUC.SetupForm();

                PostsUC.BindData(Utility.GetNullableInt(hidNewsID.Value), true, Utility.GetNullableInt(hidCampaignNewsID.Value));
            }
            catch (SMXException ex)
            {

            }
        }

        private int? UpdateItem(bool isSaveComplete)
        {
            LogManager.Pentest.LogDebug("bat dau UpdateItem");
            //Binding object
            CampaignNews item = GetDataFromPopup();
            LogManager.Pentest.LogDebug("get date  News item");
            //Add

            NewsParam param = new NewsParam(FunctionType.News.AddNewCampaignNews);
            param.CampaignNews = item;
            param.IsSaveComplete = isSaveComplete;
            MainController.Provider.Execute(param);

            return param.CampaignNews?.CampaignNewsID;
        }

        protected void rptDoc_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindDocToRepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                return;
            }
        }

        protected void rptDoc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDelete:
                        var attID = Utility.GetNullableInt(e.CommandArgument.ToString());
                        DeleteImage(attID);
                        LoadDataDoc();
                        break;
                }
            }
            catch (SMXException ex)
            {
                return;
            }
        }

        private void LoadDataDoc()
        {
            var param = new NewsParam(FunctionType.News.LoadDataDocumentCampaignNews);
            param.CampaignNewsID = Utility.GetNullableInt(hidCampaignNewsID.Value);
            MainController.Provider.Execute(param);

            ViewState["ListExistingAtt"] = param.ListAttachment;

            divDoc.Visible = param.ListAttachment != null && param.ListAttachment.Count > 0;
            rptDoc.DataSource = param.ListAttachment;
            rptDoc.DataBind();
        }

        private void DeleteImage(int? attID)
        {
            var param = new AttachmentParam(FunctionType.CommonList.Attachment.DeleteDocument);
            param.adm_Attachment = new adm_Attachment() { AttachmentID = attID };
            MainController.Provider.Execute(param);
        }

        private void BindDocToRepeaterItem(RepeaterItem rptItem)
        {
            adm_Attachment att = rptItem.DataItem as adm_Attachment;

            LinkButton btnDeleteDoc = rptItem.FindControl("btnDeleteDoc") as LinkButton;
            btnDeleteDoc.CommandArgument = Utility.GetString(att.AttachmentID);
            btnDeleteDoc.CommandName = SMX.ActionDelete;

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
    }
}