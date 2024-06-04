using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;

using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;

namespace SM.SmartInfo.BIZ.Administration
{
    class RoleConfigBiz : BizBase, ISMFormCRUDBiz<RoleParam>
    {
        RoleDao _dao = new RoleDao();

        #region AddNew
        public void SetupAddNewForm(RoleParam param)
        {
            throw new NotImplementedException();
        }

        public void AddNewItem(RoleParam param)
        {

            //01.Validate data
            ValidateItem(param);

            //02.Insert Item
            Role item = param.Role;

            item.Deleted = SMX.smx_IsNotDeleted;
            item.Version = SMX.smx_FirstVersion;
            item.CreatedDTG = DateTime.Now;
            item.CreatedBy = Profiles.MyProfile.UserName;

            CheckExitsName(item.Name, null);

            _dao.InsertItem(item);

        }

        private void CheckExitsName(string name, int? id)
        {
            bool checkName = _dao.CheckExitsName(name, id);
            if (checkName)
                throw new SMXException("Vai trò đã tồn tại.");
        }

        public void ValidateItem(RoleParam param)
        {
            Role item = param.Role;

            List<string> lstMsg = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Name))
                lstMsg.Add("[Tên vai trò] không được để trống.");

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        #endregion

        #region Edit

        public void SetupEditForm(RoleParam param)
        {
            param.Role = _dao.GetItemByID<Role>(param.RoleId);
        }

        public void LoadDataEdit(RoleParam param)
        {

        }

        public void ExportExcel(RoleParam param)
        {
            param.DataTable = _dao.GetRolePermission();
        }

        public void UpdateItem(RoleParam param)
        {
            //01. Validate Item
            ValidateItem(param);

            //2. Prepare data
            Role item = param.Role;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            CheckExitsName(item.Name, item.RoleID);

            //3. Update
            _dao.UpdateItem(item);
        }

        private void DeleteItem(Role item)
        {
            item.Deleted = SMX.smx_IsDeleted;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            if (item.Status == SMX.Status.Active)
            {
                throw new SMXException(string.Format("Bản ghi đang được sử dụng không được phép xóa !"));
            }
            _dao.UpdateItem(item);
        }

        public void DeleteItems(RoleParam param)
        {
            foreach (var item in param.Roles)
            {
                DeleteItem(item);
            }
        }

        #endregion

        #region Display

        public void LoadDataDisplay(RoleParam param)
        {
            param.Role = _dao.GetItemByID<Role>(param.RoleId);
        }

        #endregion

        #region View

        public void SetupViewForm(RoleParam param)
        {
            throw new NotImplementedException();

        }

        public void SearchItemsForView(RoleParam param)
        {
            param.Roles = _dao.SearchRole(param);

            foreach (var item in param.Roles)
            {
                item.StatusName = Utils.Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            }
        }

        #endregion

        public void GetAllRole(RoleParam param)
        {
            param.Roles = _dao.GetAllActiveRole();
        }
        public void GetAllActiveRoleExceptQTHT(RoleParam param)
        {
            param.Roles = _dao.GetAllActiveRoleExceptQTHT();
        }
        public void GetListRoleIDByPressAgencyHRID(RoleParam param)
        {
            param.RoleIDs = _dao.GetListRoleIDByPressAgencyHRID(param.PressAgencyHRID);
        }
    }
}
