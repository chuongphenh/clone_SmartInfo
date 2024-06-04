using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class adm_RegionProvince : BaseEntity
	{
		public const string C_RegionProvinceID = "RegionProvinceID";
		private int? _RegionProvinceID;
		[PropertyEntity(C_RegionProvinceID, true)]
		public int? RegionProvinceID
		{
			get { return _RegionProvinceID; }
			set { _RegionProvinceID = value; NotifyPropertyChanged(C_RegionProvinceID); }
		}

		public const string C_RegionID = "RegionID";
		private int? _RegionID;
		[PropertyEntity(C_RegionID, false)]
		public int? RegionID
		{
			get { return _RegionID; }
			set { _RegionID = value; NotifyPropertyChanged(C_RegionID); }
		}

		public const string C_ProvinceID = "ProvinceID";
		private int? _ProvinceID;
		[PropertyEntity(C_ProvinceID, false)]
		public int? ProvinceID
		{
			get { return _ProvinceID; }
			set { _ProvinceID = value; NotifyPropertyChanged(C_ProvinceID); }
		}

		public adm_RegionProvince() : base("adm_RegionProvince", "RegionProvinceID", string.Empty, string.Empty) { }
	}
}
