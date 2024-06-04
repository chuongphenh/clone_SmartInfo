using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class ntf_NotificationHistory : BaseEntity
    {
        public const string C_NotificationHistoryID = "NotificationHistoryID";
        private int? _NotificationHistoryID;
        [PropertyEntity(C_NotificationHistoryID, true)]
        public int? NotificationHistoryID
        {
            get { return _NotificationHistoryID; }
            set { _NotificationHistoryID = value; NotifyPropertyChanged(C_NotificationHistoryID); }
        }

        public const string C_RefID = "RefID";
        private int? _RefID;
        [PropertyEntity(C_RefID, false)]
        public int? RefID
        {
            get { return _RefID; }
            set { _RefID = value; NotifyPropertyChanged(C_RefID); }
        }

        public const string C_RefType = "RefType";
        private int? _RefType;
        [PropertyEntity(C_RefType, false)]
        public int? RefType
        {
            get { return _RefType; }
            set { _RefType = value; NotifyPropertyChanged(C_RefType); }
        }

        public const string C_SendDTG = "SendDTG";
        private DateTime? _SendDTG;
        [PropertyEntity(C_SendDTG, false)]
        public DateTime? SendDTG
        {
            get { return _SendDTG; }
            set { _SendDTG = value; NotifyPropertyChanged(C_SendDTG); }
        }

        public const string C_NumberOfSend = "NumberOfSend";
        private int? _NumberOfSend;
        [PropertyEntity(C_NumberOfSend, false)]
        public int? NumberOfSend
        {
            get { return _NumberOfSend; }
            set { _NumberOfSend = value; NotifyPropertyChanged(C_NumberOfSend); }
        }

        public ntf_NotificationHistory() : base("ntf_NotificationHistory", "NotificationHistoryID", string.Empty, string.Empty) { }
    }
}