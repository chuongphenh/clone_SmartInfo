using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public class er_EmulationAndReward : BaseEntity
    {
        public const string C_EmulationAndRewardID = "EmulationAndRewardID";
        private int? _EmulationAndRewardID;
        [PropertyEntity(C_EmulationAndRewardID, true)]
        public int? EmulationAndRewardID
        {
            get { return _EmulationAndRewardID; }
            set { _EmulationAndRewardID = value; NotifyPropertyChanged(C_EmulationAndRewardID); }
        }

        public const string C_Year = "Year";
        private int? _Year;
        [PropertyEntity(C_Year, false)]
        public int? Year
        {
            get { return _Year; }
            set { _Year = value; NotifyPropertyChanged(C_Year); }
        }

        public const string C_Event = "Event";
        private string _Event;
        [PropertyEntity(C_Event, false)]
        public string Event
        {
            get { return _Event; }
            set { _Event = value; NotifyPropertyChanged(C_Event); }
        }

        public const string C_EmulationAndRewardUnit = "EmulationAndRewardUnit";
        private string _EmulationAndRewardUnit;
        [PropertyEntity(C_EmulationAndRewardUnit, false)]
        public string EmulationAndRewardUnit
        {
            get { return _EmulationAndRewardUnit; }
            set { _EmulationAndRewardUnit = value; NotifyPropertyChanged(C_EmulationAndRewardUnit); }
        }

        public const string C_SubjectRewarded = "SubjectRewarded";
        private int? _SubjectRewarded;
        [PropertyEntity(C_SubjectRewarded, false)]
        public int? SubjectRewarded
        {
            get { return _SubjectRewarded; }
            set { _SubjectRewarded = value; NotifyPropertyChanged(C_SubjectRewarded); }
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

        public const string C_PeriodId = "PeriodId";
        private int? _PeriodId;
        [PropertyEntity(C_PeriodId, false)]
        public int? PeriodId
        {
            get { return _PeriodId; }
            set { _PeriodId = value; NotifyPropertyChanged(C_PeriodId); }
        }

        public const string C_AwardingTypeId = "AwardingTypeId";
        private int? _AwardingTypeId;
        [PropertyEntity(C_AwardingTypeId, false)]
        public int? AwardingTypeId
        {
            get { return _AwardingTypeId; }
            set { _AwardingTypeId = value; NotifyPropertyChanged(C_AwardingTypeId); }
        }

        public er_EmulationAndReward() : base("er_EmulationAndReward", "EmulationAndRewardID", "Deleted", "Version") { }

        [PropertyEntity("SubjectTypeCount", false, false)]
        public int? SubjectTypeCount { get; set; }

        public string TextSearch { get; set; }

        [PropertyEntity("SubjectType", false, false)]
        public int? SubjectType { get; set; }
    }
}