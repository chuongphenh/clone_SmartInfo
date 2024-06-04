using System.Collections.Generic;
using System.Linq;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.DAO.Common;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.EntityViews;

namespace SM.SmartInfo.DAO.Administration
{
    public class EmployeeDao : BaseDao
    {
        public List<Employee> GetListEmployeeByRoleID(int roleID)
        {
            string cmdText = @"SELECT enEmp.*
                                FROM adm_Employee AS enEmp
                                     INNER JOIN adm_EmployeeRole AS enEmpRole ON enEmpRole.EmployeeID = enEmp.EmployeeID
                                WHERE enEmp.Deleted = 0 AND enEmpRole.RoleID = @RoleID";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@RoleID", roleID);

            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }



        public List<Employee> GetListAllEmployee()
        {
            string cmdText = @"SELECT * FROM [adm_Employee] WHERE Deleted = 0";
            SqlCommand cmd = new SqlCommand(cmdText);
            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }
        public Employee GetEmployeeByUserName(string userName)
        {
            string cmdText = @"SELECT * FROM [adm_Employee] WHERE [Deleted] = 0 AND [Username] = @userName";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@userName", userName);
            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd).FirstOrDefault();
            }
        }

        public List<Employee> GetShortEmployeeInOrg(int orgID)
        {
            string sql = @"select enEmp.EmployeeID, enEmp.UserName, enEmp.Name
                        from OrganizationEmployee as enOrgEmp
                        inner join adm_Employee as enEmp on enOrgEmp.EmployeeID = enEmp.EmployeeID
                        where enOrgEmp.OrganizationID = @OrganizationID";
            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@OrganizationID", orgID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(command);
            }
        }

        public string GetParentListByOrganizationID(int? OrganizationID)
        {
            string sql = @"SELECT vOrg.ParentList
                                FROM [vOrganization] vOrg
                                where vOrg.OrganizationID = @OrganizationID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@OrganizationID", OrganizationID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<string>(cmd).FirstOrDefault();
            }
        }

        public List<Employee> GetEmployeeListByParentList(string lstEmpID)
        {
            string sql = @" SELECT emp.*
                            FROM OrganizationManager orgMng
                            join adm_Employee emp on emp.EmployeeID = orgMng.EmployeeID and emp.Deleted = 0
                            where @ParentList like '%,' + cast(orgMng.OrganizationID as nvarchar(20)) + ',%' and orgMng.Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@ParentList", lstEmpID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Employee>(cmd);
            }
        }

        public List<Employee> LoadDataDisplayForReport()
        {
            string query = @"select 
                                emp.EmployeeID as OrderNo,
                                emp.Username, emp.Name, emp.EmployeeCode,
	                            emp.Sector, emp.DOB, emp.Gender,
	                            emp.Phone, emp.Mobile, emp.Email,
	                            emp.Status, emp.Description, emp.Level,
	                            emp.AuthorizationType,

								org.OrganizationID, orgEmp.IsManager,

								STUFF((
									SELECT '; ' + rol.Name
									FROM 
										adm_EmployeeRole empRol
										left join adm_Role rol on rol.RoleID = empRol.RoleID
									WHERE
										empRol.EmployeeID = emp.EmployeeID
										and rol.Deleted = @NotDeleted
									FOR XML PATH('')
								), 1, 2, '' ) AS RoleName
                            from 
	                            adm_Employee emp
								left join(
									select distinct tempOrg.OrganizationID, tempOrg.EmployeeID, tempOrg.IsManager
									from
									(
										select OrganizationID, EmployeeID, CAST(0 as bit) as IsManager from OrganizationEmployee
										union all select OrganizationID, EmployeeID, CAST(1 as bit) from OrganizationManager
									) tempOrg
								) orgEmp on orgEmp.EmployeeID = emp.EmployeeID
								left join Organization org on org.OrganizationID = orgEmp.OrganizationID
                            where emp.Deleted = @NotDeleted
                            Order by emp.Username";

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Employee>(sqlCmd);
            }
        }

        public List<Organization> GetListOrganization()
        {
            string query = @"select OrganizationID, Name, OfficeID, ParentID from Organization where Deleted = @NotDeleted";

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Organization>(sqlCmd);
            }
        }
        public List<vOrganization> GetList_vOrganizationByName(string _name)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.SelectItemByColumnName<vOrganization>("Name", _name);
            }
        }
        
        public List<OrganizationEmployee> GetListOrganizationEmployee()
        {
            string query = @"select OrganizationID, EmployeeID from OrganizationEmployee where Deleted = @NotDeleted";

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<OrganizationEmployee>(sqlCmd);
            }
        }

        public List<OrganizationManager> GetListOrganizationManager()
        {
            string query = @"select OrganizationID, EmployeeID from OrganizationEmployee where Deleted = @NotDeleted";

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<OrganizationManager>(sqlCmd);
            }
        }
    }
}
