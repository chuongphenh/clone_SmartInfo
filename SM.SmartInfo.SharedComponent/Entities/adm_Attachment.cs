using System;
using SoftMart.Core.Dao;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public class adm_Attachment : BaseEntity
    {
        #region Primitive members

        public const string C_AttachmentID = "AttachmentID";
        private int? _AttachmentID;
        [PropertyEntity(C_AttachmentID, true)]
        public int? AttachmentID
        {
            get { return _AttachmentID; }
            set { _AttachmentID = value; NotifyPropertyChanged(C_AttachmentID); }
        }

        public const string C_FileName = "FileName";
        private string _FileName;
        [PropertyEntity(C_FileName, false)]
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; NotifyPropertyChanged(C_FileName); }
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

        public const string C_Status = "Status";
        private int? _Status;
        [PropertyEntity(C_Status, false)]
        public int? Status
        {
            get { return _Status; }
            set { _Status = value; NotifyPropertyChanged(C_Status); }
        }

        public const string C_Description = "Description";
        private string _Description;
        [PropertyEntity(C_Description, false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged(C_Description); }
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

        public const string C_RefCode = "RefCode";
        private string _RefCode;
        [PropertyEntity(C_RefCode, false)]
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; NotifyPropertyChanged(C_RefCode); }
        }

        public const string C_FileSize = "FileSize";
        private int? _FileSize;
        [PropertyEntity(C_FileSize, false)]
        public int? FileSize
        {
            get { return _FileSize; }
            set { _FileSize = value; NotifyPropertyChanged(C_FileSize); }
        }

        public const string C_DisplayName = "DisplayName";
        private string _DisplayName;
        [PropertyEntity(C_DisplayName, false)]
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; NotifyPropertyChanged(C_DisplayName); }
        }

        public const string C_ContentType = "ContentType";
        private string _ContentType;
        [PropertyEntity(C_ContentType, false)]
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; NotifyPropertyChanged(C_ContentType); }
        }

        public const string C_ECMItemID = "ECMItemID";
        private string _ECMItemID;
        [PropertyEntity(C_ECMItemID, false)]
        public string ECMItemID
        {
            get { return _ECMItemID; }
            set { _ECMItemID = value; NotifyPropertyChanged(C_ECMItemID); }
        }

        public const string C_CustomerIDCard = "CustomerIDCard";
        private string _CustomerIDCard;
        [PropertyEntity(C_CustomerIDCard, false)]
        public string CustomerIDCard
        {
            get { return _CustomerIDCard; }
            set { _CustomerIDCard = value; NotifyPropertyChanged(C_CustomerIDCard); }
        }

        public adm_Attachment() : base("adm_Attachment", "AttachmentID", string.Empty, string.Empty) { }

        #endregion

        #region Extent members

        [PropertyEntity("FileContent", false, false)]
        public byte[] FileContent { get; set; }

        [PropertyEntity("FullNameCreateBy", false, false)]
        public string FullNameCreateBy { get; set; }

        [PropertyEntity("DocumentName", false, false)]
        public string DocumentName { get; set; }

        [PropertyEntity("ImageBase64String", false, false)]
        public string ImageBase64String { get; set; }

        public string TextSearch { get; set; }

        public string FileURL { get; set; }

        #endregion
    }
}