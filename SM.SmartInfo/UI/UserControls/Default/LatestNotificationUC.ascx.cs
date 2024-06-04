 using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.UserControls.Default
{
    public partial class LatestNotificationUC : UserControl
    {
        public delegate void FilterTime(string filter);

        public event FilterTime Filter;

        #region Events

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                BindObjectToRepeaterItem(e.Item);
        }

        protected void ddlFilterTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Filter != null)
                Filter(ddlFilterTime.SelectedValue);
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            UIUtility.BindDicToDropDownList(ddlFilterTime, SMX.FilterTime.dicDesc, false);
            ddlFilterTime.SelectedValue = Utils.Utility.GetString(SMX.FilterTime.All);
        }

        public void BindData(List<ntf_Notification> lstItem)
        {
            rptData.DataSource = lstItem;
            rptData.DataBind();
        }

        #endregion

        #region Private Method

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            ntf_Notification item = rptItem.DataItem as ntf_Notification;

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

            HyperLink hypContent = rptItem.FindControl("hypContent") as HyperLink;
            hypContent.Text = item.Content;
            hypContent.NavigateUrl = string.Format("/UI/SmartInfos/Notifications/Default.aspx?ID={0}&T={1}", item.NotificationID, item.Type);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrNote", item.Note);
        }

        #endregion
    }
}