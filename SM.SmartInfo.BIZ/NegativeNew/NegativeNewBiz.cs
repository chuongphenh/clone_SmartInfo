using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.DAO.NegativeNew;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Linq;
using SM.SmartInfo.CacheManager;
using System;
using SM.SmartInfo.BIZ.CommonList;

namespace SM.SmartInfo.BIZ.NegativeNew
{
    class NegativeNewBiz : BizBase
    {
        private NegativeNewDao _dao = new NegativeNewDao();

        #region News
        public void GetListNews(NewsParam param)
        {
            var typeTime = param.TypeTime;
            var negativeType = param.News?.NegativeType;
            param.ListNews = _dao.GetListNews(negativeType, param.News?.Status, typeTime, param.PagingInfo);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var item in param.ListNews)
            {
                try
                {
                    item.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = item.NewsID,
                        RefType = SMX.AttachmentRefType.NegativeNews
                    });
                }
                catch
                {
                    item.Attachment = null;
                }
            }
        }

        public void SearchNegativeNews(NewsParam param)
        {
            var typeTime = param.TypeTime;
            var filterNews = param.News;
            var filterNegativeNews = param.NegativeNews;

            var lstNews = _dao.SearchNegativeNews(filterNews, typeTime, param.PagingInfo);

            var lstNegativeNews = _dao.SearchDetailNegativeNews(filterNegativeNews);

            var lstNewsSearchByNegativeNews = new List<SharedComponent.Entities.News>();

            lstNewsSearchByNegativeNews.AddRange(_dao.GetItemByListID(lstNegativeNews.Select(x => x.NewsID.GetValueOrDefault(0)).ToList()));

            //param.ListNews = lstNews.Where(x => lstNewsSearchByNegativeNews.Exists(c => c.NewsID == x.NewsID)).OrderBy(x => x.Classification).ThenByDescending(x => x.IncurredDTG).ThenBy(x => x.NegativeType).ToList();
            param.ListNews = lstNews.Where(x => lstNewsSearchByNegativeNews.Exists(c => c.NewsID == x.NewsID)).OrderByDescending(x => x.CreatedDTG).ToList();

            ///------
            //Tuần, tháng, tất cả
            //var typeTime = param.TypeTime;
            //var filterNews = param.News;
            ////var filterNegativeNews = param.NegativeNews;

            //var lstNews = _dao.SearchNegativeNews(filterNews, typeTime, param.PagingInfo);

            ////var lstNegativeNews = _dao.SearchDetailNegativeNews(filterNegativeNews);

            ////var lstNewsSearchByNegativeNews = new List<SharedComponent.Entities.News>();

            ////lstNewsSearchByNegativeNews.AddRange(_dao.GetItemByListID(lstNegativeNews.Select(x => x.NewsID.GetValueOrDefault(0)).ToList()));

            ////param.ListNews = lstNews.Where(x => lstNewsSearchByNegativeNews.Exists(c => c.NewsID == x.NewsID)).OrderBy(x => x.Classification).ThenByDescending(x => x.IncurredDTG).ThenBy(x => x.NegativeType).ToList();
            //param.ListNews = lstNews.OrderByDescending(x => x.IncurredDTG).ToList();
        }

        public void AddNewData(NewsParam param)
        {
            var item = param.News;
            item.Type = SMX.News.Type.NegativeNews;

            if (param.IsSaveComplete)
                ValidateNews(item);

            SetSystemInfo(item);

            _dao.InsertItem(item);
        }

        public void UpdateData(NewsParam param)
        {
            var item = param.News;

            if (param.IsSaveComplete)
                ValidateNews(item);

            SetSystemInfo(item);

            _dao.UpdateItem(item);
        }

        public void UpdateStatusHoanThanh(NewsParam param)
        {
            var item = param.News;
            item.Status = SMX.News.Status.HoanThanh;

            SetSystemInfo(item);

            _dao.UpdateItem(item);
        }

        public void GetAllNegativeNews(NewsParam param)
        {
            param.ListNews = _dao.GetAllNegativeNews();
        }

        public void LoadDataDisplay(NewsParam param)
        {
            int? newsID = param.NewsID;
            param.News = _dao.GetNews_ByID(newsID);
            param.News.ListAttachment = new List<adm_Attachment>();

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.News.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = newsID,
                RefType = SMX.AttachmentRefType.NegativeNews
            });
        }

        public void LoadDataImages(NewsParam param)
        {
            int? newsID = param.NewsID;

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = newsID,
                RefType = SMX.AttachmentRefType.NegativeNews
            });
        }

        public void LoadDataImagesDetail(NewsParam param)
        {
            int? negativeNewsID = param.NegativeNewsID;

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = negativeNewsID,
                RefType = SMX.AttachmentRefType.NegativeNewsDetail
            });
        }

        public void GetListNegativeNews(NewsParam param)
        {
            param.ListNegativeNews = _dao.GetListNegativeNew(param.NewsID, param.PagingInfo);
        }

        public void DeleteNewsAndNegativeNews(NewsParam param)
        {
            int? newsID = param.NewsID;
            var news = _dao.GetItemByID<SharedComponent.Entities.News>(newsID);
            news.Deleted = SMX.smx_IsDeleted;
            SetSystemInfo(news);
            var lstNegativeNews = _dao.GetListNegativeNew_ByID(newsID);
            foreach (var item in lstNegativeNews)
            {
                item.Deleted = SMX.smx_IsDeleted;
                SetSystemInfo(item);
            }
            using (TransactionScope trans = new TransactionScope())
            {
                _dao.UpdateItem(news);
                foreach (var item in lstNegativeNews)
                    _dao.UpdateItem(item);

                trans.Complete();
            }
        }

        public void DeleteNegativeNewsByNegativeNewsID(NewsParam param)
        {
            int? itemID = param.NegativeNewsID;
            _dao.DeleteNegativeNewsByNegativeNewsID(itemID);
        }

        public void ValidateNews(SharedComponent.Entities.News item)
        {
            List<string> lstErr = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Name))
                AddRequireErrorMessage("Tên sự vụ", lstErr);

            if (item.IncurredDTG == null)
                AddRequireErrorMessage("Ngày phát sinh", lstErr);

            if (item.NegativeType == null)
                AddRequireErrorMessage("Loại sự vụ", lstErr);

            if (item.Classification == null)
                AddRequireErrorMessage("Mức độ sự vụ", lstErr);

            if (item.Status == null)
                AddRequireErrorMessage("Tình trạng", lstErr);

            //if (string.IsNullOrWhiteSpace(item.RatedLevel))
            //    AddRequireErrorMessage("Đánh giá chi tiết", lstErr);

            if (string.IsNullOrWhiteSpace(item.PressAgency))
            {
                //if (item.NegativeType != null && item.NegativeType == SMX.News.NegativeNews.DaPhatSinh)
                //    AddRequireErrorMessage("Các báo đăng tải", lstErr);

                //if (item.NegativeType != null && item.NegativeType == SMX.News.NegativeNews.ChuaPhatSinh)
                //    AddRequireErrorMessage("Cơ quan báo chí liên hệ", lstErr);
                if (item.NegativeType != null && (item.NegativeType == SMX.News.NegativeNews.DaPhatSinh || item.NegativeType == SMX.News.NegativeNews.ChuaPhatSinh))
                    AddRequireErrorMessage("Thông tin cá nhân, tổ chức liên quan", lstErr);
            }

            //if (string.IsNullOrWhiteSpace(item.Resolution))
            //    AddRequireErrorMessage("Đề xuất phương án xử lý", lstErr);

            //if (string.IsNullOrWhiteSpace(item.ResolutionContent))
            //    AddRequireErrorMessage("Phê duyệt phương án xử lý", lstErr);

            if (string.IsNullOrWhiteSpace(item.Concluded))
                AddRequireErrorMessage("Thông tin tổng quan", lstErr);

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        #endregion

        #region Bang NegativeNews

        public void LoadDataNegativeNews(NewsParam param)
        {
            int? negativeNewsID = param.NegativeNewsID;
            param.NegativeNews = _dao.GetNegativeNewsFullInfo_ByID(negativeNewsID);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = negativeNewsID,
                RefType = SMX.AttachmentRefType.NegativeNewsDetail
            });
        }

        public void UpdateDataNegativeNews(NewsParam param)
        {
            var item = param.NegativeNews;
            ValidateNegativeNews(item);
            SetSystemInfo(item);
            _dao.UpdateItem(item);
        }

        public void FinishNegativeNews(NewsParam param)
        {
            var negativeNewsID = param.NegativeNewsID;
            var item = _dao.GetItemByID<NegativeNews>(negativeNewsID);
            item.Status = SMX.News.Status.HoanThanh;
            SetSystemInfo(item);
            _dao.UpdateItem(item);

        }

        public void AddNewsDataNegativeNews(NewsParam param)
        {
            var item = param.NegativeNews;
            item.Status = SMX.News.Status.MoiTao;
            ValidateNegativeNews(item);
            SetSystemInfo(item);
            _dao.InsertItem(item);
        }

        public void ValidateNegativeNews(NegativeNews item)
        {
            List<string> lstErr = new List<string>();

            if (item.Type == null)
                lstErr.Add("[Loại sự vụ] chưa được nhập");

            if (string.IsNullOrWhiteSpace(item.Name))
                lstErr.Add("[Tên vụ việc] chưa được nhập");

            if (item.IncurredDTG == null)
                lstErr.Add("[Thời gian] chưa được nhập");

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        #endregion 

        #region NegativeNewsResearched

        public void SaveNegativeNewsResearched(NewsParam param)
        {
            var item = param.NegativeNewsResearched;

            if (item.NegativeNewsResearchedID.HasValue && item.NegativeNewsResearchedID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;

                _dao.UpdateItem(item);
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = item.CreatedDTG == null ? DateTime.Now : item.CreatedDTG;

                _dao.InsertItem(item);
            }
        }

        public void GetListNegativeNewsResearched_ByNegativeNewsID(NewsParam param)
        {
            int? negativeNewsID = param.NegativeNewsID;
            param.ListNegativeNewsResearched = _dao.GetListNegativeNewsResearched_ByNegativeNewsID(negativeNewsID);
        }

        public void DeleteNegativeNewsResearched(NewsParam param)
        {
            int? itemID = param.NegativeNewsResearchedID;
            _dao.DeleteNegativeNewsResearched(itemID);
        }

        #endregion

        #region NewsResearched

        public void SaveNewsResearched(NewsParam param)
        {
            var item = param.NewsResearched;
            ValidateNewsResearched(item);
            if (item.NewsResearchedID.HasValue && item.NewsResearchedID != 0)
                _dao.UpdateItem(item);
            else
                _dao.InsertItem(item);
        }

        public void GetListNewsResearched_ByNewsID(NewsParam param)
        {
            int? negativeNewsID = param.NewsID;
            param.ListNewsResearched = _dao.GetListNewsResearched_ByNewsID(negativeNewsID);
        }

        public void DeleteNewsResearched(NewsParam param)
        {
            int? itemID = param.NewsResearchedID;
            _dao.DeleteNewsResearched(itemID);
        }

        private void ValidateNewsResearched(NewsResearched item)
        {
            List<string> lstErr = new List<string>();

            if (item.CreatedDTG == null)
                AddRequireErrorMessage("Thời gian", lstErr);

            if (string.IsNullOrWhiteSpace(item.Content))
                AddRequireErrorMessage("Nội dung trao đổi", lstErr);

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        #endregion
    }
}