using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.BIZ.Administration
{
    class SettingBiz : BizBase
    {
        SettingDao _dao = new SettingDao();
        public void GetSettingFirst(SettingParam setting)
        {
            setting.Setting = _dao.GetSettingFirst();
        }

        public void UpdateDataSetting(SettingParam setting)
        {
            _dao.UpdateDataSetting(setting);
        }
    }
}
