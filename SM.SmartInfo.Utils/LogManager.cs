using SoftMart.Core.Utilities.Diagnostics;

namespace SM.SmartInfo.Utils
{
    public class LogManager
    {
        // mobile
        public static readonly ILogger MobileLogger = LoggerManager.GetLogger("Mobile");

        // Web
        public static readonly ILogger WebLogger = LoggerManager.GetLogger("SmartInfo");

        // Servicemanagement
        public static readonly ILogger ServiceLogger = LoggerManager.GetLogger("ServiceManagement");

        // Permission
        public static readonly ILogger PermissionLogger = LoggerManager.GetLogger("Permission");

        // ServiceNotification
        public static readonly ILogger ServiceNotification = LoggerManager.GetLogger("ServiceNotification");

        // ServiceReporting
        public static readonly ILogger ServiceReporting = LoggerManager.GetLogger("ServiceReporting");

        // ServiceBatchProcessing
        public static readonly ILogger ServiceBatchProcessing = LoggerManager.GetLogger("ServiceBatchProcessing");

        // ServiceAutomation
        public static readonly ILogger ServiceAutomation = LoggerManager.GetLogger("ServiceAutomation");

        // ServiceECM
        public static readonly ILogger ServiceECM = LoggerManager.GetLogger("ServiceEcm");

        // Service ReportGenerator
        public static readonly ILogger ServiceReportGenerator = LoggerManager.GetLogger("ServiceReportGenerator");

        // Service WeeklyReport
        public static readonly ILogger ServiceWeeklyReport = LoggerManager.GetLogger("ServiceWeeklyReport");

        // service FeeGenerator
        public static readonly ILogger ServiceFeeGenerator = LoggerManager.GetLogger("ServiceFeeGenerator");

        public static readonly ILogger FinancialIntegratedService = LoggerManager.GetLogger("FinancialIntegratedService");

        public static readonly ILogger Pentest = LoggerManager.GetLogger("Pentest");

        public static readonly ILogger ValidationService = LoggerManager.GetLogger("ValidationService");
    }
}