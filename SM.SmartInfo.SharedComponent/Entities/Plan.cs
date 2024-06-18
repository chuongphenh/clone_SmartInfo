using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Plan : BaseEntity
    {
        #region Primitive members

        public const string C_PlanID = "PlanID";
        private int? _PlanID;
        [PropertyEntity(C_PlanID, true)]
        public int? PlanID
        {
            get { return _PlanID; }
            set { _PlanID = value; NotifyPropertyChanged(C_PlanID); }
        }

        public const string C_PlanCode = "PlanCode";
        private string _PlanCode;
        [PropertyEntity(C_PlanCode, false)]
        public string PlanCode
        {
            get { return _PlanCode; }
            set { _PlanCode = value; NotifyPropertyChanged(C_PlanCode); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_OrganizationName = "OrganizationName";
        private string _OrganizationName;
        [PropertyEntity(C_OrganizationName, false)]
        public string OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; NotifyPropertyChanged(C_OrganizationName); }
        }

        public const string C_StartDate = "StartDate";
        private DateTime? _StartDate;
        [PropertyEntity(C_StartDate, false)]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; NotifyPropertyChanged(C_StartDate); }
        }

        public const string C_EndDate = "EndDate";
        private DateTime? _EndDate;
        [PropertyEntity(C_EndDate, false)]
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; NotifyPropertyChanged(C_EndDate); }
        }

        public const string C_ReportCycle = "ReportCycle";
        private string _ReportCycle;
        [PropertyEntity(C_ReportCycle, false)]
        public string ReportCycle
        {
            get { return _ReportCycle; }
            set { _ReportCycle = value; NotifyPropertyChanged(C_ReportCycle); }
        }

        public const string C_ReportCycleType = "ReportCycleType";
        private int? _ReportCycleType;
        [PropertyEntity(C_ReportCycleType, false)]
        public int? ReportCycleType
        {
            get { return _ReportCycleType; }
            set { _ReportCycleType = value; NotifyPropertyChanged(C_ReportCycleType); }
        }

        public const string C_Description = "Description";
        private string _Description;
        [PropertyEntity(C_Description, false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged(C_Description); }
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

        public Plan() : base("Plan", "PlanID", "Deleted", "Version") { }

        #endregion
    }
}