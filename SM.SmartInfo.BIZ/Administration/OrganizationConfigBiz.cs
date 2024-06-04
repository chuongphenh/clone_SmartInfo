using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SM.SmartInfo.SharedComponent.Entities;
using SoftMart.Core.Dao;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.BIZ.Commons;
using SM.SmartInfo.DAO.Commons;

namespace SM.SmartInfo.BIZ.Administration
{
    class OrganizationConfigBiz : BizBase, ISMFormAddNewBiz<OrganizationParam>, ISMFormDisplayBiz<OrganizationParam>, ISMFormEditBiz<OrganizationParam>
    {
        private OrganizationConfigDao _dao = new SM.SmartInfo.DAO.Administration.OrganizationConfigDao();

        #region AddNew

        public void SetupAddNewForm(OrganizationParam param)
        {
            SystemParameterDao daoSP = new SystemParameterDao();
            param.Zones = daoSP.GetAllActiveSystemParametersByFeatureID(SMX.Features.smx_Zone);

            param.Committees = _dao.GetAllCommittees();
        }

        public void AddNewItem(OrganizationParam param)
        {
            ValidateItem(param);

            #region Prepare system data

            Organization item = param.Organization;
            item.Deleted = SMX.smx_IsNotDeleted;
            item.Version = SMX.smx_FirstVersion;
            item.CreatedBy = SM.SmartInfo.CacheManager.Profiles.MyProfile.UserName;
            item.CreatedDTG = DateTime.Now;

            List<OrganizationEmployee> lstOrgEm = CreateOrganizationEmployeeObject(param.EmployeeIDs);
            List<OrganizationManager> lstOrgMng = CreateOrganizationManagerObject(param.ManagerIDs);
            #endregion

            // Validate in database
            ValidateOrganizationInDatabase(param);

            // Save
            using (TransactionScope trans = new TransactionScope())
            {
                //Insert Organization
                item.AutomationType = (item.RuleID.HasValue ? SMX.AutomationType.Organization : 0) +
                    (item.DispatchEmployeeRuleID.HasValue ? SMX.AutomationType.Employee : 0);
                _dao.InsertOrganization(item);
                int orgID = item.OrganizationID.Value;

                //Insert OrganizationEmployee
                lstOrgEm.ForEach(c => c.OrganizationID = orgID);
                _dao.InsertOrganizationEmployee(lstOrgEm);

                //Insert OrganizationManager
                lstOrgMng.ForEach(c => c.OrganizationID = orgID);
                _dao.InsertOrganizationManager(lstOrgMng);

                trans.Complete();
            }

            GlobalCache.UpdateOrganization(item);
        }

        private List<OrganizationEmployee> CreateOrganizationEmployeeObject(List<int> lstEmpID)
        {
            string userName = Profiles.MyProfile.UserName;
            List<OrganizationEmployee> lstOrgEm = new List<OrganizationEmployee>();

            foreach (var empID in lstEmpID)
            {
                OrganizationEmployee orgEmp = new OrganizationEmployee();
                orgEmp.EmployeeID = empID;
                orgEmp.CreatedBy = userName;
                orgEmp.CreatedDTG = DateTime.Now;
                orgEmp.Deleted = SMX.smx_IsNotDeleted;
                lstOrgEm.Add(orgEmp);
            }

            return lstOrgEm;
        }

        private List<OrganizationManager> CreateOrganizationManagerObject(List<int> lstMngID)
        {
            string userName = Profiles.MyProfile.UserName;
            List<OrganizationManager> lstOrgEm = new List<OrganizationManager>();

            foreach (var empID in lstMngID)
            {
                OrganizationManager orgEmp = new OrganizationManager();
                orgEmp.EmployeeID = empID;
                orgEmp.CreatedBy = userName;
                orgEmp.CreatedDTG = DateTime.Now;
                orgEmp.Deleted = SMX.smx_IsNotDeleted;
                lstOrgEm.Add(orgEmp);
            }

            return lstOrgEm;
        }

