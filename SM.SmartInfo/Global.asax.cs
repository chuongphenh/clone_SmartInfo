using System;
using System.Web;
using System.Linq;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using SoftMart.Service.Reporting;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Constants;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;

namespace SM.SmartInfo
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            string appCnnKey = ConfigUtils.ConnectionString;
            string xmlConfigFolder = ConfigUtils.DynamicReportFolder;
            string templateFolder = ConfigUtils.TemplateFolder;
            string temporaryFolder = ConfigUtils.TemporaryFolder;

            // cache
            GlobalCache.ReloadCache();
            GlobalCache.LoadCacheOrganization();
            GlobalCache.LoadCacheEmployee();

            // transaction
            SoftMart.Core.Dao.TransactionScope.Config(appCnnKey, ConfigUtils.IsolationLevel);

            // report - ui
            ReportingApi.ConfigUI(appCnnKey, appCnnKey, xmlConfigFolder, templateFolder, temporaryFolder);

            // report - FK
            //List<ForeignKeyData> lstForeignKey = GlobalCache.CacheSystemParamenter.Select(en =>
            //   new ForeignKeyData(en.SystemParameterID.ToString(), en.Code, en.Name)).ToList();
            //ReportingApi.SetForeignKeyData(lstForeignKey);

            // rule engine
            Dictionary<int, string> dctRule = new Dictionary<int, string>();
            foreach (var item in SMX.RuleCategory.dictRuleCategory)
                dctRule.Add(item.Key.Value, item.Value);
            SoftMart.Core.BRE.RuleEngineService.ConfigureService(appCnnKey, appCnnKey, null, dctRule);

            // cau hinh cho performance (get full infor)
            SoftMart.Core.Utilities.Diagnostics.PLogger.Mode = SoftMart.Core.Utilities.Diagnostics.PLogger.OutputMode.Full;

            //Config Notification
            var mailServerInfo = new SoftMart.Service.Notification.Entity.MailServerInfo()
            {
                Host = ConfigUtils.EmailHost,
                Port = ConfigUtils.EmailPort,
                UserName = ConfigUtils.EmailUserName,
                Password = ConfigUtils.EmailPassword,
                EnableSsl = ConfigUtils.EmailEnableSSL,
                From = ConfigUtils.EmailFrom
            };
            SoftMart.Service.Notification.NotificationApi.ConfigService(appCnnKey, temporaryFolder, null, mailServerInfo);
        }

        void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {

        }

        void Session_Start(object sender, EventArgs e)
        {
            /*// Code that runs when a new session is started
            HttpRequest httpRequest = HttpContext.Current.Request;
            if (httpRequest.Browser.IsMobileDevice)
            {
                string mobileSite = Utils.ConfigUtils.GetConfig("MobileSite");
                if (!string.IsNullOrWhiteSpace(mobileSite))
                    HttpContext.Current.Response.Redirect(mobileSite);
            }
            var cookie = Request.Cookies["ASP.Net_SessionId"];
            if (cookie != null)
            {
                var httpOnly = cookie.HttpOnly; // <-- This is always false
                cookie.Secure = true;
            }
            if (Request.IsSecureConnection)
            {
                Response.Cookies["ASP.NET_SessionId"].Secure = true;
            }*/

            // Code that runs when a new session is started
            HttpRequest httpRequest = HttpContext.Current.Request;
            if (httpRequest.Browser.IsMobileDevice)
            {
                string mobileSite = Utils.ConfigUtils.GetConfig("MobileSite");
                if (!string.IsNullOrWhiteSpace(mobileSite))
                    HttpContext.Current.Response.Redirect(mobileSite);
            }



            HttpCookie cookie = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
            if (cookie != null)
            {
                cookie.HttpOnly = true;
                if (Request.IsSecureConnection)
                {
                    cookie.Secure = true;
                }
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            // execute logout

            SharedComponent.Params.Permission.AuthenticationParam param = new SharedComponent.Params.Permission.AuthenticationParam(FunctionType.Authentication.Logout);
            PermissionManager.PermissionController.Provider.Execute(param);
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            if (!string.IsNullOrWhiteSpace(url))
            {
                url = url.ToLower();
                if (url.Contains("previewimage.aspx") || url.Contains("commentcontent.aspx") || url.Contains("attachmentimage.aspx") ||
                    url.Contains("googlemapdisplay.aspx") || url.Contains("assetmaps.aspx") || !url.Contains(".aspx"))
                {
                    return;
                }
            }
            // prevent Clickjacking
            // DENY, which prevents any domain from framing the content. The "DENY" setting is recommended unless a specific need has been identified for framing
            // SAMEORIGIN, which only allows the current site to frame the content
            // ALLOW-FROM uri, which permits the specified 'uri' to frame this page
            HttpContext.Current.Response.Headers.Remove("x-frame-options");
            HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");

            // prevent X-XSS-Protection header is not defined
            HttpContext.Current.Response.Headers.Remove("X-XSS-Protection");
            HttpContext.Current.Response.AddHeader("X-XSS-Protection", "1; mode=block");

            HttpContext.Current.Response.Headers.Remove("Server");
            Context.Response.CacheControl = "no-cache";
        }
    }
}