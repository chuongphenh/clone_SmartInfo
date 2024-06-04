using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_RelationsPressAgency : BaseEntity
    {
        public const string C_RelationsPressAgencyID = "RelationsPressAgencyID";
        private int? _RelationsPressAgencyID;
        [PropertyEntity(C_RelationsPressAgencyID, true)]
        public int? RelationsPressAgencyID
        {
            get { return _RelationsPressAgencyID; }
            set { _RelationsPressAgencyID = value; NotifyPropertyChanged(C_RelationsPressAgencyID); }
        }

        public const string C_PressAgencyID = "PressAgencyID";
        private int? _PressAgencyID;
        [PropertyEntity(C_PressAgencyID, false)]
        public int? PressAgencyID
        {
            get { return _PressAgencyID; }
            set { _PressAgencyID = value; NotifyPropertyChanged(C_PressAgencyID); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_Relationship = "Relationship";
        private string _Relationship;
        [PropertyEntity(C_Relationship, false)]
        public string Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; NotifyPropertyChanged(C_Relationship); }
        }

        public const string C_Note = "Note";
        private string _Note;
        [PropertyEntity(C_Note, false)]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; NotifyPropertyChanged(C_Note); }
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

        public agency_RelationsPressAgency() : base("agency_RelationsPressAgency", "RelationsPressAgencyID", "Deleted", "Version") { }

        public string TextSearch { get; set; }
    }
}