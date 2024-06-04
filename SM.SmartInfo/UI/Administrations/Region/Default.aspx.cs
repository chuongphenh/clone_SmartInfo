using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;

using SoftMart.Kernel.Exceptions;
using SoftMart.Kernel.Entity;
using System.IO;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.Administrations.Region
{
    public partial class Default : BasePage
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    SearchItemsForView();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdMain_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BindObjectToGridItem(e.Item);
            }
        }

        protected void grdMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                grdMain.CurrentPageIndex = e.NewPageIndex;
                SearchItemsForView();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdMain_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            try
            {
                //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                //{
                //    Employee item = BindGridItemToObject(e.Item);
                //    switch (e.CommandName)
                //    {
                //        case SMX.ActionEdit:
                //        default:
                //            break;
                //    }
                //}
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        grdMain.CurrentPageIndex = 0;
        //        SearchItemsForView();
        //    }
        //    catch (SMXException ex)
        //    {
        //        ucErr.ShowError(ex);
        //    }
        //}

        #endregion

        #region Common
        public void SetupForm()
        {
            //1. Setup form
            hypAddNew.HRef = PageURL.AddNew;
            grdMain.PageSize = SMX.smx_PageSize;

        }

        public void SearchItemsForView()
        {
            RegionParam param = new RegionParam(SharedComponent.Constants.FunctionType.Administration.Region.GetItemsForView);
            param.PagingInfo = new PagingInfo(grdMain.CurrentPageIndex, grdMain.PageSize);
            MainController.Provider.Execute(param);

            UIUtility.BindDataGrid(grdMain, param.Listadm_Region, param.PagingInfo.RecordCount);
        }

        #endregion

        #region Specific

        public void BindObjectToGridItem(WebControl gridItem)
        {
            adm_Region item = ((DataGridItem)gridItem).DataItem as adm_Region;

            HyperLink hplName = (HyperLink)gridItem.FindControl("hplName");
            HyperLink hypEdit = (HyperLink)gridItem.FindControl("hypEdit");

            hplName.Text = item.Name;
            hplName.NavigateUrl = string.Format(PageURL.Display, item.RegionID);
            hypEdit.NavigateUrl = string.Format(PageURL.Edit, item.RegionID);

            UIUtility.SetGridItemHidden(gridItem, "hiID", item.RegionID);
            UIUtility.SetGridItemHidden(gridItem, "hiVersion", item.Version);
            UIUtility.SetGridItemIText(gridItem, "ltrEmployeeName", GlobalCache.GetEmployeeNameByID(item.EmployeeID));
            UIUtility.SetGridItemIText(gridItem, "ltrStatus", Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status));
        }

        #endregion

        #region Base
        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                int editCol = grdMain.Columns.Count - 1;
                return new Dictionary<object, string>()
                {
                    { this                 , FunctionCode.VIEW      },
                };
            }
        }

        #endregion

    }
}