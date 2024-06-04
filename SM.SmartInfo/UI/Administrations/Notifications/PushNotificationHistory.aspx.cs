using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.CommonList;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administration.Notifications
{
    public partial class PushNotificationHistory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindRole();
                }
            }
            catch (SMXException)
            {
                ucErr.ShowError(Messages.ItemNotExisted);
            }

        }
        protected void grdNotificationPushHistory_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            grdNotificationPushHistory.CurrentPageIndex = e.NewPageIndex;
            BindRole(); 
        }

        protected void grdRole_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            //{
            //}
        }
        private void BindRole()
        {
            NotificationParam param = new NotificationParam(FunctionType.Notification.GetListPushNotificationHistory);
            MainController.Provider.Execute(param);
            grdNotificationPushHistory.DataSource = param.ListPushNotificationHistory;
            grdNotificationPushHistory.DataBind();
        }

    }
}