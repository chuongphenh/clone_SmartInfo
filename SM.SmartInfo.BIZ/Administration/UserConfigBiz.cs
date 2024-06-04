using System;
using System.Collections.Generic;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;
using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.BIZ.Commons;
using System.Linq;
using SM.SmartInfo.DAO.Commons;

namespace SM.SmartInfo.BIZ.Administration
{
    //Standard Admin
    //Class nay khong quan tam item Active hay InActive
    class UserConfigBiz : BizBase, ISMFormCRUDBiz<UserParam>
    {
        private UserConfigDao _dao = new UserConfigDao();
        private EmployeeDao _daoEmp = new EmployeeDao();
        private RoleDao _daoRole = new RoleDao();
        private OrganizationDAO _daoOrg = new OrganizationDAO();

        #region AddNew
        public void SetupAddNewForm(UserParam param)
        {
            RoleDao roleDao = new RoleDao();
            param.Roles = roleDao.GetAllActiveRole();

            OrganizationConfigDao daoOrg = new OrganizationConfigDao();
            var lstOrg = daoOrg.GetAllShortOrganization();
            param.ListBranchCode = (from c in lstOrg where !string.IsNullOrWhiteSpace(c.OfficeID) select c.OfficeID).Distinct().ToList();
        }

        public void AddNewItem(UserParam param)
        {
            ValidateItem(param);

            DateTime now = DateTime.Now;
            string userName = Profiles.MyProfile.UserName;

            //Employee
            Employee emp = param.Employee;
            emp.Deleted = SMX.smx_IsNotDeleted;
            emp.Version = SMX.smx_FirstVersion;
            emp.CreatedBy = userName;
            emp.CreatedDTG = now;

            List<OrganizationEmployee> lstOrgEmp = new List<OrganizationEmployee>();
            List<OrganizationManager> lstOrgMgr = new List<OrganizationManager>();
            CalculateListOrganizationEmployee(emp.ListOrganizationEmployee, emp.ListOrganizationManager, userName, lstOrgEmp, lstOrgMgr);

            //EmployeeRole
            List<EmployeeRole> lstEmpRole = param.EmployeeRoles;

            DataTrackingBiz dataTrackingBiz = new DataTrackingBiz();
            string logContent = string.Empty;
            RoleDao _daoRole = new RoleDao();
            List<Role> lstRoles = _daoRole.GetAllActiveRole().Where(r => lstEmpRole.Any(er => er.RoleID == r.RoleID)).ToList();
            logContent += " Danh sách role: ";
            logContent += string.Join(", ", lstRoles.Select(r => r.Name));
            logContent += "; Chi nhánh: " + (string.IsNullOrWhiteSpace(emp.ListBranchCode) ? "" : emp.ListBranchCode);

            /*old 
             * 
             *  using (TransactionScope tran = new TransactionScope())
            {
                _dao.InsertItem(emp);

                if (lstEmpRole != null && lstEmpRole.Count > 0)
                {
                    lstEmpRole.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstEmpRole);
                }

                if (lstOrgEmp != null && lstOrgEmp.Count > 0)
                {
                    lstOrgEmp.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstOrgEmp);
                }

                if (lstOrgMgr != null && lstOrgMgr.Count > 0)
                {
                    lstOrgMgr.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstOrgMgr);
                }

                if (param.EmployeeImage != null && param.EmployeeImage.SignImage != null)
                {
                    param.EmployeeImage.EmployeeID = emp.EmployeeID;
                    _dao.InsertItem(param.EmployeeImage);
                }

                tran.Complete();
            }
             * code*/


            using (TransactionScope tran = new TransactionScope())
            {
                int empID = _dao.InsertEmployeeAndGetID(emp);
                param.Employee.EmployeeID = empID;
                if (lstEmpRole != null && lstEmpRole.Count > 0)
                {
                    lstEmpRole.ForEach(c => c.EmployeeID = empID);
                    _dao.InsertItems(lstEmpRole);
                }

                if (lstOrgEmp != null && lstOrgEmp.Count > 0)
                {
                    lstOrgEmp.ForEach(c => c.EmployeeID = empID);
                    _dao.InsertItems(lstOrgEmp);
                }

                if (lstOrgMgr != null && lstOrgMgr.Count > 0)
                {
                    lstOrgMgr.ForEach(c => c.EmployeeID = empID);
                    _dao.InsertItems(lstOrgMgr);
                }

                if (param.EmployeeImage != null && param.EmployeeImage.SignImage != null)
                {
                    param.EmployeeImage.EmployeeID = empID;
                    _dao.InsertItem(param.EmployeeImage);
                }

                tran.Complete();
            }

            string actionName = "người dùng " + emp.Username;
            dataTrackingBiz.LogDataTracking(SMX.DataTracking.Feature.Administrator, SMX.DataTracking.ActionType.AddNew,
                actionName, logContent, SMX.DataTracking.RefType.AdminUser, emp.EmployeeID);

            GlobalCache.UpdateEmployee(emp);
        }

