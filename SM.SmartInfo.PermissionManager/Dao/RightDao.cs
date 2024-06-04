using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using System.Collections.Generic;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class RightDao
    {
        public List<FunctionRight> GetRightsOfEmployee(int employeeID)
        {
            string cmdText = string.Format(
                @"select distinct * from
                    (
                    select	fea.FeatureID, fuc.FunctionID, ff.FeatureFunctionID, fuc.Code as FunctionCode, ff.URL, ff.RuleID
                            from
                                adm_Employee emp
                                inner join adm_Right rig on rig.EmployeeID = emp.EmployeeID and rig.HasPermission = 1
                                inner join adm_FeatureFunction ff on ff.FeatureID = rig.FeatureID and ff.FunctionID = rig.FunctionID and ff.Deleted = 0
                                inner join adm_Function fuc on fuc.FunctionID = ff.FunctionID and fuc.Deleted = 0
                                inner join adm_Feature fea on fea.FeatureID = ff.FeatureID and fea.Deleted = 0 and fea.Status = {0}
                            where
                                emp.Deleted = 0 and emp.Status = {0} and emp.EmployeeID = {1}
                    union all                               
                    select  fea.FeatureID, fuc.FunctionID, ff.FeatureFunctionID, fuc.Code as FunctionCode, ff.URL, ff.RuleID
                            from
                                adm_Employee emp
                                inner join adm_EmployeeRole empRol on empRol.EmployeeID = emp.EmployeeID
                                inner join adm_Role rol on rol.RoleID = empRol.RoleID and rol.Deleted = 0 and rol.Status = {0}
                                inner join adm_Right rig on rig.RoleID = rol.RoleID and rig.HasPermission = 1
                                inner join adm_FeatureFunction ff on ff.FeatureID = rig.FeatureID and ff.FunctionID = rig.FunctionID and ff.Deleted = 0
                                inner join adm_Function fuc on fuc.FunctionID = ff.FunctionID and fuc.Deleted = 0
                                inner join adm_Feature fea on fea.FeatureID = ff.FeatureID and fea.Deleted = 0 and fea.Status = {0}
                            where
                                emp.Deleted = 0 and emp.Status = {0} and emp.EmployeeID = {1}
                    ) v
                    order by FeatureID, FunctionCode  ", SMX.Status.Active, employeeID);

            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<FunctionRight>(cmdText);
                return res;
            }
        }
    }
}
