using System;
using SM.SmartInfo.BIZ;
using SoftMart.Core.BRE;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SoftMart.Core.BRE.SharedComponent;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.CatalogNews
{
    public partial class Display : BasePage
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // Load tree view and setup form
                    SetupForm();
                    LoadData();
                }

                ucCatalogNewsTreeView.SelectedNodeChanged += tvCatalogNews_SelectedNodeChanged;
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

                Response.Redirect(PageURL.DisplayNone);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #region Tree

        protected void tvCatalogNews_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                TreeNodeItem node = ucCatalogNewsTreeView.GetCurrentNode();
                int catalogNewsID = Utility.GetInt(node.ID);

                //set link edit when have node selected
                lnkEdit.NavigateUrl = string.Format(PageURL.Edit, catalogNewsID);

                //this.ClearCurrentInfo();
                this.DisplaySelectedItem(catalogNewsID);
            }
            catch (SMXException ex)
            {
                //this.ClearCurrentInfo();
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #endregion

        #region Common

        public void SetupForm()
        {
            //1. Setup form
            lnkAddNew.NavigateUrl = PageURL.AddNew;
            ucCatalogNewsTreeView.BinData();
        }

        public void LoadData()
        {
            //Neu co ID thi hien thi Ogr dc chon, neu khong thi chon mac dinh Node goc
            int itemID;
            string strID = Request.Params[SMX.Parameter.ID];
            if (!string.IsNullOrWhiteSpace(strID))
            {
                itemID = base.GetIntIdParam();
            }
            else
            {
                TreeNodeItem node = ucCatalogNewsTreeView.GetFirstNode();

                if (node != null)
                {
                    itemID = int.Parse(node.ID);

                    ucCatalogNewsTreeView.SetSelectedNode(itemID);
                }
                else
                    itemID = 0;
            }

            lnkEdit.NavigateUrl = string.Format(PageURL.Edit, itemID);
            //Su dung trang Display thay cho Default luon. Phan tree dc bind rieng.
            DisplaySelectedItem(itemID);
        }

        public void DeleteItems()
        {
            TreeNodeItem node = ucCatalogNewsTreeView.GetCurrentNode();
            var item = new SharedComponent.Entities.CatalogNews();
            item.CatalogNewsID = Utility.GetNullableInt(node.ID);
            item.Version = Utility.GetNullableInt(hidVersion.Value);

            NewsParam param = new NewsParam(FunctionType.CatalogNews.DeleteCatalogNews);
            param.CatalogNews = item;
            MainController.Provider.Execute(param);
        }

        #endregion

        #region Specific

        public void BindObjectToForm(SharedComponent.Entities.CatalogNews item)
        {
            // Main information
            lblCode.Text = item.Code;
            lblName.Text = item.Name;
            hidVersion.Value = item.Version.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Display selected organization on tree
        /// </summary>
        /// <param name="catalogNewsID"></param>
        private void DisplaySelectedItem(int catalogNewsID)
        {
            //2. Get Data
            var param = new NewsParam(FunctionType.CatalogNews.LoadDataDisplayCatalogNews);
            param.CatalogNewsID = catalogNewsID;
            MainController.Provider.Execute(param);

            //3. Bind data to form
            SharedComponent.Entities.CatalogNews item = param.CatalogNews;
            BindObjectToForm(item);

            // Grid Employee
            ucCatalogNewsTreeView.SetSelectedNode(catalogNewsID);
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { lnkAddNew , FunctionCode.ADD      },
                    { lnkEdit   , FunctionCode.EDIT     },
                    { btnDelete , FunctionCode.DELETE   },
                };
            }
        }
    }
}