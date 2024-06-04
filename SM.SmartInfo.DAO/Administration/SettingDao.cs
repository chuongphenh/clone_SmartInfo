using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO.Administration
{
    public class SettingDao : BaseDao
    {
        public Setting GetSettingFirst()
        {
            var cmdText = @"SELECT * FROM Setting";

            var cmd = new SqlCommand(cmdText);
            using (var dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<Setting>(cmd).First();
            }
        }
        public void UpdateDataSetting(SettingParam param)
        {
            Setting item = param.Setting;
            UpdateItem(item);
        }
    }
}
