using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_RelationshipWithMB : BaseEntity
    {
        public const string C_RelationshipWithMBID = "RelationshipWithMBID";
        private int? _RelationshipWithMBID;
        [PropertyEntity(C_RelationshipWithMBID, true)]
        public int? RelationshipWithMBID
        {
            get { return _RelationshipWithMBID; }
            set { _RelationshipWithMBID = value; NotifyPropertyChanged(C_RelationshipWithMBID); }
        }

        public const string C_PressAgencyID = "PressAgencyID";
        private int? _PressAgencyID;
        [PropertyEntity(C_PressAgencyID, false)]
        public int? PressAgencyID
        {
            get { return _PressAgencyID; }
            set { _PressAgencyID = value; NotifyPropertyChanged(C_PressAgencyID); }
        }

        public const string C_FromDTG = "FromDTG";
        private DateTime? _FromDTG;
        [PropertyEntity(C_FromDTG, false)]
        public DateTime? FromDTG
        {
            get { return _FromDTG; }
            set { _FromDTG = value; NotifyPropertyChanged(C_FromDTG); }
        }

        public const string C_ToDTG = "ToDTG";
        private DateTime? _ToDTG;
        [PropertyEntity(C_ToDTG, false)]
        public DateTime? ToDTG
        {
            get { return _ToDTG; }
            set { _ToDTG = value; NotifyPropertyChanged(C_ToDTG); }
        }

        public const string C_Relationship = "Relationship";
        private int? _Relationship;
        [PropertyEntity(C_Relationship, false)]
        public int? Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; NotifyPropertyChanged(C_Relationship); }
        }

        public agency_RelationshipWithMB() : base("agency_RelationshipWithMB", "RelationshipWithMBID", string.Empty, string.Empty) { }
    }
}