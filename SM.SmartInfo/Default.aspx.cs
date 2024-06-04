using System;

namespace SM.SmartInfo
{
    public partial class _Default : SoftMart.Core.Security.UnsecuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = SharedComponent.Constants.PageURL.DefaultPage;
            Response.Redirect(url);
        }
    }
}