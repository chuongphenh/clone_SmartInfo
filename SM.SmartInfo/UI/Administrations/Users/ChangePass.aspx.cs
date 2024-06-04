
using System;
using System.Collections.Generic;
using System.Web.UI;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Params.Permission;

namespace SM.SmartInfo.UI.Administrations.Users
{
    public partial class ChangePass : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    ltrUserName.Text = Profiles.MyProfile.UserName;
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> lstMG = new List<string>();

                if (string.IsNullOrWhiteSpace(txtOldPassword.Text))
                    lstMG.Add("Bạn chưa nhập mật khẩu cũ <br/>");
                if (string.IsNullOrWhiteSpace(txtNewPassword1.Text))
                    lstMG.Add("Bạn chưa nhập mật khẩu mới<br/>");
                if (string.IsNullOrWhiteSpace(txtNewPassword2.Text))
                    lstMG.Add("Bạn chưa nhập lại mật khẩu mới<br/>");
                if (txtNewPassword1.Text.Trim() != txtNewPassword2.Text.Trim())
                    lstMG.Add("Mật khẩu mới và mật khẩu nhập lại không trùng nhau<br/>");

                if (lstMG.Count > 0)
                    throw new SMXException(lstMG);

                var param = new AuthenticationParam(FunctionType.Authentication.ChangePassword);
                param.UserName = Profiles.MyProfile.UserName;
                param.OldPassword = txtOldPassword.Text;
                param.NewPassword = txtNewPassword1.Text;
                PermissionManager.PermissionController.Provider.Execute(param);

                ucErr.ShowError("Đổi mật khẩu thành công. Vui lòng đăng nhập lại với mật khẩu mới.");
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
    }
}