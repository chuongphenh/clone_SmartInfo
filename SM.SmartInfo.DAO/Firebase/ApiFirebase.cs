using Newtonsoft.Json;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Notification;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO.Firebase
{
    public class ApiFirebase
    {
        private readonly string _keycloakUrl;
        private readonly string _pushNotiUrl;
        private readonly string _grantType;
        private readonly string _userName;
        private readonly string _passWord;
        private NotificationDao _dao = new NotificationDao();
        public ApiFirebase()
        {
            _keycloakUrl = Utils.ConfigUtils.GetConfig("UrlApiKeyCloak");
            _grantType = Utils.ConfigUtils.GetConfig("GrantType");
            _userName = Utils.ConfigUtils.GetConfig("UserBasicAuth");
            _passWord = Utils.ConfigUtils.GetConfig("PassBasicAuth");
            _pushNotiUrl = Utils.ConfigUtils.GetConfig("UrlApiMessage");
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                LogManager.ServiceNotification.LogDebug("START: >>>>>>>>>>>>>>>>> Call Keycloak >>>>>>>>>>>>>>>>>");
                LogManager.ServiceNotification.LogDebug("URL Call Keycloak       : " + _keycloakUrl);
                LogManager.ServiceNotification.LogDebug("Grant Type              : " + _grantType);
                LogManager.ServiceNotification.LogDebug("UserName                : " + _userName);
                LogManager.ServiceNotification.LogDebug("Password                : " + _passWord);
                LogManager.ServiceNotification.LogDebug("URL push Notification   : " + _pushNotiUrl);

                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri(_keycloakUrl);
                client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "bW9iaWxlcmV0YWlsX3VzZXIxOjg3MDkyZTY5LWJkNjUtNDhhOC1iOGJhLTg3NWI5Y2NkNTg3NA==");
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_userName}:{_passWord}"));
                client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", _grantType)
                });
                string contentString = await content.ReadAsStringAsync();
                LogManager.ServiceNotification.LogDebug("Request Content: " + contentString);
                //foreach (var header in client.DefaultRequestHeaders)
                //{
                //    LogManager.ServiceNotification.LogDebug($"Header: {header.Key} - {string.Join(",", header.Value)}");
                //}
                HttpResponseMessage response = await client1.PostAsync(_keycloakUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    LogManager.ServiceNotification.LogDebug("END: <<<<<<<<<<<<<<<<<<<<<< KeyCloak call SUCCESSFUL  <<<<<<<<<<<<<<<<<<<<<<");
                    string responseString = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                    //LogManager.ServiceNotification.LogDebug("Response Keycloak: " + jsonResponse);
                    string accessToken = jsonResponse.access_token;
                    string typeToken = jsonResponse.token_type;
                    //HttpContext.Current.Response.Cookies.Add(new HttpCookie("Token_Type", typeToken));
                    return accessToken;
                }
                else
                {
                    LogManager.ServiceNotification.LogError("Call API KeyCloak: " + response);
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.ServiceNotification.LogError("ERROR: Call API KeyCloak Failed: " + Environment.NewLine + ex.ToString());
                return null;
            }
        }
        public async Task<bool> PushMessageNoti(RequestBodyDTO requestBody, string tokenKeyCloak, string userFullName)
        {
            try
            {
                //this.InsertFirebaseDB(title, content, jsonData);
                LogManager.ServiceNotification.LogDebug("STATUS: >>>>>>>>>>>>>>>>> Bắt đầu gửi thông báo >>>>>>>>>>>>>>>>>");
                LogManager.ServiceNotification.LogDebug($"INFO: URL push thông báo: {_pushNotiUrl}");
                if (string.IsNullOrEmpty(tokenKeyCloak))
                {
                    LogManager.ServiceNotification.LogError("ERROR: Không có Token Keycloak");
                    return false;
                }
                HttpClient client = new HttpClient();
                string transactionId = Guid.NewGuid().ToString();
                string clientMessageId = Guid.NewGuid().ToString();
                client.BaseAddress = new Uri(_pushNotiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKeyCloak);
                client.DefaultRequestHeaders.Add("transactionId", transactionId);
                client.DefaultRequestHeaders.Add("clientMessageId", clientMessageId);

                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                //
                string contentString = await Content.ReadAsStringAsync();
                LogManager.ServiceNotification.LogDebug("INFO: Request Content: " + contentString);
                //foreach (var header in client.DefaultRequestHeaders)
                //{
                //    LogManager.ServiceNotification.LogError($"Header: {header.Key} - {string.Join(",", header.Value)}");
                //}
                HttpResponseMessage response = await client.PostAsync(_pushNotiUrl, Content);
                // Xử lý phản hồi từ dịch vụ
                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung phản hồi
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Chuyển đổi nội dung phản hồi sang đối tượng SuccessResponse
                    SuccessResponse successResponse = JsonConvert.DeserializeObject<SuccessResponse>(jsonResponse);
                    LogManager.ServiceNotification.LogError($"INFO: Response: {jsonResponse}");

                    string status = "";
                    // Kiểm tra trường data trong phản hồi để xem chi tiết thông báo
                    if (successResponse != null)
                    {
                        switch (successResponse.data.status)
                        {
                            case 0:
                                LogManager.ServiceNotification.LogDebug("SUCCESS: Push notification sent successfully.");
                                LogManager.ServiceNotification.LogDebug("STATUS: Start insert firebase.");
                                InsertPushNotification(requestBody, status, transactionId, 0, clientMessageId, userFullName);
                                LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                                // Log thông báo thành công
                                return true;
                            //break;
                            case 1:
                                LogManager.ServiceNotification.LogDebug("ERROR: Failed to send push notification due to invalid request data.");
                                status = "Failed to send push notification due to invalid request data";
                                LogManager.ServiceNotification.LogDebug("Status: Start insert firebase");
                                InsertPushNotification(requestBody, status, transactionId, 1, clientMessageId, userFullName);
                                LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                                // Log thông báo thành công
                                // Log thông báo lỗi do request data invalid
                                return false;
                            //break;
                            case -1:
                                status = "Failed to push message to RabbitMQ.";
                                InsertPushNotification(requestBody, status, transactionId, -1, clientMessageId, userFullName);
                                // Log thông báo lỗi do push message lên rabbit lỗi
                                LogManager.ServiceNotification.LogDebug("ERROR: Failed to push message to RabbitMQ.");
                                LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                                return false;
                            //break;
                            default:
                                // Log thông báo lỗi khác
                                LogManager.ServiceNotification.LogDebug("ERROR: Failed to send push notification. Error message: " + successResponse.data.errorMessage);
                                LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                                return false;
                                //break;
                        }
                    }
                    else
                    {
                        LogManager.ServiceNotification.LogError("ERROR: Failed to send push notification. Error message: " + successResponse.data.errorMessage);
                        status = "Push to core failed: Response null";
                        LogManager.ServiceNotification.LogDebug("Status: Lưu firebase.");
                        InsertPushNotification(requestBody, status, transactionId, -2, clientMessageId, userFullName);
                        LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                        return false;
                    }
                }
                else
                {
                    InsertPushNotification(requestBody, "Push to core failed: response.IsSuccessStatusCode false", "", -2, "", "");
                    LogManager.ServiceNotification.LogError($"ERROR:  {response}");
                    LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.ServiceNotification.LogError("ERROR: Push to core failed.");
                InsertPushNotification(requestBody, "Push to core failed:" + ex.ToString(), "", -2, "", "");
                LogManager.ServiceNotification.LogError("ERROR: An error occurred: " + Environment.NewLine + ex.ToString());
                LogManager.ServiceNotification.LogDebug("STATUS: <<<<<<<<<<<<<<<<<<<<<< Kết thúc gửi thông báo <<<<<<<<<<<<<<<<<<<<<<");
                return false;
            }
        }
        public void InsertPushNotification(RequestBodyDTO requestBodyDTO, string status, string transactionId, int statusCode, string clientMessageID, string userFullName)
        {
            try
            {
                foreach (var item in requestBodyDTO.listMessages)
                {
                    foreach (var receiver in item.listReceiver)
                    {
                        try
                        {
                            ntf_NotificationPushHistory notiPushHistory = new ntf_NotificationPushHistory();
                            NotificationDao _notificationDao = new NotificationDao();
                            notiPushHistory.UserFullName = userFullName ?? string.Empty;
                            notiPushHistory.Title = item.bodyTitle;
                            notiPushHistory.Content = item.bodyShortContent;
                            notiPushHistory.RefData = item.bodyContent;
                            notiPushHistory.IsRead = (int)NotificationStatus.Unread;

                            if (!string.IsNullOrEmpty(receiver?.receiverUserId))
                            {
                                notiPushHistory.EmployeeID = int.Parse(receiver.receiverUserId);
                            }
                            if (!string.IsNullOrEmpty(receiver?.receiverToken))
                            {
                                notiPushHistory.DeviceID = receiver.receiverToken;
                            }

                            notiPushHistory.Error = status;
                            notiPushHistory.Status = statusCode;
                            notiPushHistory.ClientMessageID = clientMessageID;
                            notiPushHistory.TransactionId = transactionId;

                            _notificationDao.InsertNewNotificationPushHistory(notiPushHistory);
                        }
                        catch (Exception ex)
                        {
                            string jsonData = JsonConvert.SerializeObject(receiver);
                            LogManager.ServiceNotification.LogError($"ERROR: Insert Push Notification: title: {item.bodyTitle}, Content: {item.bodyShortContent}, Receiver: {jsonData}", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.ServiceNotification.LogError($"ERROR: Insert Push Notification" + Environment.NewLine, ex);
            }
        }

        public enum NotificationStatus
        {
            Read = 1,
            Unread = 0,
            PushedToCore = 0,
            PushedToCoreSuccess = 1,
            PushedToCodeFailed = -2
        }

    }

}
