using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class AwardingPeriodResult : BaseEntity
    {
        public const string C_Id = "Id";
        private int? _Id;
        [PropertyEntity(C_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(C_Id); }
        }

        public const string C_AwardedPressAgencyId = "AwardedPressAgencyId";
        private int? _AwardedPressAgencyId;
        [PropertyEntity(C_AwardedPressAgencyId, true)]
        public int? AwardedPressAgencyId
        {
            get { return _AwardedPressAgencyId; }
            set { _AwardedPressAgencyId = value; NotifyPropertyChanged(C_AwardedPressAgencyId); }
        }

        public const string C_AwardedEmployeeId = "AwardedEmployeeId";
        private int? _AwardedEmployeeId;
        [PropertyEntity(C_AwardedEmployeeId, true)]
        public int? AwardedEmployeeId
        {
            get { return _AwardedEmployeeId; }
            set { _AwardedEmployeeId = value; NotifyPropertyChanged(C_AwardedEmployeeId); }
        }

        public const string C_AwardingLevelId = "AwardingLevelId";
        private int? _AwardingLevelId;
        [PropertyEntity(C_AwardingLevelId, true)]
        public int? AwardingLevelId
        {
            get { return _AwardingLevelId; }
            set { _AwardingLevelId = value; NotifyPropertyChanged(C_AwardingLevelId); }
        }

        public const string C_Name = "Name";
        private string _Name;
        [PropertyEntity(C_Name, false)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged(C_Name); }
        }

        public const string C_ListImageId = "ListImageId";
        private string _ListImageId;
        [PropertyEntity(C_ListImageId, false)]
        public string ListImageId
        {
            get { return _ListImageId; }
            set { _ListImageId = value; NotifyPropertyChanged(C_ListImageId); }
        }

        public const string C_AwardingPeriodId = "AwardingPeriodId";
        private int? _AwardingPeriodId;
        [PropertyEntity(C_AwardingPeriodId, true)]
        public int? AwardingPeriodId
        {
            get { return _AwardingPeriodId; }
            set { _AwardingPeriodId = value; NotifyPropertyChanged(C_AwardingPeriodId); }
        }

        public const string C_Result = "Result";
        private string _Result;
        [PropertyEntity(C_Result, false)]
        public string Result
        {
            get { return _Result; }
            set { _Result = value; NotifyPropertyChanged(C_Result); }
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

        public const string C_isDeleted = "isDeleted";
        private int? _isDeleted;
        [PropertyEntity(C_isDeleted, true)]
        public int? isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; NotifyPropertyChanged(C_isDeleted); }
        }

        public AwardingPeriodResult() : base("AwardingPeriodResult", "Id", string.Empty, string.Empty) { }
    }
}
