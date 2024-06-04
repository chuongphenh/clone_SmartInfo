using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.UI.UserControls.Search
{
    public partial class SearchNotificationUC : UserControl
    {
        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
            LoadData(Request.Params["q"], Utility.GetNullableInt(Request.Params["t"]));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);
            LoadData(Request.Params["q"], Utility.GetNullableInt(Request.Params["t"]));
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                BindObjectToRepeaterItem(e.Item);
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            hidPage.Value = "1";
        }

        public void LoadData(string searchText, int? searchType)
        {
            CommonParam param = new CommonParam(FunctionType.Common.NotificationSearch);
            param.SearchText = searchText;
            param.SearchType = searchType;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetNullableInt(hidPage.Value).GetValueOrDefault(1) - 1
            };
            MainController.Provider.Execute(param);

            rptData.DataSource = param.ListNotification;
            rptData.DataBind();

            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;
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
                ltrDoDTG.Text = Utils.Utility.GetDateTimeString(item.DoDTG, "dd/MM");

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
                ltrDoDTG.Text = Utils.Utility.GetDateTimeString(item.DoDTG, "dd/MM");
            }

            HyperLink hypContent = rptItem.FindControl("hypContent") as HyperLink;
            hypContent.Text = item.Content;
            hypContent.NavigateUrl = string.Format("/UI/SmartInfos/Notifications/Default.aspx?ID={0}", item.NotificationID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrNote", item.Note);
        }

        #endregion
    }
}