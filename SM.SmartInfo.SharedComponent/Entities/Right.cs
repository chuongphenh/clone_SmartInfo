using SoftMart.Core.Dao;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Right : BaseEntity
    {
        #region Primitive members

        public const string C_RightID = "RightID";
        private int? _RightID;
        [PropertyEntity(C_RightID, true)]
        public int? RightID
        {
            get { return _RightID; }
            set { _RightID = value; NotifyPropertyChanged(C_RightID); }
        }


        public const string C_RoleID = "RoleID";
        private int? _RoleID;
        [PropertyEntity(C_RoleID, false)]
        public int? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; NotifyPropertyChanged(C_RoleID); }
        }

        public const string C_FeatureFunctionID = "FeatureFunctionID";
        private int? _FeatureFunctionID;
        [PropertyEntity(C_FeatureFunctionID, false)]
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

        public const string C_HasPermission = "HasPermission";
        private bool? _HasPermission;
        [PropertyEntity(C_HasPermission, false)]
        public bool? HasPermission
        {
            get { return _HasPermission; }
            set { _HasPermission = value; NotifyPropertyChanged(C_HasPermission); }
        }

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public Right() : base("adm_Right", "RightID", "Deleted", "Version") { }

        #endregion


        #region Extends Members

        [PropertyEntity("RoleName", false, false)]
        public string RoleName { get; set; }

        [PropertyEntity("FeatureFunctions", false, false)]
        public List<FeatureFunction> FeatureFunctions { get; set; }

        [PropertyEntity("FeatureName", false, false)]
        public string FeatureName { get; set; }

        //[PropertyEntity("FeatureID", false, false)]
        //public int? FeatureID { get; set; }

        [PropertyEntity("UserName", false, false)]
        public string UserName { get; set; }

        [PropertyEntity("DisplayName", false, false)]
        public string DisplayName { get; set; }

        #endregion
    }

    public class BuildRightConfig
    {
        public int? ItemID { get; set; }
        public string Name { get; set; }
        public List<int?> FunctionIDs { get; set; }
    }
}
