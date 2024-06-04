using System;
using SM.SmartInfo.Service.ECM.Entities;

namespace SM.SmartInfo.Service.ECM.Service
{
    class SMService : IService
    {
        private string ServiceURL { get { return Utils.ConfigUtils.GetConfig("DMSService"); } }
        private string AccessKey { get { return Utils.ConfigUtils.GetConfig("DMSAccessKey"); } }

        public string Upload(DMSUploadInfo uploadInfo)
        {
            string xmlUploadInfo = SoftMart.Core.Utilities.SerializeHelper.SerializeToString(uploadInfo);
            var xmlResult = SOAPHelper.Execute<string>(ServiceURL, "UploadDocument", new object[] { AccessKey, xmlUploadInfo });
            var result = SoftMart.Core.Utilities.SerializeHelper.Deserialize<DMSUploadResult>(xmlResult);

            if (result.ErrorMessage != null)
                throw new Exception(result.ErrorMessage);

            return result.ListDocument[0].ID;
        }

        public DMSSearchResult Search(DMSSearchInfo searchInfo, string userName)
        {
            string xmlSearchInfo = SoftMart.Core.Utilities.SerializeHelper.SerializeToString(searchInfo);
            var xmlDataResult = SOAPHelper.Execute<string>(ServiceURL, "SearchDocument", new object[] { AccessKey, xmlSearchInfo, userName });
            DMSSearchResult result = SoftMart.Core.Utilities.SerializeHelper.Deserialize<DMSSearchResult>(xmlDataResult);
            if (result.ErrorMessage != null)
                throw new Exception(result.ErrorMessage);

            return result;
        }

        public DMSDocumentInfo Download(string ecmItemID, string fileName, string userName)
        {
            string xmlDataResult = SOAPHelper.Execute<string>(ServiceURL, "DownloadDocument", new object[] { AccessKey, ecmItemID, userName });
            DMSDownloadResult result = SoftMart.Core.Utilities.SerializeHelper.Deserialize<DMSDownloadResult>(xmlDataResult);
            if (result.ErrorMessage != null)
                throw new Exception(result.ErrorMessage);

            return result.Document;
        }

        public void Delete(string ecmItemID, string userName)
        {
        }
    }
}
