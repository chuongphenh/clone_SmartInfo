using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class OrganizationManager : BaseEntity
    {
        #region Primitive members

        public const string C_OrganizationManagerID = "OrganizationManagerID";
        private int? _OrganizationManagerID;
        [PropertyEntity(C_OrganizationManagerID, true)]
        public int? OrganizationManagerID
        {
            get { return _OrganizationManagerID; }
            set { _OrganizationManagerID = value; NotifyPropertyChanged(C_OrganizationManagerID); }
        }

        public const string C_OrganizationID = "OrganizationID";
        private int? _OrganizationID;
        [PropertyEntity(C_OrganizationID, false)]
        public int? OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; NotifyPropertyChanged(C_OrganizationID); }
        }

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
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

        public const string C_Deleted = "Deleted";
        private int? _Deleted;
        [PropertyEntity(C_Deleted, false)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
        }

        public OrganizationManager() : base("OrganizationManager", "OrganizationManagerID", "Deleted", string.Empty) { }

        #endregion

        #region Extend members

        #endregion
    }
}
