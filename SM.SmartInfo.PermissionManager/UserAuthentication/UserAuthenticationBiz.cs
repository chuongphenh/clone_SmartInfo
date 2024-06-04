using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.PermissionManager.Dao;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Permission;
using SM.SmartInfo.Utils;
using SoftMart.Core.Utilities.Diagnostics;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SM.SmartInfo.PermissionManager.UserAuthentication
{
    class UserAuthenticationBiz
    {
        private EmployeeLogDao _dao = new EmployeeLogDao();

        #region Login - stateable

        public void Login(AuthenticationParam param)
        {
            string username = param.UserName;
            string password = param.Password;

            param.Employee = Login(username, password, true);
        }

        public void LoginBySinhTracHoc(AuthenticationParam param)
        {
            string username = param.UserName;
            string devideName = param.DeviceName;
            string guid = param.Guid;
            string udid = param.UDID;
            param.Employee = LoginBySinhTracHoc(username, devideName, guid, udid);
        }

        public void AutoLogin(AuthenticationParam param)
        {
            string username = param.UserName;
            string password = string.Empty;

            Login(username, password, false);
        }

        /*private Employee Login(string username, string password, bool isCheckAD)
        {
            // reset profile
            Profiles.SetMyProfile(null);

            // validate with auth-service
            string strIP = GetIPAddress();
            Employee employee = VerifyAndGetEmployee(username, password, strIP, isCheckAD);
            if (employee == null)
                return employee;

            // get profile
            int clientNetworkType = GetNetworkType(strIP);
            var profile = GetProfile(employee, clientNetworkType);

            // sign-in
            MembershipHelper.FormService.SignIn(employee.Username);

            // keep log
            EmployeeLog log = InsertLog(employee, strIP);
            profile.EmployeeLog = log;

            // cache profile
            Profiles.SetMyProfile(profile);

            return employee;
        }*/

        private Employee Login(string username, string password, bool isCheckAD)
        {
            // reset profile
            Profiles.SetMyProfile(null);

            // validate with auth-service
            //string strIP = GetIPAddress();
            string strIP = "::1";
            Employee employee = VerifyAndGetEmployee(username, password, strIP, isCheckAD);
            if (employee == null)
                return employee;

            // get profile
            //int clientNetworkType = GetNetworkType(strIP);
            int clientNetworkType = 1;
            var profile = GetProfile(employee, clientNetworkType);

            // sign-in
            MembershipHelper.FormService.SignIn(employee.Username);

            // keep log
            EmployeeLog log = InsertLog(employee, strIP);
            profile.EmployeeLog = log;

            // cache profile
            Profiles.SetMyProfile(profile);

            return employee;
        }

        private Employee LoginBySinhTracHoc(string username, string devideName, string guid, string udid)
        {
            // reset profile
            Profiles.SetMyProfile(null);

            // validate with auth-service
            string strIP = GetIPAddress();
            Employee employee = VerifyAndGetEmployee(username, devideName, guid, strIP, udid);
            if (employee == null)
                return employee;

            // get profile
            int clientNetworkType = GetNetworkType(strIP);
            var profile = GetProfile(employee, clientNetworkType);

            // sign-in
            MembershipHelper.FormService.SignIn(employee.Username);

            // keep log
            EmployeeLog log = InsertLog(employee, strIP);
            profile.EmployeeLog = log;

            // cache profile
            Profiles.SetMyProfile(profile);

            return employee;
        }

        private Employee VerifyAndGetEmployee(string username, string password, string fromIPAddress, bool isCheckAD)
        {
            // validate IP
            List<SystemParameter> lstBlockIP = CacheManager.GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_BlockIP);
            bool isBlocked = lstBlockIP.Exists(c => string.Equals(c.Name, fromIPAddress) &&
                c.Deleted == SMX.smx_IsNotDeleted && c.Status == SMX.Status.Active);
            if (isBlocked)
            {
                LogManager.WebLogger.LogDebug(string.Format("IP [{0}] dang bi chan", fromIPAddress));
                return null;
            }

            // validate user with DB
            string userName = MembershipHelper.GetRealUserName(username);
            EmployeeDao daoUser = new EmployeeDao();
            Employee employee = daoUser.GetActiveEmployee(userName);

            if (employee == null)
            {
                LogManager.WebLogger.LogDebug("Khong tim thay nguoi dung trong he thong");
                return null;
            }
            if (employee.IsLocked == false)
            {
                LogManager.WebLogger.LogDebug("Người dùng đang bị khóa");
                return null;
            }

            // validate user with AD
            bool isValid = true;
            if (isCheckAD)
            {
                var membership = MembershipHelper.GetMembershipService(employee.AuthorizationType);
                isValid = membership.ValidateUser(username, password, employee.LdapCnnName);
                if (!isValid)
                {
                    LogManager.WebLogger.LogDebug("Tên đăng nhập/Mật khẩu không đúng.");
                    return null;
                }
            }

            return employee;
        }

        private Employee VerifyAndGetEmployee(string username, string deviceName, string guid, string fromIPAddress, string udid)
        {
            // validate IP
            List<SystemParameter> lstBlockIP = CacheManager.GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_BlockIP);
            bool isBlocked = lstBlockIP.Exists(c => string.Equals(c.Name, fromIPAddress) &&
                c.Deleted == SMX.smx_IsNotDeleted && c.Status == SMX.Status.Active);
            if (isBlocked)
            {
                LogManager.WebLogger.LogDebug(string.Format("IP [{0}] dang bi chan", fromIPAddress));
                return null;
            }

            // validate user with DB
            string userName = MembershipHelper.GetRealUserName(username);
            EmployeeDao daoUser = new EmployeeDao();
            Employee employee = daoUser.GetActiveEmployee(userName);
            if (employee == null)
            {
                LogManager.WebLogger.LogDebug("Khong tim thay nguoi dung trong he thong");
                return null;
            }
            if (employee.IsLocked == false)
            {
                LogManager.WebLogger.LogDebug("Người dùng đang bị khóa");
                return null;
            }
            if (employee.UDID == null || employee.IsCheckFinger == null || employee.IsCheckFinger == false || employee.DeviceName == null || employee.Guid == null)
            {
                throw new SMXException("Người dùng chưa đăng ký sử dụng đăng nhập bằng sinh trắc học.");
                //LogManager.WebLogger.LogDebug("Người dùng chưa đăng ký sử dụng đăng nhập bằng sinh trắc học");
                //return null;
            }
            if (!deviceName.Equals(employee.DeviceName) || !udid.Equals(employee.UDID) || !guid.Equals(employee.Guid))
            {
                throw new SMXException("Dữ liệu đã bị thay đổi bởi người dùng khác.");
            }

            MembershipHelper.FormService.SignIn(employee.Username);
            // keep log
            EmployeeLog log = InsertLog(employee, fromIPAddress);

            return employee;
        }

        private UserProfile GetProfile(Employee employee, int clientNetworkType)
        {
            int employeeID = employee.EmployeeID.Value;

            //Get Role
            RoleDao daoRole = new RoleDao();
            List<Role> lstRole = daoRole.GetRolesOfEmployee(employeeID);

            //Get Right
            RightDao daoRight = new RightDao();
            List<FunctionRight> lstRight = daoRight.GetRightsOfEmployee(employeeID);

            //Get Feature
            FeatureDao daoFeature = new FeatureDao();
            List<int> lstFeatureID = lstRight.Select(c => c.FeatureID).Distinct().ToList();
            List<Feature> lstFeature = daoFeature.GetActiveFeaturesByID(lstFeatureID, clientNetworkType);

            // FeatureFunction
            List<int> lstFeatureFunctionID = lstRight.Select(c => c.FeatureFunctionID).Distinct().ToList();
            List<FeatureFunction> lstFeatureFunction = daoFeature.GetFeatureFunctionsByID(lstFeatureFunctionID);

            // Function
            List<int> lstFunctionID = lstRight.Select(c => c.FunctionID).Distinct().ToList();
            List<Function> lstFunction = daoFeature.GetFunctionsByID(lstFunctionID);

            OrganizationDAO daoOrg = new OrganizationDAO();
            List<int> lstManaingOrganizationID = daoOrg.GetListManagingOrganizationID(employeeID); // ManagingOrganizationIDs
            List<int> lstOwningOrganizationID = daoOrg.GetListOwningOrganizationID(employeeID); // OwningOrganizationIDs
            int? organizationID = daoOrg.GetOrganizationOfEmployee(employeeID);
            Organization org = new Organization();
            if (organizationID.HasValue)
                org = daoOrg.GetOrganizationByID(organizationID.Value);

            // Fixed BussinesCode
            List<int> lsRoleID = lstRole.Where(en => en.RoleID.HasValue).Select(en => en.RoleID.Value).ToList();
            //List<string> lsFixedPermissionCode = daoRole.GetFixedPermissionCode(lsRoleID);

            List<SystemSupporting> lstSup = new List<SystemSupporting>();
            SystemSupporting sup = null;
            if (lstSup.Count > 0 && lstRole.Count > 0)
            {
                Role roleSupport = lstRole.Where(d => String.Equals(d.Name, "Hỗ trợ hệ thống", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (roleSupport != null)
                    sup = lstSup.Where(c => c.RoleID == roleSupport.RoleID).FirstOrDefault();
            }
            //Create Profile
            UserProfile profile = new UserProfile();

            profile.Employee = employee;
            profile.EmployeeID = employee.EmployeeID.Value;
            profile.UserName = employee.Username;
            profile.FullName = employee.Name;
            profile.Roles = lstRole;
            profile.EmployeeRights = lstRight;
            profile.Features = lstFeature;
            profile.FeatureFunctions = lstFeatureFunction;
            profile.Functions = lstFunction;
            profile.ListAllManagingOrganizationID = lstManaingOrganizationID;
            profile.ListDirectManagingOrganizationID = lstOwningOrganizationID;
            profile.OrganizationID = organizationID;
            profile.Organization = org;
            //profile.ListFixedPermissionCode = lsFixedPermissionCode;
            profile.ClientNetworkType = clientNetworkType;
            profile.SystemSupporting = sup;

            return profile;
        }

        private string GetIPAddress()
        {
            string visitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                visitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return visitorsIPAddr;
        }

        private int GetNetworkType(string ipAddress)
        {
            try
            {
                int networkLAN = 1;
                int networkWAN = 2;

                if (string.IsNullOrWhiteSpace(ipAddress))
                    return networkWAN;

                /*if (ipAddress == "::1" || ipAddress == "127.0.0.1")// special: localhost
                    return networkLAN;*/

                string strLAN = ConfigUtils.GetConfig("LANIP");
                if (string.IsNullOrWhiteSpace(strLAN))
                {
                    // https://en.wikipedia.org/wiki/Private_network
                    strLAN = "10.0.0.0|10.255.255.255;172.16.0.0|172.31.255.255;192.168.0.0|192.168.255.255";
                }

                long ip = BitConverter.ToInt32(System.Net.IPAddress.Parse(ipAddress).GetAddressBytes().Reverse().ToArray(), 0);
                string[] arrLAN = strLAN.Split(';');
                foreach (string arrangeLAN in arrLAN)
                {
                    if (string.IsNullOrWhiteSpace(arrangeLAN))
                        continue;
                    string[] arrIP = arrangeLAN.Trim().Split('|');
                    if (arrIP.Length != 2)
                        continue;

                    bool isInRange = IsInRangeIP(arrIP[0].Trim(), arrIP[1].Trim(), ip);
                    if (isInRange)
                        return networkLAN;
                }

                return networkWAN;
            }
            catch(SMXException ex)
            {
                LogManager.WebLogger.LogError("Log in failed", ex);
                return -1;
            }
        }

        private bool IsInRangeIP(string startRangeIP, string endRangeIP, long checkIP)
        {
            long startRange = BitConverter.ToInt32(System.Net.IPAddress.Parse(startRangeIP).GetAddressBytes().Reverse().ToArray(), 0);
            long endRange = BitConverter.ToInt32(System.Net.IPAddress.Parse(endRangeIP).GetAddressBytes().Reverse().ToArray(), 0);

            return checkIP >= startRange && checkIP <= endRange;
        }
        #endregion

        #region Login - stateless (API)
        public void LoginStateless(AuthenticationParam param)
        {
            string username = param.UserName;
            string password = param.Password;

            // validate user with DB
            string userName = MembershipHelper.GetRealUserName(username);
            EmployeeDao daoUser = new EmployeeDao();
            Employee employee = daoUser.GetActiveEmployee(userName);
            if (employee != null)
            {
                if (employee.IsLocked == false)
                {
                    LogManager.WebLogger.LogDebug("Người dùng đang bị khóa");
                    employee = null;
                }
                bool isValid = true;

#if !DEBUG
                var membership = MembershipHelper.GetMembershipService(employee.AuthorizationType);
                isValid = membership.ValidateUser(username, password, employee.LdapCnnName);
#endif

                if (!isValid)
                {
                    LogManager.WebLogger.LogDebug("Khong trust duoc voi AD");
                    employee = null;
                }
            }

            param.Employee = employee;
        }

        public void GetActiveFeatureFunction(AuthenticationParam param)
        {

        }
        #endregion

        #region Log-out

        public void Logout()
        {
            UserProfile profile = Profiles.MyProfile;
            if (profile != null)
            {
                // keep log
                EmployeeLog log = profile.EmployeeLog;
                log.SignOutDTG = DateTime.Now;
                _dao.UpdateEmployeeLog(log);
            }

            // sign-out
            MembershipHelper.FormService.SignOut();

            // clean session: http://support.microsoft.com/kb/899918
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            HttpContext.Current.Session.Clear();

            // clean cache
            Profiles.SetMyProfile(null);
        }

        #endregion

        #region Change pass
        public void ChangePassword(AuthenticationParam param)
        {
            if (param.NewPassword.Length < 8)
                throw new SMXException("Mật khẩu ít nhất 8 ký tự");
            EmployeeDao dao = new EmployeeDao();
            bool isValid = false;
            string userName = MembershipHelper.GetRealUserName(param.UserName);
            Employee emp = dao.GetActiveEmployee(userName);
            if (emp != null)
            {
                var membership = MembershipHelper.GetMembershipService(emp.AuthorizationType);
                isValid = membership.ChangePassword(param.UserName, param.OldPassword, param.NewPassword, emp.LdapCnnName);
            }

            if (!isValid)
                throw new SMXException("Không thay đổi được mật khẩu. Hãy kiểm tra lại thông tin nhập hoặc liên hệ admin để được hỗ trợ");
        }

        private bool IsValidPassword(string plainText)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(.{0,7}|[^0-9]*|[^A-Z])$");
            System.Text.RegularExpressions.Match match = regex.Match(plainText);
            return match.Success;
        }
        /// <summary>
        /// At least one lower case letter,
        ///  At least one upper case letter,
        ///   At least special character,
        ///  At least one number
        ///At least 8 characters length
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private bool ValidatePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        public void SetPassword(AuthenticationParam param)
        {
            EmployeeDao dao = new EmployeeDao();
            bool isValid = false;
            string userName = MembershipHelper.GetRealUserName(param.UserName);
            string newPass = param.NewPassword;
            if (!ValidatePassword(newPass))
            {
                List<string> lstErr = new List<string>();
                lstErr.Add("Phải có ít nhất 1 ký tự thường");
                lstErr.Add("Phải có ít nhất 1 ký tự hoa");
                lstErr.Add("Phải có ít nhất 1 ký tự đặc biệt");
                lstErr.Add("Phải có ít nhất 1 ký tự chữ số");
                lstErr.Add("Phải có ít nhất 8 ký");
                throw new SMXException(lstErr);
            }
            Employee emp = dao.GetActiveEmployee(userName);
            if (emp != null)
            {
                SystemParameter sysImpersonateAcc = GlobalCache.GetItemByFeatureIDAndCode(SMX.Features.smx_HomePage, SMX.FixedSPCode.Impersonate.ImpersonateAccount);
                SystemParameter sysImpersonatePass = GlobalCache.GetItemByFeatureIDAndCode(SMX.Features.smx_HomePage, SMX.FixedSPCode.Impersonate.ImpersonatePassword);
                string adminUser = sysImpersonateAcc == null ? string.Empty : sysImpersonateAcc.Ext1;
                string adminPass = sysImpersonatePass == null ? string.Empty : sysImpersonatePass.Ext1;

                var membership = MembershipHelper.GetMembershipService(emp.AuthorizationType);
                isValid = membership.SetPassword(adminUser, adminPass, userName, newPass, emp.LdapCnnName);
            }

            if (!isValid)
                throw new SMXException("Không thay đổi được mật khẩu. Hãy kiểm tra lại thông tin nhập hoặc liên hệ admin để được hỗ trợ");
        }
        #endregion

        #region Log history
        public EmployeeLog InsertLog(Employee employee, string strIP)
        {
            EmployeeLog log = new EmployeeLog();
            log.EmployeeID = employee.EmployeeID;
            log.IPAddress = strIP;
            log.SignInDTG = DateTime.Now;
            _dao.InsertEmployeeLog(log);

            return log;
        }

        public void GetLog(AuthenticationParam param)
        {
            if (param.FromDTG != null)
                param.FromDTG = new DateTime(param.FromDTG.Value.Year, param.FromDTG.Value.Month, param.FromDTG.Value.Day, 0, 0, 0);
            if (param.ToDTG != null)
                param.ToDTG = new DateTime(param.ToDTG.Value.Year, param.ToDTG.Value.Month, param.ToDTG.Value.Day, 23, 59, 59);
            param.UserName = Profiles.MyProfile.UserName;
            param.EmployeeId = Profiles.MyProfile.EmployeeID;

            _dao.GetLog(param);
        }

        public void DeleteLog(AuthenticationParam param)
        {
            if (param.FromDTG != null)
                param.FromDTG = new DateTime(param.FromDTG.Value.Year, param.FromDTG.Value.Month, param.FromDTG.Value.Day, 0, 0, 0);
            if (param.ToDTG != null)
                param.ToDTG = new DateTime(param.ToDTG.Value.Year, param.ToDTG.Value.Month, param.ToDTG.Value.Day, 23, 59, 59);

            _dao.DeleteLog(param);
        }
        #endregion

        public void LockedUser(AuthenticationParam param)
        {
            string username = param.UserName;
            string password = param.Password;

            string userName = MembershipHelper.GetRealUserName(username);
            EmployeeDao daoUser = new EmployeeDao();
            Employee employee = daoUser.GetActiveEmployee(userName);
            if (employee == null)
            {
                LogManager.WebLogger.LogDebug("Khong tim thay nguoi dung trong he thong");
                param.Employee = null;
                return;
            }
            else
            {
                /*if (employee.IsLocked != false)
                {
                    employee.UpdatedDTG = DateTime.Now;
                    employee.UpdatedBy = "System";
                    employee.UnlockedTime = param.UnlockedTime;

                    daoUser.LockedUser(employee);

                    LogManager.WebLogger.LogDebug("Khóa người dùng");
                }*/

                employee.UpdatedDTG = DateTime.Now;
                employee.UpdatedBy = "System";
                employee.UnlockedTime = param.UnlockedTime;

                daoUser.LockedUser(employee);

                LogManager.WebLogger.LogDebug("Khóa người dùng");

                param.Employee = employee;
            }
        }

        public void UnlockUser(AuthenticationParam param)
        {
            string username = param.UserName;
            string password = param.Password;

            string userName = MembershipHelper.GetRealUserName(username);
            EmployeeDao daoUser = new EmployeeDao();
            Employee employee = daoUser.GetActiveEmployee(userName);
            if (employee == null)
            {
                LogManager.WebLogger.LogDebug("Khong tim thay nguoi dung trong he thong");
                param.Employee = null;
                return;
            }
            else
            {
                /*if (employee.IsLocked != true)
                {
                    employee.UpdatedDTG = DateTime.Now;
                    employee.UpdatedBy = "System";
                    daoUser.UnLockedUser(employee);
                    LogManager.WebLogger.LogDebug("Mở khóa người dùng");
                }*/

                employee.UpdatedDTG = DateTime.Now;
                employee.UpdatedBy = "System";
                daoUser.UnLockedUser(employee);
                LogManager.WebLogger.LogDebug("Mở khóa người dùng");

                param.Employee = employee;
            }
        }

        public void CheckValidUser(AuthenticationParam param)
        {
            try
            {
                param.StatusCode = _dao.CheckValidUser(param);
            }
            catch(Exception ex)
            {
                return;
            }

        }
        
        public void UpdateLoggingAttemp(AuthenticationParam param)
        {
            try
            {
                _dao.UpdateLoggingAttemp(param);
            }
            catch(Exception ex)
            {

            }
        }
        
        public void GetLoggingAttemptByUsername(AuthenticationParam param)
        {
            try
            {
                param.Employee = _dao.GetLoggingAttemptByUsername(param);
            }
            catch(Exception ex)
            {

            }
        }
    }
}