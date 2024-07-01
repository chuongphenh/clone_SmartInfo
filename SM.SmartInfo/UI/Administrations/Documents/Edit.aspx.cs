//using SM.SmartInfo.BIZ;
//using SM.SmartInfo.PermissionManager.Shared;
//using SM.SmartInfo.SharedComponent.Constants;
//using SM.SmartInfo.SharedComponent.Entities;
//using SM.SmartInfo.SharedComponent.Params.Administration;
//using SM.SmartInfo.SharedComponent.Params.CommonList;
//using SM.SmartInfo.Utils;
//using SoftMart.Kernel.Exceptions;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace SM.SmartInfo.UI.Administrations.Plans
//{
//    public partial class Edit : PlansBase, ISMFormEdit<SharedComponent.Entities.Documents>
//    {
//        #region Event

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            try
//            {
//                if (!IsPostBack)
//                {
//                    SetupForm();
//                    LoadData();
//                }
//            }
//            catch (SMXException ex)
//            {
//                ucErr.ShowError(ex);
//            }
//        }

//        protected void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                UpdateItem();

//                Response.Redirect(string.Format(PageURL.Display, hidID.Value));
//            }
//            catch (SMXException ex)
//            {
//                ucErr.ShowError(ex);
//            }
//        }
//        #region Entry form

//        private void ucTarget_OnValidateAddEmployee(int employeeID)
//        {
//            int orgID = int.Parse(hidID.Value);
//            base.ValidateEmployeeIsInOtherOrganization(employeeID, orgID);
//        }

//        #endregion

//        #endregion

//        #region Common

//        public void SetupForm()
//        {
//            //1. Setup form
//            lnkExit.NavigateUrl = PageURL.DisplayNone;
//            UIUtility.BindListToDropDownList(rcbReportCycleType, SMX.PlanType.dctReportCycle.ToList(), false);
//        }

//        public void LoadData()
//        {
//            //2. Get data
//            DocumentParam param = new DocumentParam(FunctionType.Administration.Plan.LoadDataEdit);
//            param.PlanID = GetIntIdParam();
//            MainController.Provider.Execute(param);

//            //3. Bind data to form
//            var item = param.Plan;
//            hidVersion.Value = Utility.GetString(item.Version);

//            ucTarget.BindData(param.TargetInfos);

//            BindObjectToForm(item);
//        }

//        public void UpdateItem()
//        {
//            DocumentParam param = new DocumentParam(FunctionType.Administration.Plan.UpdateItem);
//            param.Plan = BindFormToObject();
//            // Get list target
//            param.TargetIDs = ucTarget.GetListTargetInGrid().Select(t => t.TargetID.Value).ToList();
//            MainController.Provider.Execute(param);
//        }

//        public void BindObjectToForm(SharedComponent.Entities.Documents item)
//        {
//            // Main information
//            txtPlanCode.Text = item.PlanCode;
//            txtPlanName.Text = item.Name;
//            rdpStartDate.SelectedDate = item.StartDate;
//            rdpEndDate.SelectedDate = item.EndDate;
//            txtReportCycle.Text = item.ReportCycle;
//            rcbReportCycleType.SelectedValue = Utils.Utility.GetDictionaryValue(SMX.PlanType.dctReportCycle, item.ReportCycleType);

//            hidID.Value = item.PlanID.ToString();
//            hidVersion.Value = item.Version.ToString();
//            lnkExit.NavigateUrl = string.Format(PageURL.Display, hidID.Value);
//        }

//        public SharedComponent.Entities.Documents BindFormToObject()
//        {
//            var item = new SharedComponent.Entities.Documents();
//            item.PlanID = GetIntIdParam();
//            item.PlanCode = txtPlanCode.Text;
//            item.Name = txtPlanName.Text;
//            item.StartDate = rdpStartDate.SelectedDate;
//            item.EndDate = rdpEndDate.SelectedDate;
//            item.ReportCycle = txtReportCycle.Text;
//            item.Version = Utility.GetNullableInt(hidVersion.Value);
//            item.ReportCycleType = Utility.GetNullableInt(rcbReportCycleType.SelectedValue);
//            return item;
//        }

//        #endregion

//        protected override Dictionary<object, string> FunctionCodeMapping
//        {
//            get
//            {
//                return new Dictionary<object, string>()
//                {
//                    { this      , FunctionCode.EDIT },
//                    { btnSave   , FunctionCode.EDIT },
//                };
//            }
//        }
//    }
//}
>>>>>>> 56a8783fbe5c61c74e49504bf1c233cad6d2f8e3
