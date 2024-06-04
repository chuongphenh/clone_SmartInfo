using System.Collections.Generic;
using System.Linq;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.EntityInfos;

using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.DAO.Administration
{
    public class RegionDao : BaseDao
    {
        public List<adm_Region> GetRegionsForView(RegionParam param)
        {
            //Get Emp
            string cmdText = @" select * from adm_Region where Deleted=@Deleted
                                ";

            SqlCommand cmdGetUser = new SqlCommand(cmdText);
            cmdGetUser.Parameters.AddWithValue("@Deleted", SMX.smx_IsNotDeleted);

            using (DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<adm_Region>(dataContext, cmdGetUser, "CreatedDTG", param.PagingInfo);
            }
        }

        public List<adm_RegionProvince> GetListRegionProvince(int? regionID)
        {
            //Get Emp
            string cmdText = @" select * from adm_RegionProvince where RegionID=@RegionID
                                ";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@RegionID", regionID);
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_RegionProvince>(sqlCmd);
            }
        }

        public void Delete_adm_RegionProvinceByRegionID(int? regionID)
        {
            string query = @"delete from adm_RegionProvince
                            where RegionID = @RegionID";

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@RegionID", regionID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(sqlCmd);
            }
        }

        public void Delete_adm_RegionByRegionID(int? regionID)
        {
            string query = @"delete from adm_Region
                            where RegionID = @RegionID";

            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@RegionID", regionID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(sqlCmd);
            }
        }
    }
}
