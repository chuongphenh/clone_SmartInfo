using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administrations.Users
{
    public partial class Edit : BasePage, ISMFormEdit<Employee>
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
                UpdateItem();

                Response.Redirect(string.Format(PageURL.Display, hdId.Value));
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
                hiRoleID.Value = item.RoleID.ToString(); ;
            }
        }
        #endregion

        #region Common
        public void SetupForm()
        {
            hdId.Value = GetParamId();

            lnkExit.NavigateUrl = PageURL.Default;
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
        }

        public void LoadData()
        {
            int? itemID = Utility.GetNullableInt(hdId.Value);
            UserParam param = new UserParam(FunctionType.Administration.User.LoadDataEditUser);
            param.EmployeeId = itemID;
            MainController.Provider.Execute(param);

            Employee employee = param.Employee;
            BindObjectToForm(employee);
            BindRole(param.Roles, param.UserRoles);

            if (param.EmployeeImage != null && param.EmployeeImage.SignImage != null)
                imgSignImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(param.EmployeeImage.SignImage);
            else
                imgSignImage.Visible = false;
        }

        public void UpdateItem()
        {
            Employee employee = BindFormToObject();
            List<EmployeeRole> lstEmpRole = GetSelectedRoles(employee.EmployeeID.Value);

            EmployeeImage empImage = null;
            if (fileSignImage.HasFile)
            {
                empImage = new EmployeeImage();
                empImage.ContentType = fileSignImage.PostedFile.ContentType;
                empImage.SignImage = fileSignImage.FileBytes;
            }

            UserParam param = new UserParam(FunctionType.Administration.User.UpdateItem);
            param.Employee = employee;
            param.EmployeeRoles = lstEmpRole;
            param.EmployeeImage = empImage;
            MainController.Provider.Execute(param);
        }
        #endregion

        #region Specific
        public void BindObjectToForm(Employee item)
        {
            hdVersion.Value = Utility.GetString(item.Version);
            lblUserName.Text = item.Username;
            txtFullName.Text = item.Name;
            txtEmployeeCode.Text = item.EmployeeCode;
            rdpDOB.SelectedDate = item.DOB;
            UIUtility.SetDropDownListValue(ddlGender, item.Gender);
            txtHomePhone.Text = item.Phone;
            txtMobilePhone.Text = item.Mobile;
            txtEmail.Text = item.Email;
            UIUtility.SetDropDownListValue(ddlStatus, item.Status);
            txtNote.Text = item.Description;
            UIUtility.SetDropDownListValue(ddlSector, item.Sector);
            //UIUtility.SetDropDownListValue(ddlLevel, item.Level);
            UIUtility.SetDropDownListValue(ddlAuthorizationType, item.AuthorizationType);
            UIUtility.SetDropDownListValue(ddlLdapCnnName, item.LdapCnnName);
            if (item.ListOrganizationEmployee.Count > 0)
                BuildListOrganization(item.ListOrganizationEmployee, ltrOrganizationEmployee, hidDeletedOrgEmpID);
            if (item.ListOrganizationManager.Count > 0)
                BuildListOrganization(item.ListOrganizationManager, ltrOrganizationManager, hidDeletedOrgMgrID);
        }

        public Employee BindFormToObject()
        {
            Employee item = new Employee();

            item.EmployeeID = int.Parse(hdId.Value);
            item.Version = Utility.GetNullableInt(hdVersion.Value);
            item.Username = lblUserName.Text.Trim();
            item.Name = txtFullName.Text.Trim();
            item.EmployeeCode = txtEmployeeCode.Text.Trim();
            //item.CIFCode = txtCIFCode.Text.Trim();
            item.DOB = rdpDOB.SelectedDate;
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

            item.ListDeletedOrganizationEmployeeID = new List<int?>();
            string[] arrDeletedOrgID = hidDeletedOrgEmpID.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strOrgID in arrDeletedOrgID)
            {
                int? orgID = Utility.GetNullableInt(strOrgID);
                if (orgID != null)
                    item.ListDeletedOrganizationEmployeeID.Add(orgID);
            }

            item.ListDeletedOrganizationManagerID = new List<int?>();
            arrDeletedOrgID = hidDeletedOrgMgrID.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strOrgID in arrDeletedOrgID)
            {
                int? orgID = Utility.GetNullableInt(strOrgID);
                if (orgID != null)
                    item.ListDeletedOrganizationManagerID.Add(orgID);
            }

            return item;
        }
        #endregion

        #region Private
        private void BindRole(List<Role> datas, List<EmployeeRole> selectedRoles)
        {
            foreach (Role role in datas)
            {
                if (selectedRoles.Exists(r => r.RoleID == role.RoleID))
                    role.IsSelect = true;
            }
            grdRole.DataSource = datas;
            grdRole.DataBind();
        }

        private List<EmployeeRole> GetSelectedRoles(int emID)
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
                        role.EmployeeID = emID;
                        HiddenField hiRoleID = item.FindControl("hiRoleID") as HiddenField;
                        role.RoleID = int.Parse(hiRoleID.Value);
                        roles.Add(role);
                    }
                }
            }
            return roles;
        }

        private void BuildListOrganization(List<Organization> lstOrg, Literal ltrOrgDisplay, HiddenField hidLog)
        {
            List<string> lstItem = new List<string>();
            foreach(Organization org in lstOrg)
            {
                lstItem.Add(string.Format(
                    "<span id=\"s{0}\" class=\"flex_searchbox-multiselect-item\" onclick=\"removeItemInPanel(this, '{1}')\">{2}</span>",
                    org.OrganizationID ,hidLog.ClientID, org.Name));
            }

            ltrOrgDisplay.Text = string.Join("", lstItem);
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