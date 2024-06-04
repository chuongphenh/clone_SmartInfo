using System.Data.SqlClient;
using SM.SmartInfo.DAO.Common;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.DAO.Notification
{
    public class TokenDeviceDao : BaseDao
    {
        public List<ntf_TokenDevice> GetAllTokenDevice(int? featureID)
        {
            string cmdText = $@"SELECT DISTINCT ntf_td.* 
                                FROM ntf_TokenDevice ntf_td 
                                JOIN adm_Employee emp ON emp.EmployeeID = ntf_td.EmployeeID 
                                JOIN adm_EmployeeRole empRole ON empRole.EmployeeID = emp.EmployeeID
                                JOIN adm_Role role ON role.RoleID = empRole.RoleID
                                WHERE ntf_td.FCMToken IS NOT NULL 
                                  AND ntf_td.DeviceID IS NOT NULL  
                                  AND emp.Status = 1 
                                  AND emp.Deleted = 0
                                  AND EXISTS (
                                      SELECT 1
                                      FROM adm_Right r
                                      INNER JOIN adm_Function fu ON r.FunctionID = fu.FunctionID
                                      INNER JOIN adm_Feature f ON r.FeatureID = f.FeatureID
                                      INNER JOIN adm_EmployeeRole er ON (r.RoleID = er.RoleID OR r.EmployeeID = er.EmployeeID)
                                      WHERE er.EmployeeID = emp.EmployeeID
                                        AND f.FeatureID = {featureID} -- 14:Sự vụ 15:Tổ chức 16:Tin tức 18:Thông báo
                                        AND fu.FunctionID IN (1, 5) --Xem danh sách và xem chi tiết 
                                      --GROUP BY er.EmployeeID
                                      --HAVING COUNT(DISTINCT fu.FunctionID) = 2  Đảm bảo có cả 1 và 5
                                   )";

            SqlCommand cmd = new SqlCommand(cmdText);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_TokenDevice>(cmd);
            }
        }
        // lấy theo những tài khoản có quyền xem thông báo 
        public List<ntf_TokenDevice> GetListTokenDeviceByPressAgencyHRID(int? PressAgencyHRID)
        {
            string cmdText = $@"SELECT DISTINCT ntf_td.* 
                                FROM ntf_TokenDevice ntf_td 
                                JOIN adm_Employee emp ON emp.EmployeeID = ntf_td.EmployeeID 
                                JOIN adm_EmployeeRole empRole ON empRole.EmployeeID = emp.EmployeeID
                                JOIN adm_Role role ON role.RoleID = empRole.RoleID
                                WHERE ntf_td.FCMToken IS NOT NULL 
                                  AND ntf_td.DeviceID IS NOT NULL  
                                  AND emp.Status = 1 
                                  AND emp.Deleted = 0
                                  AND EXISTS (
                                      SELECT 1
                                      FROM adm_Right r
                                      INNER JOIN adm_Function fu ON r.FunctionID = fu.FunctionID
                                      INNER JOIN adm_Feature f ON r.FeatureID = f.FeatureID
                                      INNER JOIN adm_EmployeeRole er ON (r.RoleID = er.RoleID OR r.EmployeeID = er.EmployeeID)
                                       WHERE er.EmployeeID = emp.EmployeeID
										AND f.FeatureID = 18 -- Menu thông báo
										AND fu.FunctionID IN (1, 5) --Xem danh sách và xem chi tiết 
									  --GROUP BY er.EmployeeID
									  --HAVING COUNT(DISTINCT fu.FunctionID) = 2  Đảm bảo có cả 1 và 5
                                  )
                                  AND (
                                      LOWER(role.Name) = 'qtht' 
                                      OR EXISTS (
                                          SELECT 1 
                                          FROM [dbo].[SharingManagement] sm 
                                          WHERE 
                                              sm.PressAgencyHRID = {PressAgencyHRID} 
                                              AND sm.UserId = ntf_td.EmployeeID 
                                              AND sm.isShared = 1 
                                              AND sm.GroupName IS NULL
                                          OR (
                                              sm.PressAgencyHRID = {PressAgencyHRID}
                                              AND sm.UserId IS NULL 
                                              AND sm.UserEmail IS NULL 
                                              AND sm.GroupName IN (
                                                  SELECT r.Name
                                                  FROM adm_EmployeeRole er2
                                                  JOIN adm_Employee e ON e.EmployeeID = er2.EmployeeID
                                                  JOIN adm_Role r ON r.RoleID = er2.RoleID
                                                  WHERE e.EmployeeID = ntf_td.EmployeeID
                                              )
                                          )
                                      )
                                  );";

            SqlCommand cmd = new SqlCommand(cmdText);
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_TokenDevice>(cmd);
            }
        }
        public List<string> GetListStrTokenDevice()
        {
            string query = @"Select [Token] From [ntf_TokenDevice]";
            SqlCommand cmd = new SqlCommand(query);
            using (DataContext ctx = new DataContext())
            {
                return ctx.ExecuteSelect<string>(cmd);
            }
        }
    }
}