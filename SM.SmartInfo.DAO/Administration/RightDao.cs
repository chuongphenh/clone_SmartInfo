using System.Data.SqlClient;
using System.Collections.Generic;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.DAO.Administration
{
    public class RightDao : BaseDao
    {
        #region Modification methods

        public void InsertRight(List<Right> lstItem)
        {
            using (DataContext dataContext = new DataContext())
            {
                foreach (var item in lstItem)
                {
                    dataContext.InsertItem<Right>(item);
                }
            }
        }

        public List<int> GetRightsToDelete(int featureID, int? employeeID, int? roleID)
        {
            var cmdText = @"SELECT RightID
                            FROM adm_Right as enRight
                            WHERE (@EmployeeID is null or EmployeeID=@EmployeeID)
	                            AND (@RoleID is null or RoleID=@RoleID)
	                            AND FeatureID=@FeatureID";

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@RoleID", roleID);

            using (var datacontext = new DataContext())
            {
                return datacontext.ExecuteSelect<int>(cmd);
            }
        }

        public void DeleteItems(int featureID, int? employeeID, int? roleID)
        {
            List<int> lstDelete = GetRightsToDelete(featureID, employeeID, roleID);

            using (var dataContext = new DataContext())
            {
                foreach (var item in lstDelete)
                {
                    dataContext.DeleteItem<Right>(item);
                }
            }
        }

        #endregion

        #region  Getting Methods

        public List<Function> GetFunctionsByFeatureID(int featureID)
        {
            var cmdText = @"select distinct enFunc.FunctionID,enFunc.Code,enFunc.Name
                            from adm_Function as enFunc
                            inner join adm_FeatureFunction as enFF on enFF.FunctionID=enFunc.FunctionID
                            inner join adm_Feature as enFeature on enFeature.FeatureID=enFF.FeatureID
                            where enFeature.FeatureID=@FeatureID";

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);

            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Function>(cmd);
            }
        }

        public List<int?> GetFunctionIDsByItemID(int featureID, int? roleID, int? employeeID)
        {
            var cmdText = @"SELECT enRight.FunctionID
                            FROM adm_FeatureFunction AS enFF
                                INNER JOIN adm_Right as enRight ON enFF.FeatureID=enRight.FeatureID and enFF.FunctionID=enRight.FunctionID
                            WHERE enFF.FeatureID=@FeatureID 
                                AND (@RoleID is null OR RoleID=@RoleID)
                                AND (@EmployeeID is null OR EmployeeID=@EmployeeID)";

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);
            cmd.Parameters.AddWithValue("@RoleID", roleID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<int?>(cmd);
            }
        }

        #endregion
    }
}