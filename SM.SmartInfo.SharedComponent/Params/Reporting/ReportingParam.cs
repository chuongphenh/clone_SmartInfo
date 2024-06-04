using SM.SmartInfo.SharedComponent.EntityInfos;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Params.Reporting
{
    [Serializable]
    public class ReportingParam : BaseParam
    {
        public ReportingParam() : base(string.Empty, string.Empty) { }

        public ReportingParam(string functionType) : base(Constants.BusinessType.Reporting, functionType) { }

        public ReportInfo ReportInfo { get; set; }

        public string Query { get; set; }

        public List<ReportParamInfo> ListParam { get; set; }
    }
}
