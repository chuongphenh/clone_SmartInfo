using System;
using System.Linq;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.BIZ.CommonList;
using SM.SmartInfo.DAO.PressAgency;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.DAO.Notification;
using System.Globalization;
using SM.SmartInfo.Utils;

namespace SM.SmartInfo.BIZ.PressAgency
{
    class PressAgencyBiz : BizBase
    {
        private PressAgencyDao _dao = new PressAgencyDao();
        private NotificationDao _ntfDao = new NotificationDao();

        public void DeleteShare(PressAgencyParam param)
        {
            _dao.DeleteShare(param);
        }

        public void GetListSharedUser(PressAgencyParam param)
        {
            param.listUserShared = _dao.GetListSharedUser(param);
        }

        public void GetPressAgencyTypeByCode(PressAgencyParam param)
        {
            param.AgencyType = _dao.GetPressAgencyTypeByCode(param);
        }

        public void AddNewPressAgency(PressAgencyParam param)
        {
            _dao.InsertPressAgency(param.PressAgency);
        }
        public void GetPressAgencyByName(PressAgencyParam param)
        {
            param.PressAgency = _dao.GetPressAgencyByName(param);
        }
        public void GetPressAgencyHRByName(PressAgencyParam param)
        {
            param.PressAgencyHR = _dao.GetPressAgencyHRByName(param);
        }
        public void GetListPressAgencyHRByName(PressAgencyParam param)
        {
            param.ListPressAgencyHR = _dao.GetListPressAgencyHRByName(param);
        }
        public void ImportOrUpdateListPressAgencyHRFromExcel(PressAgencyParam param)
        {
            _dao.ImportOrUpdateListPressAgencyHRFromExcel(param);
        }
        public void GetListPressAgencyByType(PressAgencyParam param)
        {
            param.ListPressAgency = _dao.GetListPressAgencyByType(param);
        }
        public void SearchPressAgency(CommonParam param)
        {
            param.ListPressAgency = _dao.SearchPressAgency(param.SearchText, param.PagingInfo);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var pressAgency in param.ListPressAgency)
            {
                try
                {
                    pressAgency.CountHR = _dao.GetAllListPressAgencyHR_ByPressAgencyID(pressAgency.PressAgencyID).Count;
                    pressAgency.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = pressAgency.PressAgencyID,
                        RefType = SMX.AttachmentRefType.PressAgency
                    });
                }
                catch
                {
                    pressAgency.Attachment = null;
                }
            }
        }

        public void SetupFormDefault(PressAgencyParam param)
        {
            param.ListPressAgency = _dao.SetupFormDefault(param.txtSearchUserShared);
            param.ListPressAgencyHR = _dao.GetAllListPressAgencyHR();
        }

        public void GetListPressAgency(PressAgencyParam param)
        {
            param.ListPressAgency = _dao.GetListPressAgency(param.PagingInfo);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var pressAgency in param.ListPressAgency)
            {
                try
                {
                    pressAgency.CountHR = _dao.GetAllListPressAgencyHR_ByPressAgencyID(pressAgency.PressAgencyID).Count;
                    pressAgency.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = pressAgency.PressAgencyID,
                        RefType = SMX.AttachmentRefType.PressAgency
                    });
                }
                catch
                {
                    pressAgency.Attachment = null;
                }
            }
        }

        public void SearchItemsForView(PressAgencyParam param)
        {
            var pa = param.PressAgency;
            var paHR = param.PressAgencyHR;
            var paHRHistory = param.PressAgencyHRHistory;
            var paHRRelatives = param.PressAgencyHRRelatives;
            var paHistory = param.PressAgencyHistory;
            var paMeeting = param.PressAgencyMeeting;
            var paRelations = param.RelationsPressAgency;
            var att = param.Attachment;

            var lstResult = _dao.SearchPressAgency(pa);

            var lstPAIDOther = _dao.SearchPressAgencyHR_GetPressAgencyID(paHR);

            if (lstPAIDOther == null)
                lstPAIDOther = new List<agency_PressAgency>();

            var lstPAHRHistory = _dao.SearchPressAgencyHRHistory(paHRHistory);

            var lstPAHRRelatives = _dao.SearchPressAgencyHRRelatives(paHRRelatives);

            var lstPAHistory = _dao.SearchPressAgencyHistory(paHistory);

            var lstPAMeeting = _dao.SearchPressAgencyMeeting(paMeeting);

            var lstPARelations = _dao.SearchPressAgencyRelations(paRelations);

            var lstPAAtt = _dao.SearchPressAgencyOtherImage(att);

            var lstPA = lstResult.Where(x => lstPAIDOther.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || lstPAHRHistory.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || lstPAHRRelatives.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || lstPAHistory.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || lstPAMeeting.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || lstPARelations.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || lstPAAtt.Exists(c => c.PressAgencyID == x.PressAgencyID)
                                        || x.PressAgencyID == x.PressAgencyID).OrderBy(x => x.Name).ThenBy(x => x.DisplayOrder).ToList();

            var pagingInfo = param.PagingInfo;
            pagingInfo.RecordCount = lstPA.Count;
            param.ListPressAgency = lstPA.Skip(pagingInfo.PageIndex * pagingInfo.PageSize).Take(pagingInfo.PageSize).ToList();

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var pressAgency in param.ListPressAgency)
            {
                try
                {
                    pressAgency.CountHR = _dao.GetAllListPressAgencyHR_ByPressAgencyID(pressAgency.PressAgencyID).Count;
                    pressAgency.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = pressAgency.PressAgencyID,
                        RefType = SMX.AttachmentRefType.PressAgency
                    });
                }
                catch
                {
                    pressAgency.Attachment = null;
                }
            }
        }

        public void LoadDataDisplay(PressAgencyParam param)
        {
            int? pressAgencyID = param.PressAgency.PressAgencyID;
            param.PressAgency = _dao.GetPressAgency_ByID(pressAgencyID);

            try
            {
                ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
                param.PressAgency.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                {
                    RefID = pressAgencyID,
                    RefType = SMX.AttachmentRefType.PressAgency
                });
            }
            catch
            {
                param.Attachment = null;
            }
        }

        public void SavePressAgency(PressAgencyParam param)
        {
            var item = param.PressAgency;

            if (param.IsSaveComplete)
                ValidatePressAgency(item);

            if (item.PressAgencyID.HasValue && item.PressAgencyID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
                try
                {
                    LogManager.WebLogger.LogDebug("Start Cập nhật Sự vụ: ---- Xóa noti nếu update ngày sự vụ", null);
                    var currentHr = _dao.GetAgencyByID((int)item.PressAgencyID);
                    if (currentHr != null && currentHr.EstablishedDTG != null && item.EstablishedDTG != currentHr.EstablishedDTG) DeleteNotificationHR((int)item.PressAgencyID);
                    LogManager.WebLogger.LogDebug("End Cập nhật Sự vụ: ---- Xóa noti nếu update sự vụ", null);
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogDebug($"Cập nhật Sự vụ: {ex.Message}", null);
                }
                finally
                {
                    _dao.UpdateItem(item);
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

        public void DeletePressAgency(PressAgencyParam param)
        {
            int? itemID = param.PressAgency.PressAgencyID;
            try
            {
                LogManager.WebLogger.LogDebug("Start Xóa sự vụ", null);
                DeleteNotificationHR((int)itemID);
                LogManager.WebLogger.LogDebug("End Xóa  sự vụ", null);
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogDebug($"Cập nhật Danh bạ: {ex.Message}", null);
            }
            finally
            {
                _dao.DeletePressAgency(itemID);
            }
        }

        public void SearchPressAgencySelector(PressAgencyParam param)
        {
            param.ListPressAgency = _dao.SearchPressAgencySelector(param.Name);
        }

        #region Press Agency HR

        public void AddNewAgencyType(PressAgencyParam param)
        {
            var item = param.AgencyType;
            if (param.IsSaveComplete)
                ValidateAgencyType(item);
            _dao.InsertAgencyTypeItem(item);
        }

        public void SavePressAgencyHR(PressAgencyParam param)
        {
            var item = param.PressAgencyHR;

            if (param.IsSaveComplete)
                ValidatePressAgencyHR(item);

            if (item.PressAgencyHRID.HasValue && item.PressAgencyHRID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
                try
                {
                    LogManager.WebLogger.LogDebug("Start Cập nhật Danh bạ: ---- Xóa noti nếu update ngày sinh", null);
                    var currentHr = _dao.GetPressAgencyHR_ByID(item.PressAgencyHRID);
                    if (currentHr != null && currentHr.DOB != null && item.DOB != currentHr.DOB) DeleteNotificationHR((int)item.PressAgencyHRID);
                    LogManager.WebLogger.LogDebug("End Cập nhật Danh bạ: ---- Xóa noti nếu update ngày sinh", null);

                    //Cập nhật nhóm quyền
                        // xoá nhóm quyền cũ
                        _dao.DeleteNamePermissionGroups(item.PressAgencyHRID);
                        //Cập nhật
                        foreach (var itemNamePermissionGroup in item.NamePermissionGroups)
                        {
                            _dao.InsertNamePermissionGroups(itemNamePermissionGroup, item.PressAgencyHRID.ToString());
                        }

                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogError("ERROR: DANH BẠ _ Insert nhóm quyền cho PressAngcyHR khi thêm cập nhật danh bạ");
                    LogManager.WebLogger.LogDebug($"Cập nhật Danh bạ: {ex.Message}", null);
                }
                finally
                {
                    _dao.UpdateItem(item);
                }

            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;
                //_dao.InsertItem(item);
                try
                {
                    item.PressAgencyHRID = _dao.InsertPressAgencyHRAndGetID(item);
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogError("ERROR: DANH BẠ _ Insert nhóm quyền cho PressAngcyHR khi tạo danh bạ mới " + ex.ToString());
                }
                //Thêm nhóm quyền của PressAgencyHR
                try
                {
                    foreach (var itemNamePermissionGroup in item.NamePermissionGroups)
                    {
                        _dao.InsertNamePermissionGroups(itemNamePermissionGroup, item.PressAgencyHRID.ToString());
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogError("ERROR: DANH BẠ _ Insert nhóm quyền cho PressAngcyHR khi thêm mới danh bạ" + ex.ToString());
                }
            }
        }
        // Xóa để tránh thong báo
        public void DeleteNotificationHR(int pressAgencyHRID)
        {

            _ntfDao.DeleteNotificationByHRAlertID(pressAgencyHRID);

        }
        public void UpdateNotiContact(agency_PressAgencyHR item)
        {
            ChineseLunisolarCalendar lunarCalendar = new ChineseLunisolarCalendar();
            var lstPressAgencyHRAlert = _dao.GetListPressAgencyHRAlert_ByPressAgencyHRID(item.PressAgencyHRID);
            foreach (var hr in lstPressAgencyHRAlert)
            {
                if (_ntfDao.checkExistingNotificationByAlertId(hr.PressAgencyHRAlertID))
                {
                    //var typeDate = hr.TypeDate == 1 ? "Dương lịch" : "Âm lịch";
                    string smg = "";
                    string strDate = "";
                    if (hr.TypeDate == 2)
                    {

                        smg += "Âm Lịch";
                        hr.lunarDay = lunarCalendar.GetDayOfMonth(hr.AlertDTG.Value);
                        hr.lunarMonth = lunarCalendar.GetMonth(hr.AlertDTG.Value);
                        hr.lunarYear = lunarCalendar.GetYear(hr.AlertDTG.Value);

                        strDate = $"{ lunarCalendar.GetDayOfMonth(hr.AlertDTG.Value)}/{ lunarCalendar.GetMonth(hr.AlertDTG.Value)}/{lunarCalendar.GetYear(hr.AlertDTG.Value)}";
                    }
                    else
                    {
                        smg += "Dương Lịch";
                        strDate = Utility.GetDateString(hr.AlertDTG);

                    }

                    _ntfDao.UpdateNotification(new ntf_Notification()
                    {
                        lunarDay = hr.TypeDate == 1 ? lunarCalendar.GetDayOfMonth(DateTime.Now) : hr.lunarDay,
                        lunarMonth = hr.TypeDate == 1 ? lunarCalendar.GetMonth(DateTime.Now) : hr.lunarMonth,
                        lunarYear = hr.TypeDate == 1 ? lunarCalendar.GetYear(DateTime.Now) : hr.lunarYear,
                        DoDTG = hr.AlertDTG,
                        Content = hr.TypeDate == 1 ? (string.IsNullOrEmpty(hr.Content) ? "Sinh nhật" : hr.Content) + " của ông/bà" + $" {item.FullName} " + $"({smg})" :
                        (string.IsNullOrEmpty(hr.Content) ? "Ngày giỗ" : hr.Content) + " của ông/bà" + $" {item.FullName} " + $"({strDate} - {smg})",
                        Type = hr.TypeDate == 1 ? 1 : 5,
                        Note = item.Position,
                        Comment = null,
                        AlertID = hr.PressAgencyHRAlertID,
                        UpdateDTG = DateTime.Now,
                        CreatedBy = Profiles.MyProfile.UserName
                    });
                }
            }
        }
        public void DeletePressAgencyHR(PressAgencyParam param)
        {
            int? itemID = param.PressAgencyHR.PressAgencyHRID;
            try
            {
                LogManager.WebLogger.LogDebug("Start Xóa Người thân: ---- Xóa noti nếu update ngày sinh", null);
                DeleteNotificationHR((int)itemID);
                _dao.UpdatePressAgencyHRAlert_ByPressAgencyHRID(itemID);
                LogManager.WebLogger.LogDebug("End Xóa Người thân: ---- Xóa noti nếu update ngày sinh", null);
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogDebug($"Cập nhật Danh bạ: {ex.Message}", null);
            }
            finally
            {
                _dao.DeletePressAgencyHR(itemID);

            }

        }

        public void GetPressAgencyHR_ByID(PressAgencyParam param)
        {
            int? pressAgencyHRID = param.PressAgencyHR.PressAgencyHRID;

            if (pressAgencyHRID.HasValue && pressAgencyHRID != 0)
            {
                param.PressAgencyHR = _dao.GetPressAgencyHR_ByID(pressAgencyHRID);
                if (param.PressAgencyHR != null)
                    param.PressAgencyHR.PressAgencyTypeString = Utils.Utility.GetDictionaryValue(SMX.PressAgencyType.dicDescFull, param.PressAgencyHR.PressAgencyType);

                try
                {
                    ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
                    param.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = pressAgencyHRID,
                        RefType = SMX.AttachmentRefType.PressAgencyHR
                    });
                }
                catch
                {
                    param.Attachment = null;
                }
            }
        }

        public void GetListPressAgencyHR_ByPressAgencyID(PressAgencyParam param)
        {
            int? pressAgencyID = param.PressAgency.PressAgencyID;
            int? attitude = param.PressAgency.Attitude;
            param.ListPressAgencyHR = _dao.GetListPressAgencyHR_ByPressAgencyID(pressAgencyID, attitude);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var hr in param.ListPressAgencyHR)
            {
                try
                {
                    hr.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = hr.PressAgencyHRID,
                        RefType = SMX.AttachmentRefType.PressAgencyHR
                    });
                }
                catch
                {
                    hr.Attachment = null;
                }
            }
        }

        public void GetListPressAgencyHR(PressAgencyParam param)
        {
            param.ListPressAgencyHR = _dao.GetListPressAgencyHR(param.PagingInfo, param.UserId, Profiles.MyProfile.UserName);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var hr in param.ListPressAgencyHR)
            {
                try
                {
                    if (string.IsNullOrEmpty(hr.PressAgencyTypeString))
                    {
                        hr.PressAgencyTypeString = Utils.Utility.GetDictionaryValue(SMX.PressAgencyType.dicDescFull, hr.PressAgencyType);
                    }

                    hr.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = hr.PressAgencyHRID,
                        RefType = SMX.AttachmentRefType.PressAgencyHR
                    });
                }
                catch
                {
                    hr.Attachment = null;
                }
            }
        }

        public void GetListPressAgencyHR_ByFilter(PressAgencyParam param)
        {
            var filter = param.PressAgencyHR;
            param.ListPressAgencyHR = _dao.GetListPressAgencyHR_ByFilter(filter, param.PagingInfo, param.UserId, Profiles.MyProfile.UserName);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var hr in param.ListPressAgencyHR)
            {
                try
                {
                    hr.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = hr.PressAgencyHRID,
                        RefType = SMX.AttachmentRefType.PressAgencyHR
                    });
                }
                catch
                {
                    hr.Attachment = null;
                }
            }
        }

        public void GetListPressAgencyHR_ByFilterNoPaging(PressAgencyParam param)
        {
            var filter = param.PressAgencyHR;
            param.ListPressAgencyHR = _dao.GetListPressAgencyHR_ByFilter(filter, param.UserId, Profiles.MyProfile.UserName);

            var all = param.ListPressAgencyHR.Sum(x => x.CountByType);
            param.ListPressAgencyHR.Insert(0, new agency_PressAgencyHR()
            {
                PressAgencyType = SMX.PressAgencyType.All,
                CountByType = all,
            });

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var hr in param.ListPressAgencyHR)
            {
                try
                {
                    hr.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = hr.PressAgencyHRID,
                        RefType = SMX.AttachmentRefType.PressAgencyHR
                    });
                }
                catch
                {
                    hr.Attachment = null;
                }
            }
        }

        #region Press Agency HR History

        public void GetListPressAgencyHRHistory_ByPressAgencyHRID(PressAgencyParam param)
        {
            int? pressAgencyHRID = param.PressAgencyHR.PressAgencyHRID;
            param.ListPressAgencyHRHistory = _dao.GetListPressAgencyHRHistory_ByPressAgencyHRID(pressAgencyHRID);
        }

        public void SavePressAgencyHRHistory(PressAgencyParam param)
        {
            var item = param.PressAgencyHRHistory;

            if (item.PressAgencyHRHistoryID.HasValue && item.PressAgencyHRHistoryID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;

                _dao.UpdateItem(item);
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                _dao.InsertItem(item);
            }
        }

        public void DeletePressAgencyHRHistory(PressAgencyParam param)
        {
            int? itemID = param.PressAgencyHRHistory.PressAgencyHRHistoryID;
            _dao.DeletePressAgencyHRHistory(itemID);
        }

        #endregion

        #region Press Agency HR Alert

        public void GetListPressAgencyHRAlert_ByPressAgencyHRID(PressAgencyParam param)
        {
            int? pressAgencyHRID = param.PressAgencyHR.PressAgencyHRID;
            param.ListPressAgencyHRAlert = _dao.GetListPressAgencyHRAlert_ByPressAgencyHRID(pressAgencyHRID);
        }

        public void SavePressAgencyHRAlert(PressAgencyParam param)
        {
            var item = param.PressAgencyHRAlert;

            if (item.PressAgencyHRAlertID.HasValue && item.PressAgencyHRAlertID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
                try
                {
                    LogManager.WebLogger.LogDebug("Start Cập nhật quan hệ ng thân", null);
                    var currentHr = _dao.GetListPressAgencyHRAlert_ByPressAgencyHRAlert(item.PressAgencyHRAlertID);
                    if (currentHr != null)
                    {
                        // Kiểm tra loại ngày là ngày dương (TypeDate == 1)
                        if (currentHr.TypeDate == 1 && currentHr.AlertDTG != item.AlertDTG)
                        {
                            DeleteNotificationHR((int)item.PressAgencyHRAlertID);
                        }
                        // Kiểm tra loại ngày là ngày Âm (TypeDate == 2)
                        else if (currentHr.TypeDate == 2 &&
                                 (currentHr.lunarYear != item.lunarYear ||
                                  currentHr.lunarMonth != item.lunarMonth ||
                                  currentHr.lunarDay != item.lunarDay))
                        {
                            DeleteNotificationHR((int)item.PressAgencyHRAlertID);
                        }
                    }
                    LogManager.WebLogger.LogDebug("End Cập nhật quan hệ ng thân", null);
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogDebug($"Cập nhật Danh bạ: {ex.Message}", null);
                }
                finally
                {
                    _dao.UpdateItem(item);
                }


                _dao.UpdateItem(item);
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                _dao.InsertItem(item);
            }
        }

        public void DeletePressAgencyHRAlert(PressAgencyParam param)
        {
            int? itemID = param.PressAgencyHRAlert.PressAgencyHRAlertID;
            _dao.DeletePressAgencyHRAlert(itemID);
        }
        public void DeletePressAgencyHRAlertByPressAgenctyHrID(PressAgencyParam param)
        {
            int? itemID = param.PressAgency.PressAgencyID;
            _dao.DeletePressAgencyHRAlertByPressAgenctyHrID(itemID);
        }

        #endregion

        #region Press Agency HR Relatives

        public void GetListPressAgencyHRRelatives_ByPressAgencyHRID(PressAgencyParam param)
        {
            int? pressAgencyHRID = param.PressAgencyHR.PressAgencyHRID;
            param.ListPressAgencyHRRelatives = _dao.GetListPressAgencyHRRelatives_ByPressAgencyHRID(pressAgencyHRID);
        }

        public void SavePressAgencyHRRelatives(PressAgencyParam param)
        {
            var item = param.PressAgencyHRRelatives;

            if (item.PressAgencyHRRelativesID.HasValue && item.PressAgencyHRRelativesID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;
                try
                {
                    LogManager.WebLogger.LogDebug("Start Cập nhật quan hệ danh bạ: ---- Xóa noti nếu update ngày sinh", null);
                    var currentHr = _dao.GetListRelationsByRelationsID(item.PressAgencyHRRelativesID);
                    if (currentHr != null && currentHr != null && item.DOB != currentHr.DOB) DeleteNotificationHR((int)item.PressAgencyHRRelativesID);
                    LogManager.WebLogger.LogDebug("End Cập nhật quan hệ danh bạ: ---- Xóa noti nếu update ngày sinh", null);
                }
                catch (Exception ex)
                {
                    LogManager.WebLogger.LogDebug($"Cập nhật quan hệ danh bạ: {ex.Message}", null);
                }
                finally
                {
                    _dao.UpdateItem(item);
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

        public void DeletePressAgencyHRRelatives(PressAgencyParam param)
        {
            int? itemID = param.PressAgencyHRRelatives.PressAgencyHRRelativesID;
            try
            {
                LogManager.WebLogger.LogDebug("Start Xóa Người thân: ---- Xóa noti nếu update ngày sinh", null);
                DeleteNotificationHR((int)itemID);
                LogManager.WebLogger.LogDebug("End Xóa Người thân: ---- Xóa noti nếu update ngày sinh", null);
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogDebug($"Cập nhật Danh bạ: {ex.Message}", null);
            }
            finally
            {
                _dao.DeletePressAgencyHRRelatives(itemID);
            }

        }

        #endregion

        #endregion

        #region Press Agency History

        public void GetListPressAgencyHistory_ByPressAgencyID(PressAgencyParam param)
        {
            int? pressAgencyID = param.PressAgency.PressAgencyID;
            param.ListPressAgencyHistory = _dao.GetListPressAgencyHistory_ByPressAgencyID(pressAgencyID, param.PagingInfo);
        }

        public void SavePressAgencyHistory(PressAgencyParam param)
        {
            var item = param.PressAgencyHistory;

            if (item.PressAgencyHistoryID.HasValue && item.PressAgencyHistoryID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;

                _dao.UpdateItem(item);
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                _dao.InsertItem(item);
            }
        }

        public void DeletePressAgencyHistory(PressAgencyParam param)
        {
            int? itemID = param.PressAgencyHistory.PressAgencyHistoryID;
            _dao.DeletePressAgencyHistory(itemID);
        }

        #endregion

        #region Press Agency Meeting

        public void GetListPressAgencyMeeting_ByPressAgencyID(PressAgencyParam param)
        {
            int? pressAgencyID = param.PressAgency.PressAgencyID;
            param.ListPressAgencyMeeting = _dao.GetListPressAgencyMeeting_ByPressAgencyID(pressAgencyID, param.PagingInfo);
        }

        public void SavePressAgencyMeeting(PressAgencyParam param)
        {
            var item = param.PressAgencyMeeting;

            if (item.PressAgencyMeetingID.HasValue && item.PressAgencyMeetingID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;

                _dao.UpdateItem(item);
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                _dao.InsertItem(item);
            }
        }

        public void DeletePressAgencyMeeting(PressAgencyParam param)
        {
            int? itemID = param.PressAgencyMeeting.PressAgencyMeetingID;
            _dao.DeletePressAgencyMeeting(itemID);
        }

        #endregion

        #region Relations Press Agency

        public void GetListRelationsPressAgency_ByPressAgencyID(PressAgencyParam param)
        {
            int? pressAgencyID = param.PressAgency.PressAgencyID;
            param.ListRelationsPressAgency = _dao.GetListRelationsPressAgency_ByPressAgencyID(pressAgencyID, param.PagingInfo);
        }

        public void SaveRelationsPressAgency(PressAgencyParam param)
        {
            var item = param.RelationsPressAgency;

            if (item.RelationsPressAgencyID.HasValue && item.RelationsPressAgencyID != 0)
            {
                item.UpdatedBy = Profiles.MyProfile.UserName;
                item.UpdatedDTG = DateTime.Now;

                _dao.UpdateItem(item);
            }
            else
            {
                item.Deleted = SMX.smx_IsNotDeleted;
                item.CreatedBy = Profiles.MyProfile.UserName;
                item.CreatedDTG = DateTime.Now;

                _dao.InsertItem(item);
            }
        }

        public void DeleteRelationsPressAgency(PressAgencyParam param)
        {
            int? itemID = param.RelationsPressAgency.RelationsPressAgencyID;
            _dao.DeleteRelationsPressAgency(itemID);
        }

        #endregion

        #region Relationship With MB

        public void GetListRelationshipWithMB_ByPressAgencyID(PressAgencyParam param)
        {
            int? pressAgencyID = param.PressAgency.PressAgencyID;
            param.ListRelationshipWithMB = _dao.GetListRelationshipWithMB_ByPressAgencyID(pressAgencyID, param.PagingInfo);
        }

        public void SaveRelationshipWithMB(PressAgencyParam param)
        {
            var item = param.RelationshipWithMB;

            if (item.RelationshipWithMBID.HasValue && item.RelationshipWithMBID != 0)
                _dao.UpdateItem(item);
            else
                _dao.InsertItem(item);
        }

        public void DeleteRelationshipWithMB(PressAgencyParam param)
        {
            int? itemID = param.RelationshipWithMB.RelationshipWithMBID;
            _dao.DeleteRelationshipWithMB(itemID);
        }

        #endregion

        public void ValidatePressAgency(agency_PressAgency item)
        {
            List<string> lstErr = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Name))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Tên tổ chức"));

            if (item.Type == null)
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Loại tổ chức"));

            if (item.Type != null && item.Type == SMX.PressAgencyType.Other && string.IsNullOrWhiteSpace(item.TypeName))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Tổ chức"));

            //if (item.EstablishedDTG == null)
            //    lstErr.Add(string.Format(Messages.FieldNotEmpty, "Ngày thành lập"));

            if (string.IsNullOrWhiteSpace(item.Email))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Email"));

            if (string.IsNullOrWhiteSpace(item.Phone))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Điện thoại"));

            if (string.IsNullOrWhiteSpace(item.Agency))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Cơ quan chủ quản"));

            if (string.IsNullOrWhiteSpace(item.Address))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Địa chỉ"));

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        public void ValidateAgencyType(AgencyType agencyType)
        {
            List<string> lstErr = new List<string>();
            if (string.IsNullOrWhiteSpace(agencyType.TypeName))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Tên loại tổ chức"));
            if (string.IsNullOrWhiteSpace(agencyType.Code))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Mã tổ chức"));
        }

        public void ValidatePressAgencyHR(agency_PressAgencyHR item)
        {
            List<string> lstErr = new List<string>();

            if (string.IsNullOrWhiteSpace(item.FullName))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Họ và tên"));

            if (string.IsNullOrWhiteSpace(item.Position))
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Chức danh"));

            if (item.Attitude == null)
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Thái độ"));

            if (item.DOB == null)
                lstErr.Add(string.Format(Messages.FieldNotEmpty, "Ngày sinh"));

            if (lstErr.Count > 0)
                throw new SMXException(lstErr);
        }

        public void GetListOtherImage_ByPressAgencyID(PressAgencyParam param)
        {
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();

            param.ListOtherImage = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = param.PressAgency?.PressAgencyID,
                RefType = SMX.AttachmentRefType.PressAgencyOtherImage
            });
        }

        public void LoadDataOtherImage_PressAgencyHR(PressAgencyParam param)
        {
            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();

            param.ListAttachment = bizECM.GetListAttachmentByRefIDAndRefType(new adm_Attachment()
            {
                RefID = param.PressAgencyHR?.PressAgencyHRID,
                RefType = SMX.AttachmentRefType.PressAgencyHROtherImage
            });
        }

        public void SearchItemsForViewPressAgencyHR(PressAgencyParam param)
        {
            var paHR = param.PressAgencyHR;
            var paHRHistory = param.PressAgencyHRHistory;
            var paHRRelatives = param.PressAgencyHRRelatives;

            var lstSearchHR = _dao.SearchPressAgencyHR(paHR);

            var lstPAHRHistory = _dao.SearchPressAgencyHRHistory_GetPressAgencyHRID(paHRHistory);

            var lstPAHRRelatives = _dao.SearchPressAgencyHRRelatives_GetPressAgencyHRID(paHRRelatives);

            var lstPAHR = lstSearchHR.Where(x => lstPAHRHistory.Exists(c => c.PressAgencyHRID == x.PressAgencyHRID)
                                            && lstPAHRRelatives.Exists(c => c.PressAgencyHRID == x.PressAgencyHRID)).OrderBy(x => x.PressAgencyName).ToList();

            var pagingInfo = param.PagingInfo;
            pagingInfo.RecordCount = lstPAHR.Count;
            param.ListPressAgencyHR = lstPAHR.Skip(pagingInfo.PageIndex * pagingInfo.PageSize).Take(pagingInfo.PageSize).ToList();

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();
            foreach (var hr in param.ListPressAgencyHR)
            {
                try
                {
                    hr.Attachment = bizECM.GetAttachmentByRefIDAndRefType(new adm_Attachment()
                    {
                        RefID = hr.PressAgencyHRID,
                        RefType = SMX.AttachmentRefType.PressAgencyHR
                    });
                }
                catch
                {
                    hr.Attachment = null;
                }
            }
        }

        public void GetListAgencyType(PressAgencyParam param)
        {
            param.ListAgencyType = _dao.GetListAgencyType(param);
        }
    }
}