        public void ValidateItem(UserParam param)
        {
            Employee employee = param.Employee;

            List<string> lstMsg = new List<string>();

            if (!string.IsNullOrEmpty(employee.Email) && !string.IsNullOrEmpty(Utility.ValidateEmailAddress(employee.Email)))
                lstMsg.Add("Bạn nhập sai định dạng email.");

            if (string.IsNullOrEmpty(employee.Username))
                lstMsg.Add("[Tên đăng nhập] trống");

            if (string.IsNullOrEmpty(employee.Name))
                lstMsg.Add("[Tên đầy đủ] trống");

            if (string.IsNullOrEmpty(employee.Mobile))
                lstMsg.Add("[Số Mobile] trống");

            if (string.IsNullOrEmpty(employee.EmployeeCode))
                lstMsg.Add("[Mã nhân viên] trống");
            if (string.IsNullOrEmpty(employee.Email))
                lstMsg.Add("[Email] trống");
            if (string.IsNullOrEmpty(employee.Description))
                lstMsg.Add("[Chức danh] trống");

            if (employee.EmployeeID == null)
            {
                bool isUserNameDuplicated = _dao.CheckUserNameExist(employee.Username);
                if (isUserNameDuplicated)
                    lstMsg.Add("[Tên đăng nhập] đã tồn tại trong hệ thống");
            }

            if (param.EmployeeImage != null && param.EmployeeImage.SignImage != null)
            {
                bool isImage = FileTypeHelper.IsImage(param.EmployeeImage.SignImage);
                if (!isImage)
                    lstMsg.Add("File hình ảnh chữ ký chỉ chấp nhận định dạng ảnh.");
            }

            if (lstMsg.Count > 0)
                throw new SMXException(lstMsg);
        }

        private void CalculateListOrganizationEmployee(List<Organization> lstNewOrgEmp, List<Organization> lstNewOrgMgr,
            string createdBy, List<OrganizationEmployee> lstOrgEmp, List<OrganizationManager> lstOrgMgr)
        {
            if (lstNewOrgEmp != null && lstNewOrgEmp.Count > 0)
            {
                List<Organization> lstOrg = new List<Organization>();
                if (lstNewOrgEmp.Exists(c => c.OrganizationID == 0)) // all branch
                    lstOrg = GlobalCache.GetAllShortOrganization().Where(c => !string.IsNullOrWhiteSpace(c.OfficeID)).ToList();
                else
                    lstOrg = lstNewOrgEmp;

                foreach (Organization org in lstOrg)
                {
                    lstOrgEmp.Add(new OrganizationEmployee()
                    {
                        CreatedBy = createdBy,
                        CreatedDTG = DateTime.Now,
                        Deleted = SMX.smx_IsNotDeleted,
                        OrganizationID = org.OrganizationID
                    });
                }
            }

            if (lstNewOrgMgr != null && lstNewOrgMgr.Count > 0)
            {
                List<Organization> lstOrg = new List<Organization>();
                if (lstNewOrgMgr.Exists(c => c.OrganizationID == 0)) // all branch
                    lstOrg = GlobalCache.GetAllShortOrganization().Where(c => !string.IsNullOrWhiteSpace(c.OfficeID)).ToList();
                else
                    lstOrg = lstNewOrgMgr;

                foreach (Organization org in lstOrg)
                {
                    lstOrgMgr.Add(new OrganizationManager()
                    {
                        CreatedBy = createdBy,
                        CreatedDTG = DateTime.Now,
                        Deleted = SMX.smx_IsNotDeleted,
                        OrganizationID = org.OrganizationID
                    });
                }
            }
        }
        #endregion

