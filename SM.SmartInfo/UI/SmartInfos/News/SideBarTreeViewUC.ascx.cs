using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Core.UIControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class SideBarTreeViewUC : BaseUserControl
    {
        public event EventHandler SelectedNodeChanged;

        protected void tvNews_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (SelectedNodeChanged != null)
            {
                SelectedNodeChanged(sender, e);
            }
        }

        public SharedComponent.Entities.News GetCurrentCategory()
        {
            List<TreeNodeItem> lstNode = this.tvNews.Nodes.Cast<TreeNodeItem>().ToList();

            TreeNodeItem node = this.tvNews.SelectedNode;
            string path = string.Empty;
            do
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = node.Text;
                }
                else
                {
                    path = node.Text + " > " + path;
                }

                node = lstNode.Find(en => en.ID == node.Parent);
            } while (node != null);

            SharedComponent.Entities.News item = new SharedComponent.Entities.News();
            if (this.tvNews.SelectedNode != null && !string.IsNullOrWhiteSpace(this.tvNews.SelectedNode.Parent))
            {
                item.CategoryName = path;
                item.Category = int.Parse(tvNews.SelectedValue.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0]);
            }

            return item;
        }

        public TreeNodeItem GetCurrentNode()
        {
            return this.tvNews.SelectedNode;
        }

        public TreeNodeItem GetFirstNode()
        {
            List<TreeNodeItem> lstNode = this.tvNews.Nodes.Cast<TreeNodeItem>().ToList();
            return lstNode.FirstOrDefault();
        }

        public void SetSelectedNode(int orgID)
        {
            this.tvNews.SelectedValue = orgID.ToString();
        }

        public void SetupForm()
        {
            NewsParam param = new NewsParam(FunctionType.News.BuildTreeListNews);
            MainController.Provider.Execute(param);

            List<SharedComponent.Entities.News> lstNews = param.ListNews;

            List<TreeNodeItem> lstDataTree = new List<TreeNodeItem>();

            TreeNodeItem nodeAll = new TreeNodeItem();
            nodeAll.ID = "All";
            nodeAll.Parent = string.Empty;
            nodeAll.Text = "Tất cả";
            lstDataTree.Add(nodeAll);

            foreach (var item in lstNews.GroupBy(x => x.YearCreated))
            {
                TreeNodeItem node = new TreeNodeItem();

                if (item.Key == DateTime.Now.Year)
                {
                    node.ID = Utility.GetString(item.Key);
                    node.Parent = string.Empty;
                    node.Text = string.Format("Năm nay ({0})", item.Key);
                }
                else
                {
                    node.ID = Utility.GetString(item.Key);
                    node.Parent = string.Empty;
                    node.Text = string.Format("Năm {0}", item.Key);
                }

                lstDataTree.Add(node);

                //foreach (var itemDetail in item.ToList())
                //{
                //    TreeNodeItem nodeDetail = new TreeNodeItem();
                //    nodeDetail.ID = string.Format("{0}_{1}", Utility.GetString(itemDetail.Category), Utility.GetString(itemDetail.YearCreated));
                //    nodeDetail.Parent = Utility.GetString(itemDetail.YearCreated);
                //    nodeDetail.Text = string.Format("{0} ({1})", itemDetail.CategoryName, itemDetail.CategoryCount);

                //    lstDataTree.Add(nodeDetail);
                //}
            }

            tvNews.DataSource = lstDataTree;
            tvNews.DataBind();
        }
    }
}