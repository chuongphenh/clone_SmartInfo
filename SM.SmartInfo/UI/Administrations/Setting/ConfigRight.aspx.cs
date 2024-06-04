using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Setting
{
    public partial class ConfigRight : BasePage
    {
        #region Page Events

        public bool IsEmployee
        {
            get
            {
                string s = (string)ViewState["IsEmployee"];
                if (string.IsNullOrEmpty(s))
                    s = bool.FalseString;

                return bool.Parse(s);
            }
            set
            {
                ViewState["IsEmployee"] = value.ToString();
            }
        }

        public bool IsRole
        {
            get
            {
                string s = (string)ViewState["IsRole"];
                if (string.IsNullOrEmpty(s))
                    s = bool.FalseString;

                return bool.Parse(s);
            }
            set
            {
                ViewState["IsRole"] = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                }
                LoadData();
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

        protected void cbvFeature_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format(PageURL.ConfigRight, cbvFeature.SelectedValue, ddlType.SelectedValue,
                    ucEmployee.SelectedValue, HtmlUtils.UrlEncode(ucEmployee.Text)));
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
                SaveItem();

                ShowMessage("Lưu thành công");
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ucEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (int.Parse(ddlType.SelectedValue) == 1)
                {
                    thUser.Visible = true;
                    tdUser.Visible = true;
                    IsEmployee = true;
                    IsRole = false;
                }
                else
                {
                    IsEmployee = false;
                    IsRole = true;

                    thUser.Visible = false;
                    tdUser.Visible = false;
                    ucEmployee.Text = string.Empty;
                    ucEmployee.SelectedValue = null;
                }
                LoadData(false);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Method

        public void SetupForm()
        {
            IsRole = true;
            // lấy ra danh sách các menu 
            RightParam param = new RightParam(FunctionType.Administration.Right.SetupViewForm);
            MainController.Provider.Execute(param);
            BindDataToCbxTree(param.Features);

            UIUtility.BindDicToDropDownList(ddlType, SMX.dctRoleOrEmployee, false);

            int? selectedFeatureID = Utility.GetNullableInt(Request.Params[SMX.Parameter.ID]);
            if (selectedFeatureID.HasValue)
                cbvFeature.SelectedValue = selectedFeatureID.ToString();

            int? selectedType = Utility.GetNullableInt(Request.Params[SMX.Parameter.RefType]);
            if (selectedType.HasValue)
            {
                ddlType.SelectedValue = selectedType.ToString();
                if (int.Parse(ddlType.SelectedValue) == 1)
                {
                    thUser.Visible = true;
                    tdUser.Visible = true;
                    IsEmployee = true;
                    IsRole = false;
                }
                else
                {
                    IsEmployee = false;
                    IsRole = true;

                    thUser.Visible = false;
                    tdUser.Visible = false;
                    ucEmployee.Text = string.Empty;
                    ucEmployee.SelectedValue = null;
                }
            }

            int? selectedEmp = Utility.GetNullableInt(Request.Params[SMX.Parameter.RefID]);
            if (selectedEmp.HasValue)
            {
                string empName = HtmlUtils.UrlDecode(Request.Params[SMX.Parameter.Code]);
                ucEmployee.SetSelectedItem(selectedEmp, empName);
            }
        }

        public void LoadData(bool isChange = true)
        {
            Right filter = GetSearchFilter();
            if (filter == null)
                return;
            if (isChange)
            {
                RightParam param = new RightParam(FunctionType.Administration.Right.GetItemsForView);
                param.Right = filter;
                MainController.Provider.Execute(param);

                //Buid Grid
                ConfigRightUIHelper helper = new ConfigRightUIHelper(hidDynamicColumn);
                DataGrid newGrid = helper.DefineGridStructure(param);
                newGrid.ItemDataBound += new DataGridItemEventHandler(grdMain_ItemDataBound);
                if (IsEmployee)
                    newGrid.Columns[0].HeaderText = "Tên chuyên viên";

                grdMain = newGrid;

                UIUtility.BindDataGrid(grdMain, param.BuildRightConfigs);
            }
            else
            {
                grdMain = new DataGrid();
            }
        }

        public void BindObjectToGridItem(WebControl gridItem)
        {
            BuildRightConfig dataItem = ((DataGridItem)gridItem).DataItem as BuildRightConfig;

            UIUtility.SetGridItemIText(gridItem, "ltrName", dataItem.Name);
            UIUtility.SetGridItemHidden(gridItem, "hidItemID", dataItem.ItemID);

            List<string> cols = GetDynamicColumnID();
            foreach (string strUniqueName in cols)
            {
                CheckBox ckSelect = gridItem.FindControl("ck" + strUniqueName) as CheckBox;

                int functionID = int.Parse(strUniqueName);
                ckSelect.Checked = this.CheckHasValue(dataItem.FunctionIDs, functionID);
            }
        }

        public BuildRightConfig BindGridItemToObject(WebControl gridItem, bool isEmployee = false)
        {
            BuildRightConfig item = new BuildRightConfig();

            HiddenField hidItemID = gridItem.FindControl("hidItemID") as HiddenField;
            item.ItemID = Utility.GetNullableInt(hidItemID.Value);

            List<int?> lstFunctionID = new List<int?>();
            List<string> lstColumn = this.GetDynamicColumnID();
            foreach (string colID in lstColumn)
            {
                CheckBox ckDynamic = gridItem.FindControl("ck" + colID) as CheckBox;
                if (ckDynamic.Checked)
                {
                    lstFunctionID.Add(int.Parse(colID));
                }
            }

            item.FunctionIDs = lstFunctionID;
            return item;
        }

        private List<BuildRightConfig> GetAllItemOnRow(RightParam param)
        {
            List<BuildRightConfig> lstSaveItem = new List<BuildRightConfig>();

            foreach (DataGridItem gridItem in grdMain.Items)
            {
                BuildRightConfig item = BindGridItemToObject(gridItem);
                lstSaveItem.Add(item);
            }
            return lstSaveItem;
        }

        private void SaveItem()
        {
            if (string.IsNullOrWhiteSpace(cbvFeature.SelectedValue))
                return;
            RightParam param = new RightParam(FunctionType.Administration.Right.SaveItem);
            param.BuildRightConfigs = GetAllItemOnRow(param);
            param.FeatureId = int.Parse(cbvFeature.SelectedValue);
            param.IsEmployee = IsEmployee;

            MainController.Provider.Execute(param);
        }

        private Right GetSearchFilter()
        {
            int? featureID = Utility.GetNullableInt(cbvFeature.SelectedValue);
            if (featureID == null)
                return null;

            Right filter = new Right();
            filter.FeatureID = featureID;
            filter.EmployeeID = Utility.GetNullableInt(ucEmployee.SelectedValue);

            return filter;
        }

        #endregion

        #region Utility

        private void BindDataToCbxTree(List<Feature> lstOrg)
        {
            List<TreeNodeItem> lstNode = new List<TreeNodeItem>();
            foreach (var item in lstOrg)
            {
                TreeNodeItem node = new TreeNodeItem();
                node.ID = Utility.GetString(item.FeatureID);
                node.Parent = Utility.GetString(item.ParentID);

                if (!string.IsNullOrWhiteSpace(item.Description))
                    node.Text = item.Description;
                else
                    node.Text = item.Name;

                lstNode.Add(node);
            }
            cbvFeature.DataSource = lstNode;
            cbvFeature.DataBind();
        }

        private List<string> GetDynamicColumnID()
        {
            return hidDynamicColumn.Value.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private bool CheckHasValue(List<int?> lstFunctionID, int functionID)
        {
            bool isCheck = lstFunctionID.Exists(c => c.Value == functionID);
            return isCheck;
        }

        private DataGrid grdMain
        {
            get
            {
                DataGrid item = divGrid.Controls.OfType<DataGrid>().FirstOrDefault();
                return item;
            }

            set
            {
                DataGrid item = divGrid.Controls.OfType<DataGrid>().FirstOrDefault();
                if (item != null)
                    divGrid.Controls.Remove(item);

                divGrid.Controls.Add(value);
            }
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    //{ btnSave   , FunctionCode.ADD  },
                };
            }
        }
    }
}