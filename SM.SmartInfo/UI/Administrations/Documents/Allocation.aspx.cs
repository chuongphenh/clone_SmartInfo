using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administrations.Plans
{
    public partial class Allocation : DocumentsBase, ISMFormDisplay<Document>
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

                Response.Redirect(PageURL.Default);
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
            hidID.Value = Utility.GetString(base.GetIntIdParam());
            lnkExit.NavigateUrl = PageURL.Default;
        }

        public void LoadData()
        {
            int itemID = 0;
            string strID = Request.Params[SMX.Parameter.ID];
            if (!string.IsNullOrWhiteSpace(strID))
            {
                itemID = base.GetIntIdParam();
            }
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, itemID);
            DisplaySelectedItem(itemID);
        }

        public void DeleteItems()
        {
            PlanParam param = new PlanParam(FunctionType.Administration.Plan.DeleteItem);
            Document deletionPlan = new Document();
            deletionPlan.PlanID = Utility.GetNullableInt(hidID.Value);
            //target.Status = Utility.GetInt(hidStatus.Value);
            param.Plan = deletionPlan;
            MainController.Provider.Execute(param);
        }

        public void BindObjectToForm(Document item)
        {
            // Main information
            lblPlanCode.Text = item.PlanCode;
            lblPlanName.Text = item.Name;
            lblStartDate.Text = item.StartDate.HasValue ? item.StartDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblEndDate.Text = item.EndDate.HasValue ? item.EndDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            lblReportCycle.Text = item.ReportCycle;
            lblReportCycleType.Text = Utils.Utility.GetDictionaryValue(SMX.PlanType.dctReportCycle, item.ReportCycleType);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Display selected organization on tree
        /// </summary>
        /// <param name="planID"></param>
        private void DisplaySelectedItem(int planID)
        {
            //2. Get Data
            var param = new PlanParam(FunctionType.Administration.Plan.LoadDataDisplay);
            param.PlanID = planID;
            MainController.Provider.Execute(param);

            //3. Bind data to form
            var item = param.Plan;
            BindObjectToForm(item);
            // Grid Employee
            ucTarget.BindData(param.TargetInfos);
        }
        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}