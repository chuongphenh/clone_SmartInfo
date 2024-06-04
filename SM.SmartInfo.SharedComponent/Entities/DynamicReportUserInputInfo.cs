using System;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public class DynamicReportUserInputInfo
    {
        public string DisplayName { get; set; }
        public string ConfigFileName { get; set; }
        //public List<DynamicReportDataSource> DynamicReportDataSources { get; set; }

        //public DynamicReportUserInputInfo()
        //{
        //}

        public DynamicReportUserInputInfo(string displayName, string configFileName)
        {
            DisplayName = displayName;
            ConfigFileName = configFileName;
        }
    }

    //[Serializable]
    //public class DynamicReportDataSource
    //{
    //    public string Name { get; set; }
    //    public string CommandText { get; set; }
    //    public string OrderStatement { get; set; }

    //    /// <summary>
    //    /// Type of object to auto binding from DataTable, then Export List item to Excel
    //    /// </summary>
    //    public string OutputType { get; set; }

    //    public List<DynamicReportCondition> DynamicReportConditions { get; set; }
    //}

    public class DynamicReportCondition
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
    }
}