        #region Display

        public void SearchUserForSharing(UserParam param)
        {
            param.Employee = _dao.SearchUserForSharing(param);
        }

        public void ShareToStaff(UserParam param)
        {
            _dao.GetListUserForSharing(param);

            if (param.Employees.Count > 0)
            {
                _dao.InsertOrUpdateSharingManagement(param);
            }
        }

        public void SearchListUserForSharing(UserParam param)
        {
            param.Employees = _dao.SearchListUserForSharing(param);
        }
        public void LoadDataDisplay(UserParam param)
        {
            int employeeID = param.EmployeeId.Value;

            Employee employee = _dao.GetItemByID<Employee>(employeeID);
            if (employee == null)
                throw new SMXException(Messages.ItemNotExisted);

            DAO.Commons.OrganizationDAO daoOrg = new DAO.Commons.OrganizationDAO();
            List<OrganizationEmployee> lstOrgEmp = daoOrg.GetOrganizationOfEmployee(employeeID);
            List<int?> lstOrgEmpID = lstOrgEmp.Select(c => c.OrganizationID).Distinct().ToList();
            List<Organization> lstOrgEmpName = new List<Organization>();
            foreach (var orgID in lstOrgEmpID)
            {
                var org = GlobalCache.GetOrganizationByID(orgID);
                if (org != null)
                {
                    var orgParent = GlobalCache.GetOrganizationByID(org.ParentID);
                    if (orgParent != null)
                        lstOrgEmpName.Add(new Organization() { OrganizationID = orgID, Name = string.Format("{0} > {1}", orgParent.Name, org.Name) });
                    else
                        lstOrgEmpName.Add(org);
                }
            }
            employee.ListOrganizationEmployee = lstOrgEmpName;

            List<OrganizationManager> lstOrgMgr = daoOrg.GetOrganizationOfManager(employeeID);
            List<int?> lstOrgMgrID = lstOrgMgr.Select(c => c.OrganizationID).Distinct().ToList();
            List<Organization> lstOrgMgrName = new List<Organization>();
            foreach (var orgID in lstOrgMgrID)
            {
                var org = GlobalCache.GetOrganizationByID(orgID);
                if (org != null)
                {
                    var orgParent = GlobalCache.GetOrganizationByID(org.ParentID);
                    if (orgParent != null)
                        lstOrgMgrName.Add(new Organization() { OrganizationID = orgID, Name = string.Format("{0} > {1}", orgParent.Name, org.Name) });
                    else
                        lstOrgMgrName.Add(org);
                }
            }
            employee.ListOrganizationManager = lstOrgMgrName;

            //Phong dang quan ly: Ko lay
            RoleDao daoRole = new RoleDao();
            param.Employee = employee;
            param.UserRoles = _dao.GetEmployeeRoleByEmployeeID(employeeID);
            param.Roles = daoRole.GetAllActiveRole();

            param.EmployeeImage = _dao.GetEmployeeImageByEmployeeID(employeeID);
        }

        public void LoadDataDisplayForReport(UserParam param)
        {
            EmployeeDao daoEmp = new EmployeeDao();
            var lstEmp = daoEmp.LoadDataDisplayForReport();
            var lstOrg = daoEmp.GetListOrganization();

            // update Organiza
            foreach (Organization org in lstOrg)
            {
                if (!lstOrg.Exists(c => c.ParentID == org.OrganizationID))
                {
                    var parent = lstOrg.FirstOrDefault(c => c.OrganizationID == org.ParentID);
                    if (parent != null)
                        org.Name = parent.Name + " > " + org.Name;
                }
            }

            foreach (var item in lstEmp)
            {
                var org = lstOrg.FirstOrDefault(c => c.OrganizationID == item.OrganizationID);
                if (org != null)
                {
                    item.OrganizationName = org.Name;
                    item.ListBranchCode = org.OfficeID;
                }
                item.SectorName = GlobalCache.GetNameByID(item.Sector);
                item.GenderName = Utility.GetDictionaryValue(SMX.dicGender, item.Gender);
                item.StatusName = Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
                item.LevelName = GlobalCache.GetNameByID(item.Level);

                if (item.IsManager.HasValue)
                    item.PositionName = (item.IsManager == true) ? "Quản lý" : "Chuyên viên";

                item.AuthorizationTypeName = Utility.GetDictionaryValue(SMX.dicAuthenticationType, item.AuthorizationType);
            }

            param.Employees = lstEmp;
        }

