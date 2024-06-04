using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Params.CommonList
{
    public class OrganizationParam : BaseParam
    {
        public OrganizationParam(string functionType)
            : base(Constants.BusinessType.Administrations, functionType)
        {
        }

        public int OrganizationID { get; set; }
        public Organization Organization { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<Committee> Committees { get; set; }
        //public Office Office { get; set; }
        public List<OrganizationType> OrganizationTypes { get; set; }
        public List<int> OrganizationIDs { get; set; }
        public List<int> EmployeeIDs { get; set; }
        public List<int> ManagerIDs { get; set; }
        public Employee Employee { get; set; }
        public List<Employee> ListEmployee { get; set; }
        public List<EmployeeInfo> EmployeeInfos { get; set; }
        public List<EmployeeInfo> ManagerInfos { get; set; }
        public List<SystemParameter> Zones { get; set; }
        public OrganizationEmployee OrganizationEmployee { get; set; }

        public string EmployeeID { get; set; }
        public string OfficeID { get; set; }
        public string OrganizationName { get; set; }
    }
}
