using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SM.SmartInfo.CacheManager
{
    public static class GlobalCache
    {
        #region System Parameter
        private static List<SystemParameter> _systemParameter = null;
        private static DateTime? _lastOrgDTG = null;
        private static Dictionary<int?, adm_SyncCache> _dicCacheSP = null;

        public static void ReloadCache()
        {
            using (DAO.Common.DataContext context = new DAO.Common.DataContext())
            {
                _systemParameter = context.ExecuteSelect<SystemParameter>("Select * from adm_SystemParameter where Deleted = 0");
            }
        }

        public static List<SystemParameter> CacheSystemParamenter
        {
            get
            {
                if (_systemParameter == null || _systemParameter.Count == 0)
                    ReloadCache();

                return _systemParameter;
            }
        }

        public static string GetCodeByID(int? id)
        {
            SystemParameter item = GetSystemParameterByID(id);

            return item == null ? string.Empty : item.Code;
        }

        public static SystemParameter GetSystemParameterByID(int? id)
        {
            if (CacheSystemParamenter != null && id.HasValue)
                return CacheSystemParamenter.Find(item => item.SystemParameterID == id);

            return null;
        }

        public static SystemParameter GetSystemParameterByName(string name)
        {
            if (CacheSystemParamenter != null && !string.IsNullOrEmpty(name))
                return CacheSystemParamenter.Find(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));

            return null;
        }

        public static string GetNameByID(int? id)
        {
            SystemParameter item = null;
            if (CacheSystemParamenter != null && id.HasValue)
                item = GetSystemParameterByID(id);

            return item == null ? string.Empty : item.Name;
        }

        public static int? GetIdByFeatureAndCode(int featureID, string code)
        {
            if (CacheSystemParamenter != null)
            {
                SystemParameter enSystem = CacheSystemParamenter.Find(item => item.FeatureID == featureID && item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
                if (enSystem != null)
                    return enSystem.SystemParameterID.Value;
            }

            return null;
        }

        public static int? GetIdByFeatureAndCodeAndExt1i(int featureID, string code, int? ext1i)
        {
            if (CacheSystemParamenter != null)
            {
                SystemParameter enSystem = CacheSystemParamenter.Find(item => item.FeatureID == featureID &&
                    item.Code.Equals(code, StringComparison.OrdinalIgnoreCase) &&
                    item.Ext1i == ext1i);
                if (enSystem != null)
                    return enSystem.SystemParameterID.Value;
            }

            return null;
        }

        public static SystemParameter GetItemByFeatureIDAndCode(int featureID, string code)
        {
            SystemParameter result = CacheSystemParamenter.Find(en => en.FeatureID == featureID && en.Code.Trim().Equals(code.Trim(), StringComparison.OrdinalIgnoreCase));
            return result;
        }

        public static SystemParameter GetItemByFeatureIDAndExt1i(int featureID, int ext1i)
        {
            SystemParameter result = CacheSystemParamenter.Find(en => en.FeatureID == featureID && en.Ext1i == ext1i);
            return result;
        }

        public static List<SystemParameter> GetListSystemParameterByFeatureID(int? featureID, bool exclusiveActive = false)
        {
            List<SystemParameter> lstSystem;

            if (CacheSystemParamenter != null && featureID.HasValue)
            {
                lstSystem = CacheSystemParamenter.Where(item => item.FeatureID == featureID).ToList();
                if (exclusiveActive)
                    lstSystem = lstSystem.Where(c => c.Status == 1).ToList();
                return lstSystem;
            }

            return new List<SystemParameter>();
        }
        private static List<SharedComponent.EntityViews.vOrganization> _lstvOrganization = new List<SharedComponent.EntityViews.vOrganization>();
        private static List<Organization> _lstOrganization = new List<Organization>();
        public static void LoadCacheOrganization()
        {
            CacheDao dao = new CacheDao();

            _lstvOrganization = dao.ExecuteSelect<SharedComponent.EntityViews.vOrganization>("select * from vOrganization");
            _lstOrganization = dao.ExecuteSelect<Organization>("select * from Organization where Deleted = 0");
            _lastOrgDTG = DateTime.Now;
        }

        public static List<Organization> GetAllShortOrganization()
        {
            CacheDao dao = new CacheDao();
            var dbCache = dao.ExecuteSelect<adm_SyncCache>("select * from adm_SyncCache where Type = 2").FirstOrDefault();
            if (dbCache != null && dbCache.LastUpdatedDTG.HasValue && _lastOrgDTG.HasValue &&
                dbCache.LastUpdatedDTG.Value > _lastOrgDTG.Value)
            {
                LoadCacheOrganization();
            }

            return (from c in _lstOrganization
                    select new Organization()
                    {
                        OrganizationID = c.OrganizationID,
                        ParentID = c.ParentID,
                        Name = c.Name,
                        CommitteeID = c.CommitteeID,
                        OfficeID = c.OfficeID
                    }).ToList();
        }

        public static Organization GetOrganizationByID(int? orgID)
        {
            if (orgID == null)
                return null;

            //return _lstOrganization.FirstOrDefault(c => c.OrganizationID == orgID);
            CacheDao dao = new CacheDao();
            return dao.GetItemByID<Organization>(orgID);
        }
        public static List<SystemParameter> GetListSystemParameterByFeatureIDAndExt1i(int? featureID, int? ext1i, bool exclusiveActive = false)
        {
            List<SystemParameter> lstSystem;

            if (CacheSystemParamenter != null && ext1i.HasValue && featureID.HasValue)
            {
                lstSystem = CacheSystemParamenter.Where(item => item.FeatureID == featureID && item.Ext1i == ext1i).ToList();
                if (exclusiveActive)
                    lstSystem = lstSystem.Where(c => c.Status == 1).ToList();
                return lstSystem;
            }

            return new List<SystemParameter>();
        }

        public static List<SystemParameter> GetListSystemParameterByFeatureIDAndExt1iAndExt3(int? featureID, int? ext1i, string ext3, bool exclusiveActive = false)
        {
            List<SystemParameter> lstSystem;

            if (CacheSystemParamenter != null && ext1i.HasValue && !string.IsNullOrEmpty(ext3) && featureID.HasValue)
            {
                lstSystem = CacheSystemParamenter.Where(item => item.FeatureID == featureID && item.Ext1i == ext1i && !string.IsNullOrWhiteSpace(item.Ext3) && item.Ext3.Split(',').ToList().Contains(ext3)).ToList();
                if (exclusiveActive)
                    lstSystem = lstSystem.Where(c => c.Status == 1).ToList();
                return lstSystem;
            }

            return new List<SystemParameter>();
        }

        public static void UpdateItem(SystemParameter item)
        {
            if (item == null || item.SystemParameterID == null)
                return;

            int index = _systemParameter.FindIndex(c => c.SystemParameterID == item.SystemParameterID);
            if (index >= 0) // ban ghi da ton tai
            {
                if (item.Deleted == SMX.smx_IsDeleted) // xoa du lieu
                {
                    _systemParameter.RemoveAt(index);
                }
                else // cap nhat du lieu
                {
                    using (DAO.Common.DataContext context = new DAO.Common.DataContext())
                    {
                        _systemParameter[index] = context.SelectItemByID<SystemParameter>(item.SystemParameterID);
                    }
                }
            }
            else // them moi
            {
                _systemParameter.Add(item);
            }
        }

        public static void UpdateEmployee(Employee item)
        {
            //if (item == null || item.EmployeeID == null)
            //    return;

            //CacheDao dao = new CacheDao();
            //int index = _lstEmployee.FindIndex(c => c.EmployeeID == item.EmployeeID);
            //if (index >= 0) // ban ghi da ton tai
            //{
            //    _lstEmployee[index] = dao.GetItemByID<Employee>(item.EmployeeID);
            //}
            //else // them moi
            //{
            //    _lstEmployee.Add(item);
            //}

            //var dbCache = dao.ExecuteSelect<adm_SyncCache>("select * from adm_SyncCache where Type = 4").FirstOrDefault();
            //if (dbCache == null)
            //    dbCache = new adm_SyncCache() { Type = 2, Version = 1 };
            //dbCache.LastUpdatedDTG = DateTime.Now;
            //_lastEmpDTG = dbCache.LastUpdatedDTG;
            //if (dbCache.SyncCacheID.HasValue)
            //    dao.UpdateItem(dbCache);
            //else
            //    dao.InsertItem(dbCache);
        }
        #endregion

        #region employee
        //private static List<Employee> _lstEmployee = new List<Employee>();
        public static void LoadCacheEmployee()
        {
            //CacheDao dao = new CacheDao();
            //_lastEmpDTG = DateTime.Now;
            //_lstEmployee = dao.ExecuteSelect<Employee>("select * from adm_Employee where Deleted = 0");
        }

        public static Employee GetEmployeeByID(int? employeeID)
        {
            if (employeeID == null)
                return null;

            //return _lstEmployee.FirstOrDefault(c => c.EmployeeID == employeeID);
            CacheDao dao = new CacheDao();
            return dao.GetItemByID<Employee>(employeeID);
        }
        public static string GetEmployeeNameByID(int? employeeID)
        {
            var emp = GetEmployeeByID(employeeID);
            if (emp != null)
                return emp.Name;

            return null;
        }
        public static string GetEmployeeUsernameByID(int? employeeID)
        {
            var emp = GetEmployeeByID(employeeID);
            if (emp != null)
                return emp.Username;

            return null;
        }

        #endregion

        public static void UpdateOrganization(Organization item)
        {
            if (item == null || item.OrganizationID == null)
                return;

            CacheDao dao = new CacheDao();
            int index = _lstOrganization.FindIndex(c => c.OrganizationID == item.OrganizationID);
            if (index >= 0) // ban ghi da ton tai
            {
                _lstOrganization[index] = dao.GetItemByID<Organization>(item.OrganizationID);
            }
            else // them moi
            {
                _lstOrganization.Add(item);
            }

            index = _lstvOrganization.FindIndex(c => c.OrganizationID == item.OrganizationID);
            SharedComponent.EntityViews.vOrganization vItem = null;
            vItem = dao.ExecuteSelect<SharedComponent.EntityViews.vOrganization>(string.Format(
                "select * from vOrganization where OrganizationID = {0}", item.OrganizationID ?? 0)).FirstOrDefault();
            if (vItem != null)
            {
                if (index >= 0) // ban ghi da ton tai
                {
                    _lstvOrganization[index] = vItem;
                }
                else // them moi
                {
                    _lstvOrganization.Add(vItem);
                }
            }

            var dbCache = dao.ExecuteSelect<adm_SyncCache>("select * from adm_SyncCache where Type = 2").FirstOrDefault();
            if (dbCache == null)
                dbCache = new adm_SyncCache() { Type = 2, Version = 1 };
            dbCache.LastUpdatedDTG = DateTime.Now;
            _lastOrgDTG = dbCache.LastUpdatedDTG;
            if (dbCache.SyncCacheID.HasValue)
                dao.UpdateItem(dbCache);
            else
                dao.InsertItem(dbCache);
        }

        private class CacheDao
        {
            public void InsertItem<T>(T item) where T : SoftMart.Core.Dao.BaseEntity
            {
                using (DAO.Common.DataContext context = new DAO.Common.DataContext())
                {
                    context.InsertItem<T>(item);
                }
            }

            public int UpdateItem<T>(T item, bool throwExceptionIfUpdateFail = false) where T : SoftMart.Core.Dao.BaseEntity
            {
                using (DAO.Common.DataContext context = new DAO.Common.DataContext())
                {
                    int affectedRow = context.UpdateItem<T>(item);
                    if (throwExceptionIfUpdateFail == true
                        && affectedRow == 0)
                    {
                        throw new SoftMart.Kernel.Exceptions.SMXException(Messages.ItemNotExitOrChanged);
                    }

                    return affectedRow;
                }
            }

            public T GetItemByID<T>(object id) where T : SoftMart.Core.Dao.BaseEntity
            {
                if (id == null)
                    return null;

                using (DAO.Common.DataContext context = new DAO.Common.DataContext())
                {
                    return context.SelectItemByID<T>(id);
                }
            }

            public List<T> ExecuteSelect<T>(string query) where T : SoftMart.Core.Dao.BaseEntity
            {
                using (DAO.Common.DataContext context = new DAO.Common.DataContext())
                {
                    return context.ExecuteSelect<T>(query);
                }
            }

            public List<T> ExecuteSelect<T>(System.Data.SqlClient.SqlCommand sqlCmd) where T : SoftMart.Core.Dao.BaseEntity
            {
                using (DAO.Common.DataContext context = new DAO.Common.DataContext())
                {
                    return context.ExecuteSelect<T>(sqlCmd);
                }
            }
        }

        public static List<SharedComponent.EntityViews.vOrganization> SearchvOrganization(int? type, string name)
        {
            SharedComponent.EntityViews.vOrganization rootItem = _lstvOrganization.FirstOrDefault(c => c.ParentID == null);
            List<int?> lstOrgIDByType = new List<int?>();
            if (type != null)
                lstOrgIDByType = (from c in _lstOrganization where c.Type.HasValue && (c.Type.Value & type.Value) == c.Type select c.OrganizationID).ToList();

            List<SharedComponent.EntityViews.vOrganization> lstItem = new List<SharedComponent.EntityViews.vOrganization>();
            foreach (var item in _lstvOrganization)
            {
                // khong match [name]
                if (!string.IsNullOrWhiteSpace(name) && !item.BreadCrumb.ToLower().Contains(name.Trim().ToLower()))
                    continue;

                // khong match [type]
                if (type != null && !lstOrgIDByType.Exists(c => c == item.OrganizationID))
                    continue;

                SharedComponent.EntityViews.vOrganization cloneItem = ClonevOrganization(item, rootItem);
                lstItem.Add(cloneItem);
            }

            return lstItem.OrderBy(c => c.BreadCrumb).ToList();
        }

        private static SharedComponent.EntityViews.vOrganization ClonevOrganization(SharedComponent.EntityViews.vOrganization item,
           SharedComponent.EntityViews.vOrganization rootItem)
        {
            SharedComponent.EntityViews.vOrganization cloneItem = new SharedComponent.EntityViews.vOrganization()
            {
                Code = item.Code,
                OrganizationID = item.OrganizationID,
                Name = item.Name,
                ParentID = item.ParentID,
                Level = item.Level,
                BreadCrumb = item.BreadCrumb
            };

            if (rootItem != null && cloneItem.OrganizationID != rootItem.OrganizationID)
                cloneItem.BreadCrumb = cloneItem.BreadCrumb.Substring(rootItem.Name.Length + 3);

            return cloneItem;
        }
    }
}