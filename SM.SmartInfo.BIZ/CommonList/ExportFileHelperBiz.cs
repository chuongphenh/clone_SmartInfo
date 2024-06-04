using System.Data;
using System.Collections.Generic;
using SM.SmartInfo.DAO.Administration;
using static SM.SmartInfo.BIZ.Commons.ExportFilesBiz;

namespace SM.SmartInfo.BIZ.CommonList
{
    public class ExportFileHelperBiz : IExportFile
    {
        public byte[] GetExportWordByXslt(string mapFile, Dictionary<string, object> dicParam)
        {
            List<System.Data.SqlClient.SqlParameter> lstQueryParam = new List<System.Data.SqlClient.SqlParameter>();
            foreach (KeyValuePair<string, object> param in dicParam)
            {
                lstQueryParam.Add(new System.Data.SqlClient.SqlParameter(param.Key, param.Value));
            }

            // transform data (fill data to result file)
            //bool isDeleteTemporaryFile = true;
            //string keepExportPDFTemporaryValue = Utils.ConfigUtils.GetConfig("KeepExportPDFTemporary");
            //if (keepExportPDFTemporaryValue == "1" || keepExportPDFTemporaryValue == "true")
            //    isDeleteTemporaryFile = false;
            //byte[] fileContent = SoftMart.Service.Reporting.ReportingApi.TranformToWord(mapFilePath, lstQueryParam, isDeleteTemporaryFile);
            byte[] fileContent = SoftMart.Service.Reporting.ReportingApi.TranformToWord(mapFile, lstQueryParam);

            return fileContent;
        }

        public DataTable GetQueryData(string query, Dictionary<string, object> dicQueryParam)
        {
            SystemParameterDao dao = new SystemParameterDao();
            return dao.GetQueryData(query, dicQueryParam);
        }
    }
}