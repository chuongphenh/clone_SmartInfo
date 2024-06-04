using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;

using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.PermissionManager;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.Administrations.Province
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
            lnkExit.NavigateUrl = string.Format(PageURL.Default);
        }

        public void LoadData()
        {
            int itemID = base.GetIntIdParam();
            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, itemID);

            var param = new SystemParameterParam(FunctionType.Administration.Province.LoadDataEditProvince);
            param.SystemParameter = new SystemParameter { SystemParameterID = itemID };
            MainController.Provider.Execute(param);
            BindObjectToForm(param.SystemParameter);
        }

        public void DeleteItems()
        {
            var item = new SystemParameter();
            item.SystemParameterID = GetIntIdParam();
            item.Version = Utility.GetNullableInt(HiddenVersion.Value);
            var param = new SystemParameterParam(FunctionType.Administration.Province.DeleteItemsProvince);
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
            HiddenVersion.Value = Utility.GetString(item.Version);
            hdID.Value = Utility.GetString(item.SystemParameterID);
            lblCode.Text = item.Code;
            lblName.Text = item.Name;
            lblStatus.Text = Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            lblDescription.Text = item.Description;
            lblZone.Text = GlobalCache.GetNameByID(item.Ext1i);
            lblDisplayOrder.Text = Utility.GetStringVND(item.DisplayOrder);

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