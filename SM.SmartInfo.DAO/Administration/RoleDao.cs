using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Params.Administration;
using System.Data.SqlClient;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;

namespace SM.SmartInfo.DAO.Administration
{
    public class RoleDao : BaseDao
    {
        public bool CheckExitsName(string name, int? id)
        {
            string cmdText = @"select count(*)
                                from adm_Role
                                where Deleted=0 and Name=@Name AND (@RoleID is null OR RoleID<>@RoleID)";
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@Name", name);
            sqlCmd.Parameters.AddWithValue("@RoleID", id);

            using (var dataContext = new DataContext())
            {
                int count = dataContext.ExecuteSelect<int>(sqlCmd).FirstOrDefault();
                return count > 0;
            }
        }

        #region Get
        public List<Role> GetAllActiveRole()
        {
            List<Role> res;
            using (var dataContext = new DataContext())
            {
                // lấy ra list VD: QTHT, chuyên gia, chuyên viên,...
                res = dataContext.SelectItemByColumnName<Role>(Role.C_Status, SMX.Status.Active);
            }
            return res.OrderBy(c => c.Name).ToList();
        }
        public List<Role> GetAllActiveRoleExceptQTHT()
        {
            List<Role> res;
            using (var dataContext = new DataContext())
            {
                // lấy ra list VD: QTHT, chuyên gia, chuyên viên,...
                res = dataContext.SelectItemByColumnName<Role>(Role.C_Status, SMX.Status.Active);
            }
            return res
                .Where(role => !role.Name.Equals("QTHT", StringComparison.OrdinalIgnoreCase))
                .OrderBy(role => role.Name)
                .ToList();
        }

        public List<string> GetListRoleIDByPressAgencyHRID(int? PressAgencyHRID)
        {
            if (PressAgencyHRID == null || PressAgencyHRID == 0)
            {
                return new List<string>();
            }

            string cmdText = @"SELECT 
                            role.RoleID
                       FROM [SharingManagement] sm
                       JOIN adm_Role role ON LOWER(role.Name) = LOWER(sm.GroupName)
                       WHERE PressAgencyHRID = @PressAgencyHRID;";
            using (var dataContext = new DataContext())
            {
                using (SqlCommand sqlCmd = new SqlCommand(cmdText))
                {
                    sqlCmd.Parameters.AddWithValue("@PressAgencyHRID", PressAgencyHRID);
                    List<int> resultInts = dataContext.ExecuteSelect<int>(sqlCmd);
                    List<string> roleIDs = resultInts.Select(o => o.ToString()).ToList();
                    return roleIDs;
                }
            }
        }


        public List<Role> GetAllActiveRoleByName(List<string> lstName)
        {
            if (lstName == null || lstName.Count == 0)
            {
                return new List<Role>(); 
            }
            var formattedNames = lstName.Select(name => $"N'{name}'").ToList();
            var cmdText = $@"select RoleID, Name, Status, Version, Description from adm_Role
                     where Deleted = 0 AND Status = 1 AND Name IN ({string.Join(", ", formattedNames)})";

            var cmd = new SqlCommand(cmdText);
            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Role>(cmd);
            }

        }

        public List<Role> SearchRole(RoleParam param)
        {
            var cmdText = @"select RoleID,Name,Status,Version,Description from adm_Role
                            where Deleted=0";

            var cmd = new SqlCommand(cmdText);
            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Role>(cmd);
            }
        }
        #endregion

        public System.Data.DataTable GetRolePermission()
        {
            string query = @"with  CTE (FeatureID, Name, ParentID)
as
(
	select FeatureID, CAST(Name as nvarchar(512)), FeatureID
	    
	from adm_Feature
	where Deleted = 0 and Status = 1 and (IsVisible = 1 or ISNULL(URL, '#') <> '#') and ParentID is null
	
	union all
	
	select enChild.FeatureID, CAST(enParent.Name + ' > ' + enChild.Name as nvarchar(512)) as Name, enChild.ParentID
	    
	from CTE as enParent
	inner join adm_Feature as enChild on enParent.FeatureID = enChild.ParentID
	where Deleted = 0 and Status = 1-- and (IsVisible = 1 or ISNULL(URL, '#') <> '#')
)
select distinct
	1 as OrderNo,
	rol.Name as RoleName,
	fea.Name as FeatureName,
	fun.Name as FunctionName,
	(case rol.Status when 1 then N'Đang sử dụng' when 2 then N'Không sử dụng' else '' end) as StatusName
from 
	adm_Role rol
	left join adm_Right rig on rig.RoleID = rol.RoleID and rig.RoleID is not null
	left join CTE fea on fea.FeatureID = rig.FeatureID
	left join adm_Function fun on  fun.FunctionID = rig.FunctionID
order by rol.Name, fea.Name, fun.Name;";
            using (DataContext context = new DataContext())
            {
                return context.ExecuteDataTable(query);
            }
        }

        public List<Employee> GetListEmployeeByListFixedBizPermission(List<string> lstFixedBizPermission)
        {
            string query = @"select
	                            emp.EmployeeID, emp.Name
                            from
	                            adm_Role rol
	                            inner join adm_EmployeeRole empRole on empRole.RoleID = rol.RoleID
	                            inner join adm_Employee emp on emp.EmployeeID = empRole.EmployeeID
	                            inner join adm_FixedBusinessPermission fixBiz on fixBiz.RoleID = rol.RoleID
                            where rol.Deleted = 0 and emp.Deleted = 0 and emp.Status = 1 and fixBiz.Code in ({0})";
            query = string.Format(query, BuildInCondition(lstFixedBizPermission));
            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Employee>(query);
            }
        }
    }
}