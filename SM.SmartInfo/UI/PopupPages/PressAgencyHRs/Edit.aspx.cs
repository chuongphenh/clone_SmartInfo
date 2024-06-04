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
using SM.SmartInfo.SharedComponent.Params.Administration;
using System.Linq;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class Edit : BasePagePopup
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ucPressAgencyHRAlert.RequestSave_PressAgencyHR += RequestSave_PressAgencyHR;
                ucPressAgencyHRHistory.RequestSave_PressAgencyHR += RequestSave_PressAgencyHR;
                ucPressAgencyHRRelatives.RequestSave_PressAgencyHR += RequestSave_PressAgencyHR;

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
        private void SelectDefaultGroups()
        {
            try
            {
                int? pressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
                if (pressAgencyHRID.HasValue)
                {
                    RoleParam param = new RoleParam(FunctionType.Administration.Role.GetListRoleIDByPressAgencyHRID);
                    param.PressAgencyHRID = pressAgencyHRID;
                    MainController.Provider.Execute(param);
                    var roleIDs = param.RoleIDs;
                    if (cmbGroups != null && roleIDs.Any())
                    {
                        foreach (ListItem item in cmbGroups.Items)
                        {
                            if (roleIDs.Contains(item.Value))
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        private void BindPermissionGroups()
        {
            RoleParam param = new RoleParam(FunctionType.Administration.Role.GetAllActiveRoleExceptQTHT);
            MainController.Provider.Execute(param);
            cmbGroups.DataSource = param.Roles;
            cmbGroups.DataValueField = "RoleID";
            cmbGroups.DataTextField = "Name";
            cmbGroups.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetData();

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyHR);
                param.PressAgencyHR = item;
                param.IsSaveComplete = true;
                MainController.Provider.Execute(param);

                if (fileUpload.HasFile)
                    UploadImage(param.PressAgencyHR?.PressAgencyHRID);

                ClickParentButton(GetParamCallbackButton(), string.Format("Display.aspx?ID={0}&callback={1}",
                    Utility.Encrypt(Profiles.MyProfile.EmployeeID,
                    new int[] { Utility.GetNullableInt(hidPressAgencyID.Value).GetValueOrDefault(0),
                        (param.PressAgencyHR?.PressAgencyHRID).GetValueOrDefault(0) }), GetParamCallbackButton()));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSaveContinues_Click(object sender, EventArgs e)
        {
            try
            {
                var item = GetData();

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgencyHR);
                param.PressAgencyHR = item;
                param.IsSaveComplete = false;
                MainController.Provider.Execute(param);

                if (fileUpload.HasFile)
                    UploadImage(param.PressAgencyHR?.PressAgencyHRID);

                hidPressAgencyHRID.Value = Utility.GetString(param.PressAgencyHR?.PressAgencyHRID);
                ShowMessage("Lưu thành công");
                LoadData();
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

        private void RequestSave_PressAgencyHR(object sender, EventArgs e)
        {
            try
            {
                throw new SMXException("Vui lòng bấm Lưu trước khi thực hiện thao tác này.");
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
            ucPressAgencyHRAlert.RequestSave_PressAgencyHR += RequestSave_PressAgencyHR;
            ucPressAgencyHRHistory.RequestSave_PressAgencyHR += RequestSave_PressAgencyHR;
            ucPressAgencyHRRelatives.RequestSave_PressAgencyHR += RequestSave_PressAgencyHR;

            string key = UIUtility.GetParamId();
            int? empID = 0;
            int[] arrRefParam;

            Utility.Decrypt(key, out empID, out arrRefParam);

            if (empID != Profiles.MyProfile.EmployeeID && arrRefParam.Length != 2)
                Response.Redirect(PageURL.ErrorPage);

            var a = Utility.Decrypt(Request.QueryString["ID"]);

            hidPressAgencyID.Value = Utility.GetString(arrRefParam[0]);
            hidPressAgencyHRID.Value = Utility.GetString(arrRefParam[1]);
            UIUtility.BindDicToDropDownList(ddlAttitude, SMX.Attitude.dicDesc);

            lnkExit.NavigateUrl = string.Format("Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID,
                new int[] { Utility.GetNullableInt(hidPressAgencyID.Value).GetValueOrDefault(0),
                    Utility.GetNullableInt(hidPressAgencyHRID.Value).GetValueOrDefault(0), }), GetParamCallbackButton());
        }

        private void LoadData()
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyHR_ByID);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value) };
            MainController.Provider.Execute(param);

            lblPressAgencyName.Text = string.IsNullOrWhiteSpace(param.PressAgencyHR?.PressAgencyName) ? "" : "- " + param.PressAgencyHR.PressAgencyName.ToUpper();
            lblHRTypeName.Text = string.IsNullOrWhiteSpace(param.PressAgencyHR?.PressAgencyTypeString) ? "" : "- " + param.PressAgencyHR.PressAgencyTypeString.ToUpper();

            BindObjectToForm(param.PressAgencyHR);

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { param.Attachment?.AttachmentID ?? 0 })), 1000, 600);
            divViewDetailImage.Attributes.Add("onclick", url);

            BindDataImage(img, param.Attachment);

            var pressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);

            ucPressAgencyHRHistory.BindData(pressAgencyHRID, true);

            ucPressAgencyHRAlert.BindData(pressAgencyHRID, true);

            ucPressAgencyHRRelatives.BindData(pressAgencyHRID, true);

            ucPressAgencyHR.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), param.PressAgencyHR.Attitude, param.PressAgencyHR.PressAgencyHRID, false);

            LoadDataImages();

            ucComment.SetupForm();
            ucComment.LoadData(pressAgencyHRID, SMX.CommentRefType.PressAgencyHR, true);

            // đổ danh sách nhóm quyền và selected giá trị mặc định
            BindPermissionGroups();
            SelectDefaultGroups();
        }

        private void BindObjectToForm(agency_PressAgencyHR item)
        {
            if (item != null)
            {
                txtFullName.Text = item.FullName;
                txtPosition.Text = item.Position;
                ddlAttitude.SelectedValue = Utility.GetString(item.Attitude);
                ltrAge.Text = item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty;
                dpkDOB.SelectedDate = item.DOB;
                txtMobile.Text = item.Mobile;
                txtEmail.Text = item.Email;
                txtAddress.Text = item.Address;
                txtHobby.Text = item.Hobby;
                txtRelatedInformation.Text = item.RelatedInformation;

                ltrAttitude1.Text = Utility.GetDictionaryValue(SMX.Attitude.dicDesc, item.Attitude);

                switch (item.Attitude)
                {
                    case SMX.Attitude.TichCuc:
                        ltrAttitude1.Attributes["class"] = "positive";
                        break;
                    case SMX.Attitude.TieuCuc:
                        ltrAttitude1.Attributes["class"] = "negative";
                        break;
                    case SMX.Attitude.TrungLap:
                        ltrAttitude1.Attributes["class"] = "medium";
                        break;
                }
            }
        }

        private agency_PressAgencyHR GetData()
        {
            agency_PressAgencyHR result = new agency_PressAgencyHR();

            result.PressAgencyID = Utility.GetNullableInt(hidPressAgencyID.Value);
            result.PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
            result.FullName = txtFullName.Text;
            result.Position = txtPosition.Text;
            result.Attitude = Utility.GetNullableInt(ddlAttitude.SelectedValue);
            result.DOB = dpkDOB.SelectedDate;
            result.Mobile = txtMobile.Text;
            result.Email = txtEmail.Text;
            result.Address = txtAddress.Text;
            result.Hobby = txtHobby.Text;
            result.RelatedInformation = txtRelatedInformation.Text;

            // lấy giá trị của các nhóm quyền
            List<string> selectedValues = new List<string>();
            if (cmbGroups != null)
            {
                foreach (ListItem item in cmbGroups.Items)
                {
                    if (item.Selected)
                    {
                        selectedValues.Add(item.Text);
                    }
                }
            }
            result.NamePermissionGroups = selectedValues;
            return result;
        }

        private void UploadImage(int? refID)
        {
            adm_Attachment item = new adm_Attachment();
            item.AttachmentID = Utility.GetNullableInt(hidAttID.Value);
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.RefID = refID;
            item.RefType = SMX.AttachmentRefType.PressAgencyHR;
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

        private void UploadFiles()
        {
            adm_Attachment item = new adm_Attachment();
            item.FileName = fileUploadOtherImage.FileName;
            item.DisplayName = fileUploadOtherImage.FileName;
            item.FileSize = fileUploadOtherImage.PostedFile.ContentLength;
            item.ContentType = fileUploadOtherImage.PostedFile.ContentType;
            item.Description = txtDescription.Text;
            item.RefID = Utility.GetNullableInt(hidPressAgencyHRID.Value);
            item.RefType = SMX.AttachmentRefType.PressAgencyHROtherImage;
            item.FileContent = fileUploadOtherImage.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private void LoadDataImages()
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.LoadDataOtherImage_PressAgencyHR);
            param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value) };
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

        private void BindDataImage(HtmlImage imgUI, adm_Attachment imgData)
        {
            if (imgData != null)
            {
                hidAttID.Value = Utility.GetString(imgData.AttachmentID);

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