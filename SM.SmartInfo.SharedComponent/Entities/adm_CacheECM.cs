using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class adm_CacheECM : BaseEntity
    {
        #region Primitive members

        public const string C_CacheECMID = "CacheECMID";
        private int? _CacheECMID;
        [PropertyEntity(C_CacheECMID, true)]
        public int? CacheECMID
        {
            get { return _CacheECMID; }
            set { _CacheECMID = value; NotifyPropertyChanged(C_CacheECMID); }
        }

        public const string C_AttachmentID = "AttachmentID";
        private int? _AttachmentID;
        [PropertyEntity(C_AttachmentID, false)]
        public int? AttachmentID
        {
            get { return _AttachmentID; }
            set { _AttachmentID = value; NotifyPropertyChanged(C_AttachmentID); }
        }

        public const string C_FileContent = "FileContent";
        private byte[] _FileContent;
        [PropertyEntity(C_FileContent, false)]
        public byte[] FileContent
        {
            get { return _FileContent; }
            set { _FileContent = value; NotifyPropertyChanged(C_FileContent); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public adm_CacheECM() : base("adm_CacheECM", "CacheECMID", string.Empty, string.Empty) { }

        #endregion
    }
}