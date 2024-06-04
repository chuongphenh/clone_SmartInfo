using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_PressAgencyHR : BaseEntity
    {
        #region Primitive members

        public const string C_PressAgencyHRID = "PressAgencyHRID";
        private int? _PressAgencyHRID;
        [PropertyEntity(C_PressAgencyHRID, true)]
        public int? PressAgencyHRID
        {
            get { return _PressAgencyHRID; }
            set { _PressAgencyHRID = value; NotifyPropertyChanged(C_PressAgencyHRID); }
        }

        public const string C_PressAgencyID = "PressAgencyID";
        private int? _PressAgencyID;
        [PropertyEntity(C_PressAgencyID, false)]
        public int? PressAgencyID
        {
            get { return _PressAgencyID; }
            set { _PressAgencyID = value; NotifyPropertyChanged(C_PressAgencyID); }
        }

        public const string C_FullName = "FullName";
        private string _FullName;
        [PropertyEntity(C_FullName, false)]
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; NotifyPropertyChanged(C_FullName); }
        }

        public const string C_Position = "Position";
        private string _Position;
        [PropertyEntity(C_Position, false)]
        public string Position
        {
            get { return _Position; }
            set { _Position = value; NotifyPropertyChanged(C_Position); }
        }

        public const string C_DOB = "DOB";
        private DateTime? _DOB;
        [PropertyEntity(C_DOB, false)]
        public DateTime? DOB
        {
            get { return _DOB; }
            set { _DOB = value; NotifyPropertyChanged(C_DOB); }
        }

        public const string C_Mobile = "Mobile";
        private string _Mobile;
        [PropertyEntity(C_Mobile, false)]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; NotifyPropertyChanged(C_Mobile); }
        }

        public const string C_Address = "Address";
        private string _Address;
        [PropertyEntity(C_Address, false)]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; NotifyPropertyChanged(C_Address); }
        }

        public const string C_Email = "Email";
        private string _Email;
        [PropertyEntity(C_Email, false)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; NotifyPropertyChanged(C_Email); }
        }

        public const string C_Hobby = "Hobby";
        private string _Hobby;
        [PropertyEntity(C_Hobby, false)]
        public string Hobby
        {
            get { return _Hobby; }
            set { _Hobby = value; NotifyPropertyChanged(C_Hobby); }
        }

        public const string C_RelatedInformation = "RelatedInformation";
        private string _RelatedInformation;
        [PropertyEntity(C_RelatedInformation, false)]
        public string RelatedInformation
        {
            get { return _RelatedInformation; }
            set { _RelatedInformation = value; NotifyPropertyChanged(C_RelatedInformation); }
        }

        public const string C_Attitude = "Attitude";
        private int? _Attitude;
        [PropertyEntity(C_Attitude, false)]
        public int? Attitude
        {
            get { return _Attitude; }
            set { _Attitude = value; NotifyPropertyChanged(C_Attitude); }
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

        public agency_PressAgencyHR() : base("agency_PressAgencyHR", "PressAgencyHRID", "Deleted", "Version") { }

        #endregion

        #region Extent members

        public adm_Attachment Attachment { get; set; }

        [PropertyEntity("PressAgencyName", false, false)]
        public string PressAgencyName { get; set; }

        [PropertyEntity("PressAgencyType", false, false)]
        public int? PressAgencyType { get; set; }

        [PropertyEntity("PressAgencyTypeString", false, false)]
        public string PressAgencyTypeString { get; set; }

        [PropertyEntity("CountByType", false, false)]
        public int? CountByType { get; set; }

        public DateTime? FromDOB { get; set; }

        public DateTime? ToDOB { get; set; }

        public string TextSearch { get; set; }
        public string TypeName { get; set; }
        [PropertyEntity("PermissionGroupName", false, false)]
        public string PermissionGroupName { get; set; }
        public List<string> NamePermissionGroups { get; set; }

        #endregion
    }
}