using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class PlanParam : BaseParam
    {

        public PlanParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public int? PlanID { get; set; }
        public Document Plan { get; set; }
        public List<Document> Plans { get; set; }
        public List<int> TargetIDs { get; set; }
        public List<TargetInfo> TargetInfos { get; set; }

    }
}
