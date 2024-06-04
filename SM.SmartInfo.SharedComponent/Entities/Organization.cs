using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public class Organization : BaseEntity
    {
        #region Primitive Properties

        public const string C_OrganizationID = "OrganizationID";
        private int? _OrganizationID;
        [PropertyEntity(C_OrganizationID, true)]
        public int? OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; NotifyPropertyChanged(C_OrganizationID); }
        }

        public const string C_ParentID = "ParentID";
        private int? _ParentID;
        [PropertyEntity(C_ParentID, false)]
        public int? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; NotifyPropertyChanged(C_ParentID); }
        }

        public const string C_CommitteeID = "CommitteeID";
        private int? _CommitteeID;
        [PropertyEntity(C_CommitteeID, false)]
        public int? CommitteeID
        {
            get { return _CommitteeID; }
            set { _CommitteeID = value; NotifyPropertyChanged(C_CommitteeID); }
        }

        public const string C_ZoneID = "ZoneID";
        private int? _ZoneID;
        [PropertyEntity(C_ZoneID, false)]
        public int? ZoneID
        {
            get { return _ZoneID; }
            set { _ZoneID = value; NotifyPropertyChanged(C_ZoneID); }
        }

        public const string C_RuleID = "RuleID";
        private int? _RuleID;
        [PropertyEntity(C_RuleID, false)]
        public int? RuleID
        {
            get { return _RuleID; }
            set { _RuleID = value; NotifyPropertyChanged(C_RuleID); }
        }

        public const string C_DispatchEmployeeRuleID = "DispatchEmployeeRuleID";
        private int? _DispatchEmployeeRuleID;
        [PropertyEntity(C_DispatchEmployeeRuleID, false)]
        public int? DispatchEmployeeRuleID
        {
            get { return _DispatchEmployeeRuleID; }
            set { _DispatchEmployeeRuleID = value; NotifyPropertyChanged(C_DispatchEmployeeRuleID); }
        }

        public const string C_Code = "Code";
        private string _Code;
        [PropertyEntity(C_Code, false)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; NotifyPropertyChanged(C_Code); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_Priority = "Priority";
        private int? _Priority;
        [PropertyEntity(C_Priority, false)]
        public int? Priority
        {
            get { return _Priority; }
            set { _Priority = value; NotifyPropertyChanged(C_Priority); }
        }

        public const string C_Province = "Province";
        private int? _Province;
        [PropertyEntity(C_Province, false)]
        public int? Province
        {
            get { return _Province; }
            set { _Province = value; NotifyPropertyChanged(C_Province); }
        }

        public const string C_Description = "Description";
        private string _Description;
        [PropertyEntity(C_Description, false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged(C_Description); }
        }

        public const string C_Type = "Type";
        private int? _Type;
        [PropertyEntity(C_Type, false)]
        public int? Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public const string C_UpdatedDTG = "UpdatedDTG";
        private DateTime? _UpdatedDTG;
        [PropertyEntity(C_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
        }

        public const string C_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
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

        public const string C_OfficeID = "OfficeID";
        private string _OfficeID;
        [PropertyEntity(C_OfficeID, false)]
        public string OfficeID
        {
            get { return _OfficeID; }
            set { _OfficeID = value; NotifyPropertyChanged(C_OfficeID); }
        }

        public const string C_AutomationType = "AutomationType";
        private int? _AutomationType;
        [PropertyEntity(C_AutomationType, false)]
        public int? AutomationType
        {
            get { return _AutomationType; }
            set { _AutomationType = value; NotifyPropertyChanged(C_AutomationType); }
        }

        public const string C_Address = "Address";
        private string _Address;
        [PropertyEntity(C_Address, false)]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; NotifyPropertyChanged(C_Address); }
        }

        public const string C_BranchEmail = "BranchEmail";
        private string _BranchEmail;
        [PropertyEntity(C_BranchEmail, false)]
        public string BranchEmail
        {
            get { return _BranchEmail; }
            set { _BranchEmail = value; NotifyPropertyChanged(C_BranchEmail); }
        }

        public Organization() : base("Organization", "OrganizationID", "Deleted", "Version") { }

        #endregion

        #region Extended
        [PropertyEntity("ManagerName", false, false)]
        public string ManagerName { get; set; }

        [PropertyEntity("AuthorizedName", false, false)]
        public string AuthorizedName { get; set; }

        [PropertyEntity("ZoneName", false, false)]
        public string ZoneName { get; set; }

        [PropertyEntity("ParentZoneID", false, false)]
        public int? ParentZoneID { get; set; }

        [PropertyEntity("OrganizationTypeName", false, false)]
        public string OrganizationTypeName { get; set; }

        [PropertyEntity("ManagerEmail", false, false)]
        public string ManagerEmail { get; set; }

        [PropertyEntity("OfficeName", false, false)]
        public string OfficeName { get; set; }

        [PropertyEntity("BreadCrumb", false, false)]
        public string BreadCrumb { get; set; }

        [PropertyEntity("RoleOrganizationEmployeeID", false, false)]
        public int? RoleOrganizationEmployeeID { get; set; }

        #region Committee information

        [PropertyEntity("Level", false, false)]
        public int? Level { get; set; }

        [PropertyEntity("CommitteeName", false, false)]
        public string CommitteeName { get; set; }

        [PropertyEntity("CommitteeCode", false, false)]
        public string CommitteeCode { get; set; }

        [PropertyEntity("CommitteeManagerID", false, false)]
        public string CommitteeManagerID { get; set; }

        [PropertyEntity("CommitteeType", false, false)]
        public int? CommitteeType { get; set; }

        [PropertyEntity("CommitteeAuthorizedID", false, false)]
        public string CommitteeAuthorizedID { get; set; }

        #endregion

        #endregion
    }
}