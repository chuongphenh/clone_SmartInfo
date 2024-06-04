﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.Administrations.Zone
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

            UIUtility.BindListToDropDownList(ddStatus, SMX.Status.dctStatus.ToList(), false);
        }

        public void LoadData()
        {
            //2. Get data
            var param = new SystemParameterParam(FunctionType.Administration.Zone.LoadDataEditZone);
            param.SystemParameter = new SystemParameter { SystemParameterID = GetIntIdParam() };
            MainController.Provider.Execute(param);
            BindObjectToForm(param.SystemParameter);

        }

        public void UpdateItem()
        {
            var param = new SystemParameterParam(FunctionType.Administration.Zone.UpdateZone);
            param.SystemParameter = BindFormToObject();
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specific

        public void BindObjectToForm(SystemParameter item)
        {
            if (item == null)
                return;
            txtCode.Enabled = false;
            HiddenVersion.Value = Utility.GetString(item.Version);
            hidID.Value = Utility.GetString(item.SystemParameterID);
            txtCode.Text = item.Code;
            txtName.Text = item.Name;
            ddStatus.SelectedValue = Utility.GetString(item.Status);
            txtDescription.Text = item.Description;
        }

        public SystemParameter BindFormToObject()
        {
            SystemParameter item = new SystemParameter();

            item.SystemParameterID = int.Parse(hidID.Value);
            item.Version = int.Parse(HiddenVersion.Value);
            item.Code = txtCode.Text.Trim();
            item.Name = txtName.Text.Trim();
            item.FeatureID = SMX.Features.smx_Zone;
            item.Description = txtDescription.Text.Trim();
            item.Status = Utility.GetNullableInt(ddStatus.SelectedValue);

            return item;
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