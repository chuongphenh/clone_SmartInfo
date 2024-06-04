using System.Collections.Generic;
using System.Linq;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using System.Data.SqlClient;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.DAO.Commons
{
    public class OrganizationDAO : BaseDao
    {
        public List<Organization> GetOrganizationByType(int type)
        {
            string cmdText = string.Format(@"SELECT DISTINCT parent.* 
                                            FROM Organization AS org
                                            INNER JOIN vOrganizationInGroup AS tree on tree.OrganizationID = org.OrganizationID
                                            INNER JOIN Organization AS parent ON tree.ParentID = parent.OrganizationID and parent.Deleted=0
                                            WHERE org.Deleted=0 
                                                AND (org.Type & {0} = org.Type)", type);
            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<Organization>(cmdText);
                return res;
            }
        }

        public List<Organization> GetOrganizationByListDirectManagingOrganizationID(List<int> lstDirectManagingOrganizationID)
        {
            string cmdText = string.Format(@"SELECT DISTINCT parent.* 
                                            FROM Organization AS org
                                                INNER JOIN vOrganizationInGroup AS tree on tree.OrganizationID = org.OrganizationID
                                                INNER JOIN Organization AS parent ON tree.ParentID = parent.OrganizationID and parent.Deleted=0
                                            WHERE org.Deleted=0 AND org.OrganizationID IN ({0})", base.BuildInCondition(lstDirectManagingOrganizationID));
            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<Organization>(cmdText);
                return res;
            }
        }

        public Organization GetOrganizationByID(int? id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<Organization>(id);
            }
        }

        public int? GetZoneIDByOrganizationID(int organizationID)
        {
            Organization org = null;
            using (var dataContext = new DataContext())
            {
                org = dataContext.SelectFieldsByColumnName<Organization>(new string[] { Organization.C_ZoneID },
                                                                        Organization.C_OrganizationID, organizationID).FirstOrDefault();
            }
            return org == null ? null : org.ZoneID;
        }
        public List<Organization> GetListOrganizationByName(string _name)
        {
            string query = @"
                            SELECT org.OrganizationID, org.[Name], org.OfficeID, org.ParentID, vOrg.BreadCrumb 
                            FROM Organization org 
                            JOIN vOrganization vOrg on org.OrganizationID = vOrg.OrganizationID 
                            WHERE org.Deleted = @NotDeleted AND LOWER(vOrg.[Name]) = LOWER(@name)";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            sqlCmd.Parameters.AddWithValue("@name", _name);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Organization>(sqlCmd);
            }
        }
        public List<OrganizationEmployee> GetOrganizationOfEmployee(int? empID)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<OrganizationEmployee>(OrganizationEmployee.C_EmployeeID, empID);
            }
        }

        public List<OrganizationManager> GetOrganizationOfManager(int? empID)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<OrganizationManager>(OrganizationManager.C_EmployeeID, empID);
            }
        }

        public List<Organization> GetListOrganizationByProvinceId(int provinceId)
        {
            ConditionList conditions = new ConditionList(){
                {Organization.C_Province, provinceId}
            };
            using (var context = new DataContext())
            {
                return context.SelectItemByColumnName<Organization>(conditions);
            }
        }

        public List<Employee> GetListEmployeeByZoneID(int? zoneID, List<int> lstOrganizationType)
        {
            string query = @"select distinct
	                            emp.EmployeeID,
	                            emp.Name 
                            from 
	                            OrganizationEmployee orgEmp
	                            left join Organization org ON org.OrganizationID = orgEmp.OrganizationID
	                            left join adm_Employee emp ON emp.EmployeeID = orgEmp.EmployeeID and emp.Status = @StatusActive
                            where 
                                orgEmp.Deleted = @NotDeleted
                                and org.Deleted = @NotDeleted and org.ZoneID = @ZoneID
	                            and org.Type in ({0})";

            query = string.Format(query, BuildInCondition(lstOrganizationType));

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@ZoneID", zoneID);
            sqlCmd.Parameters.AddWithValue("@StatusActive", SMX.Status.Active);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(sqlCmd);
            }
        }

        public List<Employee> GetDataListValuationEmployee()
        {
            string query = @"select distinct
	                            emp.EmployeeID,
	                            emp.Name 
                            from 
	                            OrganizationEmployee orgEmp
	                            left join Organization org ON org.OrganizationID = orgEmp.OrganizationID
	                            left join adm_Employee emp ON emp.EmployeeID = orgEmp.EmployeeID and emp.Status = 1
                            where 
                                orgEmp.Deleted = 0
	                            and org.Type = 4";

            SqlCommand sqlCmd = new SqlCommand(query);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(sqlCmd);
            }
        }

        public List<Employee> GetDataListApprovedEmployee()
        {
            string query = @"select distinct
	                            emp.EmployeeID,
	                            emp.Name 
                            from 
	                            OrganizationManager orgEmp
	                            left join adm_Employee emp ON emp.EmployeeID = orgEmp.EmployeeID and emp.Status = 1
                            where 
                                orgEmp.Deleted = 0";

            SqlCommand sqlCmd = new SqlCommand(query);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(sqlCmd);
            }
        }

        public string GetOrganizationBreadcrumbByID(int? orgID)
        {
            string query = "Select BreadCrumb From vOrganization Where OrganizationID = @OrganizationID";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@OrganizationID", orgID);
            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<string>(sqlCmd).FirstOrDefault();
            }
        }

        public List<Organization> GetListOrganizationByZoneId(int? ZoneId)
        {
            ConditionList conditions = new ConditionList(){
                {Organization.C_ZoneID, ZoneId}
            };
            using (var context = new DataContext())
            {
                return context.SelectItemByColumnName<Organization>(conditions);
            }
        }

        public List<Organization> GetOrganizationByTypeAndCommitteeCode(List<int> lstType, string committeCode)
        {
            if (lstType == null || lstType.Count == 0)
                lstType = new List<int>() { -1 };

            string cmdText = @" SELECT 
	                                org.* 
                                FROM 
	                                Organization org 
	                                INNER JOIN Committee cm ON cm.CommitteeID = org.CommitteeID
                                WHERE 
	                                org.Deleted = 0 
	                                AND cm.Deleted = 0
	                                AND org.Type in ({0})
	                                AND cm.Code = @CommitteCode	";
            cmdText = string.Format(cmdText, string.Join(", ", lstType));
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@CommitteCode", committeCode);
            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Organization>(sqlCmd);
            }
        }

        public List<Organization> GetOrganizationByParentID(int parentID)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<Organization>(Organization.C_ParentID, parentID);
            }
        }

        public List<Organization> SearchOrganization(string keyWord, int? type)
        {
            string sql = @"select top 20 OrganizationID, Name + ' - ' + Code as Name
                           from Organization
                           where Deleted = 0
                               and (@KeyWord is null OR Name like @KeyWord OR Code like @KeyWord)
                               and (@Type is null OR Type & @Type = Type)
                           order by Name";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@KeyWord", BuildLikeFilter(keyWord));
            command.Parameters.AddWithValue("@Type", type);
            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<Organization>(command);
                return res;
            }
        }

        public List<Employee> GetListEmployeeByOrganizationID(int orgID)
        {
            string query = @"select
	                            emp.*
                            from
	                            OrganizationEmployee orgEmp
	                            left join adm_Employee emp on emp.EmployeeID = orgEmp.EmployeeID
                            where
	                            orgEmp.Deleted = {0} 
	                            and orgEmp.OrganizationID = {1}
	                            and emp.Deleted = {0}";
            query = string.Format(query, SMX.smx_IsNotDeleted, orgID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Employee>(query);
            }
        }

        public List<Employee> GetListManagerByOrganizationID(int orgID)
        {
            string query = @"select
	                            emp.*
                            from
	                            OrganizationManager orgEmp
	                            left join adm_Employee emp on emp.EmployeeID = orgEmp.EmployeeID
                            where
	                            orgEmp.Deleted = {0} 
	                            and orgEmp.OrganizationID = {1}
	                            and emp.Deleted = {0}";
            query = string.Format(query, SMX.smx_IsNotDeleted, orgID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Employee>(query);
            }
        }

        public Organization GetBranch_ByOrgID(int orgID)
        {
            string cmdText = @"SELECT 
	* 
FROM vOrganizationInGroup v 
	LEFT JOIN Organization org on org.Deleted = 0 AND org.OrganizationID = v.ParentID
	INNER JOIN Committee c on c.Code = @TrungTam AND c.CommitteeID = org.CommitteeID
WHERE v.OrganizationID = @OrganizationID";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@TrungTam", SMX.CommitteeCode.CKS_TrungTam);
            cmd.Parameters.AddWithValue("@OrganizationID", orgID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Organization>(cmd).FirstOrDefault();
            }
        }
    }
}
