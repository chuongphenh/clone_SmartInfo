using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Core.Dao;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class EmployeeDao
    {

        private string ConnectionString = ConfigUtils.ConnectionString;

        public Employee GetActiveEmployee(string userName)
        {

            string sql = @"SELECT * FROM adm_Employee WHERE Username COLLATE Latin1_General_BIN = @Username AND Status = 1";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext context = new DataContext())
            {
                cmd.Parameters.AddWithValue("@Username", userName);
                return context.ExecuteSelect<Employee>(cmd).FirstOrDefault();
                /*return context.SelectItemByColumnName<Employee>(new ConditionList
                                                {
                                                    {Employee.C_Username, userName},
                                                    {Employee.C_Status, SMX.Status.Active}
                                                }).FirstOrDefault();*/
                //return context.SelectItemByColumnName<Employee>(Employee.C_Name, userName).FirstOrDefault();
                /*string sql = "SELECT * FROM adm_Employee WHERE EmployeeID = 1 ";*/
                // empId = 2, 4, 5
                //string sql = "SELECT * FROM adm_Employee WHERE EmployeeID = 1 ";
                //SqlCommand cmd = new SqlCommand(sql);
                //return context.ExecuteSelect<Employee>(cmd).FirstOrDefault();
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            using (DataContext context = new DataContext())
            {
                context.UpdateItem<Employee>(emp);
            }
        }

        public void LockedUser(Employee emp)
        {
            string sql = @"UPDATE adm_Employee 
                        SET IsLockByLogin = 1, 
                            UnlockedTime = @UnlockedTime, 
                            IsLocked = 0, 
                            UpdatedDTG = @UpdatedDTG, 
                            UpdatedBy = @UpdatedBy 
                        WHERE Username COLLATE Latin1_General_BIN = @Username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@UnlockedTime", emp.UnlockedTime);
                cmd.Parameters.AddWithValue("@UpdatedDTG", emp.UpdatedDTG);
                cmd.Parameters.AddWithValue("@UpdatedBy", emp.UpdatedBy);
                cmd.Parameters.AddWithValue("@Username", emp.Username);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UnLockedUser(Employee emp)
        {
            string sql = @"UPDATE adm_Employee 
                  SET IsLockByLogin = 0, 
                      IsLocked = 1, 
                      UpdatedDTG = @UpdatedDTG, 
                      UpdatedBy = @UpdatedBy,
                      LoggingAttemp = 0
                  WHERE Username COLLATE Latin1_General_BIN = @Username";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@UpdatedDTG", emp.UpdatedDTG);
                cmd.Parameters.AddWithValue("@UpdatedBy", emp.UpdatedBy);
                cmd.Parameters.AddWithValue("@Username", emp.Username);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
