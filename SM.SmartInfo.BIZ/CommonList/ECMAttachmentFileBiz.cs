using System;
using System.IO;
using System.Linq;
using System.Text;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using SoftMart.Core.Ranking.Biz;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.DAO.CommonList;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System.Drawing;
using SoftMart.Kernel.Entity;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.BIZ.CommonList
{
    public class ECMAttachmentFileBiz : BaseBiz
    {
        private AttachmentDao _dao = new AttachmentDao();

        #region Get/Search

        public void GetAttachmentByID(AttachmentParam param)
        {
            var att = _dao.GetAttachmentByID(param.AttachmentID.GetValueOrDefault(0));
            param.adm_Attachment = DownloadInECM(att);
        }

        public void GetListAttachment(AttachmentParam param)
        {
            int? refID = param.adm_Attachment.RefID;
            int? refType = param.adm_Attachment.RefType;

            param.adm_Attachments = _dao.GetAttachments(refID, refType);
        }

        public void GetListAttachmentByRefType(AttachmentParam param)
        {
            int? refType = param.adm_Attachment.RefType;

            param.adm_Attachments = _dao.GetAttachmentsByRefType(refType);
        }

        public adm_Attachment GetAttachmentByRefIDAndRefType(adm_Attachment item)
        {
            int? refID = item.RefID;
            int? refType = item.RefType;

            var att = _dao.GetAttachments(refID, refType).FirstOrDefault();

            return DownloadInECM(att);
        }

        public List<adm_Attachment> GetListAttachmentByRefIDAndRefType(int? itemRefID, int? itemRefType, PagingInfo paging = null)
        {
            int? refID = itemRefID;
            int? refType = itemRefType;

            var att = new List<adm_Attachment>();

            if (paging == null)
            {
                att = _dao.GetAttachmentsByDefault(refID, refType);
            }
            else
            {
                att = _dao.GetAttachmentsByDefault(refID, refType, paging);
            }

            if (att.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < att.Count; i++)
                {
                    try
                    {
                        att[i] = DownloadInECMContact(att[i]);
                    }
                    catch
                    {
                        continue;
                    }
                }
                return att;
            }
        }

        public adm_Attachment GetAttachmentByRefIDAndRefTypeContact(adm_Attachment item)
        {
            int? refID = item.RefID;
            int? refType = item.RefType;

            var att = _dao.GetAttachments(refID, refType).FirstOrDefault();

            return DownloadInECMContact(att);
        }

        public adm_Attachment GetAttachmentByRefIDAndRefTypeContactByAlertID(adm_Attachment item)
        {
            int? refID = item.RefID;
            int? refType = item.RefType;

            //var att = _dao.GetAttachmentIDByHrAlertID(refID, refType).FirstOrDefault();
            return DownloadInECMContact(null);
        }

        public adm_Attachment GetAttachmentByRefIDAndRefTypeContactByHR(adm_Attachment item)
        {
            int? refID = item.RefID;
            int? refType = item.RefType;
            var att = _dao.GetAttachmentIDByHrAlertID(refID, refType).FirstOrDefault();
            return DownloadInECMContact(att);
        }
        public List<adm_Attachment> GetAttachmentByRefIDAndRefImageContact(adm_Attachment item, int PageIndex, int? HistoryRefType = -1)
        {
            int? refID = item.RefID;
            int? refType = item.RefType;

            var lstAtt = _dao.GetAttachmentsImageContact(refID, refType, SMX.AttachmentRefType.PressAgencyHROtherImage, SMX.AttachmentRefType.PressAgencyHRHistory, PageIndex, HistoryRefType);
            foreach (var att in lstAtt)
            {
                DownloadInECMContact(att);
            }
            return lstAtt;
        }

        public adm_Attachment GetAttachmentByRefIDAndRefTypeForImageLibrary(adm_Attachment item)
        {
            var att = _dao.GetAttachmentByIDForImageLibrary(item.AttachmentID);

            return DownloadInECMForImageLibrary(att);
        }

        public List<adm_Attachment> GetListAttachmentByRefIDAndRefType(adm_Attachment item)
        {
            int? refID = item.RefID;
            int? refType = item.RefType;

            var lstAtt = _dao.GetAttachments(refID, refType);
            foreach (var att in lstAtt)
            {
                DownloadInECM(att);
            }
            return lstAtt;
        }

        public List<adm_Attachment> GetListAttachmentByRefIDAndRefTypeDefault(int? refID, int? refType)
        {
            int? RefID = refID;
            int? RefType = refType;

            var lstAtt = _dao.GetAttachmentsByDefault(RefID, RefType);

            foreach (var att in lstAtt)
            {
                DownloadInECM(att);
            }

            return lstAtt;
        }

        #endregion

        #region Replace File
        public void ReplaceExistingFile(AttachmentParam param, bool validateNeeded = true)
        {
            var item = param.adm_Attachment;
            // validate
            if (validateNeeded)
            {
                ValidateAttachment(item);
            }

            adm_Attachment itemDelete = _dao.GetItemByID<adm_Attachment>(item.AttachmentID.Value);

            DeleteInECM(itemDelete);

            _dao.DeleteAttachment(item.AttachmentID);

            //delete old cache
            _dao.DeleteCacheECM_ByAttachmentID(item.AttachmentID.Value);

            // audit
            item.CreatedBy = Profiles.MyProfile == null ? "System" : Profiles.MyProfile.UserName;
            item.CreatedDTG = DateTime.Now;
            item.AttachmentID = null;

            // upload to ECM
            UploadToECM(item);

            // keep in app (for query)
            _dao.InsertAttachment(item);

            param.AttachmentID = item?.AttachmentID;
        }
        #endregion

        #region Upload

        public void UploadFile(adm_Attachment item, bool validateNeeded = true)
        {
            // validate
            if (validateNeeded)
            {
                ValidateAttachment(item);
            }

            if (item.AttachmentID.HasValue)
            {
                // overwrite: xoa tai lieu cu tren ECM, upload tai lieu moi len ECM
                adm_Attachment itemDelete = _dao.GetItemByID<adm_Attachment>(item.AttachmentID.Value);
                DeleteInECM(itemDelete);

                //delete old cache
                _dao.DeleteCacheECM_ByAttachmentID(item.AttachmentID.Value);

                UploadToECM(item);

                // cap nhat lai thong tin EcmItemID
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
                _dao.UpdateItem(item);
                var check = _dao.GetCacheECM_ByAttachmentID(item.AttachmentID);
                if (check == null)
                {
                    adm_CacheECM itemCache = new adm_CacheECM()
                    {
                        AttachmentID = item.AttachmentID,
                        FileContent = item.FileContent,
                        CreatedBy = "",
                        CreatedDTG = DateTime.Now
                    };
                    using (DataContext dataContext = new DataContext())
                    {
                        dataContext.InsertItem<adm_CacheECM>(itemCache);
                    }
                }
            }
            else
            {
                // audit
                item.CreatedBy = Profiles.MyProfile == null ? "System" : Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                // upload to ECM
                UploadToECM(item);

                // keep in app (for query)
                _dao.InsertAttachment(item);
                var check = _dao.GetCacheECM_ByAttachmentID(item.AttachmentID);
                if (check == null)
                {
                    adm_CacheECM itemCache = new adm_CacheECM()
                    {
                        AttachmentID = item.AttachmentID,
                        FileContent = item.FileContent,
                        CreatedBy = "",
                        CreatedDTG = DateTime.Now
                    };
                    using (DataContext dataContext = new DataContext())
                    {
                        dataContext.InsertItem<adm_CacheECM>(itemCache);
                    }
                }
            }
        }

        public void UploadOtherFile(adm_Attachment item)
        {
            // validate
            ValidateAttachment(item);

            if (item.AttachmentID.HasValue)
            {
                // overwrite: xoa tai lieu cu tren ECM, upload tai lieu moi len ECM
                adm_Attachment itemDelete = _dao.GetItemByID<adm_Attachment>(item.AttachmentID.Value);
                DeleteInECM(itemDelete);

                //delete old cache
                _dao.DeleteCacheECM_ByAttachmentID(item.AttachmentID.Value);

                UploadToECM(item);

                // cap nhat lai thong tin EcmItemID
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
                _dao.UpdateItem(item);
            }
            else
            {
                // audit
                item.CreatedBy = Profiles.MyProfile == null ? "System" : Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                // upload to ECM
                UploadToECM(item);

                // keep in app (for query)
                _dao.InsertAttachment(item);
            }
        }

        public List<string> lstExtensionFile = new List<string>()
            {
                ".doc", ".docx", ".xls", ".xlsx", ".pdf",
                ".png", ".jpg", ".gif", ".jpeg", ".bmp",
                ".DOC", ".DOCX", ".XLS", ".XLSX", ".PDF",
                ".PNG", ".JPG", ".GIF", ".JPEG", ".BMP",
            };

        private bool IsValidExtensionFile(string fileName)
        {
            bool valid = false;
            if (string.IsNullOrWhiteSpace(fileName))
                return valid;
            //Get the file extension
            string extension = Path.GetExtension(fileName);
            valid = lstExtensionFile.Exists(c => c.Equals(extension));
            return valid;
        }

        private void ValidateAttachment(adm_Attachment item)
        {
            List<string> lstError = new List<string>();

            if (IsValidExtensionFile(item.FileName) == false)
                lstError.Add("File không đúng dạng chuẩn cho phép là (Word,Excel,PDF,Ảnh).");
            if (string.IsNullOrEmpty(Utils.MimeType.GetMimeType(item.FileContent, item.FileName)))
                lstError.Add("File không đúng dạng chuẩn cho phép là (Word,Excel,PDF,Ảnh).");
            if (!CheckValidFile(item.FileContent, item.FileName, item))
                lstError.Add("File không đúng dạng chuẩn cho phép là (Word,Excel,PDF,Ảnh).");
            if (string.IsNullOrEmpty(item.FileName))
                lstError.Add("Đường dẫn file không đúng.");
            if (item.FileSize > Utils.ConfigUtils.MaxUploadSize)
                lstError.Add("Dung lượng file lớn hơn mức giới hạn cho phép.");

            if (lstError.Count > 0)
                throw new SMXException(lstError);
        }

        private bool CheckValidFile(byte[] fileContent, string fileName, adm_Attachment att)
        {
            string extension = Path.GetExtension(fileName) == null ? string.Empty : Path.GetExtension(fileName).ToUpper();
            if (!string.IsNullOrEmpty(extension))
            {
                switch (extension)
                {
                    case ".DOCX":
                    case ".DOC":
                        {
                            try
                            {
                                MemoryStream ms = new MemoryStream(fileContent);
                                System.IO.Packaging.Package.Open(ms, FileMode.Open);
                            }
                            catch (Exception ex)
                            {
                                LogManager.WebLogger.LogDebug(string.Format("{0} DEBUG: ECMAttachmentFileBiz.CheckValidFile(byte[] fileContent, string fileName): fileName = {1}| Message: {2}", DateTime.Now, fileName, ex.Message));
                                return false;
                            }
                            break;
                        }
                    case ".XLSX":
                    case ".XLS":
                        {
                            try
                            {
                                MemoryStream ms = new MemoryStream(fileContent);
                                DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Open(ms, false);
                            }
                            catch (Exception ex)
                            {
                                LogManager.WebLogger.LogDebug(string.Format("{0} DEBUG: ECMAttachmentFileBiz.CheckValidFile(byte[] fileContent, string fileName): fileName = {1}| Message: {2}", DateTime.Now, fileName, ex.Message));
                                return false;
                            }
                            break;
                        }
                    case ".GIF":
                    case ".JPG":
                    case ".JPEG":
                    case ".PNG":
                    case ".BMP":
                        {
                            try
                            {
                                using (var ms = new MemoryStream(fileContent))
                                {
                                    using (var image = System.Drawing.Image.FromStream(ms))
                                    {
                                        Bitmap A = new Bitmap(image);
                                        Bitmap B = (Bitmap)A.Clone();
                                        using (MemoryStream mStream = new MemoryStream())
                                        {
                                            B.Save(mStream, System.Drawing.Imaging.ImageFormat.Bmp);
                                            att.FileContent = mStream.ToArray();
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.WebLogger.LogDebug(string.Format("{0} DEBUG: ECMAttachmentFileBiz.CheckValidFile(byte[] fileContent, string fileName): fileName = {1}| Message: {2}", DateTime.Now, fileName, ex.Message));
                                return false;
                            }
                            break;
                        }
                    case ".PDF":
                        {

                            try
                            {
                                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(fileContent);
                            }
                            catch (Exception ex)
                            {
                                LogManager.WebLogger.LogDebug(string.Format("{0} DEBUG: ECMAttachmentFileBiz.CheckValidFile(byte[] fileContent, string fileName): fileName = {1}| Message: {2}", DateTime.Now, fileName, ex.Message));
                                return false;
                            }
                            break;
                        }
                }
            }
            return true;
        }

        public void UploadToECM(adm_Attachment att)
        {
            try
            {
                //UpdateMetadataInformation(att);

                Service.ECM.Entities.DMSUploadInfo upInfo = new Service.ECM.Entities.DMSUploadInfo();

                // thong tin upload
                upInfo.Category = att.RefCode;
                upInfo.Description = att.Description;
                upInfo.FileContent = Convert.ToBase64String(att.FileContent);
                upInfo.FileName = att.FileName;
                upInfo.FileType = att.ContentType;
                upInfo.Name = att.DisplayName;
                upInfo.UploadBy = Profiles.MyProfile == null ? "System" : Profiles.MyProfile.UserName;

                // metadata
                string docType = att.RefCode;
                if (string.IsNullOrWhiteSpace(docType))
                    docType = "Undefine";
                upInfo.AddMetadata(Service.ECM.Service.ECMMetadataKey.DocumentType, docType);


                // upload
                string ticket = string.Format("RefID = {0}, RefType = {1}, Ticket = {2}", att.RefID, att.RefType, Guid.NewGuid().ToString());
                LogManager.ServiceECM.LogDebug(string.Format("Bat dau upload {0}", ticket));
                att.ECMItemID = Service.ECM.Service.ServiceWrapper.Upload(upInfo);
                LogManager.ServiceECM.LogDebug(string.Format("Ket thuc upload {0}, ECMItemID = {1}", ticket, att.ECMItemID));
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogError("UploadToECM failed.", ex);
                throw new SMXException("Không kết nối được đến ECM để tải tài liệu lên. Hãy liên hệ admin để được trợ giúp.");
            }
        }

        #endregion

        #region Delete

        public void DeleteDocument(adm_Attachment att)
        {
            att = _dao.GetAttachmentByID(att.AttachmentID.Value);

            _dao.DeleteAttachment(att.AttachmentID.Value);

            DeleteInECM(att);
        }

        public void DeleteDocumentByRefIDAndRefType(int? refID, int? refType)
        {
            var listAtt = _dao.GetListAttachmentByRefIDAndRefType(refID, refType);

            foreach (var att in listAtt)
            {
                _dao.DeleteAttachment(att.AttachmentID.Value);

                DeleteInECM(att);
            }
        }

        public void DeleteInECM(adm_Attachment att)
        {
            try
            {
                Service.ECM.Service.ServiceWrapper.Delete(att.ECMItemID, Profiles.MyProfile.UserName);
                _dao.DeleteCacheECM_ByAttachmentID(att.AttachmentID);
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogError(string.Format("DeleteInECM[{0}] failed.", att.ECMItemID), ex);
            }
        }

        #endregion

        public void Download(adm_Attachment att)
        {
            att = _dao.GetItemByID<adm_Attachment>(att.AttachmentID.Value);
            adm_Attachment item = DownloadInECM(att);

            if (item.FileContent != null)
            {
                Commons.ExportFilesBiz bizExport = new Commons.ExportFilesBiz();
                bizExport.PushToDownload(item.FileContent, SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_BINARY, item.FileName);
            }
            else
            {
                throw new SMXException("Không tìm thấy tài liệu trên ECM.");
            }
        }

        public void ViewDocument(adm_Attachment att)
        {
            att = _dao.GetItemByID<adm_Attachment>(att.AttachmentID.Value);
            adm_Attachment item = DownloadInECM(att);
            if (item.FileContent != null)
            {
                string contentType = item.ContentType;
                byte[] fileContent = item.FileContent;

                var response = System.Web.HttpContext.Current.Response;
                response.ClearHeaders();
                response.HeaderEncoding = Encoding.UTF8;
                response.Clear();
                response.Buffer = true;
                response.ContentType = contentType;
                response.AddHeader("content-disposition", "inline; filename=\"" + item.FileName.Replace(" ", "_") + "\"");
                response.AddHeader("Content-Length", fileContent.Length.ToString());

                response.BinaryWrite(fileContent);
                response.Flush();
            }
            else
            {
                throw new SMXException("Không tìm thấy tài liệu trên ECM.");
            }
        }

        private adm_Attachment DownloadInECM(adm_Attachment att)
        {
            try
            {
                if (att == null) return null;
                adm_CacheECM cacheECM = null;

                // lay tu ECM hay khong
                string getMode = ConfigUtils.GetConfig("GetImageMode");
                bool isGetFromCache = !"ECM".Equals(getMode); // => CachedMode (default)
                if (isGetFromCache)
                {
                    try
                    {
                        cacheECM = _dao.GetCacheECMByAttachmentID(att.AttachmentID);
                    }
                    catch { cacheECM = null; }
                }

                if (cacheECM != null)
                {
                    att.FileContent = cacheECM.FileContent;
                }
                else
                {
                    Service.ECM.Entities.DMSDocumentInfo doc = Service.ECM.Service.ServiceWrapper.Download(att.ECMItemID, att.FileName, Profiles.MyProfile.Employee.Username);

                    if (doc == null)
                        return null;

                    if (!string.IsNullOrWhiteSpace(doc.FileType))
                    {
                        att.ContentType = doc.FileType;
                    }
                    if (!string.IsNullOrWhiteSpace(doc.FileName))
                    {
                        att.FileName = doc.FileName;
                    }

                    att.FileContent = Convert.FromBase64String(doc.FileContent);
                    att.DisplayName = doc.FileName;

                    if (isGetFromCache)
                        InsertCacheECM(att.AttachmentID.Value, att.FileContent);
                }

                return att;
            }
            catch (SMXException ex)
            {
                LogManager.WebLogger.LogError("DownloadInECM failed.", ex);
                throw new SMXException("Không tải được tài liệu từ ECM. Hãy liên hệ admin để được trợ giúp.");
            }
        }


        private adm_Attachment DownloadInECMContact(adm_Attachment att)
        {
            try
            {
                if (att == null) return null;
                adm_CacheECM cacheECM = null;

                // lay tu ECM hay khong
                string getMode = ConfigUtils.GetConfig("GetImageMode");
                bool isGetFromCache = !"ECM".Equals(getMode); // => CachedMode (default)
                if (isGetFromCache)
                {
                    try
                    {
                        cacheECM = _dao.GetCacheECMByAttachmentID(att.AttachmentID);
                    }
                    catch { cacheECM = null; }
                }

                //if (cacheECM != null)
                //{
                //    att.FileContent = cacheECM.FileContent;
                //}
                else
                {
                    Service.ECM.Entities.DMSDocumentInfo doc = Service.ECM.Service.ServiceWrapper.Download(att.ECMItemID, att.FileName, Profiles.MyProfile.Employee.Username);

                    if (doc == null)
                        return null;

                    if (!string.IsNullOrWhiteSpace(doc.FileType))
                    {
                        att.ContentType = doc.FileType;
                    }
                    if (!string.IsNullOrWhiteSpace(doc.FileName))
                    {
                        att.FileName = doc.FileName;
                    }

                    //  att.FileContent = Convert.FromBase64String(doc.FileContent);
                    att.DisplayName = doc.FileName;

                    if (isGetFromCache)
                        InsertCacheECM(att.AttachmentID.Value, att.FileContent);
                }

                return att;
            }
            catch (SMXException ex)
            {
                LogManager.WebLogger.LogError("DownloadInECM failed.", ex);
                throw new SMXException("Không tải được tài liệu từ ECM. Hãy liên hệ admin để được trợ giúp.");
            }
        }

        private adm_Attachment DownloadInECMForImageLibrary(adm_Attachment att)
        {
            try
            {
                if (att == null) return null;
                adm_CacheECM cacheECM = null;

                // lay tu ECM hay khong
                string getMode = ConfigUtils.GetConfig("GetImageMode");
                bool isGetFromCache = !"ECM".Equals(getMode); // => CachedMode (default)
                if (isGetFromCache)
                {
                    try
                    {
                        cacheECM = _dao.GetCacheECMByAttachmentID(att.AttachmentID);
                    }
                    catch { cacheECM = null; }
                }

                if (cacheECM != null)
                {
                    att.FileContent = cacheECM.FileContent;
                }
                else
                {
                    Service.ECM.Entities.DMSDocumentInfo doc = Service.ECM.Service.ServiceWrapper.Download(att.ECMItemID, att.FileName, Profiles.MyProfile.Employee.Username);

                    if (doc == null)
                        return att;

                    if (!string.IsNullOrWhiteSpace(doc.FileType))
                    {
                        att.ContentType = doc.FileType;
                    }
                    if (!string.IsNullOrWhiteSpace(doc.FileName))
                    {
                        att.FileName = doc.FileName;
                    }

                    att.FileContent = Convert.FromBase64String(doc.FileContent);
                    att.DisplayName = doc.FileName;

                    if (isGetFromCache)
                        InsertCacheECM(att.AttachmentID.Value, att.FileContent);
                }

                return att;
            }
            catch (SMXException ex)
            {
                LogManager.WebLogger.LogError("DownloadInECM failed.", ex);
                throw new SMXException("Không tải được tài liệu từ ECM. Hãy liên hệ admin để được trợ giúp.");
            }
        }

        private void InsertCacheECM(int attID, byte[] fileContent)
        {
            adm_CacheECM cacheECM = new adm_CacheECM();
            cacheECM.AttachmentID = attID;
            cacheECM.CreatedBy = Profiles.MyProfile.Employee.Username;
            cacheECM.CreatedDTG = DateTime.Now;
            cacheECM.FileContent = fileContent;
            _dao.InsertCacheECM(cacheECM);
        }

        public void GetByteArrayForMobile(AttachmentParam param)
        {
            try
            {
                adm_Attachment att = param.adm_Attachment;

                adm_CacheECM cacheECM = null;

                // lay tu ECM hay khong
                string getMode = ConfigUtils.GetConfig("GetImageMode");
                bool isGetFromCache = !"ECM".Equals(getMode); // => CachedMode (default)
                if (isGetFromCache)
                {
                    try
                    {
                        cacheECM = _dao.GetCacheECMByAttachmentID(att.AttachmentID);
                    }
                    catch { cacheECM = null; }
                }

                if (cacheECM != null)
                {
                    att.FileContent = cacheECM.FileContent;
                }
                else
                {
                    LogManager.MobileLogger.LogError("File Image - cacheECM NOT FOUND");
                    Service.ECM.Entities.DMSDocumentInfo doc = Service.ECM.Service.ServiceWrapper.Download(
                            att.ECMItemID, att.FileName, Profiles.MyProfile.Employee.Username);
                    if (doc == null)
                        return;

                    if (!string.IsNullOrWhiteSpace(doc.FileType))
                    {
                        att.ContentType = doc.FileType;
                    }
                    if (!string.IsNullOrWhiteSpace(doc.FileName))
                    {
                        att.FileName = doc.FileName;
                    }

                    att.FileContent = Convert.FromBase64String(doc.FileContent);
                    att.DisplayName = doc.FileName;

                    if (isGetFromCache)
                        InsertCacheECM(att.AttachmentID.Value, att.FileContent);
                }

                param.adm_Attachment = att;
            }
            catch (Exception ex)
            {
                throw new SMXException("Không tải được tài liệu từ ECM. Hãy liên hệ admin để được trợ giúp.");
            }
        }
    }
}