using System.Linq;
using System.Data.SqlClient;
using SoftMart.Kernel.Entity;
using SM.SmartInfo.DAO.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using System;
using System.Runtime.Remoting.Contexts;
using static SM.SmartInfo.SharedComponent.Constants.FunctionType.Administration;
using SoftMart.Kernel.Exceptions;

namespace SM.SmartInfo.DAO.EmulationAndRewards
{
    public class EmulationAndRewardDao : BaseDao
    {
        private string ConnectionString = ConfigUtils.ConnectionString;

        public void AddNewEmulationAndRewardSubject(er_EmulationAndRewardSubject item, EmulationAndRewardParam param)
        {
            List<er_EmulationAndRewardSubject> listExisting = new List<er_EmulationAndRewardSubject>();

            string getAll = @"SELECT * FROM [dbo].[er_EmulationAndRewardSubject]";

            SqlCommand cmda = new SqlCommand(getAll);

            using (DataContext dataContext = new DataContext())
            {
                listExisting = dataContext.ExecuteSelect<er_EmulationAndRewardSubject>(cmda);
            }

            var existingItem = listExisting.Where(x => x.Code.Equals(item.Code) && x.Type.Equals(item.Type)).FirstOrDefault();

            if (existingItem == null)
            {
                string sql = @"INSERT INTO [dbo].[er_EmulationAndRewardSubject] (EmulationAndRewardID, Code, Name, LatestTitle, Unit, Email, Mobile, Type, Deleted, Version, CreatedBy, CreatedDTG)
                            VALUES (@EmulationAndRewardID, @Code, @Name, @LatestTitle, @Unit, @Email, @Mobile, @Type, @Deleted, @Version, @CreatedBy, @CreatedDTG)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@EmulationAndRewardID", item.EmulationAndRewardID);
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@LatestTitle", item.LatestTitle);
                    cmd.Parameters.AddWithValue("@Unit", item.Unit);
                    cmd.Parameters.AddWithValue("@Email", item.Email);
                    cmd.Parameters.AddWithValue("@Mobile", item.Mobile);
                    cmd.Parameters.AddWithValue("@Type", item.Type);
                    cmd.Parameters.AddWithValue("@Deleted", item.Deleted);
                    cmd.Parameters.AddWithValue("@Version", item.Version);
                    cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                GetListEmulationAndRewardSubject(param);

                if (param.ListEmulationAndRewardSubject.Count > 0)
                {
                    InsertIntoEmulationAndRewardHistory(new er_EmulationAndRewardHistory()
                    {
                        EmulationAndRewardSubjectID = param.ListEmulationAndRewardSubject.OrderByDescending(x => x.EmulationAndRewardSubjectID).First().EmulationAndRewardSubjectID,
                        EmulationAndRewardID = Convert.ToInt32(item.EmulationAndRewardID),
                        Title = item.LatestTitle,
                        EmulationAndRewardUnit = item.Unit,
                        RewardedDTG = item.CreatedDTG,
                        AwardingType = param.AwardingTypeName
                    });
                }
            }
            else
            {
                string sql = @"UPDATE [dbo].[er_EmulationAndRewardSubject] SET EmulationAndRewardID = CONCAT(EmulationAndRewardID, ',', @EmulationAndRewardID), Name = @Name, LatestTitle = @LatestTitle, LatestEmulationAndRewardUnit = @Unit, 
                                Email = @Email, Mobile = @Mobile, Version = @Version, UpdatedBy = @UpdatedBy, UpdatedDTG = @UpdatedDTG, Deleted = @Deleted WHERE Code = @Code";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@EmulationAndRewardID", item.EmulationAndRewardID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@LatestTitle", item.LatestTitle);
                    cmd.Parameters.AddWithValue("@Unit", item.Unit);
                    cmd.Parameters.AddWithValue("@Email", item.Email);
                    cmd.Parameters.AddWithValue("@Mobile", item.Mobile);
                    cmd.Parameters.AddWithValue("@Version", existingItem.Version + 1);
                    cmd.Parameters.AddWithValue("@UpdatedBy", item.CreatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedDTG", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Deleted", 0);
                    cmd.Parameters.AddWithValue("@Code", existingItem.Code);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }

                InsertIntoEmulationAndRewardHistory(new er_EmulationAndRewardHistory()
                {
                    EmulationAndRewardSubjectID = existingItem.EmulationAndRewardSubjectID,
                    EmulationAndRewardID = Convert.ToInt32(item.EmulationAndRewardID),
                    Title = item.LatestTitle,
                    EmulationAndRewardUnit = item.Unit,
                    RewardedDTG = item.CreatedDTG,
                    AwardingType = param.AwardingTypeName
                });
            }
        }

