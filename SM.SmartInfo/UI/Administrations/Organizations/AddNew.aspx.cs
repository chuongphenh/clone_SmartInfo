
using System;
using System.Linq;
using System.Web.UI.WebControls;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;

using SoftMart.Kernel.Exceptions;
using SoftMart.Core.BRE;
using System.Collections.Generic;
using SoftMart.Core.UIControls;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.Administrations.Organizations
{
    public partial class AddNew : OrganizationBase, ISMFormAddNew<Organization>
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
                ucOrgTreeView.SelectedNodeChanged += tvOrg_SelectedNodeChanged;
                ucOrganizationEmployee.OnValidateAddEmployee += ucOrganizationEmployee_OnValidateAddEmployee;
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void tvOrg_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                Organization item = ucOrgTreeView.GetCurrentOrg();
                lblParent.Text = item.BreadCrumb;

                int? zoneID = base.GetSelectedOrganizationZoneID(item.OrganizationID.Value);

                ddlZone.SelectedValue = Utility.GetString(zoneID);
                ddlZone.Enabled = zoneID == null;

                ddlZone_SelectedIndexChange(null, null);
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

        private void ucOrganizationEmployee_OnValidateAddEmployee(int employeeID)
        {
            base.ValidateEmployeeIsInOtherOrganization(employeeID, null);
        }

        protected void ddlZone_SelectedIndexChange(object sender, EventArgs e)
        {
            int? selectedValue = Utility.GetNullableInt(ddlZone.SelectedValue);
            ddlProvince.Items.Clear();
            List<SystemParameter> lstProvince = GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_Province, selectedValue);
            UIUtility.BindSPToDropDownList(ddlProvince, lstProvince.OrderBy(c => c.DisplayOrder).ToList());
        }

        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            lnkExit.NavigateUrl = PageURL.DisplayNone;

            //2. Load all data
            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.SetupAddNewForm);
            MainController.Provider.Execute(param);

            //3. Bind data to Form
            UIUtility.BindListToDropDownList(ddlZone, param.Zones);
            UIUtility.BindListToDropDownList(ddlCommittee, param.Committees);
            UIUtility.BindListToDropDownList(dropOrganizationType, SMX.OrganizationType.dctOrganizationType.ToList());

            var lstRuleToOrg = RuleEngineService.GetRuleInCategory(SMX.RuleCategory.DispatchOrganization);
            UIUtility.BindListToDropDownList(rcbRule, lstRuleToOrg);

            var lstRuleEmp = RuleEngineService.GetRuleInCategory(SMX.RuleCategory.DispatchEmployee);
            UIUtility.BindListToDropDownList(rcbDispatchEmployeeRule, lstRuleEmp);

            //tree view
            ucOrgTreeView.BinData();
            ucOrganizationEmployee.BindData(null);
            ucOrganizationManager.BindData(null);
        }

        public object AddNewItem()
        {
            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.AddNewOrganization);
            param.Organization = BindFormToObject();
            // Get list employees, Managers
            param.EmployeeIDs = ucOrganizationEmployee.GetListEmpInGrid().Select(t => t.EmployeeID.Value).ToList();
            param.ManagerIDs = ucOrganizationManager.GetListEmpInGrid().Select(t => t.EmployeeID.Value).ToList();
            // Save
            MainController.Provider.Execute(param);

            return param.Organization.OrganizationID;
        }

        #endregion

        #region Specific

        public Organization BindFormToObject()
        {
            // Get Organization Information
            TreeNodeItem node = ucOrgTreeView.GetCurrentNode();

            var item = new Organization();

            if (node != null)
                item.ParentID = Utility.GetNullableInt(node.ID);
            item.Code = txtCode.Text;
            item.Name = txtName.Text;
            item.Type = Utility.GetNullableInt(dropOrganizationType.SelectedValue);

            item.RuleID = Utility.GetNullableInt(rcbRule.SelectedValue);
            item.DispatchEmployeeRuleID = Utility.GetNullableInt(rcbDispatchEmployeeRule.SelectedValue);
            item.ZoneID = Utility.GetNullableInt(ddlZone.SelectedValue);
            item.Province = Utility.GetNullableInt(ddlProvince.SelectedValue);
            item.CommitteeID = Utility.GetNullableInt(ddlCommittee.SelectedValue);
            item.OfficeID = txtOffice.Text;
            item.Address = txtAddress.Text;
            item.BranchEmail = txtBranchEmail.Text;
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