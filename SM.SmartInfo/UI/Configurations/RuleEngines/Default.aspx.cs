using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.Utils;

using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.PermissionManager.Shared;
using SoftMart.Core.BRE.SharedComponent.Entities;

namespace SM.SmartInfo.UI.Configurations.RuleEngines
{
    public partial class Default : BasePage, ISMFormDefault<Flex_Rule>
    {
        #region Page Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucListRule.InitConfig("display.aspx?id={0}");
                SetupForm();

                SearchItemsForView();
            }
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { hplAddNew                 , FunctionCode.ADD      }
                };
            }
        }

        public Flex_Rule BindGridItemToObject(WebControl gridItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteItems()
        {
        }

        public void SetupForm()
        {
            hplAddNew.NavigateUrl = PageURL.AddNew;
        }

        public void BindObjectToGridItem(WebControl gridItem)
        {
        }

        public void SearchItemsForView()
        {
        }
    }
}