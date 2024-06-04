using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class agency_PressAgencyHistory : BaseEntity
    {
        public const string C_PressAgencyHistoryID = "PressAgencyHistoryID";
        private int? _PressAgencyHistoryID;
        [PropertyEntity(C_PressAgencyHistoryID, true)]
        public int? PressAgencyHistoryID
        {
            get { return _PressAgencyHistoryID; }
            set { _PressAgencyHistoryID = value; NotifyPropertyChanged(C_PressAgencyHistoryID); }
        }

        public const string C_PressAgencyID = "PressAgencyID";
        private int? _PressAgencyID;
        [PropertyEntity(C_PressAgencyID, false)]
        public int? PressAgencyID
        {
            get { return _PressAgencyID; }
            set { _PressAgencyID = value; NotifyPropertyChanged(C_PressAgencyID); }
        }

        public const string C_PositionChange = "PositionChange";
        private string _PositionChange;
        [PropertyEntity(C_PositionChange, false)]
        public string PositionChange
        {
            get { return _PositionChange; }
            set { _PositionChange = value; NotifyPropertyChanged(C_PositionChange); }
        }

        public const string C_OldEmployee = "OldEmployee";
        private string _OldEmployee;
        [PropertyEntity(C_OldEmployee, false)]
        public string OldEmployee
        {
            get { return _OldEmployee; }
            set { _OldEmployee = value; NotifyPropertyChanged(C_OldEmployee); }
        }

        public const string C_NewEmployee = "NewEmployee";
        private string _NewEmployee;
        [PropertyEntity(C_NewEmployee, false)]
        public string NewEmployee
        {
            get { return _NewEmployee; }
            set { _NewEmployee = value; NotifyPropertyChanged(C_NewEmployee); }
        }

        public const string C_ChangeDTG = "ChangeDTG";
        private DateTime? _ChangeDTG;
        [PropertyEntity(C_ChangeDTG, false)]
        public DateTime? ChangeDTG
        {
            get { return _ChangeDTG; }
            set { _ChangeDTG = value; NotifyPropertyChanged(C_ChangeDTG); }
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

        public agency_PressAgencyHistory() : base("agency_PressAgencyHistory", "PressAgencyHistoryID", "Deleted", "Version") { }

        public DateTime? FromChangeDTG { get; set; }

        public DateTime? ToChangeDTG { get; set; }

        public string TextSearch { get; set; }
    }
}