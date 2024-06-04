using System;
using System.Collections.Generic;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public partial class Employee : BaseEntity
    {
        #region Primitive members

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, true)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_Username = "Username";
        private string _Username;
        [PropertyEntity(C_Username, false)]
        public string Username
        {
            get { return _Username; }
            set { _Username = value; NotifyPropertyChanged(C_Username); }
        }

        public const string C_Password = "Password";
        private string _Password;
        [PropertyEntity(C_Password, false)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; NotifyPropertyChanged(C_Password); }
        }

        public const string C_Gender = "Gender";
        private int? _Gender;
        [PropertyEntity(C_Gender, false)]
        public int? Gender
        {
            get { return _Gender; }
            set { _Gender = value; NotifyPropertyChanged(C_Gender); }
        }
        //
        public const string C_DOB = "DOB";
        private DateTime? _DOB;
        [PropertyEntity(C_DOB, false)]
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; NotifyPropertyChanged(C_DOB); }
        }

        public const string C_Notes = "Notes";
        private string _Notes;
        [PropertyEntity(C_Notes, false)]
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; NotifyPropertyChanged(C_Notes); }
        }
        
        public const string C_UDID = "UDID";
        private string _UDID;
        [PropertyEntity(C_UDID, false)]
        public string UDID
        {
            get { return _UDID; }
            set { _UDID = value; NotifyPropertyChanged(C_UDID); }
        }

        public const string C_Status = "Status";
        private int? _Status;
        [PropertyEntity(C_Status, false)]
        public int? Status
        {
            get { return _Status; }
            set { _Status = value; NotifyPropertyChanged(C_Status); }
        }

        public const string C_Deleted = "Deleted";
        private int? _Deleted;
        [PropertyEntity(C_Deleted, false)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
        }

        public const string C_Version = "Version";
        private int? _Version;
        [PropertyEntity(C_Version, false)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
        }

        public const string C_UpdatedDTG = "UpdatedDTG";
        private DateTime? _UpdatedDTG;
        [PropertyEntity(C_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
        }

        public const string C_CreatedOn = "CreatedOn";
        private string _CreatedOn;
        [PropertyEntity(C_CreatedOn, false)]
        public string CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; NotifyPropertyChanged(C_CreatedOn); }
        }

        public const string C_Description = "Description";
        private string _Description;
        [PropertyEntity(C_Description, false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged(C_Description); }
        }

        public const string C_Email = "Email";
        private string _Email;
        [PropertyEntity(C_Email, false)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; NotifyPropertyChanged(C_Email); }
        }

        public const string C_Mobile = "Mobile";
        private string _Mobile;
        [PropertyEntity(C_Mobile, false)]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; NotifyPropertyChanged(C_Mobile); }
        }

        public const string C_Phone = "Phone";
        private string _Phone;
        [PropertyEntity(C_Phone, false)]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; NotifyPropertyChanged(C_Phone); }
        }

        public const string C_Sector = "Sector";
        private int? _Sector;
        [PropertyEntity(C_Sector, false)]
        public int? Sector
        {
            get { return _Sector; }
            set { _Sector = value; NotifyPropertyChanged(C_Sector); }
        }

        public const string C_EmployeeCode = "EmployeeCode";
        private string _EmployeeCode;
        [PropertyEntity(C_EmployeeCode, false)]
        public string EmployeeCode
        {
            get { return _EmployeeCode; }
            set { _EmployeeCode = value; NotifyPropertyChanged(C_EmployeeCode); }
        }

        public const string C_AuthorizationType = "AuthorizationType";
        private int? _AuthorizationType;
        [PropertyEntity(C_AuthorizationType, false)]
        public int? AuthorizationType
        {
            get { return _AuthorizationType; }
            set { _AuthorizationType = value; NotifyPropertyChanged(C_AuthorizationType); }
        }

        public const string C_LdapCnnName = "LdapCnnName";
        private string _LdapCnnName;
        [PropertyEntity(C_LdapCnnName, false)]
        public string LdapCnnName
        {
            get { return _LdapCnnName; }
            set { _LdapCnnName = value; NotifyPropertyChanged(C_LdapCnnName); }
        }

        public const string C_Level = "Level";
        private int? _Level;
        [PropertyEntity(C_Level, false)]
        public int? Level
        {
            get { return _Level; }
            set { _Level = value; NotifyPropertyChanged(C_Level); }
        }

        public const string C_IsLocked = "IsLocked";
        private bool? _IsLocked;
        [PropertyEntity(C_IsLocked, false)]
        public bool? IsLocked
        {
            get { return _IsLocked; }
            set { _IsLocked = value; NotifyPropertyChanged(C_IsLocked); }
        }

        public const string C_ListBranchCode = "ListBranchCode";
        private string _ListBranchCode;
        [PropertyEntity(C_ListBranchCode, false)]
        public string ListBranchCode
        {
            get { return _ListBranchCode; }
            set { _ListBranchCode = value; NotifyPropertyChanged(C_ListBranchCode); }
        }

        public const string C_IsManager = "IsManager";
        private bool? _IsManager;
        [PropertyEntity(C_IsManager, false)]
        public bool? IsManager
        {
            get { return _IsManager; }
            set { _IsManager = value; NotifyPropertyChanged(C_IsManager); }
        }
        public const string C_CIFCode = "CIFCode";
        private string _CIFCode;
        [PropertyEntity(C_CIFCode, false)]
        public string CIFCode
        {
            get { return _CIFCode; }
            set { _CIFCode = value; NotifyPropertyChanged(C_CIFCode); }
        }

        public const string C_DeviceName = "DeviceName";
        private string _DeviceName;
        [PropertyEntity(C_DeviceName, false)]
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; NotifyPropertyChanged(C_DeviceName); }
        }

        public const string C_Guid = "Guid";
        private string _Guid;
        [PropertyEntity(C_Guid, false)]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; NotifyPropertyChanged(C_Guid); }
        }

        public const string C_IsCheckFinger = "IsCheckFinger";
        private bool? _IsCheckFinger;
        [PropertyEntity(C_IsCheckFinger, false)]
        public bool? IsCheckFinger
        {
            get { return _IsCheckFinger; }
            set { _IsCheckFinger = value; NotifyPropertyChanged(C_IsCheckFinger); }
        }

        public const string C_IsLockByLogin = "IsLockByLogin";
        private int? _IsLockByLogin;
        [PropertyEntity(C_IsLockByLogin, true)]
        public int? IsLockByLogin
        {
            get { return _IsLockByLogin; }
            set { _IsLockByLogin = value; NotifyPropertyChanged(C_IsLockByLogin); }
        }

        public const string C_UnlockedTime = "UnlockedTime";
        private DateTime? _UnlockedTime;
        [PropertyEntity(C_UnlockedTime, false)]
        public DateTime? UnlockedTime
        {
            get { return _UnlockedTime; }
            set { _UnlockedTime = value; NotifyPropertyChanged(C_UnlockedTime); }
        }

        public const string C_LoggingAttemp = "LoggingAttemp";
        private int? _LoggingAttemp;
        [PropertyEntity(C_LoggingAttemp, true)]
        public int? LoggingAttemp
        {
            get { return _LoggingAttemp; }
            set { _LoggingAttemp = value; NotifyPropertyChanged(C_LoggingAttemp); }
        }

        public const string C_lastLoginDate = "lastLoginDate";
        private DateTime? _lastLoginDate;
        [PropertyEntity(C_lastLoginDate, false)]
        public DateTime? lastLoginDate
        {
            get { return _lastLoginDate; }
            set { _lastLoginDate = value; NotifyPropertyChanged(C_lastLoginDate); }
        }
        public Employee() : base("adm_Employee", "EmployeeID", "Deleted", "Version") { }
        #endregion

        #region Extend members
        [PropertyEntity("OrganizationID", false, false)]
        public int? OrganizationID { get; set; }

        [PropertyEntity("OrganizationName", false, false)]
        public string OrganizationName { get; set; }

        [PropertyEntity("OfficeName", false, false)]
        public string OfficeName { get; set; }

        [PropertyEntity("OrganizationBreadCrumb", false, false)]
        public string OrganizationBreadCrumb { get; set; }

        [PropertyEntity("PriorityType", false, false)]
        public int? PriorityType { get; set; }

        [PropertyEntity("ReceiveResultInBranchEmail", false, false)]
        public string ReceiveResultInBranchEmail { get; set; }

        [PropertyEntity("ZoneID", false, false)]
        public int? ZoneID { get; set; }

        [PropertyEntity("RoleName", false, false)]
        public string RoleName { get; set; }

        public string SectorName { get; set; }
        public string GenderName { get; set; }
        public string StatusName { get; set; }
        public string LevelName { get; set; }
        public string PositionName { get; set; }
        public string AuthorizationTypeName { get; set; }
        public adm_Attachment Attachment { get; set; }
        public List<string> NamePermissionGroups { get; set; }
        public int? ItemID
        {
            get
            {
                return EmployeeID;
            }
            set
            {
                EmployeeID = value;
            }
        }
        #endregion

        public System.Collections.Generic.List<int?> ListDeletedOrganizationEmployeeID { get; set; }
        public System.Collections.Generic.List<Organization> ListOrganizationEmployee { get; set; }
        public System.Collections.Generic.List<int?> ListDeletedOrganizationManagerID { get; set; }
        public System.Collections.Generic.List<Organization> ListOrganizationManager { get; set; }

        public System.Collections.Generic.List<int?> ListRoleID { get; set; }
        public System.Collections.Generic.List<int?> ListOrgID { get; set; }
    }
}