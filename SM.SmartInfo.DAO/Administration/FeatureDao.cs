using System.Data.SqlClient;
using System.Collections.Generic;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.DAO.Administration
{
    public class FeatureDao : BaseDao
    {
        public List<Feature> GetAllActiveFeature()
        {
            // Them dieu kien de show trang chu
            var cmdText = @"SELECT FeatureID, Name, ParentID, Level, Description
                            FROM adm_Feature 
                            WHERE Deleted = @NotDeleted and Status=@Status and (IsVisible = 1 or IsNull(URL, '#') <> '#')";

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Status", SMX.Status.Active);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Feature>(cmd);
            }
        }

        public List<Feature> GetAllActiveFeatureBy_ListParentID(List<int> lstParentID)
        {
            // Them dieu kien de show trang chu
            var cmdText = @"SELECT FeatureID, Name, ParentID, URL, Description
                            FROM adm_Feature 
                            WHERE Deleted = @NotDeleted 
                            and ParentID in ({0})
                            and Status=@Status and (IsVisible = 1 or IsNull(URL, '#') <> '#')";

            cmdText = string.Format(cmdText, base.BuildInCondition(lstParentID));
            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Status", SMX.Status.Active);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Feature>(cmd);
            }
        }
    }
}