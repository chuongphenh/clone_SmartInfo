using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class agency_PressAgencyHRAlert : BaseEntity
	{
		public const string C_PressAgencyHRAlertID = "PressAgencyHRAlertID";
		private int? _PressAgencyHRAlertID;
		[PropertyEntity(C_PressAgencyHRAlertID, true)]
		public int? PressAgencyHRAlertID {
			get { return _PressAgencyHRAlertID; }
			set { _PressAgencyHRAlertID = value; NotifyPropertyChanged(C_PressAgencyHRAlertID); }
		}

		public const string C_PressAgencyHRID = "PressAgencyHRID";
		private int? _PressAgencyHRID;
		[PropertyEntity(C_PressAgencyHRID, false)]
		public int? PressAgencyHRID {
			get { return _PressAgencyHRID; }
			set { _PressAgencyHRID = value; NotifyPropertyChanged(C_PressAgencyHRID); }
		}

		public const string C_AlertDTG = "AlertDTG";
		private DateTime? _AlertDTG;
		[PropertyEntity(C_AlertDTG, false)]
		public DateTime? AlertDTG {
			get { return _AlertDTG; }
			set { _AlertDTG = value; NotifyPropertyChanged(C_AlertDTG); }
		}

		public const string C_Content = "Content";
		private string _Content;
		[PropertyEntity(C_Content, false)]
		public string Content {
			get { return _Content; }
			set { _Content = value; NotifyPropertyChanged(C_Content); }
		}

		public const string C_Deleted = "Deleted";
		private int? _Deleted;
		[PropertyEntity(C_Deleted, false)]
		public int? Deleted {
			get { return _Deleted; }
			set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
		}

        public const string C_TypeDate = "TypeDate";
        private int? _TypeDate;
        [PropertyEntity(C_TypeDate, false)]
        public int? TypeDate
        {
            get { return _TypeDate; }
            set { _TypeDate = value; NotifyPropertyChanged(C_TypeDate); }
        }

        public const string C_Version = "Version";
		private int? _Version;
		[PropertyEntity(C_Version, false)]
		public int? Version {
			get { return _Version; }
			set { _Version = value; NotifyPropertyChanged(C_Version); }
		}

		public const string C_CreatedBy = "CreatedBy";
		private string _CreatedBy;
		[PropertyEntity(C_CreatedBy, false)]
		public string CreatedBy {
			get { return _CreatedBy; }
			set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
		}

		public const string C_CreatedDTG = "CreatedDTG";
		private DateTime? _CreatedDTG;
		[PropertyEntity(C_CreatedDTG, false)]
		public DateTime? CreatedDTG {
			get { return _CreatedDTG; }
			set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
		}

		public const string C_UpdatedBy = "UpdatedBy";
		private string _UpdatedBy;
		[PropertyEntity(C_UpdatedBy, false)]
		public string UpdatedBy {
			get { return _UpdatedBy; }
			set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
		}

		public const string C_UpdatedDTG = "UpdatedDTG";
		private DateTime? _UpdatedDTG;
		[PropertyEntity(C_UpdatedDTG, false)]
		public DateTime? UpdatedDTG {
			get { return _UpdatedDTG; }
			set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
		}
		public const string C_lunarDay = "lunarDay";
		private int? _lunarDay;

		[PropertyEntity(C_lunarDay, false)]
		public int? lunarDay
		{
			get { return _lunarDay; }
			set { _lunarDay = value; NotifyPropertyChanged(C_lunarDay); }
		}

		public const string C_lunarMonth = "lunarMonth";
		private int? _lunarMonth;

		[PropertyEntity(C_lunarMonth, false)]
		public int? lunarMonth
		{
			get { return _lunarMonth; }
			set { _lunarMonth = value; NotifyPropertyChanged(C_lunarMonth); }
		}

		public const string C_lunarYear = "lunarYear";
		private int? _lunarYear;

		[PropertyEntity(C_lunarYear, false)]
		public int? lunarYear
		{
			get { return _lunarYear; }
			set { _lunarYear = value; NotifyPropertyChanged(C_lunarYear); }
		}
		public agency_PressAgencyHRAlert() : base("agency_PressAgencyHRAlert", "PressAgencyHRAlertID", "Deleted", "Version") { }
		
	}
}
