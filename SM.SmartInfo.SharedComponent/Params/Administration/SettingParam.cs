using SM.SmartInfo.SharedComponent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class SettingParam : BaseParam
    {
        public SettingParam(string functionType) : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public Setting Setting { get; set; }
    }
}
