using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Core.Security;
using SoftMart.Core.Security.Entity;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SM.SmartInfo.UI
{
    public class BasePagePopup : UnsecuredPage
    {
        public List<IFunctionRight> GetPagePermission()
        {
            PermissionParam param = new PermissionParam(PermissionType.AccessPage);
            PermissionController.Provider.Execute(param);

            return param.FunctionRights.ToList<IFunctionRight>();
        }
        public string GetParamCallbackButton(string paramName = "callback")
        {
            string encodedVal = System.Web.HttpContext.Current.Request.Params[paramName];
            if (string.IsNullOrEmpty(encodedVal))
            {
                return string.Empty;
            }
            else
            {
                return HtmlUtils.DecodeHtml(encodedVal);
            }
        }

        public void ClickParentButton(string parentButtonClientID, string responseUrl = "")
        {
            string script = string.Empty;
            if (!string.IsNullOrWhiteSpace(parentButtonClientID))
            {
                script = string.Format("clickParentButton('{0}'); ", parentButtonClientID);
            }

            if (!string.IsNullOrWhiteSpace(responseUrl))
            {
                script += string.Format("location.href='{0}';", responseUrl);
            }

            if (!string.IsNullOrWhiteSpace(script))
            {
                if (ScriptManager.GetCurrent(this.Page) == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "clickParentButton", script, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "clickParentButton", script, true);
                }
            }
        }

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