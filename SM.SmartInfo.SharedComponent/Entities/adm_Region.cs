using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class adm_Region : BaseEntity, Interfaces.ISystemEntity
	{
		public const string C_RegionID = "RegionID";
		private int? _RegionID;
		[PropertyEntity(C_RegionID, true)]
		public int? RegionID
		{
			get { return _RegionID; }
			set { _RegionID = value; NotifyPropertyChanged(C_RegionID); }
		}

		public const string C_Name = "Name";
		private string _Name;
		[PropertyEntity(C_Name, false)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; NotifyPropertyChanged(C_Name); }
		}

		public const string C_EmployeeID = "EmployeeID";
		private int? _EmployeeID;
		[PropertyEntity(C_EmployeeID, false)]
		public int? EmployeeID
		{
			get { return _EmployeeID; }
			set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
		}

		public const string C_Status = "Status";
		private int? _Status;
		[PropertyEntity(C_Status, false)]
		public int? Status
		{
			get { return _Status; }
			set { _Status = value; NotifyPropertyChanged(C_Status); }
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

		public adm_Region() : base("adm_Region", "RegionID", "Deleted", "Version") { }
		public int? ItemID
		{
			get
			{
				return RegionID;
			}

			set
			{
				RegionID = value;
			}
		}
	}
}