        public void ValidateItem(OrganizationParam param)
        {
            Organization item = param.Organization;
            List<string> lstMsg = new List<string>();

            bool isAddNew = item.OrganizationID == null;

            if (string.IsNullOrWhiteSpace(item.Code))
            {
                lstMsg.Add(Messages.Organization.RequiredOrgCode);
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                lstMsg.Add(Messages.Organization.RequiredOrgName);
            }

            //if (!string.IsNullOrWhiteSpace(item.OfficeID))
            //{
            //    OfficeDAO daoOffice = new OfficeDAO();
            //    Office office = daoOffice.GetOfficeByID(item.OfficeID);
            //    if (office == null)
            //    {
            //        lstMsg.Add("Mã chi nhánh không tồn tại");
            //    }
            //}

            // Summary validated results
            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        /// <summary>
        /// Validate organization before inserting/updating
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="param"></param>
        /// <param name="isEdit"></param>
        private void ValidateOrganizationInDatabase(OrganizationParam param, bool isEdit = false)
        {
            Organization item = param.Organization;
            List<string> lstMsg = new List<string>();

            // Validate Code
            bool isDuplicatedCode = _dao.CheckDuplicatedCode(item.OrganizationID, item.Code);
            if (isDuplicatedCode)
                lstMsg.Add(Messages.Organization.DuplicateCode);

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        #endregion

        #region Display

        public void LoadDataDisplay(OrganizationParam param)
        {
            int orgID = param.OrganizationID;

            // Get organization info
            Organization item = _dao.GetOrganizationInfoByID(orgID);
            if (item == null)
                throw new SMXException(Messages.ItemNotExisted);

            param.Organization = item;

            // Get list Employee, Manager in organization
            param.EmployeeInfos = _dao.GetEmployeeInOrganizationByOrgID(orgID);
            param.ManagerInfos = _dao.GetManagerInOrganizationByOrgID(orgID);

            //// get Office
            //if (!string.IsNullOrWhiteSpace(item.OfficeID))
            //{
            //    OfficeDAO daoOffice = new OfficeDAO();
            //    param.Office = daoOffice.GetOfficeByID(item.OfficeID);
            //}
        }

        public void DeleteItems(OrganizationParam param)
        {
            Organization item = param.Organization;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;
            item.Deleted = SMX.smx_IsDeleted;

            int orgID = item.OrganizationID.Value;

            // Validate
            int count = _dao.GetChildrenCount(orgID);
            if (count > 0)
                throw new SMXException(Messages.Organization.CannotDeleteIsParent);
            ValidateOrganizationInCharged(orgID);

            // Delete
            using (TransactionScope trans = new TransactionScope())
            {
                _dao.UpdateOrganization(item);
                //_dao.DeleteOrganizationFlowByID(orgID);
                _dao.DeleteOrganizationEmployee(orgID);

                trans.Complete();
            }
        }

        private void ValidateOrganizationInCharged(int organizationID)
        {
            // kiem tra xem phong co dang incharge De nghi, Dinh gia, KSCL nao ko
        }

        #endregion

        #region Edit

        public void SetupEditForm(OrganizationParam param)
        {
            SetupAddNewForm(param);
        }

        public void LoadDataEdit(OrganizationParam param)
        {
            LoadDataDisplay(param);
        }

        public void UpdateItem(OrganizationParam param)
        {
            // Validate
            ValidateItem(param);

            // Prepare system data
            Organization item = param.Organization;
            item.UpdatedBy = Profiles.MyProfile.UserName;
            item.UpdatedDTG = DateTime.Now;

            List<OrganizationEmployee> lstOrgEm = CreateOrganizationEmployeeObject(param.EmployeeIDs);
            List<OrganizationManager> lstOrgMng = CreateOrganizationManagerObject(param.ManagerIDs);

            DataTrackingBiz dataTrackingBiz = new DataTrackingBiz();
            string logContent = string.Empty;
            UserDao userDao = new UserDao();
            logContent += Environment.NewLine + "Danh sách chuyên viên: ";
            foreach (OrganizationEmployee orgEm in lstOrgEm)
            {
                Employee em = _dao.GetItemByID<Employee>(orgEm.EmployeeID.Value);
                if (em != null)
                    logContent += em.Username + ", ";
            }
            logContent += Environment.NewLine + "Danh sách Quản lý: ";
            foreach (OrganizationManager orgMan in lstOrgMng)
            {
                Employee em = _dao.GetItemByID<Employee>(orgMan.EmployeeID.Value);
                if (em != null)
                    logContent += em.Username + ", ";
            }

            int orgID = item.OrganizationID.Value;

            //Validate on database
            ValidateOrganizationInDatabase(param, true);

            Organization oldItem = _dao.GetItemByID<Organization>(item.OrganizationID);
            bool isNeedLoadCache = false;
            if (oldItem != null && (oldItem.ParentID != item.ParentID || oldItem.Name != item.Name))
                isNeedLoadCache = true;
            bool isNeedUpdateProvince = (item.Province.HasValue && item.Province != oldItem.Province) &&
                (item.Type == SMX.OrganizationType.ManagementUnit);

            // Save
            using (TransactionScope trans = new TransactionScope())
            {
                //Update Organization
                item.AutomationType = (item.RuleID.HasValue ? SMX.AutomationType.Organization : 0) +
                    (item.DispatchEmployeeRuleID.HasValue ? SMX.AutomationType.Employee : 0);
                _dao.UpdateOrganization(item);

                //Insert OrganizationEmployee
                lstOrgEm.ForEach(c => c.OrganizationID = orgID);
                _dao.DeleteOrganizationEmployee(orgID);
                _dao.InsertOrganizationEmployee(lstOrgEm);

                //Insert OrganizationManager
                lstOrgMng.ForEach(c => c.OrganizationID = orgID);
                _dao.DeleteOrganizationManager(orgID);
                _dao.InsertOrganizationManager(lstOrgMng);

                trans.Complete();
            }

            dataTrackingBiz.LogDataTracking(SMX.DataTracking.Feature.Administrator, SMX.DataTracking.ActionType.Edit,
                    "cơ cấu tổ chức", logContent, SMX.DataTracking.RefType.AdminOrganizationStructure, orgID);

            if (isNeedLoadCache)
                GlobalCache.UpdateOrganization(item);

        }

        #endregion

        public void GetOrganizationTreeData(OrganizationParam param)
        {
            param.Organizations = _dao.GetAllShortOrganization();
        }

        public void ValidateEmployeeIsInOtherOrganization(OrganizationParam param)
        {
            OrganizationEmployee orgEmp = param.OrganizationEmployee;

            bool isValid = _dao.ValidateEmployeeIsInOtherOrganization(orgEmp.EmployeeID.Value, orgEmp.OrganizationID);

            if (!isValid)
                throw new SMXException("Chuyên viên đã thuộc phòng khác.");
        }

        public void GetListEmployeeByOrganizationID(OrganizationParam param)
        {
            int? orgID = param.Organization.OrganizationID;
            if (orgID.HasValue)
            {
                DAO.Commons.OrganizationDAO daoOrg = new DAO.Commons.OrganizationDAO();
                param.ListEmployee = daoOrg.GetListEmployeeByOrganizationID(orgID.Value);
            }
        }
    }
}