using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class ntf_NotificationPushHistory : BaseEntity
	{
		public const string C_ID = "ID";
		private int? _ID;
		[PropertyEntity(C_ID, true)]
		public int? ID
		{
			get { return _ID; }
			set { _ID = value; NotifyPropertyChanged(C_ID); }
		}
		
		public const string C_Type = "Type";
		private int? _Type;
		[PropertyEntity(C_Type, true)]
		public int? Type
		{
			get { return _Type; }
			set { _Type = value; NotifyPropertyChanged(C_Type); }
		}
		
		public const string C_RefData = "RefData";
		private string _RefData;
		[PropertyEntity(C_RefData, false)]
		public string RefData
		{
			get { return _RefData; }
			set { _RefData = value; NotifyPropertyChanged(C_RefData); }
		}

		public const string C_Content = "Content";
		private string _Content;
		[PropertyEntity(C_Content, false)]
		public string Content
		{
			get { return _Content; }
			set { _Content = value; NotifyPropertyChanged(C_Content); }
		}

		public const string C_Title = "Title";
		private string _Title;
		[PropertyEntity(C_Title, false)]
		public string Title
		{
			get { return _Title; }
			set { _Title = value; NotifyPropertyChanged(C_Title); }
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

		public const string C_EmployeeID = "EmployeeID";
		private int? _EmployeeID;
		[PropertyEntity(C_EmployeeID, false)]
		public int? EmployeeID
		{
			get { return _EmployeeID; }
			set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
		}

		public const string C_IsRead = "IsRead";
		private int? _IsRead;
		[PropertyEntity(C_IsRead, false)]
		public int? IsRead
		{
			get { return _IsRead; }
			set { _IsRead = value; NotifyPropertyChanged(C_IsRead); }
		}

		public const string C_Status = "Status";
		private int? _Status;
		[PropertyEntity(C_Status, false)]
		public int? Status
		{
			get { return _Status; }
			set { _Status = value; NotifyPropertyChanged(C_Status); }
		}

		public const string C_ClientMessageID = "ClientMessageID";
		private string _ClientMessageID;
		[PropertyEntity(C_ClientMessageID, false)]
		public string ClientMessageID
		{
			get { return _ClientMessageID; }
			set { _ClientMessageID = value; NotifyPropertyChanged(C_ClientMessageID); }
		}

		public const string C_Error = "Error";
		private string _Error;
		[PropertyEntity(C_Error, false)]
		public string Error
		{
			get { return _Error; }
			set { _Error = value; NotifyPropertyChanged(C_Error); }
		}

		public const string C_DeviceID = "DeviceID";
		private string _DeviceID;
		[PropertyEntity(C_DeviceID, false)]
		public string DeviceID
		{
			get { return _DeviceID; }
			set { _DeviceID = value; NotifyPropertyChanged(C_DeviceID); }
		}

		public const string C_TransactionId = "TransactionId";
		private string _TransactionId;
		[PropertyEntity(C_TransactionId, false)]
		public string TransactionId
		{
			get { return _TransactionId; }
			set { _TransactionId = value; NotifyPropertyChanged(C_TransactionId); }
		}
		public ntf_NotificationPushHistory() : base("ntf_NotificationPushHistory", "ID", String.Empty, String.Empty) { }

		public adm_Attachment Attachment { get; set; }
		public string UserFullName { get; set; }

	}
}
