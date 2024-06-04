using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Commons;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.SmartInfo.BIZ.Commons
{
    class DataTrackingBiz : BizBase
    {
        private DataTrackingDao _dao = new DataTrackingDao();

        /// <summary>
        ///   Chức năng lưu vết lại thao tác trên hệ thống
        /// </summary>
        /// <param name="feature">Lấy trong SMX.DataTracking.Feature</param>
        /// <param name="actionType">Lấy trong SMX.DataTracking.ActionType</param>
        /// <param name="nameAction">Tên thao tác tác nghiệp vào hệ thống</param>
        /// <param name="content">Nội dung chi tiết của thao tác</param>
        public void LogDataTracking(int feature, int? actionType, string nameAction, string content, int refType, int? refID)
        {
            //try
            //{
            if (Profiles.MyProfile == null)
                return;
            DataTracking item = new DataTracking();
            item.Feature = feature;
            item.ActionType = actionType;
            item.Name = nameAction;
            string featureName = Utils.Utility.GetDictionaryValue(SMX.DataTracking.Feature.dicDescFeatureType, feature);
            string actionName = Utils.Utility.GetDictionaryValue(SMX.DataTracking.ActionType.dicDescActionType, actionType);
            item.Content = string.Format("{0}:", featureName);
            if (!string.IsNullOrEmpty(actionName))
                item.Content += " " + actionName;
            if (!string.IsNullOrEmpty(nameAction))
                item.Content += " " + nameAction;
            if (!string.IsNullOrEmpty(content))
                item.Content += " " + content;
            item.ActionDTG = DateTime.Now;
            item.UserID = Profiles.MyProfile.EmployeeID;
            item.OrganizationID = (Profiles.MyProfile.OrganizationID ?? Profiles.MyProfile.ListDirectManagingOrganizationID.FirstOrDefault());
            item.RefType = refType;
            item.RefID = refID;
            _dao.InsertDataTracking(item);
            //}
            //catch
            //{
            //}
        }
    }
}