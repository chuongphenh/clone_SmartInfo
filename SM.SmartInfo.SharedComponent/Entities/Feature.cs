using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Feature : BaseEntity
    {
        #region Primitive members

        public const string C_FeatureID = "FeatureID";
        private int? _FeatureID;
        [PropertyEntity(C_FeatureID, false)]
        public int? FeatureID
        {
            get { return _FeatureID; }
            set { _FeatureID = value; NotifyPropertyChanged(C_FeatureID); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_ParentID = "ParentID";
        private int? _ParentID;
        [PropertyEntity(C_ParentID, false)]
        public int? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; NotifyPropertyChanged(C_ParentID); }
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

        public const string C_DisplayOrder = "DisplayOrder";
        private int? _DisplayOrder;
        [PropertyEntity(C_DisplayOrder, false)]
        public int? DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; NotifyPropertyChanged(C_DisplayOrder); }
        }

        public const string C_Version = "Version";
        private int? _Version;
        [PropertyEntity(C_Version, false)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_CreatedOn = "CreatedOn";
        private string _CreatedOn;
        [PropertyEntity(C_CreatedOn, false)]
        public string CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; NotifyPropertyChanged(C_CreatedOn); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public const string C_UpdatedDTG = "UpdatedDTG";
        private DateTime? _UpdatedDTG;
        [PropertyEntity(C_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
        }

        public const string C_UpdatedOn = "UpdatedOn";
        private string _UpdatedOn;
        [PropertyEntity(C_UpdatedOn, false)]
        public string UpdatedOn
        {
            get { return _UpdatedOn; }
            set { _UpdatedOn = value; NotifyPropertyChanged(C_UpdatedOn); }
        }

        public const string C_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
        }

        public const string C_Deleted = "Deleted";
        private int? _Deleted;
        [PropertyEntity(C_Deleted, false)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
        }

        public const string C_URL = "URL";
        private string _URL;
        [PropertyEntity(C_URL, false)]
        public string URL
        {
            get { return _URL; }
            set { _URL = value; NotifyPropertyChanged(C_URL); }
        }

        public const string C_Level = "Level";
        private int? _Level;
        [PropertyEntity(C_Level, false)]
        public int? Level
        {
            get { return _Level; }
            set { _Level = value; NotifyPropertyChanged(C_Level); }
        }

        public const string C_OnMobile = "OnMobile";
        private int? _OnMobile;
        [PropertyEntity(C_OnMobile, false)]
        public int? OnMobile
        {
            get { return _OnMobile; }
            set { _OnMobile = value; NotifyPropertyChanged(C_OnMobile); }
        }

        public const string C_IsVisible = "IsVisible";
        private int? _IsVisible;
        [PropertyEntity(C_IsVisible, false)]
        public int? IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; NotifyPropertyChanged(C_IsVisible); }
        }

        public const string C_FeatureType = "FeatureType";
        private int? _FeatureType;
        [PropertyEntity(C_FeatureType, false)]
        public int? FeatureType
        {
            get { return _FeatureType; }
            set { _FeatureType = value; NotifyPropertyChanged(C_FeatureType); }
        }

        public Feature() : base("adm_Feature", "FeatureID", "Deleted", "Version") { }

        #endregion
    }
}
