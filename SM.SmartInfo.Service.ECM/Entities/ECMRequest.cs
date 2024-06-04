//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SoftMart.Core.Dao;

//namespace SM.SmartInfo.Service.ECM.Entities
//{
//    public class ECMRequest : BaseEntity
//    {
//        public const string C_ECMRequestID = "ECMRequestID";
//        private int? _ECMRequestID;
//        [PropertyEntity(C_ECMRequestID, true)]
//        public int? ECMRequestID
//        {
//            get { return _ECMRequestID; }
//            set { _ECMRequestID = value; NotifyPropertyChanged(C_ECMRequestID); }
//        }

//        public const string C_CustomerID = "CustomerID";
//        private string _CustomerID;
//        [PropertyEntity(C_CustomerID, false)]
//        public string CustomerID
//        {
//            get { return _CustomerID; }
//            set { _CustomerID = value; NotifyPropertyChanged(C_CustomerID); }
//        }

//        public const string C_CustomerType = "CustomerType";
//        private int? _CustomerType;
//        [PropertyEntity(C_CustomerType, false)]
//        public int? CustomerType
//        {
//            get { return _CustomerType; }
//            set { _CustomerType = value; NotifyPropertyChanged(C_CustomerType); }
//        }

//        public const string C_DocumentStatus = "DocumentStatus";
//        private int? _DocumentStatus;
//        [PropertyEntity(C_DocumentStatus, false)]
//        public int? DocumentStatus
//        {
//            get { return _DocumentStatus; }
//            set { _DocumentStatus = value; NotifyPropertyChanged(C_DocumentStatus); }
//        }

//        public const string C_InputtedDTG = "InputtedDTG";
//        private DateTime? _InputtedDTG;
//        [PropertyEntity(C_InputtedDTG, false)]
//        /// <summary>
//        /// Inputted date, ngày nhận hồ sơ
//        /// </summary>
//        public DateTime? InputtedDTG
//        {
//            get { return _InputtedDTG; }
//            set { _InputtedDTG = value; NotifyPropertyChanged(C_InputtedDTG); }
//        }

//        public const string C_ApprovedDTG = "ApprovedDTG";
//        private DateTime? _ApprovedDTG;
//        [PropertyEntity(C_ApprovedDTG, false)]
//        public DateTime? ApprovedDTG
//        {
//            get { return _ApprovedDTG; }
//            set { _ApprovedDTG = value; NotifyPropertyChanged(C_ApprovedDTG); }
//        }

//        public const string C_ModifiedDTG = "ModifiedDTG";
//        private DateTime? _ModifiedDTG;
//        [PropertyEntity(C_ModifiedDTG, false)]
//        public DateTime? ModifiedDTG
//        {
//            get { return _ModifiedDTG; }
//            set { _ModifiedDTG = value; NotifyPropertyChanged(C_ModifiedDTG); }
//        }

//        public const string C_Request = "Request";
//        private string _Request;
//        [PropertyEntity(C_Request, false)]
//        public string Request
//        {
//            get { return _Request; }
//            set { _Request = value; NotifyPropertyChanged(C_Request); }
//        }

//        public const string C_Status = "Status";
//        private int? _Status;
//        [PropertyEntity(C_Status, false)]
//        public int? Status
//        {
//            get { return _Status; }
//            set { _Status = value; NotifyPropertyChanged(C_Status); }
//        }

//        public const string C_CreatedDTG = "CreatedDTG";
//        private DateTime? _CreatedDTG;
//        [PropertyEntity(C_CreatedDTG, false)]
//        public DateTime? CreatedDTG
//        {
//            get { return _CreatedDTG; }
//            set { _CreatedDTG = value; NotifyPropertyChanged(C_CreatedDTG); }
//        }

//        public const string C_UpdatedDTG = "UpdatedDTG";
//        private DateTime? _UpdatedDTG;
//        [PropertyEntity(C_UpdatedDTG, false)]
//        public DateTime? UpdatedDTG
//        {
//            get { return _UpdatedDTG; }
//            set { _UpdatedDTG = value; NotifyPropertyChanged(C_UpdatedDTG); }
//        }

//        public const string C_Reason = "Reason";
//        private string _Reason;
//        [PropertyEntity(C_Reason, false)]
//        public string Reason
//        {
//            get { return _Reason; }
//            set { _Reason = value; NotifyPropertyChanged(C_Reason); }
//        }

//        public ECMRequest() : base("ECMRequest", "ECMRequestID", "", "") { }
//    }
//}
