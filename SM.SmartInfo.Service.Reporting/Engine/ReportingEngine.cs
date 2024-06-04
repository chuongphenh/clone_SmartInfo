using System;
using System.Data;
using System.Linq;
using SpreadsheetGear;
using System.Collections.Generic;

namespace SM.SmartInfo.Service.Reporting.Engine
{
    public class ReportingEngine
    {
        #region Constructors

        private ReportInfo _reportInfo;
        private Dictionary<string, object> _lstParam;
        private Helper _IRenderHelper;

        public string SaveAsFileName
        {
            get
            {
                if (_reportInfo == null)
                    return string.Empty;

                return _reportInfo.SaveAsFileName;
            }
        }

        /// <summary>
        /// ReportingEngine
        /// </summary>
        /// <param name="fileName">Tên file xml cấu hình phiếu in/báo cáo</param>
        public ReportingEngine(string fileName)
        {
            var reportInfo = ReadXmlConfig(fileName);
            Init(reportInfo);
        }

        private void Init(ReportInfo reportInfo)
        {
            _reportInfo = reportInfo;

            _lstParam = new Dictionary<string, object>()
            { };

            _IRenderHelper = new Helper() { Params = _lstParam, };
        }

        public static ReportInfo ReadXmlConfig(string fileName)
        {
            var fileFullPath = System.IO.Path.Combine(Utils.ConfigUtils.DynamicReportFolder, fileName);
            return SoftMart.Core.Utilities.SerializeHelper.DeserializeXmlFile<ReportInfo>(fileFullPath);
        }

        #endregion

        #region Render
        /// <summary>
        /// Lấy dữ liệu và merge vào template
        /// </summary>
        /// <returns></returns>
        public virtual byte[] Render()
        {
            var template = _reportInfo.Templates.First(c => c.IsDefault);
            _IRenderHelper.TemplateFilePath = System.IO.Path.Combine(Utils.ConfigUtils.TemplateFolder, template.FileName);
            var renderHelper = new ExcelRenderHelper(_IRenderHelper, _reportInfo);
            byte[] fileContent = renderHelper.RenderToMemory();

            return fileContent;
        }

        #endregion

        #region Parameters
        /// <summary>
        /// Trường hợp chỉ dùng duy nhất ID bản ghi là đủ => Làm hàm riêng set cho dễ
        /// </summary>
        /// <param name="itemID"></param>
        public void SetParameters_ItemID(object itemID)
        {
            _lstParam.Add("ID_BanGhi", itemID);
        }

        public void SetParameters(string paramName, object value)
        {
            _lstParam.Add(paramName, value);
        }

        public void SetParameters(Dictionary<string, object> lstParam)
        {
            foreach (var item in lstParam)
            {
                _lstParam.Add(item.Key, item.Value);
            }
        }
        #endregion

        #region IHelper implementation
        public class Helper : ExcelRenderHelper.IHelper
        {
            public Dictionary<string, object> Params { get; set; }

            #region IHelper
            public string TemplateFilePath { get; set; }

            public string NumberToText(int number)
            {
                throw new NotImplementedException();
            }

            public DataTable GetData(string cmdText)
            {
                if (string.IsNullOrWhiteSpace(cmdText))
                    return new DataTable();

                var sqlCmd = new System.Data.SqlClient.SqlCommand(cmdText);
                foreach (var item in Params)
                {
                    sqlCmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                using (var dt = new DataContext())
                {
                    var dataTable = dt.ExecuteDataTable(sqlCmd);
                    return dataTable;
                }
            }

            public object InvokeCSharpMethod(string methodName, IWorksheet workSheet)
            {
                throw new NotImplementedException();
            }

            public byte[] GenBarcode(string value, string barcodeType, int width, int height)
            {
                throw new NotImplementedException();
            }

            public byte[] GenQRcode(string value, int height)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        #endregion
    }
}