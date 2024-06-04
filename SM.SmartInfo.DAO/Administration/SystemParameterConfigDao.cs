using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;

using SoftMart.Core.Dao;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.SharedComponent.Params;
using SoftMart.Kernel.Exceptions;

namespace SM.SmartInfo.DAO.Administration
{
    //Standard Admin
    //Class nay khong quan tam Item Active hay InActive
    public class SystemParameterConfigDao : BaseDao
    {
        #region Modification methods

        public void InsertSystemParameter(SystemParameter item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<SystemParameter>(item);
            }
        }

        public void UpdateSystemParameter(SystemParameter item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<SystemParameter>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "SystemParameter"));
            }
        }


        #endregion

        #region Getting methods

        public SystemParameter GetSystemParameterByID(int id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<SystemParameter>(id);
            }
        }

        public void GetDataBySearchItem(SystemParameterParam param)
        {
            SystemParameter searchItem = param.SystemParameter;

            var cmdText = string.Format(@"SELECT SysDoc.Name, SysMap.Checked
                FROM adm_SystemParameter SysMap
                left join adm_SystemParameter SysDoc on SysDoc.SystemParameterID = SysMap.Ext3
                left join adm_SystemParameter SysAss on SysAss.SystemParameterID = SysMap.Ext1i
                WHERE SysMap.FeatureID = 1314 and SysMap.Ext1i = @Ext1i and SysMap.Ext2i = @Ext2i");

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Ext1i", searchItem.Ext1i);
            cmd.Parameters.AddWithValue("@Ext2i", searchItem.Ext2i);

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "Code", param.PagingInfo);
            }
        }



        public void SearchSystemParameter(SystemParameterParam param)
        {
            SystemParameter filter = param.SystemParameter;

            List<int> lstStatus = new List<int>(){
                SMX.Status.Active,
                SMX.Status.Draft
            };

            var cmdText = string.Format(@"SELECT * 
                             FROM adm_SystemParameter 
                             WHERE Deleted=0 AND FeatureID=@FeatureID 
                             AND (@Name IS NULL OR Name like @Name OR Code like @Name)
                             AND Status in ({0})", base.BuildInCondition(lstStatus));

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", filter.FeatureID);
            cmd.Parameters.AddWithValue("@Name", base.BuildLikeFilter(filter.Name));

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "Code", param.PagingInfo);
            }
        }

        public bool CheckCodeExist(string code, int? SystemParameterID, int featureID)
        {
            var cmdText = @"SELECT COUNT(*) 
                              FROM adm_SystemParameter 
                              WHERE Deleted = 0 AND Name = @Code
                                    AND (@SystemParameterID is null Or SystemParameterID <> @SystemParameterID)
                                    AND FeatureID=@FeatureID";
            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@SystemParameterID", SystemParameterID);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);

            using (DataContext datacontext = new DataContext())
            {
                int count = datacontext.ExecuteSelect<int>(cmd).First();
                return count > 0;
            }
        }

        public bool CheckIfCodeExist(string code, int? SystemParameterID, int featureID)
        {
            var cmdText = @"SELECT COUNT(*) 
                              FROM adm_SystemParameter 
                              WHERE Deleted = 0 AND Code = @Code
                                    AND (@SystemParameterID is null Or SystemParameterID <> @SystemParameterID)
                                    AND FeatureID=@FeatureID";
            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@SystemParameterID", SystemParameterID);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);

            using (DataContext datacontext = new DataContext())
            {
                int count = datacontext.ExecuteSelect<int>(cmd).First();
                return count > 0;
            }
        }

        public void SearchYearDepreciationMax(SystemParameterParam param)
        {
            var cmdText = @" SELECT *
                               FROM adm_SystemParameter enSys
                                --left join adm_SystemParameter enConstructionType on enConstructionType.Code  = enSys.Ext1  and enConstructionType.FeatureID= 1326
                                --left join adm_SystemParameter enConstructionTypeByLegal on enConstructionTypeByLegal.SystemParameterID  = enSys.Ext1i
                                WHERE enSys.FeatureID=@FeatureID  AND enSys.Deleted=0
                                and (@ConstructionTypeCode is NULL or @ConstructionTypeCode = enSys.Ext1)
                                and (@ContructionTypeByLegalID IS NULL OR @ContructionTypeByLegalID = enSys.Ext1i)";

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", param.FeatureID);
            cmd.Parameters.AddWithValue("@ConstructionTypeCode", param.Code);
            cmd.Parameters.AddWithValue("@ContructionTypeByLegalID", param.Ext1i);

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "SystemParameterID DESC", param.PagingInfo);
            }
        }

        public void SearchDepreciationMaxTime(SystemParameterParam param)
        {
            List<int> lstStatus = new List<int>(){
                SMX.Status.Active,
                SMX.Status.Draft
            };

            var cmdText = string.Format(@" SELECT enSys.SystemParameterID, enSys.Ext1i, enSys.Ext2i, enSys.Status
                               FROM adm_SystemParameter enSys
                                WHERE enSys.FeatureID=@FeatureID  AND enSys.Deleted=0
                                and (@VehicleType is NULL or @VehicleType = enSys.Ext1i)
                                and Status in ({0})", base.BuildInCondition(lstStatus));

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", param.FeatureID);
            cmd.Parameters.AddWithValue("@VehicleType", param.Ext1i);

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "SystemParameterID", param.PagingInfo);
            }
        }

        public void SearchCustomerRelationship(SystemParameterParam param)
        {
            SystemParameter searchItem = param.SystemParameter;
            List<int> lstStatus = new List<int>(){
                SMX.Status.Active,
                SMX.Status.Draft
            };

            var cmdText = string.Format(@" SELECT *
                               FROM adm_SystemParameter enSys
                                WHERE enSys.FeatureID=@FeatureID AND enSys.Deleted=0
                                and (@CustomerType is NULL or @CustomerType = enSys.Ext2i)
                                and Status in ({0})", base.BuildInCondition(lstStatus));

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", searchItem.FeatureID);
            cmd.Parameters.AddWithValue("@CustomerType", searchItem.Ext2i);

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "SystemParameterID", param.PagingInfo);
            }
        }

        public void SearchMaximumLifeSpan(SystemParameterParam param)
        {
            List<int> lstStatus = new List<int>()
            {
                SMX.Status.Active,
                SMX.Status.Draft,
            };

            var cmdText = string.Format(@" SELECT enSys.SystemParameterID, enSys.Ext1i, enSys.Ext2i, enSys.Status, enSys.CreatedDTG
                               FROM adm_SystemParameter enSys
                                WHERE enSys.FeatureID=@FeatureID AND enSys.Deleted=0
                                and (@VesselType is NULL or @VesselType = enSys.Ext1i)
                                and Status in ({0})", base.BuildInCondition(lstStatus));

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", param.FeatureID);
            cmd.Parameters.AddWithValue("@VesselType", param.Ext1i);

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "CreatedDTG desc", param.PagingInfo);
            }
        }

        public void SearchIndustrialZone(SystemParameterParam param)
        {
            List<int> lstStatus = new List<int>(){
                SMX.Status.Active,
                SMX.Status.Draft
            };
            var cmdText = string.Format(@" SELECT enSys.*
                               FROM adm_SystemParameter enSys
                                WHERE enSys.FeatureID=@FeatureID AND enSys.Deleted=0
                                --and (@Name is NULL or enSys.Name like @Name)
                                and (@Ext1 is NULL or enSys.Ext1 like @Ext1)
                                and (@Ext1i is NULL or enSys.Ext1i like @Ext1i)
                                and Status in ({0})", base.BuildInCondition(lstStatus));

            var cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@FeatureID", param.FeatureID);
            //cmd.Parameters.AddWithValue("@Name", BuildLikeFilter(param.SystemParameterName));
            cmd.Parameters.AddWithValue("@Ext1", BuildLikeFilter(param.Ext1));
            cmd.Parameters.AddWithValue("@Ext1i", param.Ext1i);

            using (var dataContext = new DataContext())
            {
                param.SystemParameters = base.ExecutePaging<SystemParameter>(dataContext, cmd, "CreatedDTG desc", param.PagingInfo);
            }
        }

        public List<SystemParameter> GetListItemByFeatureID(int? featureID)
        {
            string query = @"select SystemParameterID, Code, Status from adm_SystemParameter where FeatureID = @FeatureID and Deleted = @NotDeleted";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@FeatureID", featureID);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SystemParameter>(sqlCmd);
            }
        }

        public List<SystemParameter> GetListItemByFeatureIDAndCode(int? featureID, string code)
        {
            string query = @"select SystemParameterID, Name, Ext3, Ext4, Code, Status, Description
                            from adm_SystemParameter where FeatureID = @FeatureID and Code = @Code and Deleted = @NotDeleted";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@FeatureID", featureID);
            sqlCmd.Parameters.AddWithValue("@Code", code);
            sqlCmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SystemParameter>(sqlCmd);
            }
        }
        #endregion
    }
}
