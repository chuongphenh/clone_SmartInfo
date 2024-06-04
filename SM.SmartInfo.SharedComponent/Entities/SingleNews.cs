using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class SingleNews: BaseEntity
    {
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

        public const string C_PostedDate = "PostedDate";
        private DateTime? _PostedDate;
        [PropertyEntity(C_PostedDate, false)]
        public DateTime? PostedDate
        {
            get { return _PostedDate; }
            set { _PostedDate = value; NotifyPropertyChanged(C_PostedDate); }
        }

        public const string C_Chanel = "Chanel";
        private string _Chanel;
        [PropertyEntity(C_Chanel, false)]
        public string Chanel
        {
            get { return _Chanel; }
            set { _Chanel = value; NotifyPropertyChanged(C_Chanel); }
        }

        public const string C_Summary = "Summary";
        private string _Summary;
        [PropertyEntity(C_Summary, false)]
        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; NotifyPropertyChanged(C_Summary); }
        }

        public const string C_Link = "Link";
        private string _Link;
        [PropertyEntity(C_Link, false)]
        public string Link
        {
            get { return _Link; }
            set { _Link = value; NotifyPropertyChanged(C_Link); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_UpdatedDTG = "UpdatedDTG";
        private DateTime? _UpdatedDTG;
        [PropertyEntity(C_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public const string C_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
        }

        public const string C_Version = "Version";
        private int? _Version;
        [PropertyEntity(C_Version, true)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }

        public const string C_Deleted = "Deleted";
        private int? _Deleted;
        [PropertyEntity(C_Deleted, true)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
        }

        public const string C_NewsId = "NewsId";
        private int? _NewsId;
        [PropertyEntity(C_NewsId, true)]
        public int? NewsId
        {
            get { return _NewsId; }
            set { _NewsId = value; NotifyPropertyChanged(C_NewsId); }
        }
        
        public const string C_CampaignId = "CampaignId";
        private int? _CampaignId;
        [PropertyEntity(C_CampaignId, true)]
        public int? CampaignId
        {
            get { return _CampaignId; }
            set { _CampaignId = value; NotifyPropertyChanged(C_CampaignId); }
        }

        public adm_Attachment attachment { get; set; }
        public SingleNews() : base("SingleNews", "Id", "Deleted", "Version") { }
    }
}
