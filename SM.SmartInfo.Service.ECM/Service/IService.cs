
using SM.SmartInfo.Service.ECM.Entities;

namespace SM.SmartInfo.Service.ECM.Service
{
    interface IService
    {
        string Upload(DMSUploadInfo uploadInfo);
        DMSSearchResult Search(DMSSearchInfo searchInfo, string userName);
        DMSDocumentInfo Download(string ecmItemID, string fileName, string userName);
        void Delete(string ecmItemID, string userName);
    }
}
