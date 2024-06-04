using SoftMart.Core.Dao;

namespace SM.SmartInfo.PermissionManager.Shared
{
    public class FixedBusinessPermission : BaseEntity
    {
        #region Primitive members

        public const string C_FixedBusinessPermissionID = "FixedBusinessPermissionID";
        private int? _FixedBusinessPermissionID;
        [PropertyEntity(C_FixedBusinessPermissionID, true)]
        public int? FixedBusinessPermissionID
        {
            get { return _FixedBusinessPermissionID; }
            set { _FixedBusinessPermissionID = value; NotifyPropertyChanged(C_FixedBusinessPermissionID); }
        }

        public const string C_Code = "Code";
        private string _Code;
        [PropertyEntity(C_Code, false)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; NotifyPropertyChanged(C_Code); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_RoleID = "RoleID";
        private int? _RoleID;
        [PropertyEntity(C_RoleID, false)]
        public int? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; NotifyPropertyChanged(C_RoleID); }
        }

        public FixedBusinessPermission() : base("adm_FixedBusinessPermission", "FixedBusinessPermissionID", "", "") { }

        #endregion
    }
}
