
using System;
using System.Configuration;
using System.DirectoryServices;

namespace SM.SmartInfo.PermissionManager.UserAuthentication
{
    #region interface
    interface IMembershipService
    {
        bool ValidateUser(string userName, string password, string ldapCnnName);

        bool ChangePassword(string userName, string oldPassword, string newPassword, string ldapCnnName);

        bool SetPassword(string adminUser, string adminPassword, string userName, string newPassword, string ldapCnnName);
    }
    #endregion

    #region active directory
    class ADMembershipService : IMembershipService
    {
        #region validate user
        public bool ValidateUser(string userName, string password, string ldapCnnName)
        {
            string ldapString = string.Empty;
            int authenticationType = 0;

            try
            {
                GetLdapConnection(ldapCnnName, out ldapString, out authenticationType);

                DirectoryEntry entry = new DirectoryEntry(ldapString, userName, password, (AuthenticationTypes)authenticationType);
                object nativeObject = entry.NativeObject;

                entry.Close();
                entry.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogManager.WebLogger.LogDebug(string.Format("{0}: {1}: {2}", ldapCnnName, authenticationType, userName), ex);
            }

            return false;
        }
        #endregion

        #region password
        public bool ChangePassword(string userName, string oldPassword, string newPassword, string ldapCnnName)
        {
            throw new NotImplementedException();
        }

        public bool SetPassword(string adminUser, string adminPassword, string userName, string newPassword, string ldapCnnName)
        {
            const AuthenticationTypes authenticationTypes = AuthenticationTypes.Secure |
                AuthenticationTypes.Sealing | AuthenticationTypes.ServerBind;

            DirectoryEntry entry = null;
            DirectorySearcher searcher = null;
            DirectoryEntry userEntry = null;
            bool isSucceeded = false;

            Utils.LogManager.WebLogger.LogDebug(string.Format("Change password for user [{0}] by impersonate = [{1}] at LDAP = [{2}]",
                userName, adminUser, ldapCnnName));
            try
            {
                string ldapString = string.Empty;
                int authenticationType = 0;
                GetLdapConnection(ldapCnnName, out ldapString, out authenticationType);
                entry = new DirectoryEntry(ldapString, adminUser, adminPassword, authenticationTypes);

                searcher = new DirectorySearcher(entry);
                searcher.Filter = String.Format("sAMAccountName={0}", MembershipHelper.GetRealUserName(userName));
                searcher.SearchScope = SearchScope.Subtree;
                searcher.CacheResults = false;

                SearchResult searchResult = searcher.FindOne(); ;
                if (searchResult == null)
                {
                    Utils.LogManager.WebLogger.LogDebug("There are no users found");
                    return false;
                }

                userEntry = searchResult.GetDirectoryEntry();
                userEntry.Invoke("SetOption", new object[] { 6, 389 }); // ADS_OPTION_PASSWORD_PORTNUMBER, Port
                userEntry.Invoke("SetOption", new object[] { 7, 1 }); // ADS_OPTION_PASSWORD_METHOD, ADS_PASSWORD_ENCODE_CLEAR
                userEntry.Invoke("SetPassword", new object[] { newPassword });
                userEntry.CommitChanges();

                isSucceeded = true;
            }
            catch (Exception ex)
            {
                Utils.LogManager.WebLogger.LogDebug("ChangePassword failed.", ex);
            }
            finally
            {
                if (userEntry != null) userEntry.Dispose();
                if (searcher != null) searcher.Dispose();
                if (entry != null) entry.Dispose();
            }

            return isSucceeded;
        }
        #endregion

        private void GetLdapConnection(string ldapCnnName, out string ldapString, out int authenticationType)
        {
            ConnectionStringSettings ldapConnection = ConfigurationManager.ConnectionStrings[ldapCnnName];

            // connection string
            ldapString = ldapConnection.ConnectionString;

            // authentication type
            authenticationType = 0;
            string strAuthen = ldapConnection.ProviderName;
            if (!int.TryParse(strAuthen, out authenticationType))
                authenticationType = 0;
        }
    }
    #endregion

    #region sql/ local
    class SQLMembershipService : IMembershipService
    {
        public bool ValidateUser(string userName, string password, string ldapCnnName)
        {
            string realUserName = MembershipHelper.GetRealUserName(userName);
            string passwordHash = MembershipHelper.GetPasswordHash(realUserName, password);

            Dao.EmployeeDao dao = new Dao.EmployeeDao();
            var emp = dao.GetActiveEmployee(realUserName);

            if (emp != null && emp.Password == passwordHash)
                return true;

            return false;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword, string ldapCnnName)
        {
            string realUserName = MembershipHelper.GetRealUserName(userName);
            Dao.EmployeeDao dao = new Dao.EmployeeDao();
            var emp = dao.GetActiveEmployee(realUserName);

            if (emp == null)
            {
                Utils.LogManager.WebLogger.LogDebug("There are no users found");
                return false;
            }

            string passwordHash = MembershipHelper.GetPasswordHash(realUserName, oldPassword);
            if (passwordHash != emp.Password)
            {
                Utils.LogManager.WebLogger.LogDebug("Old password not match");
                return false;
            }

            string newPasswordHash = MembershipHelper.GetPasswordHash(realUserName, newPassword);
            emp.Password = newPasswordHash;
            emp.UpdatedBy = CacheManager.Profiles.MyProfile.UserName;
            emp.UpdatedDTG = DateTime.Now;

            bool isSucceeded = false;
            try
            {
                dao.UpdateEmployee(emp);
                isSucceeded = true;
            }
            catch (Exception ex)
            {
                Utils.LogManager.WebLogger.LogDebug("ChangePassword failed.", ex);
                isSucceeded = false;
            }

            return isSucceeded;
        }

        public bool SetPassword(string adminUser, string adminPassword, string userName, string newPassword, string ldapCnnName)
        {
            string realUserName = MembershipHelper.GetRealUserName(userName);
            Dao.EmployeeDao dao = new Dao.EmployeeDao();
            var emp = dao.GetActiveEmployee(realUserName);

            if (emp == null)
            {
                Utils.LogManager.WebLogger.LogDebug("There are no users found");
                return false;
            }

            string newPasswordHash = MembershipHelper.GetPasswordHash(realUserName, newPassword);
            emp.Password = newPasswordHash;
            emp.UpdatedBy = CacheManager.Profiles.MyProfile.UserName;
            emp.UpdatedDTG = DateTime.Now;

            bool isSucceeded = false;
            try
            {
                dao.UpdateEmployee(emp);
                isSucceeded = true;
            }
            catch (Exception ex)
            {
                Utils.LogManager.WebLogger.LogDebug("SetPassword failed.", ex);
                isSucceeded = false;
            }

            return isSucceeded;
        }
    }
    #endregion
}
