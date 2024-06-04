using System.IO;
using System.Net;
using System.Net.Security;
using System.Collections.Generic;
using SM.SmartInfo.DAO.Notification;
using System.Web.Script.Serialization;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Common;
using System.Security.Cryptography.X509Certificates;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System;
using SM.SmartInfo.SharedComponent.Constants;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SM.SmartInfo.DAO.Firebase;
using SM.SmartInfo.Utils;
using System.Threading.Tasks;
using SM.SmartInfo.CacheManager;
using System.Linq;

namespace SM.SmartInfo.BIZ.Notification
{
    class NotificationBiz : BizBase
    {
        private NotificationDao _dao = new NotificationDao();
        private ApiFirebase firebase = new ApiFirebase();
        private TokenDeviceDao _daoToken = new TokenDeviceDao();


        public void DeleteNotificationByHRAlertID(int alertID)
        {
            _dao.DeleteNotificationByHRAlertID(alertID);
        }
        public void DeleteNotificationByPressAgencyID(int alertID)
        {
            _dao.DeleteNotificationByPressAgencyID(alertID);
        }

        public void AddOrUpdateItem(ntf_Notification notification)
        {
            if (_dao.checkExistingNotificationByAlertId(notification.AlertID))
            {
                notification.UpdateDTG = DateTime.Now;

                _dao.UpdateNotification(notification);
            }
            else
            {
                _dao.InsertNewNotification(notification);
            }
        }
        public void AddOrUpdateItemNotification(agency_PressAgency pressAgency)
        {
            // var newNTF = new ntf_Notification()
            //    {
            //        DoDTG = pressAgency.,
            //        Content = item.TypeDate == 1 ? (string.IsNullOrEmpty(item.Content) ? "Sinh nhật" : item.Content) + " của ông/bà" + $" {hrName} " + $"({typeDate})" :
            //        (string.IsNullOrEmpty(item.Content) ? "Ngày giỗ" : item.Content) + " của ông/bà" + $" {hrName} " + $"({Convert.ToDateTime(item.AlertDTG).ToString("dd/MM/yyyy")} - {typeDate})",
            //        Type = item.TypeDate == 1 ? 1 : 5,
            //        Note = hrPosition,
            //        Comment = null,
            //        AlertID = param.PressAgencyHRAlert.PressAgencyHRAlertID,
            //        isDeleted = 0,
            //        CreatedBy = Profiles.MyProfile.UserName
            //    };
            //if (_dao.checkExistingNotificationByAlertId(pressAgency.PressAgencyID))
            //{
            //    notification.UpdateDTG = DateTime.Now;

            //    _dao.UpdateNotification(notification);
            //}
            //else
            //{
            //    _dao.InsertNewNotification(notification);
            //}
        }
        public void SearchNotification(CommonParam param)
        {
            param.ListNotification = _dao.SearchNotification(param.SearchText, param.PagingInfo, param.EmployeeId.GetValueOrDefault(0));
        }

        public List<ntf_Notification> GetAllNotification(int userId)
        {
            return _dao.GetAllListNotificationForDefault(userId);
        }
        public void GetListNotificationPushHistory(NotificationParam param)
        {
            param.ListPushNotificationHistory = _dao.GetNotificationPushHistory();
        }

        public void FilterNotification(CommonParam param)
        {
            int? typeTime = param.TypeTime;
            param.ListNotification = _dao.GetAllListNotification(typeTime, param.PagingInfo, param.EmployeeId.GetValueOrDefault(0));
        }

        public void UpdateItem(NotificationParam param)
        {
            _dao.UpdateItem(param.Notification);
        }

        public void InsertFirebase(NotificationParam param)
        {
            _dao.InsertItem(param.ntf_NotificationPushHistory);
        }

