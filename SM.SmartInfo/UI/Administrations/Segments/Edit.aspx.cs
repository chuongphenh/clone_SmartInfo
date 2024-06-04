using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.Administrations.Segments
{
    public partial class Edit : BasePage, ISMFormEdit<SystemParameter>
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

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? province = Utility.GetNullableInt(ddlProvince.SelectedValue);
            List<SystemParameter> lstDistrict = new List<SystemParameter>();
            if (province.HasValue)
                lstDistrict = CacheManager.GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_District, province);

            UIUtility.BindSPToDropDownList(ddlDistrict, lstDistrict);
            ddlDistrict_SelectedIndexChanged(null, null);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? district = Utility.GetNullableInt(ddlDistrict.SelectedValue);
            List<SystemParameter> lstStreet = new List<SystemParameter>();
            if (district.HasValue)
                lstStreet = CacheManager.GlobalCache.GetListSystemParameterByFeatureIDAndExt1i(SMX.Features.smx_Street, district);

            UIUtility.BindSPToDropDownList(ddlStreet, lstStreet);
        }
        #endregion

        #region Common
        public void SetupForm()
        {
            lnkExit.NavigateUrl = PageURL.Default;
            hidID.Value = GetParamId();

            var lstProvince = GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_Province, true);
            UIUtility.BindSPToDropDownList(ddlProvince, lstProvince.OrderBy(c => c.DisplayOrder).ToList());

            UIUtility.BindDicToDropDownList(ddlStatus, SMX.Status.dctStatus);
        }

        public void LoadData()
        {
            int? itemID = Utility.GetNullableInt(hidID.Value);

            var param = new SystemParameterParam(FunctionType.Administration.Segment.LoadDataEditSegment);
            param.SystemParameter = new SystemParameter { SystemParameterID = itemID };
            MainController.Provider.Execute(param);

            BindObjectToForm(param.SystemParameter);
        }

        public void UpdateItem()
        {
            var param = new SystemParameterParam(FunctionType.Administration.Segment.UpdateSegment);
            param.SystemParameter = BindFormToObject();
            MainController.Provider.Execute(param);
        }
        #endregion

        #region Specific
        public void BindObjectToForm(SystemParameter item)
        {
            if (item == null)
                return;

            hidVersion.Value = Utility.GetString(item.Version);
            ltrCode.Text = item.Code;
            ltrName.Text = item.Name;
            txtSegmentFrom.Text = item.Ext1;
            txtSegmentTo.Text = item.Ext2;
            UIUtility.SetDropDownListValue(ddlStatus, item.Status);
            txtDescription.Text = item.Description;

            SystemParameter street = GlobalCache.GetSystemParameterByID(item.Ext1i);
            if(street != null)
            {
                SystemParameter district = GlobalCache.GetSystemParameterByID(street.Ext1i);

                if(district != null)
                {
                    UIUtility.SetDropDownListValue(ddlProvince, district.Ext1i);
                    ddlProvince_SelectedIndexChanged(null, null);
                }

                UIUtility.SetDropDownListValue(ddlDistrict, street.Ext1i);
                ddlDistrict_SelectedIndexChanged(null, null);
            }
            UIUtility.SetDropDownListValue(ddlStreet, item.Ext1i);
        }

        public SystemParameter BindFormToObject()
        {
            SystemParameter item = new SystemParameter();

            item.SystemParameterID = Utility.GetNullableInt(hidID.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            item.Ext1 = txtSegmentFrom.Text.Trim();
            item.Ext2 = txtSegmentTo.Text.Trim();
            item.FeatureID = SMX.Features.smx_Segment;
            item.Description = txtDescription.Text.Trim();
            item.Status = Utility.GetNullableInt(ddlStatus.SelectedValue);
            item.Ext1i = Utility.GetNullableInt(ddlStreet.SelectedValue);

            if (string.IsNullOrWhiteSpace(item.Ext2))
                item.Name = item.Ext1;
            else
                item.Name = string.Format("{0} - {1}", item.Ext1, item.Ext2);
            item.Code = string.Format("{0} - {1}", item.Ext1i, item.Name);

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