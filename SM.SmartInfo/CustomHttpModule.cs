using System;
using System.Web;

namespace SM.SmartInfo
{
    public class CustomHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
            context.EndRequest += OnEndRequest;
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.RawUrl;
            //if (url.IndexOf("/UI/", StringComparison.OrdinalIgnoreCase) >= 0 && HttpContext.Current.Request.UrlReferrer != null)
            //{
            //    string urlLocalPath = HttpContext.Current.Request.UrlReferrer.LocalPath;
            //    if (!string.IsNullOrWhiteSpace(urlLocalPath) && !urlLocalPath.StartsWith("/UI"))
            //        HttpContext.Current.Response.Redirect(SharedComponent.Constants.PageURL.ErrorPage);
            //}
            bool noAccess = false;
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (url.IndexOf("/Logs/", StringComparison.OrdinalIgnoreCase) >= 0)
                    HttpContext.Current.Response.Redirect(SharedComponent.Constants.PageURL.ErrorPage);
                noAccess = url.IndexOf("/Repository/", StringComparison.OrdinalIgnoreCase) >= 0;
                if (!noAccess)
                    noAccess = url.IndexOf("/Templates/", StringComparison.OrdinalIgnoreCase) >= 0;
                if (!noAccess)
                    noAccess = url.IndexOf("/XmlConfigs/", StringComparison.OrdinalIgnoreCase) >= 0;
            }
            if (noAccess)
            {
                bool isLogin = false;
                try
                {
                    isLogin = (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated);
                }
                catch
                {
                    //
                }
                if (!isLogin)
                    HttpContext.Current.Response.Redirect(SharedComponent.Constants.PageURL.ErrorPage);
            }
        }

        private void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            // modify the “Server” Http Header
            HttpContext.Current.Response.Headers.Remove("Server");
        }

        public void Dispose()
        {
        }
    }
}