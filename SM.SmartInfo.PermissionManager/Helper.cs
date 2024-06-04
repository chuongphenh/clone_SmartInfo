using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.BRE;
using System.Collections.Generic;
using System.Linq;

namespace SM.SmartInfo.PermissionManager
{
    public class Helper
    {
        public void GetPermissionView(string bizTable, string bizColumn, int? specificFeatureID,
            out string permissionView, out List<System.Data.SqlClient.SqlParameter> lstParam)
        {
            var param = new PermissionParam(PermissionType.AccessView);
            param.ViewDataPermission = new ViewDataPermissionInfo();
            param.ViewDataPermission.BizTable = bizTable;
            param.ViewDataPermission.BizColumn = bizColumn;
            if (specificFeatureID.HasValue)
                param.Feature = new Feature() { FeatureID = specificFeatureID };
            PermissionController.Provider.Execute(param);

            var viewDataInfo = param.ViewDataPermission;

            permissionView = viewDataInfo.TemporaryViewQuery;

            lstParam = new List<System.Data.SqlClient.SqlParameter>();
            foreach (var item in viewDataInfo.Params)
            {
                lstParam.Add(new System.Data.SqlClient.SqlParameter(item.Name, item.Value));
            }
        }

        public void GetPermissionView(string bizTable, string bizColumn,
            out string permissionView, out List<System.Data.SqlClient.SqlParameter> lstParam)
        {
            var param = new PermissionParam(PermissionType.AccessView);
            param.ViewDataPermission = new ViewDataPermissionInfo();
            param.ViewDataPermission.BizTable = bizTable;
            param.ViewDataPermission.BizColumn = bizColumn;
            PermissionController.Provider.Execute(param);

            var viewDataInfo = param.ViewDataPermission;

            permissionView = viewDataInfo.TemporaryViewQuery;

            lstParam = new List<System.Data.SqlClient.SqlParameter>();
            foreach (var item in viewDataInfo.Params)
            {
                lstParam.Add(new System.Data.SqlClient.SqlParameter(item.Name, item.Value));
            }
        }

        public void GetPermissionView(ViewDataPermissionInfo info, string ruleCode,
            out string permissionView, out List<System.Data.SqlClient.SqlParameter> lstParam)
        {
            var rule = RuleEngineService.GetRuleConditionByCode(ruleCode);
            RuleEngineInfo ruleConditon = new RuleEngineInfo();
            ruleConditon.Condition = rule.Condition;
            ruleConditon.PrimaryKey = rule.PrimaryKey;
            ruleConditon.RuleID = rule.RuleID;
            ruleConditon.ViewName = rule.ViewName;

            UserProfile profile = Profiles.MyProfile;
            List<int> lstRoleID = (from c in profile.Roles where c.RoleID != null select c.RoleID.Value).ToList();
            Dao.RuleEngineDao ruleDao = new Dao.RuleEngineDao();
            List<string> lstFixedBizCode = GetFixedBizCode();
            ruleDao.BuildTemporaryViewDataPermission(info, ruleConditon,
                profile.EmployeeID, profile.ListAllManagingOrganizationID, profile.ListDirectManagingOrganizationID,
                profile.ListCoordinatorOrganizationID, lstRoleID, lstFixedBizCode, profile.ClientNetworkType, profile.SystemSupporting != null ? profile.SystemSupporting.OrganizationListID : string.Empty);

            permissionView = info.TemporaryViewQuery;
            lstParam = new List<System.Data.SqlClient.SqlParameter>();
            foreach (var item in info.Params)
            {
                lstParam.Add(new System.Data.SqlClient.SqlParameter(item.Name, item.Value));
            }
        }

        private List<string> GetFixedBizCode()
        {
            // Roles
            List<Role> roles = Profiles.MyProfile.Roles;

            // fixed permission
            Dao.FixBusinessPermissionDao dao = new Dao.FixBusinessPermissionDao();
            List<int?> roleIDs = roles.Select(c => c.RoleID).ToList();
            List<FixedBusinessPermission> fixedBusinessPermissions = dao.GetFixedBusinessPermissions(roleIDs);

            return fixedBusinessPermissions.Select(c => c.Code).ToList();
        }

        public static string GetCurrentPageURL()
        {
            try
            {
                string pageUrl = System.Web.HttpContext.Current.Request.Url.ToString();
                string pattern = @"/ui/.*\.aspx(\?SPTID=\d+)?(\?type=\w+)?";
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Match match = reg.Match(pageUrl);

                return match != null ? match.Value : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}