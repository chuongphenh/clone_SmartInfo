using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class RoleParam : BaseParam
    {
        public RoleParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public List<Role> Roles { get; set; }
        public Role Role { get; set; }
        public List<string> RoleIDs { get; set; }

        public int RoleId { get; set; }
        public int? PressAgencyHRID { get; set; }

        public System.Data.DataTable DataTable { get; set; }

    }
}
