
using System.Linq;
using SM.SmartInfo.Service.ECM.Entities;

namespace SM.SmartInfo.Service.ECM.Service
{
    public class ServiceWrapper
    {
        private static IService ECMService
        {
            get
            {
                string serviceMode = Utils.ConfigUtils.GetConfig("DMSServiceMode");
                switch (serviceMode)
                {
                    case "SM":
                        return new SMService();
                    default:
                        return new NoneService();
                }
            }
        }

        public static string Upload(DMSUploadInfo upInfo)
        {
            return ECMService.Upload(upInfo);
        }

        public static DMSSearchResult Search(DMSSearchInfo searchInfo, string userName)
        {
            return ECMService.Search(searchInfo, userName);
        }

        public static DMSDocumentInfo Download(string ecmItemID, string fileName, string userName)
        {
            try
            {
                return ECMService.Download(ecmItemID, fileName, userName);
            }
            catch
            {
                return null;
            }
        }

        public static void Delete(string ecmItemID, string userName)
        {
            ECMService.Delete(ecmItemID, userName);
        }

        public static T CallWebService<T>(string serviceLink, string serviceMethod, object[] args) where T: class
        {
            return SOAPHelper.Execute<T>(serviceLink, serviceMethod, args);
        }

        public static string PostRequest(string serviceLink, string methodName, string soapRequest)
        {
            return SOAPHelper.Execute(serviceLink, methodName, soapRequest);
        }
    }
}
