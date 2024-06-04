using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.PermissionManager.UserAuthentication;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Permission;
using System;

namespace SM.SmartInfo.PermissionManager
{
    public class PermissionController
    {
        private static PermissionController _provider = null;

        private PermissionController() { }

        public static PermissionController Provider
        {
            get
            {
                if (_provider == null)
                    _provider = new PermissionController();

                return _provider;   
            }
        }

        public void Execute(PermissionParam param)
        {
            string function = string.Format("Permission: {0}", param.Type.ToString());
            using (var logger = new SoftMart.Core.Utilities.Diagnostics.PLogger(function))
            {
                IPermission permission = new BasePermission();
                switch (param.Type)
                {
                    case PermissionType.AccessPage:
                        permission.CheckPagePermission(param);
                        break;
                    case PermissionType.AccessItem:
                        permission.CheckItemPermission(param);
                        break;
                    case PermissionType.AccessView:
                        permission.GetTemporaryViewDataPermission(param);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Execute(AuthenticationParam param)
        {
            var cBiz = new UserAuthenticationBiz();

            switch (param.FunctionType)
            {
                case FunctionType.Authentication.AutoLogin:
                    cBiz.AutoLogin(param);
                    break;
                case FunctionType.Authentication.Login:
                    cBiz.Login(param);
                    break;
                case FunctionType.Authentication.LoginBySinhTracHoc:
                    cBiz.LoginBySinhTracHoc(param);
                    break;
                case FunctionType.Authentication.LoginAPI:
                    cBiz.LoginStateless(param);
                    break;
                case FunctionType.Authentication.Logout:
                    cBiz.Logout();
                    break;
                case FunctionType.Authentication.ChangePassword:
                    cBiz.ChangePassword(param);
                    break;
                case FunctionType.Authentication.ResetPassword:
                    cBiz.SetPassword(param);
                    break;
                case FunctionType.Authentication.GetLog:
                    cBiz.GetLog(param);
                    break;
                case FunctionType.Authentication.DeleteLog:
                    cBiz.DeleteLog(param);
                    break;
                case FunctionType.Authentication.LockUser:
                    cBiz.LockedUser(param);
                    break;
                case FunctionType.Authentication.UnlockUser:
                    cBiz.UnlockUser(param);
                    break;
                case FunctionType.Authentication.CheckValidUser:
                    cBiz.CheckValidUser(param);
                    break;
                case FunctionType.Authentication.UpdateLoggingAttemp:
                    cBiz.UpdateLoggingAttemp(param);
                    break;
                case FunctionType.Authentication.GetLoggingAttemptByUsername:
                    cBiz.GetLoggingAttemptByUsername(param);
                    break;
                default:
                    throw new Exception(string.Format("FunctionType is not supported: {0}", param.FunctionType));
            }
        }
    }
}
