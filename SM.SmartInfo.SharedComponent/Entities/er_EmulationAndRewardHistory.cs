using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class er_EmulationAndRewardHistory : BaseEntity
    {
        public const string C_EmulationAndRewardHistoryID = "EmulationAndRewardHistoryID";
        private int? _EmulationAndRewardHistoryID;
        [PropertyEntity(C_EmulationAndRewardHistoryID, true)]
        public int? EmulationAndRewardHistoryID
        {
            get { return _EmulationAndRewardHistoryID; }
            set { _EmulationAndRewardHistoryID = value; NotifyPropertyChanged(C_EmulationAndRewardHistoryID); }
        }

        public const string C_EmulationAndRewardSubjectID = "EmulationAndRewardSubjectID";
        private int? _EmulationAndRewardSubjectID;
        [PropertyEntity(C_EmulationAndRewardSubjectID, false)]
        public int? EmulationAndRewardSubjectID
        {
            get { return _EmulationAndRewardSubjectID; }
            set { _EmulationAndRewardSubjectID = value; NotifyPropertyChanged(C_EmulationAndRewardSubjectID); }
        }

        public const string C_EmulationAndRewardID = "EmulationAndRewardID";
        private int? _EmulationAndRewardID;
        [PropertyEntity(C_EmulationAndRewardID, false)]
        public int? EmulationAndRewardID
        {
            get { return _EmulationAndRewardID; }
            set { _EmulationAndRewardID = value; NotifyPropertyChanged(C_EmulationAndRewardID); }
        }

        public const string C_Title = "Title";
        private string _Title;
        [PropertyEntity(C_Title, false)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; NotifyPropertyChanged(C_Title); }
        }

        public const string C_EmulationAndRewardUnit = "EmulationAndRewardUnit";
        private string _EmulationAndRewardUnit;
        [PropertyEntity(C_EmulationAndRewardUnit, false)]
        public string EmulationAndRewardUnit
        {
            get { return _EmulationAndRewardUnit; }
            set { _EmulationAndRewardUnit = value; NotifyPropertyChanged(C_EmulationAndRewardUnit); }
        }

        public const string C_RewardedDTG = "RewardedDTG";
        private DateTime? _RewardedDTG;
        [PropertyEntity(C_RewardedDTG, false)]
        public DateTime? RewardedDTG
        {
            get { return _RewardedDTG; }
            set { _RewardedDTG = value; NotifyPropertyChanged(C_RewardedDTG); }
        }

        public const string C_AwardingType = "AwardingType";
        private string _AwardingType;
        [PropertyEntity(C_AwardingType, false)]
        public string AwardingType
        {
            get { return _AwardingType; }
            set { _AwardingType = value; NotifyPropertyChanged(C_AwardingType); }
        }
        public er_EmulationAndRewardHistory() : base("er_EmulationAndRewardHistory", "EmulationAndRewardHistoryID", string.Empty, string.Empty) { }
    }
}