using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class er_EmulationAndRewardSubject : BaseEntity
    {
        public const string C_EmulationAndRewardSubjectID = "EmulationAndRewardSubjectID";
        private int? _EmulationAndRewardSubjectID;
        [PropertyEntity(C_EmulationAndRewardSubjectID, true)]
        public int? EmulationAndRewardSubjectID
        {
            get { return _EmulationAndRewardSubjectID; }
            set { _EmulationAndRewardSubjectID = value; NotifyPropertyChanged(C_EmulationAndRewardSubjectID); }
        }

        public const string C_EmulationAndRewardID = "EmulationAndRewardID";
        private string _EmulationAndRewardID;
        [PropertyEntity(C_EmulationAndRewardID, false)]
        public string EmulationAndRewardID
        {
            get { return _EmulationAndRewardID; }
            set { _EmulationAndRewardID = value; NotifyPropertyChanged(C_EmulationAndRewardID); }
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

        public const string C_LatestTitle = "LatestTitle";
        private string _LatestTitle;
        [PropertyEntity(C_LatestTitle, false)]
        public string LatestTitle
        {
            get { return _LatestTitle; }
            set { _LatestTitle = value; NotifyPropertyChanged(C_LatestTitle); }
        }

        public const string C_LatestEmulationAndRewardUnit = "LatestEmulationAndRewardUnit";
        private string _LatestEmulationAndRewardUnit;
        [PropertyEntity(C_LatestEmulationAndRewardUnit, false)]
        public string LatestEmulationAndRewardUnit
        {
            get { return _LatestEmulationAndRewardUnit; }
            set { _LatestEmulationAndRewardUnit = value; NotifyPropertyChanged(C_LatestEmulationAndRewardUnit); }
        }

        public const string C_Unit = "Unit";
        private string _Unit;
        [PropertyEntity(C_Unit, false)]
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; NotifyPropertyChanged(C_Unit); }
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

        public const string C_Type = "Type";
        private int? _Type;
        [PropertyEntity(C_Type, false)]
        public int? Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
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

        public er_EmulationAndRewardSubject() : base("er_EmulationAndRewardSubject", "EmulationAndRewardSubjectID", "Deleted", "Version") { }

        [PropertyEntity("EmulationAndRewardUnit", false, false)]
        public string EmulationAndRewardUnit { get; set; }
    }
}