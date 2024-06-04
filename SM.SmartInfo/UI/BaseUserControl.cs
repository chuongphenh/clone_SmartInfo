using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        #region Log Performance

        SoftMart.Core.Utilities.Diagnostics.PLogger _logger;

        public BaseUserControl()
            : base()
        {
            string ucName = System.IO.Path.GetFileName(this.GetType().BaseType.Name) + ".ascx";
            _logger = new SoftMart.Core.Utilities.Diagnostics.PLogger(ucName);
        }

        public override void Dispose()
        {
            _logger.Dispose();
            base.Dispose();
        }

        #endregion

        #region Show notification

        public void ShowError(string msg)
        {
            ShowError(new List<string>() { msg });
        }

        public void ShowError(SMXException ex)
        {
            if (ex.ListMessage != null)
            {
                ShowError(ex.ListMessage);
            }
            else
            {
                ShowError(new List<string>() { ex.Message });
            }
        }

        public void ShowError(List<string> lstMsg, string sumary = "")
        {
            BaseMaster master = (BaseMaster)this.Page.Master;
            master.ShowError(lstMsg, sumary);
        }

        public void ShowMessage(string msg)
        {
            BaseMaster master = (BaseMaster)this.Page.Master;
            master.ShowMessage(msg);
        }

        #endregion
    }
}