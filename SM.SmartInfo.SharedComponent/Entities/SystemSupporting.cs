using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class SystemSupporting : BaseEntity
    {
        #region Primitive members

        public const string C_SystemSupportingID = "SystemSupportingID"; // 
        private int? _SystemSupportingID;
        [PropertyEntity(C_SystemSupportingID, true)]
        public int? SystemSupportingID
        {
            get { return _SystemSupportingID; }
            set { _SystemSupportingID = value; NotifyPropertyChanged(C_SystemSupportingID); }
        }

        public const string C_EmployeeID = "EmployeeID"; // 
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public const string C_RoleID = "RoleID"; // 
        private int? _RoleID;
        [PropertyEntity(C_RoleID, false)]
        public int? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; NotifyPropertyChanged(C_RoleID); }
        }

        public const string C_OrganizationListID = "OrganizationListID"; // 
        private string _OrganizationListID;
        [PropertyEntity(C_OrganizationListID, false)]
        public string OrganizationListID
        {
            get { return _OrganizationListID; }
            set { _OrganizationListID = value; NotifyPropertyChanged(C_OrganizationListID); }
        }

        public const string C_OrganizationListName = "OrganizationListName"; // 
        private string _OrganizationListName;
        [PropertyEntity(C_OrganizationListName, false)]
        public string OrganizationListName
        {
            get { return _OrganizationListName; }
            set { _OrganizationListName = value; NotifyPropertyChanged(C_OrganizationListName); }
        }

        public const string C_Deleted = "Deleted"; // 
        private int? _Deleted;
        [PropertyEntity(C_Deleted, false)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
        }

        public const string C_Version = "Version"; // 
        private int? _Version;
        [PropertyEntity(C_Version, false)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }

        public const string C_CreatedBy = "CreatedBy"; // 
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public const string C_CreatedDTG = "CreatedDTG"; // 
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_UpdatedBy = "UpdatedBy"; // 
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
        }

        public const string C_UpdatedDTG = "UpdatedDTG"; // 
        private DateTime? _UpdatedDTG;
        [PropertyEntity(C_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
        }

        public SystemSupporting() : base(string.Empty, string.Empty, string.Empty, string.Empty) { }

        #endregion

        #region Extend members
        [PropertyEntity("EmployeeName", false, false)]
        public string EmployeeName { get; set; }

        [PropertyEntity("RoleName", false, false)]
        public string RoleName { get; set; }
        #endregion
    }
}