using System.IO;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.Service.Management.Biz;

namespace SM.SmartInfo.Service.Management
{
    public class ServiceManager
    {
        private SoftMart.Core.ServiceManager.ServiceManager _serviceManager;

        public void StartService()
        {
            PrepareSystemData();

            _serviceManager = new SoftMart.Core.ServiceManager.ServiceManager();
            _serviceManager.StartService();
        }

        public void TerminateService()
        {
            if (_serviceManager != null)
                _serviceManager.TerminateService();
        }

        private void PrepareSystemData()
        {
            Profiles.SetMyProfile((UserProfile)new BizProvider().CreateProfile(-1, "InventoryService", "InventoryService"));

            // caching
            GlobalCache.ReloadCache();
            GlobalCache.LoadCacheOrganization();

            // load other
            ConfigForCoreComponent();

            LogManager.ServiceLogger.LogDebug("PrepareSystemData done ");
        }

        private void ConfigForCoreComponent()
        {
            string appCnnKey = ConfigUtils.ConnectionString;
            //Use SqlTransaction
            SoftMart.Core.Dao.TransactionScope.Config(appCnnKey, ConfigUtils.IsolationLevel);
            string xmlContent = GetConfiguration();
            SoftMart.Core.ServiceManager.ServiceManagerApi.ConfigService(xmlContent, new BizProvider());
            // config dynamic report
            string xmlConfigFolderRpt = ConfigUtils.DynamicReportFolder;
            string xmlConfigFolderBPS = ConfigUtils.BPSConfigFolder;
            string templateFolder = ConfigUtils.TemplateFolder;
            string temporaryFolder = ConfigUtils.TemporaryFolder;
            //Config Reporting service
            //int reportCommandTimeout = 60 * 30; // don vi Second
            SoftMart.Service.Reporting.ReportingApi.ConfigService(appCnnKey, appCnnKey, xmlConfigFolderRpt, templateFolder, temporaryFolder,
                    new Reporting.Biz.ServiceReportingBiz());
            //Config Notification service
            var mailServerInfo = new SoftMart.Service.Notification.Entity.MailServerInfo()
            {
                Host = ConfigUtils.EmailHost,
                Port = ConfigUtils.EmailPort,
                UserName = ConfigUtils.EmailUserName,
                Password = ConfigUtils.EmailPassword,
                EnableSsl = ConfigUtils.EmailEnableSSL,
                From = ConfigUtils.EmailFrom
            };
            SoftMart.Service.Notification.NotificationApi.ConfigService(appCnnKey, temporaryFolder,
                                                                        new SM.SmartInfo.Service.Notification.BIZ.BizProvider(),
                                                                        mailServerInfo);
            #region NotUsed
            //Config BatchProcessing service
            //SoftMart.Service.BatchProcessing.BatchProcessingApi.ConfigService(appCnnKey, xmlConfigFolderBPS, templateFolder, temporaryFolder,
            //                                                                  new Service.BatchProcessing.Biz.BizProvider());
            ////Config Automation service
            //SoftMart.Service.Automation.AutomationApi.ConfigService(new SM.SmartInfo.Service.Automation.Biz.BizProvider());

            // Setting for workflow
            //SoftMart.Core.Workflow.WorkflowApi.ConfigService(appCnnKey, appCnnKey);

            //// Code that runs on application startup
            //int ruleCacheDuration = 60 * 60 * 4; // don vi = second
            //Dictionary<int, string> dctRule = new Dictionary<int, string>();
            //foreach (int key in SMX.RuleCategory.dictRuleCategory.Keys)
            //{
            //    dctRule.Add(key, SMX.RuleCategory.dictRuleCategory[key]);
            //}
            //SoftMart.Core.BRE.RuleEngineService.ConfigureService(appCnnKey, appCnnKey, ruleCacheDuration, dctRule); 
            #endregion
        }

        public string GetConfiguration()
        {
            string configFile = ConfigUtils.GetAndCheckConfig("ServiceManagementConfig");

            string xmlContent = File.ReadAllText(configFile);
            return xmlContent;
        }
    }
}