        public void InsertIntoEmulationAndRewardHistory(er_EmulationAndRewardHistory item)
        {
            string sql = @"INSERT INTO [dbo].[er_EmulationAndRewardHistory] (EmulationAndRewardSubjectID, EmulationAndRewardID, Title, EmulationAndRewardUnit, AwardingType, RewardedDTG) 
                            VALUES (@EmulationAndRewardSubjectID, @EmulationAndRewardID, @Title, @EmulationAndRewardUnit, @AwardingType, @RewardedDTG)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@EmulationAndRewardSubjectID", item.EmulationAndRewardSubjectID);
                cmd.Parameters.AddWithValue("@EmulationAndRewardID", item.EmulationAndRewardID);
                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@EmulationAndRewardUnit", item.EmulationAndRewardUnit);
                cmd.Parameters.AddWithValue("@RewardedDTG", item.RewardedDTG);
                cmd.Parameters.AddWithValue("@AwardingType", item.AwardingType);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GetListEmulationAndRewardSubject(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[er_EmulationAndRewardSubject] WHERE Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListEmulationAndRewardSubject = dataContext.ExecuteSelect<er_EmulationAndRewardSubject>(cmd);
            }
        }

        public void GetListEmulationAndReward(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[er_EmulationAndReward] WHERE Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListEmulationAndReward = dataContext.ExecuteSelect<er_EmulationAndReward>(cmd);
            }
        }

        public void AddNewEmulationAndReward(EmulationAndRewardParam param)
        {
            string sql = @"INSERT INTO [dbo].[er_EmulationAndReward] (Year, Event, EmulationAndRewardUnit, SubjectRewarded, Deleted, Version, CreatedBy, CreatedDTG, PeriodId, AwardingTypeId)
                            VALUES (@Year, @Event, @EmulationAndRewardUnit, @SubjectRewarded, @Deleted, @Version, @CreatedBy, @CreatedDTG, @PeriodId, @AwardingTypeId)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Year", param.er_EmulationAndReward.Year);
                cmd.Parameters.AddWithValue("@Event", (object)param.er_EmulationAndReward.Event ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EmulationAndRewardUnit", (object)param.er_EmulationAndReward.EmulationAndRewardUnit ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubjectRewarded", (object)param.er_EmulationAndReward.SubjectRewarded ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Deleted", param.er_EmulationAndReward.Deleted);
                cmd.Parameters.AddWithValue("@Version", param.er_EmulationAndReward.Version);
                cmd.Parameters.AddWithValue("@CreatedBy", param.er_EmulationAndReward.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDTG", param.er_EmulationAndReward.CreatedDTG);
                cmd.Parameters.AddWithValue("@PeriodId", (object)param.er_EmulationAndReward.PeriodId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AwardingTypeId", (object)param.er_EmulationAndReward.AwardingTypeId ?? DBNull.Value);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSelectedAwardingType(EmulationAndRewardParam param)
        {
            string sql = @"DELETE FROM [dbo].[AwardingType] WHERE Id = @Id AND isDeleted = 0";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", param.AwardingTypeId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GetAwardingTypeById(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingType] WHERE isDeleted = 0 AND Id = @Id";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Id", param.AwardingTypeId);

            using (DataContext dataContext = new DataContext())
            {
                param.AwardingType = dataContext.ExecuteSelect<AwardingType>(cmd).FirstOrDefault();
            }
        }

        public void EditAwardingType(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingType] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingType = dataContext.ExecuteSelect<AwardingType>(all);
            }

            if (param.ListAwardingType.Any(x => !x.Id.Equals(param.AwardingTypeId) && x.Name.ToLower().Equals(param.AwardingTypeName.ToLower())))
            {
                throw new SMXException($"Hình thức khen thưởng {param.AwardingTypeName} đã tồn tại, không thể sửa");
            }
            else
            {
                string sql = @"UPDATE [dbo].[AwardingType] SET Name = @name, UpdateDTG = @updateDTG WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@name", param.AwardingTypeName);
                    cmd.Parameters.AddWithValue("@updateDTG", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id", param.AwardingTypeId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CreateAwardingType(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingType] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingType = dataContext.ExecuteSelect<AwardingType>(all);
            }

            if (param.ListAwardingType.Any(x => x.Name.ToLower().Equals(param.AwardingType.Name.ToLower())))
            {
                throw new SMXException($"Hình thức khen thưởng {param.AwardingType.Name} đã tồn tại, không thể thêm");
            }
            else
            {
                string sql = @"INSERT INTO [dbo].[AwardingType] (Id, Name, CreatedDTG, CreateUserId, isDeleted)
                            VALUES (@id, @name, @createdDTG, @createUserId, @isDeleted)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@id", param.AwardingType.Id);
                    cmd.Parameters.AddWithValue("@name", param.AwardingType.Name);
                    cmd.Parameters.AddWithValue("@createdDTG", param.AwardingType.CreatedDTG);
                    cmd.Parameters.AddWithValue("@createUserId", param.AwardingType.CreateUserId);
                    cmd.Parameters.AddWithValue("@isDeleted", param.AwardingType.isDeleted);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GetListAwardingType(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingType] WHERE isDeleted = 0 ";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingType = ExecutePaging<AwardingType>(dataContext, cmd, "CreatedDTG desc", param.PagingInfo);
            }
        }

        public void GetListAwardingTypeNoPaging(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingType] WHERE isDeleted = 0 ";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingType = dataContext.ExecuteSelect<AwardingType>(cmd);
            }
        }

        public void GetAwardingTypeCount(EmulationAndRewardParam param)
        {
            string sql = @"SELECT COUNT(*) FROM [dbo].[AwardingType] WHERE isDeleted = 0 ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Execute the query and get the count value
                    param.AwardingTypeId = (int)cmd.ExecuteScalar() + 1;
                }
            }
        }

        public void EditAwardingPeriod(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingPeriod] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingPeriod = dataContext.ExecuteSelect<AwardingPeriod>(all);
            }

            if (param.ListAwardingPeriod.Any(x => !x.Id.Equals(param.AwardingPeriodId) && x.Name.ToLower().Equals(param.AwardingPeriodName.ToLower())))
            {
                throw new SMXException($"Đợt khen thưởng {param.AwardingPeriodName} đã tồn tại, không thể sửa");
            }
            else
            {
                string sql = @"UPDATE [dbo].[AwardingPeriod] SET Name = @name, AwardingTime = @awardingTime, UpdateDTG = @updateDTG WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@name", param.AwardingPeriodName);
                    cmd.Parameters.AddWithValue("@updateDTG", DateTime.Now);
                    cmd.Parameters.AddWithValue("@awardingTime", param.AwardingTime);
                    cmd.Parameters.AddWithValue("@Id", param.AwardingPeriodId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSelectedAwardingPeriod(EmulationAndRewardParam param)
        {
            string sql = @"DELETE FROM [dbo].[AwardingPeriod] WHERE Id = @Id AND isDeleted = 0";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", param.AwardingPeriodId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GetAwardingPeriodById(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingPeriod] WHERE isDeleted = 0 AND Id = @Id";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Id", param.AwardingPeriodId);

            using (DataContext dataContext = new DataContext())
            {
                param.AwardingPeriod = dataContext.ExecuteSelect<AwardingPeriod>(cmd).FirstOrDefault();
            }
        }

        public void GetListAwardingPeriod(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingPeriod] WHERE isDeleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingPeriod = ExecutePaging<AwardingPeriod>(dataContext, cmd, "CreatedDTG desc", param.PagingInfo);
            }
        }

        public void GetListAwardingPeriodNoPaging(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingPeriod] WHERE isDeleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingPeriod = dataContext.ExecuteSelect<AwardingPeriod>(cmd);
            }
        }

        public void CreateAwardingPeriod(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingPeriod] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingPeriod = dataContext.ExecuteSelect<AwardingPeriod>(all);
            }

            if (param.ListAwardingPeriod.Any(x => x.Name.ToLower().Equals(param.AwardingPeriod.Name.ToLower())))
            {
                throw new SMXException($"Đợt khen thưởng {param.AwardingPeriod.Name} đã tồn tại, không thể thêm");
            }
            else
            {
                string sql = @"INSERT INTO [dbo].[AwardingPeriod] (Name, AwardingTime, CreatedDTG, CreateUserId, isDeleted)
                            VALUES (@name, @awardingTime, @createdDTG, @createUserId, @isDeleted)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@name", param.AwardingPeriod.Name);
                    cmd.Parameters.AddWithValue("@awardingTime", param.AwardingPeriod.AwardingTime);
                    cmd.Parameters.AddWithValue("@createdDTG", param.AwardingPeriod.CreatedDTG);
                    cmd.Parameters.AddWithValue("@createUserId", param.AwardingPeriod.CreateUserId);
                    cmd.Parameters.AddWithValue("@isDeleted", param.AwardingPeriod.isDeleted);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSelectedAwardingLevel(EmulationAndRewardParam param)
        {
            string sql = @"DELETE FROM [dbo].[AwardingLevel] WHERE Id = @Id AND isDeleted = 0";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", param.AwardingLevelId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditAwardingLevel(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingLevel] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingLevel = dataContext.ExecuteSelect<AwardingLevel>(all);
            }

            if (param.ListAwardingLevel.Any(x => !x.Id.Equals(param.AwardingLevelId) && x.Level.Equals(param.Level)))
            {
                throw new SMXException($"Cấp đánh giá {param.Level} đã tồn tại, không thể sửa");
            }
            else
            {
                string sql = @"UPDATE [dbo].[AwardingLevel] SET Level = @level, Description = @description, Category = @category, UpdateDTG = @updateDTG WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@level", param.Level);
                    cmd.Parameters.AddWithValue("@updateDTG", DateTime.Now);
                    cmd.Parameters.AddWithValue("@description", param.AwardingLevelDescription);
                    cmd.Parameters.AddWithValue("@category", param.AwardingLevelCategory);
                    cmd.Parameters.AddWithValue("@Id", param.AwardingLevelId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GetAwardingLevelById(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingLevel] WHERE Id = @Id AND isDeleted = 0";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Id", param.AwardingLevelId);

            using (DataContext dataContext = new DataContext())
            {
                param.AwardingLevel = dataContext.ExecuteSelect<AwardingLevel>(cmd).FirstOrDefault();
            }
        }

        public void CreateAwardingLevel(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingLevel] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingLevel = dataContext.ExecuteSelect<AwardingLevel>(all);
            }

            if (param.ListAwardingLevel.Any(x => x.Level.Equals(param.AwardingLevel.Level)))
            {
                throw new SMXException($"Cấp đánh giá {param.AwardingLevel.Level} đã tồn tại, không thể thêm");
            }
            else
            {
                string sql = @"INSERT INTO [dbo].[AwardingLevel] (Level, Description, Category, CreatedDTG, CreateUserId, isDeleted)
                            VALUES (@level, @description, @Category, @createdDTG, @createUserId, @isDeleted)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@level", param.AwardingLevel.Level);
                    cmd.Parameters.AddWithValue("@description", param.AwardingLevel.Description);
                    cmd.Parameters.AddWithValue("@category", param.AwardingLevel.Category);
                    cmd.Parameters.AddWithValue("@createdDTG", param.AwardingLevel.CreatedDTG);
                    cmd.Parameters.AddWithValue("@createUserId", param.AwardingLevel.CreateUserId);
                    cmd.Parameters.AddWithValue("@isDeleted", param.AwardingLevel.isDeleted);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void GetListAwardingLevel(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingLevel] WHERE isDeleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingLevel = ExecutePaging<AwardingLevel>(dataContext, cmd, "CreatedDTG desc", param.PagingInfo);
            }
        }

        public void GetListAwardingLevelNoPaging(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingLevel] WHERE isDeleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingLevel = dataContext.ExecuteSelect<AwardingLevel>(cmd);
            }
        }

        public void GetAwardingCatalogById(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingCatalog] WHERE Id = @Id AND isDeleted = 0";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Id", param.AwardingCatalogId);

            using (DataContext dataContext = new DataContext())
            {
                param.AwardingCatalog = dataContext.ExecuteSelect<AwardingCatalog>(cmd).FirstOrDefault();
            }
        }

        public void DeleteSelectedAwardingCatalog(EmulationAndRewardParam param)
        {
            string sql = @"DELETE FROM [dbo].[AwardingCatalog] WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", param.AwardingCatalogId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GetListAwardingCatalog(EmulationAndRewardParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AwardingCatalog] WHERE isDeleted = 0 ";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingCatalog = ExecutePaging<AwardingCatalog>(dataContext, cmd, "CreatedDTG desc", param.PagingInfo);
            }
        }

        public void EditAwardingCatalog(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingCatalog] WHERE isDeleted = 0";

            using (DataContext dataContext = new DataContext())
            {
                param.ListAwardingCatalog = dataContext.ExecuteSelect<AwardingCatalog>(all);
            }

            if (param.ListAwardingCatalog.Any(x => !x.Id.Equals(param.AwardingCatalogId) && x.Name.ToLower().Equals(param.AwardingCatalogName.ToLower())))
            {
                throw new SMXException($"Tên danh mục {param.AwardingCatalogName} đã tồn tại, không thể sửa");
            }
            else
            {
                string sql = @"UPDATE [dbo].[AwardingCatalog] SET Name = @name, UpdateDTG = @updateDTG WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@name", param.AwardingCatalogName);
                    cmd.Parameters.AddWithValue("@updateDTG", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Id", param.AwardingCatalogId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddNewAwardingCatalog(EmulationAndRewardParam param)
        {
            string all = @"SELECT * FROM [dbo].[AwardingCatalog] WHERE isDeleted = 0";

            using(DataContext dataContext = new DataContext())
            {
                param.ListAwardingCatalog = dataContext.ExecuteSelect<AwardingCatalog>(all);
            }

            if (param.ListAwardingCatalog.Any(x => x.Name.ToLower().Equals(param.AwardingCatalog.Name.ToLower())))
            {
                throw new SMXException($"Tên danh mục {param.AwardingCatalog.Name} đã tồn tại, không thể thêm");
            }
            else
            {
                string sql = @"INSERT INTO [dbo].[AwardingCatalog] (Name, CreatedDTG, CreateUserId, isDeleted)
                            VALUES (@name, @createdDTG, @createUserId, @isDeleted)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@name", param.AwardingCatalog.Name);
                    cmd.Parameters.AddWithValue("@createdDTG", param.AwardingCatalog.CreatedDTG);
                    cmd.Parameters.AddWithValue("@createUserId", param.AwardingCatalog.CreateUserId);
                    cmd.Parameters.AddWithValue("@isDeleted", param.AwardingCatalog.isDeleted);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<er_EmulationAndRewardSubject> SetupFormDisplay(er_EmulationAndReward filter)
        {
            string cmdText = @"select dt.* from (
                                select
                                    sub.*
                                    , STUFF((
	                                    SELECT ',' + erEAR.EmulationAndRewardUnit
	                                    FROM 
		                                    er_EmulationAndReward erEAR
	                                    WHERE
		                                   ',' + CAST(sub.EmulationAndRewardID AS NVARCHAR(256)) + ',' LIKE '%,' + CAST(erEAR.EmulationAndRewardID AS NVARCHAR(256)) + ',%'
	                                    FOR XML PATH('')
                                    ), 1, 0, '' ) AS EmulationAndRewardUnit
                                    , STUFF((
	                                    SELECT ',' + cast(erEAR.Year as nvarchar(256))
	                                    FROM 
		                                    er_EmulationAndReward erEAR
	                                    WHERE
		                                    ',' + CAST(sub.EmulationAndRewardID AS NVARCHAR(256)) + ',' LIKE '%,' + CAST(erEAR.EmulationAndRewardID AS NVARCHAR(256)) + ',%'
	                                    FOR XML PATH('')
                                    ), 1, 0, '' ) AS Year
                                    , STUFF((
	                                    SELECT ',' + erEAR.Event
	                                    FROM 
		                                    er_EmulationAndReward erEAR
	                                    WHERE
		                                    ',' + CAST(sub.EmulationAndRewardID AS NVARCHAR(256)) + ',' LIKE '%,' + CAST(erEAR.EmulationAndRewardID AS NVARCHAR(256)) + ',%'
	                                    FOR XML PATH('')
                                    ), 1, 0, '' ) AS Event
                                from er_EmulationAndRewardSubject sub
                                where sub.Deleted = 0) dt
                                where dt.Deleted = 0 ";

            SqlCommand cmd = new SqlCommand();

            if (filter != null)
            {
                if (filter.Year != null)
                    cmdText += " and dt.Year like @Year ";

                if (!string.IsNullOrWhiteSpace(filter.Event))
                    cmdText += " and dt.Event like @Event ";

                if (!string.IsNullOrWhiteSpace(filter.EmulationAndRewardUnit))
                    cmdText += " and dt.EmulationAndRewardUnit like @EmulationAndRewardUnit ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    cmdText += " and (dt.Name like @TextSearch or dt.LatestTitle like @TextSearch or dt.Email like @TextSearch or dt.Mobile like @TextSearch) ";

                cmd.Parameters.AddWithValue("@Year", BuildLikeFilter(Utils.Utility.GetString(filter.Year)));
                cmd.Parameters.AddWithValue("@Event", BuildLikeFilter(filter.Event));
                cmd.Parameters.AddWithValue("@TextSearch", BuildLikeFilter(filter.TextSearch));
                cmd.Parameters.AddWithValue("@EmulationAndRewardUnit", BuildLikeFilter(filter.EmulationAndRewardUnit));
            }

            cmd.CommandText = cmdText;

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<er_EmulationAndRewardSubject>(cmd);
            }
        }

        public List<er_EmulationAndReward> BuildTreeListEmulationAndRewards()
        {
            string query = @"select
	                            MAX(a.Year) as Year
	                            , a.EmulationAndRewardUnit
                            from er_EmulationAndReward a
                            where a.Deleted = 0
                            and isnull(a.EmulationAndRewardUnit, '') <> ''
                            group by a.EmulationAndRewardUnit
                            order by MAX(a.Year) desc ";

            SqlCommand command = new SqlCommand(query);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<er_EmulationAndReward>(command);
            }
        }

        public List<er_EmulationAndRewardSubject> GetListEmulationAndRewardSubjectByFilter(er_EmulationAndReward filter, PagingInfo pagingInfo)
        {
            string cmdText = @"select
                                   dt.*
                               from er_EmulationAndRewardSubject dt
                               where dt.Deleted = 0 ";

            SqlCommand cmd = new SqlCommand();

            if (filter != null)
            {
                if (filter.SubjectType != null)
                    cmdText += " and dt.Type = @SubjectRewardedType ";

                if (filter.Year != null)
                    cmdText += " and exists (select top 1 * from er_EmulationAndReward erEAR where ',' + CAST(dt.EmulationAndRewardID AS NVARCHAR(256)) + ',' LIKE '%,' + CAST(erEAR.EmulationAndRewardID AS NVARCHAR(256)) + ',%'  and Year like @Year) ";

                if (!string.IsNullOrWhiteSpace(filter.Event))
                    cmdText += " and exists (select top 1 * from er_EmulationAndReward erEAR where ',' + CAST(dt.EmulationAndRewardID AS NVARCHAR(256)) + ',' LIKE '%,' + CAST(erEAR.EmulationAndRewardID AS NVARCHAR(256)) + ',%'  and Event like @Event) ";

                if (!string.IsNullOrWhiteSpace(filter.EmulationAndRewardUnit))
                    cmdText += " and exists (select top 1 * from er_EmulationAndReward erEAR where ',' + CAST(dt.EmulationAndRewardID AS NVARCHAR(256)) + ',' LIKE '%,' + CAST(erEAR.EmulationAndRewardID AS NVARCHAR(256)) + ',%'  and EmulationAndRewardUnit like @EmulationAndRewardUnit) ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    cmdText += " and (dt.Name like @TextSearch or dt.LatestTitle like @TextSearch or dt.Email like @TextSearch or dt.Mobile like @TextSearch) ";

                cmd.Parameters.AddWithValue("@Year", BuildLikeFilter(Utils.Utility.GetString(filter.Year)));
                cmd.Parameters.AddWithValue("@Event", BuildLikeFilter(filter.Event));
                cmd.Parameters.AddWithValue("@SubjectRewardedType", filter.SubjectType);
                cmd.Parameters.AddWithValue("@TextSearch", BuildLikeFilter(filter.TextSearch));
                cmd.Parameters.AddWithValue("@EmulationAndRewardUnit", BuildLikeFilter(filter.EmulationAndRewardUnit));
            }

            cmd.CommandText = cmdText;

            using (DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<er_EmulationAndRewardSubject>(dataContext, cmd, "Code", pagingInfo);
            }
        }

        public List<er_EmulationAndRewardHistory> GetListEmulationAndRewardHistory(int? subjectID)
        {
            string cmdText = @"select 
                                   his.*
                               from er_EmulationAndRewardHistory his
                               where his.EmulationAndRewardSubjectID = @EmulationAndRewardSubjectID ";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@EmulationAndRewardSubjectID", subjectID);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<er_EmulationAndRewardHistory>(cmd);
            }
        }

        public er_EmulationAndRewardSubject GetEmulationAndRewardSubjectByCode(string code)
        {
            string cmdText = @"select 
                                   sub.*
                               from er_EmulationAndRewardSubject sub
                               where sub.Deleted = 0 and sub.Code = @Code ";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@Code", code);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<er_EmulationAndRewardSubject>(cmd).FirstOrDefault();
            }
        }
    }
}