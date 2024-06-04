using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class CampaignNews : BaseEntity
	{
		public const string C_CampaignNewsID = "CampaignNewsID";
		private int? _CampaignNewsID;
		[PropertyEntity(C_CampaignNewsID, true)]
		public int? CampaignNewsID
		{
			get { return _CampaignNewsID; }
			set { _CampaignNewsID = value; NotifyPropertyChanged(C_CampaignNewsID); }
		}

		public const string C_NewsID = "NewsID";
		private int? _NewsID;
		[PropertyEntity(C_NewsID, false)]
		public int? NewsID
		{
			get { return _NewsID; }
			set { _NewsID = value; NotifyPropertyChanged(C_NewsID); }
		}

		public const string C_Campaign = "Campaign";
		private string _Campaign;
		[PropertyEntity(C_Campaign, false)]
		public string Campaign
		{
			get { return _Campaign; }
			set { _Campaign = value; NotifyPropertyChanged(C_Campaign); }
		}

		public const string C_NumberOfNews = "NumberOfNews";
		private int? _NumberOfNews;
		[PropertyEntity(C_NumberOfNews, false)]
		public int? NumberOfNews
		{
			get { return _NumberOfNews; }
			set { _NumberOfNews = value; NotifyPropertyChanged(C_NumberOfNews); }
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

        public const string C_PostedDTG = "PostedDTG";
        private DateTime? _PostedDTG;
        [PropertyEntity(C_PostedDTG, false)]
        public DateTime? PostedDTG
        {
            get { return _PostedDTG; }
            set { _PostedDTG = value; NotifyPropertyChanged(C_PostedDTG); }
        }
        public CampaignNews() : base("CampaignNews", "CampaignNewsID", "Deleted", "Version") { }
	}
}
