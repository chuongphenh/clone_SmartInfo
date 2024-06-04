
using System;
using System.Collections.Generic;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.UI.UserControls;

using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administrations.Roles
{
    public partial class Edit : BasePage, ISMFormEdit<Role>
    {

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
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

        public void SetupForm()
        {
            lnkExit.NavigateUrl = PageURL.Default;
            UIUtility.BindListToDropDownList(ddlStatus, SMX.Status.dctStatus.ToList(), false);
        }

        public void LoadData()
        {
            GetItemForView();
        }

        private void GetItemForView()
        {
            int id = GetIntIdParam();

            RoleParam param = new RoleParam(FunctionType.Administration.Role.SetupEditForm);
            param.RoleId = id;
            MainController.Provider.Execute(param);

            BindObjectToForm(param.Role);

        }

        public void UpdateItem()
        {

            RoleParam param = new RoleParam(FunctionType.Administration.Role.UpdateItem);
            param.Role = BindFormToObject();

            MainController.Provider.Execute(param);

        }

        #endregion

        #region Specific

        public void BindObjectToForm(Role item)
        {
            hidVersion.Value = Utils.Utility.GetString(item.Version);
            hidID.Value = item.RoleID.ToString();
            txtRoleName.Text = item.Name;
            ddlStatus.SelectedValue = Utility.GetString(item.Status);
            txtDescription.Text = item.Description;
        }

        public Role BindFormToObject()
        {

            Role item = new Role();
            item.RoleID = int.Parse(hidID.Value);
            item.Version = Utils.Utility.GetNullableInt(hidVersion.Value);
            item.Name = txtRoleName.Text;
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
                    { this      , FunctionCode.EDIT },
                    { btnSave   , FunctionCode.EDIT },
                };
            }
        }
    }
}