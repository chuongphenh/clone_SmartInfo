using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class Profile : BaseEntity
    {
        #region Primitive members

        public const string C_Id = "Id";
        private int? _Id;
        [PropertyEntity(C_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(C_Id); }
        }

        public const string C_Title = "Title";
        private string _Title;
        [PropertyEntity(C_Title, false)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; NotifyPropertyChanged(C_Title); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_PressAgencyHRId = "PressAgencyHRId";
        private int? _PressAgencyHRId;
        [PropertyEntity(C_PressAgencyHRId, false)]
        public int? PressAgencyHRId
        {
            get { return _PressAgencyHRId; }
            set { _PressAgencyHRId = value; NotifyPropertyChanged(C_PressAgencyHRId); }
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

        public Profile() : base("Profile", "Id", string.Empty, string.Empty) { }
        #endregion
    }
}
