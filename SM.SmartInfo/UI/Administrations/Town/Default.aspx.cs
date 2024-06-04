using System;
using System.Linq;
using System.Collections.Generic;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;

using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.Administrations.Town
{
    public partial class Default : BasePage, ISMForm
    {
        private const string REPORT_TYPE = SMX.DynamicReport.AdministrationTown;
        private const string PROVINCE_PARAM_NAME = "Province";
        private const string DISTRICT_PARAM_NAME = "District";
        private const string CODE_PARAM_NAME = "CodeORName";

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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string url = string.Format("Default.aspx?{0}={1}&{2}={3}&{4}={5}",
                PROVINCE_PARAM_NAME, ddlProvince.SelectedValue,
                DISTRICT_PARAM_NAME, ddlDistrict.SelectedValue,
                CODE_PARAM_NAME, txtCodeOrName.Text.Trim());
            Response.Redirect(url);
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = string.Format("Default.aspx?{0}={1}", PROVINCE_PARAM_NAME, ddlProvince.SelectedValue);
            Response.Redirect(url);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = string.Format("Default.aspx?{0}={1}&{2}={3}",
                PROVINCE_PARAM_NAME, ddlProvince.SelectedValue,
                DISTRICT_PARAM_NAME, ddlDistrict.SelectedValue);
            Response.Redirect(url);
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
            // Get initial conditions
            string provinceTxt = UIUtility.GetNullParamId(PROVINCE_PARAM_NAME);
            int? provinceId = Utility.GetNullableInt(provinceTxt);
            string districtTxt = UIUtility.GetNullParamId(DISTRICT_PARAM_NAME);
            int? districtId = Utility.GetNullableInt(districtTxt);
            string codeOrName = UIUtility.GetNullParamId(CODE_PARAM_NAME);

            // Set conditions
            List<KeyValuePair<string, object>> conditions = new List<KeyValuePair<string, object>>();
            conditions.Add(new KeyValuePair<string, object>(PROVINCE_PARAM_NAME, provinceId));
            conditions.Add(new KeyValuePair<string, object>(DISTRICT_PARAM_NAME, districtId));
            conditions.Add(new KeyValuePair<string, object>(CODE_PARAM_NAME, codeOrName));
            ucDynamicReport.BuildReportForm(REPORT_TYPE, conditions);

            // Set selected value for control
            List<SystemParameter> provinceSysParams = GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_Province);
            UIUtility.BindSPToDropDownList(ddlProvince, provinceSysParams.OrderBy(c => c.DisplayOrder).ToList());
            ddlProvince.SelectedValue = provinceTxt;
            List<SystemParameter> districtSysParams = GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_District, provinceId);
            UIUtility.BindSPToDropDownList(ddlDistrict, districtSysParams);
            ddlDistrict.SelectedValue = districtTxt;
            txtCodeOrName.Text = codeOrName;
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