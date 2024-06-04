using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using static SM.SmartInfo.UI.SmartInfos.PressAgencies.Default;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class EditUC : BaseUserControl
    {
        public event EventHandler RequestExit;

        public delegate void SaveContinue(int? newsID);

        public event SaveContinue RequestSaveContinue;

        public delegate void RequestPermission(RequestPermissionArgs param);

        public event RequestPermission RequestItemPermission;

        public enum ActionTab
        {
            DanhSachLienHe,
            LichSuThayDoiNhanSu,
            LichSuHopTacGapGo,
            QuanHeGiuaCacCoQuanBaoChi,
            LichSuQuanHe,
            AnhKhac
        }

        public int? CurrrentTab
        {
            get
            {
                return Utility.GetNullableInt(hidCurrentTab.Value);
            }
            set
            {
                hidCurrentTab.Value = Utility.GetString(value);
            }
        }

        public int? PressAgencyID
        {
            get { return Utility.GetNullableInt(hidPressAgencyID.Value); }
            set { hidPressAgencyID.Value = Utility.GetString(value); }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ucPressAgencyHR.RequestSave_PressAgency += RequestSave_PressAgency;
                ucPressAgencyHistory.RequestSave_PressAgency += RequestSave_PressAgency;
                ucPressAgencyMeeting.RequestSave_PressAgency += RequestSave_PressAgency;
                ucRelationshipWithMB.RequestSave_PressAgency += RequestSave_PressAgency;
                ucRelationsPressAgency.RequestSave_PressAgency += RequestSave_PressAgency;
                ucOtherImage.RequestSave_PressAgency += RequestSave_PressAgency;

                if (CurrrentTab != null)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Show Tab", "showTab(" + CurrrentTab + ");", true);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgency);
                param.PressAgency = GetData();
                param.IsSaveComplete = true;
                MainController.Provider.Execute(param);

                if (fileUpload.HasFile)
                    UploadImage(param.PressAgency?.PressAgencyID);

                PressAgencyID = param.PressAgency.PressAgencyID;

                if (RequestExit != null)
                    RequestExit(null, null);
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
                var param = new PressAgencyParam(FunctionType.PressAgency.SavePressAgency);
                param.PressAgency = GetData();
                param.IsSaveComplete = false;
                MainController.Provider.Execute(param);

                if (fileUpload.HasFile)
                    UploadImage(param.PressAgency?.PressAgencyID);

                PressAgencyID = param.PressAgency.PressAgencyID;

                ShowMessage("Lưu thành công");

                if (RequestSaveContinue != null)
                    RequestSaveContinue(PressAgencyID);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Utility.GetNullableInt(ddlType.SelectedValue) == SMX.PressAgencyType.Other)
                    divTitleTypeName.Visible = divTypeName.Visible = true;
                else
                {
                    divTitleTypeName.Visible = divTypeName.Visible = false;
                    txtTypeName.Text = string.Empty;
                }
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
            ucPressAgencyHR.RequestSave_PressAgency += RequestSave_PressAgency;
            ucPressAgencyHistory.RequestSave_PressAgency += RequestSave_PressAgency;
            ucPressAgencyMeeting.RequestSave_PressAgency += RequestSave_PressAgency;
            ucRelationshipWithMB.RequestSave_PressAgency += RequestSave_PressAgency;
            ucRelationsPressAgency.RequestSave_PressAgency += RequestSave_PressAgency;
            ucOtherImage.RequestSave_PressAgency += RequestSave_PressAgency;

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListAgencyType);

            MainController.Provider.Execute(param);

            var typeDic = new Dictionary<int?, string>();
            foreach (var type in param.ListAgencyType)
            {
                typeDic.Add(type.Id, type.TypeName);
            }

            //UIUtility.BindDicToDropDownList(ddlType, SMX.PressAgencyType.dicDesc);
            UIUtility.BindDicToDropDownList(ddlType, typeDic);
            UIUtility.BindDicToDropDownList(ddlRelationshipWithMB, SMX.RelationshipWithMB.dicDesc);
        }

        private void RequestSave_PressAgency(object sender, EventArgs e)
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

        public void BindData(agency_PressAgency pressAgency)
        {
            RequestButtonPermission(pressAgency);

            CurrrentTab = (int)ActionTab.DanhSachLienHe;

            PressAgencyID = pressAgency.PressAgencyID;

            hidPressAgencyID.Value = Utility.GetString(pressAgency.PressAgencyID);

            BindObject2Form(pressAgency);

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { pressAgency.Attachment?.AttachmentID ?? 0 })), 1000, 600);
            divViewDetailImage.Attributes.Add("onclick", ResolveUrl(url));

            BindDataImage(img, pressAgency.Attachment);
            hidAttID.Value = Utility.GetString(pressAgency.Attachment?.AttachmentID);

            ucPressAgencyHR.SetupForm();
            ucPressAgencyHR.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);

            ucPressAgencyHistory.SetupForm();
            ucPressAgencyHistory.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);

            ucPressAgencyMeeting.SetupForm();
            ucPressAgencyMeeting.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);

            ucRelationshipWithMB.SetupForm();
            ucRelationshipWithMB.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);

            ucRelationsPressAgency.SetupForm();
            ucRelationsPressAgency.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);

            ucOtherImage.SetupForm();
            ucOtherImage.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);

            ucComment.SetupForm();
            ucComment.LoadData(pressAgency.PressAgencyID, SMX.CommentRefType.PressAgency, true);
            ucComment.Visible = divComment.Visible = PressAgencyID != null && PressAgencyID != 0;
        }

        #endregion

        #region Private Methods

        private void RequestButtonPermission(agency_PressAgency pressAgency)
        {
            if (RequestItemPermission != null)
            {
                var param = new RequestPermissionArgs();
                RequestItemPermission(param);

                if (pressAgency.PressAgencyID != null)
                {
                    if (!param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT))
                        Response.Redirect(PageURL.ErrorPage);
                }
                else
                {
                    if (!(param.lstRight.Exists(x => x.FunctionCode == FunctionCode.ADD)))
                        Response.Redirect(PageURL.ErrorPage);
                }
            }
        }

        private void BindObject2Form(agency_PressAgency item)
        {
            if (item != null)
            {
                txtName.Text = item.Name;
                ddlType.SelectedValue = Utility.GetString(item.Type);
                ddlType_SelectedIndexChanged(null, null);
                ddlRelationshipWithMB.SelectedValue = Utility.GetString(item.RelationshipWithMB);
                txtTypeName.Text = item.TypeName;
                txtAgency.Text = item.Agency;
                txtEmail.Text = item.Email;
                txtAddress.Text = item.Address;
                txtRate.Text = item.Rate;
                txtNote.Text = item.Note;
                numDisplayOrder.Value = item.DisplayOrder;
                txtPhone.Text = item.Phone;
                txtFax.Text = item.Fax;
                dpkEstablishedDTG.SelectedDate = item.EstablishedDTG;
            }
        }

        private void BindDataImage(HtmlImage imgUI, adm_Attachment imgData)
        {
            if (imgData != null)
            {
                imgUI.Alt = imgData.Description;
                imgUI.Src = GetImageURL(imgData);
                imgUI.Attributes["class"] = "";
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

        private void UploadImage(int? refID)
        {
            adm_Attachment item = new adm_Attachment();
            item.AttachmentID = Utility.GetNullableInt(hidAttID.Value);
            item.FileName = fileUpload.FileName;
            item.DisplayName = fileUpload.FileName;
            item.FileSize = fileUpload.PostedFile.ContentLength;
            item.ContentType = fileUpload.PostedFile.ContentType;
            item.RefID = refID;
            item.RefType = SMX.AttachmentRefType.PressAgency;
            item.FileContent = fileUpload.FileBytes;

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
            param.adm_Attachment = item;
            MainController.Provider.Execute(param);
        }

        private agency_PressAgency GetData()
        {
            agency_PressAgency result = new agency_PressAgency();

            result.PressAgencyID = Utility.GetNullableInt(hidPressAgencyID.Value);
            result.Name = txtName.Text;
            result.Agency = txtAgency.Text;
            result.Email = txtEmail.Text;
            result.Address = txtAddress.Text;
            result.Rate = txtRate.Text;
            result.Note = txtNote.Text;
            result.DisplayOrder = (int?)numDisplayOrder.Value;
            result.Phone = txtPhone.Text;
            result.Fax = txtFax.Text;
            result.EstablishedDTG = dpkEstablishedDTG.SelectedDate;
            result.Type = Utility.GetNullableInt(ddlType.SelectedValue);
            result.RelationshipWithMB = Utility.GetNullableInt(ddlRelationshipWithMB.SelectedValue);
            result.TypeName = txtTypeName.Text;

            return result;
        }

        #endregion
    }
}