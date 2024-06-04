using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Configurations.RuleEngines
{
    public partial class Display : BasePage
    {
        #region Page Event

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SetupForm();

                LoadData();
            }
        }

        #endregion

        #region Common

        private void SetupForm()
        {
            string id = GetParamId(SMX.Parameter.ID);
            hplEdit.NavigateUrl = string.Format(PageURL.Edit, id);
            hplExit.NavigateUrl = PageURL.Default;
        }

        private void LoadData()
        {
            try
            {
                int ruleID = int.Parse(GetParamId(SMX.Parameter.ID));
                ucDisplay.LoadData(ruleID);

                ucDisplay.ConfigDisplay(true, true, true, true, true);
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
                    { this      , FunctionCode.DISPLAY  },
                    { hplEdit   , FunctionCode.EDIT     },
                };
            }
        }
    }
}