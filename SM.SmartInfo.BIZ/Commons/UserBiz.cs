using SM.SmartInfo.DAO.Commons;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.CacheManager;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using System.Linq;

namespace SM.SmartInfo.BIZ.Commons
{
    class UserBiz : BizBase
    {
        private UserDao _dao = new UserDao();

        #region Search

        public void SearchShortEmployee(CommonParam param)
        {
            param.Employees = _dao.SearchShortEmployee(param.EmployeeName);
        }

        public void SearchUserByName(CommonParam param)
        {
            param.Employees = _dao.SearchUserByName(param.EmployeeName);
        }

        public void EmployeeSelectorSearch(CommonParam param)
        {
            switch (param.EmployeeSelectorMode)
            {
                case EmployeeSelectorMode.All:
                    {
                        param.Employees = _dao.SearchUserByName(param.EmployeeName);
                        break;
                    }
                case EmployeeSelectorMode.OrganizationID:
                    {
                        List<int> lstOrg = new List<int>() { param.OrganizationID.Value };
                        param.Employees = _dao.SearchUserInOnlyOrganizationID(param.EmployeeName, lstOrg);
                        break;
                    }
                case EmployeeSelectorMode.CurrentUserOrgAndBelow:
                    {
                        List<int> lstOrg = Profiles.MyProfile.ListAllManagingOrganizationID;
                        param.Employees = _dao.SearchUserInOnlyOrganizationID(param.EmployeeName, lstOrg);
                        break;
                    }
                case EmployeeSelectorMode.OrganizationType:
                    {
                        param.Employees = _dao.SearchUserInOrganizationByType(param.OrganizationType, param.EmployeeName);
                        break;
                    }
                case EmployeeSelectorMode.RoleIDs:
                    {
                        param.Employees = _dao.SearchUserByRoleIDs(param.RoleIDs);
                        break;
                    }
                default:
                    throw new SMXException("Chua ho tro Mode: " + param.EmployeeSelectorMode.ToString());
            }
        }

        public void GetShortUserByID(CommonParam param)
        {
            param.EmployeeInfo = _dao.GetShortUserByID(param.EmployeeId.Value);
        }

        public void GetListEmployeeByOrganizationID(CommonParam param)
        {
            List<Employee> lstEmp = _dao.SearchListUserByOrganizationID(param.OrganizationID);
            List<Employee> lstMgr = _dao.SearchListManagerByOrganizationID(param.OrganizationID);

            foreach (Employee mgr in lstMgr)
            {
                if (!lstEmp.Exists(c => c.EmployeeID == mgr.EmployeeID))
                    lstEmp.Add(mgr);
            }

            param.Employees = lstEmp.OrderBy(c => c.Name).ToList();
        }

        #endregion

        public void GetListEmployeeByListStringOrganizationID(CommonParam param)
        {
            List<Employee> lstEmp = _dao.SearchListUserByListStringOrganizationID(param.ListStringOrganizationID);
            List<Employee> lstMgr = _dao.SearchListManagerByListStringOrganizationID(param.ListStringOrganizationID);

            foreach (Employee mgr in lstMgr)
            {
                if (!lstEmp.Exists(c => c.EmployeeID == mgr.EmployeeID))
                    lstEmp.Add(mgr);
            }

            param.Employees = lstEmp.OrderBy(c => c.Name).ToList();
        }
    }
}