        private void DeleteItem(Employee employee)
        {
            employee.Deleted = SMX.smx_IsDeleted;
            employee.UpdatedBy = Profiles.MyProfile.UserName;
            employee.UpdatedDTG = DateTime.Now;

            using (TransactionScope trans = new TransactionScope())
            {
                //Employee must InActive
                bool isActive = _dao.CheckUserActive(employee.EmployeeID.Value);
                if (isActive)
                    throw new SMXException(Messages.Users.UserIsUing);

                //Delete Employee
                _dao.UpdateItem(employee, true);

                //Delete OrganizationEmployee
                _dao.DeleteOrganizationEmployee(employee.EmployeeID.Value);

                trans.Complete();
            }
        }

        public void UpdateIsLockedOpen(UserParam param)
        {
            Employee emp = param.Employee;
            emp.Deleted = SMX.smx_IsNotDeleted;
            emp.UpdatedBy = Profiles.MyProfile.UserName;
            emp.UpdatedDTG = DateTime.Now;
            _dao.UpdateItem(emp);
        }

        #endregion

        #region Edit
        public void SetupEditForm(UserParam param)
        {

        }

        public void LoadDataEdit(UserParam param)
        {
            LoadDataDisplay(param);

            SetupAddNewForm(param);
        }

        public void UpdateItem(UserParam param)
        {
            ValidateItem(param);

            DateTime now = DateTime.Now;
            string userName = Profiles.MyProfile.UserName;

            //Employee
            Employee emp = param.Employee;
            emp.UpdatedBy = userName;
            emp.UpdatedDTG = now;

            //EmployeeRole
            List<EmployeeRole> lstEmpRole = param.EmployeeRoles;

            List<OrganizationEmployee> lstOrgEmp = new List<OrganizationEmployee>();
            List<OrganizationManager> lstOrgMgr = new List<OrganizationManager>();
            CalculateListOrganizationEmployee(emp.ListOrganizationEmployee, emp.ListOrganizationManager, userName, lstOrgEmp, lstOrgMgr);

            // xoa phong
            List<int?> lstDeletedOrgEmpID = emp.ListDeletedOrganizationEmployeeID;
            lstDeletedOrgEmpID.RemoveAll(c => lstOrgEmp.Exists(d => d.OrganizationID == c));

            List<int?> lstDeletedMgrEmpID = emp.ListDeletedOrganizationManagerID;
            lstDeletedMgrEmpID.RemoveAll(c => lstOrgMgr.Exists(d => d.OrganizationID == c));

            //data trackng
            DataTrackingBiz dataTrackingBiz = new DataTrackingBiz();
            string logContent = string.Empty;
            RoleDao _daoRole = new RoleDao();
            List<Role> lstRoles = _daoRole.GetAllActiveRole().Where(r => lstEmpRole.Any(er => er.RoleID == r.RoleID)).ToList();
            logContent += " Danh sách role: ";
            logContent += string.Join(", ", lstRoles.Select(r => r.Name));
            logContent += "; Chi nhánh: " + (string.IsNullOrWhiteSpace(emp.ListBranchCode) ? "" : emp.ListBranchCode);

            using (TransactionScope trans = new TransactionScope())
            {
                _dao.UpdateItem(emp, true);

                _dao.DeleteEmployeeRole(emp.EmployeeID.Value);
                if (lstEmpRole != null && lstEmpRole.Count > 0)
                {
                    lstEmpRole.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstEmpRole);
                }

                _dao.DeleteOrganizationEmployee(emp.EmployeeID, lstDeletedOrgEmpID);
                if (lstOrgEmp != null && lstOrgEmp.Count > 0)
                {
                    lstOrgEmp.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstOrgEmp);
                }

