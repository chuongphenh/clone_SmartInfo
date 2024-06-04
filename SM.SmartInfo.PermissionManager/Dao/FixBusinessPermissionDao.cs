using SM.SmartInfo.DAO.Common;
using System.Collections.Generic;

namespace SM.SmartInfo.PermissionManager.Dao
{
    class FixBusinessPermissionDao
    {
        public List<Shared.FixedBusinessPermission> GetFixedBusinessPermissions(List<int?> roleIDs)
        {
            if (roleIDs == null || roleIDs.Count == 0)
                return new List<Shared.FixedBusinessPermission>();

            string query = "Select * From adm_FixedBusinessPermission where RoleID in ({0})";
            query = string.Format(query, string.Join(",", roleIDs));
            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Shared.FixedBusinessPermission>(query);
            }
        }
    }
}
