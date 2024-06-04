
using System;
using System.Collections.Generic;

using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.UI.UserControls;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Constants;

using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.Utils;
using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administrations.Roles
{
    public partial class Display : BasePage, ISMFormDisplay<Role>
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
                ucErr.ShowError(ex);
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
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Common

        public void SetupForm()
        {
            lnkExit.NavigateUrl = PageURL.Default;
        }

        public void LoadData()
        {
            int id = GetIntIdParam();

            RoleParam param = new RoleParam(FunctionType.Administration.Role.LoadDataDisplayRole);
            param.RoleId = id;
            MainController.Provider.Execute(param);

            BindObjectToForm(param.Role);

            hypEdit.NavigateUrl = string.Format(PageURL.Edit, id);
        }

        #endregion

        #region Specific

        public void BindObjectToForm(Role item)
        {
            hidVersion.Value = Utility.GetString(item.Version);
            hidID.Value = item.RoleID.ToString();
            hidStatus.Value = Utility.GetString(item.Status);
            ltrRoleName.Text = item.Name;
            ltrStatus.Text = Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            ltrDescription.Text = item.Description;
        }

        #endregion

        public void DeleteItems()
        {
            var item = new Role();
            item.RoleID = GetIntIdParam();
            item.Version = Utility.GetNullableInt(hidVersion.Value);
            item.Status = Utility.GetNullableInt(hidStatus.Value);
            var param = new RoleParam(FunctionType.Administration.Role.DeleteItems);
            param.Roles = new List<Role>() { item };
            MainController.Provider.Execute(param);
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { hypEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}