
using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using SoftMart.Core.BRE;
using System.Collections.Generic;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.Administrations.Organizations
{
    public partial class Edit : OrganizationBase, ISMFormEdit<Organization>
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
                }
                ucOrgTreeView.SelectedNodeChanged += tvOrg_SelectedNodeChanged;
                ucOrganizationEmployee.OnValidateAddEmployee += ucOrganizationEmployee_OnValidateAddEmployee;
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

        protected void tvOrg_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChangeParent.Checked)
                {
                    Organization item = ucOrgTreeView.GetCurrentOrg();
                    lblLevelInfo.Text = item.BreadCrumb;

                    hidNewParentID.Value = Utility.GetString(item.OrganizationID);

                    int? zoneID = base.GetSelectedOrganizationZoneID(item.OrganizationID.Value);

                    ddlZone.SelectedValue = zoneID != null ? zoneID.ToString() : string.Empty;
                    ddlZone.Enabled = zoneID == null;
                    ddlZone_SelectedIndexChange(null, null);
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddlZone_SelectedIndexChange(object sender, EventArgs e)
        {
            int? selectedValue = Utility.GetNullableInt(ddlZone.SelectedValue);
            ddlProvince.Items.Clear();
            List<SystemParameter> lstProvince = GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_Province, selectedValue);
            UIUtility.BindSPToDropDownList(ddlProvince, lstProvince.OrderBy(c => c.DisplayOrder).ToList());
        }

        #region Entry form

        protected void chkUsingPlan_CheckedChanged(object sender, EventArgs e)
        {
            //spanRequireRule.Visible = chkUsingPlan.Checked;
            //spanRequireManager.Visible = chkUsingPlan.Checked;
            //spanRequirePriority.Visible = chkUsingPlan.Checked;
        }

        private void ucOrganizationEmployee_OnValidateAddEmployee(int employeeID)
        {
            int orgID = int.Parse(hidID.Value);
            base.ValidateEmployeeIsInOtherOrganization(employeeID, orgID);
        }

        #endregion

        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            ucOrgTreeView.BinData();

            //2. Get data
            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.SetupEditForm);
            MainController.Provider.Execute(param);

            UIUtility.BindListToDropDownList(ddlZone, param.Zones);
            UIUtility.BindListToDropDownList(ddlCommittee, param.Committees);
            UIUtility.BindDicToDropDownList(dropOrganizationType, SMX.OrganizationType.dctOrganizationType, true);

            var lstRuleToOrg = RuleEngineService.GetRuleInCategory(SMX.RuleCategory.DispatchOrganization);
            UIUtility.BindListToDropDownList(rcbRule, lstRuleToOrg);

            var lstRuleEmp = RuleEngineService.GetRuleInCategory(SMX.RuleCategory.DispatchEmployee);
            UIUtility.BindListToDropDownList(rcbDispatchEmployeeRule, lstRuleEmp);
        }

        public void LoadData()
        {
            //2. Get data
            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.LoadDataEditOrganization);
            param.OrganizationID = GetIntIdParam();
            MainController.Provider.Execute(param);

            //3. Bind data to form
            Organization item = param.Organization;
            hidOldParentID.Value = Utility.GetString(item.ParentID);
            hidVersion.Value = Utility.GetString(item.Version);

            //Office office = param.Office;
            //if (office != null)
            //    txtOffice.Text = office.OfficeID;

            //Tab OrganizationEmployee
            ucOrganizationEmployee.BindData(param.EmployeeInfos);
            ucOrganizationManager.BindData(param.ManagerInfos);
            ucOrgTreeView.SetSelectedNode(item.OrganizationID.Value);

            BindObjectToForm(item);
        }

        public void UpdateItem()
        {
            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.UpdateOrganization);
            param.Organization = BindFormToObject();
            // Get list employees
            param.EmployeeIDs = ucOrganizationEmployee.GetListEmpInGrid().Select(t => t.EmployeeID.Value).ToList();
            param.ManagerIDs = ucOrganizationManager.GetListEmpInGrid().Select(t => t.EmployeeID.Value).ToList();
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specific

        public void BindObjectToForm(Organization item)
        {
            lblCurrentLevelInfo.Text = TruncateParentPath(item.BreadCrumb, item.Name);
            txtCode.Text = item.Code;
            txtName.Text = item.Name;
            dropOrganizationType.SelectedValue = Utility.GetString(item.Type);

            rcbRule.SelectedValue = Utility.GetString(item.RuleID);
            rcbDispatchEmployeeRule.SelectedValue = Utility.GetString(item.DispatchEmployeeRuleID);
            ddlZone.SelectedValue = item.ZoneID != null ? Utility.GetString(item.ZoneID) : Utility.GetString(item.ParentZoneID);
            ddlZone_SelectedIndexChange(null, null);
            ddlProvince.SelectedValue = Utility.GetString(item.Province);
            ddlCommittee.SelectedValue = Utility.GetString(item.CommitteeID);
            txtAddress.Text = item.Address;
            txtOffice.Text = item.OfficeID;
            txtBranchEmail.Text = item.BranchEmail;
            txtDescription.Text = item.Description;

            hidID.Value = item.OrganizationID.ToString();
            lnkExit.NavigateUrl = string.Format(PageURL.Display, hidID.Value);
        }

        public Organization BindFormToObject()
        {
            // Get Organization Information
            Organization item = new Organization();

            item.OrganizationID = Utility.GetInt(hidID.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            if (chkChangeParent.Checked)
            {
                item.ParentID = string.IsNullOrEmpty(hidNewParentID.Value) ? Utility.GetNullableInt(hidOldParentID.Value) :
                                                                             Utility.GetNullableInt(hidNewParentID.Value);
            }
            else
            {
                item.ParentID = Utility.GetNullableInt(hidOldParentID.Value);
            }

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
                    { this      , FunctionCode.EDIT },
                    { btnSave   , FunctionCode.EDIT },
                };
            }
        }
    }
}