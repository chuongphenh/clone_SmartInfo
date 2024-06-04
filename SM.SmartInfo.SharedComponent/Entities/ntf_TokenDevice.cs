using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class ntf_TokenDevice : BaseEntity
	{
		public const string C_TokenDeviceID = "TokenDeviceID";
		private int? _TokenDeviceID;
		[PropertyEntity(C_TokenDeviceID, true)]
		public int? TokenDeviceID {
			get { return _TokenDeviceID; }
			set { _TokenDeviceID = value; NotifyPropertyChanged(C_TokenDeviceID); }
		}

        public const string C_FCMToken = "FCMToken"; 
        private string _FCMToken;
        [PropertyEntity(C_FCMToken, false)]
        public string FCMToken
        {
            get { return _FCMToken; }
            set { _FCMToken = value; NotifyPropertyChanged(C_FCMToken); }
        }

        public const string C_DeviceID = "DeviceID"; 
        private string _DeviceID;
        [PropertyEntity(C_DeviceID, false)]
        public string DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; NotifyPropertyChanged(C_DeviceID); }
        }

        public const string C_DeviceName = "DeviceName"; 
        private string _DeviceName;
        [PropertyEntity(C_DeviceName, false)]
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; NotifyPropertyChanged(C_DeviceName); }
        }

        public const string C_Guid = "Guid"; 
        private string _Guid;
        [PropertyEntity(C_Guid, false)]
        public string Guid
        {
            get { return _Guid; }
            set { _Guid = value; NotifyPropertyChanged(C_Guid); }
        }

        public const string C_AppVersion = "AppVersion"; 
        private string _AppVersion;
        [PropertyEntity(C_AppVersion, false)]
        public string AppVersion
        {
            get { return _AppVersion; }
            set { _AppVersion = value; NotifyPropertyChanged(C_AppVersion); }
        }

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public const string C_LastLoginDate = "LastLoginDate";
        private DateTime? _LastLoginDate;
        [PropertyEntity(C_LastLoginDate, false)]
        public DateTime? LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; NotifyPropertyChanged(C_LastLoginDate); }
        }

        public ntf_TokenDevice() : base("ntf_TokenDevice", "TokenDeviceID", "Deleted", "Version") { }
	}
}
