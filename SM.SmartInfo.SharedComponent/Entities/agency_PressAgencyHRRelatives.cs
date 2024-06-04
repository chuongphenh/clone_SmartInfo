using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_PressAgencyHRRelatives : BaseEntity
    {
        public const string C_PressAgencyHRRelativesID = "PressAgencyHRRelativesID";
        private int? _PressAgencyHRRelativesID;
        [PropertyEntity(C_PressAgencyHRRelativesID, true)]
        public int? PressAgencyHRRelativesID
        {
            get { return _PressAgencyHRRelativesID; }
            set { _PressAgencyHRRelativesID = value; NotifyPropertyChanged(C_PressAgencyHRRelativesID); }
        }

        public const string C_PressAgencyHRID = "PressAgencyHRID";
        private int? _PressAgencyHRID;
        [PropertyEntity(C_PressAgencyHRID, false)]
        public int? PressAgencyHRID
        {
            get { return _PressAgencyHRID; }
            set { _PressAgencyHRID = value; NotifyPropertyChanged(C_PressAgencyHRID); }
        }

        public const string C_FullName = "FullName";
        private string _FullName;
        [PropertyEntity(C_FullName, false)]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; NotifyPropertyChanged(C_FullName); }
        }

        public const string C_DOB = "DOB";
        private DateTime? _DOB;
        [PropertyEntity(C_DOB, false)]
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; NotifyPropertyChanged(C_DOB); }
        }

        public const string C_Hobby = "Hobby";
        private string _Hobby;
        [PropertyEntity(C_Hobby, false)]
        public string Hobby
        {
            get { return _Hobby; }
            set { _Hobby = value; NotifyPropertyChanged(C_Hobby); }
        }

        public const string C_OtherNote = "OtherNote";
        private string _OtherNote;
        [PropertyEntity(C_OtherNote, false)]
        public string OtherNote
        {
            get { return _OtherNote; }
            set { _OtherNote = value; NotifyPropertyChanged(C_OtherNote); }
        }

        public const string C_Relationship = "Relationship";
        private string _Relationship;
        [PropertyEntity(C_Relationship, false)]
        public string Relationship
        {
            get { return _Relationship; }
            set { _Relationship = value; NotifyPropertyChanged(C_Relationship); }
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

        public const string C_RelativeHRID = "RelativeHRID";
        private int? _RelativeHRID;
        [PropertyEntity(C_RelativeHRID, false)]
        public int? RelativeHRID
        {
            get { return _RelativeHRID; }
            set { _RelativeHRID = value; NotifyPropertyChanged(C_RelativeHRID); }
        }

        public agency_PressAgencyHRRelatives() : base("agency_PressAgencyHRRelatives", "PressAgencyHRRelativesID", "Deleted", "Version") { }

        #region Extent members

        [PropertyEntity("HRPosition", false, false)]
        public string HRPosition { get; set; }

        [PropertyEntity("HRFullName", false, false)]
        public string HRFullName { get; set; }

        [PropertyEntity("PressAgencyName", false, false)]
        public string PressAgencyName { get; set; }

        public DateTime? FromDOB{ get; set; }

        public DateTime? ToDOB { get; set; }

        public string TextSearch { get; set; }

        #endregion
    }
}