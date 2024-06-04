using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.UserControls.Common
{
    public partial class ErrorMessageUC : System.Web.UI.UserControl
    {
        #region Properties

        public string ValidationGroup
        {
            get
            {
                return valSum.ValidationGroup;
            }
            set
            {
                valSum.ValidationGroup = value;
            }
        }

        #endregion

        #region Event handlers
        #endregion

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
            foreach (string msg in lstMsg)
            {
                CustomValidator enValidator = new CustomValidator();
                enValidator.ValidationGroup = this.ValidationGroup;
                enValidator.IsValid = false;
                enValidator.ErrorMessage = msg;
                Page.Validators.Add(enValidator);
            }

            this.AutoHideError();
        }

        private void AutoHideError()
        {
            string script = @"$(function () {
                                setTimeout(function () {
                                    $('.err').hide('blind', {}, 500)
                                }, 120000);
                            });";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), string.Empty, script, true);
        }
        #endregion
    }
}