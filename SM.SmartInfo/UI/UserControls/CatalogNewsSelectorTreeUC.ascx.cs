using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class CatalogNewsSelectorTreeUC : BaseUserControl
    {
        #region Properties

        public string TreeClientID
        {
            get { return treeOrg.ClientID; }
        }

        public bool AutoPostBack
        {
            get
            {
                return treeOrg.AutoPostBack;
            }
            set
            {
                treeOrg.AutoPostBack = value;
            }
        }

        public string SelectedText
        {
            get
            {
                List<TreeNodeItem> lstItem = treeOrg.Nodes.Cast<TreeNodeItem>().ToList();
                string text = lstItem.Where(c => c.ID == treeOrg.SelectedValue).Select(c => c.Text).FirstOrDefault();
                return text;
            }
        }

        public string SelectedValue
        {
            get
            {
                return treeOrg.SelectedValue;
            }
            set
            {
                treeOrg.SelectedValue = value;
            }
        }

        public Unit Width
        {
            get
            {
                return treeOrg.Width;
            }
            set
            {
                treeOrg.Width = value;
            }
        }

        #endregion

        public void LoadData()
        {
            treeOrg.SelectedValue = string.Empty;

            NewsParam param = new NewsParam(FunctionType.CatalogNews.GetCatalogNewsTreeData);
            MainController.Provider.Execute(param);

            List<TreeNodeItem> lstItemNode = new List<TreeNodeItem>();
            lstItemNode.Insert(0, new TreeNodeItem(string.Empty, string.Empty, "--- Không chọn danh mục nào ---"));
            foreach (var item in param.ListCatalogNews)
            {
                TreeNodeItem node = new TreeNodeItem();
                node.ID = Utility.GetString(item.CatalogNewsID);
                node.Parent = Utility.GetString(item.ParentID);
                node.Text = item.Name;

                lstItemNode.Add(node);
            }

            treeOrg.DataSource = lstItemNode;
            treeOrg.DataBind();
        }
    }
}