using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.CatalogNews
{
    public partial class Edit : BasePage
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
            ucCatalogNewsTreeView.BinData();
        }

        public void LoadData()
        {
            //2. Get data
            NewsParam param = new NewsParam(FunctionType.CatalogNews.LoadDataEditCatalogNews);
            param.CatalogNewsID = GetIntIdParam();
            MainController.Provider.Execute(param);

            ucCatalogNewsTreeView.SetSelectedNode(param.CatalogNewsID.Value);

            BindObjectToForm(param.CatalogNews);
        }

        public void UpdateItem()
        {
            NewsParam param = new NewsParam(FunctionType.CatalogNews.UpdateCatalogNews);
            param.CatalogNews = BindFormToObject();
            // Get list employees
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specific

        public void BindObjectToForm(SharedComponent.Entities.CatalogNews item)
        {
            txtCode.Text = item.Code;
            txtName.Text = item.Name;
            hidID.Value = item.CatalogNewsID.ToString();
            hidVersion.Value = Utility.GetString(item.Version);
            lnkExit.NavigateUrl = string.Format(PageURL.Display, hidID.Value);
        }

        public SharedComponent.Entities.CatalogNews BindFormToObject()
        {
            // Get CatalogNews Information
            SharedComponent.Entities.CatalogNews item = new SharedComponent.Entities.CatalogNews();

            item.CatalogNewsID = Utility.GetInt(hidID.Value);
            item.Version = Utility.GetNullableInt(hidVersion.Value);

            item.Code = txtCode.Text;
            item.Name = txtName.Text;
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
                    { btnSave      , FunctionCode.EDIT },
                };
            }
        }
    }
}