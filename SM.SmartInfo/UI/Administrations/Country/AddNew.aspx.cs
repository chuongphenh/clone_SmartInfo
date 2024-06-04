using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Country
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
                ShowError(ex);
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
                ShowError(ex);
            }
        }

        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            lnkExit.NavigateUrl = string.Format(PageURL.Default);

            //3. Bind data to Form
            UIUtility.BindListToDropDownList(ddStatus, SMX.Status.dctStatus.ToList(), false);
        }

        public object AddNewItem()
        {
            SystemParameter item = BindFormToObject();

            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Country.AddNewCountry);
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
            item.FeatureID = SMX.Features.CountryOfManufacturer;
            item.Description = txtDescription.Text.Trim();
            item.Status = Utility.GetNullableInt(ddStatus.SelectedValue);
            item.DisplayOrder = (int?)numDisplayOrder.Value;

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