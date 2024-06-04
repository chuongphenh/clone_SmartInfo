using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Configurations.RuleEngines
{
    public partial class Edit : BasePage
    {
        #region Page event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupForm();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateItem();
                Response.Redirect(string.Format(PageURL.Display, hidID.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        #endregion

        #region Common

        private void SetupForm()
        {
            hplExit.NavigateUrl = PageURL.Default;
            hidID.Value = GetIntIdParam().ToString();
            GetItemForView();
        }

        public void GetItemForView()
        {
            int ruleID = int.Parse(GetParamId(SMX.Parameter.ID));

            ucEditRule.BindRule(ruleID);
            ucEditRule.ConfigDisplay(true, true, true, true, true);
        }

        public void UpdateItem()
        {
            try
            {
                ucEditRule.SaveRule();

                //// yeu cau cap nhat blacklist
                //SharedComponent.Params.Common.CommonParam param = 
                //    new SharedComponent.Params.Common.CommonParam(SharedComponent.Constants.FunctionType.Common.AddBlacklistPriceServiceCommand);
                //param.ServiceRankingCommand = new SharedComponent.Entities.ServiceRankingCommand();
                //param.ServiceRankingCommand.RefID = Utility.GetInt(hidID.Value);
                //BIZ.MainController.Provider.Execute(param);
            }
            catch (Exception ex)
            {
                throw new SMXException(ex.Message);
            }
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this      , FunctionCode.EDIT },
                    { btnSave   , FunctionCode.EDIT },
                };
            }
        }
    }
}