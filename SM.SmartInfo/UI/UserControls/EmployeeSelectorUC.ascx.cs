using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.UIControls;
using SM.SmartInfo.Utils;
using System.Linq;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class EmployeeSelectorUC : System.Web.UI.UserControl
    {
        public event EventHandler SelectedIndexChanged;

        #region Public Properties

        public EmployeeSelectorMode Mode
        {
            get { return string.IsNullOrEmpty(hidMode.Value) ? EmployeeSelectorMode.All : (EmployeeSelectorMode)Enum.Parse(typeof(EmployeeSelectorMode), hidMode.Value); }
            set { hidMode.Value = value.ToString(); }
        }

        public string ComboboxClientID
        {
            get { return rcbEmployee.ClientID; }
        }

        public string HidOrganizationID
        {
            get { return hidOrganizationID.Value; }
            set { hidOrganizationID.Value = value; }
        }

        public Unit Width
        {
            get { return rcbEmployee.Width; }
            set { rcbEmployee.Width = value; }
        }

        public string SelectedValue
        {
            get { return rcbEmployee.SelectedValue; }
            set { rcbEmployee.SelectedValue = value; }
        }

        public string Text
        {
            get { return rcbEmployee.Text; }
            set { rcbEmployee.Text = value; }
        }

        public string DataValueField
        {
            get { return rcbEmployee.DataValueField; }
            set { rcbEmployee.DataValueField = value; }
        }

        public string DataTextField
        {
            get { return rcbEmployee.DataTextField; }
            set { rcbEmployee.DataTextField = value; }
        }

        public bool AutoPostBack
        {
            get { return rcbEmployee.AutoPostBack; }
            set { rcbEmployee.AutoPostBack = value; }
        }

        public int? OrganizationType
        {
            get { return Utility.GetNullableInt(hidOrganizationType.Value); }
            set { hidOrganizationType.Value = value.ToString(); }
        }

        /// <summary>
        /// List of RoleID separated by ','
        /// </summary>
        public List<int> RoleIDs
        {
            get
            {
                return hidRoleIDs.Value.Split(',')
                    .Select(en => Utility.GetNullableInt(en))
                    .Where(en => en.HasValue)
                    .Select(en => en.Value).ToList();
            }
            set
            {
                hidRoleIDs.Value = string.Join(",", value);
            }
        }

        #endregion

        #region Event

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        protected void rcbEmployee_ItemDataBound(object sender, SoftMart.Core.UIControls.SearchBoxItemEventArgs e)
        {
            Employee enEmployee = (Employee)e.Item.DataItem;

            Literal ltrUsername = (Literal)e.Item.FindControl("ltrUsername");
            ltrUsername.Text = enEmployee.Username;

            Literal ltrCustomerName = (Literal)e.Item.FindControl("ltrName");
            ltrCustomerName.Text = enEmployee.Name;
        }

        protected void rcbEmployee_TextChanged(object sender, string newText)
        {
            CommonParam param = new CommonParam(FunctionType.Common.EmployeeSelectorSearch);
            param.EmployeeSelectorMode = Mode;
            param.EmployeeName = Utility.NullIfEmptyString(newText);
            param.OrganizationID = Utility.GetNullableInt(hidOrganizationID.Value);
            param.OrganizationType = OrganizationType;
            param.RoleIDs = RoleIDs;

            MainController.Provider.Execute(param);

            BindData(param.Employees.Take(30).ToList());
        }

        protected void rcbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                SelectedIndexChanged(sender, e);
            }
        }

        #endregion

        #region Public methods

        public void SetSelectedItem(int? selectedValue, string text)
        {
            //Luon phai set cung luc. Neu ko, khi select lan thu 2 se ko co value.
            this.SelectedValue = Utility.GetString(selectedValue);
            this.Text = text;
        }

        public void BindData(Employee employee)
        {
            List<Employee> lst = new List<Employee>() { employee };
            BindData(lst);
        }

        public void BindData(List<Employee> lstEmployee)
        {
            rcbEmployee.DataSource = lstEmployee;
            rcbEmployee.DataBind();
        }

        #endregion
    }
}