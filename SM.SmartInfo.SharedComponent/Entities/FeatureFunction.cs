using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class FeatureFunction : BaseEntity
    {
        #region Primitive members

        public const string C_FeatureFunctionID = "FeatureFunctionID";
        private int? _FeatureFunctionID;
        [PropertyEntity(C_FeatureFunctionID, true)]
        public int? FeatureFunctionID
        {
            get { return _FeatureFunctionID; }
            set { _FeatureFunctionID = value; NotifyPropertyChanged(C_FeatureFunctionID); }
        }

        public const string C_FeatureID = "FeatureID";
        private int? _FeatureID;
        [PropertyEntity(C_FeatureID, false)]
        public int? FeatureID
        {
            get { return _FeatureID; }
            set { _FeatureID = value; NotifyPropertyChanged(C_FeatureID); }
        }

        public const string C_FunctionID = "FunctionID";
        private int? _FunctionID;
        [PropertyEntity(C_FunctionID, false)]
        public int? FunctionID
        {
            get { return _FunctionID; }
            set { _FunctionID = value; NotifyPropertyChanged(C_FunctionID); }
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

        public const string C_URL = "URL";
        private string _URL;
        [PropertyEntity(C_URL, false)]
        public string URL
        {
            get { return _URL; }
            set { _URL = value; NotifyPropertyChanged(C_URL); }
        }

        public const string C_RuleID = "RuleID";
        private int? _RuleID;
        [PropertyEntity(C_RuleID, false)]
        public int? RuleID
        {
            get { return _RuleID; }
            set { _RuleID = value; NotifyPropertyChanged(C_RuleID); }
        }

        public FeatureFunction() : base("adm_FeatureFunction", "FeatureFunctionID", "Deleted", "Version") { }

        #endregion

        #region Extend members

        [PropertyEntity("FunctionName", false)]
        public string FunctionName { get; set; }

        #endregion
    }
}
