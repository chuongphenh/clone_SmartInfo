using System;
using System.IO;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Web;
using System.Data;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.SmartInfos.EmulationAndRewards
{
    public partial class AddNew : BasePage
    {
        private adm_Attachment _Attachment_CaNhan
        {
            get
            {
                adm_Attachment lstItem = (adm_Attachment)ViewState["_Attachment_CaNhan"];
                if (lstItem == null)
                    lstItem = new adm_Attachment();
                return lstItem;
            }
            set { ViewState["_Attachment_CaNhan"] = value; }
        }

        private adm_Attachment _Attachment_DonVi
        {
            get
            {
                adm_Attachment lstItem = (adm_Attachment)ViewState["_Attachment_DonVi"];
                if (lstItem == null)
                    lstItem = new adm_Attachment();
                return lstItem;
            }
            set { ViewState["_Attachment_DonVi"] = value; }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "ResetFileName", "resetFileName();", true);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SetupForm()
        {
            EmulationAndRewardParam param2 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingPeriodNoPaging);
            MainController.Provider.Execute(param2);

            ddlAwardingPeriod.DataSource = param2.ListAwardingPeriod;
            ddlAwardingPeriod.DataBind();

            EmulationAndRewardParam param3 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingLevelNoPaging);
            MainController.Provider.Execute(param3);

            ddlAwardingLevel.DataSource = param3.ListAwardingLevel;
            ddlAwardingLevel.DataBind();

            EmulationAndRewardParam param4 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingTypeNoPaging);
            MainController.Provider.Execute(param4);

            ddlAwardingType.DataSource = param4.ListAwardingType;
            ddlAwardingType.DataBind();

            if (param3.ListAwardingLevel.Count > 0)
            {
                if (param3.ListAwardingLevel[0].Category == 1)
                {
                    liCaNhan.Visible = true;
                    liDonVi.Visible = false;
                }
                else if (param3.ListAwardingLevel[0].Category == 2)
                {
                    liCaNhan.Visible = false;
                    liDonVi.Visible = true;
                }
                else
                {
                    liCaNhan.Visible = true;
                    liDonVi.Visible = true;
                }
            }
            else
            {
                liCaNhan.Visible = true;
                liDonVi.Visible = true;
            }
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this                  , FunctionCode.ADD },
                };
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateItem(true);
                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnDownloadTemplate_CaNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = SMX.ImportingType.DicTemplate[SMX.ImportingType.ImportCaNhanKhenThuong];
                string filePath = Path.Combine(ConfigUtils.TemplateFolder, fileName);
                byte[] fileContent = File.ReadAllBytes(filePath);

                SoftMart.Core.Utilities.DownloadHelper.PushExcelContentForDownload(fileContent, fileName);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnDownloadTemplate_DonVi_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = SMX.ImportingType.DicTemplate[SMX.ImportingType.ImportDonViKhenThuong];
                string filePath = Path.Combine(ConfigUtils.TemplateFolder, fileName);
                byte[] fileContent = File.ReadAllBytes(filePath);

                SoftMart.Core.Utilities.DownloadHelper.PushExcelContentForDownload(fileContent, fileName);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private void UpdateItem(bool isSaveComplete)
        {
            try
            {
                er_EmulationAndReward item = new er_EmulationAndReward()
                {
                    Year = (int?)numYear.Value,
                    Event = ddlAwardingPeriod.SelectedItem == null ? null : ddlAwardingPeriod.SelectedItem.Text,
                    EmulationAndRewardUnit = txtUnit.Text,
                    Deleted = 0,
                    SubjectRewarded = Utility.GetNullableInt(ddlAwardingLevel.SelectedValue),
                    PeriodId = Utility.GetNullableInt(ddlAwardingPeriod.SelectedValue),
                    AwardingTypeId = Utility.GetNullableInt(ddlAwardingType.SelectedValue),
                    Version = 1,
                    CreatedBy = Profiles.MyProfile.UserName,
                    CreatedDTG = DateTime.Now
                };

                List<string> lstErr = new List<string>();

                EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.AddNewEmulationAndReward);
                param.er_EmulationAndReward = item;
                param.IsSaveComplete = isSaveComplete;
                param.AwardingTypeName = ddlAwardingType.SelectedItem == null ? null : ddlAwardingType.SelectedItem.Text;

                switch (item.SubjectRewarded)
                {
                    case 0:
                        {
                            if (!fileUploadCaNhan.HasFile || !fileUploadDonVi.HasFile)
                                lstErr.Add("Chưa chọn file Cá nhân hoặc Đơn vị được khen thưởng");
                            else
                            {
                                bool isExcelFile = FileUtil.IsExcelFile(fileUploadCaNhan.FileName, fileUploadCaNhan.PostedFile.ContentType) == true ?
                                    FileUtil.IsExcelFile(fileUploadDonVi.FileName, fileUploadDonVi.PostedFile.ContentType) :
                                    FileUtil.IsExcelFile(fileUploadCaNhan.FileName, fileUploadCaNhan.PostedFile.ContentType);

                                if (!isExcelFile)
                                {
                                    throw new SMXException("Loại file này không được hỗ trợ. Chỉ hỗ trợ file có định dạng excel");
                                }
                                else
                                {
                                    param.ListEmulationAndRewardSubject = GetExcelData(fileUploadCaNhan.FileBytes, 1);
                                    param.ListEmulationAndRewardSubject.AddRange(GetExcelData(fileUploadDonVi.FileBytes, 2));
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            if (!fileUploadCaNhan.HasFile)
                                lstErr.Add("Chưa chọn file Cá nhân được khen thưởng");
                            else
                            {
                                bool isExcelFile = FileUtil.IsExcelFile(fileUploadCaNhan.FileName, fileUploadCaNhan.PostedFile.ContentType);

                                if (!isExcelFile)
                                {
                                    throw new SMXException("Loại file này không được hỗ trợ. Chỉ hỗ trợ file có định dạng excel");
                                }
                                else
                                {
                                    param.ListEmulationAndRewardSubject = GetExcelData(fileUploadCaNhan.FileBytes, 1);
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            if (!fileUploadDonVi.HasFile)
                                lstErr.Add("Chưa chọn file Đơn vị được khen thưởng");
                            else
                            {
                                bool isExcelFile = FileUtil.IsExcelFile(fileUploadDonVi.FileName, fileUploadDonVi.PostedFile.ContentType);

                                if (!isExcelFile)
                                {
                                    throw new SMXException("Loại file này không được hỗ trợ. Chỉ hỗ trợ file có định dạng excel");
                                }
                                else
                                {
                                    param.ListEmulationAndRewardSubject = GetExcelData(fileUploadDonVi.FileBytes, 2);
                                }
                            }
                            break;
                        }
                }

                param.AttachmentCaNhan = _Attachment_CaNhan;
                param.AttachmentDonVi = _Attachment_DonVi;

                switch (item.SubjectRewarded)
                {
                    case SMX.EmulationAndRewardSubjectRewarded.All:
                        if (param.AttachmentCaNhan == null || param.AttachmentDonVi == null)
                            lstErr.Add("Chưa chọn file Cá nhân hoặc Đơn vị được khen thưởng");
                        break;
                    case SMX.EmulationAndRewardSubjectRewarded.CaNhan:
                        if (param.AttachmentCaNhan == null)
                            lstErr.Add("Chưa chọn file Cá nhân được khen thưởng");
                        break;
                    case SMX.EmulationAndRewardSubjectRewarded.DonViToChuc:
                        if (param.AttachmentDonVi == null)
                            lstErr.Add("Chưa chọn file Đơn vị được khen thưởng");
                        break;
                }
                if (lstErr.Count > 0)
                    throw new SMXException(lstErr);

                param.ListAttachment = ucAttachment.GetData();

                MainController.Provider.Execute(param);

                ucAttachment.ClearTempFile();
            }
            catch (SMXException ex)
            {
                throw ex;
            }
        }

        private List<er_EmulationAndRewardSubject> GetExcelData(byte[] fileContent, int awardingLevel)
        {
            var result = new List<er_EmulationAndRewardSubject>();

            DataTable dataTable = ExcelReader.ConvertExcelFileBytesToDataTable(fileContent);

            bool check = false;

            foreach (DataRow row in dataTable.Rows)
            {
                if (!check)
                {
                    if (row[1].ToString() == "STT")
                    {
                        check = true;
                    }
                    continue;
                }

                if (row.IsNull(2) && check)
                {
                    break;
                }

                if (string.IsNullOrEmpty(row[2].ToString()) || string.IsNullOrEmpty(row[3].ToString()) || string.IsNullOrEmpty(row[4].ToString()) ||
                    string.IsNullOrEmpty(row[5].ToString()) || string.IsNullOrEmpty(row[6].ToString()) || string.IsNullOrEmpty(row[7].ToString()))
                {
                    break;
                }

                var Code = row[2].ToString();
                var Name = row[3].ToString();
                var LatestTitle = row[4].ToString();
                var Unit = row[5].ToString();
                var Phone = row[7].ToString();
                var Email = row[6].ToString();

                result.Add(new er_EmulationAndRewardSubject()
                {
                    Code = Code,
                    Name = Name,
                    LatestTitle = LatestTitle,
                    Unit = Unit,
                    Email = Email,
                    Mobile = Phone,
                    Type = awardingLevel,
                    Deleted = 0,
                    Version = 1,
                    CreatedBy = Profiles.MyProfile.UserName,
                    CreatedDTG = DateTime.Now
                });
            }

            return result;
        }

        protected void btnReuploadFile_CaNhan_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (HttpPostedFile postedFile in fileUploadCaNhan.PostedFiles)
                {
                    if (postedFile.ContentLength > 0)
                    {
                        MemoryStream ms = new MemoryStream(postedFile.ContentLength);
                        postedFile.InputStream.CopyTo(ms);

                        _Attachment_CaNhan = new adm_Attachment()
                        {
                            ContentType = postedFile.ContentType,
                            FileName = postedFile.FileName,
                            FileContent = ms.ToArray(),
                        };
                    }
                }

                if (_Attachment_CaNhan != null)
                    hidFileCaNhan_Name.Value = _Attachment_CaNhan.FileName;
            }
            catch (SMXException ex)
            {
                throw ex;
            }
        }

        protected void btnReuploadFile_DonVi_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (HttpPostedFile postedFile in fileUploadCaNhan.PostedFiles)
                {
                    if (postedFile.ContentLength > 0)
                    {
                        MemoryStream ms = new MemoryStream(postedFile.ContentLength);
                        postedFile.InputStream.CopyTo(ms);

                        _Attachment_DonVi = new adm_Attachment()
                        {
                            ContentType = postedFile.ContentType,
                            FileName = postedFile.FileName,
                            FileContent = ms.ToArray(),
                        };
                    }
                }

                if (_Attachment_DonVi != null)
                    hidFileDonVi_Name.Value = _Attachment_DonVi.FileName;
            }
            catch (SMXException ex)
            {
                throw ex;
            }
        }

        protected void ddlAwardingLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlAwardingLevel.SelectedValue) == 1)
                {
                    liCaNhan.Visible = true;
                    liDonVi.Visible = false;
                }
                else if (Convert.ToInt32(ddlAwardingLevel.SelectedValue) == 2)
                {
                    liCaNhan.Visible = false;
                    liDonVi.Visible = true;
                }
                else
                {
                    liCaNhan.Visible = true;
                    liDonVi.Visible = true;
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
    }
}