using System;
using System.Web.UI;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.MasterPages.Common
{
    public partial class SmartInfo : BaseMaster
    {
        public delegate void SearchAll(string searchText);

        public event SearchAll Search;

        protected void Page_Load(object sender, EventArgs e)
        {
            ucHeaderContent.Search += ucHeaderContent_Search;
        }

        private void ucHeaderContent_Search(string searchText)
        {
            if (Search != null)
                Search(searchText);
        }

        protected void sptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {

        }

        #region Protected application to avoid: Cross site request forgery (CSRF), Cross site scripting (XSS)
        // http://software-security.sans.org/developer-how-to/developer-guide-csrf
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";

        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";

        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // First, check for the existence of the Anti-XSS cookie 
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            // If the CSRF cookie is found, parse the token from the cookie. Then, set the global page variable and view state user key.
            // The global variable will be used to validate that it matches in the view state method
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                //Set the global token variable so the cookie value can be validated against the value in the view state form field in the Page.PreLoad method.
                _antiXsrfTokenValue = requestCookie.Value;
                //Set the view state user key, which will be validated by the framework during each request 
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else //If the CSRF cookie is not found, then this is a new session.
            {
                //Generate a new Anti-XSRF token             
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");

                //Set the view state user key, which will be validated by the framework during each request
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                //Create the non-persistent CSRF cookie             
                var responseCookie = new System.Web.HttpCookie(AntiXsrfTokenKey)
                {
                    //Set the HttpOnly property to prevent the cookie from being accessed by client side script                 
                    HttpOnly = true,
                    //Add the Anti-XSRF token to the cookie value                 
                    Value = _antiXsrfTokenValue
                };

                //If we are using SSL, the cookie should be set to secure to prevent it from being sent over HTTP connections 
                if (System.Web.Security.FormsAuthentication.RequireSSL && Request.IsSecureConnection) responseCookie.Secure = true;

                //Add the CSRF cookie to the response             
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += new EventHandler(Page_PreLoad);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            //During the initial page load, add the Anti-XSRF token and user name to the ViewState 
            if (!IsPostBack)
            {
                //Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                //If a user name is assigned, set the user name
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            //During all subsequent post backs to the page, the token value from             
            //the cookie should be validated against the token in the view state             
            //form field. Additionally user name should be compared to the authenticated users name 
            else
            {
                //Validate the Anti-XSRF token 
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue ||
                    (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }
        #endregion

        public override void ShowError(List<string> lstMsg, string sumary = "")
        {
            var script = ScriptManager.GetCurrent(Page);
            if (script != null && script.IsInAsyncPostBack)
            {
                asysErr.ShowError(lstMsg);
            }
            else
            {
                ucErr.ShowError(lstMsg);
            }
        }

        public override void ShowMessage(string msg)
        {
            var script = ScriptManager.GetCurrent(Page);
            if (script != null && script.IsInAsyncPostBack)
            {
                asysErr.ShowMessage(msg);
            }
            else
            {
                ucErr.ShowMessage(msg);
            }
        }
    }
}