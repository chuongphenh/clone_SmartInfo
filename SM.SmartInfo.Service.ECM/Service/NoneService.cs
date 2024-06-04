using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.Service.ECM.Entities;

namespace SM.SmartInfo.Service.ECM.Service
{
    class NoneService : IService
    {
        private string ECMFolder { get { return Utils.ConfigUtils.GetConfig("ECMFolder"); } }

        public string Upload(Entities.DMSUploadInfo uploadInfo)
        {
            string fileName = uploadInfo.FileName;
            byte[] fileContent = Convert.FromBase64String(uploadInfo.FileContent);

            fileName = Guid.NewGuid().ToString() + fileName;

            string filePath = System.IO.Path.Combine(ECMFolder, fileName);
            System.IO.File.WriteAllBytes(filePath, fileContent);

            return fileName;
        }

        public Entities.DMSSearchResult Search(Entities.DMSSearchInfo searchInfo, string userName)
        {
            DMSSearchResult searchResult = new DMSSearchResult();
            searchResult.ListDocument = new List<DMSDocumentInfo>();

            return searchResult;
        }

        public Entities.DMSDocumentInfo Download(string ecmItemID, string fileName, string userName)
        {
            DMSDocumentInfo result = new DMSDocumentInfo();
            result.Link = System.IO.Path.Combine(ECMFolder, ecmItemID);
            result.FileName = fileName;
            result.FileContent = Convert.ToBase64String(System.IO.File.ReadAllBytes(result.Link));

            return result;
        }

        public void Delete(string ecmItemID, string userName)
        {
            Utils.FileUtil.TryDelete(System.IO.Path.Combine(ECMFolder, ecmItemID));
        }
    }
}
