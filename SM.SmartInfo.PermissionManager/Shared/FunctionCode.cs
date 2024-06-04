namespace SM.SmartInfo.PermissionManager.Shared
{
    public class FunctionCode
    {
        public const string VIEW = "VIEW";
        public const string ADD = "ADD";
        public const string EDIT = "EDIT";
        public const string DELETE = "DELETE";
        public const string DISPLAY = "DISPLAY";
        public const string APPROVE = "APPROVE";
        public const string AssignOrganization = "AssignOrganization";
        public const string AssignEmployee = "AssignEmployee";
        public const string AssignSubEmployee = "AssignSubEmployee";
        public const string ChangeOrganization = "ChangeOrganization";
        public const string ChangeEmployee = "ChangeEmployee";
        public const string ChangePass = "ChangePass";
        public const string RequestApprove = "RequestApprove";
        public const string CancelRequest = "CancelRequest";
        public const string SHARE = "SHARE";
        //public const string EditManagementPlan = "EditManagementPlan";
        public const string REJECT = "REJECT";
        public const string ApprovedTallyFee = "APPROVETALLYFEE";
        public const string ApprovedMaMngFee = "APPROVEMAMNGFEE"; //MA MNG FEE
        public const string RequestChangeTallyFee = "REQUESTCHANGETALLYFEE";
        public const string RequestChangeMaMngFee = "REQUESTCHANGEMAMNGFEE";
        public const string RequestFeeConfirm = "REQUESTFEECONFIRM";

        public const string RequestStopService = "REQUESTSTOPSERVICE";
        public const string CompleteService = "CompleteService";
        public const string AssignDocument = "AssignDocument"; //CVKD ban giao ho so cho NVGS
        public const string RecieveDocument = "RecieveDocument"; //NVGS nhan ho so
        public const string SendMessage = "SendMessage"; //Dung cho tat ca cac nut gui thong bao
        public const string Complete = "Complete"; //Dung cho tat ca cac nut hoan thanh

        public const string CLONE = "CLONE"; // Dung cho cac chuc nang sao chep

        public const string Adjustment = "Adjustment";
        public const string RevokeAdjustment = "RevokeAdjustment";
        public const string ConfirmAdjustment = "ConfirmAdjustment";
        public const string ApproveAdjustment = "ApproveAdjustment";
        public const string ConfirmRejectAdjust = "ConfirmRejectAdjust";
        public const string RequestedApproveAdjustment = "RequestedApproveAdjustment";
    }
}