                _dao.DeleteOrganizationManager(emp.EmployeeID, lstDeletedMgrEmpID);
                if (lstOrgMgr != null && lstOrgMgr.Count > 0)
                {
                    lstOrgMgr.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstOrgMgr);
                }

                // EmployeeImage
                if (param.EmployeeImage != null && param.EmployeeImage.SignImage != null)
                {
                    param.EmployeeImage.EmployeeID = emp.EmployeeID;

                    int? employeeImageID = _dao.GetEmployeeImageIDByEmployeeID(emp.EmployeeID);
                    if (employeeImageID.HasValue)
                    {
                        param.EmployeeImage.EmployeeImageID = employeeImageID;
                        _dao.UpdateItem(param.EmployeeImage);
                    }
                    else
                        _dao.InsertItem(param.EmployeeImage);
                }

                trans.Complete();
            }

            string actionName = "người dùng " + emp.Username;
            dataTrackingBiz.LogDataTracking(SMX.DataTracking.Feature.Administrator, SMX.DataTracking.ActionType.Edit,
                actionName, logContent, SMX.DataTracking.RefType.AdminUser, emp.EmployeeID);

            GlobalCache.UpdateEmployee(emp);
        }

        public void UpdateItemFromExcel(Employee employee, List<EmployeeRole> EmployeeRoles)
        {
            DateTime now = DateTime.Now;
            string userName = Profiles.MyProfile.UserName;

            //Employee
            Employee emp = employee;
            //EmployeeRole
            List<EmployeeRole> lstEmpRole = EmployeeRoles;

            List<OrganizationEmployee> lstOrgEmp = new List<OrganizationEmployee>();
            List<OrganizationManager> lstOrgMgr = new List<OrganizationManager>();
            //lấy danh sách phòng ban theo tên phòng ban
            var lstvOrgEmp = _daoOrg.GetListOrganizationByName(emp.OrganizationName);
            // lấy danh sách phòng ban mà user đang thuộc về
            var lstAllOrgOfEmp = _daoOrg.GetOrganizationOfEmployee(emp.EmployeeID);
            List<int?> lstDeletedAllOrgEmpID = new List<int?>();
            if (lstAllOrgOfEmp != null && lstAllOrgOfEmp.Any())
            {
                lstDeletedAllOrgEmpID = lstAllOrgOfEmp.Select(x => x.OrganizationID).ToList();
            }
            CalculateListOrganizationEmployee(lstvOrgEmp, emp.ListOrganizationManager, userName, lstOrgEmp, lstOrgMgr);
            //CalculateListOrganizationEmployee(emp.ListOrganizationEmployee, emp.ListOrganizationManager, userName, lstOrgEmp, lstOrgMgr);

            // xoa phong
            //List<int?> lstDeletedOrgEmpID = emp.ListDeletedOrganizationEmployeeID;
            //List<int?> lstDeletedOrgEmpID = new List<int?>();
            //if (lstvOrgEmp != null && lstvOrgEmp.Any())
            //{
            //    lstDeletedOrgEmpID = lstvOrgEmp.Select(x => x.OrganizationID).ToList();
            //}

            //if (lstDeletedOrgEmpID != null)
            //    lstDeletedOrgEmpID.RemoveAll(c => lstOrgEmp.Exists(d => d.OrganizationID == c));

            List<int?> lstDeletedMgrEmpID = emp.ListDeletedOrganizationManagerID;
            if (lstDeletedMgrEmpID != null)
                lstDeletedMgrEmpID.RemoveAll(c => lstOrgMgr.Exists(d => d.OrganizationID == c));

            //data trackng
            DataTrackingBiz dataTrackingBiz = new DataTrackingBiz();
            string logContent = string.Empty;
            RoleDao _daoRole = new RoleDao();
            List<Role> lstRoles = _daoRole.GetAllActiveRole().Where(r => lstEmpRole.Any(er => er.RoleID == r.RoleID)).ToList();
            logContent += " Danh sách role: ";
            logContent += string.Join(", ", lstRoles.Select(r => r.Name));
            logContent += "; Chi nhánh: " + (string.IsNullOrWhiteSpace(emp.ListBranchCode) ? "" : emp.ListBranchCode);

            using (TransactionScope trans = new TransactionScope())
            {
                //_dao.UpdateItem(emp);
                _dao.UpdateEmployeeByID(emp);

                _dao.DeleteEmployeeRole(emp.EmployeeID.Value);
                if (lstEmpRole != null && lstEmpRole.Count > 0)
                {
                    lstEmpRole.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstEmpRole);
                }

                _dao.DeleteOrganizationEmployee(emp.EmployeeID, lstDeletedAllOrgEmpID);
                //_dao.DeleteOrganizationEmployee(emp.EmployeeID, lstDeletedOrgEmpID);
                if (lstOrgEmp != null && lstOrgEmp.Count > 0)
                {
                    lstOrgEmp.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstOrgEmp);
                }

                _dao.DeleteOrganizationManager(emp.EmployeeID, lstDeletedMgrEmpID);
                if (lstOrgMgr != null && lstOrgMgr.Count > 0)
                {
                    lstOrgMgr.ForEach(c => c.EmployeeID = emp.EmployeeID);
                    _dao.InsertItems(lstOrgMgr);
                }

                trans.Complete();
            }

            string actionName = "người dùng " + emp.Username;
            dataTrackingBiz.LogDataTracking(SMX.DataTracking.Feature.Administrator, SMX.DataTracking.ActionType.Edit,
                actionName, logContent, SMX.DataTracking.RefType.AdminUser, emp.EmployeeID);
        }
        public void AddNewItemFromExcel(Employee employee, List<EmployeeRole> EmployeeRoles)
        {
            DateTime now = DateTime.Now;
            string userName = Profiles.MyProfile.UserName;
            //Employee
            Employee emp = employee;
            emp.Deleted = SMX.smx_IsNotDeleted;
            emp.Version = SMX.smx_FirstVersion;
            emp.CreatedBy = userName;
            emp.CreatedDTG = now;

            List<OrganizationEmployee> lstOrgEmp = new List<OrganizationEmployee>();
            List<OrganizationManager> lstOrgMgr = new List<OrganizationManager>();

            var lstvOrgEmp = _daoOrg.GetListOrganizationByName(emp.OrganizationName);
            //CalculateListOrganizationEmployee(emp.ListOrganizationEmployee, emp.ListOrganizationManager, userName, lstOrgEmp, lstOrgMgr);
            CalculateListOrganizationEmployee(lstvOrgEmp, emp.ListOrganizationManager, userName, lstOrgEmp, lstOrgMgr);

            //EmployeeRole
            List<EmployeeRole> lstEmpRole = EmployeeRoles;

            DataTrackingBiz dataTrackingBiz = new DataTrackingBiz();
            string logContent = string.Empty;
            RoleDao _daoRole = new RoleDao();
            List<Role> lstRoles = _daoRole.GetAllActiveRole().Where(r => lstEmpRole.Any(er => er.RoleID == r.RoleID)).ToList();
            logContent += " Danh sách role: ";
            logContent += string.Join(", ", lstRoles.Select(r => r.Name));
            logContent += "; Chi nhánh: " + (string.IsNullOrWhiteSpace(emp.ListBranchCode) ? "" : emp.ListBranchCode);

            using (TransactionScope tran = new TransactionScope())
            {
                //int empID = _dao.InsertEmployeeAndGetID(emp);
                int empID = (int)emp.EmployeeID;
                if (lstEmpRole != null && lstEmpRole.Count > 0)
                {
                    lstEmpRole.ForEach(c => c.EmployeeID = empID);
                    _dao.InsertItems(lstEmpRole);
                }

                if (lstOrgEmp != null && lstOrgEmp.Count > 0)
                {
                    lstOrgEmp.ForEach(c => c.EmployeeID = empID);
                    _dao.InsertItems(lstOrgEmp);
                }

                if (lstOrgMgr != null && lstOrgMgr.Count > 0)
                {
                    lstOrgMgr.ForEach(c => c.EmployeeID = empID);
                    _dao.InsertItems(lstOrgMgr);
                }

                tran.Complete();
            }

            string actionName = "người dùng " + emp.Username;
            dataTrackingBiz.LogDataTracking(SMX.DataTracking.Feature.Administrator, SMX.DataTracking.ActionType.AddNew,
                actionName, logContent, SMX.DataTracking.RefType.AdminUser, emp.EmployeeID);

        }
        #endregion

        #region View

        public void SetupViewForm(UserParam param)
        {
            throw new NotImplementedException();
        }

        public void DeleteItems(UserParam param)
        {
            foreach (var item in param.Employees)
            {
                try
                {
                    DeleteItem(item);
                }
                catch (SMXException ex)
                {
                    throw ex;
                }
            }
        }

        public void SearchItemsForView(UserParam param)
        {
            param.EmployeeInfos = _dao.GetEmployeesForView(param);
        }

        public void GetEmployeeByID(UserParam param)
        {
            int? empID = param.Employee.EmployeeID;

            if (empID.HasValue)
                param.Employee = _dao.GetItemByID<Employee>(empID.Value);
            else
                param.Employee = null;
        }
        public void GetEmployeeByUserName(UserParam param)
        {
            param.Employee = _daoEmp.GetEmployeeByUserName(param.UserName);
        }
        public void ImportOrUpdateListEmployeeFromExcel(UserParam param)
        {

            List<Employee> listHR = _daoEmp.GetListAllEmployee();

            List<Employee> listNew = new List<Employee>();
            List<Employee> listExisting = new List<Employee>();

            // Loại bỏ các bản ghi trùng lặp trong danh sách mới dựa trên FullName và Email
            var distinctListEmployee = param.Employees
                .GroupBy(x => new { x.Username })
                .Select(g => g.First())
                .ToList();

            var temp = distinctListEmployee.Select(x =>
            {
                var dataTemp = listHR.FirstOrDefault(p => p.Username.Equals(x.Username));
                if (dataTemp != null)
                {
                    listExisting.Add(x);
                    return x;
                }
                else
                {
                    listNew.Add(x);
                    return x;
                }
            }).ToList();

            // Thêm bản ghi mới
            if (listNew.Count > 0)
            {
                foreach (var item in listNew)
                {
                    List<EmployeeRole> roles = new List<EmployeeRole>();
                    try
                    {
                        item.EmployeeID = _dao.InsertEmployeeAndGetID(item);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogError("ERROR: NGƯỜI DÙNG _ Insert Employee khi import Excel", ex);
                        continue;
                    }

                    var lstRoleByName = _daoRole.GetAllActiveRoleByName(item.NamePermissionGroups);
                    //Thêm nhóm quyền của Employee
                    foreach (var itemRole in lstRoleByName)
                    {
                        if (item.EmployeeID == -1)
                            continue;
                        EmployeeRole role = new EmployeeRole();
                        role.EmployeeID = item.EmployeeID;
                        role.RoleID = itemRole.RoleID;
                        roles.Add(role);
                    }
                    try
                    {
                        AddNewItemFromExcel(item, roles);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogError("ERROR: NGƯỜI DÙNG _ Insert nhóm quyền cho Employee khi import Excel", ex);
                        continue;
                    }
                }

            }
            // Cập nhật bản ghi đã tồn tại
            if (listExisting.Count > 0)
            {
                foreach (var item in listExisting)
                {
                    List<EmployeeRole> roles = new List<EmployeeRole>();
                    //_daoEmp.UpdateItem(item);
                    var lstRoleByName = _daoRole.GetAllActiveRoleByName(item.NamePermissionGroups);
                    foreach (var itemRole in lstRoleByName)
                    {
                        if (item.EmployeeID == -1)
                            continue;
                        EmployeeRole role = new EmployeeRole();
                        role.EmployeeID = item.EmployeeID;
                        role.RoleID = itemRole.RoleID;
                        roles.Add(role);
                    }
                    try
                    {
                        UpdateItemFromExcel(item, roles);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogError("ERROR: NGƯỜI DÙNG _ UPDATE nhóm quyền cho Employee khi import Excel", ex);
                        continue;
                    }
                }
            }
        }
        #endregion
    }
}