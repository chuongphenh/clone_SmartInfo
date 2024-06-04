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

namespace SM.SmartInfo.UI.Administrations.Province
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
            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Province.SetupAddFormProvince);
            MainController.Provider.Execute(param);

            //3. Bind data to Form
            UIUtility.BindListToDropDownList(ddStatus, SMX.Status.dctStatus.ToList(), false);
            UIUtility.BindListToDropDownList(ddlZone, GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_Zone), false);
        }

        public object AddNewItem()
        {
            SystemParameter item = BindFormToObject();
            if (!item.Ext1i.HasValue)
                throw new SMXException("Miền chưa được chọn");

            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Province.AddNewProvince);
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
            item.FeatureID = SMX.Features.smx_Province;
            item.Description = txtDescription.Text.Trim();
            item.Status = Utility.GetNullableInt(ddStatus.SelectedValue);
            item.Ext1i = Utility.GetNullableInt(ddlZone.SelectedValue);
            item.DisplayOrder = Utility.GetNullableInt(txtDisplayOrder.Text);

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