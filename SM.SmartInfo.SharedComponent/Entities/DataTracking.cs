using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class DataTracking : BaseEntity
    {
        #region Primitive members

        public const string C_DataTrackingID = "DataTrackingID";
        private int? _DataTrackingID;
        [PropertyEntity(C_DataTrackingID, true)]
        public int? DataTrackingID
        {
            get { return _DataTrackingID; }
            set { _DataTrackingID = value; NotifyPropertyChanged(C_DataTrackingID); }
        }

        public const string C_Feature = "Feature";
        private int? _Feature;
        [PropertyEntity(C_Feature, false)]
        public int? Feature
        {
            get { return _Feature; }
            set { _Feature = value; NotifyPropertyChanged(C_Feature); }
        }

        public const string C_UserID = "UserID";
        private int? _UserID;
        [PropertyEntity(C_UserID, false)]
        public int? UserID
        {
            get { return _UserID; }
            set { _UserID = value; NotifyPropertyChanged(C_UserID); }
        }

        public const string C_ActionDTG = "ActionDTG";
        private DateTime? _ActionDTG;
        [PropertyEntity(C_ActionDTG, false)]
        public DateTime? ActionDTG
        {
            get { return _ActionDTG; }
            set { _ActionDTG = value; NotifyPropertyChanged(C_ActionDTG); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_ActionType = "ActionType";
        private int? _ActionType;
        [PropertyEntity(C_ActionType, false)]
        public int? ActionType
        {
            get { return _ActionType; }
            set { _ActionType = value; NotifyPropertyChanged(C_ActionType); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_OrganizationID = "OrganizationID";
        private int? _OrganizationID;
        [PropertyEntity(C_OrganizationID, false)]
        public int? OrganizationID
        {
            get { return _OrganizationID; }
            set { _OrganizationID = value; NotifyPropertyChanged(C_OrganizationID); }
        }

        public const string C_RefType = "RefType";
        private int? _RefType;
        [PropertyEntity(C_RefType, false)]
        public int? RefType
        {
            get { return _RefType; }
            set { _RefType = value; NotifyPropertyChanged(C_RefType); }
        }

        public const string C_RefID = "RefID";
        private int? _RefID;
        [PropertyEntity(C_RefID, false)]
        public int? RefID
        {
            get { return _RefID; }
            set { _RefID = value; NotifyPropertyChanged(C_RefID); }
        }

        public DataTracking() : base("DataTracking", "DataTrackingID", string.Empty, string.Empty) { }

        #endregion

        #region Extend members

        [PropertyEntity("StartDTG", false, false)]
        public DateTime? StartDTG { get; set; }

        [PropertyEntity("EndDTG", false, false)]
        public DateTime? EndDTG { get; set; }

        [PropertyEntity("UserName", false, false)]
        public string UserName { get; set; }

        [PropertyEntity("FeatureName", false, false)]
        public string FeatureName { get; set; }

        [PropertyEntity("OrganizationName", false, false)]
        public string OrganizationName { get; set; }

        [PropertyEntity("ManagementOrganizationName", false, false)]
        public string ManagementOrganizationName { get; set; }
        #endregion
    }
}