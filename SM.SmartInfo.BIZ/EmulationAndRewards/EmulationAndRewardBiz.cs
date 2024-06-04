using System;
using System.Linq;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.BIZ.CommonList;
using SM.SmartInfo.DAO.CommonList;
using System.Text.RegularExpressions;
using SM.SmartInfo.DAO.EmulationAndRewards;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ.EmulationAndRewards
{
    class EmulationAndRewardBiz : BizBase
    {
        private AttachmentDao _daoAtt = new AttachmentDao();

        private EmulationAndRewardDao _dao = new EmulationAndRewardDao();

        private ECMAttachmentFileBiz _bizECM = new ECMAttachmentFileBiz();

        public void GetListAwardingTypeNoPaging(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingTypeNoPaging(param);
        }

        public void GetListAwardingLevelNoPaging(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingLevelNoPaging(param);
        }

        public void GetListAwardingPeriodNoPaging(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingPeriodNoPaging(param);
        }

        public void GetAwardingTypeCount(EmulationAndRewardParam param)
        {
            _dao.GetAwardingTypeCount(param);
        }

        public void DeleteSelectedAwardingType(EmulationAndRewardParam param)
        {
            _dao.DeleteSelectedAwardingType(param);
        }

        public void GetAwardingTypeById(EmulationAndRewardParam param)
        {
            _dao.GetAwardingTypeById(param);
        }

        public void EditAwardingType(EmulationAndRewardParam param)
        {
            _dao.EditAwardingType(param);
        }

        public void CreateAwardingType(EmulationAndRewardParam param)
        {
            _dao.CreateAwardingType(param);
        }

        public void GetListAwardingType(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingType(param);
        }

        public void EditAwardingPeriod(EmulationAndRewardParam param)
        {
            _dao.EditAwardingPeriod(param);
        }

        public void DeleteSelectedAwardingPeriod(EmulationAndRewardParam param)
        {
            _dao.DeleteSelectedAwardingPeriod(param);
        }

        public void GetAwardingPeriodById(EmulationAndRewardParam param)
        {
            _dao.GetAwardingPeriodById(param);
        }

        public void GetListAwardingPeriod(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingPeriod(param);
        }


        public void CreateAwardingPeriod(EmulationAndRewardParam param)
        {
            _dao.CreateAwardingPeriod(param);
        }

        public void DeleteSelectedAwardingLevel(EmulationAndRewardParam param)
        {
            _dao.DeleteSelectedAwardingLevel(param);
        }

        public void EditAwardingLevel(EmulationAndRewardParam param)
        {
            _dao.EditAwardingLevel(param);
        }

        public void GetAwardingLevelById(EmulationAndRewardParam param)
        {
            _dao.GetAwardingLevelById(param);
        }

        public void CreateAwardingLevel(EmulationAndRewardParam param)
        {
            _dao.CreateAwardingLevel(param);
        }

        public void GetListAwardingLevel(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingLevel(param);
        }

        public void GetAwardingCatalogById(EmulationAndRewardParam param)
        {
            _dao.GetAwardingCatalogById(param);
        }

        public void DeleteSelectedAwardingCatalog(EmulationAndRewardParam param)
        {
            _dao.DeleteSelectedAwardingCatalog(param);
        }

        public void GetListAwardingCatalog(EmulationAndRewardParam param)
        {
            _dao.GetListAwardingCatalog(param);
        }

        public void EditAwardingCatalog(EmulationAndRewardParam param)
        {
            _dao.EditAwardingCatalog(param);
        }

        public void AddNewAwardingCatalog(EmulationAndRewardParam param)
        {
            _dao.AddNewAwardingCatalog(param);
        }

        public void SetupFormDisplay(EmulationAndRewardParam param)
        {
            param.ListEmulationAndRewardSubject = _dao.SetupFormDisplay(param.er_EmulationAndReward);
        }

        public void BuildTreeListEmulationAndRewards(EmulationAndRewardParam param)
        {
            param.ListEmulationAndReward = _dao.BuildTreeListEmulationAndRewards();
        }

        public void GetListEmulationAndRewardByFilter(EmulationAndRewardParam param)
        {
            var filter = param.er_EmulationAndReward;

            param.ListEmulationAndRewardSubject = _dao.GetListEmulationAndRewardSubjectByFilter(filter, param.PagingInfo);
        }

        public void SaveEmulationAndRewardSubject(EmulationAndRewardParam param)
        {
            var subject = param.er_EmulationAndRewardSubject;

            _dao.UpdateItem(subject);
        }

        public void DeleteEmulationAndRewardSubject(EmulationAndRewardParam param)
        {
            var subject = param.er_EmulationAndRewardSubject;
            subject.Deleted = SMX.smx_IsDeleted;

            _dao.UpdateItem(subject);
        }

        public void GetListEmulationAndRewardHistory(EmulationAndRewardParam param)
        {
            var subjectID = param.er_EmulationAndRewardHistory?.EmulationAndRewardSubjectID;

            var subject = _dao.GetItemByID<er_EmulationAndRewardSubject>(subjectID);

            param.er_EmulationAndRewardSubject = subject;

            param.ListAttachment = _daoAtt.GetAttachmentsByListRefID(subject.EmulationAndRewardID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Utility.GetNullableInt(x).GetValueOrDefault(0)).ToList());

            param.ListEmulationAndRewardHistory = _dao.GetListEmulationAndRewardHistory(subjectID);
        }

        public void AddNewEmulationAndReward(EmulationAndRewardParam param)
        {
            try
            {
                if (param.IsSaveComplete)
                    ValidateEmulationAndReward(param.er_EmulationAndReward);

                _dao.AddNewEmulationAndReward(param);

                _dao.GetListEmulationAndReward(param);

                int? erID = param.ListEmulationAndReward.OrderByDescending(x => x.CreatedDTG).FirstOrDefault() != null ?
                    param.ListEmulationAndReward.OrderByDescending(x => x.CreatedDTG).FirstOrDefault().EmulationAndRewardID : null;

                if (param.ListEmulationAndRewardSubject != null)
                {
                    foreach (var item in param.ListEmulationAndRewardSubject)
                    {
                        item.EmulationAndRewardID = erID.ToString();

                        _dao.AddNewEmulationAndRewardSubject(item, param);
                    }
                }

                if (param.ListAttachment != null)
                {
                    foreach (var attachment in param.ListAttachment)
                    {
                        attachment.RefID = erID;

                        if (attachment.FileContent != null)
                        {
                            attachment.CreatedBy = Profiles.MyProfile.UserName;
                            attachment.CreatedDTG = DateTime.Now;

                            _bizECM.UploadFile(attachment);
                        }
                    }
                }
            }
            catch (SMXException ex)
            {
                throw new SMXException(ex.ToString());
            }

            /*try
            {
                _dao.InsertItem(er);
                erID = er.EmulationAndRewardID;

                string xmlFilePath = string.Empty;
                switch (er.SubjectRewarded)
                {
                    case SMX.EmulationAndRewardSubjectRewarded.CaNhan:
                        var attCaNhan = param.AttachmentCaNhan;

                        if (attCaNhan != null)
                        {
                            xmlFilePath = SMX.ImportingType.dic[SMX.ImportingType.ImportCaNhanKhenThuong].MappingFileName;
                            var lstCaNhan = ExcelReader.Read<er_EmulationAndRewardSubject>(attCaNhan.FileName, attCaNhan.FileContent, xmlFilePath);

                            ValidateSubject(lstCaNhan);

                            BulkData(lstCaNhan, er.EmulationAndRewardID, SMX.EmulationAndRewardSubjectType.CaNhan);
                        }
                        break;
                    case SMX.EmulationAndRewardSubjectRewarded.DonViToChuc:
                        var attDonVi = param.AttachmentDonVi;

                        if (attDonVi != null)
                        {
                            xmlFilePath = SMX.ImportingType.dic[SMX.ImportingType.ImportDonViKhenThuong].MappingFileName;
                            var lstDonVi = ExcelReader.Read<er_EmulationAndRewardSubject>(attDonVi.FileName, attDonVi.FileContent, xmlFilePath);

                            ValidateSubject(lstDonVi);

                            BulkData(lstDonVi, er.EmulationAndRewardID, SMX.EmulationAndRewardSubjectType.DonViToChuc);
                        }
                        break;
                    case SMX.EmulationAndRewardSubjectRewarded.All:
                        var attAll_CaNhan = param.AttachmentCaNhan;

                        if (attAll_CaNhan != null)
                        {
                            xmlFilePath = SMX.ImportingType.dic[SMX.ImportingType.ImportCaNhanKhenThuong].MappingFileName;
                            var lstAll_CaNhan = ExcelReader.Read<er_EmulationAndRewardSubject>(attAll_CaNhan.FileName, attAll_CaNhan.FileContent, xmlFilePath);

                            ValidateSubject(lstAll_CaNhan);

                            BulkData(lstAll_CaNhan, er.EmulationAndRewardID, SMX.EmulationAndRewardSubjectType.CaNhan);
                        }

                        var attAll_DonVi = param.AttachmentDonVi;

                        if (attAll_DonVi != null)
                        {
                            xmlFilePath = SMX.ImportingType.dic[SMX.ImportingType.ImportDonViKhenThuong].MappingFileName;
                            var lstAll_DonVi = ExcelReader.Read<er_EmulationAndRewardSubject>(attAll_DonVi.FileName, attAll_DonVi.FileContent, xmlFilePath);

                            ValidateSubject(lstAll_DonVi);

                            BulkData(lstAll_DonVi, er.EmulationAndRewardID, SMX.EmulationAndRewardSubjectType.DonViToChuc);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                var erDeleted = new er_EmulationAndReward()
                {
                    EmulationAndRewardID = erID,
                    Deleted = 0,
                };
                _dao.UpdateItem(erDeleted);

                throw new SMXException("File import lỗi.");
            }*/
        }

        private void ValidateEmulationAndReward(er_EmulationAndReward item)
        {
            Regex regexDoubleQuote = new Regex("\"([^\"]*)\"");
            Regex regex = new Regex(@"^[a-zA-Z’\-'()/.,\s]+$");

            List<string> lstErr = new List<string>();

            if (item.Year == null)
                lstErr.Add("[Năm] không được để trống.");

            if (string.IsNullOrWhiteSpace(item.EmulationAndRewardUnit))
                lstErr.Add("[Đơn vị khen thưởng] không được để trống.");

            /*if (regex.IsMatch(item.EmulationAndRewardUnit) || regexDoubleQuote.IsMatch(item.EmulationAndRewardUnit))
                lstErr.Add("[Đơn vị khen thưởng] không được chứa ký tự đặc biệt.");*/

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        private void ValidateSubject(List<er_EmulationAndRewardSubject> lstItem)
        {
            List<string> lstErr = new List<string>();

            if (lstItem.Exists(x => string.IsNullOrWhiteSpace(x.Code)))
                lstErr.Add("[Mã] không được để trống.");

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        private void BulkData(List<er_EmulationAndRewardSubject> lstSubject, int? erID, int? type)
        {
            var er = _dao.GetItemByID<er_EmulationAndReward>(erID);

            foreach (var sub in lstSubject)
            {
                sub.Type = type;
                sub.LatestEmulationAndRewardUnit = er.EmulationAndRewardUnit;

                var existsSub = _dao.GetEmulationAndRewardSubjectByCode(sub.Code);
                if (existsSub != null)
                {
                    sub.EmulationAndRewardID = existsSub.EmulationAndRewardID + string.Format(",{0},", erID);
                    sub.EmulationAndRewardSubjectID = existsSub.EmulationAndRewardSubjectID;
                    sub.UpdatedBy = Profiles.MyProfile.UserName;
                    sub.UpdatedDTG = DateTime.Now;

                    _dao.UpdateItem(sub);
                }
                else
                {
                    sub.EmulationAndRewardID = string.Format(",{0},", erID);
                    sub.Deleted = SMX.smx_IsNotDeleted;
                    sub.Version = SMX.smx_FirstVersion;
                    sub.CreatedBy = Profiles.MyProfile.UserName;
                    sub.CreatedDTG = DateTime.Now;

                    _dao.InsertItem(sub);
                }

                er_EmulationAndRewardHistory his = new er_EmulationAndRewardHistory()
                {
                    EmulationAndRewardSubjectID = sub.EmulationAndRewardSubjectID,
                    EmulationAndRewardID = erID,
                    Title = sub.LatestTitle,
                    EmulationAndRewardUnit = er.EmulationAndRewardUnit,
                    RewardedDTG = DateTime.Now
                };

                _dao.InsertItem(his);
            }
        }
    }
}