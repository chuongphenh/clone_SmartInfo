using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public class ImageCatalog : BaseEntity
    {
        #region Primitive members

        public const string IC_Id = "Id";
        private int? _Id;
        [PropertyEntity(IC_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(IC_Id); }
        }

        public const string IC_CatalogName = "CatalogName";
        private string _CatalogName;
        [PropertyEntity(IC_CatalogName, false)]
        public string CatalogName
        {
            get { return _CatalogName; }
            set { _CatalogName = value; NotifyPropertyChanged(IC_CatalogName); }
        }

        public const string IC_ParentId = "ParentId";
        private int? _ParentId;
        [PropertyEntity(IC_ParentId, true)]
        public int? ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; NotifyPropertyChanged(IC_ParentId); }
        }

        public const string IC_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(IC_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(IC_CreatedBy); }
        }

        public const string IC_CreatedUserId = "CreatedUserId";
        private int? _CreatedUserId;
        [PropertyEntity(IC_CreatedUserId, true)]
        public int? CreatedUserId
        {
            get { return _CreatedUserId; }
            set { _CreatedUserId = value; NotifyPropertyChanged(IC_CreatedUserId); }
        }

        public const string IC_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(IC_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(IC_CreatedDTG); }
        }
        
        public const string IC_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(IC_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(IC_UpdatedBy); }
        }

        public const string IC_UpdatedDTG = "UpdatedDTG";
        private DateTime? _UpdatedDTG;
        [PropertyEntity(IC_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(IC_UpdatedDTG); }
        }

        public const string IC_refType = "refType";
        private int? _refType;
        [PropertyEntity(IC_refType, true)]
        public int? refType
        {
            get { return _refType; }
            set { _refType = value; NotifyPropertyChanged(IC_refType); }
        }

        #endregion

        public ImageCatalog() : base("ImageCatalog", "Id", string.Empty, string.Empty) { }
    }
    
}
