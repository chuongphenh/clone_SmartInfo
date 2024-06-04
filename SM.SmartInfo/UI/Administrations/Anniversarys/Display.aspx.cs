using System;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Params.CommonList;

using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SoftMart.Core.Utilities;

namespace SM.SmartInfo.UI.Administration.Anniversarys
{
    public partial class Display : BasePage, ISMFormDisplay<SystemParameter>
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
        #endregion

        #region Common
        public void SetupForm()
        {
            lnkExit.NavigateUrl = string.Format(PageURL.Default);
            hidID.Value = GetParamId();
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, hidID.Value);
        }

        public void LoadData()
        {
            int? itemID = Utils.Utility.GetNullableInt(hidID.Value);

            var param = new SystemParameterParam(FunctionType.Administration.Anniversary.LoadDataEditAnniversary);
            param.SystemParameter = new SystemParameter { SystemParameterID = itemID };
            MainController.Provider.Execute(param);

            BindObjectToForm(param.SystemParameter);
        }

        public void DeleteItems()
        {
            var item = new SystemParameter();
            item.SystemParameterID = Utils.Utility.GetNullableInt(hidID.Value);
            item.Version = Utils.Utility.GetNullableInt(hidVersion.Value);
            var param = new SystemParameterParam(FunctionType.Administration.Anniversary.DeleteItemsAnniversary);
            param.SystemParameters = new List<SystemParameter>() { item };
            MainController.Provider.Execute(param);

        }
        #endregion

        #region Specific
        public void BindObjectToForm(SystemParameter item)
        {
            if (item == null)
                return;
            //bind System
            hidVersion.Value = Utils.Utility.GetString(item.Version);
            ltrCode.Text = item.Code;
            ltrName.Text = item.Name;
            ltrStatus.Text = Utils.Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            ltrExt1i.Text = Utils.Utility.GetDictionaryValue(SMX.Notification.CauHinhGuiThongBao.dicDesc, item.Ext1i);
            ltrExt4.Text = Utils.Utility.GetDateString(item.Ext4);
            ltrDescription.Text =UIUtility.ConvertBreakLine2Html(item.Description);
        }
        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}