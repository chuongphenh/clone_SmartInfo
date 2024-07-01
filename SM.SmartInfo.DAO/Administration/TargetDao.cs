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
    public class TargetDao : BaseDao
    {
        #region Modification methods

        public void InsertTarget(Target item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<Target>(item);
            }
        }

        public void UpdateTarget(Target item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<Target>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "Target"));
            }
        }

        #endregion

        #region Getting methods

        public Target GetTargetByID(int? id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<Target>(id);
            }
        }

        public void GetEmailTemplates(TargetParam param)
        {
            var target = param.Targets;

            string cmdText = @"SELECT *
                               FROM Target
                               WHERE (@Name IS NULL OR TargetName like @Name) AND (@Subject is null or Subject like @Subject) AND Deleted = @NotDeleted";
            var cmd = new SqlCommand(cmdText);
            //cmd.Parameters.AddWithValue("@Name", BuildLikeFilter(target.N));
            //cmd.Parameters.AddWithValue("@Subject", BuildLikeFilter(target.Subject));
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);


            using (var dataContext = new DataContext())
            {
                param.Targets = base.ExecutePaging<Target>(dataContext, cmd, " Name desc", param.PagingInfo);
            }
        }
        public TargetInfo GetShortTargetByID(int targetID)
        {

            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.SelectFieldsByColumnName<TargetInfo>(new string[] { Target.C_TargetCode, Target.C_TargetName, Target.C_RequestResult, Target.C_TargetID },
                                                        new ConditionList()
                                                        {
                                                            {Target.C_TargetID, targetID},
                                                        }).FirstOrDefault();

                return res;
            }
        }
        #endregion
    }
}
