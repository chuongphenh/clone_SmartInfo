using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class EmployeeLog : BaseEntity
    {
        #region Primitive Properties

        public const string C_EmployeeLogID = "EmployeeLogID";
        private int? _EmployeeLogID;
        [PropertyEntity(C_EmployeeLogID, true)]
        public int? EmployeeLogID
        {
            get { return _EmployeeLogID; }
            set { _EmployeeLogID = value; NotifyPropertyChanged(C_EmployeeLogID); }
        }

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public const string C_SignInDTG = "SignInDTG";
        private DateTime? _SignInDTG;
        [PropertyEntity(C_SignInDTG, false)]
        public DateTime? SignInDTG
        {
            get { return _SignInDTG; }
            set { _SignInDTG = value; NotifyPropertyChanged(C_SignInDTG); }
        }

        public const string C_SignOutDTG = "SignOutDTG";
        private DateTime? _SignOutDTG;
        [PropertyEntity(C_SignOutDTG, false)]
        public DateTime? SignOutDTG
        {
            get { return _SignOutDTG; }
            set { _SignOutDTG = value; NotifyPropertyChanged(C_SignOutDTG); }
        }

        public const string C_IPAddress = "IPAddress";
        private string _IPAddress;
        [PropertyEntity(C_IPAddress, false)]
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; NotifyPropertyChanged(C_IPAddress); }
        }

        public EmployeeLog() : base("adm_EmployeeLog", "EmployeeLogID", "Deleted", "Version") { }
        #endregion
    }
}
