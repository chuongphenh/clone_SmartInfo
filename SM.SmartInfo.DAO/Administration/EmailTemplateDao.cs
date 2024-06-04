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

namespace SM.SmartInfo.DAO.Administration
{
    public class EmailTemplateDao : BaseDao
    {
        #region Modification methods

        public void InsertEmailTemplate(Flex_EmailTemplate item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<Flex_EmailTemplate>(item);
            }
        }

        public void UpdateEmailTemplate(Flex_EmailTemplate item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<Flex_EmailTemplate>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "EmailTemplate"));
            }
        }

        #endregion

        #region Getting methods

        public Flex_EmailTemplate GetEmailTemplateByID(int? id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<Flex_EmailTemplate>(id);
            }
        }

        public void GetEmailTemplates(EmailTemplateParam param)
        {
            var email = param.EmailTemplate;

            string cmdText = @"SELECT *
                               FROM flex_EmailTemplate
                               WHERE (@Name IS NULL OR Name like @Name) AND (@Subject is null or Subject like @Subject) AND Deleted = @NotDeleted";
            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Name", BuildLikeFilter(email.Name));
            cmd.Parameters.AddWithValue("@Subject", BuildLikeFilter(email.Subject));
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);


            using (var dataContext = new DataContext())
            {
                param.EmailTemplates = base.ExecutePaging<Flex_EmailTemplate>(dataContext, cmd, " Name desc", param.PagingInfo);
            }
        }
        #endregion
    }
}
