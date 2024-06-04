using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Core.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SM.SmartInfo.DAO
{
    public abstract class BaseDao
    {
        protected string BuildLikeFilter(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return null;
            return string.Format("%{0}%", keyword.Trim());
        }

        protected string BuildLikeFilterStartWith(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return null;
            return string.Format("{0}%", keyword.Trim());
        }

        protected string BuildInCondition(List<int> lstValue)
        {
            if (lstValue.Count == 0)
            {
                return "null";
            }
            else
            {
                return string.Join(", ", lstValue.ToArray());
            }
        }

        protected string BuildInCondition(List<string> lstValue)
        {
            if (lstValue.Count == 0)
            {
                return "null";
            }
            else
            {
                string result = string.Empty;
                string separator = string.Empty;

                foreach (string item in lstValue)
                {
                    result += separator + "N'" + item.Trim().Replace("'", "''") + "'";
                    separator = ",";
                }
                return result;
            }
        }

        protected string GetTopRowByPaging(SoftMart.Kernel.Entity.PagingInfo pagingInfo)
        {
            if (pagingInfo == null)
                return "100 percent";

            return ((pagingInfo.PageIndex + 10) * pagingInfo.PageSize).ToString();
        }

        protected List<T> GetItemByPaging<T>(List<T> lstItem, SoftMart.Kernel.Entity.PagingInfo pagingInfo) where T : class
        {
            if (pagingInfo != null)
            {
                pagingInfo.RecordCount = lstItem.Count;
                return lstItem.Skip(pagingInfo.RowsSkip).Take(pagingInfo.PageSize).ToList();
            }

            return lstItem;
        }

        public void SetProperty<T>(T item, string field, object value)
        {
            PropertyInfo propertyInfo = item.GetType().GetTypeInfo().GetDeclaredProperty(field);
            if (propertyInfo != null && propertyInfo.GetValue(item) == null)
                propertyInfo.SetValue(item, value);
        }

        public void InsertItem<T>(T item) where T : BaseEntity
        {
            SetProperty(item, "Deleted", SMX.smx_IsNotDeleted);
            SetProperty(item, "Version", SMX.smx_FirstVersion);
            SetProperty(item, "CreatedBy", Profiles.MyProfile.UserName);
            SetProperty(item, "CreatedDTG", DateTime.Now);
            using (DataContext context = new DataContext())
            {
                context.InsertItem<T>(item);
            }
        }

        public void InsertItems<T>(List<T> lstItem) where T : BaseEntity
        {
            foreach (var item in lstItem)
            {
                SetProperty(item, "Deleted", SMX.smx_IsNotDeleted);
                SetProperty(item, "Version", SMX.smx_FirstVersion);
                SetProperty(item, "CreatedBy", Profiles.MyProfile.UserName);
                SetProperty(item, "CreatedDTG", DateTime.Now);
            }
            using (DataContext context = new DataContext())
            {
                context.InsertItems<T>(lstItem);
            }
        }

        public void UpdateItem<T>(T item) where T : BaseEntity
        {
            SetProperty(item, "UpdatedBy", Profiles.MyProfile.UserName);
            SetProperty(item, "UpdatedDTG", DateTime.Now);
            using (DataContext context = new DataContext())
            {
                int eff = context.UpdateItem<T>(item);
            }
        }

        public int UpdateItem<T>(T item, bool throwExceptionIfUpdateFail = false) where T : BaseEntity
        {
            using (DataContext context = new DataContext())
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

        public T GetItemByID<T>(object id) where T : BaseEntity
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByID<T>(id);
            }
        }

        protected List<T> ExecutePaging<T>(DataContext dataContext, System.Data.SqlClient.SqlCommand command, string orderStatement, SoftMart.Kernel.Entity.PagingInfo pagingInfo)
        {
            int recordCord;
            List<T> lst = dataContext.ExecutePaging<T>(command, pagingInfo.PageIndex, pagingInfo.PageSize, orderStatement, out recordCord);
            pagingInfo.RecordCount = recordCord;
            return lst;
        }

        protected List<T> ExecutePagingTopSelect<T>(System.Data.SqlClient.SqlCommand sqlCmd, SoftMart.Kernel.Entity.PagingInfo pagingInfo)
        {
            string topRow = GetTopRowByPaging(pagingInfo);
            sqlCmd.CommandText = string.Format(sqlCmd.CommandText, topRow);
            List<T> lst = new List<T>();
            using (DataContext dataContext = new DataContext())
            {
                lst = dataContext.ExecuteSelect<T>(sqlCmd);
            }
            if (pagingInfo != null)
            {
                pagingInfo.RecordCount = lst.Count;
                lst = lst.Skip(pagingInfo.RowsSkip).Take(pagingInfo.PageSize).ToList();
            }
            return lst;
        }

        #region Export
        public System.Data.DataTable GetQueryData(string query, Dictionary<string, object> dicQueryParam)
        {
            if (string.IsNullOrWhiteSpace(query))
                return null;

            System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(query);
            if (dicQueryParam != null)
            {
                foreach (KeyValuePair<string, object> objParam in dicQueryParam)
                    sqlCmd.Parameters.AddWithValue(objParam.Key, objParam.Value);
            }
            using (DataContext context = new DataContext())
            {
                return context.ExecuteDataTable(sqlCmd);
            }
        }
        #endregion

        #region Store
        protected void ExecuteStore(string storeName, params object[] arrParam)
        {
            Utils.LogManager.WebLogger.LogDebug(string.Format("Start {0}", storeName));

            string query = string.Format("exec {0} ", storeName);
            SqlCommand sqlCmd = new SqlCommand();
            if (arrParam != null)
            {
                string delimieter = "";
                for (int index = 0; index < arrParam.Length; index++)
                {
                    string paramName = string.Format("@Param{0}", index);
                    query = query + delimieter + paramName;
                    sqlCmd.Parameters.AddWithValue(paramName, arrParam[index]);
                    delimieter = ", ";
                }
            }

            sqlCmd.CommandText = query;
            int itemCount = 0;
            using (DataContext context = new DataContext())
            {
                itemCount = context.ExecuteNonQuery(sqlCmd);
            }

            Utils.LogManager.WebLogger.LogDebug(string.Format("End {0}. ItemCount: {1}", storeName, itemCount));
        }

        protected List<T> ExecuteStoreGetResult<T>(string storeName, params object[] arrParam) where T : class
        {
            Utils.LogManager.WebLogger.LogDebug(string.Format("Start {0}", storeName));

            string query = string.Format("exec {0} ", storeName);
            SqlCommand sqlCmd = new SqlCommand();
            if (arrParam != null)
            {
                string delimieter = "";
                for (int index = 0; index < arrParam.Length; index++)
                {
                    string paramName = string.Format("@Param{0}", index);
                    query = query + delimieter + paramName;
                    sqlCmd.Parameters.AddWithValue(paramName, arrParam[index]);
                    delimieter = ", ";
                }
            }
            sqlCmd.CommandText = query;

            List<T> lstItem = null;
            using (DataContext context = new DataContext())
            {
                lstItem = context.ExecuteSelect<T>(sqlCmd);
            }

            int count = (lstItem == null ? 0 : lstItem.Count);
            Utils.LogManager.WebLogger.LogDebug(string.Format("End {0}. ItemCount: {1}", storeName, count));
            return lstItem;
        }

        protected DataTable ExecuteStoreGetDataTable(string storeName, params object[] arrParam)
        {
            Utils.LogManager.WebLogger.LogDebug(string.Format("Start {0}", storeName));

            string query = string.Format("exec {0} ", storeName);
            SqlCommand sqlCmd = new SqlCommand();
            if (arrParam != null)
            {
                string delimieter = "";
                for (int index = 0; index < arrParam.Length; index++)
                {
                    string paramName = string.Format("@Param{0}", index);
                    query = query + delimieter + paramName;
                    sqlCmd.Parameters.AddWithValue(paramName, arrParam[index]);
                    delimieter = ", ";
                }
            }
            sqlCmd.CommandText = query;

            DataTable table = null;
            using (DataContext context = new DataContext())
            {
                table = context.ExecuteDataTable(sqlCmd);
            }

            int count = (table == null ? 0 : table.Rows.Count);
            Utils.LogManager.WebLogger.LogDebug(string.Format("End {0}. ItemCount: {1}", storeName, count));

            return table;
        }
        #endregion
    }
}
