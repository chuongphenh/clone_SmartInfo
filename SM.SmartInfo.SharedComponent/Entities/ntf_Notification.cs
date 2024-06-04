using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class ntf_Notification : BaseEntity
    {
        #region Primitive members

        public const string C_NotificationID = "NotificationID";
        private int? _NotificationID;
        [PropertyEntity(C_NotificationID, true)]
        public int? NotificationID
        {
            get { return _NotificationID; }
            set { _NotificationID = value; NotifyPropertyChanged(C_NotificationID); }
        }

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_UpdateDTG = "UpdateDTG";
        private DateTime? _UpdateDTG;
        [PropertyEntity(C_UpdateDTG, false)]
        public DateTime? UpdateDTG
        {
            get { return _UpdateDTG; }
            set { _UpdateDTG = value; NotifyPropertyChanged(C_UpdateDTG); }
        }

        public const string C_DoDTG = "DoDTG";
        private DateTime? _DoDTG;
        [PropertyEntity(C_DoDTG, false)]
        public DateTime? DoDTG
        {
            get { return _DoDTG; }
            set { _DoDTG = value; NotifyPropertyChanged(C_DoDTG); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_Note = "Note";
        private string _Note;
        [PropertyEntity(C_Note, false)]
        public string Note
        {
            get { return _Note; }
            set { _Note = value; NotifyPropertyChanged(C_Note); }
        }

        public const string C_Type = "Type";
        private int _Type;
        [PropertyEntity(C_Type, false)]
        public int Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
        }

        public const string C_Comment = "Comment";
        private string _Comment;
        [PropertyEntity(C_Comment, false)]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; NotifyPropertyChanged(C_Comment); }
        }

        public const string C_AlertID = "AlertID";
        private int? _AlertID;
        [PropertyEntity(C_AlertID, true)]
        public int? AlertID
        {
            get { return _AlertID; }
            set { _AlertID = value; NotifyPropertyChanged(C_AlertID); }
        }

        public const string C_isDeleted = "isDeleted";
        private int? _isDeleted;
        [PropertyEntity(C_isDeleted, true)]
        public int? isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(C_isDeleted); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, true)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
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

        [PropertyEntity(C_lunarYear,false)]
        public int? lunarYear
        {
            get { return _lunarYear; }
            set { _lunarYear = value; NotifyPropertyChanged(C_lunarYear); }
        }
        public ntf_Notification() : base("ntf_Notification", "NotificationID", string.Empty, string.Empty) { }

        #endregion

        #region Extent members

        [PropertyEntity("NumberOfSend", false, false)]
        public int? NumberOfSend { get; set; }

        public ntf_NotificationHistory NotificationHistory { get; set; }
        public agency_PressAgencyHRAlert PressAgencyHRAlert { get; set; }

        public DateTime? FromDoDTG { get; set; }

        public DateTime? ToDoDTG { get; set; }

        public string TextSearch { get; set; }
        public string DateConvert { get; set; }
        public string SubID { get; set; }

        #endregion
    }
}