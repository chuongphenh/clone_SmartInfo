using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
	public class NegativeNews : BaseEntity, Interfaces.ISystemEntity
	{
		#region Primitive members

		public const string C_NegativeNewsID = "NegativeNewsID";
		private int? _NegativeNewsID;
		[PropertyEntity(C_NegativeNewsID, true)]
		public int? NegativeNewsID
		{
			get { return _NegativeNewsID; }
			set { _NegativeNewsID = value; NotifyPropertyChanged(C_NegativeNewsID); }
		}

		public const string C_NewsID = "NewsID";
		private int? _NewsID;
		[PropertyEntity(C_NewsID, false)]
		public int? NewsID
		{
			get { return _NewsID; }
			set { _NewsID = value; NotifyPropertyChanged(C_NewsID); }
		}

		public const string C_IncurredDTG = "IncurredDTG";
		private DateTime? _IncurredDTG;
		[PropertyEntity(C_IncurredDTG, false)]
		public DateTime? IncurredDTG
		{
			get { return _IncurredDTG; }
			set { _IncurredDTG = value; NotifyPropertyChanged(C_IncurredDTG); }
		}

		public const string C_PressAgencyID = "PressAgencyID";
		private int? _PressAgencyID;
		[PropertyEntity(C_PressAgencyID, false)]
		public int? PressAgencyID
		{
			get { return _PressAgencyID; }
			set { _PressAgencyID = value; NotifyPropertyChanged(C_PressAgencyID); }
		}

		public const string C_OtherChannel = "OtherChannel";
		private bool? _OtherChannel;
		[PropertyEntity(C_OtherChannel, false)]
		public bool? OtherChannel
		{
			get { return _OtherChannel; }
			set { _OtherChannel = value; NotifyPropertyChanged(C_OtherChannel); }
		}

		public const string C_OtherChannelName = "OtherChannelName";
		private string _OtherChannelName;
		[PropertyEntity(C_OtherChannelName, false)]
		public string OtherChannelName
		{
			get { return _OtherChannelName; }
			set { _OtherChannelName = value; NotifyPropertyChanged(C_OtherChannelName); }
		}

		public const string C_Content = "Content";
		private string _Content;
		[PropertyEntity(C_Content, false)]
		public string Content
		{
			get { return _Content; }
			set { _Content = value; NotifyPropertyChanged(C_Content); }
		}

		public const string C_Judged = "Judged";
		private string _Judged;
		[PropertyEntity(C_Judged, false)]
		public string Judged
		{
			get { return _Judged; }
			set { _Judged = value; NotifyPropertyChanged(C_Judged); }
		}

		public const string C_MethodHandle = "MethodHandle";
		private string _MethodHandle;
		[PropertyEntity(C_MethodHandle, false)]
		public string MethodHandle
		{
			get { return _MethodHandle; }
			set { _MethodHandle = value; NotifyPropertyChanged(C_MethodHandle); }
		}

		public const string C_Result = "Result";
		private string _Result;
		[PropertyEntity(C_Result, false)]
		public string Result
		{
			get { return _Result; }
			set { _Result = value; NotifyPropertyChanged(C_Result); }
		}

		public const string C_Status = "Status";
		private int? _Status;
		[PropertyEntity(C_Status, false)]
		public int? Status
		{
			get { return _Status; }
			set { _Status = value; NotifyPropertyChanged(C_Status); }
		}

		public const string C_Url = "Url";
		private string _Url;
		[PropertyEntity(C_Url, false)]
		public string Url
		{
			get { return _Url; }
			set { _Url = value; NotifyPropertyChanged(C_Url); }
		}

		public const string C_ReporterInformation = "ReporterInformation";
		private string _ReporterInformation;
		[PropertyEntity(C_ReporterInformation, false)]
		public string ReporterInformation
		{
			get { return _ReporterInformation; }
			set { _ReporterInformation = value; NotifyPropertyChanged(C_ReporterInformation); }
		}

		public const string C_PressAgencyReview = "PressAgencyReview";
		private string _PressAgencyReview;
		[PropertyEntity(C_PressAgencyReview, false)]
		public string PressAgencyReview
		{
			get { return _PressAgencyReview; }
			set { _PressAgencyReview = value; NotifyPropertyChanged(C_PressAgencyReview); }
		}

		public const string C_Question = "Question";
		private string _Question;
		[PropertyEntity(C_Question, false)]
		public string Question
		{
			get { return _Question; }
			set { _Question = value; NotifyPropertyChanged(C_Question); }
		}

		public const string C_QuestionDetail = "QuestionDetail";
		private string _QuestionDetail;
		[PropertyEntity(C_QuestionDetail, false)]
		public string QuestionDetail
		{
			get { return _QuestionDetail; }
			set { _QuestionDetail = value; NotifyPropertyChanged(C_QuestionDetail); }
		}

		public const string C_Resolution = "Resolution";
		private string _Resolution;
		[PropertyEntity(C_Resolution, false)]
		public string Resolution
		{
			get { return _Resolution; }
			set { _Resolution = value; NotifyPropertyChanged(C_Resolution); }
		}

		public const string C_ResolutionContent = "ResolutionContent";
		private string _ResolutionContent;
		[PropertyEntity(C_ResolutionContent, false)]
		public string ResolutionContent
		{
			get { return _ResolutionContent; }
			set { _ResolutionContent = value; NotifyPropertyChanged(C_ResolutionContent); }
		}

		public const string C_Note = "Note";
		private string _Note;
		[PropertyEntity(C_Note, false)]
		public string Note
		{
			get { return _Note; }
			set { _Note = value; NotifyPropertyChanged(C_Note); }
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

		public const string C_Type = "Type";
		private int? _Type;
		[PropertyEntity(C_Type, false)]
		public int? Type
		{
			get { return _Type; }
			set { _Type = value; NotifyPropertyChanged(C_Type); }
		}

		public const string C_Name = "Name";
		private string _Name;
		[PropertyEntity(C_Name, false)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value; NotifyPropertyChanged(C_Name); }
		}

		public const string C_Place = "Place";
		private string _Place;
		[PropertyEntity(C_Place, false)]
		public string Place
		{
			get { return _Place; }
			set { _Place = value; NotifyPropertyChanged(C_Place); }
		}

		public const string C_Title = "Title";
		private string _Title;
		[PropertyEntity(C_Title, false)]
		public string Title
		{
			get { return _Title; }
			set { _Title = value; NotifyPropertyChanged(C_Title); }
		}

		public NegativeNews() : base("NegativeNews", "NegativeNewsID", "Deleted", "Version") { }

		public int? ItemID
		{
			get
			{
				return NegativeNewsID;
			}

			set
			{
				NegativeNewsID = value;
			}
		}

#endregion

		#region Extends

		public adm_Attachment Attachment { get; set; }

		[PropertyEntity("AttachmentID", false, false)]
		public int? AttachmentID { get; set; }

		[PropertyEntity("ECMItemID", false, false)]
		public string ECMItemID { get; set; }

		[PropertyEntity("FileName", false, false)]
		public string FileName { get; set; }

		[PropertyEntity("PressAgencyName", false, false)]
		public string PressAgencyName { get; set; }

		public DateTime? FromIncurredDTG { get; set; }

		public DateTime? ToIncurredDTG { get; set; }

		public string SearchText { get; set; }

		#endregion
	}
}