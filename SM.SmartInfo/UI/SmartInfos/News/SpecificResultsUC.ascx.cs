using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Net;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class SpecificResultsUC : UserControl
    {
        public event EventHandler RequestSave_News;
        public event EventHandler Complete;
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
                var item = GetDataFromPopup();

                NewsParam param = new NewsParam(FunctionType.PositiveNews.SavePositiveNews);
                param.PositiveNews = item;
                MainController.Provider.Execute(param);

                popEdit.Hide();
                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value));
                Complete?.Invoke(null, null);
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
                trImage.Visible = false;
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
                ucErr.ShowError(ex);
            }
        }

        protected void rptPositiveNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptPositiveNews_ItemCommand(object source, RepeaterCommandEventArgs e)
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
                        break;
                    case "ShowLink":
                        if (string.IsNullOrWhiteSpace(item.Url))
                            throw new SMXException("Tin tức này chưa có link");
                        //if (!RemoteFileExists(item.Url))
                        //    throw new SMXException("Link không tồn tại, Vui lòng kiểm tra lại link thực tế.");
                        UIUtility.OpenPopupWindow(this.Page, item.Url, 1000, 700);
                        break;
                }

                BindData(Utility.GetNullableInt(hidNewsID.Value), Utility.GetNullableBool(hidIsEdit.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }


        #endregion

        #region Public Methods

        public void SetupForm(int? newsID)
        {
            hidPage.Value = "1";

            UIUtility.BindDicToDropDownList(ddlType, SMX.News.PositiveNews.dicType);

            NewsParam param = new NewsParam(FunctionType.PositiveNews.PrepareDataCampaign);
            param.NewsID = newsID;
            MainController.Provider.Execute(param);

            UIUtility.BindListToDropDownList(ddlCampaignID, param.ListCampaignNews, CampaignNews.C_CampaignNewsID, CampaignNews.C_Campaign);
        }

        public void BindData(int? newsID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidNewsID.Value = Utility.GetString(newsID);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thEdit.Visible = thAddNew.Visible = footerEdit.Visible = isEdit.GetValueOrDefault(false);

            NewsParam param = new NewsParam(FunctionType.PositiveNews.GetListPositiveNewsByNewsID);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };
            param.NewsID = newsID;
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptPositiveNews.DataSource = param.ListPositiveNews;
            rptPositiveNews.DataBind();
        }

        #endregion

        #region Private Methods

        private void ClearPopup()
        {
            hidPositiveNewsID.Value = ddlCampaignID.SelectedValue = txtBrief.Text = txtUrl.Text = txtTitle.Text = ddlType.SelectedValue = string.Empty;
            ucPressAgencySelector.SetSelectedItem(null, null);
            dpkPublishDTG.SelectedDate = null;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            PositiveNews item = rptItem.DataItem as PositiveNews;

            HiddenField hidPositiveNewsID = rptItem.FindControl("hidPositiveNewsID") as HiddenField;
            hidPositiveNewsID.Value = Utility.GetString(item.PositiveNewsID);

            HiddenField hidPressAgencryID = rptItem.FindControl("hidPressAgencryID") as HiddenField;
            hidPressAgencryID.Value = Utility.GetString(item.PressAgencryID);

            HiddenField hidCampaignID = rptItem.FindControl("hidCampaignID") as HiddenField;
            hidCampaignID.Value = Utility.GetString(item.CampaignID);

            HiddenField hidBrief = rptItem.FindControl("hidBrief") as HiddenField;
            hidBrief.Value = item.Brief;

            HiddenField hidUrl = rptItem.FindControl("hidUrl") as HiddenField;
            hidUrl.Value = item.Url;

            HiddenField hidType = rptItem.FindControl("hidType") as HiddenField;
            hidType.Value = Utility.GetString(item.Type);

            HiddenField hidTitle = rptItem.FindControl("hidTitle") as HiddenField;
            hidTitle.Value = item.Title;

            DropDownList ddlType = rptItem.FindControl("ddlType") as DropDownList;
            ddlType.SelectedValue = Utility.GetString(item.Type);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrType", Utility.GetDictionaryValue(SMX.News.PositiveNews.dicType, item.Type));

            UIUtility.SetRepeaterItemIText(rptItem, "ltrCampaign", item.CampaignName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrPressAgencryName", item.PressAgencyName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrPublishDTG", Utility.GetDateString(item.PublishDTG));

            LinkButton btnTitle = rptItem.FindControl("btnTitle") as LinkButton;
            btnTitle.CommandName = "ShowLink";
            btnTitle.Text = item.Title;

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

            DatePicker dpkPublishDTG = rptItem.FindControl("dpkPublishDTG") as DatePicker;
            dpkPublishDTG.SelectedDate = item.PublishDTG;

            LinkButton btnEdit = rptItem.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            btnEdit.CommandName = SMX.ActionEdit;
            btnDelete.CommandName = SMX.ActionDelete;
        }

        private PositiveNews GetCurrentRowData(RepeaterItem rptItem)
        {
            PositiveNews result = new PositiveNews();

            HiddenField hidPositiveNewsID = rptItem.FindControl("hidPositiveNewsID") as HiddenField;
            HiddenField hidCampaignID = rptItem.FindControl("hidCampaignID") as HiddenField;
            HiddenField hidBrief = rptItem.FindControl("hidBrief") as HiddenField;
            HiddenField hidUrl = rptItem.FindControl("hidUrl") as HiddenField;
            HiddenField hidType = rptItem.FindControl("hidType") as HiddenField;
            HiddenField hidPressAgencryID = rptItem.FindControl("hidPressAgencryID") as HiddenField;
            HiddenField hidTitle = rptItem.FindControl("hidTitle") as HiddenField;
            Literal ltrPressAgencryName = rptItem.FindControl("ltrPressAgencryName") as Literal;
            DatePicker dpkPublishDTG = rptItem.FindControl("dpkPublishDTG") as DatePicker;

            result.PositiveNewsID = Utility.GetNullableInt(hidPositiveNewsID.Value);
            result.Type = Utility.GetNullableInt(hidType.Value);
            result.CampaignID = Utility.GetNullableInt(hidCampaignID.Value);
            result.PressAgencryID = Utility.GetNullableInt(hidPressAgencryID.Value);
            result.PressAgencyName = ltrPressAgencryName.Text;
            result.Title = hidTitle.Value;
            result.Brief = hidBrief.Value;
            result.Url = hidUrl.Value;
            result.PublishDTG = dpkPublishDTG.SelectedDate;

            return result;
        }

        private void BindObjectToPopup(PositiveNews item)
        {
            hidPositiveNewsID.Value = Utility.GetString(item.PositiveNewsID);
            ddlCampaignID.SelectedValue = Utility.GetString(item.CampaignID);
            ddlType.SelectedValue = Utility.GetString(item.Type);
            ucPressAgencySelector.SetSelectedItem(item.PressAgencryID, item.PressAgencyName);
            txtTitle.Text = item.Title;
            txtBrief.Text = item.Brief;
            txtUrl.Text = item.Url;
            dpkPublishDTG.SelectedDate = item.PublishDTG;

            if (item.PositiveNewsID == null)
                trImage.Visible = false;
            else
            {
                trImage.Visible = true;
                LoadDataImages();
            }
        }

        private PositiveNews GetDataFromPopup()
        {
            PositiveNews result = new PositiveNews();

            result.PositiveNewsID = Utility.GetNullableInt(hidPositiveNewsID.Value);
            result.NewsID = Utility.GetNullableInt(hidNewsID.Value);
            result.CampaignID = Utility.GetNullableInt(ddlCampaignID.SelectedValue);
            result.Type = Utility.GetNullableInt(ddlType.SelectedValue);
            result.PressAgencryID = Utility.GetNullableInt(ucPressAgencySelector.SelectedValue);
            result.Title = txtTitle.Text;
            result.Brief = txtBrief.Text;
            result.Url = txtUrl.Text;
            result.PublishDTG = dpkPublishDTG.SelectedDate;

            return result;
        }

        private void DeleteItem(PositiveNews item)
        {
            NewsParam param = new NewsParam(FunctionType.PositiveNews.DeletePositiveNews);
            param.PositiveNewsID = item.PositiveNewsID;
            param.PositiveNews = item;
            MainController.Provider.Execute(param);
            Complete?.Invoke(null, null);
        }

        private void UploadFiles()
        {
            adm_Attachment item = new adm_Attachment();
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.Description = txtDescription.Text;
            item.RefID = Utility.GetNullableInt(hidPositiveNewsID.Value);
            item.RefType = SMX.AttachmentRefType.PositiveNews;
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
            var param = new NewsParam(FunctionType.News.LoadDataImagesPositiveNews);
            param.PositiveNewsID = Utility.GetNullableInt(hidPositiveNewsID.Value);
            MainController.Provider.Execute(param);

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