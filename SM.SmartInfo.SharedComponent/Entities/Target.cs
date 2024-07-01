using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Target : BaseEntity
    {
        #region Primitive members

        public const string C_TargetID = "TargetID";
        private int? _TargetID;
        [PropertyEntity(C_TargetID, true)]
        public int? TargetID
        {
            get { return _TargetID; }
            set { _TargetID = value; NotifyPropertyChanged(C_TargetID); }
        }

        public const string C_TargetCode = "TargetCode";
        private string _TargetCode;
        [PropertyEntity(C_TargetCode, false)]
        public string TargetCode
        {
            get { return _TargetCode; }
            set { _TargetCode = value; NotifyPropertyChanged(C_TargetCode); }
        }

        public const string C_TargetName = "TargetName";
        private string _TargetName;
        [PropertyEntity(C_TargetName, false)]
        public string TargetName
        {
            get { return _TargetName; }
            set { _TargetName = value; NotifyPropertyChanged(C_TargetName); }
        }

        public const string C_Version = "Version";
        private int? _Version;
        [PropertyEntity(C_Version, false)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }

        public const string C_Deleted = "Deleted";
        private int? _Deleted;
        [PropertyEntity(C_Deleted, false)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
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

        public Target() : base("Target", "TargetID", "Deleted", "Version") { }

        #endregion
    }
}