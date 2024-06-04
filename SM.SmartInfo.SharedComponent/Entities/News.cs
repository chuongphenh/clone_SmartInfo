using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class News : BaseEntity, Interfaces.ISystemEntity
    {
        #region Primitive members

        public const string C_NewsID = "NewsID";
        private int? _NewsID;
        [PropertyEntity(C_NewsID, true)]
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

        public const string C_PostingFromDTG = "PostingFromDTG";
        private DateTime? _PostingFromDTG;
        [PropertyEntity(C_PostingFromDTG, false)]
        public DateTime? PostingFromDTG
        {
            get { return _PostingFromDTG; }
            set { _PostingFromDTG = value; NotifyPropertyChanged(C_PostingFromDTG); }
        }

        public const string C_PostingToDTG = "PostingToDTG";
        private DateTime? _PostingToDTG;
        [PropertyEntity(C_PostingToDTG, false)]
        public DateTime? PostingToDTG
        {
            get { return _PostingToDTG; }
            set { _PostingToDTG = value; NotifyPropertyChanged(C_PostingToDTG); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_RatedLevel = "RatedLevel";
        private string _RatedLevel;
        [PropertyEntity(C_RatedLevel, false)]
        public string RatedLevel
        {
            get { return _RatedLevel; }
            set { _RatedLevel = value; NotifyPropertyChanged(C_RatedLevel); }
        }

        public const string C_Concluded = "Concluded";
        private string _Concluded;
        [PropertyEntity(C_Concluded, false)]
        public string Concluded
        {
            get { return _Concluded; }
            set { _Concluded = value; NotifyPropertyChanged(C_Concluded); }
        }

        public const string C_Status = "Status";
        private int? _Status;
        [PropertyEntity(C_Status, false)]
        public int? Status
        {
            get { return _Status; }
            set { _Status = value; NotifyPropertyChanged(C_Status); }
        }

        public const string C_Type = "Type";
        private int? _Type;
        [PropertyEntity(C_Type, false)]
        public int? Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(C_Type); }
        }

        public const string C_Classification = "Classification";
        private int? _Classification;
        [PropertyEntity(C_Classification, false)]
        public int? Classification
        {
            get { return _Classification; }
            set { _Classification = value; NotifyPropertyChanged(C_Classification); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_OtherNote = "OtherNote";
        private string _OtherNote;
        [PropertyEntity(C_OtherNote, false)]
        public string OtherNote
        {
            get { return _OtherNote; }
            set { _OtherNote = value; NotifyPropertyChanged(C_OtherNote); }
        }

        public const string C_CatalogID = "CatalogID";
        private int? _CatalogID;
        [PropertyEntity(C_CatalogID, false)]
        public int? CatalogID
        {
            get { return _CatalogID; }
            set { _CatalogID = value; NotifyPropertyChanged(C_CatalogID); }
        }

        public const string C_NumberOfPublish = "NumberOfPublish";
        private int? _NumberOfPublish;
        [PropertyEntity(C_NumberOfPublish, false)]
        public int? NumberOfPublish
        {
            get { return _NumberOfPublish; }
            set { _NumberOfPublish = value; NotifyPropertyChanged(C_NumberOfPublish); }
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

        public const string C_NegativeType = "NegativeType";
        private int? _NegativeType;
        [PropertyEntity(C_NegativeType, false)]
        public int? NegativeType
        {
            get { return _NegativeType; }
            set { _NegativeType = value; NotifyPropertyChanged(C_NegativeType); }
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

        public const string C_PressAgency = "PressAgency";
        private string _PressAgency;
        [PropertyEntity(C_PressAgency, false)]
        public string PressAgency
        {
            get { return _PressAgency; }
            set { _PressAgency = value; NotifyPropertyChanged(C_PressAgency); }
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

        public const string C_Category = "Category";
        private int? _Category;
        [PropertyEntity(C_Category, false)]
        public int? Category
        {
            get { return _Category; }
            set { _Category = value; NotifyPropertyChanged(C_Category); }
        }

        public const string C_DisplayOrder = "DisplayOrder";
        private int? _DisplayOrder;
        [PropertyEntity(C_DisplayOrder, false)]
        public int? DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; NotifyPropertyChanged(C_DisplayOrder); }
        }

        public const string C_Hastag = "Hastag";
        private string _Hastag;
        [PropertyEntity(C_Hastag, false)]
        public string Hastag
        {
            get { return _Hastag; }
            set { _Hastag = value; NotifyPropertyChanged(C_Hastag); }
        }
        public const string C_isSingleCamp = "isSingleCamp";
        private bool? _isSingleCamp;
        [PropertyEntity(C_isSingleCamp, false)]
        public bool? isSingleCamp
        {
            get { return _isSingleCamp; }
            set { _isSingleCamp = value; NotifyPropertyChanged(C_isSingleCamp); }
        }

        public News() : base("News", "NewsID", "Deleted", "Version") { }
        #endregion

        #region Extent members

        public adm_Attachment Attachment { get; set; }

        public SingleNews SingleNews { get; set; }

        public List<adm_Attachment> ListAttachment { get; set; }

        public List<NegativeNews> ListNegativeNews { get; set; }

        [PropertyEntity("CatalogName", false, false)]
        public string CatalogName { get; set; }

        [PropertyEntity("CategoryName", false, false)]
        public string CategoryName { get; set; }

        [PropertyEntity("CategoryCount", false, false)]
        public int? CategoryCount { get; set; }

        [PropertyEntity("YearCreated", false, false)]
        public int? YearCreated { get; set; }

        public DateTime? FromIncurredDTG { get; set; }

        public DateTime? ToIncurredDTG { get; set; }

        public string SearchText { get; set; }

        #endregion

        public int? ItemID
        {
            get
            {
                return NewsID;
            }

            set
            {
                NewsID = value;
            }
        }
    }
}