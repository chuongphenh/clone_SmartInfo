using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class EmployeeRole : BaseEntity
    {
        #region Primitive members

        public const string C_EmployeeRoleID = "EmployeeRoleID";
        private int? _EmployeeRoleID;
        [PropertyEntity(C_EmployeeRoleID, true)]
        public int? EmployeeRoleID
        {
            get { return _EmployeeRoleID; }
            set { _EmployeeRoleID = value; NotifyPropertyChanged(C_EmployeeRoleID); }
        }

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public const string C_RoleID = "RoleID";
        private int? _RoleID;
        [PropertyEntity(C_RoleID, false)]
        public int? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; NotifyPropertyChanged(C_RoleID); }
        }

        public EmployeeRole() : base("adm_EmployeeRole", "EmployeeRoleID", "", "Version") { }

        #endregion
    }
}
