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
using static SM.SmartInfo.UI.SmartInfos.PressAgencies.Default;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
{
    public partial class DisplayUC : BaseUserControl
    {
        public event EventHandler RequestEdit;

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

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CurrrentTab != null)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Show Tab", "showTab(" + CurrrentTab + ");", true);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestEdit != null)
                    RequestEdit(null, null);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int? itemID = Utility.GetNullableInt(hidPressAgencyID.Value);

                if (itemID != null)
                {
                    var param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgency);
                    param.PressAgency = new agency_PressAgency() { PressAgencyID = itemID };
                    MainController.Provider.Execute(param);

                    NotificationParam ntfParam = new NotificationParam(FunctionType.Notification.DeleteNotificationByHRAlertID);
                    ntfParam.AlertID = (int)itemID;
                    LogManager.WebLogger.LogDebug("ID Tổ chức khi xóa trong Noti" + ntfParam.AlertID);
                    MainController.Provider.Execute(ntfParam);
                }

                Response.Redirect(PageURL.Default);
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
            if (RequestItemPermission != null)
            {
                var param = new RequestPermissionArgs();
                RequestItemPermission(param);

                if (!param.lstRight.Exists(x => x.FunctionCode == FunctionCode.DISPLAY))
                    Response.Redirect(PageURL.ErrorPage);

                btnEdit.Visible = param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT);

                btnDelete.Visible = param.lstRight.Exists(x => x.FunctionCode == FunctionCode.DELETE);
            }
        }

        public void BindData(agency_PressAgency pressAgency)
        {
            CurrrentTab = (int)ActionTab.DanhSachLienHe;

            hidPressAgencyID.Value = Utility.GetString(pressAgency.PressAgencyID);

            BindObject2Form(pressAgency);

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/ViewImages/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { pressAgency.Attachment?.AttachmentID ?? 0 })), 1000, 600);
            divViewDetailImage.Attributes.Add("onclick", ResolveUrl(url));

            BindDataImage(img, pressAgency.Attachment);

            ucPressAgencyHR.SetupForm();
            ucPressAgencyHR.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), btnEdit.Visible);

            ucPressAgencyHistory.SetupForm();
            ucPressAgencyHistory.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), btnEdit.Visible);

            ucPressAgencyMeeting.SetupForm();
            ucPressAgencyMeeting.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), btnEdit.Visible);

            ucRelationshipWithMB.SetupForm();
            ucRelationshipWithMB.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), btnEdit.Visible);

            ucOtherImage.SetupForm();
            ucOtherImage.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), btnEdit.Visible);

            ucComment.SetupForm();
            ucComment.LoadData(pressAgency.PressAgencyID, SMX.CommentRefType.PressAgency, true);

            ucRelationsPressAgency.SetupForm();
            ucRelationsPressAgency.BindData(Utility.GetNullableInt(hidPressAgencyID.Value), true);
        }

        #endregion

        #region Private Methods

        private void BindObject2Form(agency_PressAgency item)
        {
            string typeName = string.Empty;
            if (item.Type == SMX.PressAgencyType.Other)
                typeName = item.TypeName;
            else
                typeName = Utility.GetDictionaryValue(SMX.PressAgencyType.dicDesc, item.Type);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListAgencyType);

            MainController.Provider.Execute(param);

            var typeDic = new Dictionary<int?, string>();
            foreach (var type in param.ListAgencyType)
            {
                typeDic.Add(type.Id, type.TypeName);
            }
            ltrName.Text = string.Format("{0} - {1}", typeName, item.Name);
            ltrAgency.Text = item.Agency;
            ltrEmail.Text = item.Email;
            ltrAddress.Text = item.Address;
            ltrRelationshipWithMB.Text = Utility.GetDictionaryValue(SMX.RelationshipWithMB.dicDesc, item.RelationshipWithMB);
            ltrRate.Text = UIUtility.ConvertBreakLine2Html(item.Rate);
            ltrType.Text = Utility.GetDictionaryValue(typeDic, item.Type);
            ltrNote.Text = UIUtility.ConvertBreakLine2Html(item.Note);
            ltrPhone.Text = item.Phone;
            ltrFax.Text = item.Fax;
            ltrEstablishedDTG.Text = Utility.GetDateString(item.EstablishedDTG);
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