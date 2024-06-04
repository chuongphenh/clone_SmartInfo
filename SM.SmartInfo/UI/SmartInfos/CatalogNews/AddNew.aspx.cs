using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SoftMart.Core.UIControls;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.CatalogNews
{
    public partial class AddNew : BasePage
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
                //ucCatalogNewsTreeView.SelectedNodeChanged += tvOrg_SelectedNodeChanged;
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
            //1. Setup form
            lnkExit.NavigateUrl = PageURL.DisplayNone;

            //tree view
            ucCatalogNewsTreeView.BinData();
        }

        public object AddNewItem()
        {
            NewsParam param = new NewsParam(FunctionType.CatalogNews.AddNewCatalogNews);
            param.CatalogNews = BindFormToObject();
            MainController.Provider.Execute(param);

            return param.CatalogNews.CatalogNewsID;
        }

        #endregion

        #region Specific

        public SharedComponent.Entities.CatalogNews BindFormToObject()
        {
            // Get Organization Information
            TreeNodeItem node = ucCatalogNewsTreeView.GetCurrentNode();

            var item = new SharedComponent.Entities.CatalogNews();

            if (node != null)
                item.ParentID = Utility.GetNullableInt(node.ID);
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
                    { this   , FunctionCode.ADD  },
                    { btnSave   , FunctionCode.ADD  },
                };
            }
        }
    }
}