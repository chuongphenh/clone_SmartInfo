using System;
using System.Collections.Generic;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;

using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.Utils;

namespace SM.SmartInfo.UI.Administrations.Province
{
    public partial class Default : BasePage, ISMForm
    {
        private const string REPORT_TYPE = SMX.DynamicReport.AdministrationProvince;

        #region events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                SetupForm();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    popupExporting.ReportDisplayName = SMX.DynamicReport.dicReports[REPORT_TYPE].DisplayName;
        //    popupExporting.Show();
        //}

        //protected void popupExporting_StartExport(object sender, EventArgs e)
        //{
        //    ucDynamicReport.ExportReport(popupExporting.ReportDisplayName);
        //}

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                popupUploadFile.Show();
            }
            catch (SMXException ex)
            {
                base.ShowError(ex);
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuImportExcel.HasFile)
                {
                    throw new SMXException("Bạn chưa chọn tài liệu.");
                }

                bool isExcelFile = FileUtil.IsExcelFile(fuImportExcel.FileName, fuImportExcel.PostedFile.ContentType);
                if (!isExcelFile)
                {
                    throw new SMXException("Loại file này không được hỗ trợ. Chỉ hỗ trợ file có định dạng excel");
                }

                SoftMart.Service.BatchProcessing.BatchProcessingApi.AddImporting(
                    fuImportExcel.FileBytes, fuImportExcel.FileName, SMX.ImportingType.ImportDiaChi);

                base.ShowMessage("Import thành công");

                popupUploadFile.Hide();
            }
            catch (SMXException ex)
            {
                base.ShowError(ex);
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            SetupForm();
        }
     
        #endregion

        public void SetupForm()
        {
            // Put conditions to report
            hypAdd.NavigateUrl = PageURL.AddNew;
            hplDownload.NavigateUrl = string.Format("/Templates/{0}", Utility.GetDictionaryValue(SMX.ImportingType.DicTemplate, SMX.ImportingType.ImportDiaChi));
            List<KeyValuePair<string, object>> conditions = new List<KeyValuePair<string, object>>();
            ucDynamicReport.BuildReportForm(REPORT_TYPE, conditions);
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { hypAdd    , FunctionCode.ADD      },
                    { this,     PermissionManager.Shared.FunctionCode.VIEW },
                    {btnImport, PermissionManager.Shared.FunctionCode.ADD },
                };
            }
        }
    }
}