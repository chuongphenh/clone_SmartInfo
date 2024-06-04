using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Entity;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Roles
{
    public partial class Default : BasePage
    {
        private const int REPORT_TYPE = SMX.ManuallyReport.AdministrationRoles;

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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // lay du lieu
                RoleParam param = new RoleParam(FunctionType.Administration.Role.ExportExcel);
                MainController.Provider.Execute(param);
                var dataTable = param.DataTable;

                // STT
                dataTable.Columns["OrderNo"].ReadOnly = false;
                int orderNo = 1;
                foreach (System.Data.DataRow row in dataTable.Rows)
                    row["OrderNo"] = orderNo++;

                // chuan bi template
                string templateName = "Excel_DanhSach_NhomNguoiDung.xlsx";
                string templatePath = Path.Combine(ConfigUtils.TemplateFolder, templateName);
                string resultName = string.Format("Excel_DanhSach_NhomNguoiDung_{0}.xlsx", Guid.NewGuid().ToString());
                string resultPath = Path.Combine(ConfigUtils.TemporaryFolder, resultName);
                File.Copy(templatePath, resultPath);

                // Xuat du lieu
                ExcelTransformHelper excel = new ExcelTransformHelper();
                excel.Transform(resultPath, "TableData", dataTable);

                // download
                SoftMart.Core.Utilities.DownloadHelper.PushExcelFileForDownload(resultPath, resultName, true);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void grdMain_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    var item = (Role)e.Item.DataItem;
                    BindObjectToRow(item, e.Item);
                    break;
            }
        }
        #endregion

        #region LoadData

        public void SetupForm()
        {
            hypAdd.NavigateUrl = PageURL.AddNew;
        }

        private void SearchItemForView()
        {
            RoleParam param = new RoleParam(FunctionType.Administration.Role.GetItemsForView);
            MainController.Provider.Execute(param);

            UIUtility.BindDataGrid(grdMain, param.Roles);
        }

        private void BindObjectToRow(Role item, DataGridItem rowItem)
        {
            HyperLink hypName = (HyperLink)rowItem.FindControl("hypName");
            hypName.Text = item.Name;
            hypName.NavigateUrl = string.Format(PageURL.Display, item.RoleID);

            UIUtility.SetGridItemIText(rowItem, "ltrDescription", UIUtility.ConvertBreakLine2Html(item.Description));
            UIUtility.SetGridItemIText(rowItem, "ltrStatus", Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status));

            HyperLink hypCode = (HyperLink)rowItem.FindControl("hypCode");
            hypCode.NavigateUrl = string.Format(PageURL.Edit, item.RoleID);
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                     { this,     PermissionManager.Shared.FunctionCode.VIEW }
                };
            }
        }
        #endregion
    }
}