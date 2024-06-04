using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class NotificationParam : BaseParam
    {
        public NotificationParam(string functionType)
            : base(Constants.BusinessType.Notification, functionType)
        {
        }
        public int PressAgencyHRID { get; set; }
        public agency_PressAgency PressAgency { get; set; }
        public int AlertID { get; set; }
        public int Type { get; set; }
        public int? NotificationID { get; set; }

        public ntf_Notification Notification { get; set; }

        public List<ntf_Notification> ListNotification { get; set; }
        public ntf_NotificationPushHistory ntf_NotificationPushHistory { get; set; }

        public List<ntf_NotificationPushHistory> ListPushNotificationHistory { get; set; }

        public int? TypeTime { get; set; }

        public int sharedUserId { get; set; }
        public  string TypeNoti { get; set; }
        public  string TitleNoti { get; set; }

    }
}