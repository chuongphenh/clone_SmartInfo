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

namespace SM.SmartInfo.UI.SmartInfos.EmulationAndRewards
{
    public partial class SideBarTreeViewUC : BaseUserControl
    {
        public event EventHandler SelectedNodeChanged;

        protected void tvEmulationAndReward_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (SelectedNodeChanged != null)
            {
                SelectedNodeChanged(sender, e);
            }
        }

        public er_EmulationAndReward GetCurrentCategory()
        {
            List<TreeNodeItem> lstNode = this.tvEmulationAndReward.Nodes.Cast<TreeNodeItem>().ToList();

            TreeNodeItem node = this.tvEmulationAndReward.SelectedNode;
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

            er_EmulationAndReward item = new er_EmulationAndReward();
            if (this.tvEmulationAndReward.SelectedNode != null && !string.IsNullOrWhiteSpace(this.tvEmulationAndReward.SelectedNode.Parent))
                item.EmulationAndRewardUnit = tvEmulationAndReward.SelectedValue;

            return item;
        }

        public TreeNodeItem GetCurrentNode()
        {
            return this.tvEmulationAndReward.SelectedNode;
        }

        public TreeNodeItem GetFirstNode()
        {
            List<TreeNodeItem> lstNode = this.tvEmulationAndReward.Nodes.Cast<TreeNodeItem>().ToList();
            return lstNode.FirstOrDefault();
        }

        public void SetSelectedNode(string id)
        {
            this.tvEmulationAndReward.SelectedValue = id;
        }

        public void SetupForm()
        {
            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.BuildTreeListEmulationAndRewards);
            MainController.Provider.Execute(param);

            List<er_EmulationAndReward> lstEmulationAndReward = param.ListEmulationAndReward;

            List<TreeNodeItem> lstDataTree = new List<TreeNodeItem>();

            foreach (var item in lstEmulationAndReward.GroupBy(x => x.Year))
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

                foreach (var itemDetail in item.ToList())
                {
                    TreeNodeItem nodeDetail = new TreeNodeItem();
                    nodeDetail.ID = itemDetail.EmulationAndRewardUnit;
                    nodeDetail.Parent = Utility.GetString(itemDetail.Year);
                    nodeDetail.Text = itemDetail.EmulationAndRewardUnit;

                    lstDataTree.Add(nodeDetail);
                }
            }

            tvEmulationAndReward.DataSource = lstDataTree;
            tvEmulationAndReward.DataBind();
        }
    }
}