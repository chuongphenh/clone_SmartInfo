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

namespace SM.SmartInfo.UI.Administrations.Organizations
{
    public partial class OrgTreeViewUC : BaseUserControl // System.Web.UI.UserControl
    {
        public event EventHandler SelectedNodeChanged;

        protected void tvOrg_SelectedNodeChanged(object sender, EventArgs e)
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

        public Organization GetCurrentOrg()
        {
            List<TreeNodeItem> lstNode = this.tvOrg.Nodes.Cast<TreeNodeItem>().ToList();

            TreeNodeItem node = this.tvOrg.SelectedNode;
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

            Organization item = new Organization();
            item.BreadCrumb = path;
            item.OrganizationID = int.Parse(tvOrg.SelectedValue);

            return item;
        }

        public TreeNodeItem GetCurrentNode()
        {
            return this.tvOrg.SelectedNode;
        }

        public TreeNodeItem GetFirstNode()
        {
            List<TreeNodeItem> lstNode = this.tvOrg.Nodes.Cast<TreeNodeItem>().ToList();
            return lstNode.FirstOrDefault();
        }

        public void SetSelectedNode(int orgID)
        {
            this.tvOrg.SelectedValue = orgID.ToString();
        }

        private void SetupForm()
        {
            OrganizationParam orgParam = new OrganizationParam(FunctionType.Administration.Organization.GetOrganizationTreeData);
            MainController.Provider.Execute(orgParam);
            List<Organization> lstAllOrg = orgParam.Organizations;

            List<TreeNodeItem> lstDataTree = new List<TreeNodeItem>();
            foreach (var item in lstAllOrg)
            {
                TreeNodeItem node = new TreeNodeItem();
                node.ID = Utility.GetString(item.OrganizationID);
                node.Parent = Utility.GetString(item.ParentID);
                node.Text = item.Name;
                lstDataTree.Add(node);
            }
            tvOrg.DataSource = lstDataTree;
            tvOrg.DataBind();
        }
    }
}