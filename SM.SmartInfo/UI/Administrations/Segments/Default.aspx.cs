using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administrations.Segments
{
    public partial class Default : BasePage, ISMForm
    {
        private const string REPORT_TYPE = SMX.DynamicReport.AdministrationSegment;
        private const string PROVINCE_PARAM_NAME = "Province";
        private const string DISTRICT_PARAM_NAME = "District";
        private const string STREET_PARAM_NAME = "Street";
        private const string STATUS_PARAM_NAME = "Status";
        private const string SEGFROM_PARAM_NAME = "Ext1";
        private const string SEGTO_PARAM_NAME = "Ext2";

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
            string url = string.Format("Default.aspx?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}",
                PROVINCE_PARAM_NAME, ddlProvince.SelectedValue,
                DISTRICT_PARAM_NAME, ddlDistrict.SelectedValue,
                STREET_PARAM_NAME, ddlStreet.SelectedValue,
                STATUS_PARAM_NAME, ddlStatus.SelectedValue,
                SEGFROM_PARAM_NAME, txtExt1.Text.Trim(),
                SEGTO_PARAM_NAME, txtExt2.Text.Trim());
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

        protected void ddlStreet_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = string.Format("Default.aspx?{0}={1}&{2}={3}&{4}={5}",
                PROVINCE_PARAM_NAME, ddlProvince.SelectedValue,
                DISTRICT_PARAM_NAME, ddlDistrict.SelectedValue,
                STREET_PARAM_NAME, ddlStreet.SelectedValue);
            Response.Redirect(url);
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
            string streetTxt = UIUtility.GetNullParamId(STREET_PARAM_NAME);
            int? streetId = Utility.GetNullableInt(streetTxt);
            string statusTxt = UIUtility.GetNullParamId(STATUS_PARAM_NAME);
            int? statusId = Utility.GetNullableInt(statusTxt);

            string segmentFrom = UIUtility.GetNullParamId(SEGFROM_PARAM_NAME);
            string segmentTo = UIUtility.GetNullParamId(SEGTO_PARAM_NAME);

            // Set conditions
            List<KeyValuePair<string, object>> conditions = new List<KeyValuePair<string, object>>();
            conditions.Add(new KeyValuePair<string, object>(PROVINCE_PARAM_NAME, provinceId));
            conditions.Add(new KeyValuePair<string, object>(DISTRICT_PARAM_NAME, districtId));
            conditions.Add(new KeyValuePair<string, object>(STREET_PARAM_NAME, streetId));
            conditions.Add(new KeyValuePair<string, object>(STATUS_PARAM_NAME, statusId));
            conditions.Add(new KeyValuePair<string, object>(SEGFROM_PARAM_NAME, segmentFrom));
            conditions.Add(new KeyValuePair<string, object>(SEGTO_PARAM_NAME, segmentTo));
            ucDynamicReport.BuildReportForm(REPORT_TYPE, conditions);

            // Set selected value for control
            List<SystemParameter> lstProvince = GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_Province);
            UIUtility.BindSPToDropDownList(ddlProvince, lstProvince.OrderBy(c => c.DisplayOrder).ToList());
            UIUtility.SetDropDownListValue(ddlProvince, provinceTxt);

            List<SystemParameter> lstDistrict = new List<SystemParameter>();
            if (provinceId.HasValue)
                lstDistrict = GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_District, provinceId);
            UIUtility.BindSPToDropDownList(ddlDistrict, lstDistrict);
            UIUtility.SetDropDownListValue(ddlDistrict, districtTxt);

            List<SystemParameter> lstStreet = new List<SystemParameter>();
            if (districtId.HasValue)
                lstStreet = GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_Street, districtId);
            UIUtility.BindSPToDropDownList(ddlStreet, lstStreet);
            UIUtility.SetDropDownListValue(ddlStreet, streetTxt);

            UIUtility.BindDicToDropDownList(ddlStatus, SMX.Status.dctStatus);
            UIUtility.SetDropDownListValue(ddlStatus, statusTxt);

            txtExt1.Text = segmentFrom;
            txtExt2.Text = segmentTo;
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { hypAdd    ,       FunctionCode.ADD },
                };
            }
        }
    }
}