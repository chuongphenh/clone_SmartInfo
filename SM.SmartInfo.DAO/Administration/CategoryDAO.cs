using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SoftMart.Core.Dao;

namespace SM.SmartInfo.DAO.Administration
{
    public class CategoryDAO : BaseDao
    {
        #region Modification methods

        public void InsertCategory(Category item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<Category>(item);
            }
        }

        public void UpdateCategory(Category item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<Category>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "Category"));
            }
        }

        #endregion

        #region Getting methods

        public Category GetCategoryByID(int? id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<Category>(id);
            }
        }

        public void GetCategoryTemplates(CategoryParam param)
        {
            var Category = param.Categories;

            string cmdText = @"SELECT *
                               FROM Category
                               WHERE (@Name IS NULL OR CategoryName like @Name) AND (@Subject is null or Subject like @Subject) AND Deleted = @NotDeleted";
            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);


            using (var dataContext = new DataContext())
            {
                param.Categories = base.ExecutePaging<Category>(dataContext, cmd, " Name desc", param.PagingInfo);
            }
        }
        #endregion
    }
}
