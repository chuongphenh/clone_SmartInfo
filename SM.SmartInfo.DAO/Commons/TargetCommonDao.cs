using System.Linq;
using System.Collections.Generic;

using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SM.SmartInfo.SharedComponent.Params;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.DAO.Commons
{
    public class TargetCommonDao : BaseDao
    {
        #region Get

        public Employee GetActiveEmployee(string userName)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<Employee>(new ConditionList
                                                {
                                                    {Employee.C_Username, userName},
                                                    {Employee.C_Status, SMX.Status.Active}
                                                }).FirstOrDefault();
            }
        }

        public Employee GetActiveEmployee(int employeeID)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<Employee>(new ConditionList
                                                {
                                                    {Employee.C_EmployeeID, employeeID},
                                                    {Employee.C_Status, SMX.Status.Active}
                                                }).FirstOrDefault();
            }
        }

        public TargetInfo GetShortTargetByID(int targetID)
        {
            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.SelectFieldsByColumnName<TargetInfo>(new string[] { Target.C_Name, Target.C_TargetID, Target.C_TargetCode, Target.C_Description, Target.C_TargetType },
                                                        new ConditionList()
                                                        {
                                                            {Target.C_TargetID, targetID},
                                                            //{Employee.C_Status, SMX.Status.Active},
                                                        }).FirstOrDefault();

                return res;
            }
        }

        #endregion

        #region Search

        public List<Target> SearchShortTarget(string keyWord)
        {
            string sql = @"select top 20 TargetID, Name, TargetCode
                           from Target
                           where Deleted = 0
                               and (Name like @KeyWord OR TargetCode like @KeyWord)
                           order by Name";

            SqlCommand command = new SqlCommand(sql);
            //command.Parameters.AddWithValue("@Status", SMX.Status.Active);
            command.Parameters.AddWithValue("@KeyWord", BuildLikeFilter(keyWord));

            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<Target>(command);
                return res;
            }
        }

        public List<Target> SearchTargetByName(string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                return new List<Target>();
            }

            string sql = @"select *
                           from Target
                           where Deleted = 0
                               and (Name like @Name OR TargetCode like @Name)";

            using (SqlCommand command = new SqlCommand(sql))
            {
                command.Parameters.AddWithValue("@Name", BuildLikeFilter(searchValue));

                using (var dataContext = new DataContext())
                {
                    var res = dataContext.ExecuteSelect<Target>(command);
                    return res;
                }
            }
        }

        public List<Employee> SearchUserInOnlyOrganizationID(string employeeName, List<int> lstOrganizationID)
        {
            string sql = string.Format(
                              @"SELECT enEmployee.*
                                FROM adm_Employee AS enEmployee
									INNER JOIN OrganizationEmployee AS enOrgEmployee ON enEmployee.EmployeeID = enOrgEmployee.EmployeeID
                                    INNER JOIN Organization AS enOrg ON enOrgEmployee.OrganizationID = enOrg.OrganizationID and enOrg.Deleted = 0
                                WHERE enEmployee.Deleted = 0
                                    AND enOrg.OrganizationID in ({0})
                                    AND enEmployee.Status = @Status
                                    AND (@Name IS NULL OR enEmployee.Name LIKE @Name OR enEmployee.Username LIKE @Name)",
                                base.BuildInCondition(lstOrganizationID));

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@Status", SMX.Status.Active);
            command.Parameters.AddWithValue("@Name", BuildLikeFilter(employeeName));

            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<Employee>(command);
                return res;
            }
        }

        public List<Employee> SearchUserInOrganizationByType(int? type, string employeeName = "")
        {
            string sql = @" select enEmployee.*

                            from OrganizationEmployee as enOrganizationEmployee 
      
                                  inner join adm_Employee as enEmployee 
                                  on enOrganizationEmployee.EmployeeID = enEmployee.EmployeeID
                                  and enEmployee.Deleted = 0

	                              inner join Organization as enOrganization
	                              on enOrganizationEmployee.OrganizationID = enOrganization.OrganizationID
	                              and enOrganization.Deleted = 0
	                              and enOrganization.Type = @type
	  
                            where enOrganizationEmployee.Deleted = 0";
            if (employeeName != "")
            {
                sql += " AND (enEmployee.Name like @Name OR enEmployee.Username like @Name)";
            }
            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@Name", base.BuildLikeFilter(employeeName));

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(command);
            }
        }

        #endregion

        public List<Employee> SearchUserByRoleIDs(List<int> list)
        {
            string sql = @"select enEmployee.EmployeeID, enEmployee.Username, enEmployee.Name
                            from adm_EmployeeRole as enEmployeeRole

                            inner join adm_Employee as enEmployee
                            on enEmployee.EmployeeID = enEmployeeRole.EmployeeID
                            and enEmployee.Deleted = 0
                            and enEmployee.Status = @Active

                            where RoleID in ({0})";

            sql = string.Format(sql, base.BuildInCondition(list));
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Active", SMX.Status.Active);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }


        public List<Employee> SearchListUserByOrganizationID(int? organizationID)
        {
            string sql = @"select distinct enEmp.* 
                            from adm_Employee enEmp
	                        inner join OrganizationEmployee enOrganizationEmp on enEmp.EmployeeID = enOrganizationEmp.EmployeeID and enOrganizationEmp.Deleted = 0 
	                        inner join vOrganizationInGroup enOrganization on enOrganizationEmp.OrganizationID = enOrganization.OrganizationID
                        where enEmp.Deleted = 0 and enOrganization.ParentID = @organizationID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@organizationID", organizationID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }

        public List<Employee> SearchListManagerByOrganizationID(int? organizationID)
        {
            string sql = @"select distinct enEmp.* 
                            from adm_Employee enEmp
	                        inner join OrganizationManager enOrganizationManager on enEmp.EmployeeID = enOrganizationManager.EmployeeID and enOrganizationManager.Deleted = 0 
	                        inner join vOrganizationInGroup enOrganization on enOrganizationManager.OrganizationID = enOrganization.OrganizationID
                        where enEmp.Deleted = 0 and enOrganization.ParentID = @organizationID";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@organizationID", organizationID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }

        public List<Employee> SearchListUserByListStringOrganizationID(string lstOrganizationID)
        {
            string sql = string.Format(@"select distinct enEmp.* 
                            from adm_Employee enEmp
	                        inner join OrganizationEmployee enOrganizationEmp on enEmp.EmployeeID = enOrganizationEmp.EmployeeID and enOrganizationEmp.Deleted = 0 
	                        inner join vOrganizationInGroup enOrganization on enOrganizationEmp.OrganizationID = enOrganization.OrganizationID
                        where enEmp.Deleted = 0 and enOrganization.ParentID in ({0})", lstOrganizationID);
            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }

        public List<Employee> SearchListManagerByListStringOrganizationID(string lstOrganizationID)
        {
            string sql = string.Format(@"select distinct enEmp.* 
                            from adm_Employee enEmp
	                        inner join OrganizationManager enOrganizationManager on enEmp.EmployeeID = enOrganizationManager.EmployeeID and enOrganizationManager.Deleted = 0 
	                        inner join vOrganizationInGroup enOrganization on enOrganizationManager.OrganizationID = enOrganization.OrganizationID
                        where enEmp.Deleted = 0 and enOrganization.ParentID in ({0})", lstOrganizationID);
            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }

        public string GetEmployeeName(int employeeID)
        {
            using (DataContext dataContext = new DataContext())
            {
                var item = dataContext.SelectFieldsByID<Employee>(new string[] { Employee.C_Name }, employeeID);
                return item == null ? null : item.Name;
            }
        }
    }
}
