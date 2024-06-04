using System;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Service.Reporting.Engine;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.BIZ;

namespace SM.SmartInfo.UI.SmartInfos.EmulationAndRewards
{
    public partial class Default : BasePage
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    SearchItemForView();
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(PageURL.AddNew);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ucSideBarTreeView.SetSelectedNode(null);

                er_EmulationAndReward filter = new er_EmulationAndReward();
                filter.Year = (int?)numYear.Value;
                filter.Event = ddlAwardingPeriod.SelectedItem == null ? "" : ddlAwardingPeriod.SelectedItem.Text;
                filter.EmulationAndRewardUnit = txtUnit.Text;
                filter.TextSearch = txtTextSearchSubject.Text;
                filter.SubjectType = ucDisplay.Filter.SubjectType;

                ucDisplay.LoadData(filter);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void ucSideBarTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                var curNode = ucSideBarTreeView.GetCurrentNode();

                var selectedYear = Utils.Utility.GetNullableInt(string.IsNullOrWhiteSpace(curNode.Parent) ? curNode.ID : curNode.Parent);
                var unit = ucSideBarTreeView.GetCurrentCategory()?.EmulationAndRewardUnit;

                er_EmulationAndReward filter = new er_EmulationAndReward();
                filter.Year = selectedYear;
                filter.EmulationAndRewardUnit = unit;
                filter.SubjectType = SMX.EmulationAndRewardSubjectType.CaNhan;

                numYear.Value = selectedYear;
                txtUnit.Text = unit;

                ucDisplay.ActiveTab(SMX.EmulationAndRewardSubjectType.CaNhan);
                ucDisplay.LoadData(filter);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SetupForm()
        {
            EmulationAndRewardParam param2 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingPeriodNoPaging);
            MainController.Provider.Execute(param2);

            ddlAwardingPeriod.DataSource = param2.ListAwardingPeriod;
            ddlAwardingPeriod.DataBind();

            ucSideBarTreeView.SetupForm();
            ucDisplay.SetupForm();
        }

        private void SearchItemForView()
        {
            ucDisplay.LoadData(new er_EmulationAndReward()
            {
                SubjectType = SMX.EmulationAndRewardSubjectType.CaNhan
            });
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this                  , FunctionCode.VIEW },
                    { btnAddNew             , FunctionCode.ADD  },
                };
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListEmulationAndReward, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private void Export(string xmlFile, Dictionary<string, object> param, string ouputFileName = "")
        {
            ReportingEngine engine = new ReportingEngine(xmlFile);
            engine.SetParameters(param);
            var fileContent = engine.Render();

            if (string.IsNullOrWhiteSpace(ouputFileName))
                ouputFileName = engine.SaveAsFileName + ".xlsx";

            UIUtilities.ExportHelper.PushToDownload(fileContent, SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_XLSX, ouputFileName);
        }
    }
}