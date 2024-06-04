using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftMart.Core.Dao;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.SharedComponent.EntityInfos
{
    public class EmployeeInfo : Employee
    {
        [PropertyEntity("DepartmentName", false)]
        public string DepartmentName { get; set; }

        [PropertyEntity("OrganizationOfManagerName", false)]
        public string OrganizationOfManagerName { get; set; }

        public string GenderName
        {
            get
            {
                return this.Gender.HasValue && SMX.dicGender.ContainsKey(this.Gender) ? SMX.dicGender[this.Gender] : string.Empty;
            }
        }

        #region Properties used for approve process flow (committee)

        public string IsOrgManager { get { return !string.IsNullOrEmpty(OrganizationOfManagerName) ? "Có" : string.Empty; } }

        #endregion
    }
}
