using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public partial class Flex_EmailTemplate : BaseEntity
    {
        #region Primitive Properties

        public const string C_EmailTemplateID = "EmailTemplateID";
        private int? _EmailTemplateID;
        [PropertyEntity(C_EmailTemplateID, true)]
        public int? EmailTemplateID
        {
            get { return _EmailTemplateID; }
            set { _EmailTemplateID = value; NotifyPropertyChanged(C_EmailTemplateID); }
        }

        public const string C_Subject = "Subject";
        private string _Subject;
        [PropertyEntity(C_Subject, false)]
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; NotifyPropertyChanged(C_Subject); }
        }

        public const string C_Content = "Content";
        private string _Content;
        [PropertyEntity(C_Content, false)]
        public string Content
        {
            get { return _Content; }
            set { _Content = value; NotifyPropertyChanged(C_Content); }
        }

        public const string C_Properties = "Properties";
        private string _Properties;
        [PropertyEntity(C_Properties, false)]
        public string Properties
        {
            get { return _Properties; }
            set { _Properties = value; NotifyPropertyChanged(C_Properties); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_Code = "Code";
        private string _Code;
        [PropertyEntity(C_Code, false)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; NotifyPropertyChanged(C_Code); }
        }

        public const string C_TemplateType = "TemplateType";
        private int? _TemplateType;
        [PropertyEntity(C_TemplateType, false)]
        public int? TemplateType
        {
            get { return _TemplateType; }
            set { _TemplateType = value; NotifyPropertyChanged(C_TemplateType); }
        }

        public const string C_TransformType = "TransformType";
        private int? _TransformType;
        [PropertyEntity(C_TransformType, false)]
        public int? TransformType
        {
            get { return _TransformType; }
            set { _TransformType = value; NotifyPropertyChanged(C_TransformType); }
        }

        public const string C_ContentBinary = "ContentBinary";
        private byte[] _ContentBinary;
        [PropertyEntity(C_ContentBinary, false)]
        public byte[] ContentBinary
        {
            get { return _ContentBinary; }
            set { _ContentBinary = value; NotifyPropertyChanged(C_ContentBinary); }
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

        public const string C_CreatedDTG = "CreatedDTG";
        private DateTime? _CreatedDTG;
        [PropertyEntity(C_CreatedDTG, false)]
        public DateTime? CreatedDTG
        {
            get { return _CreatedDTG; }
            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
        }

        public const string C_CreatedBy = "CreatedBy";
        private string _CreatedBy;
        [PropertyEntity(C_CreatedBy, false)]
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; NotifyPropertyChanged(C_CreatedBy); }
        }

        public const string C_UpdatedDTG = "UpdatedDTG";
        private DateTime? _UpdatedDTG;
        [PropertyEntity(C_UpdatedDTG, false)]
        public DateTime? UpdatedDTG
        {
            get { return _UpdatedDTG; }
            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
        }

        public const string C_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
        }

        public const string C_Status = "Status";
        private int? _Status;
        [PropertyEntity(C_Status, false)]
        public int? Status
        {
            get { return _Status; }
            set { _Status = value; NotifyPropertyChanged(C_Status); }
        }

        public const string C_IsManually = "IsManually";
        private int? _IsManually;
        [PropertyEntity(C_IsManually, false)]
        public int? IsManually
        {
            get { return _IsManually; }
            set { _IsManually = value; NotifyPropertyChanged(C_IsManually); }
        }

        public const string C_TriggerType = "TriggerType";
        private int? _TriggerType;
        [PropertyEntity(C_TriggerType, false)]
        public int? TriggerType
        {
            get { return _TriggerType; }
            set { _TriggerType = value; NotifyPropertyChanged(C_TriggerType); }
        }

        public const string C_TriggerTime = "TriggerTime";
        private DateTime? _TriggerTime;
        [PropertyEntity(C_TriggerTime, false)]
        public DateTime? TriggerTime
        {
            get { return _TriggerTime; }
            set { _TriggerTime = value; NotifyPropertyChanged(C_TriggerTime); }
        }

        public Flex_EmailTemplate() : base("flex_EmailTemplate", "EmailTemplateID", "Deleted", "Version") { }

        #endregion
    }
}
