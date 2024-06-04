using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Country
{
    public partial class Default : BasePage, ISMForm
    {
        private const string REPORT_TYPE = SMX.DynamicReport.AdministrationCountry;

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
                ShowError(ex);
            }
        }

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    popupExporting.ReportDisplayName = SMX.DynamicReport.dicReports[REPORT_TYPE].DisplayName;
        //    popupExporting.Show();
        //}

        //protected void popupExporting_StartExport(object sender, EventArgs e)
        //{
        //    ucDynamicReport.ExportReport(popupExporting.ReportDisplayName);
        //}
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
                    {this       , FunctionCode.VIEW     },
                    { hypAdd    , FunctionCode.ADD      },
                    //{ btnExport , FunctionCode.VIEW     },
                };
            }
        }
    }
}