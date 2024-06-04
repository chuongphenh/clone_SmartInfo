using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.IO;
using System.Web.Script.Serialization;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.Utils;
using SoftMart.Core.Ranking.Biz;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.DAO.CommonList;
using System.Drawing;
using SoftMart.Kernel.Entity;
using SoftMart.Core.Utilities.Profiles;
using SM.SmartInfo.Service.Reporting;

namespace SM.SmartInfo.UI.Administrations.Users
{
    /// <summary>
    /// Summary description for TagAJax
    /// </summary>
    public class UploadAjax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = context.Request.Files[0];
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(fileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                    string hidIdValue = context.Request["hidId"];
                    // Kiểm tra xem tệp tin có phải là hình ảnh có phần mở rộng phù hợp hay không
                    if (Array.IndexOf(allowedExtensions, fileExtension) != -1)
                    {

                        AttachmentDao _attDao = new AttachmentDao();
                        var itemCheck = _attDao.GetListAttachmentByRefIDAndRefType(int.Parse(hidIdValue), SMX.AttachmentRefType.PressAgencyHR);
                        adm_Attachment item = new adm_Attachment();
                        if (itemCheck.Any())
                        {
                            item.AttachmentID = itemCheck.FirstOrDefault().AttachmentID;
                        }

                        ///
                        item.FileName = fileName;
                        item.DisplayName = fileName;
                        item.FileSize = file.ContentLength;
                        item.ContentType = file.ContentType;
                        //item.Description = txtDescription.Text;
                        item.RefID = int.Parse(hidIdValue);
                        item.RefType = SMX.AttachmentRefType.PressAgencyHR;
                        item.FileContent = ReadFileContent(file.InputStream);

                        AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Upload);
                        param.adm_Attachment = item;
                        UploadFile(param.adm_Attachment, hidIdValue);
                        context.Response.Write(GetImageURL(param.adm_Attachment));
                    }
                    else
                    {
                        throw new Exception("Chỉ được phép tải lên các tệp tin có đuôi .jpg, .jpeg hoặc .png.");
                    }
                }
                else
                {
                    throw new Exception("Không có tệp tin nào được chọn.");
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500; // HTTP status code for server error
                context.Response.Write(ex.Message);
            }
        }
        private byte[] ReadFileContent(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        private string GetImageURL(adm_Attachment image)
        {
            string url = SMX.DefaultImage;

            if (image != null && image.FileContent != null)
            {
                string imageFileName = string.Format("{0}_{1}", image.AttachmentID, image.FileName);
                string imageFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository", "ECM");
                if (!System.IO.Directory.Exists(imageFilePath))
                    System.IO.Directory.CreateDirectory(imageFilePath);
                imageFilePath = System.IO.Path.Combine(imageFilePath, imageFileName);
                if (!System.IO.File.Exists(imageFilePath))
                    System.IO.File.WriteAllBytes(imageFilePath, image.FileContent);
                url = VirtualPathUtility.ToAbsolute("/Repository/ECM/" + imageFileName);
            }

            return VirtualPathUtility.ToAbsolute(url);
        }
        public void UploadFile(adm_Attachment item, string hidIdValue, bool validateNeeded = true)
        {
            AttachmentDao _dao = new AttachmentDao();
            // validate
            if (validateNeeded)
            {
                ValidateAttachment(item);
            }

            if (item.AttachmentID.HasValue)
            {
                // overwrite: xoa tai lieu cu tren ECM, upload tai lieu moi len ECM
                adm_Attachment itemDelete = _dao.GetItemByID<adm_Attachment>(item.AttachmentID.Value);

                try
                {
                    Service.ECM.Service.ServiceWrapper.Delete(itemDelete.ECMItemID, hidIdValue);
                    _dao.DeleteCacheECM_ByAttachmentID(itemDelete.AttachmentID);
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogError(string.Format("DeleteInECM[{0}] failed.", itemDelete.ECMItemID), ex);
                }

                //delete old cache
                _dao.DeleteCacheECM_ByAttachmentID(item.AttachmentID.Value);

                UploadToECM(item);

                // cap nhat lai thong tin EcmItemID
                item.UpdatedDTG = DateTime.Now;
                //_dao.UpdateItem(item);
                using (DataContext context = new DataContext())
                {
                    int eff = context.UpdateItem<adm_Attachment>(item);
                }
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
                //item.CreatedBy = Profiles.MyProfile == null ? "System" : Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                // upload to ECM
                UploadToECM(item);
                
                using (DataContext dataContext = new DataContext())
                {
                    dataContext.InsertItem<adm_Attachment>(item);
                }
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
                //upInfo.UploadBy = Profiles.MyProfile == null ? "System" : Profiles.MyProfile.UserName;

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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}