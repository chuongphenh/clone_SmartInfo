using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.PermissionManager.UserAuthentication;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Permission;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class EmployeeLogDao
    {
        public void InsertEmployeeLog(EmployeeLog log)
        {
            using (DataContext context = new DataContext())
            {
                context.InsertItem<EmployeeLog>(log);
            }
        }

        public void UpdateEmployeeLog(EmployeeLog log)
        {
            using (DataContext context = new DataContext())
            {
                context.UpdateItem<EmployeeLog>(log);
            }
        }

        public void GetLog(AuthenticationParam param)
        {
            var pagingInfo = param.PagingInfo;
            string cmdText = @"select e.Username as [User],
	                                   cast((case when el.SignOutDTG IS null then 1 else 0 end) as bit) as IsLogin,
	                                   (case when el.SignOutDTG IS null then el.SignInDTG else el.SignOutDTG end) as Date,
                                      el.IPAddress
                                from adm_EmployeeLog el
                                left join adm_Employee e on el.EmployeeID = e.EmployeeID
                                where (@EmpID is null or el.EmployeeID = @EmpID) 
                                  and (@FromDTG is null or SignInDTG >= @FromDTG or SignOutDTG >= @FromDTG)
                                  and (@ToDTG is null or SignInDTG <= @ToDTG or SignOutDTG <= @ToDTG)";

            var cmd = new System.Data.SqlClient.SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@EmpID", param.EmployeeId);
            cmd.Parameters.AddWithValue("@FromDTG", param.FromDTG);
            cmd.Parameters.AddWithValue("@ToDTG", param.ToDTG);

            if (pagingInfo != null)
            {
                using (var dataContext = new DataContext())
                {
                    int recordCord;
                    List<EmployeeLog> lstData = dataContext.ExecutePaging<EmployeeLog>(cmd, pagingInfo.PageIndex, pagingInfo.PageSize, " Date desc", out recordCord);
                    pagingInfo.RecordCount = recordCord;

                    param.EmployeeLogs = lstData;
                }
            }
            else
            {
                using (var dataContext = new DataContext())
                {
                    param.EmployeeLogs = dataContext.ExecuteSelect<EmployeeLog>(cmd);
                }
            }
        }

        public void DeleteLog(AuthenticationParam param)
        {
            string cmdText = @"Delete adm_EmployeeLog
                                where (@empID is null or EmployeeID = @empID) 
                                  and (@fromDTG is null or SignInDTG >= @fromDTG or SignOutDTG >= @fromDTG)
                                  and (@toDTG is null or SignInDTG <= @toDTG or SignOutDTG <= @toDTG)";

            var cmd = new System.Data.SqlClient.SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@empID", param.EmployeeId);
            cmd.Parameters.AddWithValue("@fromDTG", param.FromDTG);
            cmd.Parameters.AddWithValue("@toDTG", param.ToDTG);

            using (var dataContext = new DataContext())
            {
                int count = dataContext.ExecuteNonQuery(cmd);
            }
        }

        public int CheckValidUser(AuthenticationParam param)
        {
            var realUserName = MembershipHelper.GetRealUserName(param.UserName);
            var passwordHash = MembershipHelper.GetPasswordHash(realUserName, param.Password);
            //var encryptPassword = Utils.Utility.Encrypt(param.UserName.ToLower(), param.Password);

            string sql = $@"SELECT * FROM adm_Employee WHERE Username COLLATE Latin1_General_BIN = @Username AND Password = '{passwordHash}' AND Deleted = 0";

            var user = new Employee();

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                cmd.Parameters.AddWithValue("@Username", param.UserName);
                user = dataContext.ExecuteSelect<Employee>(cmd).FirstOrDefault();
            }

            if (user == null)
            {
                return 404;
            }

            if (user.IsLockByLogin == 1 && user.UnlockedTime < DateTime.Now && user.LoggingAttemp == 5)
            {
                return 400;
            }

            param.Employee = user;

            return 200;
        }
        
        public void UpdateLoggingAttemp(AuthenticationParam param)
        {
            string sql = $@"UPDATE adm_Employee SET LoggingAttemp = LoggingAttemp + 1 WHERE Username COLLATE Latin1_General_BIN = @Username";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                cmd.Parameters.AddWithValue("@Username", param.UserName);
                dataContext.ExecuteNonQuery(cmd);
            }
        }
        
        public Employee GetLoggingAttemptByUsername(AuthenticationParam param)
        {
            string sql = @"SELECT * FROM adm_Employee WHERE Username COLLATE Latin1_General_BIN = @Username";

            SqlCommand cmd = new SqlCommand(sql);

            var user = new Employee();

            using (DataContext dataContext = new DataContext())
            {
                cmd.Parameters.AddWithValue("@Username", param.UserName);
                return dataContext.ExecuteSelect<Employee>(cmd).First();
            }
        }
    }
}