using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Regions
{
    public partial class Edit : BasePage
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
                ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();

                ShowMessage("Lưu thành công");
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnAddRegionProvince_Click(object sender, EventArgs e)
        {
            try
            {
                var lstRegionProvince = BindRptRegionProvince2Object();

                lstRegionProvince.Add(new adm_RegionProvince()
                {
                });

                rptRegionProvince.DataSource = lstRegionProvince;
                rptRegionProvince.DataBind();
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

        protected void rptRegionProvince_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDelete:
                        {
                            // get data
                            int index = e.Item.ItemIndex;

                            // Delete item
                            DeleteItemAt(index);

                            break;
                        }
                    default:
                        break;

                }
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
            hidRegionID.Value = GetParamId();

            lnkExit.NavigateUrl = string.Format(PageURL.Default);
            UIUtility.BindDicToDropDownList(ddlStatus, SMX.Status.dctStatus, false);
            int? selectedEmp = Utility.GetNullableInt(Request.Params[SMX.Parameter.RefID]);
            if (selectedEmp.HasValue)
            {
                string empName = HtmlUtils.UrlDecode(Request.Params[SMX.Parameter.Code]);
                ucEmployee.SetSelectedItem(selectedEmp, empName);
            }
        }

        public void SaveData()
        {
            //Binding object
            adm_Region region = GetRegion();
            List<adm_RegionProvince> lstRegionProvince = BindRptRegionProvince2Object();

            //Add
            RegionParam param = new RegionParam(FunctionType.Administration.Region.SaveDataEdit);
            param.adm_Region = region;
            param.Listadm_RegionProvince = lstRegionProvince;
            MainController.Provider.Execute(param);
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
            txtName.Text = item.Name;
            //ucEmployee.DataValueField = Utility.GetString(item.EmployeeID);
            ddlStatus.SelectedValue = Utility.GetString(item.Status);

            if (item.EmployeeID.HasValue)
            {
                string empName = SM.SmartInfo.CacheManager.GlobalCache.GetEmployeeNameByID(item.EmployeeID);
                ucEmployee.SetSelectedItem(item.EmployeeID, empName);
            }
        }

        public adm_Region GetRegion()
        {
            adm_Region item = new adm_Region();

            item.RegionID = Utility.GetNullableInt(hidRegionID.Value);
            item.Name = txtName.Text;
            item.EmployeeID = Utility.GetNullableInt(ucEmployee.SelectedValue);
            item.Status = Utility.GetNullableInt(ddlStatus.SelectedValue);

            return item;
        }

        private void DeleteItemAt(int index)
        {
            // Add item
            List<adm_RegionProvince> lstItem = BindRptRegionProvince2Object();
            lstItem.RemoveAt(index);

            // Rebind data
            rptRegionProvince.DataSource = lstItem;
            rptRegionProvince.DataBind();
        }

        private void BindObject2RptRegionProvince(RepeaterItem rptItem)
        {
            adm_RegionProvince regionProvince = rptItem.DataItem as adm_RegionProvince;

            DropDownList ddlProvince = rptItem.FindControl("ddlProvince") as DropDownList;
            var lstProvince = CacheManager.GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.smx_Province, true);
            UIUtility.BindSPToDropDownList(ddlProvince, lstProvince);
            ddlProvince.SelectedValue = Utility.GetString(regionProvince.ProvinceID);
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
            btnDelete.OnClientClick = "return confirm('Bạn có chắc chắn muốn xóa không?')";
            btnDelete.CommandName = SMX.ActionDelete;
        }

        public List<adm_RegionProvince> BindRptRegionProvince2Object()
        {
            List<adm_RegionProvince> lstItem = new List<adm_RegionProvince>();

            foreach (RepeaterItem item in rptRegionProvince.Items)
            {
                DropDownList ddlProvince = item.FindControl("ddlProvince") as DropDownList;

                lstItem.Add(new adm_RegionProvince()
                {
                    ProvinceID = Utility.GetNullableInt(ddlProvince.SelectedValue),
                });
            }
            return lstItem;
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