using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.PermissionManager.Shared;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class RuleEngineDao
    {
        public List<int?> ExecuteMatchingRules(List<RuleEngineInfo> lstCondition, int? primaryVal,
            int? empID, List<int> lstManagingOrgID, List<int> lstOwnerOrgID, List<int> lstCoordinatorOrgID,
            List<int> lstRoleID, List<string> lstFixedBizCode, int clientNetworkType, string listSupportOrganization)
        {
            /*
                select (case 
			                when ( condition of Rule[1]) then CaseID1
			                when ( condition of Rule[n]) then CaseIDn
			                else null
		                end) as RuleID from 
                [ViewName] where [PrimaryKey] = @PrimaryKey
             */
            StringBuilder query = new StringBuilder();
            string strUnion = string.Empty;
            foreach (var condition in lstCondition)
            {
                if (!string.IsNullOrWhiteSpace(strUnion))
                    query.AppendLine(strUnion);
                else
                    strUnion = "union all ";

                query.AppendFormat("SELECT (CASE WHEN ({0}) THEN {1} ELSE NULL END) as CaseID ", condition.Condition, condition.CaseID).AppendLine();
                query.AppendFormat("FROM [{0}] WHERE [{1}] = @PrimaryKey ", condition.ViewName, condition.PrimaryKey);
            }

            SqlCommand sqlCmd = new SqlCommand(query.ToString());
            SqlParameter[] arrParam = GetPermissionParameter(primaryVal, empID, lstManagingOrgID, lstOwnerOrgID, lstCoordinatorOrgID,
                lstRoleID, lstFixedBizCode, clientNetworkType, listSupportOrganization);
            sqlCmd.Parameters.AddRange(arrParam);

            Log(query.ToString(), arrParam);
            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<int?>(sqlCmd);
            }
        }

        public void BuildTemporaryViewDataPermission(ViewDataPermissionInfo info, RuleEngineInfo condition,
            int? empID, List<int> lstManagingOrgID, List<int> lstOwnerOrgID, List<int> lstCoordinatorOrgID,
            List<int> lstRoleID, List<string> lstFixedBizCode, int clientNetworkType, string listSupportOrganization)
        {
            // (select [PrimaryKey] From [ViewName] where [Condition]) alias
            info.TemporaryViewQuery = string.Format("(exists (SELECT [{0}] FROM [{1}] WHERE ({2}) AND [{0}] = [{3}].[{4}]))",
                condition.PrimaryKey, condition.ViewName, condition.Condition, info.BizTable, info.BizColumn);

            SqlParameter[] arrParam = GetPermissionParameter(null, empID, lstManagingOrgID, lstOwnerOrgID, lstCoordinatorOrgID,
                lstRoleID, lstFixedBizCode, clientNetworkType, listSupportOrganization);

            info.Params = new List<ViewDataPermissionParam>();
            foreach (SqlParameter param in arrParam)
                info.AddParam(param.ParameterName, param.Value);

            Log(info.TemporaryViewQuery, arrParam);
        }

        private SqlParameter[] GetPermissionParameter(int? primaryVal,
            int? empID, List<int> lstManagingOrgID, List<int> lstOwnerOrgID, List<int> lstCoordinatorOrgID,
            List<int> lstRoleID, List<string> lstFixedBizCode, int clientNetworkType, string listSupportOrganization,
            List<ViewDataPermissionParam> lstCustomParams = null)
        {
            int standardParamCount = 9;
            int paramCount = standardParamCount + (lstCustomParams == null ? 0 : lstCustomParams.Count);
            SqlParameter[] arrParam = new SqlParameter[paramCount];

            // primarykey
            int index = 0;
            arrParam[index++] = new SqlParameter("@PrimaryKey", primaryVal);

            // chuyen vien
            arrParam[index++] = new SqlParameter("@aut_EmpID", empID);

            // phong quan ly (bao gom ca con/chau)
            if (lstManagingOrgID == null || lstManagingOrgID.Count == 0)
                lstManagingOrgID = new List<int>() { -1 };
            string strArrayManagingOrgID = "," + string.Join(",", lstManagingOrgID) + ",";
            arrParam[index++] = new SqlParameter("@aut_ListOrg_Managing", strArrayManagingOrgID);

            // phong quan ly truc tiep
            if (lstOwnerOrgID == null || lstOwnerOrgID.Count == 0)
                lstOwnerOrgID = new List<int>() { -1 };
            string strArrayOwnerOrgID = "," + string.Join(",", lstOwnerOrgID) + ",";
            arrParam[index++] = new SqlParameter("@aut_ListOrg_Owner", strArrayOwnerOrgID);

            // phong duoc uy quyen dieu phoi
            if (lstCoordinatorOrgID == null || lstCoordinatorOrgID.Count == 0)
                lstCoordinatorOrgID = new List<int>() { -1 };
            string strArrayCoordinatorOrgID = "," + string.Join(",", lstCoordinatorOrgID) + ",";
            arrParam[index++] = new SqlParameter("@aut_ListOrg_Coordinator", strArrayCoordinatorOrgID);

            // role
            if (lstRoleID == null || lstRoleID.Count == 0)
                lstRoleID = new List<int>() { -1 };
            string strArrRoleID = "," + string.Join(",", lstRoleID) + ",";
            arrParam[index++] = new SqlParameter("@aut_ListRole", strArrRoleID);

            // fix biz
            if (lstFixedBizCode == null || lstFixedBizCode.Count == 0)
                lstFixedBizCode = new List<string>() { "" };
            string strArrayFixedBiz = "," + string.Join(",", lstFixedBizCode) + ",";
            arrParam[index++] = new SqlParameter("@aut_ListFixRole", strArrayFixedBiz);

            // client NetworkType
            arrParam[index++] = new SqlParameter("@aut_ClientNetworkType", clientNetworkType);

            //Supporting
            arrParam[index++] = new SqlParameter("@aut_Supporting", listSupportOrganization);

            for (; index < paramCount; index++)
            {
                ViewDataPermissionParam customParam = lstCustomParams[index - standardParamCount];
                arrParam[index] = new SqlParameter(customParam.Name, customParam.Value);
            }

            return arrParam;
        }

        private void Log(string query, System.Collections.Generic.IEnumerable<SqlParameter> lstParam)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("<PermissionItem Time=\"{0}\">", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
            builder.AppendLine("<Query>");
            foreach (SqlParameter param in lstParam)
            {
                builder.AppendLine(string.Format("declare {0} {1} = '{2}'", param.ParameterName, param.SqlDbType, param.Value));
            }
            builder.AppendLine(query);
            builder.AppendLine("</Query>");
            builder.Append("</PermissionItem>");

            Utils.LogManager.PermissionLogger.LogDebug(builder.ToString());
        }
    }
}
