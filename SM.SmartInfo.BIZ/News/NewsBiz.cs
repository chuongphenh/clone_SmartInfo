using SM.SmartInfo.BIZ.CommonList;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.News;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Common;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using static SM.SmartInfo.SharedComponent.Constants.SMX.DataTracking;

namespace SM.SmartInfo.BIZ.News
{
    class NewsBiz : BizBase
    {
        private NewsDao _dao = new NewsDao();

        public void SearchNews(CommonParam param)
        {
            param.ListTinTuc = _dao.SearchNews(param.SearchText, param.PagingInfo);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var item in param.ListTinTuc)
            {
                try
                {
                    item.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = item.NewsID,
                        RefType = SMX.AttachmentRefType.News
                    });
                }
                catch
                {
                    item.Attachment = null;
                }
            }
        }

        public void SearchNegativeNews(CommonParam param)
        {
            param.ListSuVu = _dao.SearchNegativeNews(param.SearchText, param.PagingInfo);
        }

        public List<SharedComponent.Entities.News> Get5TinTuc()
        {
            var lstNews = _dao.Get5TinTichCucMoiNhat();

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var item in lstNews)
            {
                try
                {
                    item.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = item.NewsID,
                        RefType = SMX.AttachmentRefType.News
                    });
                }
                catch
                {
                    item.Attachment = null;
                }
            }

            return lstNews;
        }

        public List<SharedComponent.Entities.News> Get4TinSuVu()
        {
            return _dao.Get4TinTieuCucMoiNhat();
        }

        #region News
        public void GetListNews(NewsParam param)
        {
            param.ListNews = _dao.GetListNews();
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var item in param.ListNews)
            {
                try
                {
                    item.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = item.NewsID,
                        RefType = SMX.AttachmentRefType.News
                    });
                    item.SingleNews = _dao.GetSingleNewsByNewsId(item.NewsID);
                }
                catch
                {
                    item.Attachment = null;
                    param.SingleNews = null;
                }
            }
        }

        public void SearchNewsForView(NewsParam param)
        {
            var filterNews = param.News;
            var quickFilterNews = param.QuickFilterNews;
            var filterPositiveNews = param.PositiveNews;

            var lstNews = _dao.SearchNewsForView(filterNews, quickFilterNews);

            var lstPositiveNews = _dao.SearchDetailPositiveNews(filterPositiveNews);

            var lstNewsSearchByPositiveNews = new List<SharedComponent.Entities.News>();

            lstNewsSearchByPositiveNews.AddRange(_dao.GetItemByListID(lstPositiveNews.Select(x => x.NewsID.GetValueOrDefault(0)).ToList()));

            param.ListNews = lstNews.Where(x => lstNewsSearchByPositiveNews.Exists(c => c.NewsID == x.NewsID)).OrderByDescending(x => x.CreatedDTG).ThenByDescending(x => x.NewsID).ToList();

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var item in param.ListNews)
            {
                try
                {
                    item.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = item.NewsID,
                        RefType = SMX.AttachmentRefType.News
                    });
                }
                catch
                {
                    item.Attachment = null;
                }
            }
        }

        public void IsNameExists(NewsParam param)
        {
            LogManager.WebLogger.LogDebug("Check Hastag");

              param.IsNameExists = _dao.IsNameExists(param.Hastag);
            LogManager.WebLogger.LogDebug("Check Hastag Done");
        }
        public void IsSingleCamp(NewsParam param)
        {
            LogManager.WebLogger.LogDebug("Check IsSingleCamp");

            param.IsSingleCamp = _dao.IsSingleCamp(param.NewsID);
            LogManager.WebLogger.LogDebug("Check IsSingleCamp Done");
        }
        public void SetIsSingleCamp(NewsParam param)
        {
            LogManager.WebLogger.LogDebug("Check SetIsSingleCamp");

            bool IsSingleCamp = _dao.SetIsSingleCamp(param.NewsID, param.IsSingleCamp);
            LogManager.WebLogger.LogDebug(IsSingleCamp.ToString());
            LogManager.WebLogger.LogDebug("Check SetIsSingleCamp Done");
        }
        public void CreateHastag(NewsParam param)
        {
            LogManager.Pentest.LogDebug("bat dau biz CreateHastag");
            //var item = param.HastagManagement;
            HastagManagement item = new HastagManagement();
            LogManager.Pentest.LogDebug("InsertItem");
            item.CreatedBy = Profiles.MyProfile.UserName;
            item.CreatedDTG = DateTime.Now;
            item.Name = param.Hastag;

            _dao.InsertHastag(item);
            LogManager.Pentest.LogDebug("InsertItem Done");
        }

        public void AddNewData(NewsParam param)
        {
            LogManager.Pentest.LogDebug("bat dau biz AddNewData");
            var item = param.News;
            item.Type = SMX.News.Type.News;
            if (param.IsSaveComplete)
                ValidateNews(item);
            //SetSystemInfo(item);
            LogManager.Pentest.LogDebug("InsertItem");
            _dao.InsertItem(item);
            LogManager.Pentest.LogDebug("InsertItem Done");
        }
        
        public void AddNewCampaignData(NewsParam param)
        {
            LogManager.Pentest.LogDebug("bat dau biz AddNewData");
            var item = param.CampaignNews;
            if(item.CampaignNewsID == null)
            {
                LogManager.Pentest.LogDebug("InsertItem");
                _dao.InsertItem(item);
                LogManager.Pentest.LogDebug("InsertItem Done");
                return;
            }
            LogManager.Pentest.LogDebug("UpdateItem");
            _dao.UpdateItem(item);
            LogManager.Pentest.LogDebug("UpdateItem Done");
        }

        public void AddDocumentCampaignNews(NewsParam param)
        {
            try
            {
                ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
                foreach (var item in param.ListAttachment)
                {
                    item.RefType = SMX.AttachmentRefType.CampaignNews;
                    item.RefID = param.CampaignNewsID;
                    bizECM.UploadFile(item);
                }
            }
            catch(Exception ex)
            {
                throw new SMXException(ex.Message);
            }
        }
        
        public void GetListHastag(NewsParam param)
        {
            try
            {
                param.ListHastag =_dao.GetListHastag(param);
            }
            catch(Exception ex)
            {
                throw new SMXException(ex.Message);
            }
        }

        public void UpdateData(NewsParam param)
        {
            var item = param.News;

            if (param.IsSaveComplete)
                ValidateNews(item);

            SetSystemInfo(item);

            _dao.UpdateItem(item);
        }

        public void LoadDataDisplay(NewsParam param)
        {
            int? newsID = param.NewsID;
            param.News = _dao.GetNews_ByID(newsID);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.NewsID, SMX.AttachmentRefType.News);
            /*param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = newsID,
                RefType = SMX.AttachmentRefType.News
            });*/
        }

        public void LoadDataImagesNews(NewsParam param)
        {
            int? newsID = param.NewsID;
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.NewsID, SMX.AttachmentRefType.News);
            /*param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = newsID,
                RefType = SMX.AttachmentRefType.News
            });*/
        }

        public void LoadDataImagesSingleNews(NewsParam param)
        {
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.SingleNewsId, SMX.AttachmentRefType.SingleNews);
        }
        
        public void LoadDataDocumentCampaignNews(NewsParam param)
        {
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.CampaignNewsID, SMX.AttachmentRefType.CampaignNews);
        }

        public void getCountUploadedDocument(NewsParam param)
        {
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.attCount = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.attRefID, param.attRefType).Count;
            //param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.attRefID, param.attRefType);
        }
        public void getAtt(NewsParam param)
        {
            //ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            //param.Attachment = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.attRefID, param.attRefType).First();                
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            var attachments = bizECM.GetListAttachmentByRefIDAndRefTypeDefault(param.attRefID, param.attRefType);

            if (attachments.Any())
            {
                param.Attachment = attachments.First();
            }
            else
            {
                param.Attachment = null;
                // Xử lý trường hợp khi chuỗi attachments không chứa phần tử nào.
                // Bạn có thể đặt param.Attachment thành giá trị mặc định hoặc thực hiện các hành động khác dựa trên trường hợp này.
            }
        }
        public void DeleteNewsAndPositiveNewsAndCampaignNews(NewsParam param)
        {
            int? newsID = param.NewsID;
            var news = new SharedComponent.Entities.News() { NewsID = newsID, Deleted = SMX.smx_IsDeleted };
            _dao.UpdateItem(news);
        }

        public void BuildTreeListNews(NewsParam param)
        {
            var lstNews = _dao.BuildTreeListNews();

            lstNews.ForEach(x => x.CategoryName = Utility.GetDictionaryValue(SMX.NewsCategory.dicDesc, x.Category));

            param.ListNews = lstNews;
        }
        
        public void GetListSingleNewsByNewsID(NewsParam param)
        {
            try
            {
                param.ListSingleNews = _dao.GetListSingleNewsByNewsID(param.NewsID, param.PagingInfo);
            }
            catch(Exception ex)
            {
                return;
            }
        }
        
        public void GetListSingleNewsByNewsIDAndCampaignID(NewsParam param)
        {
            try
            {
                param.ListSingleNews = _dao.GetListSingleNewsByNewsIDAndCampaignID(param.NewsID, param.CampaignNewsID, param.PagingInfo);
            }
            catch(Exception ex)
            {
                return;
            }
        }
        
        public void SaveSingleNews(NewsParam param)
        {
            try
            {
                var item = param.SingleNews;

                ValidateSingleNews(item);

                ECMAttachmentFileBiz attBiz = new ECMAttachmentFileBiz();

                if (item.Id.HasValue && item.Id != 0)
                {
                    item.UpdatedBy = Profiles.MyProfile.UserName;
                    item.UpdatedDTG = DateTime.Now;

                    _dao.UpdateSingleNews(item);

                    foreach (var att in param.ListAttachment)
                    {
                        if (att.FileName != null && item.Id.HasValue)
                        {
                            try
                            {
                                att.RefID = item.Id;
                                att.RefType = SMX.AttachmentRefType.SingleNews;
                                attBiz.UploadFile(att);
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    item.Deleted = SMX.smx_IsNotDeleted;
                    item.CreatedBy = Profiles.MyProfile.UserName;
                    item.CreatedDTG = DateTime.Now;

                    _dao.InsertSingleNews(item);

                    var refID = _dao.GetLatestSingleNewsIdByCreator(item.NewsId);

                    foreach (var att in param.ListAttachment)
                    {
                        if (att.FileName != null)
                        {
                            try
                            {
                                att.RefID = refID;
                                att.RefType = SMX.AttachmentRefType.SingleNews;
                                attBiz.UploadFile(att);
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new SMXException(ex.Message);
            }
        }

        public void DeleteSingleNews(NewsParam param)
        {
            try
            {
                ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
                _dao.DeleteSingleNews(param.SingleNewsId);
                bizECM.DeleteDocumentByRefIDAndRefType(param.SingleNewsId, SMX.AttachmentRefType.SingleNews);
            }
            catch(Exception ex)
            {
                throw new SMXException(ex.Message);
            }
        }

        public void ValidateNews(SharedComponent.Entities.News item)
        {
            List<string> lstErr = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Name))
                lstErr.Add("[Tên tin] chưa được nhập");

            if (item.PostingFromDTG == null)
                lstErr.Add("[Ngày đăng tin từ] chưa được nhập");

            if (item.PostingToDTG == null)
                lstErr.Add("[Ngày đăng tin đến] chưa được nhập");

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        #endregion

        #region PositiveNews

        public void SavePositiveNews(NewsParam param)
        {
            var item = param.PositiveNews;
            if (item.NewsID == null) return;

            List<string> lstErr = new List<string>();
            if (string.IsNullOrWhiteSpace(item.Title))
                AddRequireErrorMessage("Tiêu đề", lstErr);
            if (lstErr.Count > 0)
                throw new SMXException(lstErr);

            using (TransactionScope tran = new TransactionScope())
            {
                if (item.PositiveNewsID.HasValue && item.PositiveNewsID != 0)
                    _dao.UpdateItem(item);
                else
                    _dao.InsertItem(item);
                SyncCampaignNewsNumberOfNew(item.NewsID.Value);
                tran.Complete();
            }
        }

        private void SyncCampaignNewsNumberOfNew(int newID)
        {
            List<SharedComponent.Entities.CampaignNews> lstCam = _dao.GetListCampaignNews_ByNewsID(newID);
            List<SharedComponent.Entities.PositiveNews> lstNews = _dao.GetListPositiveNews_ByID(newID);
            foreach (var cam in lstCam)
            {
                cam.NumberOfNews = lstNews.Count(c => c.CampaignID == cam.CampaignNewsID);
                _dao.UpdateItem(cam);
            }
        }

        public void PrepareDataCampaign(NewsParam param)
        {
            int? newsID = param.NewsID;
            param.ListCampaignNews = _dao.GetListCampaignNews_ByNewsID(newsID);
        }

        public void GetListPositiveNews_ByNewsID(NewsParam param)
        {
            int? newsID = param.NewsID;
            param.ListPositiveNews = _dao.GetListPositiveNews_ByNewsID(newsID, param.PagingInfo);
        }

        public void DeleteNegativeNewsResearched(NewsParam param)
        {
            int? itemID = param.PositiveNewsID;
            if (itemID == null) return;
            CampaignNews cam = null;
            if (param.PositiveNews.CampaignID.HasValue)
            {
                cam = _dao.GetItemByID<CampaignNews>(param.PositiveNews.CampaignID);
                if (cam.NumberOfNews.HasValue && cam.NumberOfNews != 0)
                    cam.NumberOfNews = cam.NumberOfNews - 1;
            }
            using (TransactionScope tran = new TransactionScope())
            {
                _dao.DeletePositiveNewsByPositiveNewsID(itemID);
                if (cam != null)
                    _dao.UpdateItem(cam);
                tran.Complete();
            }
        }

        public void LoadDataImagesPositiveNews(NewsParam param)
        {
            int? positiveNewsID = param.PositiveNewsID;

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = positiveNewsID,
                RefType = SMX.AttachmentRefType.PositiveNews
            });
        }

        #endregion 

        #region CampaignNews
        public void SaveCampaignNews(NewsParam param)
        {
            var item = param.CampaignNews;

            ValidateCampaignNews(item);

            if (item.CampaignNewsID.HasValue && item.CampaignNewsID != 0)
            {
                ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;

                _dao.UpdateItem<CampaignNews>(item);

                foreach(var doc in param.ListAttachment)
                {
                    doc.RefID = item.CampaignNewsID;
                    doc.RefType = SMX.AttachmentRefType.CampaignNews;
                    bizECM.UploadFile(doc);
                }
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                _dao.InsertItem(item);
            }
        }

        public void GetListCampaignNews_ByNewsID(NewsParam param)
        {
            int? newsID = param.NewsID;
            param.ListCampaignNews = _dao.GetListCampaignNews_ByNewsID(newsID, param.PagingInfo);
        }

        public void DeleteCampaignNews(NewsParam param)
        {
            int? itemID = param.CampaignNewsID;

            int? newsID = param.NewsID;

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();

            _dao.DeleteCampaignNewsByCampaignNewsID(itemID);

            bizECM.DeleteDocumentByRefIDAndRefType(itemID, SMX.AttachmentRefType.CampaignNews);

            var listSingleNews = _dao.GetListSingleNewsByNewsIDAndCampaignIDNoPaging(newsID, itemID);

            foreach(var item in  listSingleNews)
            {
                bizECM.DeleteDocumentByRefIDAndRefType(item.Id, SMX.AttachmentRefType.SingleNews);
                _dao.DeleteSingleNews(item.Id);
            }
        }

        private void ValidateCampaignNews(CampaignNews item)
        {
            List<string> lstErr = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Campaign))
                AddRequireErrorMessage("Tuyến bài", lstErr);

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }
        
        private void ValidateSingleNews(SingleNews item)
        {
            List<string> lstErr = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Title))
                AddRequireErrorMessage("Tiêu đề", lstErr);

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }


        #endregion
    }
}