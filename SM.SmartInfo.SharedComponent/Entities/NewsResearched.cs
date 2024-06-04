using System;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class NewsResearched : BaseEntity, Interfaces.ISystemEntity
    {
        public const string C_NewsResearchedID = "NewsResearchedID";
        private int? _NewsResearchedID;
        [PropertyEntity(C_NewsResearchedID, true)]
        public int? NewsResearchedID
        {
            get { return _NewsResearchedID; }
            set { _NewsResearchedID = value; NotifyPropertyChanged(C_NewsResearchedID); }
        }

        public const string C_NewsID = "NewsID";
        private int? _NewsID;
        [PropertyEntity(C_NewsID, false)]
        public int? NewsID
        {
            get { return _NewsID; }
            set { _NewsID = value; NotifyPropertyChanged(C_NewsID); }
        }

        public const string C_ObjectContact = "ObjectContact";
        private string _ObjectContact;
        [PropertyEntity(C_ObjectContact, false)]
        public string ObjectContact
        {
            get { return _ObjectContact; }
            set { _ObjectContact = value; NotifyPropertyChanged(C_ObjectContact); }
        }

        public const string C_TypeOfContact = "TypeOfContact";
        private string _TypeOfContact;
        [PropertyEntity(C_TypeOfContact, false)]
        public string TypeOfContact
        {
            get { return _TypeOfContact; }
            set { _TypeOfContact = value; NotifyPropertyChanged(C_TypeOfContact); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_Result = "Result";
        private string _Result;
        [PropertyEntity(C_Result, false)]
        public string Result
        {
            get { return _Result; }
            set { _Result = value; NotifyPropertyChanged(C_Result); }
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

        public NewsResearched() : base("NewsResearched", "NewsResearchedID", "Deleted", "Version") { }

        public int? ItemID
        {
            get
            {
                return NewsResearchedID;
            }

            set
            {
                NewsResearchedID = value;
            }
        }
    }
}