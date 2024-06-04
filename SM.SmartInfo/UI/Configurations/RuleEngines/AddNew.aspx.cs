
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using System;

namespace SM.SmartInfo.UI.Configurations.RuleEngines
{
    public partial class AddNew : BasePage
    {
        #region Page Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ruleID = ucEditRule.SaveRule();

                //// yeu cau cap nhat blacklist
                //SharedComponent.Params.Common.CommonParam param =
                //    new SharedComponent.Params.Common.CommonParam(SharedComponent.Constants.FunctionType.Common.AddBlacklistPriceServiceCommand);
                //param.ServiceRankingCommand = new SharedComponent.Entities.ServiceRankingCommand();
                //param.ServiceRankingCommand.RefID = ruleID;
                //BIZ.MainController.Provider.Execute(param);

                Response.Redirect(string.Format(PageURL.Display, ruleID));
            }
            catch (Exception ex)
            {
                throw new SMXException(ex.Message);
            }
        }

        #endregion

        #region Common

        public void SetupForm()
        {
            ucEditRule.BindRule(null);
            ucEditRule.ConfigDisplay(true, true, true, true, true);
            hplExit.NavigateUrl = PageURL.Default;
        }

        #endregion
    }
}