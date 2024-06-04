using SM.SmartInfo.SharedComponent.Entities;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Params.Permission
{
    public class AuthenticationParam : BaseParam
    {
        public AuthenticationParam(string functionType)
            : base(Constants.BusinessType.Authentication, functionType)
        {
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string DeviceName { get; set; }
        public string Guid { get; set; }
        public string UDID { get; set; }
        public Employee Employee { get; set; }

        public List<EmployeeLog> EmployeeLogs { get; set; }
        public List<Employee> Employees { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? FromDTG { get; set; }
        public DateTime? ToDTG { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public int? LogingAttemp { get; set; }
        public DateTime UnlockedTime { get; set; }
        public int StatusCode { get; set; }
    }
}
