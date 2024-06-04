using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.BRE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SM.SmartInfo.PermissionManager
{
    class BasePermission : IPermission
    {
        #region Check Page Permission
        public void CheckPagePermission(PermissionParam param)
        {
            Feature curFeature = null;
            if (param.Feature != null)
                curFeature = Profiles.MyProfile.Features.FirstOrDefault(c => c.FeatureID == param.Feature.FeatureID);
            else
                curFeature = GetCurrentAccessedFeature();

            param.FunctionRights = curFeature == null ? new List<FunctionRight>() :
                                            Profiles.MyProfile.EmployeeRights.Where(c => c.FeatureID == curFeature.FeatureID).ToList();
        }

        public void CheckSpecificPagePermission(PermissionParam param)
        {
            Feature curFeature = param.Feature ?? GetCurrentAccessedFeature();
            if (curFeature == null)
                throw new Exception(Messages.NoPermission);

            param.Feature = curFeature;
            param.FunctionRights = Profiles.MyProfile.EmployeeRights.Where(c => c.FeatureID == curFeature.FeatureID).ToList();
        }

        private Feature GetCurrentAccessedFeature()
        {
            string pageUrl = Helper.GetCurrentPageURL();
            UserProfile profile = Profiles.MyProfile;

            // get all granted right
            List<FunctionRight> lstRight = profile.EmployeeRights;

            // get granted right corresponding with URL
            FunctionRight urlRight = lstRight.FirstOrDefault(c => pageUrl.Equals(c.URL, StringComparison.OrdinalIgnoreCase));
            if (urlRight == null)
                return null;

            return profile.Features.FirstOrDefault(c => c.FeatureID == urlRight.FeatureID);
        }
        #endregion

        #region Check Item Permission
        public void CheckItemPermission(PermissionParam param)
        {
            int? itemID = param.ItemID;
            List<string> functionCodes = param.FunctionCodes;
            List<FunctionRight> lstFunctionRight = param.FunctionRights;

            // scope lai danh sach cac functions can check permission neu co chi dinh
            List<string> lstSpecificFunctionCode = functionCodes.Distinct().ToList();
            if (lstSpecificFunctionCode != null && lstSpecificFunctionCode.Count > 0)
                lstFunctionRight = lstFunctionRight.Where(c => lstSpecificFunctionCode.Exists(f => string.Equals(f, c.FunctionCode, StringComparison.OrdinalIgnoreCase))).ToList();

            // check permission cho tung Function dua vao Rule (neu co dinh nghia rule)
            List<RuleEngineInfo> lstRuleCondition = new List<RuleEngineInfo>();
            foreach (var right in lstFunctionRight)
            {
                if (right.RuleID == null)
                    continue;

                if (lstRuleCondition.Exists(c => c.RuleID == right.RuleID))
                    continue;

                var rule = RuleEngineService.GetRuleConditionByRuleID(right.RuleID.Value);
                RuleEngineInfo ruleConditon = new RuleEngineInfo();
                ruleConditon.CaseID = right.RuleID;
                ruleConditon.Condition = rule.Condition;
                ruleConditon.PrimaryKey = rule.PrimaryKey;
                ruleConditon.RuleID = rule.RuleID;
                ruleConditon.ViewName = rule.ViewName;
                lstRuleCondition.Add(ruleConditon);
            }

            if (lstRuleCondition.Count > 0)
            {
                UserProfile profile = Profiles.MyProfile;
                Dao.RuleEngineDao ruleDao = new Dao.RuleEngineDao();
                List<string> lstFixedBizCode = GetFixedBizCode();
                List<int> lstRoleID = (from c in profile.Roles where c.RoleID != null select c.RoleID.Value).ToList();
                List<int?> lstPassedRuleID = ruleDao.ExecuteMatchingRules(lstRuleCondition, itemID,
                    profile.EmployeeID, profile.ListAllManagingOrganizationID, profile.ListDirectManagingOrganizationID, profile.ListCoordinatorOrganizationID,
                    lstRoleID, lstFixedBizCode, profile.ClientNetworkType, profile.SystemSupporting != null ? profile.SystemSupporting.OrganizationListID : string.Empty);

                lstFunctionRight = (from c in lstFunctionRight where c.RuleID == null || lstPassedRuleID.Contains(c.RuleID) select c).ToList();
            }

            param.FunctionRights = lstFunctionRight;
        }
        #endregion

        #region Check View Data Permission
        public void GetTemporaryViewDataPermission(PermissionParam param)
        {
            // default value
            param.ViewDataPermission.TemporaryViewQuery = string.Empty;
            param.ViewDataPermission.Params = new List<ViewDataPermissionParam>();

            // kiem tra quyen truy cap trang
            CheckPagePermission(param);

            // get right cua function VIEW
            FunctionRight viewDataRight = param.FunctionRights.FirstOrDefault(c => FunctionCode.VIEW.Equals(c.FunctionCode, StringComparison.OrdinalIgnoreCase));

            // neu ko set rule => ko check them
            if (viewDataRight == null || viewDataRight.RuleID == null)
                return;

            var rule = RuleEngineService.GetRuleConditionByRuleID(viewDataRight.RuleID.Value);
            RuleEngineInfo ruleConditon = new RuleEngineInfo();
            ruleConditon.CaseID = viewDataRight.FeatureFunctionID;
            ruleConditon.Condition = rule.Condition;
            ruleConditon.PrimaryKey = rule.PrimaryKey;
            ruleConditon.RuleID = rule.RuleID;
            ruleConditon.ViewName = rule.ViewName;

            UserProfile profile = Profiles.MyProfile;
            List<int> lstRoleID = (from c in profile.Roles where c.RoleID != null select c.RoleID.Value).ToList();
            Dao.RuleEngineDao ruleDao = new Dao.RuleEngineDao();
            List<string> lstFixedBizCode = GetFixedBizCode();
            ruleDao.BuildTemporaryViewDataPermission(param.ViewDataPermission, ruleConditon,
                profile.EmployeeID, profile.ListAllManagingOrganizationID, profile.ListDirectManagingOrganizationID,
                profile.ListCoordinatorOrganizationID, lstRoleID, lstFixedBizCode, profile.ClientNetworkType, profile.SystemSupporting != null ? profile.SystemSupporting.OrganizationListID : string.Empty);
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
        #endregion
    }
}