using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftMart.Core.Dao;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.SharedComponent.EntityInfos
{
    public class TargetInfo : Target
    {
        //[PropertyEntity("DepartmentName", false)]
        //public string DepartmentName { get; set; }

        //[PropertyEntity("OrganizationOfManagerName", false)]
        //public string OrganizationOfManagerName { get; set; }

        public string StrTargetType
        {
            get
            {
                return this.TargetType.HasValue && SMX.TargetType.dctTargetTypes.ContainsKey(this.TargetType) ? SMX.dicGender[this.TargetType] : string.Empty;
            }
        }
        public string TargetName { get; set; }
        public string TargetDescription { get; set; }
        #region Properties used for approve process flow (committee)

        //public string IsOrgManager { get { return !string.IsNullOrEmpty(OrganizationOfManagerName) ? "Có" : string.Empty; } }

        #endregion
    }
}
