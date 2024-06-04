using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using SoftMart.Core.ServiceManager.Biz;

namespace SM.SmartInfo.Service.Management.Biz
{
    public class BizProvider : BaseBizProvider
    {
        public override BaseWorker CreateService(string serviceName, string normalisedName)
        {
            BaseWorker thread = null;
            switch (normalisedName)
            {
                case SoftMart.Service.Reporting.ReportingService.SERVICE_NAME:
                    thread = new SoftMart.Service.Reporting.ReportingService();
                    break;
                //case SoftMart.Service.BatchProcessing.BatchProcessingService.SERVICE_NAME:
                //    thread = new SoftMart.Service.BatchProcessing.BatchProcessingService();
                //    break;
                //case SoftMart.Service.Automation.AutomationService.SERVICE_NAME:
                //    thread = new SoftMart.Service.Automation.AutomationService();
                //    break;
                //case Service.ReportGenerator.DailyReportGeneratorService.SERVICE_NAME:
                //    thread = new Service.ReportGenerator.DailyReportGeneratorService();
                //    break;
                //case Service.ReportGenerator.RealtimeReportGeneratorService.SERVICE_NAME:
                //    thread = new Service.ReportGenerator.RealtimeReportGeneratorService();
                //    break;
                case SoftMart.Service.Notification.NotificationService.SERVICE_NAME:
                    thread = new SoftMart.Service.Notification.NotificationService();
                    break;
                case Notification.NotifyEventService.SERVICE_NAME:
                    thread = new Notification.NotifyEventService();
                    break;
                //case Recursive.RecursiveService.SERVICE_NAME:
                //    thread = new Recursive.RecursiveService();
                //    break;
                //case Service.WeeklyReport.WeeklyReportService.SERVICE_NAME:
                //    thread = new Service.WeeklyReport.WeeklyReportService();
                //    break;
                //case FeeGenerator.FeeGeneratorService.SERVICE_NAME:
                //    thread = new FeeGenerator.FeeGeneratorService();
                //    break;
                //case Validation.ValidationService.SERVICE_NAME:
                //    thread = new Validation.ValidationService();
                //    break;
                default:
                    throw new SMXException("Service not supported: " + serviceName);
            }

            return thread;
        }

        public override SoftMart.Core.Utilities.Profiles.IUserProfile CreateProfile(int employeeID, string userName, string fullName)
        {
            // profile cua service
            UserProfile userPro = new UserProfile();
            userPro.EmployeeID = employeeID;
            userPro.FullName = fullName;
            userPro.UserName = userName;
            userPro.Employee = new SharedComponent.Entities.Employee()
            {
                EmployeeID = employeeID,
                Name = fullName,
                Username = userName
            };

            return userPro;
        }
    }
}