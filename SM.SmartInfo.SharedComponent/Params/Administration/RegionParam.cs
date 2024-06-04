using SM.SmartInfo.SharedComponent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class RegionParam : BaseParam
    {
        public RegionParam(string functionType)
           : base(Constants.BusinessType.Administrations, functionType)
        {
        }
        public adm_Region adm_Region { get; set; }
        public int? RegionID { get; set; }
        public List<adm_Region> Listadm_Region { get; set; }

        public List<adm_RegionProvince> Listadm_RegionProvince { get; set; }
    }
}
