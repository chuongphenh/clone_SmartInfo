using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.PopupPages.NegativeNews
{
    public partial class Edit : BasePagePopup
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ucNegativeNewsResearched.RequestSave_NegativeNews += ucNegativeNewsResearched_RequestSave_NegativeNews;

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int? id = UpdateItem();
                ClickParentButton(GetParamCallbackButton(), string.Format("Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidNewsId.Value).GetValueOrDefault(0), id.GetValueOrDefault(0) }), GetParamCallbackButton()));
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

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlCurrentType = sender as DropDownList;

            if (Utility.GetNullableInt(ddlCurrentType.SelectedValue) == SMX.News.NegativeNews.DaPhatSinh)
            {
                DaPhatSinh.Visible = true;
                ChuaPhatSinh.Visible = false;

                ddlType.SelectedValue = ddlType1.SelectedValue = Utility.GetString(SMX.News.NegativeNews.DaPhatSinh);
            }
            else
            {
                DaPhatSinh.Visible = false;
                ChuaPhatSinh.Visible = true;

                ddlType.SelectedValue = ddlType1.SelectedValue = Utility.GetString(SMX.News.NegativeNews.ChuaPhatSinh);
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

        #region Biz
        private void SetupForm()
        {
            ucNegativeNewsResearched.RequestSave_NegativeNews += ucNegativeNewsResearched_RequestSave_NegativeNews;

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

            UIUtility.BindDicToDropDownList(ddlType, SMX.News.NegativeNews.dicType);
            UIUtility.BindDicToDropDownList(ddlType1, SMX.News.NegativeNews.dicType);

            hidNewsId.Value = Utility.GetString(arrRefParam[0]);
            hidId.Value = Utility.GetString(arrRefParam[1]);
            lnkExit.NavigateUrl = string.Format("Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidNewsId.Value).GetValueOrDefault(0), Utility.GetNullableInt(hidId.Value).GetValueOrDefault(0) }), GetParamCallbackButton());
        }

        private void ucNegativeNewsResearched_RequestSave_NegativeNews(object sender, EventArgs e)
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

        private void LoadData()
        {
            var param = new NewsParam(FunctionType.NegativeNew.LoadDataNegativeNews);
            param.NegativeNewsID = Utility.GetNullableInt(hidId.Value);
            MainController.Provider.Execute(param);

            BindObjectToForm(param.NegativeNews);
            ucNegativeNewsResearched.BindData(param.NegativeNewsID, true);

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

        private int? UpdateItem()
        {
            //Binding object
            SharedComponent.Entities.NegativeNews item = GetNegativeNews();

            //Add
            string functionType = FunctionType.NegativeNew.UpdateNegativeNews;
            if (string.IsNullOrWhiteSpace(hidId.Value) || Utility.GetNullableInt(hidId.Value) == 0)
                functionType = FunctionType.NegativeNew.AddNewNegativeNews;

            NewsParam param = new NewsParam(functionType);
            param.NegativeNews = item;
            MainController.Provider.Execute(param);

            return param.NegativeNews?.NegativeNewsID;
        }

        private SharedComponent.Entities.NegativeNews GetNegativeNews()
        {
            //DateTime? incurredDTGTime;
            SharedComponent.Entities.NegativeNews item = new SharedComponent.Entities.NegativeNews();
            item.NewsID = Utility.GetNullableInt(hidNewsId.Value);
            item.NegativeNewsID = Utility.GetNullableInt(hidId.Value);

            if (Utility.GetNullableInt(ddlType.SelectedValue) == SMX.News.NegativeNews.DaPhatSinh)
            {
                item.PressAgencyID = Utility.GetNullableInt(ucPressAgencySelector.SelectedValue);
                item.Url = txtURL.Text;
                item.Title = txtTitle.Text;
                item.Judged = txtJudged.Text;
                item.MethodHandle = txtMethodHandle.Text;
                item.Result = txtResult.Text;
                item.Note = txtNote.Text;

                item.Name = txtName.Text;
                item.Type = Utility.GetNullableInt(ddlType.SelectedValue);
                //if (dpkIncurredDTG.SelectedDate != null && dpkHourIncurredDTG.SelectedHour != null)
                //{
                //    incurredDTGTime = dpkIncurredDTG.SelectedDate.Value.AddHours(dpkHourIncurredDTG.SelectedHour.Value);
                //    if (dpkIncurredDTG.SelectedDate != null && dpkHourIncurredDTG.SelectedHour != null && dpkHourIncurredDTG.SelectedMinute != null)
                //        incurredDTGTime = dpkIncurredDTG.SelectedDate.Value.AddHours(dpkHourIncurredDTG.SelectedHour.Value).AddMinutes(dpkHourIncurredDTG.SelectedMinute.Value);
                //}
                //else
                //    incurredDTGTime = dpkIncurredDTG.SelectedDate;
                //item.IncurredDTG = incurredDTGTime;
                item.IncurredDTG = dpkIncurredDTG.SelectedDate + new TimeSpan(dpkHourIncurredDTG.SelectedHour ?? 0, dpkHourIncurredDTG.SelectedMinute ?? 0, 0);

            }
            else
            {
                item.Place = txtPlace.Text;
                item.PressAgencyID = Utility.GetNullableInt(ucPressAgencySelectorChuaXayRa.SelectedValue);
                item.Result = txtResultNoIncurred.Text;
                item.ReporterInformation = txtReporterInformation.Text;
                item.Question = txtQuestion.Text;
                item.QuestionDetail = txtQuestionDetail.Text;
                item.Resolution = txtResolution.Text;
                item.ResolutionContent = txtResolutionContent.Text;

                item.Name = txtName1.Text;
                item.Type = Utility.GetNullableInt(ddlType1.SelectedValue);
                //if (dpkIncurredDTG1.SelectedDate != null && dpkHourIncurredDTG1.SelectedHour != null)
                //{
                //    incurredDTGTime = dpkIncurredDTG1.SelectedDate.Value.AddHours(dpkHourIncurredDTG1.SelectedHour.Value);
                //    if (dpkIncurredDTG1.SelectedDate != null && dpkHourIncurredDTG1.SelectedHour != null && dpkHourIncurredDTG1.SelectedMinute != null)
                //        incurredDTGTime = dpkIncurredDTG1.SelectedDate.Value.AddHours(dpkHourIncurredDTG1.SelectedHour.Value).AddMinutes(dpkHourIncurredDTG1.SelectedMinute.Value);
                //}
                //else
                //    incurredDTGTime = dpkIncurredDTG.SelectedDate;

                //item.IncurredDTG = incurredDTGTime;
                item.IncurredDTG = dpkIncurredDTG1.SelectedDate + new TimeSpan(dpkHourIncurredDTG1.SelectedHour ?? 0, dpkHourIncurredDTG1.SelectedMinute ?? 0, 0);

            }
            return item;
        }

        private void BindObjectToForm(SharedComponent.Entities.NegativeNews item)
        {
            if (item != null && item.NegativeNewsID != null)
            {
                txtName.Text = txtName1.Text = item.Name;
                ddlType.SelectedValue = ddlType1.SelectedValue = Utility.GetString(item.Type);
                dpkIncurredDTG.SelectedDate = dpkIncurredDTG1.SelectedDate = item.IncurredDTG;

                if (item.Type == SMX.News.NegativeNews.DaPhatSinh)
                {
                    ucPressAgencySelector.SetSelectedItem(item.PressAgencyID, item.PressAgencyName);
                    txtURL.Text = item.Url;
                    txtTitle.Text = item.Title;
                    txtJudged.Text = item.Judged;
                    txtMethodHandle.Text = item.MethodHandle;
                    txtResult.Text = item.Result;
                    txtNote.Text = item.Note;
                    ChuaPhatSinh.Visible = false;
                    DaPhatSinh.Visible = true;
                    if (item.IncurredDTG.HasValue)
                    {
                        TimeSpan? selectedTime = new TimeSpan(item.IncurredDTG.Value.Hour, item.IncurredDTG.Value.Minute, 0);
                        dpkHourIncurredDTG.SelectedTime = selectedTime;
                    }
                }
                else
                {
                    txtPlace.Text = item.Place;
                    ucPressAgencySelectorChuaXayRa.SetSelectedItem(item.PressAgencyID, item.PressAgencyName);
                    txtResultNoIncurred.Text = item.Result;
                    txtReporterInformation.Text = item.ReporterInformation;
                    txtQuestion.Text = item.Question;
                    txtQuestionDetail.Text = item.QuestionDetail;
                    txtResolution.Text = item.Resolution;
                    txtResolutionContent.Text = item.ResolutionContent;
                    ChuaPhatSinh.Visible = true;
                    DaPhatSinh.Visible = false;
                    if (item.IncurredDTG.HasValue)
                    {
                        TimeSpan? selectedTime = new TimeSpan(item.IncurredDTG.Value.Hour, item.IncurredDTG.Value.Minute, 0);
                        dpkHourIncurredDTG1.SelectedTime = selectedTime;
                    }
                }
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

        #endregion
    }
}