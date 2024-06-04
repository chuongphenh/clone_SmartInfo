using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class AwardingLevel : BaseEntity 
    {
        public const string C_Id = "Id";
        private int? _Id;
        [PropertyEntity(C_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(C_Id); }
        }

        public const string C_Level = "Level";
        private string _Level;
        [PropertyEntity(C_Level, false)]
        public string Level
        {
            get { return _Level; }
            set { _Level = value; NotifyPropertyChanged(C_Level); }
        }

        public const string C_Description = "Description";
        private string _Description;
        [PropertyEntity(C_Description, false)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged(C_Description); }
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

        public const string C_CreateUserId = "CreateUserId";
        private int? _CreateUserId;
        [PropertyEntity(C_CreateUserId, false)]
        public int? CreateUserId
        {
            get { return _CreateUserId; }
            set { _CreateUserId = value; NotifyPropertyChanged(C_CreateUserId); }
        }

        public const string C_Category = "Category";
        private int? _Category;
        [PropertyEntity(C_Category, true)]
        public int? Category
        {
            get { return _Category; }
            set { _Category = value; NotifyPropertyChanged(C_Category); }
        }

        public const string C_isDeleted = "isDeleted";
        private int? _isDeleted;
        [PropertyEntity(C_isDeleted, true)]
        public int? isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(C_isDeleted); }
        }

        public AwardingLevel() : base("AwardingLevel", "Id", string.Empty, string.Empty) { }
    }
}
