using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Entity;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.EmulationAndRewards
{
    public partial class DisplayUC : BaseUserControl
    {
        public er_EmulationAndReward Filter
        {
            get
            {
                return (er_EmulationAndReward)ViewState["Filter"];
            }
            set
            {
                ViewState["Filter"] = value;
            }
        }

        #region Events

        protected void btnSwitchEmployee_Click(object sender, System.EventArgs e)
        {
            try
            {
                ActiveTab(SMX.EmulationAndRewardSubjectRewarded.CaNhan);

                Filter.SubjectType = SMX.EmulationAndRewardSubjectType.CaNhan;
                LoadData(Filter);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnSwitchOrganization_Click(object sender, System.EventArgs e)
        {
            try
            {
                ActiveTab(SMX.EmulationAndRewardSubjectRewarded.DonViToChuc);

                Filter.SubjectType = SMX.EmulationAndRewardSubjectType.DonViToChuc;
                LoadData(Filter);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            grdData.PageSize = SMX.smx_PageMiniTen;

            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.SetupFormDisplay);
            param.er_EmulationAndReward = Filter;
            MainController.Provider.Execute(param);

            btnSwitchEmployee.Text = string.Format("Cá nhân ({0})", param.ListEmulationAndRewardSubject.Count(x => x.Type == SMX.EmulationAndRewardSubjectType.CaNhan));
            btnSwitchOrganization.Text = string.Format("Tập thể ({0})", param.ListEmulationAndRewardSubject.Count(x => x.Type == SMX.EmulationAndRewardSubjectType.DonViToChuc));
        }

        public void LoadData(er_EmulationAndReward filter)
        {
            Filter = filter;

            SetupForm();

            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListEmulationAndRewardByFilter);
            param.PagingInfo = new PagingInfo(grdData.CurrentPageIndex, grdData.PageSize);
            param.er_EmulationAndReward = filter;
            MainController.Provider.Execute(param);

            UIUtility.BindDataGrid(grdData, param.ListEmulationAndRewardSubject, param.PagingInfo.RecordCount);
        }

        #endregion

        public void ActiveTab(int? subjectType)
        {
            switch (subjectType)
            {
                case SMX.EmulationAndRewardSubjectRewarded.CaNhan:
                    btnSwitchEmployee.CssClass = "title-active";
                    btnSwitchOrganization.CssClass = "";
                    break;
                case SMX.EmulationAndRewardSubjectRewarded.DonViToChuc:
                    btnSwitchEmployee.CssClass = "";
                    btnSwitchOrganization.CssClass = "title-active";
                    break;
                default:
                    btnSwitchEmployee.CssClass = "title-active";
                    btnSwitchOrganization.CssClass = "";
                    break;
            }
        }

        protected void grdData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    er_EmulationAndRewardSubject item = e.Item.DataItem as er_EmulationAndRewardSubject;

                    LinkButton btnCode = e.Item.FindControl("btnCode") as LinkButton;
                    btnCode.CommandArgument = Utils.Utility.GetString(item.EmulationAndRewardSubjectID);
                    btnCode.CommandName = SMX.ActionDisplay;
                    btnCode.Text = item.Code;

                    UIUtility.SetGridItemIText(e.Item, "ltrName", item.Name);
                    UIUtility.SetGridItemIText(e.Item, "ltrLatestTitle", item.LatestTitle);
                    UIUtility.SetGridItemIText(e.Item, "ltrLatestEmulationAndRewardUnit", item.LatestEmulationAndRewardUnit);
                    UIUtility.SetGridItemIText(e.Item, "ltrEmail", item.Email);
                    UIUtility.SetGridItemIText(e.Item, "ltrMobile", item.Mobile);
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void grdData_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDisplay:
                        var subjectID = Utils.Utility.GetNullableInt(e.CommandArgument.ToString());

                        string url = string.Format("/UI/PopupPages/EmulationAndRewards/Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { subjectID.GetValueOrDefault(0) }), btnReloadAppendix.ClientID);
                        UIUtility.OpenPopupWindow(this.Page, url, 1000, 700);
                        break;
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void grdData_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                grdData.CurrentPageIndex = e.NewPageIndex;
                LoadData(Filter);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnReloadAppendix_Click(object sender, System.EventArgs e)
        {
            try
            {
                LoadData(Filter);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
    }
}