using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administrations.Zone
{
    public partial class AddNew : BasePage, ISMFormAddNew<SystemParameter>
    {
        #region Event

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
                object itemID = AddNewItem();

                Response.Redirect(string.Format(PageURL.Display, itemID.ToString()));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            lnkExit.NavigateUrl = string.Format(PageURL.Default);

            ////2. Load all data
            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Zone.SetupAddFormZone);
            MainController.Provider.Execute(param);

            //3. Bind data to Form
            UIUtility.BindListToDropDownList(ddStatus, SMX.Status.dctStatus.ToList(), false);
        }

        public object AddNewItem()
        {
            SystemParameter item = BindFormToObject();

            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Zone.AddNewZone);
            param.SystemParameter = item;
            MainController.Provider.Execute(param);

            return item.SystemParameterID;
        }

        #endregion

        #region Specific

        public SystemParameter BindFormToObject()
        {
            SystemParameter item = new SystemParameter();

            item.Code = txtCode.Text.Trim();
            item.Name = txtName.Text.Trim();
            item.FeatureID = SMX.Features.smx_Zone;
            item.Description = txtDescription.Text.Trim();
            item.Status = Utility.GetNullableInt(ddStatus.SelectedValue);

            return item;
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { btnSave   , FunctionCode.ADD  },
                };
            }
        }
    }
}