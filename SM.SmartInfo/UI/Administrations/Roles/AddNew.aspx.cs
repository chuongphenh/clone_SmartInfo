
using System;
using System.Collections.Generic;

using SM.SmartInfo.BIZ;
using SM.SmartInfo.UI.UserControls;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;

using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administrations.Roles
{
    public partial class AddNew : BasePage, ISMFormAddNew<Role>
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
            lnkExit.NavigateUrl = string.Format(PageURL.Default);
            UIUtility.BindDicToDropDownList(ddlStatus, SMX.Status.dctStatus, false);
        }

        public object AddNewItem()
        {
            RoleParam param = new RoleParam(FunctionType.Administration.Role.AddNewItem);
            param.Role = BindFormToObject();

            MainController.Provider.Execute(param);

            return param.Role.RoleID;
        }

        #endregion

        #region Specific

        public Role BindFormToObject()
        {
            Role item = new Role();

            item.Name = txtName.Text;
            item.Status = int.Parse(ddlStatus.SelectedValue);
            item.Description = txtDescription.Text;

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