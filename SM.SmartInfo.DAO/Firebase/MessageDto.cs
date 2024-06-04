using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO.Firebase
{
    public class Receiver
    {
        public string receiverAppId { get; set; }
        public string receiverType { get; set; }
        public string receiverToken { get; set; }
        public string receiverUserId { get; set; }
    }

    public class MessageNoti
    {
        public string messageId { get; set; }
        public string messageType { get; set; }
        public string bodyTitle { get; set; }
        public string bodyShortContent { get; set; }
        public string bodyContent { get; set; }
        public List<Receiver> listReceiver { get; set; }
    }

    public class RequestBodyDTO
    {
        public List<MessageNoti> listMessages { get; set; }
    }
    public class SuccessResponse
    {
        public int status { get; set; }
        public string error { get; set; }
        public string soaErrorCode { get; set; }
        public string soaErrorDesc { get; set; }
        public string clientMessageId { get; set; }
        public string path { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public int status { get; set; }
        public string errorMessage { get; set; }
    }
}
