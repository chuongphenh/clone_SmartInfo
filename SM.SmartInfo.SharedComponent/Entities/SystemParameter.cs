using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public partial class SystemParameter : BaseEntity
    {
        #region Entity
        public const string C_SystemParameterID = "SystemParameterID";
        private int? _SystemParameterID;
        [PropertyEntity(C_SystemParameterID, true)]
        public int? SystemParameterID
        {
            get { return _SystemParameterID; }
            set { _SystemParameterID = value; NotifyPropertyChanged(C_SystemParameterID); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_Description = "Description";
        private string _Description;
        [PropertyEntity(C_Description, false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged(C_Description); }
        }

        public const string C_Code = "Code";
        private string _Code;
        [PropertyEntity(C_Code, false)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; NotifyPropertyChanged(C_Code); }
        }

        public const string C_Status = "Status";
        private int? _Status;
        [PropertyEntity(C_Status, false)]
        public int? Status
        {
            get { return _Status; }
            set { _Status = value; NotifyPropertyChanged(C_Status); }
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

        public const string C_CreatedOn = "CreatedOn";
        private string _CreatedOn;
        [PropertyEntity(C_CreatedOn, false)]
        public string CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; NotifyPropertyChanged(C_CreatedOn); }
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

        public const string C_UpdatedOn = "UpdatedOn";
        private string _UpdatedOn;
        [PropertyEntity(C_UpdatedOn, false)]
        public string UpdatedOn
        {
            get { return _UpdatedOn; }
            set { _UpdatedOn = value; NotifyPropertyChanged(C_UpdatedOn); }
        }

        public const string C_UpdatedBy = "UpdatedBy";
        private string _UpdatedBy;
        [PropertyEntity(C_UpdatedBy, false)]
        public string UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; NotifyPropertyChanged(C_UpdatedBy); }
        }

        public const string C_DisplayOrder = "DisplayOrder";
        private int? _DisplayOrder;
        [PropertyEntity(C_DisplayOrder, false)]
        public int? DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; NotifyPropertyChanged(C_DisplayOrder); }
        }

        public const string C_Deleted = "Deleted";
        private int? _Deleted;
        [PropertyEntity(C_Deleted, false)]
        public int? Deleted
        {
            get { return _Deleted; }
            set { _Deleted = value; NotifyPropertyChanged(C_Deleted); }
        }

        public const string C_Checked = "Checked";
        private int? _Checked;
        [PropertyEntity(C_Checked, false)]
        public int? Checked
        {
            get { return _Checked; }
            set { _Checked = value; NotifyPropertyChanged(C_Checked); }
        }

        public const string C_FeatureID = "FeatureID";
        private int? _FeatureID;
        [PropertyEntity(C_FeatureID, false)]
        public int? FeatureID
        {
            get { return _FeatureID; }
            set { _FeatureID = value; NotifyPropertyChanged(C_FeatureID); }
        }

        public const string C_Ext1 = "Ext1";
        private string _Ext1;
        [PropertyEntity(C_Ext1, false)]
        public string Ext1
        {
            get { return _Ext1; }
            set { _Ext1 = value; NotifyPropertyChanged(C_Ext1); }
        }

        public const string C_Ext2 = "Ext2";
        private string _Ext2;
        [PropertyEntity(C_Ext2, false)]
        public string Ext2
        {
            get { return _Ext2; }
            set { _Ext2 = value; NotifyPropertyChanged(C_Ext2); }
        }

        public const string C_Ext1i = "Ext1i";
        private int? _Ext1i;
        [PropertyEntity(C_Ext1i, false)]
        public int? Ext1i
        {
            get { return _Ext1i; }
            set { _Ext1i = value; NotifyPropertyChanged(C_Ext1i); }
        }

        public const string C_Ext2i = "Ext2i";
        private int? _Ext2i;
        [PropertyEntity(C_Ext2i, false)]
        public int? Ext2i
        {
            get { return _Ext2i; }
            set { _Ext2i = value; NotifyPropertyChanged(C_Ext2i); }
        }

        public const string C_Ext3 = "Ext3";
        private string _Ext3;
        [PropertyEntity(C_Ext3, false)]
        public string Ext3
        {
            get { return _Ext3; }
            set { _Ext3 = value; NotifyPropertyChanged(C_Ext3); }
        }

        public const string C_Ext4 = "Ext4";
        private DateTime? _Ext4;
        [PropertyEntity(C_Ext4, false)]
        public DateTime? Ext4
        {
            get { return _Ext4; }
            set { _Ext4 = value; NotifyPropertyChanged(C_Ext4); }
        }

        public SystemParameter() : base("adm_SystemParameter", "SystemParameterID", "Deleted", "Version") { }
        #endregion

        #region Extended
        [PropertyEntity("DocumentCode", false, false)]
        public string DocumentCode { get; set; }

        [PropertyEntity("Ext1iParent", false, false)]
        public int? Ext1iParent { get; set; }

        [PropertyEntity("Ext2iParent", false, false)]
        public int? Ext2iParent { get; set; }
        #endregion
    }

    public class SystemParameterInfo
    {
        public int? SystemParameterID { get; set; }
        public string Name { get; set; }
    }
}
