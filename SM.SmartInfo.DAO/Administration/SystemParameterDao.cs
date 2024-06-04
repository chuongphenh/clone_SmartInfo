using System.Linq;
using SoftMart.Core.Dao;
using System.Data.SqlClient;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.DAO.Administration
{
    public class SystemParameterDao : BaseDao
    {
        #region Modification methods

        public SystemParameter GetSystemParameterByID(int id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<SystemParameter>(id);
            }
        }

        public void InsertSystemParameter(SystemParameter item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<SystemParameter>(item);
            }
        }

        public void InsertSystemParameters(List<SystemParameter> lst)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItems<SystemParameter>(lst);
            }
        }

        public void DeleteSystemParameter(int id)
        {
            using (DataContext dataContext = new DataContext())
            {
                int affectedRows = dataContext.DeleteItem<SystemParameter>(id);
                if (affectedRows == 0)
                {
                    throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "Loại tài liệu"));
                }

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

        public List<SystemParameter> GetAllActiveSystemParametersByFeatureID(int featureID)
        {
            List<SystemParameter> res;
            using (var dataContext = new DataContext())
            {
                res = dataContext.SelectItemByColumnName<SystemParameter>(
                        new ConditionList()
                        {
                                {SystemParameter.C_FeatureID, featureID },
                                { SystemParameter.C_Status,SMX.Status.Active }
                        });
            }

            return res.OrderBy(c => c.Name).ToList();
        }

        public List<SystemParameter> GetSystemParamByFeatureID(List<int> featureIDs)
        {
            string sql = @"select SystemParameterID, Name, Code, Ext1i, Ext2i 
                            from adm_SystemParameter
                            where Deleted = 0
                                and FeatureID in ({0})";
            sql = string.Format(sql, BuildInCondition(featureIDs));

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<SystemParameter>(sql);
            }
        }

        public List<SystemParameter> GetSystemByFeatureIDAndExt1i(int featureID, int exti1)
        {
            List<SystemParameter> lstSp = null;
            using (var dataContext = new DataContext())
            {
                lstSp = dataContext.SelectItemByColumnName<SystemParameter>(new ConditionList()
                { { SystemParameter.C_Ext1i, exti1 },
                   { SystemParameter.C_FeatureID, featureID },
                  {SystemParameter.C_Status, SMX.Status.Active}});
            }

            return lstSp.OrderBy(c => c.DisplayOrder).ToList();
        }

        public List<SystemParameter> GetAllShortSystemParameter()
        {
            var lstCol = new string[] {
                SystemParameter.C_SystemParameterID, SystemParameter.C_FeatureID,
                SystemParameter.C_Code, SystemParameter.C_Name, SystemParameter.C_Ext1,
                SystemParameter.C_Ext1i, SystemParameter.C_Ext2, SystemParameter.C_Ext3, SystemParameter.C_Ext4, SystemParameter.C_Status};
            using (var dataContext = new DataContext())
            {
                var res = dataContext.SelectFieldsByColumnName<SystemParameter>(lstCol, null);
                return res;
            }
        }


        public bool CheckCodeIsExist(string code, int featureID)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByColumnName<SystemParameter>(
                    new ConditionList() {
                                            { SystemParameter.C_Code, code },
                                            { SystemParameter.C_FeatureID, featureID }
                                        }).Count > 0;
            }
        }

        public List<SystemParameter> SelectItemByFeatureIDAndExt1i(int featureID, int ext1i)
        {
            string cmdText = string.Format(@"select * from adm_SystemParameter where Deleted = 0 and FeatureID={0} and Ext1i={1}", featureID, ext1i);
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<SystemParameter>(cmdText);
            }
        }

        public List<SystemParameter> GetListSystemParameterByExt1iAndExt2i(int? featureID, int? ext1i, int? ext2i)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByColumnName<SystemParameter>(
                    new ConditionList() {
                                            { SystemParameter.C_FeatureID, featureID },
                                            { SystemParameter.C_Ext1i, ext1i },
                                            { SystemParameter.C_Ext2i, ext2i }
                                        });
            }
        }

        public int? GetIDByCodeAndFeatureID(string code, int? featureID)
        {
            string cmdText = @"SELECT 
                                enSys.SystemParameterID
                            FROM 
                                adm_SystemParameter enSys 
                            WHERE 
                                enSys.Deleted = 0 
                                AND enSys.FeatureID = @FeatureID
                                AND enSys.Code = @Code
                            ";
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<int?>(cmd).FirstOrDefault();
            }
        }

        public SystemParameter GetSystemParameterByFeatureIDAndCode(int? featureID, string code)
        {
            string cmdText = @"SELECT 
                                    enSys.*
                                FROM 
                                    adm_SystemParameter enSys 
                                WHERE 
                                    enSys.Deleted = 0 
                                    AND enSys.FeatureID = @FeatureID
                                    AND enSys.Code = @Code";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@FeatureID", featureID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<SystemParameter>(cmd).FirstOrDefault();
            }
        }
    }
}