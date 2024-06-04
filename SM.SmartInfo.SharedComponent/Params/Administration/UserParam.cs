using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Params.Administration
{
    public class UserParam : BaseParam
    {
        public UserParam(string functionType)
           : base(Constants.BusinessType.Administrations, functionType)
        {
        }
        public OrganizationEmployee OrganizationEmployee { get; set; }
        public List<EmployeeRole> EmployeeRoles { get; set; }
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; }
        public List<EmployeeInfo> EmployeeInfos { get; set; }
        public List<Role> Roles { get; set; }
        public SearchParam SearchParam { get; set; }
        public List<EmployeeRole> UserRoles { get; set; }
        public int? EmployeeId { get; set; }

        public string Code { get; set; }
        public string UserName { get; set; }

        public EmployeeImage EmployeeImage { get; set; }

        public List<string> ListBranchCode { get; set; }
        public string searchString { get; set; }

        public List<string> listEmailForSharing { get; set; }
        public int PositionId { get; set; }
        public int PressAgencyHRID { get; set; }
    }

    public class SearchParam
    {
        public string EmployeeID { get; set; }
        public string ManagerID { get; set; }
        public string IsLeader { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleID { get; set; }
    }
}
