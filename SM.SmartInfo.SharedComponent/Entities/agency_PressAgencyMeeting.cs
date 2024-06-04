using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_PressAgencyMeeting : BaseEntity
    {
        public const string C_PressAgencyMeetingID = "PressAgencyMeetingID";
        private int? _PressAgencyMeetingID;
        [PropertyEntity(C_PressAgencyMeetingID, true)]
        public int? PressAgencyMeetingID
        {
            get { return _PressAgencyMeetingID; }
            set { _PressAgencyMeetingID = value; NotifyPropertyChanged(C_PressAgencyMeetingID); }
        }

        public const string C_PressAgencyID = "PressAgencyID";
        private int? _PressAgencyID;
        [PropertyEntity(C_PressAgencyID, false)]
        public int? PressAgencyID
        {
            get { return _PressAgencyID; }
            set { _PressAgencyID = value; NotifyPropertyChanged(C_PressAgencyID); }
        }

        public const string C_Partner = "Partner";
        private string _Partner;
        [PropertyEntity(C_Partner, false)]
        public string Partner
        {
            get { return _Partner; }
            set { _Partner = value; NotifyPropertyChanged(C_Partner); }
        }

        public const string C_Location = "Location";
        private string _Location;
        [PropertyEntity(C_Location, false)]
        public string Location
        {
            get { return _Location; }
            set { _Location = value; NotifyPropertyChanged(C_Location); }
        }

        public const string C_MeetDTG = "MeetDTG";
        private DateTime? _MeetDTG;
        [PropertyEntity(C_MeetDTG, false)]
        public DateTime? MeetDTG
        {
            get { return _MeetDTG; }
            set { _MeetDTG = value; NotifyPropertyChanged(C_MeetDTG); }
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

        public const string C_ContractNo = "ContractNo";
        private string _ContractNo;
        [PropertyEntity(C_ContractNo, false)]
        public string ContractNo
        {
            get { return _ContractNo; }
            set { _ContractNo = value; NotifyPropertyChanged(C_ContractNo); }
        }

        public const string C_ContractDTG = "ContractDTG";
        private DateTime? _ContractDTG;
        [PropertyEntity(C_ContractDTG, false)]
        public DateTime? ContractDTG
        {
            get { return _ContractDTG; }
            set { _ContractDTG = value; NotifyPropertyChanged(C_ContractDTG); }
        }

        public agency_PressAgencyMeeting() : base("agency_PressAgencyMeeting", "PressAgencyMeetingID", "Deleted", "Version") { }

        public DateTime? FromContractDTG { get; set; }

        public DateTime? ToContractDTG { get; set; }

        public DateTime? FromMeetDTG { get; set; }

        public DateTime? ToMeetDTG { get; set; }

        public string TextSearch { get; set; }
    }
}