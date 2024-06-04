using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.Utilities.Profiles;
using System.Collections.Generic;

namespace SM.SmartInfo.CacheManager
{
    public class UserProfile : IUserProfile
    {
        public int EmployeeID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string TokenKeyCloak { get; set; }

        public Employee Employee { get; set; }
        public List<Role> Roles { get; set; }

        public List<FunctionRight> EmployeeRights { get; set; }
        public List<Feature> Features { get; set; }
        public List<Function> Functions { get; set; }
        public List<FeatureFunction> FeatureFunctions { get; set; }

        public EmployeeLog EmployeeLog { get; set; }
        public System.DateTime? DataBaselinedDTG { get; set; }

        public string IPAddress { get; set; }

        /// <summary>
        /// Phong ho tro
        /// </summary>
        public SystemSupporting SystemSupporting { get; set; }

        /// <summary>
        /// Phong ma user lam chuyen vien
        /// </summary>
        public int? OrganizationID { get; set; }

        /// <summary>
        /// Phong
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// Danh sach cac phong ban so huu cua 1 truong phong. Bao gom phong truc thuoc va cac phong ban cap duoi.
        /// Khong bao gio null
        /// </summary>
        public List<int> ListAllManagingOrganizationID { get; set; }

        /// <summary>
        /// Danh sach cac phong ban so huu cua 1 truong phong. Khong bao gom phong truc thuoc va cac phong ban cap duoi.
        /// Khong bao gio null
        /// </summary>
        public List<int> ListDirectManagingOrganizationID { get; set; }

        /// <summary>
        /// Danh sach cac phong ma nguoi dung duoc phan quyen dieu phoi (khong null)
        /// </summary>
        public List<int> ListCoordinatorOrganizationID { get; set; }

        public List<string> ListFixedPermissionCode { get; set; }

        /// <summary>
        /// Loai mang client dung de ket noi (1 = LAN, 2 = WAN)
        /// </summary>
        public int ClientNetworkType { get; set; }

        public UserProfile() { }

        public string Language { get; set; }
        //{
        //    get
        //    {
        //        return;
        //        //throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //        //throw new System.NotImplementedException();
        //    }
        //}
    }
}
