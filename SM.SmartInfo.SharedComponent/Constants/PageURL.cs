namespace SM.SmartInfo.SharedComponent.Constants
{
    public class PageURL
    {
        public class ReportPages
        {
            public const string DownloadReport = "~/UI/Reports/Common/Default.aspx";
        }

        public const string LoginPage = "~/UI/Shared/Common/Login.aspx";
        public const string LogoutPage = "~/UI/Shared/Common/Logout.aspx";
        public const string LoginPageWithReturn = "~/UI/Shared/Common/login.aspx?ReturnUrl={0}";
        public const string DefaultPage = "~/UI/Shared/Common/Default.aspx";
        public const string DefaultPageForRM = "~/UI/Shared/Common/DefaultRM.aspx";
        public const string DefaultPageForRMManager = "~/UI/Shared/Common/DefaultRMManager.aspx";
        public const string DefaultPageForAMC = "~/UI/Shared/Common/DefaultAMC.aspx";
        public const string DefaultPageForAMCManager = "~/UI/Shared/Common/DefaultAMCManager.aspx";
        public const string DefaultPageForGuardEmployee = "~/UI/Shared/Common/DefaultGuardEmployee.aspx";
        public const string ErrorPage = "~/UI/Shared/Common/Error.aspx";
        public const string DownloadReport = "~/UI/Reports/Common/Default.aspx";
        public const string DefaultRedirectPage = "~/UI/Shared/Common/DefaultRedirect.aspx";

        public const string AddNew = "AddNew.aspx";
        public const string Edit = "Edit.aspx?ID={0}";
        public const string Display = "Display.aspx?ID={0}";
        public const string DisplayWarning = "~/UI/ProcessCollateralDocuments/PCDSupervisionManagements/OperationRisks/Display.aspx?ID={0}";
        public const string Default = "Default.aspx";
        public const string DefaultWithID = "Default.aspx?ID={0}";
        public const string ShortProcessCollateral = "ShortProcessCollateral.aspx?ID={0}";
        public const string ShortEmployeeInfor = "ShortEmployeeInfor.aspx?username={0}&phone={1}&email={2}";
        public const string DisplayNone = "Display.aspx";

        public const string ViewAttachment = "~/UI/CommonList/ViewAttachments/Display.aspx?ID={0}";

        public const string BorrowRequest = "BorrowRequest.aspx";
        public const string BorrowApprove = "BorrowApprove.aspx";
        public const string PaidConfirm = "PaidConfirm.aspx";

        public const string ConfigRight = "~/UI/Administrations/Setting/ConfigRight.aspx?ID={0}&RefType={1}&RefID={2}&Code={3}";
        public const string CommentContent = "~/UI/CommonList/Comments/CommentContent.aspx?TableType={0}&Property={1}&RefID={2}&Callback={3}&Partner={4}";

        public const string DownloadDocument = "~/UI/CommonList/Attachment/Download.aspx?ID={0}";
        public const string PreviewDocument = "~/UI/CommonList/Attachment/DocumentViewer.aspx?ID={0}";

        public const string SearchPage = "~/UI/Shared/Common/SearchPage.aspx?q={0}&t={1}";
        public const string EditNews = "~/UI/SmartInfos/News/Edit.aspx?ID={0}";

        public class CommonList
        {
            public const string DownloadTemporary = "~/UI/CommonList/Attachments/DownloadTemporaryExport.aspx?ID={0}";
        }

        public class Appropval
        {
            public const string ActivityInfo = "~/UI/Configurations/ApprovalFlows/ActivityInfo.aspx?ActID={0}&RefID={1}&RefType={2}";
        }
    }
}