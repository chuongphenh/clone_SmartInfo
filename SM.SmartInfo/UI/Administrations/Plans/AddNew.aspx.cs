using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
namespace SM.SmartInfo.UI.Administrations.Plans
{
    public partial class AddNew : PlansBase, ISMFormAddNew<Plan>
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
                //ucTarget.OnValidateAddTarget += ucOrganizationEmployee_OnValidateAddEmployee;
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


        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            lnkExit.NavigateUrl = PageURL.DisplayNone;
            UIUtility.BindListToDropDownList(rcbReportCycleType, SMX.PlanType.dctReportCycle.ToList(), false);

            ucTarget.BindData(null);
        }

        public object AddNewItem()
        {
            PlanParam param = new PlanParam(FunctionType.Administration.Plan.AddNewItem);
            param.Plan = BindFormToObject();
            // Get list employees, Managers
            param.TargetIDs = ucTarget.GetListTargetInGrid().Select(t => t.TargetID.Value).ToList();
            // Save
            MainController.Provider.Execute(param);

            return param.PlanID;
        }

        #endregion

        #region Specific

        public Plan BindFormToObject()
        {
            var item = new Plan();
            item.PlanCode = txtPlanCode.Text;
            item.Name = txtPlanName.Text;
            item.StartDate = rdpStartDate.SelectedDate;
            item.EndDate = rdpEndDate.SelectedDate;
            item.ReportCycle = txtReportCycle.Text;
            item.ReportCycleType = Utility.GetNullableInt(rcbReportCycleType.SelectedValue);

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