using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.EntityInfos
{
    [Serializable]
    public class ReportInfo : BaseEntity
    {
        public int ReportType { get; set; }

        public string ReportTitle { get; set; }

        public string TemplateName { get; set; }

        public string XmlConfigName { get; set; }

        public ReportInfo() : base(string.Empty, string.Empty, string.Empty, string.Empty) { }
    }

    [Serializable]
    public class ReportParamInfo
    {
        public string ParamName { get; set; }

        public object ParamValue { get; set; }

        public ReportParamInfo()
        {
        }

        public ReportParamInfo(string name, object val)
        {
            ParamName = name;
            ParamValue = val;
        }
    }
}