using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class TargetParam : BaseParam
    {

        public TargetParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public int? TargetID { get; set; }
        public Target Target { get; set; }
        public List<Target> Targets { get; set; }

    }
}
