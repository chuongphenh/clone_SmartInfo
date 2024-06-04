using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.CollateralManagement.UI;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.UserControls.Attachment
{
    public delegate void SetPermissionForAttachment(List<adm_Attachment> attachments);

    public partial class AttachmentUC : BaseUserControl
    {
        public event EventHandler PopupClosed;

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            UIUtility.RegisterPostBackControl(this.Page, btnShowUpload, btnPopupUpload);
        }

        protected void btnShowUpload_Click(object sender, EventArgs e)
        {
            txtDescription.Text = string.Empty;

            popUpload.Show();
        }

        protected void btnPopupUpload_Click(object sender, EventArgs e)
        {
            try
            {
                adm_Attachment item = new adm_Attachment();
                item.FileName = fileUpload.FileName;
                item.DisplayName = fileUpload.FileName;
                item.FileSize = fileUpload.PostedFile.ContentLength;
                item.ContentType = fileUpload.PostedFile.ContentType;
                item.Description = txtDescription.Text;
                item.RefID = Utility.GetNullableInt(hidRefID.Value);
                item.RefType = Utility.GetNullableInt(hidRefType.Value);
                item.FileContent = fileUpload.FileBytes;

                AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
                param.adm_Attachment = item;
                MainController.Provider.Execute(param);

                popUpload.Hide();
                BindData();

                if (PopupClosed != null)
                    PopupClosed(sender, e);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void popUpload_PopupClosed(object sender, EventArgs e)
        {
            popUpload.Hide();

            if (PopupClosed != null)
                PopupClosed(sender, e);
        }

        protected void grdData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                adm_Attachment item = e.Item.DataItem as adm_Attachment;

                UIUtility.SetGridItemHidden(e.Item, "hidAttachmentID", item.AttachmentID);

                string docName = string.IsNullOrWhiteSpace(item.Description) ? item.FileName : item.Description;
                UIUtility.SetGridItemIText(e.Item, "ltrName", docName);

                UIUtility.SetGridItemIText(e.Item, "ltrCreatedDTG", Utility.GetDateTimeString(item.CreatedDTG));
                UIUtility.SetGridItemIText(e.Item, "ltrCreatedBy", item.FullNameCreateBy);

                if (item.AttachmentID.HasValue)
                {
                    int? empID = Profiles.MyProfile.EmployeeID;
                    var urlDownLoad = string.Format(PageURL.DownloadDocument, Utility.Encrypt(empID, item.AttachmentID));
                    urlDownLoad = UIUtility.BuildHyperlinkWithPopup(
                        "<i class=\"fa fa-lg fa-cloud-download\" aria-hidden=\"true\"></i>", urlDownLoad);
                    UIUtility.SetGridItemIText(e.Item, "lblDownLoad", urlDownLoad);
                }

                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                btnDelete.CommandName = SMX.ActionDelete;
                btnDelete.OnClientClick = "return confirm('Chắc chắn bạn muốn xóa tài liệu này !')";
                btnDelete.Visible = item.AttachmentID.HasValue;
            }
        }

        protected void grdData_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Upload":
                    {
                        txtDescription.Text = string.Empty;
                        popUpload.Show();
                        break;
                    }
                case SMX.ActionDelete:
                    {
                        int? attachmentID = Utility.GetNullableInt(UIUtility.GetGridItemHiddenValue(e.Item, "hidAttachmentID"));
                        DeletedAttachmentFile(attachmentID);
                        break;
                    }
            }
        }
        #endregion

        #region Bind data
        public void BindData(int? refID, int? refType, bool allowEdit, bool allowDelete)
        {
            hidAllowEdit.Value = allowEdit.ToString();
            hidRefID.Value = Utility.GetString(refID);
            hidRefType.Value = Utility.GetString(refType);

            grdData.Columns[grdData.Columns.Count - 1].Visible = allowDelete;
            grdData.Columns[grdData.Columns.Count - 2].Visible = allowEdit;
            btnShowUpload.Visible = allowEdit;

            if (allowEdit)
                SetupForm();

            BindData();
        }

        private void BindData()
        {
            // lay du lieu
            adm_Attachment att = new adm_Attachment();
            att.RefID = Utility.GetNullableInt(hidRefID.Value);
            att.RefType = Utility.GetNullableInt(hidRefType.Value);

            AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.GetListAttachment);
            param.adm_Attachment = att;
            MainController.Provider.Execute(param);

            // Sap sep lai thu tu hien thi danh sach
            List<adm_Attachment> lstAtt = param.adm_Attachments;
            grdData.DataSource = lstAtt;
            grdData.DataBind();
        }

        private void SetupForm()
        {
            adm_Attachment att = new adm_Attachment();
            att.RefID = Utility.GetNullableInt(hidRefID.Value);
            att.RefType = Utility.GetNullableInt(hidRefType.Value);
        }
        #endregion

        #region Delete attachment
        private void DeletedAttachmentFile(int? attachmentID)
        {
            try
            {
                AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.DeleteDocument);
                param.adm_Attachment = new adm_Attachment() { AttachmentID = attachmentID };
                MainController.Provider.Execute(param);

                BindData();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        #endregion
    }
}