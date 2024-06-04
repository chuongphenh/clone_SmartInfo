using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class adm_AttachmentDetail : BaseEntity
    {
        #region Primitive members

        public const string C_AttachmentDetailID = "AttachmentDetailID";
        private int? _AttachmentDetailID;
        [PropertyEntity(C_AttachmentDetailID, true)]
        public int? AttachmentDetailID
        {
            get { return _AttachmentDetailID; }
            set { _AttachmentDetailID = value; NotifyPropertyChanged(C_AttachmentDetailID); }
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

        public const string C_RatioImageWidth = "RatioImageWidth";
        private decimal? _RatioImageWidth;
        [PropertyEntity(C_RatioImageWidth, false)]
        public decimal? RatioImageWidth
        {
            get { return _RatioImageWidth; }
            set { _RatioImageWidth = value; NotifyPropertyChanged(C_RatioImageWidth); }
        }

        public const string C_RatioImageHeight = "RatioImageHeight";
        private decimal? _RatioImageHeight;
        [PropertyEntity(C_RatioImageHeight, false)]
        public decimal? RatioImageHeight
        {
            get { return _RatioImageHeight; }
            set { _RatioImageHeight = value; NotifyPropertyChanged(C_RatioImageHeight); }
        }

        public adm_AttachmentDetail() : base("adm_AttachmentDetail", "AttachmentDetailID", string.Empty, string.Empty) { }
        #endregion
    }
}
