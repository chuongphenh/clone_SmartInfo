using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class PositiveNews : BaseEntity
    {
        public const string C_PositiveNewsID = "PositiveNewsID";
        private int? _PositiveNewsID;
        [PropertyEntity(C_PositiveNewsID, true)]
        public int? PositiveNewsID
        {
            get { return _PositiveNewsID; }
            set { _PositiveNewsID = value; NotifyPropertyChanged(C_PositiveNewsID); }
        }

        public const string C_NewsID = "NewsID";
        private int? _NewsID;
        [PropertyEntity(C_NewsID, false)]
        public int? NewsID
        {
            get { return _NewsID; }
            set { _NewsID = value; NotifyPropertyChanged(C_NewsID); }
        }

        public const string C_CampaignID = "CampaignID";
        private int? _CampaignID;
        [PropertyEntity(C_CampaignID, false)]
        public int? CampaignID
        {
            get { return _CampaignID; }
            set { _CampaignID = value; NotifyPropertyChanged(C_CampaignID); }
        }

        public const string C_Type = "Type";
        private int? _Type;
        [PropertyEntity(C_Type, false)]
        public int? Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
        }

        public const string C_PressAgencryID = "PressAgencryID";
        private int? _PressAgencryID;
        [PropertyEntity(C_PressAgencryID, false)]
        public int? PressAgencryID
        {
            get { return _PressAgencryID; }
            set { _PressAgencryID = value; NotifyPropertyChanged(C_PressAgencryID); }
        }

        public const string C_PublishDTG = "PublishDTG";
        private DateTime? _PublishDTG;
        [PropertyEntity(C_PublishDTG, false)]
        public DateTime? PublishDTG
        {
            get { return _PublishDTG; }
            set { _PublishDTG = value; NotifyPropertyChanged(C_PublishDTG); }
        }

        public const string C_Title = "Title";
        private string _Title;
        [PropertyEntity(C_Title, false)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; NotifyPropertyChanged(C_Title); }
        }

        public const string C_Brief = "Brief";
        private string _Brief;
        [PropertyEntity(C_Brief, false)]
        public string Brief
        {
            get { return _Brief; }
            set { _Brief = value; NotifyPropertyChanged(C_Brief); }
        }

        public const string C_Url = "Url";
        private string _Url;
        [PropertyEntity(C_Url, false)]
        public string Url
        {
            get { return _Url; }
            set { _Url = value; NotifyPropertyChanged(C_Url); }
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

        public PositiveNews() : base("PositiveNews", "PositiveNewsID", "Deleted", "Version") { }

        [PropertyEntity("PressAgencyName", false, false)]
        public string PressAgencyName { get; set; }

        [PropertyEntity("CampaignName", false, false)]
        public string CampaignName { get; set; }

        public DateTime? FromPublishDTG { get; set; }

        public DateTime? ToPublishDTG { get; set; }

        public string SearchText { get; set; }
    }
}