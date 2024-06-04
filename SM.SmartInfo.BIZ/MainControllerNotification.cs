using SM.SmartInfo.BIZ.Notification;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteNotification(NotificationParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Notification.DeleteNotificationByHRAlertID:
                    {
                        var biz = new NotificationBiz();
                        biz.DeleteNotificationByHRAlertID(param.AlertID);
                        break;
                    }
                case FunctionType.Notification.DeleteNotificationByPressAgencyID:
                    {
                        var biz = new NotificationBiz();
                        biz.DeleteNotificationByPressAgencyID(param.PressAgencyHRID);
                        break;
                    }
                    
                case FunctionType.Notification.AddOrUpdateItem:
                    {
                        var biz = new NotificationBiz();
                        biz.AddOrUpdateItem(param.Notification);
                        break;
                    }
                case FunctionType.Notification.InsertFirebase:
                    {
                        var biz = new NotificationBiz();
                        biz.AddOrUpdateItem(param.Notification);
                        break;
                    }
                case FunctionType.Notification.GetListPushNotificationHistory:
                    {
                        var biz = new NotificationBiz();
                        biz.GetListNotificationPushHistory(param);
                        break;
                    }
                case FunctionType.Notification.AddOrUpdateItemNotification:
                    {
                        var biz = new NotificationBiz();
                        biz.AddOrUpdateItemNotification(param.PressAgency);
                        break;
                    }
                case FunctionType.Notification.GetAllNotification:
                    {
                        var biz = new NotificationBiz();
                        biz.GetAllNotification(param);
                        break;
                    }
                case FunctionType.Notification.SearchNotification:
                    {
                        var biz = new NotificationBiz();
                        biz.SearchNotification(param);
                        break;
                    }
                case FunctionType.Notification.LoadDataDisplay:
                    {
                        var biz = new NotificationBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.Notification.PushNotification:
                    {
                        var biz = new NotificationBiz();
                         biz.PushNotification(param);
                        break;
                    }
                case FunctionType.Notification.UpdateItem:
                    {
                        var biz = new NotificationBiz();
                        biz.UpdateItem(param);
                        break;
                    }
            }
        }
    }
}