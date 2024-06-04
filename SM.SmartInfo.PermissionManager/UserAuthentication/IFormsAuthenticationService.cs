using SM.SmartInfo.CacheManager;
using SoftMart.Core.Utilities.Statistics;
using System.Collections;
using System.Web.SessionState;

namespace SM.SmartInfo.PermissionManager.UserAuthentication
{
    interface IFormsAuthenticationService
    {
        void SignIn(string employeeID, string[] roleIDs = null);
        void SignOut();
    }

    class FormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// Chua biet de o dau thi do chinh xac -> tam thoi de day
        /// </summary>
        public static Monitor CCUMonitor = new Monitor("Concurrent Users", "Number of users logging in system");

        public void SignIn(string userName, string[] roleIDs = null)
        {
            SignOut();
            MakeSureSingleLogin(userName);
            System.Web.Security.FormsAuthentication.SetAuthCookie(userName, false);
            System.Security.Principal.GenericIdentity identity = new System.Security.Principal.GenericIdentity(userName);
            System.Security.Principal.GenericPrincipal user = new System.Security.Principal.GenericPrincipal(identity, roleIDs);
            System.Web.HttpContext.Current.User = user;

            CCUMonitor.Increase();
        }

        public void SignOut()
        {
            if (System.Web.HttpContext.Current == null)
                return;

            //Single Login Start
            var application = System.Web.HttpContext.Current.Application;
            var Session = System.Web.HttpContext.Current.Session;

            //put your logout logic here, remove the user object from the session.
            Hashtable sessions = (Hashtable)application["WEB_SESSIONS_OBJECT"];
            if (sessions == null)
            {
                sessions = new Hashtable();
            }

            if (Profiles.MyProfile != null &&
                Profiles.MyProfile.Employee != null &&
                sessions.Contains(Profiles.MyProfile.UserName))
            {
                sessions.Remove(Profiles.MyProfile.UserName);
            }

            application.Lock();
            application["WEB_SESSIONS_OBJECT"] = sessions;
            application.UnLock();
            //Single Login End

            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.User = null;

            CCUMonitor.Decrease();
        }

        private void MakeSureSingleLogin(string userName)
        {
            //Single Login Start
            var Application = System.Web.HttpContext.Current.Application;
            var Session = System.Web.HttpContext.Current.Session;

            //getting the sessions objects from the Application
            Hashtable sessions = (Hashtable)Application["WEB_SESSIONS_OBJECT"];
            if (sessions == null)
            {
                sessions = new Hashtable();
            }

            //getting the pointer to the Session of the current logged in user
            HttpSessionState existingUserSession = (HttpSessionState)sessions[userName];
            if (existingUserSession != null)
            {
                existingUserSession["UserProfileSession"] = null; // ten session fix cung (khong duoc doi)
                //logout current logged in user
            }
            else
            {
                CCUMonitor.Increase();//vi tru 1 lan luc goi signout truoc
            }

            //putting the user in the session
            sessions[userName] = Session;
            Application.Lock(); //lock to prevent duplicate objects
            Application["WEB_SESSIONS_OBJECT"] = sessions;
            Application.UnLock();
            //Single Login End
        }
    }
}
