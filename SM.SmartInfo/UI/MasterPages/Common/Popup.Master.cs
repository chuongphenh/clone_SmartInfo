using System;
using System.Web.UI;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.MasterPages.Common
{
    public partial class Popup : BaseMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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