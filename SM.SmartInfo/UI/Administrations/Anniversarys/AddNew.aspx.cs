using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.Administration.Anniversarys
{
    public partial class AddNew : BasePage, ISMFormAddNew<SystemParameter>
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                }
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
                object itemID = AddNewItem();

                Response.Redirect(string.Format(PageURL.Display, itemID.ToString()));
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
            lnkExit.NavigateUrl = string.Format(PageURL.Default);

            UIUtility.BindDicToDropDownList(ddStatus, SMX.Status.dctStatus, false);
            UIUtility.BindDicToDropDownList(ddlExt1i, SMX.Notification.CauHinhGuiThongBao.dicDesc, true);
        }

        public object AddNewItem()
        {
            SystemParameter item = BindFormToObject();

            SystemParameterParam param = new SystemParameterParam(FunctionType.Administration.Anniversary.AddNewAnniversary);
            param.SystemParameter = item;
            MainController.Provider.Execute(param);

            return item.SystemParameterID;
        }
        #endregion

        #region Specific
        public SystemParameter BindFormToObject()
        {
            SystemParameter item = new SystemParameter();

            item.Code = txtCode.Text.Trim();
            item.Name = txtName.Text.Trim();
            item.FeatureID = SMX.Features.Anniversary;
            item.Ext4 = dtpExt4.SelectedDate;
            item.Status = Utility.GetNullableInt(ddStatus.SelectedValue);
            item.Ext1i = Utility.GetNullableInt(ddlExt1i.SelectedValue);
            item.Description = txtDescription.Text;

            return item;
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