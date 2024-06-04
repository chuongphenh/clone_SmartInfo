using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_PressAgency : BaseEntity
    {
        #region Primitive members

        public const string C_PressAgencyID = "PressAgencyID";
        private int? _PressAgencyID;
        [PropertyEntity(C_PressAgencyID, true)]
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

        public const string C_Type = "Type";
        private int? _Type;
        [PropertyEntity(C_Type, false)]
        public int? Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
        }

        public const string C_TypeName = "TypeName";
        private string _TypeName;
        [PropertyEntity(C_TypeName, false)]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; NotifyPropertyChanged(C_TypeName); }
        }

        public const string C_EstablishedDTG = "EstablishedDTG";
        private DateTime? _EstablishedDTG;
        [PropertyEntity(C_EstablishedDTG, false)]
        public DateTime? EstablishedDTG
        {
            get { return _EstablishedDTG; }
            set { _EstablishedDTG = value; NotifyPropertyChanged(C_EstablishedDTG); }
        }

        public const string C_Agency = "Agency";
        private string _Agency;
        [PropertyEntity(C_Agency, false)]
        public string Agency
        {
            get { return _Agency; }
            set { _Agency = value; NotifyPropertyChanged(C_Agency); }
        }

        public const string C_CertNo = "CertNo";
        private string _CertNo;
        [PropertyEntity(C_CertNo, false)]
        public string CertNo
        {
            get { return _CertNo; }
            set { _CertNo = value; NotifyPropertyChanged(C_CertNo); }
        }

        public const string C_ChiefEditor = "ChiefEditor";
        private string _ChiefEditor;
        [PropertyEntity(C_ChiefEditor, false)]
        public string ChiefEditor
        {
            get { return _ChiefEditor; }
            set { _ChiefEditor = value; NotifyPropertyChanged(C_ChiefEditor); }
        }

        public const string C_Phone = "Phone";
        private string _Phone;
        [PropertyEntity(C_Phone, false)]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; NotifyPropertyChanged(C_Phone); }
        }

        public const string C_Fax = "Fax";
        private string _Fax;
        [PropertyEntity(C_Fax, false)]
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; NotifyPropertyChanged(C_Fax); }
        }

        public const string C_Email = "Email";
        private string _Email;
        [PropertyEntity(C_Email, false)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; NotifyPropertyChanged(C_Email); }
        }

        public const string C_Address = "Address";
        private string _Address;
        [PropertyEntity(C_Address, false)]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; NotifyPropertyChanged(C_Address); }
        }

        public const string C_Image = "Image";
        private byte[] _Image;
        [PropertyEntity(C_Image, false)]
        public byte[] Image
        {
            get { return _Image; }
            set { _Image = value; NotifyPropertyChanged(C_Image); }
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

        public const string C_Rate = "Rate";
        private string _Rate;
        [PropertyEntity(C_Rate, false)]
        public string Rate
        {
            get { return _Rate; }
            set { _Rate = value; NotifyPropertyChanged(C_Rate); }
        }

        public const string C_RelationshipWithMB = "RelationshipWithMB";
        private int? _RelationshipWithMB;
        [PropertyEntity(C_RelationshipWithMB, false)]
        public int? RelationshipWithMB
        {
            get { return _RelationshipWithMB; }
            set { _RelationshipWithMB = value; NotifyPropertyChanged(C_RelationshipWithMB); }
        }

        public const string C_Note = "Note";
        private string _Note;
        [PropertyEntity(C_Note, false)]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; NotifyPropertyChanged(C_Note); }
        }

        public const string C_DisplayOrder = "DisplayOrder";
        private int? _DisplayOrder;
        [PropertyEntity(C_DisplayOrder, false)]
        public int? DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; NotifyPropertyChanged(C_DisplayOrder); }
        }

        public agency_PressAgency() : base("agency_PressAgency", "PressAgencyID", "Deleted", "Version") { }

        #endregion

        #region Extend

        public adm_Attachment Attachment { get; set; }

        public int? CountHR { get; set; }

        public int? Attitude { get; set; }

        public DateTime? FromEstablishedDTG { get; set; }

        public DateTime? ToEstablishedDTG { get; set; }

        public string TextSearch { get; set; }

        [PropertyEntity("CountByType", false, false)]
        public int? CountByType { get; set; }

        #endregion
    }
}