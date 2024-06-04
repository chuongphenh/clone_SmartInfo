using SoftMart.Core.Dao;
using System;

namespace SM.SmartInfo.SharedComponent.Entities
{
    [Serializable]
    public class AgencyType : BaseEntity
    {
        #region Primitive members

        public const string AT_Id = "Id";
        private int? _Id;
        [PropertyEntity(AT_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(AT_Id); }
        }

        public const string AT_TypeName = "TypeName";
        private string _TypeName;
        [PropertyEntity(AT_TypeName, false)]
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; NotifyPropertyChanged(AT_TypeName); }
        }

        public const string AT_Creator = "Creator";
        private string _Creator;
        [PropertyEntity(AT_Creator, false)]
        public string Creator
        {
            get { return _Creator; }
            set { _Creator = value; NotifyPropertyChanged(AT_Creator); }
        }

        public const string AT_Code = "Code";
        private string _Code;
        [PropertyEntity(AT_Code, false)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; NotifyPropertyChanged(AT_Code); }
        }

        public const string AT_Modifier = "Modifier";
        private string _Modifier;
        [PropertyEntity(AT_Modifier, false)]
        public string Modifier
        {
            get { return _Modifier; }
            set { _Modifier = value; NotifyPropertyChanged(AT_Modifier); }
        }

        public const string AT_DateModified = "DateModified";
        private DateTime _DateModified;
        [PropertyEntity(AT_DateModified, false)]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; NotifyPropertyChanged(AT_DateModified); }
        }

        public AgencyType() : base("AgencyType", "Id", string.Empty, string.Empty) { }

        #endregion
    }
}
