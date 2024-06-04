
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

namespace SM.SmartInfo.UI.Administrations.Users
{
    public partial class Display : BasePage
    {
        #region Page Event

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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItems();
                Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnOpenPassword_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new Employee();
                item.EmployeeID = Utility.GetInt(hdId.Value);
                item.Version = Utility.GetNullableInt(hdVersion.Value);
                item.IsLocked = true;

                var param = new UserParam(FunctionType.Administration.User.UpdateIsLocked);
                param.Employee = item;
                MainController.Provider.Execute(param);
                LoadData();
                ShowMessage("Mở khóa thành công");
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnSetPassword_Click(object sender, EventArgs e)
        {
            txtNewPassword.Text = string.Empty;
            popSetPassword.Show();
        }

        protected void btnSetPasswordOK_Click(object sender, EventArgs e)
        {
            try
            {
                var param = new SharedComponent.Params.Permission.AuthenticationParam(FunctionType.Authentication.ResetPassword);
                param.UserName = ucUserDetail.Username;
                param.NewPassword = txtNewPassword.Text;
                PermissionController.Provider.Execute(param);

                popSetPassword.Hide();
                ShowMessage("Thành công");
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
        #endregion

        #region Common
        public void SetupForm()
        {
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, GetParamId());
            hypExit.NavigateUrl = PageURL.Default;

            hdId.Value = GetParamId();
        }

        public void LoadData()
        {
            //1. Setup form
            int? itemID = Utility.GetNullableInt(hdId.Value);
            UserParam param = new UserParam(FunctionType.Administration.User.LoadDataDisplayUser);
            param.EmployeeId = itemID;
            MainController.Provider.Execute(param);
            btnOpenPassword.Visible = param.Employee.IsLocked == false;
            ucUserDetail.BinData(itemID, param.Employee, param.Roles, param.UserRoles, false, param.EmployeeImage);
            hdVersion.Value = Utility.GetString(param.Employee.Version);
        }

        public void DeleteItems()
        {
            var item = new Employee();
            item.EmployeeID = Utility.GetInt(hdId.Value);
            item.Version = Utility.GetNullableInt(hdVersion.Value);

            var param = new UserParam(FunctionType.Administration.User.DeleteItems);
            param.Employees = new List<Employee>() { item };
            MainController.Provider.Execute(param);
        }
        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   ,       FunctionCode.EDIT     },
                    { btnDelete ,       FunctionCode.DELETE   },
                    { btnSetPassword ,  FunctionCode.DELETE   },
                };
            }
        }


    }
}