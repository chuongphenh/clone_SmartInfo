using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using System.Collections.Generic;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class FeatureDao
    {
        public List<Feature> GetActiveFeaturesByID(List<int> lstFeatureID, int featureType)
        {
            string cmdText = @"select
	                                fea.*
                                from
	                                adm_Feature fea
                                where 
	                                fea.Deleted = 0 and
                                    fea.Status = {0} and
	                                fea.FeatureID in ({1}) and
                                    (fea.FeatureType IS NULL OR fea.FeatureType = {2})";
            cmdText = string.Format(cmdText, SMX.Status.Active, BuildInCondition(lstFeatureID), featureType);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Feature>(cmdText);
            }
        }

        public List<FeatureFunction> GetFeatureFunctionsByID(List<int> lstFeatureFunctionID)
        {
            string cmdText = @"select
	                                feaFun.*
                                from
	                                adm_FeatureFunction feaFun
                                where 
	                                feaFun.FeatureFunctionID in ({0})";
            cmdText = string.Format(cmdText, BuildInCondition(lstFeatureFunctionID));

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<FeatureFunction>(cmdText);
            }
        }

        public List<Function> GetFunctionsByID(List<int> lstFunctionID)
        {
            string cmdText = @"select
	                                fun.*
                                from
	                                adm_Function fun
                                where 
	                                fun.Deleted=0 and fun.FunctionID in ({0})";
            cmdText = string.Format(cmdText, BuildInCondition(lstFunctionID));

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Function>(cmdText);
            }
        }

        private string BuildInCondition(List<int> lstValue)
        {
            if (lstValue.Count == 0)
            {
                return "null";
            }
            else
            {
                return string.Join(", ", lstValue.ToArray());
            }
        }
    }
}
