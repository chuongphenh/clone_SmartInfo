namespace SM.SmartInfo.UI.UIUtilities
{
    public static class ExportHelper
    {
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