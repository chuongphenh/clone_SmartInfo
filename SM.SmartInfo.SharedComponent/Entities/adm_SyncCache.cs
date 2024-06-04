using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class adm_SyncCache : BaseEntity
    {
        #region Primitive members

        public const string C_SyncCacheID = "SyncCacheID";
        private int? _SyncCacheID;
        [PropertyEntity(C_SyncCacheID, true)]
        public int? SyncCacheID
        {
            get { return _SyncCacheID; }
            set { _SyncCacheID = value; NotifyPropertyChanged(C_SyncCacheID); }
        }

        public const string C_Type = "Type";
        private int? _Type;
        [PropertyEntity(C_Type, false)]
        public int? Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
        }

        public const string C_FeatureID = "FeatureID";
        private int? _FeatureID;
        [PropertyEntity(C_FeatureID, false)]
        public int? FeatureID
        {
            get { return _FeatureID; }
            set { _FeatureID = value; NotifyPropertyChanged(C_FeatureID); }
        }

        public const string C_LastUpdatedDTG = "LastUpdatedDTG";
        private DateTime? _LastUpdatedDTG;
        [PropertyEntity(C_LastUpdatedDTG, false)]
        public DateTime? LastUpdatedDTG
        {
            get { return _LastUpdatedDTG; }
            set { _LastUpdatedDTG = value; NotifyPropertyChanged(C_LastUpdatedDTG); }
        }

        public const string C_Version = "Version";
        private int? _Version;
        [PropertyEntity(C_Version, false)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }

        public adm_SyncCache() : base("adm_SyncCache", "SyncCacheID", string.Empty, "") { }
        #endregion
    }
}
