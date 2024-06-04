using System;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Notification;
using SM.SmartInfo.Utils;

namespace SM.SmartInfo.BIZ.Administration
{
    /// <summary>
    /// Thêm xóa sửa view cho 1 loại FeatureID dữ liệu đơn giản, không liên kết
    /// </summary>
    abstract class SystemParameterCRUDBaseBiz : BizBase
    {
        #region Members
        protected int _featureID;
        protected string _msgCodeExisted;
        protected string _msgCodeEmpty;
        protected string _msgNameEmpty;
        protected SystemParameterConfigDao _dao = new SystemParameterConfigDao();
        protected NotificationDao _daoNoti = new NotificationDao();

        protected SystemParameterCRUDBaseBiz(int featureID,
                string msgCodeExisted = "Mã đã tồn tại",
                string msgCodeEmpty = "Bạn chưa nhập [Mã]",
                string msgNameEmpty = "Bạn chưa nhập [Tên]")
        {
            _featureID = featureID;
            _msgCodeExisted = msgCodeExisted;
            _msgCodeEmpty = msgCodeEmpty;
            _msgNameEmpty = msgNameEmpty;
        }
        #endregion

        #region Add New
        public virtual void SetupAddNewForm(SystemParameterParam param)
        {

        }

        public virtual void AddNewItem(SystemParameterParam param)
        {
            //1. Validate Item
            ValidateItem(param);

            //2. Prepare data
            SystemParameter item = param.SystemParameter;
            item.Deleted = SMX.smx_IsNotDeleted;
            item.Version = SMX.smx_FirstVersion;
            item.CreatedDTG = DateTime.Now;
            item.CreatedBy = Profiles.MyProfile.UserName;

            _dao.InsertSystemParameter(item);

            ReloadGlobalCache(item);
        }

        protected virtual void ValidateItemDetail(SystemParameter item, List<string> lstMsg) { }
        public virtual void ValidateItem(SystemParameterParam param)
        {
            SystemParameter item = param.SystemParameter;
            List<string> lstMsg = new List<string>();

            if (item.SystemParameterID == null) // add new
            {
                if (string.IsNullOrWhiteSpace(item.Code))
                    lstMsg.Add(_msgCodeEmpty);

                ValidateCodeUnique(item);
            }

            if (string.IsNullOrWhiteSpace(item.Name))
                lstMsg.Add(_msgNameEmpty);

            ValidateItemDetail(item, lstMsg);

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        private void ValidateCodeUnique(SystemParameter item)
        {
            List<string> lstMsg = new List<string>();

            bool isExsistedCode = _dao.CheckCodeExist(item.Code, item.SystemParameterID, _featureID);
            if (isExsistedCode)
                lstMsg.Add(_msgCodeExisted);

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }
        #endregion

        #region Display
        public void LoadDataDisplay(SystemParameterParam param)
        {
            int? id = param.SystemParameter.SystemParameterID;

            param.SystemParameter = _dao.GetItemByID<SystemParameter>(id);
        }

        public virtual void DeleteItem(SystemParameter item)
        {
            SystemParameter itemDelete = _dao.GetSystemParameterByID(item.SystemParameterID.Value);
            if (itemDelete.Status == SMX.Status.Active)
                throw new SMXException("Bản ghi đang được sử dụng không được phép xóa !");

            item.Deleted = SMX.smx_IsDeleted;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            _dao.UpdateSystemParameter(item);

            ReloadGlobalCache(item);
        }
        #endregion

        #region Edit
        public virtual void SetupEditForm(SystemParameterParam param)
        {
        }

        public virtual void LoadDataEdit(SystemParameterParam param)
        {
            int? id = param.SystemParameter.SystemParameterID;
            param.SystemParameter = _dao.GetItemByID<SystemParameter>(id);
        }

        public virtual void UpdateItem(SystemParameterParam param)
        {
            //1. Validate Item
            ValidateItem(param);

            //2. Prepare data
            SystemParameter item = param.SystemParameter;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;


            //xoá sự kiện tương ứng khi Cấu hình sự kiện có trạng thái không sử dụng
            //if (item.Status == 1)
            //    _daoNoti.DeleteNotificationByNameLike(item.Name, "0");
            //else
            //    _daoNoti.DeleteNotificationByNameLike(item.Name, "1");
            try
            {
                LogManager.WebLogger.LogDebug("Start Cập nhật Ngày truyền thống ---", null);
                var currentHr = _dao.GetSystemParameterByID((int)item.SystemParameterID);
                if ((currentHr != null && currentHr.Ext4 != null && item.Ext4 != currentHr.Ext4) || item.Status == 2)  _daoNoti.DeleteNotificationByHRAlertID((int)item.SystemParameterID);
                LogManager.WebLogger.LogDebug("End Cập nhật Ngày truyền thống: ", null);
            }
            catch (Exception ex)
            {
                LogManager.WebLogger.LogDebug($"Cập nhật ngày truyền thống: {ex.Message}", null);
            }
            finally
            {
                //3. Add to Database
                _dao.UpdateSystemParameter(item);

                ReloadGlobalCache(item);
            }

        }
        #endregion

        #region View
        public virtual void SetupViewForm(SystemParameterParam param)
        {
            SearchItemsForView(param);
        }

        public virtual void DeleteItems(SystemParameterParam param)
        {
            foreach (var item in param.SystemParameters)
            {
                try
                {
                    DeleteItem(item);
                    try
                    {
                        LogManager.WebLogger.LogDebug("Start xóa Ngày truyền thống ---", null);
                        _daoNoti.DeleteNotificationByHRAlertID((int)item.SystemParameterID);
                        LogManager.WebLogger.LogDebug("End xóa Ngày truyền thống: ", null);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogDebug($"Cập nhật Ngày kỉ niêm truyền thống: {ex.Message}", null);
                    }
                }
                catch (SMXException ex)
                {
                    throw ex;
                }
            }
        }

        public virtual void SearchItemsForView(SystemParameterParam param)
        {
            _dao.SearchSystemParameter(param);
        }
        #endregion
    }
}
