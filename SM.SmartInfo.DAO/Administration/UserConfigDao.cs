using System.Collections.Generic;
using System.Linq;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.EntityInfos;

using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Core.Dao;
using static SM.SmartInfo.SharedComponent.Constants.SMX;
using System;
using SM.SmartInfo.Utils;

namespace SM.SmartInfo.DAO.Administration
{
    //Standard Admin
    //Class nay khong quan tam item Active hay InActive
    public class UserConfigDao : BaseDao
    {
        private string ConnectionString = ConfigUtils.ConnectionString;
        private void ShareToStaff(List<Employee> listEmp, int pressAgencyHRID)
        {
            foreach (var item in listEmp)
            {
                string sql = @"INSERT INTO SharingManagement (PressAgencyHRID, UserId, UserEmail, CreatedDTG, UpdatedDTG, isShared) 
                                VALUES(@PressAgencyHRID, @UserId, @UserEmail, @CreatedDTG, @UpdatedDTG, @isShared)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);
                    command.Parameters.AddWithValue("@UserId", item.EmployeeID);
                    command.Parameters.AddWithValue("@UserEmail", item.Email);
                    command.Parameters.AddWithValue("@CreatedDTG", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedDTG", DateTime.Now);
                    command.Parameters.AddWithValue("@isShared", 1);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>EmployeeID</returns>
        public int InsertEmployeeAndGetID(Employee employee)
        {
            int newEmployeeID = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                    INSERT INTO [adm_Employee] (Name, Username, Password, Gender, DOB, Notes, UDID, Status, Deleted, Version, 
                                           CreatedBy, CreatedDTG, UpdatedBy, UpdatedDTG, CreatedOn, Description, Email, 
                                           Mobile, Phone, Sector, EmployeeCode, AuthorizationType, LdapCnnName, Level, 
                                           IsLocked, ListBranchCode, IsManager, CIFCode, DeviceName, Guid, IsCheckFinger, 
                                           IsLockByLogin, UnlockedTime, LoggingAttemp, lastLoginDate)
                    VALUES (@Name, @Username, @Password, @Gender, @DOB, @Notes, @UDID, @Status, @Deleted, @Version, 
                            @CreatedBy, @CreatedDTG, @UpdatedBy, @UpdatedDTG, @CreatedOn, @Description, @Email, 
                            @Mobile, @Phone, @Sector, @EmployeeCode, @AuthorizationType, @LdapCnnName, @Level, 
                            @IsLocked, @ListBranchCode, @IsManager, @CIFCode, @DeviceName, @Guid, @IsCheckFinger, 
                            @IsLockByLogin, @UnlockedTime, @LoggingAttemp, @lastLoginDate);
                    SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", (object)employee.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Username", (object)employee.Username ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Password", (object)employee.Password ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)employee.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DOB", (object)employee.DOB ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)employee.Notes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UDID", (object)employee.UDID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object)employee.Status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Deleted", (object)employee.Deleted ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Version", (object)employee.Version ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", (object)employee.CreatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedDTG", (object)employee.CreatedDTG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", (object)employee.UpdatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedDTG", (object)employee.UpdatedDTG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedOn", (object)employee.CreatedOn ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)employee.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)employee.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)employee.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Sector", (object)employee.Sector ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmployeeCode", (object)employee.EmployeeCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AuthorizationType", (object)employee.AuthorizationType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LdapCnnName", (object)employee.LdapCnnName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Level", (object)employee.Level ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsLocked", (object)employee.IsLocked ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ListBranchCode", (object)employee.ListBranchCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsManager", (object)employee.IsManager ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CIFCode", (object)employee.CIFCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DeviceName", (object)employee.DeviceName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Guid", (object)employee.Guid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsCheckFinger", (object)employee.IsCheckFinger ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsLockByLogin", (object)employee.IsLockByLogin ?? 0);
                    cmd.Parameters.AddWithValue("@UnlockedTime", (object)employee.UnlockedTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LoggingAttemp", (object)employee.LoggingAttemp ?? 0);
                    cmd.Parameters.AddWithValue("@lastLoginDate", (object)employee.lastLoginDate ?? DBNull.Value);

                    connection.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        newEmployeeID = Convert.ToInt32(result);
                    }
                    else
                        newEmployeeID = -1;
                }
            }
            return newEmployeeID;
        }
        public void UpdateEmployeeByID(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
            UPDATE [adm_Employee]
            SET Name = @Name,
                Username = @Username,
                Password = @Password,
                Gender = @Gender,
                DOB = @DOB,
                Notes = @Notes,
                UDID = @UDID,
                Status = @Status,
                Deleted = @Deleted,
                Version = @Version,
                CreatedBy = @CreatedBy,
                CreatedDTG = @CreatedDTG,
                UpdatedBy = @UpdatedBy,
                UpdatedDTG = @UpdatedDTG,
                CreatedOn = @CreatedOn,
                Description = @Description,
                Email = @Email,
                Mobile = @Mobile,
                Phone = @Phone,
                Sector = @Sector,
                EmployeeCode = @EmployeeCode,
                AuthorizationType = @AuthorizationType,
                LdapCnnName = @LdapCnnName,
                Level = @Level,
                IsLocked = @IsLocked,
                ListBranchCode = @ListBranchCode,
                IsManager = @IsManager,
                CIFCode = @CIFCode,
                DeviceName = @DeviceName,
                Guid = @Guid,
                IsCheckFinger = @IsCheckFinger,
                IsLockByLogin = @IsLockByLogin,
                UnlockedTime = @UnlockedTime,
                LoggingAttemp = @LoggingAttemp,
                lastLoginDate = @lastLoginDate
            WHERE EmployeeID = @EmployeeID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", (object)employee.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Username", (object)employee.Username ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Password", (object)employee.Password ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)employee.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DOB", (object)employee.DOB ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)employee.Notes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UDID", (object)employee.UDID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object)employee.Status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Deleted", (object)employee.Deleted ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Version", (object)employee.Version ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", (object)employee.CreatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedDTG", (object)employee.CreatedDTG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", (object)employee.UpdatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedDTG", (object)employee.UpdatedDTG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedOn", (object)employee.CreatedOn ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)employee.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)employee.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)employee.Mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Sector", (object)employee.Sector ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmployeeCode", (object)employee.EmployeeCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AuthorizationType", (object)employee.AuthorizationType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LdapCnnName", (object)employee.LdapCnnName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Level", (object)employee.Level ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsLocked", (object)employee.IsLocked ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ListBranchCode", (object)employee.ListBranchCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsManager", (object)employee.IsManager ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CIFCode", (object)employee.CIFCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DeviceName", (object)employee.DeviceName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Guid", (object)employee.Guid ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsCheckFinger", (object)employee.IsCheckFinger ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsLockByLogin", (object)employee.IsLockByLogin ?? 0);
                    cmd.Parameters.AddWithValue("@UnlockedTime", (object)employee.UnlockedTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LoggingAttemp", (object)employee.LoggingAttemp ?? 0);
                    cmd.Parameters.AddWithValue("@lastLoginDate", (object)employee.lastLoginDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //----
        private void UpdateExistingSharingStatus(List<SharingManagement> listSharing)
        {
            foreach (var item in listSharing)
            {
                string sql = @"UPDATE SharingManagement SET isShared = 1 WHERE ID = @Id";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", item.Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertOrUpdateSharingManagement(UserParam param)
        {
            string sql = @"SELECT sm.* FROM SharingManagement sm JOIN adm_Employee e ON sm.UserId = e.EmployeeID 
                            WHERE sm.PressAgencyHRID = @pressAgencyHRID";
            List<SharingManagement> listShared = new List<SharingManagement>();
            SqlCommand cmdGetSharedUser = new SqlCommand(sql);
            cmdGetSharedUser.Parameters.AddWithValue("@pressAgencyHRID", param.PressAgencyHRID);
            using (DataContext datacontext = new DataContext())
            {
                listShared = datacontext.ExecuteSelect<SharingManagement>(cmdGetSharedUser);
            }

            var listExisting = new List<SharingManagement>();
            var listNew = new List<Employee>();

            var querry = param.Employees.Select(x =>
            {
                var item = listShared.FirstOrDefault(ls => ls.UserId.Equals(x.EmployeeID) && ls.PressAgencyHRID.Equals(param.PressAgencyHRID));

                if (item != null)
                {
                    listExisting.Add(item);
                    return x;
                }
                else
                {
                    listNew.Add(x);
                    return x;
                }
            }).ToList();

            if (listExisting.Count > 0)
            {
                UpdateExistingSharingStatus(listExisting);
            }
            if (listNew.Count > 0)
            {
                ShareToStaff(listNew, param.PressAgencyHRID);
            }

        }

        public void GetListUserForSharing(UserParam param)
        {
            List<Employee> listEmployee = new List<Employee>();

            string sql = "";
            if(param != null && param.PositionId != -1 && param.listEmailForSharing.Count == 0)
            {
                sql += $"SELECT* FROM adm_Employee a JOIN adm_EmployeeRole b ON a.EmployeeID = b.EmployeeID Where RoleID = {param.PositionId}";
            }
            else if(param != null && param.PositionId == -1 && param.listEmailForSharing.Count > 0)
            {
                string commaSeparatedString = "'" + string.Join("', '", param.listEmailForSharing) + "'";
                sql += $"SELECT* FROM adm_Employee WHERE Email IN ({commaSeparatedString})";
            }
            else if(param != null && param.PositionId != -1 && param.listEmailForSharing.Count > 0)
            {
                string commaSeparatedString = "'" + string.Join("', '", param.listEmailForSharing) + "'";
                sql += $"SELECT DISTINCT a.* FROM adm_Employee a JOIN adm_EmployeeRole b ON a.EmployeeID = b.EmployeeID Where RoleID = {param.PositionId} OR Email IN ({commaSeparatedString})";
            }

            using (DataContext dataContext = new DataContext())
            {
                listEmployee = dataContext.ExecuteSelect<Employee>(sql);
                param.Employees = listEmployee;
            }
        }

        public Employee SearchUserForSharing(UserParam param)
        {
            string sql = @"SELECT TOP(1) [EmployeeID]
                        ,[Name]
                        ,[Username]
                        ,[Password]
                        ,[Gender]
                        ,[DOB]
                        ,[Notes]
                        ,[Status]
                        ,[Deleted]
                        ,[Version]
                        ,[CreatedBy]
                        ,[CreatedDTG]
                        ,[UpdatedBy]
                        ,[UpdatedDTG]
                        ,[CreatedOn]
                        ,[Description]
                        ,[Email]
                        ,[Mobile]
                        ,[Phone]
                        ,[Sector]
                        ,[EmployeeCode]
                        ,[AuthorizationType]
                        ,[Level]
                        ,[LdapCnnName]
                        ,[ListBranchCode]
                        ,[IsManager]
                        ,[CIFCode]
                        ,[IsLocked]
                        ,[DeviceName]
                        ,[Guid]
                        ,[IsCheckFinger] 
                FROM adm_Employee
                WHERE Email LIKE '%' + @searchString + '%'
                OR Name LIKE '%' + @searchString + '%'";
            SqlCommand cmdGetUser = new SqlCommand(sql);
            cmdGetUser.Parameters.AddWithValue("@searchString", param.searchString);
            using (DataContext datacontext = new DataContext())
            {
                return datacontext.ExecuteSelect<Employee>(cmdGetUser).FirstOrDefault();
            }
        }
        public List<Employee> SearchListUserForSharing(UserParam param)
        {
            string sql = @"SELECT [EmployeeID]
                        ,[Name]
                        ,[Username]
                        ,[Password]
                        ,[Gender]
                        ,[DOB]
                        ,[Notes]
                        ,[Status]
                        ,[Deleted]
                        ,[Version]
                        ,[CreatedBy]
                        ,[CreatedDTG]
                        ,[UpdatedBy]
                        ,[UpdatedDTG]
                        ,[CreatedOn]
                        ,[Description]
                        ,[Email]
                        ,[Mobile]
                        ,[Phone]
                        ,[Sector]
                        ,[EmployeeCode]
                        ,[AuthorizationType]
                        ,[Level]
                        ,[LdapCnnName]
                        ,[ListBranchCode]
                        ,[IsManager]
                        ,[CIFCode]
                        ,[IsLocked]
                        ,[DeviceName]
                        ,[Guid]
                        ,[IsCheckFinger] 
                FROM adm_Employee
                WHERE Email LIKE '%' + @searchString + '%'
                OR Name LIKE '%' + @searchString + '%'";
            SqlCommand cmdGetUser = new SqlCommand(sql);
            cmdGetUser.Parameters.AddWithValue("@searchString", param.searchString);
            using (DataContext datacontext = new DataContext())
            {
                return datacontext.ExecuteSelect<Employee>(cmdGetUser);
            }
        }
        public void DeleteEmployeeRole(int employeeID)
        {
            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.DeleteItemByColumn<EmployeeRole>(EmployeeRole.C_EmployeeID, employeeID);
            }
        }

        public bool CheckUserActive(int employeeID)
        {
            using (DataContext dataContext = new DataContext())
            {
                int count = (int)dataContext.CountItemByColumnName<Employee>(new ConditionList
                                                {
                                                    {Employee.C_EmployeeID, employeeID},
                                                    {Employee.C_Status, SMX.Status.Active}
                                                });
                return count > 0;
            }
        }

        public bool CheckUserNameExist(string userName)
        {
            var cmdText = @"SELECT COUNT(*) 
                              FROM adm_Employee
                              WHERE Deleted = 0 AND UserName = @UserName";
            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@UserName", userName);

            using (DataContext datacontext = new DataContext())
            {
                int count = datacontext.ExecuteSelect<int>(cmd).First();
                return count > 0;
            }
        }

        public List<EmployeeRole> GetEmployeeRoleByEmployeeID(int employeeID)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByColumnName<EmployeeRole>(EmployeeRole.C_EmployeeID, employeeID);
            }
        }

        public List<EmployeeInfo> GetEmployeesForView(UserParam param)
        {
            //Get Emp
            string cmdText = @"
                                SELECT 
	                                emp.EmployeeID, emp.Name, emp.Username, emp.Status, emp.Email, emp.Version, emp.Deleted,
	                                v.DepartmentName, v.OrganizationOfManagerName
                                FROM adm_Employee emp
	                                LEFT JOIN adm_EmployeeRole enRole on emp.EmployeeID= enRole.EmployeeID
	                                LEFT JOIN
	                                (
		                                --Lay phong chuyen vien thuoc ve
		                                select enEmployee.EmployeeID, emEmpBreadCrum.BreadCrumb as DepartmentName, null as OrganizationOfManagerName
		                                from adm_Employee as enEmployee
		                                inner join OrganizationEmployee as enOrganizationEmployee on enEmployee.EmployeeID = enOrganizationEmployee.EmployeeID
		                                inner join vOrganization as emEmpBreadCrum on enOrganizationEmployee.OrganizationID = emEmpBreadCrum.OrganizationID
	    
		                                union

		                                --Lay phong ma chuyen vien la quan ly
		                                select enEmployee.EmployeeID, null as DepartmentName, enManagerOrg.BreadCrumb as OrganizationOfManagerName
		                                from adm_Employee as enEmployee
		                                inner join OrganizationManager as enOrgMng on enOrgMng.Deleted=0
				                                and enOrgMng.EmployeeID = enEmployee.EmployeeID 
		                                inner join vOrganization as enManagerOrg on enOrgMng.OrganizationID = enManagerOrg.OrganizationID
	                                ) v on emp.EmployeeID = v.EmployeeID
                                WHERE Deleted = 0
                                    AND (@UserName is null OR (emp.Username like @UserName) OR (emp.Name like @UserName))
                                    AND (@Email IS NULL OR (emp.Email like @Email))
                                    AND (@RoleID is null OR (enRole.RoleID=@RoleID))

                                --Neu tim theo Nhieu role hoac ko theo role -> Khi Emp co nhieu Role -> Loai cac Role thua
                                GROUP BY emp.EmployeeID, emp.Name, emp.Username, emp.Status, emp.Email, emp.Version, emp.Deleted,
	                                v.DepartmentName, v.OrganizationOfManagerName";

            SqlCommand cmdGetUser = new SqlCommand(cmdText);
            cmdGetUser.Parameters.AddWithValue("@RoleID", param.SearchParam.RoleID);
            cmdGetUser.Parameters.AddWithValue("@UserName", BuildLikeFilter(param.SearchParam.Username));
            cmdGetUser.Parameters.AddWithValue("@Email", BuildLikeFilter(param.SearchParam.Email));

            using (DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<EmployeeInfo>(dataContext, cmdGetUser, "Username", param.PagingInfo);
            }
        }

        public EmployeeImage GetEmployeeImageByEmployeeID(int? empID)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<EmployeeImage>(EmployeeImage.C_EmployeeID, empID).FirstOrDefault();
            }
        }

        public int? GetEmployeeImageIDByEmployeeID(int? empID)
        {
            string query = "select EmployeeImageID from adm_EmployeeImage Where EmployeeID = @EmployeeID";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@EmployeeID", empID);
            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<int?>(sqlCmd).FirstOrDefault();
            }
        }

        public void DeleteOrganizationEmployee(int? employeeID)
        {
            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.DeleteItemByColumn<OrganizationEmployee>(OrganizationEmployee.C_EmployeeID, employeeID);
            }
        }

        public void DeleteOrganizationManager(int? employeeID)
        {
            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.DeleteItemByColumn<OrganizationManager>(OrganizationManager.C_EmployeeID, employeeID);
            }
        }

        public void DeleteOrganizationEmployee(int? employeeID, List<int?> lstOrgID)
        {
            if (lstOrgID == null || lstOrgID.Count == 0)
                return;

            string query = "delete OrganizationEmployee where EmployeeID = {0} and OrganizationID in ({1})";
            query = string.Format(query, employeeID, string.Join(",", lstOrgID));

            using (DataContext dataContext = new DataContext())
            {
                dataContext.ExecuteNonQuery(query);
            }
        }

        public void DeleteOrganizationManager(int? employeeID, List<int?> lstOrgID)
        {
            if (lstOrgID == null || lstOrgID.Count == 0)
                return;

            string query = "delete OrganizationManager where EmployeeID = {0} and OrganizationID in ({1})";
            query = string.Format(query, employeeID, string.Join(",", lstOrgID));

            using (DataContext dataContext = new DataContext())
            {
                dataContext.ExecuteNonQuery(query);
            }
        }
    }
}
