using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administrations.Documents
{
    public partial class Default : BasePage, ISMForm
    {
        private const string REPORT_TYPE = SMX.DynamicReport.AdministrationDocuments;

        #region events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                SetupForm();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            popupExporting.ReportDisplayName = SMX.DynamicReport.dicReports[REPORT_TYPE].DisplayName;
            popupExporting.Show();
        }

        protected void popupExporting_StartExport(object sender, EventArgs e)
        {
            ucDynamicReport.ExportReport(popupExporting.ReportDisplayName);
        }

        #endregion

        public void SetupForm()
        {
            hypAdd.NavigateUrl = PageURL.AddNew;
            List<KeyValuePair<string, object>> conditions = new List<KeyValuePair<string, object>>();
            ucDynamicReport.BuildReportForm(REPORT_TYPE, conditions);
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { hypAdd    , FunctionCode.ADD      },
                    { btnExport , FunctionCode.VIEW     },
                };
            }
        }
    }
}