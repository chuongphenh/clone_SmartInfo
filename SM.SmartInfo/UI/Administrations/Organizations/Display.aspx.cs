using System;
using SM.SmartInfo.BIZ;
using SoftMart.Core.BRE;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SoftMart.Core.BRE.SharedComponent;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.Administrations.Organizations
{
    public partial class Display : OrganizationBase, ISMFormDisplay<Organization>
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // Load tree view and setup form
                    SetupForm();
                    LoadData();
                }

                ucOrgTreeView.SelectedNodeChanged += tvOrg_SelectedNodeChanged;
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItems();

                Response.Redirect(PageURL.DisplayNone);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #region Tree

        protected void tvOrg_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                TreeNodeItem node = ucOrgTreeView.GetCurrentNode();
                int orgID = Utility.GetInt(node.ID);

                //set link edit when have node selected
                lnkEdit.NavigateUrl = string.Format(PageURL.Edit, orgID);

                //this.ClearCurrentInfo();
                this.DisplaySelectedItem(orgID);
            }
            catch (SMXException ex)
            {
                //this.ClearCurrentInfo();
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            lnkAddNew.NavigateUrl = PageURL.AddNew;
            ucOrgTreeView.BinData();
        }

        public void LoadData()
        {
            //Neu co ID thi hien thi Ogr dc chon, neu khong thi chon mac dinh Node goc
            int itemID;
            string strID = Request.Params[SMX.Parameter.ID];
            if (!string.IsNullOrWhiteSpace(strID))
            {
                itemID = base.GetIntIdParam();
            }
            else
            {
                TreeNodeItem node = ucOrgTreeView.GetFirstNode();

                if (node != null)
                {
                    itemID = int.Parse(node.ID);

                    ucOrgTreeView.SetSelectedNode(itemID);
                }
                else
                    itemID = 0;
            }

            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, itemID);
            //Su dung trang Display thay cho Default luon. Phan tree dc bind rieng.
            DisplaySelectedItem(itemID);
        }

        public void DeleteItems()
        {
            TreeNodeItem node = ucOrgTreeView.GetCurrentNode();
            var item = new Organization();
            item.OrganizationID = Utility.GetNullableInt(node.ID);
            item.Version = Utility.GetNullableInt(hidVersion.Value);

            OrganizationParam param = new OrganizationParam(FunctionType.Administration.Organization.DeleteOrganizations);
            param.Organization = item;
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specific

        public void BindObjectToForm(Organization item)
        {
            // Main information
            lblLevelInfo.Text = TruncateParentPath(item.BreadCrumb, item.Name);
            lblCode.Text = item.Code;
            lblName.Text = item.Name;

            if (item.Type != null)
                lblType.Text = Utility.GetDictionaryValue(SMX.OrganizationType.dctOrganizationType, item.Type);

            if (item.RuleID != null)
            {
                RuleCondition rule = RuleEngineService.GetRuleConditionByRuleID(item.RuleID.Value);
                lblRule.Text = rule == null ? string.Empty : rule.RuleName;
            }
            else
            {
                lblRule.Text = string.Empty;
            }

            if (item.DispatchEmployeeRuleID != null)
            {
                RuleCondition rule = RuleEngineService.GetRuleConditionByRuleID(item.DispatchEmployeeRuleID.Value);
                lblDispatchEmployeeRule.Text = rule == null ? string.Empty : rule.RuleName;
            }
            else
            {
                lblDispatchEmployeeRule.Text = string.Empty;
            }

            lblZoneName.Text = item.ZoneName;
            lblProvince.Text = GlobalCache.GetNameByID(item.Province);
            lblCommitteeName.Text = item.CommitteeName;
            lblAddress.Text = item.Address;
            lblOffice.Text = item.OfficeID;
            lblBranchEmail.Text = item.BranchEmail;
            lblDescription.Text = item.Description;

            hidVersion.Value = item.Version.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Display selected organization on tree
        /// </summary>
        /// <param name="orgID"></param>
        private void DisplaySelectedItem(int orgID)
        {
            //2. Get Data
            var param = new OrganizationParam(FunctionType.Administration.Organization.LoadDataDisplayOrganization);
            param.OrganizationID = orgID;
            MainController.Provider.Execute(param);

            //3. Bind data to form
            Organization item = param.Organization;
            BindObjectToForm(item);

            //Office office = param.Office;
            //lblOffice.Text = office == null ? string.Empty : office.Name;

            // Grid Employee
            ucOrganizationEmployee.BindData(param.EmployeeInfos);
            ucOrganizationManager.BindData(param.ManagerInfos);

            ucOrgTreeView.SetSelectedNode(orgID);
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkAddNew , FunctionCode.ADD      },
                    { lnkEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}