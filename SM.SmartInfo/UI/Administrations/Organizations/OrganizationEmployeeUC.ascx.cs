using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.SharedComponent.Entities;

using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.UI.UserControls;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SM.SmartInfo.SharedComponent.Params.Common;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SoftMart.Core.UIControls;

namespace SM.SmartInfo.UI.Administrations.Organizations
{
    public partial class OrganizationEmployeeUC : System.Web.UI.UserControl
    {
        public delegate void ValidateAddEmployeeEventHandler(int employeeID);
        public event ValidateAddEmployeeEventHandler OnValidateAddEmployee;

        public bool AllowEdit
        {
            get
            {
                string s = (string)ViewState["AllowEdit"];
                if (string.IsNullOrEmpty(s))
                    s = bool.FalseString;

                return bool.Parse(s);
            }
            set
            {
                ViewState["AllowEdit"] = value.ToString();
            }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdEmp.Columns[grdEmp.Columns.Count - 1].Visible = AllowEdit;
                grdEmp.ShowFooter = AllowEdit;
            }
        }

        protected void grdEmp_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                EmployeeInfo item = (EmployeeInfo)e.Item.DataItem;

                Label lblName = (Label)e.Item.FindControl("lblName");
                lblName.Text = item.Name;

                HiddenField hidEmployeeID = (HiddenField)e.Item.FindControl("hidEmployeeID");
                hidEmployeeID.Value = item.EmployeeID.ToString();

                Label lblUserName = (Label)e.Item.FindControl("lblUserName");
                lblUserName.Text = item.Username;

                Label lblGender = (Label)e.Item.FindControl("lblGender");
                lblGender.Text = Utility.GetDictionaryValue(SMX.dicGender, item.Gender);

                Label lblMobile = (Label)e.Item.FindControl("lblMobile");
                lblMobile.Text = item.Mobile;

                Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                lblEmail.Text = item.Email;

                LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
                btnDelete.OnClientClick = "return confirm('Bạn có thực sự muốn xóa?')";
                btnDelete.CommandName = SMX.ActionDelete;
            }
        }

        protected void grdEmp_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            try
            {
                List<EmployeeInfo> lstEmp = GetListEmpInGrid();
                switch (e.CommandName)
                {
                    // Delete employee from grid
                    case SMX.ActionDelete:
                        {
                            lstEmp.RemoveAt(e.Item.ItemIndex);
                            UIUtility.BindDataGrid(grdEmp, lstEmp);
                            break;
                        }

                    // Insert new employee to grid
                    case SMX.ActionNew:
                        {
                            EmployeeSearchBox ucEmployee = UIUtility.FindControlInDataGridFooter(grdEmp, "ucEmployee") as EmployeeSearchBox;
                            //EmployeeSelectorUC ucEmployee = UIUtil.FindControlInDataGridFoolter(grdEmp, "ucEmployee") as EmployeeSelectorUC;
                            int? empID = Utility.GetNullableInt(ucEmployee.SelectedValue);
                            if (empID != null)
                            {
                                ValidateEmployeeIsExistedInGrid(lstEmp, empID.Value);
                                if (OnValidateAddEmployee != null)
                                    OnValidateAddEmployee(empID.Value);

                                EmployeeInfo item = GetShortEmployeeInfoByID(empID.Value);
                                lstEmp.Add(item);

                                UIUtility.BindDataGrid(grdEmp, lstEmp);
                            }

                            break;
                        }
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        public void BindData(List<EmployeeInfo> lstEmp)
        {
            UIUtility.BindDataGrid(grdEmp, lstEmp);
        }

        public List<EmployeeInfo> GetListEmpInGrid()
        {
            List<EmployeeInfo> lstEmp = new List<EmployeeInfo>();

            foreach (DataGridItem item in grdEmp.Items)
            {
                Label lblName = (Label)item.FindControl("lblName");
                Label lblUserName = (Label)item.FindControl("lblUserName");
                Label lblGender = (Label)item.FindControl("lblGender");
                Label lblMobile = (Label)item.FindControl("lblMobile");
                Label lblEmail = (Label)item.FindControl("lblEmail");
                HiddenField hidEmployeeID = (HiddenField)item.FindControl("hidEmployeeID");

                EmployeeInfo employee = new EmployeeInfo();
                employee.Name = lblName.Text;
                employee.Username = lblUserName.Text;
                employee.Gender = Utility.GetDictionaryKey(SMX.dicGender, lblGender.Text);
                employee.Mobile = lblMobile.Text;
                employee.Email = lblEmail.Text;
                employee.EmployeeID = Utility.GetInt(hidEmployeeID.Value);

                lstEmp.Add(employee);
            }

            return lstEmp;
        }

        private EmployeeInfo GetShortEmployeeInfoByID(int id)
        {
            CommonParam param = new CommonParam(FunctionType.Common.GetShortUserByID);
            param.EmployeeId = id;

            MainController.Provider.Execute(param);

            return param.EmployeeInfo;
        }

        private void ValidateEmployeeIsExistedInGrid(List<EmployeeInfo> lstEmp, int employeeID)
        {
            int count = lstEmp.Where(t => t.EmployeeID == employeeID).Count();
            if (count > 0)
                throw new SMXException(Messages.Organization.DuplicateEmpInGrid);
        }
    }
}