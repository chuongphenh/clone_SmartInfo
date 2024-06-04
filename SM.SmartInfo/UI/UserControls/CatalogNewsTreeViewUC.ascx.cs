using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.BIZ;
using SoftMart.Core.UIControls;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class CatalogNewsTreeViewUC : BaseUserControl // System.Web.UI.UserControl
    {
        public event EventHandler SelectedNodeChanged;

        protected void tvCatalogNews_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (SelectedNodeChanged != null)
            {
                SelectedNodeChanged(sender, e);
            }
        }

        public void BinData()
        {
            SetupForm();
        }

        public CatalogNews GetCurrentOrg()
        {
            List<TreeNodeItem> lstNode = this.tvCatalogNews.Nodes.Cast<TreeNodeItem>().ToList();

            TreeNodeItem node = this.tvCatalogNews.SelectedNode;
            //string path = string.Empty;
            //do
            //{
            //    if (string.IsNullOrWhiteSpace(path))
            //    {
            //        path = node.Text;
            //    }
            //    else
            //    {
            //        path = node.Text + " > " + path;
            //    }

            //    node = lstNode.Find(en => en.ID == node.Parent);
            //} while (node != null);

            CatalogNews item = new CatalogNews();
            //item.Name = path;
            item.CatalogNewsID = int.Parse(tvCatalogNews.SelectedValue);

            return item;
        }

        public TreeNodeItem GetCurrentNode()
        {
            return this.tvCatalogNews.SelectedNode;
        }

        public TreeNodeItem GetFirstNode()
        {
            List<TreeNodeItem> lstNode = this.tvCatalogNews.Nodes.Cast<TreeNodeItem>().ToList();
            return lstNode.FirstOrDefault();
        }

        public void SetSelectedNode(int CatalogNewsID)
        {
            this.tvCatalogNews.SelectedValue = CatalogNewsID.ToString();
        }

        private void SetupForm()
        {
            NewsParam param = new NewsParam(FunctionType.CatalogNews.GetCatalogNewsTreeData);
            MainController.Provider.Execute(param);
            List<CatalogNews> lstAllCatalogNews = param.ListCatalogNews;

            List<TreeNodeItem> lstDataTree = new List<TreeNodeItem>();
            foreach (var item in lstAllCatalogNews)
            {
                TreeNodeItem node = new TreeNodeItem();
                node.ID = Utility.GetString(item.CatalogNewsID);
                node.Parent = Utility.GetString(item.ParentID);
                node.Text = item.Name;
                lstDataTree.Add(node);
            }
            tvCatalogNews.DataSource = lstDataTree;
            tvCatalogNews.DataBind();
        }
    }
}