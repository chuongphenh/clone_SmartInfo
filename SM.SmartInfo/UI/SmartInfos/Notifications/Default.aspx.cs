using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SoftMart.Core.Security.Entity;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using System.Web;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.SmartInfos.Notifications
{
    public partial class Default : BasePage
    {
        public class RequestPermissionArgs : EventArgs
        {
            public List<IFunctionRight> lstRight { get; set; }
        }

        public int? IDActive
        {
            get
            {
                return (int?)ViewState["ReportDisplayName"];
            }
            set
            {
                ViewState["ReportDisplayName"] = value;
            }
        }

        public int? Type;

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as MasterPages.Common.SmartInfo).Search += Notification_Search;

            try
            {
                ucDisplay.RequestItemPermission += uc_RequestItemPermission;
                ucDisplay.RequestEdit += ucDisplay_RequestEdit;

                ucEdit.RequestItemPermission += uc_RequestItemPermission;
                ucEdit.RequestExit += ucEdit_RequestExit;

                if (!IsPostBack)
                {
                    SetupForm();
                    GetListNotification();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void Notification_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, SMX.TypeSearch.Notification));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetListNotificationByFilter();
                popSearch.Hide();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void ucEdit_RequestExit(object sender, EventArgs e)
        {
            try
            {
                if (ucEdit.NotificationID.HasValue)
                    LoadDataDisplayWithID(ucEdit.NotificationID, ucEdit.NotificationType);
                else
                    Response.Redirect(PageURL.Default);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void ucDisplay_RequestEdit(object sender, EventArgs e)
        {
            try
            {
                LoadDataEditWithID(IDActive, Type);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void popSearch_PopupClosed(object sender, EventArgs e)
        {
            try
            {
                popSearch.Hide();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnShowPopupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                popSearch.Show();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void uc_RequestItemPermission(RequestPermissionArgs param)
        {
            try
            {
                param.lstRight = GetPagePermission();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDisplay:
                        RepeaterItem item = e.Item;
                        string combined_data = e.CommandArgument.ToString();
                        string[] data_values = combined_data.Split(',');
                        HiddenField notificationId = (HiddenField)item.FindControl("NotificationId");
                        string notificationIdValue = notificationId.Value;
                        // Lấy giá trị từ mảng data_values
                        string data1 = data_values[0];
                        string data2 = data_values[1];
                        IDActive = Utility.GetNullableInt(data1);
                        Type = Utility.GetNullableInt(data2);
                        LoadDataDisplayWithID(Utility.GetNullableInt(data1), Utility.GetNullableInt(data2));

                        string url = string.Format("Default.aspx?ID={0}&F={1}&P={2}&T={3}", Utility.GetNullableInt(data1), ddlFilterTime.SelectedValue, hidPage.Value, Utility.GetNullableInt(data2));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);

                        GetListNotificationByFilter();
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddlFilterTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetListNotificationByFilter();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void ucPager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPage.Value = e.NewPageIndex.ToString();
                GetListNotificationByFilter();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SetupForm()
        {
            if (CacheManager.Profiles.MyProfile == null) HttpContext.Current.Response.Redirect(SharedComponent.Constants.PageURL.ErrorPage);
            var page = Request.Params["P"];
            if (string.IsNullOrWhiteSpace(page))
                hidPage.Value = "1";
            else
                hidPage.Value = page;

            UIUtility.BindDicToDropDownList(ddlFilterTime, SMX.FilterTime.dicDesc, false);

            var filterTime = Request.Params["F"];
            if (string.IsNullOrWhiteSpace(filterTime))
                ddlFilterTime.SelectedValue = Utils.Utility.GetString(SMX.FilterTime.All);
            else
                ddlFilterTime.SelectedValue = filterTime;
        }

        private void GetListNotification()
        {
            NotificationParam param = new NotificationParam(FunctionType.Notification.GetAllNotification);
            param.TypeTime = Utils.Utility.GetNullableInt(ddlFilterTime.SelectedValue);
            param.sharedUserId = Profiles.MyProfile.EmployeeID;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };
            MainController.Provider.Execute(param);

            if (param.ListNotification != null && param.ListNotification.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(Request.Params[SMX.Parameter.ID]))
                    Response.Redirect(string.Format("Default.aspx?ID={0}&F={1}&P={2}&T={3}", param.ListNotification.FirstOrDefault()?.NotificationID, ddlFilterTime.SelectedValue, hidPage.Value, param.ListNotification.FirstOrDefault()?.Type));
                else
                    LoadDataDisplay();
            }

            rptData.DataSource = param.ListNotification;
            rptData.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniTen, int.Parse(hidPage.Value), 5);
        }

        private void GetListNotificationByFilter()
        {
            ntf_Notification filter = new ntf_Notification();
            filter.FromDoDTG = dpkFromDoDTG.SelectedDate;
            filter.ToDoDTG = dpkToDoDTG.SelectedDate;
            filter.TextSearch = txtTextSearch.Text;

            NotificationParam param = new NotificationParam(FunctionType.Notification.SearchNotification);
            param.TypeTime = Utils.Utility.GetNullableInt(ddlFilterTime.SelectedValue);
            param.Notification = filter;
            param.sharedUserId = Profiles.MyProfile.EmployeeID;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };
            MainController.Provider.Execute(param);

            rptData.DataSource = param.ListNotification;
            rptData.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniTen, int.Parse(hidPage.Value), 5);
        }

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            ntf_Notification item = rptItem.DataItem as ntf_Notification;
            HiddenField notificationId = (HiddenField)rptItem.FindControl("NotificationId");

            Label ltrDoDTG = (Label)rptItem.FindControl("ltrDoDTG");
            HtmlGenericControl divDoDTG = rptItem.FindControl("divDoDTG") as HtmlGenericControl;
            var dateDoDTG = new DateTime(DateTime.Now.Year, item.DoDTG.Value.Month, item.DoDTG.Value.Day);
            if (dateDoDTG > DateTime.Now.Date)
            {
                divDoDTG.Attributes["class"] = "notify-date date-gray";
                if (item.Type != SMX.Notification.CauHinhGuiThongBao.SinhNhat && item.Type != SMX.Notification.CauHinhGuiThongBao.NgayThanhLap)
                    ltrDoDTG.Text = Utils.Utility.GetDateTimeString(item.DoDTG, "dd/MM");
                else
                    ltrDoDTG.Text = Utils.Utility.GetDateTimeString(item.DoDTG, "dd/MM/yyyy");

                if (dateDoDTG == DateTime.Now.Date.AddDays(1))
                    ltrDoDTG.Text = "Ngày mai";
            }
            else if (dateDoDTG == DateTime.Now.Date)
            {
                divDoDTG.Attributes["class"] = "notify-date date-red";
                ltrDoDTG.Text = "Hôm nay";
            }
            else if (dateDoDTG < DateTime.Now.Date)
            {
                divDoDTG.Attributes["class"] = "notify-date date-blue";
                if (item.Type != SMX.Notification.CauHinhGuiThongBao.SinhNhat && item.Type != SMX.Notification.CauHinhGuiThongBao.NgayThanhLap)
                    ltrDoDTG.Text = Utils.Utility.GetDateTimeString(item.DoDTG, "dd/MM");
                else
                    ltrDoDTG.Text = Utils.Utility.GetDateTimeString(item.DoDTG, "dd/MM/yyyy");
            }
            notificationId.Value = item.AlertID != null ? Utility.GetString(item.AlertID) : Utility.GetString(item.NotificationID);

            Label hypContent = rptItem.FindControl("ltrContent") as Label;
            hypContent.Text = item.Content;

            UIUtility.SetRepeaterItemIText(rptItem, "ltrNote", item.Note);

            LinkButton btnViewDisplay = rptItem.FindControl("btnViewDisplay") as LinkButton;
            //btnViewDisplay.CommandArgument = Utility.GetString(item.NotificationID);
            string data1 = item.AlertID != null ? Utility.GetString(item.AlertID) : Utility.GetString(item.NotificationID);
            string data2 = Utility.GetString(item.Type);
            string combined_data = $"{data1},{data2}";
            btnViewDisplay.CommandArgument = combined_data;
            btnViewDisplay.CommandName = SMX.ActionDisplay;

            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            divLink.Attributes.Add("onclick", "clickViewDetail('" + btnViewDisplay.ClientID + "');");

            if (IDActive != null && IDActive == item.NotificationID)
                divLink.Attributes.Add("class", "div-active");
        }

        private void LoadDataDisplay()
        {
            ucEdit.Visible = false;

            var notificationID = Request.Params[SMX.Parameter.ID];
            string typeParam = Request.Params["T"] ?? Request.Params["t"];
            if (!string.IsNullOrEmpty(typeParam) && int.TryParse(typeParam, out int type))
            {
                Type = type;
            }
            else
                Type = 1;
            if (!string.IsNullOrWhiteSpace(notificationID))
            {
                var param = new NotificationParam(FunctionType.Notification.LoadDataDisplay);
                param.NotificationID = Utils.Utility.GetNullableInt(notificationID);
                param.Type = (int)Type;
                param.sharedUserId = Profiles.MyProfile.EmployeeID;
                MainController.Provider.Execute(param);

                IDActive = Utility.GetNullableInt(notificationID);
                ucDisplay.SetupForm();
                ucDisplay.BindData(param.Notification);
                ucDisplay.Visible = true;
            }
            else
                ucDisplay.Visible = false;
        }

        private void LoadDataDisplayWithID(int? notificationID, int? type)
        {
            ucEdit.Visible = false;

            if (notificationID != null)
            {
                var param = new NotificationParam(FunctionType.Notification.LoadDataDisplay);
                param.NotificationID = notificationID;
                param.Type = (int)type;
                MainController.Provider.Execute(param);

                ucDisplay.SetupForm();
                ucDisplay.BindData(param.Notification);
                ucDisplay.Visible = true;
            }
            else
                ucDisplay.Visible = false;
        }

        private void LoadDataEditWithID(int? notificationID, int? type)
        {
            ucDisplay.Visible = false;

            if (notificationID != null)
            {
                var param = new NotificationParam(FunctionType.Notification.LoadDataDisplay);
                param.NotificationID = notificationID;
                param.Type = (int)type;
                MainController.Provider.Execute(param);

                ucEdit.SetupForm();
                ucEdit.BindData(param.Notification);
                ucEdit.Visible = true;
            }
            else
                ucEdit.Visible = false;
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this          , FunctionCode.VIEW },
                };
            }
        }
    }
}