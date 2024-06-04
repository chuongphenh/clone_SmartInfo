using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class RoleDao
    {
        public List<Role> GetRolesOfEmployee(int employeeID)
        {
            string cmdText = @"select rol.*
                                from
	                                adm_Employee emp
	                                inner join adm_EmployeeRole empRole on empRole.EmployeeID = emp.EmployeeID
	                                inner join adm_Role rol on rol.RoleID = empRole.RoleID

                                where 
                                    rol.Status=@Status and rol.Deleted=0	                                
                                    and emp.Deleted=0 and emp.Status=@Status
	                                and emp.EmployeeID = @EmployeeID";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@Status", SMX.Status.Active);
            sqlCmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Role>(sqlCmd);
            }
        }

        public List<Role> GetRolesByUserName(string userName)
        {
            string sql = @"select enRole.*
                        from adm_Employee as enEmployee
                            inner join adm_EmployeeRole as enEmployeeRole on enEmployee.EmployeeID = enEmployeeRole.EmployeeID
                            inner join adm_Role as enRole on enEmployeeRole.RoleID = enRole.RoleID
                        where enRole.Deleted = 0
                            and enEmployee.Username = @UserName";
            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@UserName", userName);

            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<Role>(command);
                return res;
            }
        }

        public List<string> GetFixedPermissionCode(List<int> lsRoleID)
        {
            string sql = @"select Code 
                            from adm_FixedBusinessPermission as enFixedBusinessPermission
                            where RoleID in ({0})";

            string inCondition = null;
            if (lsRoleID.Count == 0)
            {
                return new List<string>();
            }
            else
            {
                inCondition = string.Join(",", lsRoleID);
            }

            sql = string.Format(sql, inCondition);

            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<string>(sql);
            }
        }
    }
}
