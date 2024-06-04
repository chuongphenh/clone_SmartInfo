using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administrations.Segments
{
    public partial class AddNew : BasePage, ISMFormAddNew<SystemParameter>
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
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
                object itemID = AddNewItem();

                Response.Redirect(string.Format(PageURL.Display, itemID.ToString()));
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

        #region Biz
        public void SetupForm()
        {
            lnkExit.NavigateUrl = string.Format(PageURL.Default);

            var lstProvince = CacheManager.GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_Province, true);
            UIUtility.BindSPToDropDownList(ddlProvince, lstProvince.OrderBy(c => c.DisplayOrder).ToList());

            UIUtility.BindDicToDropDownList(ddStatus, SMX.Status.dctStatus, false);
        }

        public object AddNewItem()
        {
            SystemParameter item = BindFormToObject();

            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Segment.AddNewSegment);
            param.SystemParameter = item;
            MainController.Provider.Execute(param);

            return item.SystemParameterID;
        }

        public SystemParameter BindFormToObject()
        {
            SystemParameter item = new SystemParameter();

            item.FeatureID = SMX.Features.smx_Segment;
            item.Description = txtDescription.Text.Trim();
            item.Status = Utility.GetNullableInt(ddStatus.SelectedValue);
            item.Ext1i = Utility.GetNullableInt(ddlStreet.SelectedValue);
            item.Ext1 = txtSegmentFrom.Text.Trim();
            item.Ext2 = txtSegmentTo.Text.Trim();

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
                    { btnSave   , FunctionCode.ADD  },
                };
            }
        }
    }
}