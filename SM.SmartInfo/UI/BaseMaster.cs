using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SM.SmartInfo.UI
{
    public abstract class BaseMaster : MasterPage
    {
        public abstract void ShowError(List<string> lstMsg, string sumary = "");

        public abstract void ShowMessage(string msg);
    }
}