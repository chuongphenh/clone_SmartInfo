using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SM.SmartInfo.DAO.Firebase;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Permission;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class Login : SoftMart.Core.Security.UnsecuredPage
    {
        private const string _SmartInfo_CheckUserName = "SmartInfo_CheckUserName";
        private string _TokenKeyCloak;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string csrfToken = Guid.NewGuid().ToString();
                ViewState["AntiForgeryToken"] = csrfToken;
            }
            Page.Form.DefaultButton = btnLogin.UniqueID;
        }

        public string GetRealUserName(string userName)
        {
            string validUserName = userName; // userName is domain\name, validUserName = name
            if (!string.IsNullOrWhiteSpace(userName) && userName.Contains("\\"))
                validUserName = userName.Substring(userName.LastIndexOf('\\') + 1);

            return validUserName;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string submittedToken = Request.Form["__CSRFToken"];

            string storedToken = (string)ViewState["AntiForgeryToken"];

            if (submittedToken == null || submittedToken != storedToken)
            {
                // Send a 400 Bad Request response
                Response.StatusCode = 400;
                Response.StatusDescription = "Bad Request";
                Response.Write("The request is invalid.");
                Response.End();
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                lblMessage.Text = "Bạn chưa nhập tên đăng nhập";
                divMess.Visible = true;
                return;
            }
            string username = string.Format("bank\\{0}", txtUserName.Text.Trim());

            //Check nhieu lan dang nhap
            username = GetRealUserName(username);
            if (HttpContext.Current.Application[_SmartInfo_CheckUserName] == null || string.IsNullOrWhiteSpace(HttpContext.Current.Application[_SmartInfo_CheckUserName].ToString()))
                HttpContext.Current.Application[_SmartInfo_CheckUserName] = username;
            else
                HttpContext.Current.Application[_SmartInfo_CheckUserName] = username + ";" + HttpContext.Current.Application[_SmartInfo_CheckUserName];
            if (HttpContext.Current.Application[_SmartInfo_CheckUserName] != null || !string.IsNullOrWhiteSpace(HttpContext.Current.Application[_SmartInfo_CheckUserName].ToString()))
            {
                var lstU = HttpContext.Current.Application[_SmartInfo_CheckUserName].ToString().Split(';');
                int cFalse = 0;
                foreach (var user in lstU)
                {
                    if (user.Equals(username))
                        cFalse += 1;
                    else
                    {
                        cFalse = 0;
                        HttpContext.Current.Application[_SmartInfo_CheckUserName] = null;
                        break;
                    }
                    if (cFalse == 10)
                    {
                        var paramLock = new AuthenticationParam(FunctionType.Authentication.LockUser);
                        paramLock.UserName = txtUserName.Text.Trim();
                        paramLock.Password = txtPassword.Text;
                        PermissionManager.PermissionController.Provider.Execute(paramLock);
                        if (paramLock.Employee != null)
                        {
                            divMess.Visible = true;
                            lblMessage.Text = "Bạn đã đăng nhập sai 10 lần liên tiếp. Tài khoản đã bị khóa vui lòng liên hệ với Quản trị để mở lại.";
                        }
                        else
                        {
                            divMess.Visible = true;
                            lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng hoặc đang bị chặn truy cập.";
                        }
                        return;
                    }
                    if (cFalse > 10)
                    {
                        divMess.Visible = true;
                        lblMessage.Text = "Bạn đã đăng nhập sai 10 lần liên tiếp. Tài khoản đã bị khóa vui lòng liên hệ với Quản trị để mở lại.";
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                divMess.Visible = true;
                lblMessage.Text = "Bạn chưa nhập mật khẩu.";
                return;
            }


            //Check nhieu lan dang nhap
            /*username = GetRealUserName(username);
            if (HttpContext.Current.Application[_SmartInfo_CheckUserName] == null || string.IsNullOrWhiteSpace(HttpContext.Current.Application[_SmartInfo_CheckUserName].ToString()))
                HttpContext.Current.Application[_SmartInfo_CheckUserName] = username;
            else
                HttpContext.Current.Application[_SmartInfo_CheckUserName] = username + ";" + HttpContext.Current.Application[_SmartInfo_CheckUserName];
            if (HttpContext.Current.Application[_SmartInfo_CheckUserName] != null || !string.IsNullOrWhiteSpace(HttpContext.Current.Application[_SmartInfo_CheckUserName].ToString()))
            {
                var lstU = HttpContext.Current.Application[_SmartInfo_CheckUserName].ToString().Split(';');

                foreach (var user in lstU)
                {
                    if (!string.IsNullOrEmpty(txtPassword.Text)  && !string.IsNullOrEmpty(username))
                    {
                        //cFalse += 1;

                        var paramCheck = new AuthenticationParam(FunctionType.Authentication.CheckValidUser);
                        paramCheck.UserName = txtUserName.Text.Trim();
                        paramCheck.Password = txtPassword.Text;
                        PermissionManager.PermissionController.Provider.Execute(paramCheck);

                        var getLoggingAttempt = new AuthenticationParam(FunctionType.Authentication.GetLoggingAttemptByUsername);
                        getLoggingAttempt.UserName = txtUserName.Text.Trim();
                        PermissionManager.PermissionController.Provider.Execute(getLoggingAttempt);

                        if (paramCheck.StatusCode == 404)
                        {
                            var paramFailedLoggingAttemp = new AuthenticationParam(FunctionType.Authentication.UpdateLoggingAttemp);
                            paramFailedLoggingAttemp.UserName = txtUserName.Text.Trim();
                            paramFailedLoggingAttemp.Password = txtPassword.Text;
                            PermissionManager.PermissionController.Provider.Execute(paramFailedLoggingAttemp);

                            if (getLoggingAttempt.Employee.LoggingAttemp >= 5 && getLoggingAttempt.Employee.IsLockByLogin == 0)
                            {
                                var paramLock = new AuthenticationParam(FunctionType.Authentication.LockUser);
                                paramLock.UserName = txtUserName.Text.Trim();
                                paramLock.Password = txtPassword.Text;
                                paramLock.UnlockedTime = DateTime.Now.AddMinutes(10);
                                PermissionManager.PermissionController.Provider.Execute(paramLock);

                                divMess.Visible = true;
                                lblMessage.Text = "Tài khoản đã bị khóa trong 10 phút, vui lòng thử lại sau";
                                return;
                            }

                            divMess.Visible = true;
                            lblMessage.Text = "Sai tên đăng nhập hoặc mật khẩu";
                            return;
                        }
                        
                        if(paramCheck.StatusCode == 400)
                        {
                            divMess.Visible = true;
                            lblMessage.Text = "Tài khoản đã bị khóa";
                            return;
                        }
                        
                        if(paramCheck.StatusCode == 500)
                        {
                            divMess.Visible = true;
                            lblMessage.Text = "Có lỗi xảy ra trong quá trình đăng nhập";
                            return;
                        }
                        if (getLoggingAttempt.Employee.LoggingAttemp >= 5 && getLoggingAttempt.Employee.IsLockByLogin == 1 && getLoggingAttempt.Employee.UnlockedTime <= DateTime.Now)
                        {
                            var paramUnLock = new AuthenticationParam(FunctionType.Authentication.UnlockUser);
                            paramUnLock.UserName = txtUserName.Text.Trim();
                            paramUnLock.Password = txtPassword.Text;
                            PermissionManager.PermissionController.Provider.Execute(paramUnLock);
                        }
                    }

                    if (user.Equals(username))
                    {
                        //cFalse += 1;
                    }
                    else
                    {
                        HttpContext.Current.Application[_SmartInfo_CheckUserName] = null;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                divMess.Visible = true;
                lblMessage.Text = "Bạn chưa nhập mật khẩu.";
                return;
            }*/

            var param = new AuthenticationParam(FunctionType.Authentication.Login);
            param.UserName = username;
            param.Password = txtPassword.Text;
            PermissionManager.PermissionController.Provider.Execute(param);

            if (CacheManager.Profiles.MyProfile == null || CacheManager.Profiles.MyProfile.Employee == null)
            {
                divMess.Visible = true;
                lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng hoặc đang bị chặn truy cập.";
                return;
            }



            if (HttpContext.Current.Application[_SmartInfo_CheckUserName] != null)
                HttpContext.Current.Application[_SmartInfo_CheckUserName] = null;

            string[] arrDomain = txtUserName.Text.Split('\\');
            if (arrDomain != null && arrDomain.Length > 0)
            {
                System.Web.HttpCookie cookie = Request.Cookies["SmartInfo_Domain"];
                if (cookie != null)
                    cookie.Value = arrDomain[0];
                else
                {
                    Response.Cookies.Add(new System.Web.HttpCookie("SmartInfo_Domain", arrDomain[0]));
                }
            }

            AntiFixationInit();
            string url = Request.QueryString["ReturnUrl"];
            if (string.IsNullOrWhiteSpace(url))
            {
                Response.Redirect(SharedComponent.Constants.PageURL.DefaultPage);
            }
            else
            {
                url = Server.UrlDecode(url);
                string rootUrl = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                // cac trang nam ngoai ung dung se ko cho redirect
                if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase) || url.StartsWith(rootUrl, StringComparison.OrdinalIgnoreCase) || !url.StartsWith("//"))
                {
                    Task.Run(async () =>
                    {
                        try
                        {
                            await HandleCallKeyCloakAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Exception occurred: {ex}");
                        }
                    }).GetAwaiter().GetResult();
                   
                    Response.Redirect(url);
                }
                else
                {
                    Response.Redirect(SharedComponent.Constants.PageURL.DefaultPage);
                }
            }
        }
        private async Task HandleCallKeyCloakAsync()
        {
            try
            {
                LogManager.ServiceNotification.LogDebug($"START: LOGIN - Calling Keycloak WEB");
                ApiFirebase firebase = new ApiFirebase();
                _TokenKeyCloak = await firebase.GetTokenAsync();
                LogManager.ServiceNotification.LogDebug($"END: Kết thúc gọi keycloak");
                string str = _TokenKeyCloak == null ? "==> Key == null" : " KeyCloak != null";
                LogManager.ServiceNotification.LogDebug($"SUCCESS: LOGIN - Call KeyCloak thành công. {str}");
                LogManager.ServiceNotification.LogDebug($"END: LOGIN - Calling Keycloak WEB");
            }
            catch (Exception ex)
            {
                LogManager.ServiceNotification.LogDebug($"ERROR: LOGIN - Call KeyCloak thất bại {ex}");
            }
        }
        private void AntiFixationInit()
        {
            string value = Guid.NewGuid().ToString();
            System.Web.HttpCookie cookie = Response.Cookies["ASPFIXATION"];
            if (cookie != null)
                cookie.Value = value;
            else
                Response.Cookies.Add(new System.Web.HttpCookie("ASPFIXATION", value));

            Session["ASPFIXATION"] = value;
        }
    }
}