using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.PopupPages.NegativeNews
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

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                FinishItem();

                ShowMessage("Hoàn thành thành công");
                LoadData();
                ClickParentButton(GetParamCallbackButton());
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

        protected void btnExportWord_Click(object sender, EventArgs e)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "NegativeNewsID", hidId.Value },
                    { "EmployeeID", Profiles.MyProfile.EmployeeID },
                };

                UIUtilities.ExportWordHelper.ExportWord(SMX.TemplateWord.WordExport_PhieuTrinhChiTietSuVu, param);
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
                if (!string.IsNullOrWhiteSpace(hidId.Value) && Utility.GetNullableInt(hidId.Value) != 0)
                    popUpload.Show();
                else
                    throw new SMXException("Vui lòng bấm Lưu trước khi thực hiện thao tác này.");
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
                if (e.Item.ItemType == ListItemType.Item
                   || e.Item.ItemType == ListItemType.AlternatingItem)
                    BindItemToRepeater(e.Item);
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

            hidNewsId.Value = Utility.GetString(arrRefParam[0]);
            hidId.Value = Utility.GetString(arrRefParam[1]);

            lnkEdit.NavigateUrl = string.Format("Edit.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidNewsId.Value).GetValueOrDefault(0), Utility.GetNullableInt(hidId.Value).GetValueOrDefault(0) }), GetParamCallbackButton());
        }

        private void LoadData()
        {
            var param = new NewsParam(FunctionType.NegativeNew.LoadDataNegativeNews);
            param.NegativeNewsID = Utility.GetNullableInt(hidId.Value);
            MainController.Provider.Execute(param);

            BindObjectToForm(param.NegativeNews);
            ucNegativeNewsResearched.BindData(param.NegativeNewsID, true);

            rptImage.DataSource = param.ListAttachment;
            rptImage.DataBind();

            rptImg.DataSource = param.ListAttachment;
            rptImg.DataBind();
        }

        private void FinishItem()
        {
            //Add
            NewsParam param = new NewsParam(FunctionType.NegativeNew.FinishNegativeNews);
            param.NegativeNewsID = Utility.GetNullableInt(hidId.Value);
            MainController.Provider.Execute(param);
        }

        private void BindItemToRepeater(RepeaterItem rptItem)
        {
            adm_Attachment item = (adm_Attachment)rptItem.DataItem;

            LinkButton btnDeleteImage = rptItem.FindControl("btnDeleteImage") as LinkButton;
            btnDeleteImage.CommandArgument = Utility.GetString(item.AttachmentID);
            btnDeleteImage.CommandName = SMX.ActionDelete;

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { item?.AttachmentID ?? 0 })), 1000, 600);
            HtmlGenericControl divViewDetailImage = rptItem.FindControl("divViewDetailImage") as HtmlGenericControl;
            divViewDetailImage.Attributes.Add("onclick", url);

            HtmlImage img = (HtmlImage)rptItem.FindControl("img");

            if (item != null)
            {
                img.Alt = item.Description;
                img.Src = GetImageURL(item);
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

        private void BindObjectToForm(SharedComponent.Entities.NegativeNews item)
        {
            if (item != null && item.NegativeNewsID != null && item.NegativeNewsID != 0)
            {
                ltrType.Text = ltrType1.Text = Utility.GetDictionaryValue(SMX.News.NegativeNews.dicType, item.Type);
                ltrIncurredDTG.Text = ltrIncurredDTG1.Text = Utility.GetDateTimeString(item.IncurredDTG, "HH:mm - dd/MM/yyyy");
                ltrName.Text = ltrName1.Text = item.Name;

                if (item.Type == SMX.News.NegativeNews.DaPhatSinh)
                {
                    ltrPressAgencyID.Text = item.PressAgencyName;
                    ltrURL.Text = UIUtility.ConvertBreakLine2Html(item.Url);
                    ltrTitle.Text = item.Title;
                    ltrJudged.Text = UIUtility.ConvertBreakLine2Html(item.Judged);
                    ltrMethodHandle.Text = UIUtility.ConvertBreakLine2Html(item.MethodHandle);
                    ltrResult.Text = UIUtility.ConvertBreakLine2Html(item.Result);
                    ltrNote.Text = UIUtility.ConvertBreakLine2Html(item.Note);
                    ChuaPhatSinh.Visible = false;
                    DaPhatSinh.Visible = true;
                }
                else
                {
                    ltrPlace.Text = item.Place;
                    ltrPressAgencyIDNoIncurred.Text = item.PressAgencyName;
                    ltrResultNoIncurred.Text = UIUtility.ConvertBreakLine2Html(item.Result);
                    ltrReporterInformation.Text = UIUtility.ConvertBreakLine2Html(item.ReporterInformation);
                    ltrQuestion.Text = UIUtility.ConvertBreakLine2Html(item.Question);
                    ltrQuestionDetail.Text = UIUtility.ConvertBreakLine2Html(item.QuestionDetail);
                    ltrResolution.Text = UIUtility.ConvertBreakLine2Html(item.Resolution);
                    ltrResolutionContent.Text = UIUtility.ConvertBreakLine2Html(item.ResolutionContent);
                    ChuaPhatSinh.Visible = true;
                    DaPhatSinh.Visible = false;
                }

                btnFinish.Visible = item.Status != SMX.News.Status.HoanThanh;
            }
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
            item.RefType = SMX.AttachmentRefType.NegativeNewsDetail;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void LoadDataImages()
        {
            var param = new NewsParam(FunctionType.NegativeNew.LoadDataImagesDetail);
            param.NegativeNewsID = Utility.GetNullableInt(hidId.Value);
            MainController.Provider.Execute(param);

            if (DaPhatSinh.Visible)
            {
                rptImage.DataSource = param.ListAttachment;
                rptImage.DataBind();
            }

            if (ChuaPhatSinh.Visible)
            {
                rptImg.DataSource = param.ListAttachment;
                rptImg.DataBind();
            }
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