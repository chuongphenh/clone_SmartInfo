using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class SharingManagement : BaseEntity
    {
        public const string C_Id = "Id";
        private int? _Id;
        [PropertyEntity(C_Id, true)]
        public int? Id
        {
            get { return _Id; }
            set { _Id = value; NotifyPropertyChanged(C_Id); }
        }

        public const string C_PressAgencyHRID = "PressAgencyHRID";
        private int? _PressAgencyHRID;
        [PropertyEntity(C_PressAgencyHRID, true)]
        public int? PressAgencyHRID
        {
            get { return _PressAgencyHRID; }
            set { _PressAgencyHRID = value; NotifyPropertyChanged(C_PressAgencyHRID); }
        }

        public const string C_UserId = "UserId";
        private int? _UserId;
        [PropertyEntity(C_UserId, false)]
        public int? UserId
        {
            get { return _UserId; }
            set { _UserId = value; NotifyPropertyChanged(C_UserId); }
        }

        public const string C_UserEmail = "UserEmail";
        private string _UserEmail;
        [PropertyEntity(C_UserEmail, false)]
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; NotifyPropertyChanged(C_UserEmail); }
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

        public const string C_isShared = "isShared";
        private int _isShared;
        [PropertyEntity(C_isShared, false)]
        public int Content
        {
            get { return _isShared; }
            set { _isShared = value; NotifyPropertyChanged(C_isShared); }
        }

        public SharingManagement() : base("SharingManagement", "Id", string.Empty, string.Empty) { }
    }
}
