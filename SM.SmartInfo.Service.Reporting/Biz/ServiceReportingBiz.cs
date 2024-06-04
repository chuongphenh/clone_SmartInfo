using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Reporting;
using SM.SmartInfo.Utils;
using SoftMart.Core.OfficeUtilities.Excel;
using SoftMart.Service.Reporting.Biz;
using SoftMart.Service.Reporting.SharedComponent.Entities;
using System.Collections.Generic;
using System.Data;

namespace SM.SmartInfo.Service.Reporting.Biz
{
    public class ServiceReportingBiz : BaseBizProvider
    {
        //private BIZ.Reportings.ReportingBiz _biz = new BIZ.Reportings.ReportingBiz();
        public override BaseBizProvider CreateInstance()
        {
            return new ServiceReportingBiz();
        }

        public override void ExportManually(string temporaryFolder, Flex_Reporting enReporting, ref string logContent)
        {
            // get command information
            ReportingParam param = SoftMart.Core.Utilities.SerializeHelper.Deserialize<ReportingParam>(enReporting.QueryContent);
            SharedComponent.EntityInfos.ReportInfo reportInfo = param.ReportInfo;
            DataTable data = null;
            Dictionary<string, object> dicFixCell = new Dictionary<string, object>();

            // validate and get information
            logContent = string.Format("{0}\n\r{1}", logContent, "Lay du lieu");

            switch (enReporting.ReportType)
            {
                case SMX.ManuallyReport.BC_ThongKeTinTuc:
                    {
                        //Log start
                        logContent = string.Format("{0}\n\r{1}", logContent, "Start BC thong ke tin tuc");

                        //fixed column
                        //dicFixCell.Add("C4", string.Format("KẾ HOẠCH {0}", param.vReportAMCBusinessResult.Year));
                        //dicFixCell.Add("G5", string.Format("KH{0}", param.vReportAMCBusinessResult.Year));
                        //dicFixCell.Add("X5", string.Format("LŨY KẾ {0}", param.vReportAMCBusinessResult.Year));

                        //GetData
                        data = ThongKeTinTuc(param);

                        //Log End
                        logContent = string.Format("{0}\n\r{1}", logContent, "End BC thong ke tin tuc");
                        break;
                    }
            }

            // copy template to result
            logContent = string.Format("{0}\n\r{1}", logContent, "Copy template");
            string templateFile = System.IO.Path.Combine(ConfigUtils.TemplateFolder, reportInfo.TemplateName);
            string excelFile = System.IO.Path.Combine(temporaryFolder, reportInfo.TemplateName);
            System.IO.File.Copy(templateFile, excelFile);

            // bind data into excel file
            logContent = string.Format("{0}\n\r{1}", logContent, "Fill du lieu vao excel");

            OpenXmlExcelHelper.ExcelSheetData sheetBaoCao = new OpenXmlExcelHelper.ExcelSheetData() { FixedCells = dicFixCell };
            sheetBaoCao.Table = new OpenXmlExcelHelper.ExcelTableData() { TableName = "TableData", DataSource = data };
            OpenXmlExcelHelper excelHelper = new OpenXmlExcelHelper();
            excelHelper.Transform(excelFile, sheetBaoCao);
        }

        /// <summary>
        /// Báo cáo kết quả kinh doanh
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private DataTable ThongKeTinTuc(ReportingParam param)
        {
            //_biz.Service_GetExportAMCBusinessResult(param);
            //List<vReportAMCBusinessResult> lstItem = param.ListReportAMCBusinessResult;
            var lstItem = new List<string>();
            List<string> lstCol = new List<string>
            {
                "STT",
                "NoiDung",
                "KeHoach_QuyI",
                "KeHoach_QuyII",
                "KeHoach_QuyIII",
                "KeHoach_QuyIV",
                "KeHoach_Nam",
                "ThucHien_Thang1",
                "ThucHien_Thang2",
                "ThucHien_Thang3",
            };

            return ExcelExportHelper.ConvertObjectsToDataTable(lstItem, lstCol);
        }
    }
}