        public void PushNotification(NotificationParam param)
        {
            NotificationDao _daoNoti = new NotificationDao();
            List<ntf_TokenDevice> lstToken = new List<ntf_TokenDevice>();
            var noti = param.Notification;
            string strTitle = "";
            string strName = "";
            switch (param.TypeNoti)
            {
                case "TinTucSuVuMoi":
                    strTitle = $"{SmartInfo.CacheManager.Profiles.MyProfile.FullName} vừa thêm một ";
                    break;
                case "Comment":
                    strTitle = $"{SmartInfo.CacheManager.Profiles.MyProfile.FullName} đã bình luận vào một ";
                    break;
            }

            switch (noti.Type)
            {
                case SMX.CommentRefType.News:
                    if (param.TypeNoti == "TinTucSuVuMoi")
                        strTitle += $"tin tức mới";
                    else
                    {
                        // nếu không phải là tin tức mới có nghĩa đang là bình luận, không có thông báo ở bình luận tin tức => return
                        return;
                    }

                    strName += _dao.GetItemByID<SharedComponent.Entities.News>(noti.NotificationID)?.Name;
                    lstToken = _daoToken.GetAllTokenDevice(SMX.Feature.News);
                    break;
                case SMX.CommentRefType.NegativeNews:
                    if (param.TypeNoti == "TinTucSuVuMoi")
                        strTitle += $"sự vụ mới";
                    else
                        strTitle += $"sự vụ";

                    strName += _dao.GetItemByID<SharedComponent.Entities.News>(noti.NotificationID)?.Name;
                    lstToken = _daoToken.GetAllTokenDevice(SMX.Feature.NegativeNews);
                    break;

                case SMX.CommentRefType.PressAgency:
                    strTitle += $"tổ chức";
                    strName += _dao.GetItemByID<agency_PressAgency>(noti.NotificationID)?.Name;
                    lstToken = _daoToken.GetAllTokenDevice(SMX.Feature.Agency_PressAgency);
                    break;

                case SMX.CommentRefType.Birthday:
                case SMX.CommentRefType.Other:
                    strTitle += $"sự kiện";
                    strName += _daoNoti.GetNotificationByID(noti.NotificationID, noti.Type)?.Content;
                    lstToken = _daoToken.GetListTokenDeviceByPressAgencyHRID(noti.NotificationID);
                    break;
                case SMX.CommentRefType.Anniversary:
                case SMX.CommentRefType.Establishday:
                case SMX.CommentRefType.Holiday:
                case SMX.CommentRefType.Notification:
                    strTitle += $"sự kiện";
                    strName += _daoNoti.GetNotificationByID(noti.NotificationID, noti.Type)?.Content;
                    lstToken = _daoToken.GetAllTokenDevice(SMX.Feature.Events);
                    break;
                default:
                    return;
            }
            string strContent = $"{strName}: {noti.Content}";

            Dictionary<string, string> dataDetail = new Dictionary<string, string>
                    {
                        { "RefID", noti.NotificationID?.ToString() ?? "" },
                        { "RefType", noti.Type.ToString() ?? "" },
                        { "CreateDate", DateTime.Now.ToString("dd/MM/yyyy") + Guid.NewGuid().ToString() ?? "" },
                        //{ "typeNews", param.Type.ToString() ?? "" },
                        { "subID", "" }
                    };
            //14:Sự vụ
            //15:Tổ chức
            //16:Tin tức
            //18:Thông báo
            SendNotificationFirebase(strTitle, strContent, dataDetail, lstToken);
            //SendNotificationFirebase(noti.Content, string.Format("Ý kiến chỉ đạo: {0}", noti.Comment));
        }

        public void LoadDataDisplay(NotificationParam param)
        {
            param.Notification = _dao.GetNotificationByID(param.NotificationID, param.Type);
        }

        public void GetAllNotification(NotificationParam param)
        {
            param.ListNotification = _dao.GetAllListNotification(param.TypeTime, param.PagingInfo, param.sharedUserId);
        }

