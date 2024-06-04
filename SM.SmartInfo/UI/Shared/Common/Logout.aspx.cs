using System;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Permission;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class Logout : SoftMart.Core.Security.UnsecuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticationParam param = new AuthenticationParam(FunctionType.Authentication.Logout);
            PermissionManager.PermissionController.Provider.Execute(param);

            Response.Redirect(PageURL.LoginPage);
        }
    }
}