using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Category : BaseEntity
    {
        #region Primitive members

        public const string C_CategoryID = "CategoryID";
        private int? _CategoryID;
        [PropertyEntity(C_CategoryID, true)]
        public int? CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; NotifyPropertyChanged(C_CategoryID); }
        }

        public const string C_CategoryName = "CategoryName";
        private string _CategoryName;
        [PropertyEntity(C_CategoryName, false)]
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; NotifyPropertyChanged(C_CategoryName); }
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

        public Category() : base("Category", "CategoryID", "Deleted", "Version") { }

        #endregion
    }
}
