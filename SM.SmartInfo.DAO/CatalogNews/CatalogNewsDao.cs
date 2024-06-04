using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Core.Dao;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO.CatalogNews
{
    public class CatalogNewsDao : BaseDao
    {
        #region Modification methods

        public void InsertNews(SharedComponent.Entities.CatalogNews item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<SharedComponent.Entities.CatalogNews>(item);
            }
        }

        public void UpdateNews(SharedComponent.Entities.CatalogNews item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<SharedComponent.Entities.CatalogNews>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(Messages.ItemNotExitOrChanged);
            }
        }

        #endregion

        public List<SharedComponent.Entities.CatalogNews> GetAllShortCatalogNews()
        {
            using (var dataContext = new DataContext())
            {
                var res = dataContext.SelectFieldsByColumnName<SharedComponent.Entities.CatalogNews>(
                    new string[] { SharedComponent.Entities.CatalogNews.C_CatalogNewsID, SharedComponent.Entities.CatalogNews.C_ParentID, SharedComponent.Entities.CatalogNews.C_Name, SharedComponent.Entities.CatalogNews.C_Code},
                                                                             new ConditionList());
                return res;
            }
        }

        public int GetChildrenCount(int CatalogNewsID)
        {
            using (var dataContext = new DataContext())
            {
                int count = dataContext.CountItemByColumnName<SharedComponent.Entities.CatalogNews>(SharedComponent.Entities.CatalogNews.C_ParentID, CatalogNewsID);
                return count;
            }
        }

        public bool CheckDuplicatedCode(int? catalogNewsID, string code)
        {
            string cmdText = @"select count(*)
                                from CatalogNews
                                where Deleted=0 and Code=@Code 
                                    and (@CatalogNewsID is null Or CatalogNewsID<>@CatalogNewsID)";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@CatalogNewsID", catalogNewsID);
            sqlCmd.Parameters.AddWithValue("@Code", code);

            using (var dataContext = new DataContext())
            {
                int count = dataContext.ExecuteSelect<int>(sqlCmd).FirstOrDefault();
                return count > 0;
            }
        }
    }
}
