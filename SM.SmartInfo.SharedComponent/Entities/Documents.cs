using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Document : BaseEntity
    {
        #region Primitive members

        public const string C_DocumentID = "DocumentID";
        private int? _DocumentID;
        [PropertyEntity(C_DocumentID, true)]
        public int? DocumentID
        {
            get { return _DocumentID; }
            set { _DocumentID = value; NotifyPropertyChanged(C_DocumentID); }
        }

        public const string C_DocumentCode = "DocumentCode";
        private string _DocumentCode;
        [PropertyEntity(C_DocumentCode, false)]
        public string DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; NotifyPropertyChanged(C_DocumentCode); }
        }

        public const string C_DocumentName = "DocumentName";
        private string _DocumentName;
        [PropertyEntity(C_DocumentName, false)]
        public string DocumentName
        {
            get { return _DocumentName; }
            set { _DocumentName = value; NotifyPropertyChanged(C_DocumentName); }
        }

        public const string C_IssuerOrganizationID = "IssuerOrganizationID";
        private int? _IssuerOrganizationID;
        [PropertyEntity(C_IssuerOrganizationID, false)]
        public int? IssuerOrganizationID
        {
            get { return _IssuerOrganizationID; }
            set { _IssuerOrganizationID = value; NotifyPropertyChanged(C_IssuerOrganizationID); }
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

        public const string C_ReleaseDate = "ReleaseDate";
        private DateTime? _ReleaseDate;
        [PropertyEntity(C_ReleaseDate, false)]
        public DateTime? ReleaseDate
        {
            get { return _ReleaseDate; }
            set { _ReleaseDate = value; NotifyPropertyChanged(C_ReleaseDate); }
        }

        public Document() : base("Document", "DocumentID", "Deleted", "Version") { }

        #endregion
    }
}
