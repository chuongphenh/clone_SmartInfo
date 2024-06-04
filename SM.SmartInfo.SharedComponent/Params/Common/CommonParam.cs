using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.SharedComponent.Params.Common
{
    public class CommonParam : BaseParam
    {
        public CommonParam(string functionType)
            : base(Constants.BusinessType.Commons, functionType)
        {
        }

        public EmployeeSelectorMode EmployeeSelectorMode { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<Employee> Employees { get; set; }
        public EmployeeInfo EmployeeInfo { get; set; }
        public string SelectorModeType { get; set; }

        public OrganizationSelectorTreeMode OrganizationSelectorTreeMode { get; set; }
        public int? OrganizationID { get; set; }
        public int? OrganizationType { get; set; }

        public string OrganizationName { get; set; }

        public int? ProvinceId { get; set; }

        public List<int> RoleIDs { get; set; }

        public Organization Organization { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<News> ListSuVu { get; set; }
        public List<News> ListTinTuc { get; set; }
        public List<ntf_Notification> ListNotification { get; set; }
        public List<agency_PressAgency> ListPressAgency { get; set; }

        public string ListStringOrganizationID { get; set; }

        public int? ZoneId { set; get; }

        public int? ImportingType { get; set; }

        public string SearchText { get; set; }

        public int? SearchType { get; set; }

        public int? TypeTime { get; set; }
    }
}