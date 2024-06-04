using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class ImgCatalogManagement : BaseEntity
    {
        public const string C_Id = "Id";
        private int? _Id;
        [PropertyEntity(C_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(C_Id); }
        }

        public const string C_ImgId = "ImgId";
        private int? _ImgId;
        [PropertyEntity(C_ImgId, true)]
        public int? ImgId
        {
            get { return _ImgId; }
            set { _ImgId = value; NotifyPropertyChanged(C_ImgId); }
        }

        public const string C_CatalogId = "CatalogId";
        private int? _CatalogId;
        [PropertyEntity(C_CatalogId, true)]
        public int? CatalogId
        {
            get { return _CatalogId; }
            set { _CatalogId = value; NotifyPropertyChanged(C_CatalogId); }
        }

        public const string C_UserId = "UserId";
        private int? _UserId;
        [PropertyEntity(C_UserId, false)]
        public int? UserId
        {
            get { return _UserId; }
            set { _UserId = value; NotifyPropertyChanged(C_UserId); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_isDeleted = "isDeleted";
        private int? _isDeleted;
        [PropertyEntity(C_isDeleted, true)]
        public int? isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(C_isDeleted); }
        }

        public const string C_refType = "refType";
        private int? _refType;
        [PropertyEntity(C_refType, true)]
        public int? refType
        {
            get { return _refType; }
            set { _refType = value; NotifyPropertyChanged(C_refType); }
        }

        public ImgCatalogManagement() : base("ImgCatalogManagement", "Id", string.Empty, string.Empty) { }
    }
}
