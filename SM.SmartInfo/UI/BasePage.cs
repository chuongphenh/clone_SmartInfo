using System.Linq;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SoftMart.Core.Security.Entity;
using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI
{
    public abstract class BasePage : SoftMart.Core.Security.SecuredPage
    {
        #region Check permission

        protected List<FunctionRight> _lstFunctionRight
        {
            get { return base.FunctionRights.Cast<FunctionRight>().ToList(); }
        }

        protected List<FunctionRight> _lstDataRight
        {
            set { base.DataRights = value == null ? null : value.ToList<IFunctionRight>(); }
        }

        protected override void HandleNotLogin()
        {
            string oldPage = Request.Url.PathAndQuery;
            string newPage = string.Format(PageURL.LoginPageWithReturn, Server.UrlEncode(oldPage));
            Response.Redirect(newPage);
        }

        protected override void ResponsePermission()
        {
            base.ResponsePermission();
        }

        protected override void HandleNoPermission()
        {
            //Bo rem de chuyen sang trang loi neu ko co quyen
            Response.Redirect(PageURL.ErrorPage);
        }

        protected override List<IFunctionRight> GetPagePermission()
        {
            PermissionParam param = new PermissionParam(PermissionType.AccessPage);
            PermissionController.Provider.Execute(param);

            return param.FunctionRights.ToList<IFunctionRight>();
        }

        //Bo rem de hien thi tat ca control
        //protected override bool SetPermission(object objControl, string code)
        //{
        //    SoftMart.Core.Utilities.ReflectionHelper.SetProperty(objControl, "Visible", true);
        //    return true;
        //}

        protected void RequestItemPermission(int? itemID)
        {
            PermissionParam param = new PermissionParam(PermissionType.AccessItem);
            param.ItemID = itemID;
            param.FunctionRights = _lstFunctionRight;
            param.FunctionCodes = FunctionCodeMapping.Values.ToList();

            PermissionController.Provider.Execute(param);

            //Store Rights on Item
            _lstDataRight = param.FunctionRights;
        }

        #endregion

        #region Url parameter

        /// <summary>
        /// SMX.smx_ID
        /// Get ID param on the query string and then convert value to int.
        /// Throw a SMXException if the conversion fail
        /// </summary>
        /// <param name="paramName">Param name on the query string</param>
        /// <returns></returns>
        protected int GetIntIdParam(string paramName = SMX.Parameter.ID)
        {
            return SM.SmartInfo.UI.UIUtility.GetIntIdParam(paramName);
        }

        /// <summary>
        /// Value: DecodeHtml
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        protected string GetParamId(string paramName = SMX.Parameter.ID)
        {
            return SM.SmartInfo.UI.UIUtility.GetParamId(paramName);
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

        #region Request permission - post back
        protected void RequestItemPermissionPostBack(int? itemID, params string[] arrControlID)
        {
            if (arrControlID == null || arrControlID.Length == 0 || itemID == null)
                return;

            string controlName = Request.Params["__EVENTTARGET"];
            if (string.IsNullOrEmpty(controlName))
            {
                System.Web.UI.Control foundControl;
                foreach (string ctl in Request.Form)
                {
                    string controlID = ctl;

                    // handle ImageButton they having an additional "quasi-property" 
                    // in their Id which identifies mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                        controlID = ctl.Substring(0, ctl.Length - 2);

                    foundControl = FindControl(controlID);
                    if (foundControl is System.Web.UI.WebControls.IButtonControl)
                    {
                        controlName = foundControl.ID;
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(controlName))
                return;

            bool isNeedRequestPermission = false;
            foreach (string checkControlID in arrControlID)
            {
                if (controlName.EndsWith(checkControlID, System.StringComparison.OrdinalIgnoreCase))
                {
                    isNeedRequestPermission = true;
                    break;
                }
            }

            if (isNeedRequestPermission)
                RequestItemPermission(itemID);
        }
        #endregion
    }
}