using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class PlanTarget : BaseEntity
    {
        #region Primitive members

        public const string C_PlanTargetID = "PlanTargetID";
        private int? _PlanTargetID;
        [PropertyEntity(C_PlanTargetID, true)]
        public int? PlanTargetID
        {
            get { return _PlanTargetID; }
            set { _PlanTargetID = value; NotifyPropertyChanged(C_PlanTargetID); }
        }

        public const string C_PlanID = "PlanID";
        private int? _PlanID;
        [PropertyEntity(C_PlanID, false)]
        public int? PlanID
        {
            get { return _PlanID; }
            set { _PlanID = value; NotifyPropertyChanged(C_PlanID); }
        }

        public const string C_TargetID = "TargetID";
        private int? _TargetID;
        [PropertyEntity(C_TargetID, false)]
        public int? TargetID
        {
            get { return _TargetID; }
            set { _TargetID = value; NotifyPropertyChanged(C_TargetID); }
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

        public PlanTarget() : base("PlanTarget", "PlanTargetID", "Deleted", "Version") { }

        #endregion
    }
}