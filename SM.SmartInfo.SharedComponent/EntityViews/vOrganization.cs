using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.EntityViews
{
    public partial class vOrganization : BaseEntity
    {
        #region Primitive Properties

        public const string C_PK = "PK";
        private int? _PK;
        [PropertyEntity(C_PK, false)]
        public int? PK
        {
            get { return _PK; }
            set { _PK = value; NotifyPropertyChanged(C_PK); }
        }

        public const string C_Code = "Code";
        private string _Code;
        [PropertyEntity(C_Code, false)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; NotifyPropertyChanged(C_Code); }
        }

        public const string C_OrganizationID = "OrganizationID";
        private int? _OrganizationID;
        [PropertyEntity(C_OrganizationID, false)]
        public int? OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; NotifyPropertyChanged(C_OrganizationID); }
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

        public const string C_ManagerID = "ManagerID";
        private string _ManagerID;
        [PropertyEntity(C_ManagerID, false)]
        public string ManagerID
        {
            get { return _ManagerID; }
            set { _ManagerID = value; NotifyPropertyChanged(C_ManagerID); }
        }

        public const string C_IsAMC = "IsAMC";
        private bool? _IsAMC;
        [PropertyEntity(C_IsAMC, false)]
        public bool? IsAMC
        {
            get { return _IsAMC; }
            set { _IsAMC = value; NotifyPropertyChanged(C_IsAMC); }
        }

        public const string C_Level = "Level";
        private int? _Level;
        [PropertyEntity(C_Level, false)]
        public int? Level
        {
            get { return _Level; }
            set { _Level = value; NotifyPropertyChanged(C_Level); }
        }

        public const string C_BreadCrumb = "BreadCrumb";
        private string _BreadCrumb;
        [PropertyEntity(C_BreadCrumb, false)]
        public string BreadCrumb
        {
            get { return _BreadCrumb; }
            set { _BreadCrumb = value; NotifyPropertyChanged(C_BreadCrumb); }
        }

        public vOrganization() : base("vOrganization", "OrganizationID", string.Empty, string.Empty) { }

        #endregion
    }
}
