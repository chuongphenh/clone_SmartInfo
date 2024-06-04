using System;
using System.Collections.Generic;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.Utils;
using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.PermissionManager.Shared;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using System.Security.Policy;

namespace SM.SmartInfo.UI.Administrations.Region
{
    public partial class Display : BasePage
    {
        #region event
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
                ShowError(ex);
            }
        }
        protected void rptRegionProvince_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObject2RptRegionProvince(e.Item);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        #endregion

        #region Biz

        public void SetupForm()
        {
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, GetParamId());
            hypExit.NavigateUrl = PageURL.Default;

            hidRegionID.Value = GetParamId();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            RegionParam param = new RegionParam(FunctionType.Administration.Region.DeleteRegion);
            param.RegionID = Utility.GetNullableInt(hidRegionID.Value);
            MainController.Provider.Execute(param);
            Response.Redirect(PageURL.Default);
        }

        public void LoadData()
        {
            int? itemID = Utility.GetNullableInt(hidRegionID.Value);
            RegionParam param = new RegionParam(FunctionType.Administration.Region.LoadDataEdit);
            param.RegionID = itemID;
            MainController.Provider.Execute(param);
            Bindata(param.adm_Region);
            rptRegionProvince.DataSource = param.Listadm_RegionProvince;
            rptRegionProvince.DataBind();
        }

        public void Bindata(adm_Region item)
        {
            ltrName.Text = item.Name;
            ltrEmployee.Text = GlobalCache.GetEmployeeNameByID(item.EmployeeID);
            ltrStatus.Text = Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
        }

        private void BindObject2RptRegionProvince(RepeaterItem rptItem)
        {
            adm_RegionProvince regionProvince = rptItem.DataItem as adm_RegionProvince;

            Literal ltrProvince = rptItem.FindControl("ltrProvince") as Literal;
            ltrProvince.Text = GlobalCache.GetNameByID(regionProvince.ProvinceID);
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   , FunctionCode.ADD  },
                    { btnDelete   , FunctionCode.ADD  },
                };
            }
        }

    }
}