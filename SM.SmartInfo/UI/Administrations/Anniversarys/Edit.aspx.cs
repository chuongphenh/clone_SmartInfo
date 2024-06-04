using System;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;

namespace SM.SmartInfo.UI.Administration.Anniversarys
{
    public partial class Edit : BasePage, ISMFormEdit<SystemParameter>
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
                ucErr.ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateItem();

                Response.Redirect(string.Format(PageURL.Display, hidID.Value));
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
            //1. Setup form
            lnkExit.NavigateUrl = PageURL.Default;
            hidID.Value = GetParamId();
            UIUtility.BindDicToDropDownList(ddStatus, SMX.Status.dctStatus, false);
            UIUtility.BindDicToDropDownList(ddlExt1i, SMX.Notification.CauHinhGuiThongBao.dicDesc, true);
        }

        public void LoadData()
        {
            int? id = Utility.GetNullableInt(hidID.Value);

            var param = new SystemParameterParam(FunctionType.Administration.Anniversary.LoadDataEditAnniversary);
            param.SystemParameter = new SystemParameter { SystemParameterID = id };
            MainController.Provider.Execute(param);

            BindObjectToForm(param.SystemParameter);
        }

        public void UpdateItem()
        {
            var param = new SystemParameterParam(FunctionType.Administration.Anniversary.UpdateAnniversary);
            param.SystemParameter = BindFormToObject();
            MainController.Provider.Execute(param);
        }
        #endregion

        #region Specific
        public void BindObjectToForm(SystemParameter item)
        {
            if (item == null)
                return;
            hidVersion.Value = Utility.GetString(item.Version);
            ltrCode.Text = item.Code;
            txtName.Text = item.Name;
            UIUtility.SetDropDownListValue(ddStatus, item.Status);
            UIUtility.SetDropDownListValue(ddlExt1i, item.Ext1i);
            txtDescription.Text = item.Description;
            dtpExt4.SelectedDate = item.Ext4;
        }

        public SystemParameter BindFormToObject()
        {
            SystemParameter item = new SystemParameter();

            item.SystemParameterID = int.Parse(hidID.Value);
            item.Version = int.Parse(hidVersion.Value);
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
                    { btnSave   , FunctionCode.EDIT },
                };
            }
        }
    }
}