using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class Comment : BaseEntity
    {
        #region Primitive members

        public const string C_CommentID = "CommentID";
        private int? _CommentID;
        [PropertyEntity(C_CommentID, true)]
        public int? CommentID
        {
            get { return _CommentID; }
            set { _CommentID = value; NotifyPropertyChanged(C_CommentID); }
        }

        public const string C_RefID = "RefID";
        private int? _RefID;
        [PropertyEntity(C_RefID, false)]
        public int? RefID
        {
            get { return _RefID; }
            set { _RefID = value; NotifyPropertyChanged(C_RefID); }
        }

        public const string C_RefTitle = "RefTitle";
        private string _RefTitle;
        [PropertyEntity(C_RefTitle, false)]
        public string RefTitle
        {
            get { return _RefTitle; }
            set { _RefTitle = value; NotifyPropertyChanged(C_RefTitle); }
        }

        public const string C_RefType = "RefType";
        private int? _RefType;
        [PropertyEntity(C_RefType, false)]
        public int? RefType
        {
            get { return _RefType; }
            set { _RefType = value; NotifyPropertyChanged(C_RefType); }
        }

        public const string C_Rate = "Rate";
        private int? _Rate;
        [PropertyEntity(C_Rate, false)]
        public int? Rate
        {
            get { return _Rate; }
            set { _Rate = value; NotifyPropertyChanged(C_Rate); }
        }
        
        public const string C_DeviceToken = "DeviceToken";
        private string _DeviceToken;
        [PropertyEntity(C_DeviceToken, false)]
        public string DeviceToken
        {
            get { return _DeviceToken; }
            set { _DeviceToken = value; NotifyPropertyChanged(C_DeviceToken); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_CommentedDTG = "CommentedDTG";
        private DateTime? _CommentedDTG;
        [PropertyEntity(C_CommentedDTG, false)]
        public DateTime? CommentedDTG
        {
            get { return _CommentedDTG; }
            set { _CommentedDTG = value; NotifyPropertyChanged(C_CommentedDTG); }
        }

        public const string C_CommentedByID = "CommentedByID";
        private int? _CommentedByID;
        [PropertyEntity(C_CommentedByID, false)]
        public int? CommentedByID
        {
            get { return _CommentedByID; }
            set { _CommentedByID = value; NotifyPropertyChanged(C_CommentedByID); }
        }

        public const string C_CommentedByName = "CommentedByName";
        private string _CommentedByName;
        [PropertyEntity(C_CommentedByName, false)]
        public string CommentedByName
        {
            get { return _CommentedByName; }
            set { _CommentedByName = value; NotifyPropertyChanged(C_CommentedByName); }
        }

        public const string C_CommentedByUserName = "CommentedByUserName";
        private string _CommentedByUserName;
        [PropertyEntity(C_CommentedByUserName, false)]
        public string CommentedByUserName
        {
            get { return _CommentedByUserName; }
            set { _CommentedByUserName = value; NotifyPropertyChanged(C_CommentedByUserName); }
        }

        public const string C_Version = "Version";
        private int? _Version;
        [PropertyEntity(C_Version, false)]
        public int? Version
        {
            get { return _Version; }
            set { _Version = value; NotifyPropertyChanged(C_Version); }
        }


        public const string C_typeNews = "typeNews";
        private int? _typeNews;
        [PropertyEntity(C_typeNews, false)]
        public int? typeNews
        {
            get { return _typeNews; }
            set { _typeNews = value; NotifyPropertyChanged(C_typeNews); }
        }
        public const string C_SubID = "SubID";
        private int? _SubID;
        [PropertyEntity(C_SubID, false)]
        public int? SubID
        {
            get { return _SubID; }
            set { _SubID = value; NotifyPropertyChanged(C_SubID); }
        }
        public Comment() : base("Comment", "CommentID", string.Empty, "Version") { }

        #endregion

        #region Extent members

        public int? TypeNoti { get; set; }
        public adm_Attachment Attachment { get; set; }
        #endregion
    }
}