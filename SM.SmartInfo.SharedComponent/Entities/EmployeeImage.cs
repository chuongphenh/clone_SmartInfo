using SoftMart.Core.Dao;

namespace SM.SmartInfo.SharedComponent.Entities
{
    public class EmployeeImage : BaseEntity
    {
        public const string C_EmployeeImageID = "EmployeeImageID";
        private int? _EmployeeImageID;
        [PropertyEntity(C_EmployeeImageID, true)]
        public int? EmployeeImageID
        {
            get { return _EmployeeImageID; }
            set { _EmployeeImageID = value; NotifyPropertyChanged(C_EmployeeImageID); }
        }

        public const string C_EmployeeID = "EmployeeID";
        private int? _EmployeeID;
        [PropertyEntity(C_EmployeeID, false)]
        public int? EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; NotifyPropertyChanged(C_EmployeeID); }
        }

        public const string C_SignImage = "SignImage";
        private byte[] _SignImage;
        [PropertyEntity(C_SignImage, false)]
        public byte[] SignImage
        {
            get { return _SignImage; }
            set { _SignImage = value; NotifyPropertyChanged(C_SignImage); }
        }

        public EmployeeImage() : base("adm_EmployeeImage", "EmployeeImageID", string.Empty, string.Empty) { }

        public string ContentType { get; set; }
    }
}
