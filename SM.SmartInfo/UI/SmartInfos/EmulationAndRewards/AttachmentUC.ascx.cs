using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.SmartInfos.EmulationAndRewards
{
    public partial class AttachmentUC : UserControl
    {
        private List<adm_Attachment> _ListAttachment_NotYetUploaded
        {
            get
            {
                List<adm_Attachment> lstItem = (List<adm_Attachment>)ViewState["ListAttachment_NotYetUploaded"];
                if (lstItem == null)
                    lstItem = new List<adm_Attachment>();
                return lstItem;
            }
            set { ViewState["ListAttachment_NotYetUploaded"] = value; }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (scriptManager != null)
            {
                scriptManager.RegisterPostBackControl(btnAddDocmument);
            }
            SetupForm(Utility.GetNullableInt(hidRefType.Value).GetValueOrDefault(0));
        }

        protected void btnAddDocmument_Click(object sender, EventArgs e)
        {
            try
            {
                List<adm_Attachment> lstItem = _ListAttachment_NotYetUploaded;
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
                        attachment.RefType = Utility.GetNullableInt(hidRefType.Value);
                        attachment.Description = txtDescription.Text;
                        attachment.CreatedBy = Profiles.MyProfile.UserName;
                        attachment.CreatedDTG = DateTime.Now;
                        attachment.RefCode = hidCode.Value;
                        attachment.FileSize = postedFile.ContentLength;
                        attachment.DisplayName = postedFile.FileName;
                        attachment.ContentType = postedFile.ContentType;

                        MemoryStream ms = new MemoryStream(postedFile.ContentLength);
                        postedFile.InputStream.CopyTo(ms);
                        attachment.FileURL = UploadFileToTempFolder(attachment.FileName, ms.ToArray(), attachment.FileSize);

                        var existsItem = lstItem.FirstOrDefault(x => x.RefCode == attachment.RefCode);
                        if (existsItem == null)
                            lstItem.Add(attachment);
                        else
                            lstItem[lstItem.FindIndex(x => x.Equals(existsItem))] = attachment;
                    }
                }

                popAttachment.Hide();

                _ListAttachment_NotYetUploaded = lstItem;

                grdListDocuments.DataSource = _ListAttachment_NotYetUploaded;

                ViewState["AttachmentsNotUploadYet"] = _ListAttachment_NotYetUploaded;

                grdListDocuments.DataBind();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdListDocuments_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    adm_Attachment attachment = e.Item.DataItem as adm_Attachment;

                    if (attachment != null)
                    {
                        var systemParameter = GlobalCache.GetItemByFeatureIDAndCode(SMX.Features.DocCodeEmulationAndRewarded, attachment.RefCode);
                        UIUtility.SetGridItemIText(e.Item, "ltrRefCodeName", systemParameter != null ? systemParameter.Name : string.Empty);
                        UIUtility.SetGridItemHidden(e.Item, "hidAttachmentID", attachment.AttachmentID);
                        UIUtility.SetGridItemHidden(e.Item, "hidECMItemID", attachment.ECMItemID);
                        UIUtility.SetGridItemHidden(e.Item, "hidRefCode", attachment.RefCode);
                        UIUtility.SetGridItemIText(e.Item, "ltrCreatedBy", attachment.CreatedBy);
                        UIUtility.SetGridItemIText(e.Item, "ltrFileName", attachment.FileName);
                        DateTime? createdDTG = attachment.CreatedDTG;
                        if (createdDTG != null)
                        {
                            string createdDTGString = attachment.CreatedDTG.Value.ToString("dd/MM/yyyy hh:mm:ss tt");
                            UIUtility.SetGridItemIText(e.Item, "ltrCreatedDTG", createdDTGString);
                        }
                    }
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdListDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionUpload:
                        ShowPopup();
                        SetupPopupEditDocument(e.Item);
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void SetupForm(int? refType)
        {
            List<adm_Attachment> lstAttachments = new List<adm_Attachment>();
            grdListDocuments.DataSource = lstAttachments;
            grdListDocuments.DataBind();

            //Danh sách tài liệu cấu hình mức độ yêu cầu tương ứng với Tác nghiệp (ActionType)
            //Lấy danh sách code trong SystemParameter có FeatureID = 1313
            List<SystemParameter> lstDocSys = GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.DocCodeEmulationAndRewarded).Distinct().ToList();

            ddlRefCode.Items.Clear();
            ddlRefCode.DataSource = lstDocSys;
            ddlRefCode.DataBind();
            ddlRefCode.Items.Insert(0, new ListItem("ABC", "Value1"));

            hidRefType.Value = Utility.GetString(refType);

            LoadData();
        }

        public List<adm_Attachment> GetData()
        {
            List<adm_Attachment> lstResult = ViewState["AttachmentsNotUploadYet"] as List<adm_Attachment>;

            if(lstResult == null)
            {
                return null;
            }

            lstResult.ForEach(x => x.FileContent = string.IsNullOrWhiteSpace(x.FileURL) ? null : File.ReadAllBytes(x.FileURL));

            return lstResult;
        }

        //Xóa file temp sau khi dùng xong
        public void ClearTempFile()
        {
            foreach (var att in _ListAttachment_NotYetUploaded)
            {
                if (!string.IsNullOrWhiteSpace(att.FileURL))
                    File.Delete(att.FileURL);
            }

            _ListAttachment_NotYetUploaded = new List<adm_Attachment>();
        }

        #endregion

        #region Private Methods

        private void ShowPopup()
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnAddDocmument);
            ClearPopup();
            popAttachment.Show();
        }

        private void SetupPopupEditDocument(DataGridItem item)
        {
            //ddlRefCode.SelectedValue = UIUtility.GetGridItemHiddenValue(item, "hidRefCode");
            HiddenField a = item.FindControl("hidRefCode") as HiddenField;
            hidCode.Value = lbRefCode.Text = a.Value;
        }

        private void ClearPopup()
        {
            //ddlRefCode.SelectedValue = "Value1";
            txtDescription.Text = string.Empty;
        }

        private void LoadData()
        {
            List<adm_Attachment> lstAttachment = new List<adm_Attachment>();

            //Danh sách tài liệu cấu hình mức độ yêu cầu tương ứng với Tác nghiệp (ActionType)
            List<SystemParameter> lstDocSys = GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.DocCodeEmulationAndRewarded).Distinct().ToList();

            //Merge danh sách tài liệu trong DB với danh sách tài liệu cấu hình
            List<adm_Attachment> lstMergeAttachment = new List<adm_Attachment>();
            for (int i = 1; i <= lstDocSys.Count; i++)
            {
                //Tìm tài liệu tương ứng với cấu hình theo RefCode
                List<adm_Attachment> lstAtt = lstAttachment.Where(c => c.RefCode == lstDocSys[i - 1].Code).ToList();

                //Nếu tìm thấy thì tài liệu nằm trong danh sách cấu hình => Thêm mức độ yêu cầu vào và cho vào danh sách hiển thị
                if (lstAtt != null && lstAtt.Count > 0)
                {
                    lstMergeAttachment.AddRange(lstAtt);
                }
                //Ngược lại thì tài liệu tạo tài liệu mới để làm danh sách mặc định
                else
                {
                    adm_Attachment att = new adm_Attachment();
                    att.RefCode = lstDocSys[i - 1].Code;

                    lstMergeAttachment.Add(att);
                }
            }

            _ListAttachment_NotYetUploaded = lstMergeAttachment;
            UIUtility.BindDataGrid(grdListDocuments, lstMergeAttachment);
        }       

        public string UploadFileToTempFolder(string fileName, byte[] fileContent, int? fileSize)
        {
            adm_Attachment item = new adm_Attachment()
            {
                FileName = fileName,
                FileContent = fileContent,
                FileSize = fileSize
            };

            ValidateAttachment(item);

            FileInfo fi = new FileInfo(fileName);
            string ext = fi.Extension;

            string fileTemp = string.Format("{0}{1}", Path.Combine(ConfigUtils.TemporaryFolder, Guid.NewGuid().ToString()), ext);
            File.WriteAllBytes(fileTemp, fileContent);

            return fileTemp;
        }

        private void ValidateAttachment(adm_Attachment item)
        {
            List<string> lstError = new List<string>();

            if (string.IsNullOrEmpty(item.FileName))
                lstError.Add("FileName: Tên file chưa có dữ liệu");

            if (item.FileSize > Utils.ConfigUtils.MaxUploadSize)
                lstError.Add("Dung lượng file lớn hơn mức giới hạn cho phép.");

            if (lstError.Count > 0)
                throw new SMXException(lstError);
        }

        #endregion
    }
}