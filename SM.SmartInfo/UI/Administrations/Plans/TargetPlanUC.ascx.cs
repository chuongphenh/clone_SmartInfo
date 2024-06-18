using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.SharedComponent.Entities;

using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.UI.UserControls;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SM.SmartInfo.SharedComponent.Params.Common;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SoftMart.Core.UIControls;

namespace SM.SmartInfo.UI.Administrations.Plans
{
    public partial class TargetUC : System.Web.UI.UserControl
    {
        public delegate void ValidateAddTargetEventHandler(int targetID);
        public event ValidateAddTargetEventHandler OnValidateAddTarget;

        public bool AllowEdit
        {
            get
            {
                string s = (string)ViewState["AllowEdit"];
                if (string.IsNullOrEmpty(s))
                    s = bool.FalseString;

                return bool.Parse(s);
            }
            set
            {
                ViewState["AllowEdit"] = value.ToString();
            }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdTarget.Columns[grdTarget.Columns.Count - 1].Visible = AllowEdit;
                grdTarget.ShowFooter = AllowEdit;
            }
        }

        protected void grdTarget_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TargetInfo item = (TargetInfo)e.Item.DataItem;

                Label lblTargetCode = (Label)e.Item.FindControl("lblTargetCode");
                lblTargetCode.Text = item.TargetCode;

                HiddenField hidEmployeeID = (HiddenField)e.Item.FindControl("hidTargetID");
                hidEmployeeID.Value = item.TargetID.ToString();

                Label lblTargetName = (Label)e.Item.FindControl("lblTargetName");
                lblTargetName.Text = item.Name;

                Label lblTargetType = (Label)e.Item.FindControl("lblTargetType");
                lblTargetType.Text = Utility.GetDictionaryValue(SMX.TargetType.dctTargetTypes, item.TargetType);

                Label lblDescribe = (Label)e.Item.FindControl("lblDescribe");
                lblDescribe.Text = item.Description;

                LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
                btnDelete.OnClientClick = "return confirm('Bạn có thực sự muốn xóa?')";
                btnDelete.CommandName = SMX.ActionDelete;
            }
        }

        protected void grdTarget_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            try
            {
                List<TargetInfo> lstTarget = GetListTargetInGrid();
                switch (e.CommandName)
                {
                    // Delete employee from grid
                    case SMX.ActionDelete:
                        {
                            lstTarget.RemoveAt(e.Item.ItemIndex);
                            UIUtility.BindDataGrid(grdTarget, lstTarget);
                            break;
                        }

                    // Insert new employee to grid
                    case SMX.ActionNew:
                        {
                            TargetSearchBox ucTarget = UIUtility.FindControlInDataGridFooter(grdTarget, "ucTarget") as TargetSearchBox;
                            //EmployeeSelectorUC ucEmployee = UIUtil.FindControlInDataGridFoolter(grdTarget, "ucEmployee") as EmployeeSelectorUC;
                            int? targetID = Utility.GetNullableInt(ucTarget.SelectedValue);
                            if (targetID != null)
                            {
                                ValidateTargetIsExistedInGrid(lstTarget, targetID.Value);
                                if (OnValidateAddTarget != null)
                                    OnValidateAddTarget(targetID.Value);

                                TargetInfo item = GetShortTargetInfoByID(targetID.Value);
                                lstTarget.Add(item);

                                UIUtility.BindDataGrid(grdTarget, lstTarget);
                            }

                            break;
                        }
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        public void BindData(List<TargetInfo> lstTarget)
        {
            UIUtility.BindDataGrid(grdTarget, lstTarget);
        }

        public List<TargetInfo> GetListTargetInGrid()
        {
            var lstTarget = new List<TargetInfo>();

            foreach (DataGridItem item in grdTarget.Items)
            {
                Label lblTargetCode = (Label)item.FindControl("lblTargetCode");
                Label lblTargetName = (Label)item.FindControl("lblTargetName");
                Label lblDescribe = (Label)item.FindControl("lblDescribe");
                Label lblTargetType = (Label)item.FindControl("lblTargetType");
                HiddenField hidTargetID = (HiddenField)item.FindControl("hidTargetID");

                var target = new TargetInfo();
                target.TargetCode = lblTargetCode.Text;
                target.Name = lblTargetName.Text;
                target.TargetType = Utility.GetDictionaryKey(SMX.TargetType.dctTargetTypes, lblTargetType.Text);
                target.TargetID = Utility.GetInt(hidTargetID.Value);
                target.Description = lblDescribe.Text;

                lstTarget.Add(target);
            }

            return lstTarget;
        }

        private TargetInfo GetShortTargetInfoByID(int id)
        {
            CommonParam param = new CommonParam(FunctionType.Common.GetShortTargetByID);
            param.TargetId = id;

            MainController.Provider.Execute(param);

            return param.TargetInfo;
        }
        
        private void ValidateTargetIsExistedInGrid(List<TargetInfo> lstTarget, int targetID)
        {
            int count = lstTarget.Where(t => t.TargetID == targetID).Count();
            if (count > 0)
                throw new SMXException(Messages.Target.DuplicateTargetInGrid);
        }
    }
}