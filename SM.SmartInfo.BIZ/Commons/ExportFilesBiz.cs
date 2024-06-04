using System;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.OfficeUtilities.OpenXml;

namespace SM.SmartInfo.BIZ.Commons
{
    public class ExportFilesBiz
    {
        #region Export Word

        public byte[] GetExportWordByMap(string mapFile, Dictionary<string, object> dicParam, WordTransform transform, out string displayName)
        {
            if (string.IsNullOrWhiteSpace(mapFile))
                throw new SMXException("Thiếu file cấu hình dữ liệu");

            string mapFilePath = System.IO.Path.Combine(Utils.ConfigUtils.DynamicReportFolder, mapFile);
            transform = SoftMart.Core.Utilities.SerializeHelper.DeserializeXmlFile<WordTransform>(mapFilePath);
            displayName = transform.TemplateName;
            if (transform == null)
                throw new SMXException("File cấu hình dữ liệu không đúng");

            // get parameter
            if (dicParam == null)
                dicParam = new Dictionary<string, object>();

            var ExportFile = new CommonList.ExportFileHelperBiz();

            if (transform.ListRange != null)
            {
                foreach (WordTransformRange range in transform.ListRange)
                {
                    range.DataSourceTable = ExportFile.GetQueryData(range.QueryData, dicParam);
                }
            }
            if (transform.ListTable != null)
            {
                foreach (WordTransformTable table in transform.ListTable)
                {
                    if (table.QueryData == null)
                    {
                        continue;
                    }

                    table.DataSourceTable = ExportFile.GetQueryData(table.QueryData, dicParam);
                }
            }

            // tao file ket qua
            string tempFileName = System.IO.Path.GetFileName(transform.TemplateName);
            string tempFilePath = System.IO.Path.Combine(Utils.ConfigUtils.TemplateFolder, tempFileName);
            string destFilePath = Utils.ConfigUtils.GenTemporaryFilePath(Guid.NewGuid().ToString() + "_" + tempFileName);

            // thuc hien transform
            Utils.WordTransformHelper wordHelper = new Utils.WordTransformHelper();
            wordHelper.Transform(transform, tempFilePath, destFilePath);

            // xuat ket qua
            Utils.FileUtil.WaitingReadyFile(destFilePath);
            byte[] result = System.IO.File.ReadAllBytes(destFilePath);
            if (transform.ExportType == WordTransformExportType.Pdf)
            {
                result = WordHelper.Docx2Pdf(result);
            }

            // delete temporary file
            Utils.FileUtil.TryDelete(destFilePath);

            return result;
        }

        #endregion

        #region Push to download
        /// <summary>
        /// Push word file content to download for client
        /// </summary>
        /// <param name="fileContent">File to download</param>
        /// <param name="displayName">Display name</param>
        public void PushWordToDownload(byte[] fileContent, string displayName)
        {
            PushToDownload(fileContent, SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_DOCX, displayName);
        }

        /// <summary>
        /// Push pdf file content to download for client
        /// </summary>
        /// <param name="fileContent">File to download</param>
        /// <param name="displayName">Display name</param>
        public void PushPdfToDownload(byte[] fileContent, string displayName)
        {
            // PushToDownload(fileContent, SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_PDF, displayName);
            var response = System.Web.HttpContext.Current.Response;
            response.ClearHeaders();
            response.HeaderEncoding = System.Text.Encoding.UTF8;
            response.Clear();
            response.Buffer = true;
            response.ContentType = SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_PDF;
            response.AddHeader("content-disposition", "inline; filename=\"" + displayName.Replace(" ", "_") + "\"");
            response.AddHeader("Content-Length", fileContent.Length.ToString());

            response.BinaryWrite(fileContent);
            response.Flush();
        }

        /// <summary>
        /// Push file content to download for client
        /// </summary>
        /// <param name="fileContent">File to download</param>
        /// <param name="contentType">Content type</param>
        /// <param name="displayName">Display name</param>
        public void PushToDownload(byte[] fileContent, string contentType, string displayName)
        {
            if (System.Web.HttpContext.Current.Response.IsClientConnected)
            {
                SoftMart.Core.Utilities.DownloadHelper.PushBinaryContent(
                    contentType,
                    fileContent, displayName);
            }
        }
        #endregion

        public interface IExportFile
        {
            System.Data.DataTable GetQueryData(string query, Dictionary<string, object> dicQueryParam);

            byte[] GetExportWordByXslt(string mapFile, Dictionary<string, object> dicParam);
        }
    }
}