using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.UI.UserControls;
using SM.SmartInfo.SharedComponent.Params;

using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.Utils;

using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administrations.Users
{
    public partial class AddNew : BasePage, ISMFormAddNew<Employee>
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupForm();
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
                ShowError(ex);
            }
        }

        protected void grdRole_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Role item = e.Item.DataItem as Role;
                HiddenField hiRoleID = e.Item.FindControl("hiRoleID") as HiddenField;
                hiRoleID.Value = item.RoleID.ToString();
            }
        }
        #endregion

        #region Common
        public void SetupForm()
        {
            lnkExit.NavigateUrl = string.Format(PageURL.Default);
            UIUtility.BindDicToDropDownList(ddlGender, SMX.dicGender, true);
            UIUtility.BindDicToDropDownList(ddlStatus, SMX.Status.dctStatus, false);

            var lstSector = CacheManager.GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.Sector, true);
            UIUtility.BindSPToDropDownList(ddlSector, lstSector);

            //var lstLevel = CacheManager.GlobalCache.GetListSystemParameterByFeatureID(SMX.Features.EmployeeLevel, true);
            //UIUtility.BindSPToDropDownList(ddlLevel, lstLevel);

            UIUtility.BindDicToDropDownList(ddlAuthorizationType, SMX.dicAuthenticationType, false);

            // lay danh sach AD
            List<string> lstLdapCnnName = UIUtility.GetListLdapCnnName();
            UIUtility.BindListToDropDownList(ddlLdapCnnName, lstLdapCnnName);

            UserParam param = new UserParam(FunctionType.Administration.User.SetupAddNewForm);
            MainController.Provider.Execute(param);
            UIUtility.BindDataGrid(grdRole, param.Roles);
        }

        public object AddNewItem()
        {
            //Binding object
            Employee employee = BindFormToObject();
            List<EmployeeRole> lstEmpRole = GetSelectedRoles();

            OrganizationEmployee orgEmp = null;
            EmployeeImage empImage = null;
            if (fileSignImage.HasFile)
            {
                empImage = new EmployeeImage();
                empImage.ContentType = fileSignImage.PostedFile.ContentType;
                empImage.SignImage = fileSignImage.FileBytes;
            }

            //Add
            UserParam param = new UserParam(FunctionType.Administration.User.AddNewItem);
            param.Employee = employee;
            param.OrganizationEmployee = orgEmp;
            param.EmployeeRoles = lstEmpRole;
            param.EmployeeImage = empImage;
            MainController.Provider.Execute(param);

            return param.Employee.EmployeeID;
        }
        #endregion

        #region Specific
        public Employee BindFormToObject()
        {
            Employee item = new Employee();

            item.Username = txtUserName.Text.Trim();
            item.Name = txtFullName.Text.Trim();
            item.IsLocked = true;
            item.EmployeeCode = txtEmployeeCode.Text.Trim();
            //item.CIFCode = txtCIFCode.Text.Trim();
            //item.DOB = rdpDOB.SelectedDate.Value;
            if (rdpDOB.SelectedDate.HasValue)
            {
                item.DOB = rdpDOB.SelectedDate.Value;
            }
            item.Gender = Utility.GetNullableInt(ddlGender.SelectedValue);
            item.Phone = txtHomePhone.Text;
            item.Mobile = txtMobilePhone.Text;
            item.Email = txtEmail.Text;
            item.Status = Utility.GetNullableInt(ddlStatus.SelectedValue);
            item.Description = txtNote.Text;
            item.Sector = Utility.GetNullableInt(ddlSector.SelectedValue);
            //item.Level = Utility.GetNullableInt(ddlLevel.SelectedValue);
            item.AuthorizationType = Utility.GetNullableInt(ddlAuthorizationType.SelectedValue);
            item.LdapCnnName = ddlLdapCnnName.SelectedValue;
            item.ListOrganizationEmployee = ucOrganizationEmployee.SelectedValue;
            item.ListOrganizationManager = ucOrganizationManager.SelectedValue;

            return item;
        }
        #endregion

        #region Private
        private List<EmployeeRole> GetSelectedRoles()
        {
            List<EmployeeRole> roles = new List<EmployeeRole>();

            foreach (DataGridItem item in grdRole.Items)
            {
                CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
                if (chkSelect != null)
                {
                    bool isSelect = chkSelect.Checked;
                    if (isSelect)
                    {
                        EmployeeRole role = new EmployeeRole();
                        HiddenField hiRoleID = item.FindControl("hiRoleID") as HiddenField;
                        role.RoleID = int.Parse(hiRoleID.Value);
                        roles.Add(role);
                    }
                }
            }
            return roles;
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