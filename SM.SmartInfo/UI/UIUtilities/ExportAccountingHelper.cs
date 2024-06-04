using SM.SmartInfo.BIZ.Commons;
using System.Collections.Generic;
using SM.SmartInfo.BIZ.CommonList;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.UI.UIUtilities
{
    public static class ExportWordHelper
    {
        /// <summary>
        /// Export word with template = docx
        /// </summary>
        /// <param name="mapFile">Xml mapping file (define query and map data)</param>
        /// <param name="dicParam">Query parameters in mapping file</param>
        /// <param name="lstTable">List customize table data. May be null</param>
        public static void ExportWord(string mapFile, Dictionary<string, object> dicParam)
        {
            string contentType = SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_DOCX;
            string displayName = string.Empty;

            var helper = new ExportFileHelperBiz();
            // get result content
            var biz = new ExportFilesBiz();
            byte[] fileContent = biz.GetExportWordByMap(mapFile, dicParam, null, out displayName);

            // push to download
            PushToDownload(fileContent, contentType, displayName);
        }

        /// <summary>
        /// Push file content to download for client
        /// </summary>
        /// <param name="fileContent">File to download</param>
        /// <param name="contentType">Content type</param>
        /// <param name="displayName">Display name</param>
        public static void PushToDownload(byte[] fileContent, string contentType, string displayName)
        {
            if (System.Web.HttpContext.Current.Response.IsClientConnected)
            {
                SoftMart.Core.Utilities.DownloadHelper.PushBinaryContent(
                    contentType,
                    fileContent, displayName);
            }
        }
    }
}