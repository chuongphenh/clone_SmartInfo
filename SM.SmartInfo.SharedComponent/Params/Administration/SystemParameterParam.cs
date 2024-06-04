using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params;

namespace SM.SmartInfo.SharedComponent.Params.CommonList
{
    public class SystemParameterParam : BaseParam
    {
        public List<SystemParameter> SystemParameters { get; set; }
        public SystemParameter SystemParameter { get; set; }
        public int FeatureID { get; set; }
        public string Ext1 { get; set; }
        public int? Ext1i { get; set; }
        public int RegionID { get; set; }
        public int SystemParameterID { get; set; }
        public int? Version { get; set; }
        public int? ID { get; set; }
        public string SystemParameterName { get; set; }
        public string Code { get; set; }
        public string OfficeID { get; set; }

        //Common List
        public SystemParameter Region { get; set; }
        public List<SystemParameter> Regions { get; set; }

        public SystemParameter Province { get; set; }
        public List<SystemParameter> Provinces { get; set; }

        public SystemParameter VehicleType { get; set; }
        public List<SystemParameter> VehicleTypes { get; set; }

        public SystemParameter Zone { get; set; }
        public List<SystemParameter> Zones { get; set; }

        //public SystemParameterArea SystemParameterArea { get; set; }
        //public List<SystemParameterArea> SystemParameterAreas { get; set; }

        public SystemParameterParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }
    }
}

