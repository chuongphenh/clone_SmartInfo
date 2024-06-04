using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class OrganizationDAO
    {
        public Organization GetOrganizationByID(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<Organization>(id);
            }
        }

        public List<int> GetListManagingOrganizationID(int empID)
        {
            string query = @"select
	                            distinct vOrg.OrganizationID
                            from
	                            Organization org
	                            inner join OrganizationManager orgMgr on orgMgr.OrganizationID = org.OrganizationID
	                            inner join vOrganizationInGroup vOrg on vOrg.ParentID = org.OrganizationID
                            where
	                            org.Deleted = {0} and orgMgr.EmployeeID = {1}";
            query = string.Format(query, SMX.smx_IsNotDeleted, empID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<int>(query);
            }
        }

        public List<int> GetListOwningOrganizationID(int empID)
        {
            string query = @"select
	                            distinct org.OrganizationID
                            from
	                            Organization org
	                            inner join OrganizationManager orgMgr on orgMgr.OrganizationID = org.OrganizationID
                            where
	                            org.Deleted = {0} and orgMgr.EmployeeID = {1}";
            query = string.Format(query, SMX.smx_IsNotDeleted, empID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<int>(query);
            }
        }

        public int? GetOrganizationOfEmployee(int? empID)
        {
            using (DataContext context = new DataContext())
            {
                OrganizationEmployee item = context.SelectItemByColumnName<OrganizationEmployee>(OrganizationEmployee.C_EmployeeID, empID).FirstOrDefault();
                return item == null ? (int?)null : item.OrganizationID;
            }
        }
    }
}