        public void SearchNotification(NotificationParam param)
        {
            param.ListNotification = _dao.SearchNotification(param.Notification, param.TypeTime, param.PagingInfo, param.sharedUserId);
        }

        #region Send Notification Firebase

        private void SendNotificationFirebase(string title, string content, Dictionary<string, string> dataDetail, List<ntf_TokenDevice> lstToken)
        {
            string jsonDetailData = JsonConvert.SerializeObject(dataDetail);

            RequestBodyDTO requestBodyDTO = new RequestBodyDTO();
            MessageNoti messageNoti = new MessageNoti
            {
                messageId = Guid.NewGuid().ToString(),
                messageType = "MSG_NOTI_APP_NEWS",
                bodyTitle = title,
                bodyShortContent = content,
                bodyContent = jsonDetailData,
                listReceiver = lstToken
                             .Where(token => !string.IsNullOrEmpty(token.FCMToken))
                             .Select(token => new Receiver
                             {
                                 receiverAppId = "MOBILEAPP_SMART_INFO",
                                 receiverType = "FCM_TOKEN",
                                 receiverToken = token.FCMToken,
                                 receiverUserId = token.EmployeeID?.ToString() ?? string.Empty
                             })
                             .ToList()
            };
            requestBodyDTO.listMessages = new List<MessageNoti> { messageNoti };
            var userName = Profiles.MyProfile.UserName;
            var tokenKeyCloak = Profiles.MyProfile.TokenKeyCloak;
            // 
            Task.Run(async () =>
            {
                try
                {
                    // Gửi thông báo
                    bool isSuccess = await firebase.PushMessageNoti(requestBodyDTO, tokenKeyCloak, userName);

                    // retry khi gửi lỗi
                    if (!isSuccess)
                    {
                        await HandlePushMessageError(requestBodyDTO, userName);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.ServiceNotification.LogDebug("ERROR: WEB - Send notification failed: " + Environment.NewLine + ex.ToString());
                }
            }).GetAwaiter().GetResult();
        }
        private async Task HandlePushMessageError(RequestBodyDTO requestBodyDTO, string userFullName)
        {
            LogManager.ServiceNotification.LogDebug("INFO: Retry sending notification due to previous failure.");
            ApiFirebase firebaseRetry = new ApiFirebase();
            int retryCount = 3;

            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    string tokenKeyCloak = await firebaseRetry.GetTokenAsync();
                    bool isSuccess = await firebaseRetry.PushMessageNoti(requestBodyDTO, tokenKeyCloak, userFullName);
                    if (isSuccess)
                    {
                        LogManager.ServiceNotification.LogDebug("INFO: Successfully resent notification on retry #" + (i + 1));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.ServiceNotification.LogError($"ERROR: Retry #{i + 1} failed: " + ex.ToString());
                }

                await Task.Delay(1000);
            }
            LogManager.ServiceNotification.LogError("ERROR_SCAN: Failed to send notification after multiple retries.");
        }
        private string webAddr = "https://exp.host/--/api/v2/push/send";

        public void PushNotify(NotifyDto request)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = request.To,
                sound = "default",
                title = request.Title,
                body = request.Body,
                data = request.Data
            };

            var serializer = new JavaScriptSerializer();
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = serializer.Serialize(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            WebResponse tresponse = httpWebRequest.GetResponse();
            Stream dataStream = tresponse.GetResponseStream();
            StreamReader treader = new StreamReader(dataStream);
            string sresponseFromServer = treader.ReadToEnd();

            dataStream.Close();
            tresponse.Close();
        }

        #endregion

        #region Class helper

        public class NotifyDto
        {
            public string To { get; set; } // token để chỉ định người nhận thông báo hoặc nhóm người nhận thông báo           

            public string Title { get; set; }

            public string Body { get; set; }

            public NotifyData Data { get; set; }
        }

        public class NotifyData
        {

        }

        #endregion
    }
}