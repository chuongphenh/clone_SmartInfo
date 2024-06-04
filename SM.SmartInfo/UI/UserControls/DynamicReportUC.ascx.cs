using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Core.Utilities;
using SoftMart.Kernel.Exceptions;
using System.Web.UI;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class DynamicReportUC : BaseUserControl
    {
        #region Public properties

        public string ValidationGroup
        {
            get
            {
                return ucDynamicReport.ValidationGroup;
            }
            set
            {
                ucDynamicReport.ValidationGroup = value;
            }
        }

        public bool AutoLoadData
        {
            get
            {
                return ucDynamicReport.AutoLoadData;
            }
            set
            {
                ucDynamicReport.AutoLoadData = value;
            }
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Build a dynamic report with list of fixed condition
        /// </summary>
        /// <param name="type">Type of report</param>
        /// <param name="conditions">Fixed condition provided by special business. Key = ColumnName, Value = value to compare</param>
        public void BuildReportForm(string type, List<KeyValuePair<string, object>> conditions)
        {
            if (type == null)
            {
                ucDynamicReport.AutoLoadData = false;
                return;
            }

            if (!SMX.DynamicReport.dicReports.ContainsKey(type))
                throw new SMXException(Messages.LinkNotExisted);

            string xmlFile = SMX.DynamicReport.dicReports[type].ConfigFileName;
            string xmlConfigFile = Path.Combine(ConfigUtils.DynamicReportFolder, xmlFile);
            if (!File.Exists(xmlConfigFile))
                throw new SMXException("File cấu hình báo cáo không tồn tại: " + xmlConfigFile);

            List<SqlParameter> lstParam = new List<SqlParameter>();
            if (conditions != null)
            {
                foreach (KeyValuePair<string, object> item in conditions)
                    lstParam.Add(new SqlParameter("@" + item.Key, item.Value));
            }

            ucDynamicReport.XmlConfigFile = xmlConfigFile;
            var reportInfo = SerializeHelper.DeserializeXmlFile<SoftMart.Service.Reporting.SharedComponent.Entities.ReportInfo>(xmlConfigFile);
            if (!string.IsNullOrWhiteSpace(reportInfo.PermissionTokenKey))
            {
                string permissionTokenValue = string.Empty;
                string fkViewPermission = reportInfo.ForgeinKeyWithViewPermission;
                if (!string.IsNullOrWhiteSpace(fkViewPermission))
                {
                    var viewDataInfo = GetAccessMortgageAssetManagementViewInfo(fkViewPermission);
                    permissionTokenValue = viewDataInfo.TemporaryViewQuery;
                    foreach (var item in viewDataInfo.Params)
                    {
                        DynamicReportCondition enCondition = new DynamicReportCondition();
                        enCondition.ParamName = item.Name;
                        enCondition.ParamValue = item.Value;
                        lstParam.Add(new SqlParameter(item.Name, item.Value));
                    }
                }

                if (!string.IsNullOrEmpty(permissionTokenValue.Trim()))
                    ucDynamicReport.PermissionTokenValue = "and " + permissionTokenValue;
            }
            ucDynamicReport.BuildReportForm(lstParam);
        }

        public void BuildReportForm(string type, List<KeyValuePair<string, object>> conditions, bool autoLoadData)
        {
            if (type == null)
            {
                ucDynamicReport.AutoLoadData = false;
                return;
            }

            if (!SMX.DynamicReport.dicReports.ContainsKey(type))
                throw new SMXException(Messages.LinkNotExisted);

            string xmlFile = SMX.DynamicReport.dicReports[type].ConfigFileName;
            string xmlConfigFile = Path.Combine(ConfigUtils.DynamicReportFolder, xmlFile);
            if (!File.Exists(xmlConfigFile))
                throw new SMXException("File cấu hình báo cáo không tồn tại: " + xmlConfigFile);

            ucDynamicReport.AutoLoadData = autoLoadData;
            List<SqlParameter> lstParam = new List<SqlParameter>();
            if (conditions != null)
            {
                foreach (KeyValuePair<string, object> item in conditions)
                    lstParam.Add(new SqlParameter("@" + item.Key, item.Value));
            }

            ucDynamicReport.XmlConfigFile = xmlConfigFile;
            var reportInfo = SerializeHelper.DeserializeXmlFile<SoftMart.Service.Reporting.SharedComponent.Entities.ReportInfo>(xmlConfigFile);
            if (!string.IsNullOrWhiteSpace(reportInfo.PermissionTokenKey))
            {
                string permissionTokenValue = string.Empty;
                string fkViewPermission = reportInfo.ForgeinKeyWithViewPermission;
                if (!string.IsNullOrWhiteSpace(fkViewPermission))
                {
                    var viewDataInfo = GetAccessMortgageAssetManagementViewInfo(fkViewPermission);
                    permissionTokenValue = viewDataInfo.TemporaryViewQuery;
                    foreach (var item in viewDataInfo.Params)
                    {
                        DynamicReportCondition enCondition = new DynamicReportCondition();
                        enCondition.ParamName = item.Name;
                        enCondition.ParamValue = item.Value;
                        lstParam.Add(new SqlParameter(item.Name, item.Value));
                    }
                }

                if (!string.IsNullOrEmpty(permissionTokenValue.Trim()))
                    ucDynamicReport.PermissionTokenValue = "and " + permissionTokenValue;
            }
            ucDynamicReport.BuildReportForm(lstParam);
        }

        /// <summary>
        /// Export report to excel file
        /// </summary>
        public void ExportReport(string reportDisplayName)
        {
            UserProfile myProfile = Profiles.MyProfile;

            this.ucDynamicReport.SaveReport(reportDisplayName);
        }

        public Control GetFilterControlByParamName(string paramName)
        {
            return this.ucDynamicReport.GetFilterControlByParamName(paramName);
        }

        #endregion

        #region Export dynamic report

        //private void ExportReport(ReportViewer ucDynamicReport, string reportDisplayName)
        //{
        //    DynamicReportSettingInfo rpInfo = SerializeHelper.DeserializeXmlFile<DynamicReportSettingInfo>(ucDynamicReport.XmlConfigFile);

        //    //Get Data source / SqlCommand
        //    List<DynamicReportDataSource> lstSourceInfo = new List<DynamicReportDataSource>();
        //    foreach (var item in rpInfo.ExportExcel.ExportExcelItems)
        //    {
        //        DynamicReportDataSource source = GetDataSourceInfo(ucDynamicReport, item.DataSource);
        //        lstSourceInfo.Add(source);
        //    }

        //    DynamicReportUserInputInfo userInput = new DynamicReportUserInputInfo();
        //    userInput.DynamicReportDataSources = lstSourceInfo;
        //    userInput.DisplayName = reportDisplayName;
        //    userInput.ConfigFileName = Path.GetFileName(ucDynamicReport.XmlConfigFile);

        //    throw new Exception("Chua implement");
        //    //DebtManagementParam param = new DebtManagementParam(FunctionType.Debt.AddDynamicReport);
        //    //param.ReportParam = new ReportParam();
        //    //param.ReportParam.DynamicReportInfo = userInput;
        //    //MainController.Provider.Execute(param);
        //}

        //private DynamicReportDataSource GetDataSourceInfo(ReportViewer ucDynamicReport, string sourceName)
        //{
        //    string orderStatement = string.Empty;
        //    SqlCommand command = ucDynamicReport.GetSqlCommand(sourceName);

        //    List<DynamicReportCondition> lstCondition = new List<DynamicReportCondition>();
        //    foreach (SqlParameter item in command.Parameters)
        //    {
        //        DynamicReportCondition enCondition = new DynamicReportCondition();
        //        enCondition.ParamName = item.ParameterName;
        //        enCondition.ParamValue = item.Value;

        //        lstCondition.Add(enCondition);
        //    }

        //    DynamicReportDataSource source = new DynamicReportDataSource();
        //    source.Name = sourceName;
        //    source.CommandText = command.CommandText;
        //    source.DynamicReportConditions = lstCondition;

        //    // add check permission
        //    string hashKey = "--$$ViewDataPermission$$";
        //    string commandText = source.CommandText;
        //    if (commandText.Contains(hashKey))
        //    {
        //        string onKey = "enAsset.MortgageAssetID";
        //        var viewDataInfo = GetAccessMortgageAssetManagementViewInfo(onKey);
        //        commandText = commandText.Replace(hashKey, viewDataInfo.TemporaryViewQuery);
        //        source.CommandText = commandText;

        //        foreach (var item in viewDataInfo.Params)
        //        {
        //            DynamicReportCondition enCondition = new DynamicReportCondition();
        //            enCondition.ParamName = item.Name;
        //            enCondition.ParamValue = item.Value;

        //            source.DynamicReportConditions.Add(enCondition);
        //        }
        //    }

        //    return source;
        //}

        private PermissionManager.Shared.ViewDataPermissionInfo GetAccessMortgageAssetManagementViewInfo(string onKey)
        {
            string[] key = onKey.Split('.');

            var param = new PermissionManager.Shared.PermissionParam(PermissionManager.Shared.PermissionType.AccessView);
            param.ViewDataPermission = new PermissionManager.Shared.ViewDataPermissionInfo();
            param.ViewDataPermission.BizTable = key.Length > 0 ? key[0] : null;
            param.ViewDataPermission.BizColumn = key.Length > 1 ? key[1] : null;

            PermissionManager.PermissionController.Provider.Execute(param);

            return param.ViewDataPermission;
        }



        #endregion
    }
}