using System.Web.UI;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.UserControls.Common
{
    public partial class ErrorHandler : UserControl
    {
        #region Public methods

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

        public void ShowError(List<string> lstMsg)
        {
            string html = string.Empty;
            foreach (string msg in lstMsg)
            {
                html += "<li>" + msg + "</li>";
            }

            html = "<div class=\"err\"><ul>" + html + "</ul></div>";
            ltrMsg.Text = html;

            this.AutoHideClass(".err", 15000);
        }

        public void ShowMessage(string msg)
        {
            string html = "<div class=\"msg\">" + msg + "</div>";
            ltrMsg.Text = html;

            this.AutoFadeMessage();
        }

        private void AutoHideClass(string className, int delay)
        {
            string script = @"$(function () {
                                setTimeout(function () {
                                    $('" + className + @"').hide('blind', {}, 500)
                                }, " + delay.ToString() + @");
                            });";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), string.Empty, script, true);
        }

        private void AutoFadeMessage()
        {
            string script = "setTimeout(function () {$('.msg').animate({opacity: 0}, 3000, function() {});}, 2000);";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), string.Empty, script, true);
        }
        #endregion
    }
}