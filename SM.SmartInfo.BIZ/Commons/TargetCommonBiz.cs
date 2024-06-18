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
    class TargetCommonBiz : BizBase
    {
        private TargetCommonDao _dao = new TargetCommonDao();

        #region Search

        public void SearchShortTarget(CommonParam param)
        {
            param.Targets = _dao.SearchShortTarget(param.TargetName);
        }

        public void SearchUserByName(CommonParam param)
        {
            param.Targets = _dao.SearchTargetByName(param.TargetName);
        }

        public void TargetSelectorSearch(CommonParam param)
        {
            param.Targets = _dao.SearchTargetByName(param.TargetName);
        }

        public void GetShortTargetByID(CommonParam param)
        {
            param.TargetInfo = _dao.GetShortTargetByID(param.TargetId.Value);
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
