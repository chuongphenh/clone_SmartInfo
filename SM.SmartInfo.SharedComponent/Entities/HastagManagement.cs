using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class HastagManagement : BaseEntity
    {
        #region Primitive members

        public const string C_HastagId = "HastagId";
        private int? _HastagId;
        [PropertyEntity(C_HastagId, true)]
        public int? HastagId
        {
            get { return _HastagId; }
            set { _HastagId = value; NotifyPropertyChanged(C_HastagId); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
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
        private DateTime _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public HastagManagement() : base("HastagId", "Name", string.Empty, string.Empty) { }

        #endregion
    }
}
