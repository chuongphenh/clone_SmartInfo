using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_PressAgencyHRHistory : BaseEntity
    {
        public const string C_PressAgencyHRHistoryID = "PressAgencyHRHistoryID";
        private int? _PressAgencyHRHistoryID;
        [PropertyEntity(C_PressAgencyHRHistoryID, true)]
        public int? PressAgencyHRHistoryID
        {
            get { return _PressAgencyHRHistoryID; }
            set { _PressAgencyHRHistoryID = value; NotifyPropertyChanged(C_PressAgencyHRHistoryID); }
        }

        public const string C_PressAgencyHRID = "PressAgencyHRID";
        private int? _PressAgencyHRID;
        [PropertyEntity(C_PressAgencyHRID, false)]
        public int? PressAgencyHRID
        {
            get { return _PressAgencyHRID; }
            set { _PressAgencyHRID = value; NotifyPropertyChanged(C_PressAgencyHRID); }
        }

        public const string C_MeetedDTG = "MeetedDTG";
        private DateTime? _MeetedDTG;
        [PropertyEntity(C_MeetedDTG, false)]
        public DateTime? MeetedDTG
        {
            get { return _MeetedDTG; }
            set { _MeetedDTG = value; NotifyPropertyChanged(C_MeetedDTG); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
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

        public agency_PressAgencyHRHistory() : base("agency_PressAgencyHRHistory", "PressAgencyHRHistoryID", "Deleted", "Version") { }

        public DateTime? FromMeetDTG { get; set; }

        public DateTime? ToMeetDTG { get; set; }

        public string TextSearch { get; set; }
    }
}