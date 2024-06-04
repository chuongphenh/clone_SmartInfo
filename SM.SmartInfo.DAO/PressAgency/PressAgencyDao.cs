using System.Linq;
using System.Data.SqlClient;
using SoftMart.Kernel.Entity;
using SM.SmartInfo.DAO.Common;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using static SM.SmartInfo.SharedComponent.Constants.SMX;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Diagnostics.Eventing.Reader;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System;

namespace SM.SmartInfo.DAO.PressAgency
{
    public class PressAgencyDao : BaseDao
    {
        #region Modification methods

        private string ConnectionString = ConfigUtils.ConnectionString;

        public void DeleteShare(PressAgencyParam param)
        {
            string sql = @"UPDATE SharingManagement SET isShared = 0 WHERE UserId = @userID AND PressAgencyHRID = @pressAgencyHRID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@pressAgencyHRID", param.pressAgencyHRID);
            cmd.Parameters.AddWithValue("@userID", param.UserId);

            using (DataContext context = new DataContext())
            {
                context.ExecuteSelect<SharingManagement>(cmd);
            }
        }

        public List<Employee> GetListSharedUser(PressAgencyParam param)
        {
            string sql = @"SELECT e.EmployeeID, e.Name, e.Email, e.Description FROM [dbo].[adm_Employee] e
                            JOIN [dbo].[SharingManagement] sm on sm.UserId = e.EmployeeID
                            WHERE sm.isShared = 1 AND sm.PressAgencyHRID = @pressAgencyHRID ";

            if (!string.IsNullOrEmpty(param.txtSearchUserShared))
            {
                sql += @" AND (e.Email LIKE '%' + @txtSearch + '%' OR e.Name LIKE '%' + @txtSearch + '%' OR e.Description LIKE '%' + @txtSearch + '%')";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@pressAgencyHRID", param.pressAgencyHRID);
            cmd.Parameters.AddWithValue("@txtSearch", param.txtSearchUserShared);
            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<Employee>(cmd);
            }
        }
        public AgencyType GetPressAgencyTypeByID(int Id)
        {
            string sql = @"SELECT * FROM [dbo].[AgencyType] WHERE Id = @ID";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@ID", Id);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<AgencyType>(cmd).FirstOrDefault();
            }
        }

        public AgencyType GetPressAgencyTypeByCode(PressAgencyParam param)
        {
            string sql = @"SELECT * FROM [dbo].[AgencyType] WHERE TRIM(LOWER(Code)) = TRIM(LOWER(@PressAgencyTypeCode))";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@PressAgencyTypeCode", param.PressAgencyTypeCode);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<AgencyType>(cmd).FirstOrDefault();
            }
        }

        public agency_PressAgencyHR GetPressAgencyHRByName(PressAgencyParam param)
        {
            string sql = @"SELECT * FROM[dbo].[agency_PressAgencyHR] WHERE TRIM(LOWER(FullName)) = TRIM(LOWER(PressAgencyHRName)) AND Deleted = 0 ";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@PressAgencyHRName", param.PressAgencyHRName);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd).FirstOrDefault();
            }
        }
        //
        public List<agency_PressAgencyHR> GetListPressAgencyHRByName(PressAgencyParam param)
        {
            string sql = @"SELECT* FROM[dbo].[agency_PressAgencyHR] WHERE TRIM(LOWER(FullName)) = TRIM(LOWER(@PressAgencyHRName)) AND Deleted = 0 ";
            

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@PressAgencyHRName", param.PressAgencyHRName);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }
        //
        public agency_PressAgency GetPressAgencyByName(PressAgencyParam param)
        {
            string sql = @"SELECT * FROM [dbo].[agency_PressAgency] WHERE TRIM(LOWER(Name)) = TRIM(LOWER(@agencyName)) AND Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@agencyName", param.PressAgencyName);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd).FirstOrDefault();
            }
        }

        // new code _PH
        public void InsertNamePermissionGroups(string groupName, string pressAgencyHRID)
        {
            string sqlQuery = @"INSERT INTO SharingManagement (PressAgencyHRID, CreatedDTG, isShared, GroupName) 
                                VALUES(@PressAgencyHRID, @CreatedDTG, @isShared, @GroupName)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);
                command.Parameters.AddWithValue("@CreatedDTG", DateTime.Now);
                command.Parameters.AddWithValue("@isShared", 1);
                command.Parameters.AddWithValue("@GroupName", groupName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int InsertPressAgencyHRAndGetID(agency_PressAgencyHR agencyHR)
        {
            string sqlQuery = @"INSERT INTO agency_PressAgencyHR (PressAgencyID, FullName, Position, DOB, Mobile, Address, Email, Hobby, RelatedInformation, Attitude,  Deleted, Version, CreatedBy, CreatedDTG, UpdatedBy, UpdatedDTG) 
                        VALUES (@PressAgencyID, @FullName, @Position, @DOB, @Mobile, @Address, @Email, @Hobby, @RelatedInformation, @Attitude,  @Deleted, @Version, @CreatedBy, @CreatedDTG, @UpdatedBy, @UpdatedDTG);
                        SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@PressAgencyID", agencyHR.PressAgencyID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FullName", agencyHR.FullName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Position", agencyHR.Position ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DOB", agencyHR.DOB ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Mobile", agencyHR.Mobile ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", agencyHR.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", agencyHR.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Hobby", agencyHR.Hobby ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RelatedInformation", agencyHR.RelatedInformation ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Attitude", agencyHR.Attitude ?? (object)DBNull.Value);
                //command.Parameters.AddWithValue("@Image", agencyHR.Image ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Deleted", agencyHR.Deleted ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Version", agencyHR.Version ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedBy", agencyHR.CreatedBy ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedDTG", agencyHR.CreatedDTG ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedBy", agencyHR.UpdatedBy ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedDTG", agencyHR.UpdatedDTG ?? (object)DBNull.Value);

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }
            }
        }
        public void DeleteNamePermissionGroups(int? hrId)
        {
            string sqlQuery = @"DELETE FROM SharingManagement WHERE PressAgencyHRID = @PressAgencyHRID AND [UserId] IS NULL AND [UserEmail] IS NULL";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@PressAgencyHRID", hrId ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        //
        public void ImportOrUpdateListPressAgencyHRFromExcel(PressAgencyParam param)
        {
            string sql = @"SELECT * FROM [dbo].[agency_PressAgencyHR] WHERE Deleted = 0";
            List<agency_PressAgencyHR> listHR = new List<agency_PressAgencyHR>();
            SqlCommand cmd = new SqlCommand(sql);
            using (DataContext context = new DataContext())
            {
                listHR = context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
            List<agency_PressAgencyHR> listNew = new List<agency_PressAgencyHR>();
            List<agency_PressAgencyHR> listExisting = new List<agency_PressAgencyHR>();

            // Loại bỏ các bản ghi trùng lặp trong danh sách mới dựa trên FullName và Email
            var distinctListPressAgencyHR = param.ListPressAgencyHR
                .GroupBy(x => new { x.FullName, x.Email })
                .Select(g => g.First())
                .ToList();

            var temp = distinctListPressAgencyHR.Select(x =>
            {
                var dataTemp = listHR.FirstOrDefault(p => p.FullName.Equals(x.FullName) && p.Email.Equals(x.Email));
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
            #region Unused code
            //if (listExisting.Count > 0)
            //{
            //    throw new SMXException("Không thể import file, do dữ liệu người dùng đã tồn tại");
            //}
            //else
            //{
            //    if (listNew.Count > 0)
            //    {
            //        foreach (var item in listNew)
            //        {
            //            string sqlCommand = @"INSERT INTO [dbo].[agency_PressAgencyHR] (PressAgencyID, FullName, Position, DOB, Mobile, Email, Hobby, RelatedInformation, Attitude, Address, Deleted, CreatedBy, CreatedDTG)
            //                    VALUES (@PressAgencyID, @FullName, @Position, @DOB, @Mobile, @Email, @Hobby, @RelatedInformation, @Attitude, @Address, @Deleted, @CreatedBy, @CreatedDTG)";

            //            using (SqlConnection connection = new SqlConnection(ConnectionString))
            //            {
            //                SqlCommand command = new SqlCommand(sqlCommand, connection);
            //                command.Parameters.AddWithValue("@PressAgencyID", item.PressAgencyID);
            //                command.Parameters.AddWithValue("@FullName", item.FullName);
            //                command.Parameters.AddWithValue("@Position", item.Position);
            //                command.Parameters.AddWithValue("@DOB", item.DOB);
            //                command.Parameters.AddWithValue("@Mobile", item.Mobile);
            //                command.Parameters.AddWithValue("@Email", item.Email);
            //                command.Parameters.AddWithValue("@Hobby", item.Hobby);
            //                command.Parameters.AddWithValue("@RelatedInformation", item.RelatedInformation);
            //                command.Parameters.AddWithValue("@Attitude", item.Attitude);
            //                command.Parameters.AddWithValue("@Address", item.Address);
            //                command.Parameters.AddWithValue("@Deleted", item.Deleted);
            //                command.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);
            //                command.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG);

            //                connection.Open();
            //                command.ExecuteNonQuery();
            //            }
            //        }
            //    }
            //}
            #endregion

            // Thêm bản ghi mới
            if (listNew.Count > 0)
            {
                foreach (var item in listNew)
                {
                    //using (DataContext dataContext = new DataContext())
                    //{
                    //    dataContext.InsertItem<agency_PressAgencyHR>(item);
                    //}
                    int hrId = -1;
                    try
                    {
                        hrId = InsertPressAgencyHRAndGetID(item);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogError("ERROR: DANH BẠ _ Insert nhóm quyền cho PressAngcyHR khi import Excel " + ex.ToString());
                        continue;
                    }

                    //Thêm nhóm quyền của PressAgencyHR
                    try
                    {
                        foreach (var itemNamePermissionGroup in item.NamePermissionGroups)
                        {
                            if (hrId == -1)
                                continue;
                            InsertNamePermissionGroups(itemNamePermissionGroup, hrId.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogError("ERROR: DANH BẠ _ Insert nhóm quyền cho PressAngcyHR khi import Excel" + ex.ToString());
                    }
                    #region  Unused code
                    //string sqlCommand = @"INSERT INTO [dbo].[agency_PressAgencyHR] (PressAgencyID, FullName, Position, DOB, Mobile, Email, Hobby, RelatedInformation, Attitude, Address, Deleted, CreatedBy, CreatedDTG)
                    //            VALUES (@PressAgencyID, @FullName, @Position, @DOB, @Mobile, @Email, @Hobby, @RelatedInformation, @Attitude, @Address, @Deleted, @CreatedBy, @CreatedDTG)";

                    //using (SqlConnection connection = new SqlConnection(ConnectionString))
                    //{
                    //    SqlCommand command = new SqlCommand(sqlCommand, connection);

                    //    command.Parameters.AddWithValue("@PressAgencyID", item.PressAgencyID ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@FullName", item.FullName ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Position", item.Position ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@DOB", item.DOB ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Mobile", item.Mobile ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Email", item.Email ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Hobby", item.Hobby ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@RelatedInformation", item.RelatedInformation ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Attitude", item.Attitude ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Address", item.Address ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Deleted", item.Deleted ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@CreatedBy", item.CreatedBy ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG ?? (object)DBNull.Value);

                    //    connection.Open();
                    //    command.ExecuteNonQuery();
                    //    using (DataContext dataContext = new DataContext())
                    //    {
                    //        dataContext.InsertItem<agency_PressAgencyHR>(item);
                    //    }
                    //}
                    #endregion
                }

            }
            // Cập nhật bản ghi đã tồn tại
            if (listExisting.Count > 0)
            {
                foreach (var item in listExisting)
                {
                    using (DataContext dataContext = new DataContext())
                    {
                        dataContext.UpdateItem<agency_PressAgencyHR>(item);
                    }

                    //Cập nhật nhóm quyền
                    try
                    {
                        // xoá nhóm quyền cũ
                        DeleteNamePermissionGroups(item.PressAgencyHRID);
                        //Cập nhật
                        foreach (var itemNamePermissionGroup in item.NamePermissionGroups)
                        {
                            if (item.PressAgencyHRID == null)
                                continue;
                            InsertNamePermissionGroups(itemNamePermissionGroup, item.PressAgencyHRID.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WebLogger.LogError("ERROR: DANH BẠ _ Insert nhóm quyền cho PressAngcyHR khi import Excel");
                    }
                    #region Unused code
                    //string sqlCommand = @"UPDATE [dbo].[agency_PressAgencyHR] SET 
                    //                        PressAgencyID=@PressAgencyID, FullName=@FullName, Position=@Position, DOB=@DOB, Mobile=@Mobile, 
                    //                        Email=@Email, Hobby=@Hobby, RelatedInformation=@RelatedInformation, Attitude=@Attitude, 
                    //                        Address=@Address, Deleted=0, CreatedBy=@CreatedBy, CreatedDTG=@CreatedDTG WHERE Email = @Email OR Mobile = @Mobile OR FullName = @FullName";

                    //using (SqlConnection connection = new SqlConnection(ConnectionString))
                    //{
                    //    SqlCommand command = new SqlCommand(sqlCommand, connection);
                    //    command.Parameters.AddWithValue("@PressAgencyID", item.PressAgencyID ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@FullName", item.FullName ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Position", item.Position ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@DOB", item.DOB ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Mobile", item.Mobile ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Email", item.Email ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Hobby", item.Hobby ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@RelatedInformation", item.RelatedInformation ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Attitude", item.Attitude ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@Address", item.Address ?? (object)DBNull.Value);

                    //    command.Parameters.AddWithValue("@CreatedBy", item.CreatedBy ?? (object)DBNull.Value);
                    //    command.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG ?? (object)DBNull.Value);


                    //    //command.Parameters.AddWithValue("@PressAgencyID", item.PressAgencyID);
                    //    //command.Parameters.AddWithValue("@FullName", item.FullName);
                    //    //command.Parameters.AddWithValue("@Position", item.Position);
                    //    //command.Parameters.AddWithValue("@DOB", item.DOB);
                    //    //command.Parameters.AddWithValue("@Mobile", item.Mobile);
                    //    //command.Parameters.AddWithValue("@Email", item.Email);
                    //    //command.Parameters.AddWithValue("@Hobby", item.Hobby);
                    //    //command.Parameters.AddWithValue("@RelatedInformation", item.RelatedInformation);
                    //    //command.Parameters.AddWithValue("@Attitude", item.Attitude);
                    //    //command.Parameters.AddWithValue("@Address", item.Address);
                    //    //command.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);
                    //    //command.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG);

                    //    connection.Open();
                    //    command.ExecuteNonQuery();


                    //}
                    #endregion
                }
            }
        }

        public List<AgencyType> GetListAgencyType(PressAgencyParam param)
        {
            string sql = @"select * from AgencyType";
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<AgencyType>(sql);
            }
        }

        public void InsertPressAgency(agency_PressAgency item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<agency_PressAgency>(item);
            }
        }

        public void UpdatePressAgency(agency_PressAgency item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<agency_PressAgency>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(Messages.ItemNotExitOrChanged);
            }
        }

        #endregion

        public List<agency_PressAgency> GetListPressAgencyByType(PressAgencyParam param)
        {
            string sql = @"SELECT * FROM agency_PressAgency WHERE Type = @type AND Deleted = @IsNotDeleted";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@type", param.PressAgencyType);
            cmd.Parameters.AddWithValue("@IsNotDeleted", smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public void InsertAgencyTypeItem(AgencyType item)
        {
            using (DataContext context = new DataContext())
            {
                context.InsertItem<AgencyType>(item);
            }
        }

        public List<agency_PressAgency> SetupFormDefault(string txtSearch)
        {
            string sql;
            if (!string.IsNullOrEmpty(txtSearch))
            {
                sql = $@"SELECT COUNT(pa.PressAgencyID) as CountByType, at.Id as Type
                            FROM [dbo].[AgencyType] at
                            LEFT JOIN (
                                SELECT ap.*
                                FROM [dbo].[agency_PressAgency] ap
                                WHERE ap.Deleted = @IsNotDeleted AND ap.Type IS NOT NULL AND 
                                        (ap.Name like N'%{txtSearch}%'
                                        or ap.Agency like N'%{txtSearch}%'
                                        or ap.CertNo like N'%{txtSearch}%'
                                        or ap.Phone like N'%{txtSearch}%'
                                        or ap.Fax like N'%{txtSearch}%'
                                        or ap.Email like N'%{txtSearch}%'
                                        or ap.Address like N'%{txtSearch}%'
                                        or ap.Rate like N'%{txtSearch}%'
                                        or ap.Note like N'%{txtSearch}%'
                                        or ap.ChiefEditor like N'%{txtSearch}%')) as pa ON at.Id = pa.Type
                            GROUP BY at.Id

                            UNION ALL

                            SELECT COUNT(*) as CountByType, 0
                            FROM [dbo].[agency_PressAgency]
                            WHERE Deleted = @IsNotDeleted AND Type IS NOT NULL AND 
                                        (Name like N'%{txtSearch}%'
                                        or Agency like N'%{txtSearch}%'
                                        or CertNo like N'%{txtSearch}%'
                                        or Phone like N'%{txtSearch}%'
                                        or Fax like N'%{txtSearch}%'
                                        or Email like N'%{txtSearch}%'
                                        or Address like N'%{txtSearch}%'
                                        or Rate like N'%{txtSearch}%'
                                        or Note like N'%{txtSearch}%'
                                        or ChiefEditor like N'%{txtSearch}%')";
            }
            else
            {
                sql = $@"SELECT COUNT(pa.PressAgencyID) as CountByType, at.Id as Type
                            FROM [dbo].[AgencyType] at
                            LEFT JOIN (
                                SELECT ap.*
                                FROM [dbo].[agency_PressAgency] ap
                                WHERE ap.Deleted = @IsNotDeleted AND ap.Type IS NOT NULL) as pa ON at.Id = pa.Type
                            GROUP BY at.Id

                            UNION ALL

                            SELECT COUNT(*) as CountByType, 0
                            FROM [dbo].[agency_PressAgency]
                            WHERE Deleted = @IsNotDeleted AND Type IS NOT NULL ";
            }

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@SearchText", txtSearch);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgency(string searchText, PagingInfo pagingInfo)
        {
            string sql = @"select * from agency_PressAgency
                        where Deleted = @IsNotDeleted ";

            if (!string.IsNullOrWhiteSpace(searchText))
                sql += " and (Name like @SearchText " +
                    "or Agency like @SearchText " +
                    "or CertNo like @SearchText " +
                    "or Phone like @SearchText " +
                    "or Fax like @SearchText " +
                    "or Email like @SearchText " +
                    "or Address like @SearchText " +
                    "or ChiefEditor like @SearchText)";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(searchText));

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<agency_PressAgency>(context, cmd, "PressAgencyID desc", pagingInfo);
            }
        }

        #region Search

        public List<agency_PressAgency> SearchPressAgency(agency_PressAgency filter)
        {
            string sql = @"select * from agency_PressAgency
                        where Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.Type != null && filter.Type != PressAgencyType.All)
                    sql += " and Type = @Type ";

                if (filter.FromEstablishedDTG != null)
                    sql += " and DATEDIFF(DAY, @FromEstablishedDTG, EstablishedDTG) >= 0 ";

                if (filter.ToEstablishedDTG != null)
                    sql += " and DATEDIFF(DAY, EstablishedDTG, @ToEstablishedDTG) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (Name like @SearchText " +
                        "or Agency like @SearchText " +
                        "or CertNo like @SearchText " +
                        "or Phone like @SearchText " +
                        "or Fax like @SearchText " +
                        "or Email like @SearchText " +
                        "or Address like @SearchText " +
                        "or Rate like @SearchText " +
                        "or Note like @SearchText " +
                        "or ChiefEditor like @SearchText)";
            }

            sql += @" ORDER BY Name";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@FromEstablishedDTG", filter.FromEstablishedDTG);
            cmd.Parameters.AddWithValue("@ToEstablishedDTG", filter.ToEstablishedDTG);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", filter.Type);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyHR_GetPressAgencyID(agency_PressAgencyHR filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join agency_PressAgencyHR paHR on paHR.PressAgencyID = pa.PressAgencyID and paHR.Deleted = @IsNotDeleted
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.Attitude != null)
                    sql += " and paHR.Attitude = @Attitude ";

                if (filter.FromDOB != null)
                    sql += " and DATEDIFF(DAY, @FromDOB, paHR.DOB) >= 0 ";

                if (filter.ToDOB != null)
                    sql += " and DATEDIFF(DAY, paHR.DOB, @ToDOB) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHR.FullName like @SearchText " +
                        "or paHR.Position like @SearchText " +
                        "or paHR.Mobile like @SearchText " +
                        "or paHR.Email like @SearchText " +
                        "or paHR.Hobby like @SearchText " +
                        "or paHR.RelatedInformation like @SearchText " +
                        "or paHR.Address like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@FromDOB", filter.FromDOB);
            cmd.Parameters.AddWithValue("@ToDOB", filter.ToDOB);
            cmd.Parameters.AddWithValue("@Attitude", filter.Attitude);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgencyHR> SearchPressAgencyHR(agency_PressAgencyHR filter)
        {
            string sql = @"select paHR.*, pa.Name as PressAgencyName
                            from agency_PressAgencyHR paHR 
                            left join agency_PressAgency pa on paHR.PressAgencyID = pa.PressAgencyID and pa.Deleted = @IsNotDeleted
                            where paHR.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.Attitude != null)
                    sql += " and paHR.Attitude = @Attitude ";

                if (filter.FromDOB != null)
                    sql += " and DATEDIFF(DAY, @FromDOB, paHR.DOB) >= 0 ";

                if (filter.ToDOB != null)
                    sql += " and DATEDIFF(DAY, paHR.DOB, @ToDOB) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHR.FullName like @SearchText " +
                        "or paHR.Position like @SearchText " +
                        "or paHR.Mobile like @SearchText " +
                        "or paHR.Email like @SearchText " +
                        "or paHR.Hobby like @SearchText " +
                        "or paHR.RelatedInformation like @SearchText " +
                        "or paHR.Address like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@FromDOB", filter.FromDOB);
            cmd.Parameters.AddWithValue("@ToDOB", filter.ToDOB);
            cmd.Parameters.AddWithValue("@Attitude", filter.Attitude);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyHRHistory(agency_PressAgencyHRHistory filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join agency_PressAgencyHR paHR on paHR.PressAgencyID = pa.PressAgencyID and paHR.Deleted = @IsNotDeleted
                            left join agency_PressAgencyHRHistory paHRHistory on paHRHistory.PressAgencyHRID = paHR.PressAgencyHRID and paHRHistory.Deleted = @IsNotDeleted
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.FromMeetDTG != null)
                    sql += " and DATEDIFF(DAY, @FromMeetDTG, paHRHistory.MeetedDTG) >= 0 ";

                if (filter.ToMeetDTG != null)
                    sql += " and DATEDIFF(DAY, paHRHistory.MeetedDTG, @ToMeetDTG) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHRHistory.Content like @SearchText) ";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@FromMeetDTG", filter.FromMeetDTG);
            cmd.Parameters.AddWithValue("@ToMeetDTG", filter.ToMeetDTG);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyHRRelatives(agency_PressAgencyHRRelatives filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join agency_PressAgencyHR paHR on paHR.PressAgencyID = pa.PressAgencyID and paHR.Deleted = @IsNotDeleted
                            left join agency_PressAgencyHRRelatives paHRRelatives on paHRRelatives.PressAgencyHRID = paHR.PressAgencyHRID and paHRRelatives.Deleted = @IsNotDeleted
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.FromDOB != null)
                    sql += " and DATEDIFF(DAY, @FromDOB, paHRRelatives.DOB) >= 0 ";

                if (filter.ToDOB != null)
                    sql += " and DATEDIFF(DAY, paHRRelatives.DOB, @ToDOB) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHRRelatives.FullName like @SearchText " +
                        "or paHRRelatives.Hobby like @SearchText " +
                        "or paHRRelatives.OtherNote like @SearchText " +
                        "or paHRRelatives.Relationship like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@FromDOB", filter.FromDOB);
            cmd.Parameters.AddWithValue("@ToDOB", filter.ToDOB);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgencyHR> SearchPressAgencyHRHistory_GetPressAgencyHRID(agency_PressAgencyHRHistory filter)
        {
            string sql = @"select paHR.PressAgencyHRID
                            from agency_PressAgencyHR paHR
                            left join agency_PressAgencyHRHistory paHRHistory on paHRHistory.PressAgencyHRID = paHR.PressAgencyHRID and paHRHistory.Deleted = @IsNotDeleted
                            where paHR.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.FromMeetDTG != null)
                    sql += " and DATEDIFF(DAY, @FromMeetDTG, paHRHistory.MeetedDTG) >= 0 ";

                if (filter.ToMeetDTG != null)
                    sql += " and DATEDIFF(DAY, paHRHistory.MeetedDTG, @ToMeetDTG) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHRHistory.Content like @SearchText) ";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@FromMeetDTG", filter.FromMeetDTG);
            cmd.Parameters.AddWithValue("@ToMeetDTG", filter.ToMeetDTG);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }

        public List<agency_PressAgencyHR> SearchPressAgencyHRRelatives_GetPressAgencyHRID(agency_PressAgencyHRRelatives filter)
        {
            string sql = @"select paHR.PressAgencyHRID
                            from agency_PressAgencyHR paHR
                            left join agency_PressAgencyHRRelatives paHRRelatives on paHRRelatives.PressAgencyHRID = paHR.PressAgencyHRID and paHRRelatives.Deleted = @IsNotDeleted
                            where paHR.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.FromDOB != null)
                    sql += " and DATEDIFF(DAY, @FromDOB, paHRRelatives.DOB) >= 0 ";

                if (filter.ToDOB != null)
                    sql += " and DATEDIFF(DAY, paHRRelatives.DOB, @ToDOB) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHRRelatives.FullName like @SearchText " +
                        "or paHRRelatives.Hobby like @SearchText " +
                        "or paHRRelatives.OtherNote like @SearchText " +
                        "or paHRRelatives.Relationship like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@FromDOB", filter.FromDOB);
            cmd.Parameters.AddWithValue("@ToDOB", filter.ToDOB);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyHistory(agency_PressAgencyHistory filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join agency_PressAgencyHistory paHistory on paHistory.PressAgencyID = pa.PressAgencyID and paHistory.Deleted = @IsNotDeleted
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.FromChangeDTG != null)
                    sql += " and DATEDIFF(DAY, @FromChangeDTG, paHistory.ChangeDTG) >= 0 ";

                if (filter.ToChangeDTG != null)
                    sql += " and DATEDIFF(DAY, paHistory.ChangeDTG, @ToChangeDTG) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paHistory.PositionChange like @SearchText " +
                        "or paHistory.OldEmployee like @SearchText " +
                        "or paHistory.NewEmployee like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@FromChangeDTG", filter.FromChangeDTG);
            cmd.Parameters.AddWithValue("@ToChangeDTG", filter.ToChangeDTG);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyMeeting(agency_PressAgencyMeeting filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join agency_PressAgencyMeeting paMeeting on paMeeting.PressAgencyID = pa.PressAgencyID and paMeeting.Deleted = @IsNotDeleted
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (filter.FromContractDTG != null)
                    sql += " and DATEDIFF(DAY, @FromContractDTG, paMeeting.ContractDTG) >= 0 ";

                if (filter.ToContractDTG != null)
                    sql += " and DATEDIFF(DAY, paMeeting.ContractDTG, @ToContractDTG) >= 0 ";

                if (filter.FromMeetDTG != null)
                    sql += " and DATEDIFF(DAY, @FromMeetDTG, paMeeting.MeetDTG) >= 0 ";

                if (filter.ToMeetDTG != null)
                    sql += " and DATEDIFF(DAY, paMeeting.MeetDTG, @ToMeetDTG) >= 0 ";

                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paMeeting.Partner like @SearchText " +
                        "or paMeeting.Location like @SearchText " +
                        "or paMeeting.Content like @SearchText " +
                        "or paMeeting.ContractNo like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@FromContractDTG", filter.FromContractDTG);
            cmd.Parameters.AddWithValue("@ToContractDTG", filter.ToContractDTG);
            cmd.Parameters.AddWithValue("@FromMeetDTG", filter.FromMeetDTG);
            cmd.Parameters.AddWithValue("@ToMeetDTG", filter.ToMeetDTG);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyRelations(agency_RelationsPressAgency filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join agency_RelationsPressAgency paRelations on paRelations.PressAgencyID = pa.PressAgencyID and paRelations.Deleted = @IsNotDeleted
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (paRelations.Name like @SearchText " +
                        "or paRelations.Relationship like @SearchText " +
                        "or paRelations.Note like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencyOtherImage(adm_Attachment filter)
        {
            string sql = @"select pa.PressAgencyID
                            from agency_PressAgency pa
                            left join adm_Attachment att on att.RefID = pa.PressAgencyID and att.RefType = @OtherImage
                            left join adm_Employee emp on emp.Username = att.CreatedBy and emp.Deleted = 0
                            where pa.Deleted = @IsNotDeleted ";

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                    sql += " and (att.Description like @SearchText " +
                            "or att.FileName like @SearchText " +
                            "or emp.Name like @SearchText)";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@OtherImage", SMX.AttachmentRefType.PressAgencyOtherImage);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        #endregion

        public List<agency_PressAgency> GetListPressAgency(PagingInfo pagingInfo)
        {
            string sql = @"select pa.*, agt.TypeName as AgencyTypeName 
                            from AgencyType agt 
                            left join agency_PressAgency pa on agt.Id = pa.Type
                            where Deleted = @IsNotDeleted and pa.Type is not null";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                //return base.ExecutePaging<agency_PressAgency>(context, cmd, "CASE WHEN DisplayOrder IS NULL THEN 1 ELSE 0 END, DisplayOrder", pagingInfo);
                return base.ExecutePaging<agency_PressAgency>(context, cmd, " Name", pagingInfo);
            }
        }

        public List<agency_PressAgency> GetAllListPressAgency()
        {
            string sql = @"select * from agency_PressAgency
                            where (Deleted = @IsNotDeleted OR Deleted IS NULL) 
                            order by CASE WHEN DisplayOrder IS NULL THEN 1 ELSE 0 END, DisplayOrder";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public List<agency_PressAgency> GetItemByListID(List<int> lstID)
        {
            string sql = @"select * from agency_PressAgency
                            where Deleted = @IsNotDeleted and PressAgencyID in ({0})";

            sql = string.Format(sql, BuildInCondition(lstID));

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd);
            }
        }

        public agency_PressAgency GetPressAgency_ByID(int? pressAgencyID)
        {
            string sql = @"select * from agency_PressAgency
                                where PressAgencyID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd).FirstOrDefault();
            }
        }

        public void DeletePressAgency(int? itemID)
        {
            string sql = @"update agency_PressAgency
                                set Deleted = @Deleted
                                where PressAgencyID = @PressAgencyID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_PressAgency> SearchPressAgencySelector(string searchValue)
        {
            string sql = @"select *
                           from agency_PressAgency
                           where Deleted = 0
                               and (@Name is null OR Name like @Name)";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@Name", BuildLikeFilter(searchValue));

            using (var dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<agency_PressAgency>(command);
                return res;
            }
        }

        #region Press Agency HR

        public agency_PressAgencyHR GetPressAgencyHR_ByID(int? pressAgencyHRID)
        {
            string sql = @"select 
                               hr.*
                               , pa.Name as PressAgencyName
                               , agt.Id as PressAgencyType
                           from AgencyType agt
                           left join agency_PressAgency pa on agt.Id = pa.Type
                           join agency_PressAgencyHR hr on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                           where hr.PressAgencyHRID = @PressAgencyHRID
                           and hr.Deleted = @IsNotDeleted and pa.Type is not null";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd).FirstOrDefault();
            }
        }

        public void DeletePressAgencyHR(int? itemID)
        {
            string sql = @"update agency_PressAgencyHR
                                set Deleted = @Deleted
                                where PressAgencyHRID = @PressAgencyHRID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_PressAgencyHR> GetListPressAgencyHR_ByPressAgencyID(int? pressAgencyID, int? attitude)
        {
            string sql = @"select * from agency_PressAgencyHR
                                where PressAgencyID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

            if (attitude != null)
                sql += @" and Attitude = @Attitude";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Attitude", attitude);
            cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }

        public bool isAdmin()
        {
            return Profiles.MyProfile.Roles.Any(x => x.Name.ToLower().Contains("qtht"));
        }

        // Lấy ra danh sách HR
        public List<agency_PressAgencyHR> GetListPressAgencyHR(PagingInfo pagingInfo, int UserId, string userName)
        {
            string sql;

            if (isAdmin())
            {
                sql = @"select 
                               hr.*
	                           , agt.TypeName as PressAgencyTypeString
	                           , agt.Id as PressAgencyType
                               , pa.Name as PressAgencyName
                               ,  STUFF(
                                        (SELECT ', ' + sm.GroupName
                                         FROM [SharingManagement] sm
                                         WHERE sm.PressAgencyHRID = hr.PressAgencyHRID
                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),
                                        1, 2, ''
                                    ) AS PermissionGroupName
                           from AgencyType agt
                           left join agency_PressAgency pa on agt.Id = pa.Type
                           join agency_PressAgencyHR hr on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                           where hr.Deleted = @IsNotDeleted and pa.Type is not null ";
            }
            //else
            //{
            //    sql = $@"select 
            //                   hr.*
            //                , agt.TypeName as PressAgencyTypeString
            //                , agt.Id as PressAgencyType
            //                   , pa.Name as PressAgencyName
            //               from AgencyType agt
            //               left join agency_PressAgency pa on agt.Id = pa.Type
            //               join agency_PressAgencyHR hr on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
            //               join [dbo].[SharingManagement] sm on hr.PressAgencyHRID = sm.PressAgencyHRID
            //               where hr.Deleted = @IsNotDeleted and pa.Type is not null AND sm.UserId = {UserId} AND isShared = 1 ";
            //}
            else
            {
                sql = $@"SELECT 
                        hr.*
                        , agt.TypeName AS PressAgencyTypeString
                        , agt.Id AS PressAgencyType
                        , pa.Name AS PressAgencyName
                        ,  STUFF(
                            (SELECT ', ' + sm.GroupName
                             FROM [SharingManagement] sm
                             WHERE sm.PressAgencyHRID = hr.PressAgencyHRID
                             FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),
                            1, 2, ''
                        ) AS PermissionGroupName
                    FROM 
                        AgencyType agt
                    LEFT JOIN 
                        agency_PressAgency pa ON agt.Id = pa.Type
                    JOIN 
                        agency_PressAgencyHR hr ON pa.PressAgencyID = hr.PressAgencyID AND pa.Deleted = @IsNotDeleted
                    WHERE 
                        hr.Deleted = @IsNotDeleted AND pa.Type IS NOT NULL AND EXISTS (
                            SELECT 1 
                            FROM [dbo].[SharingManagement] sm 
                            WHERE 
                                (hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId = {UserId} AND isShared = 1 AND GroupName IS NULL) 
                                OR 
                                (hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN (
                                        SELECT adm_Role.Name
                                        FROM [adm_EmployeeRole] empRole
                                        JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                        JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                        WHERE emp.EmployeeID = {UserId}
                                    )
                                )
                        )";
            }


            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<agency_PressAgencyHR>(context, cmd, "FullName ASC", pagingInfo);
            }
        }
        //Đếm số lượng nhân sự của tổ chức
        public List<agency_PressAgencyHR> GetListPressAgencyHR_ByFilter(agency_PressAgencyHR filter, int UserId, string userName)
        {
            string sql = @"
                        SELECT
                            at.TypeName,
                            ISNULL(SUM(agt.CountByType), 0) AS CountByType,
                            at.Id AS PressAgencyType
                        FROM
                            AgencyType at
                        LEFT JOIN (
                            SELECT
                                pa.Type AS PressAgencyType,
                                pa.TypeName,
                                COUNT(DISTINCT hr.PressAgencyHRID) AS CountByType
                            FROM
                                agency_PressAgencyHR hr
                            JOIN 
                                agency_PressAgency pa ON pa.PressAgencyID = hr.PressAgencyID AND pa.Deleted = 0
                            WHERE
                                hr.Deleted = 0 ";

            if (!isAdmin())
            {
                //sql += " AND EXISTS (SELECT 1 FROM [dbo].[SharingManagement] sm WHERE hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId = @UserId AND isShared = 1) ";
                sql += @" AND EXISTS (
                    SELECT 1 
                    FROM [dbo].[SharingManagement] sm 
                    WHERE 
                        (hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId = @UserId AND isShared = 1 AND GroupName IS NULL) 
                        OR 
                        (hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN (
                                SELECT adm_Role.Name
                                FROM [adm_EmployeeRole] empRole
                                JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                WHERE emp.EmployeeID = @UserId
                            )
                        )
                )";
            }

            if (!string.IsNullOrWhiteSpace(filter.Mobile))
                sql += " AND hr.Mobile LIKE @Mobile ";

            if (!string.IsNullOrWhiteSpace(filter.FullName))
                sql += " AND hr.FullName LIKE @FullName ";

            if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                sql += " AND (hr.FullName LIKE @TextSearch OR hr.Mobile LIKE @TextSearch OR hr.Email LIKE @TextSearch) ";

            if (!string.IsNullOrWhiteSpace(filter.Address))
                sql += " AND hr.Address LIKE @Address ";

            if (!string.IsNullOrWhiteSpace(filter.RelatedInformation))
                sql += " AND hr.RelatedInformation LIKE @RelatedInformation ";

            sql += @"
                        GROUP BY
                            pa.Type, pa.TypeName
                    ) AS agt ON agt.PressAgencyType = at.Id 
                    WHERE  
                        at.Id IS NOT NULL 
                    GROUP BY
                        at.TypeName, at.Id
                    ORDER BY
                        at.Id; ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Mobile", BuildLikeFilter(filter.Mobile));
            cmd.Parameters.AddWithValue("@Address", BuildLikeFilter(filter.Address));
            cmd.Parameters.AddWithValue("@FullName", BuildLikeFilter(filter.FullName));
            cmd.Parameters.AddWithValue("@TextSearch", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@RelatedInformation", BuildLikeFilter(filter.RelatedInformation));

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }
        #region old code GetListPressAgencyHR_ByFilter
        // code cũ
        //public List<agency_PressAgencyHR> GetListPressAgencyHR_ByFilter(agency_PressAgencyHR filter, int UserId, string userName)
        //{
        //    string sql;

        //    if(isAdmin())
        //    {

        //        sql = @"SELECT
        //                    at.TypeName,
        //                    ISNULL(agt.CountByType, 0) AS CountByType,
        //                    at.Id AS PressAgencyType
        //                FROM
        //                    AgencyType at
        //                    LEFT JOIN (
        //                        SELECT
        //                            pa.Type AS PressAgencyType,pa.TypeName,
        //                            COUNT(DISTINCT hr.PressAgencyHRID) AS CountByType
        //                        FROM
        //                            agency_PressAgencyHR hr
        //                            JOIN agency_PressAgency pa ON pa.PressAgencyID = hr.PressAgencyID AND pa.Deleted = 0
        //                        WHERE
        //                            hr.Deleted = 0 ";                    
        //    }
        //    else
        //    {
        //        sql = $@"SELECT
        //                    at.TypeName,
        //                    ISNULL(agt.CountByType, 0) AS CountByType,
        //                    at.Id AS PressAgencyType
        //                FROM
        //                    AgencyType at
        //                    LEFT JOIN (
        //                        SELECT
        //                            pa.Type AS PressAgencyType, pa.TypeName,
        //                            COUNT(DISTINCT hr.PressAgencyHRID) AS CountByType
        //                        FROM
        //                            agency_PressAgencyHR hr
        //                            JOIN agency_PressAgency pa ON pa.PressAgencyID = hr.PressAgencyID AND pa.Deleted = 0
        //                            JOIN [dbo].[SharingManagement] sm on hr.PressAgencyHRID = sm.PressAgencyHRID
        //                        WHERE
        //                            hr.Deleted = 0 AND pa.Deleted = 0 AND sm.UserId = {UserId} AND isShared = 1 ";
        //    }

        //    if (!string.IsNullOrWhiteSpace(filter.Mobile))
        //        sql += " and hr.Mobile like @Mobile ";

        //    if (!string.IsNullOrWhiteSpace(filter.FullName))
        //        sql += " and hr.FullName like @FullName ";

        //    if (!string.IsNullOrWhiteSpace(filter.TextSearch))
        //        sql += " and (hr.FullName like @TextSearch or hr.Mobile like @TextSearch or hr.Email like @TextSearch) ";

        //    if (!string.IsNullOrWhiteSpace(filter.Address))
        //        sql += " and hr.Address like @Address ";

        //    if (!string.IsNullOrWhiteSpace(filter.RelatedInformation))
        //        sql += " and hr.RelatedInformation like @RelatedInformation ";


        //    sql += @" GROUP BY pa.Type, pa.TypeName
        //                    ) AS agt ON agt.PressAgencyType = at.Id Where ";

        //    if (filter.PressAgencyType != null && filter.PressAgencyType != PressAgencyType.All)
        //    {
        //        sql += " at.Id = @PressAgencyType OR at.Id IS NOT NULL ";
        //    }
        //    if (filter.PressAgencyType == null || filter.PressAgencyType == PressAgencyType.All)
        //    {
        //        sql += " at.Id IS NOT NULL";
        //    }

        //    if (!string.IsNullOrWhiteSpace(filter.PressAgencyTypeString))
        //        sql += $@" and agt.TypeName like N'%{filter.PressAgencyTypeString}%' ";

        //    sql += " ORDER BY at.Id; ";

        //    SqlCommand cmd = new SqlCommand(sql);
        //    cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
        //    cmd.Parameters.AddWithValue("@Mobile", BuildLikeFilter(filter.Mobile));
        //    cmd.Parameters.AddWithValue("@PressAgencyType", SMX.PressAgencyType.PressAgency);
        //    cmd.Parameters.AddWithValue("@Address", BuildLikeFilter(filter.Address));
        //    //cmd.Parameters.AddWithValue("@PressAgencyTypeString", BuildLikeFilter(filter.PressAgencyTypeString));
        //    cmd.Parameters.AddWithValue("@FullName", BuildLikeFilter(filter.FullName));
        //    cmd.Parameters.AddWithValue("@TextSearch", BuildLikeFilter(filter.TextSearch));
        //    cmd.Parameters.AddWithValue("@RelatedInformation", BuildLikeFilter(filter.RelatedInformation));

        //    using (DataContext context = new DataContext())

        //    {
        //        return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
        //    }
        //}
        // code cũ
        #endregion
        public List<agency_PressAgencyHR> GetListPressAgencyHR_ByFilter(agency_PressAgencyHR filter, PagingInfo pagingInfo, int UserId, string userName)
        {
            string sql;

            if (isAdmin())
            {
                sql = @"select dt.* from(
                           select
                               hr.*
                               , agt.Id as PressAgencyType
                               , agt.TypeName as PressAgencyTypeString
                               , pa.Name as PressAgencyName
                               , STUFF(
                                        (SELECT ', ' + sm.GroupName
                                         FROM [SharingManagement] sm
                                         WHERE sm.PressAgencyHRID = hr.PressAgencyHRID
                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),
                                        1, 2, ''
                                    ) AS PermissionGroupName
                           from agency_PressAgencyHR hr
                           join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                           join AgencyType agt on agt.Id = pa.Type 
                           where pa.Deleted = @IsNotDeleted and hr.Deleted = @IsNotDeleted and pa.Type is not null) as dt
                           where dt.Deleted = @IsNotDeleted ";
            }
            else
            {
                //sql = $@"select dt.* from(
                //           select
                //               hr.*
                //               , agt.Id as PressAgencyType
                //               , agt.TypeName as PressAgencyTypeString
                //               , pa.Name as PressAgencyName
                //           from agency_PressAgencyHR hr
                //           join [dbo].[SharingManagement] sm on hr.PressAgencyHRID = sm.PressAgencyHRID
                //           join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                //           join AgencyType agt on agt.Id = pa.Type 
                //           where pa.Deleted = @IsNotDeleted and hr.Deleted = @IsNotDeleted and pa.Type is not null AND sm.UserId = {UserId} AND isShared = 1) as dt 
                //           where dt.Deleted = @IsNotDeleted ";
                sql = $@"select dt.* from(
                           select
                               hr.*
                               , agt.Id as PressAgencyType
                               , agt.TypeName as PressAgencyTypeString
                               , pa.Name as PressAgencyName
                               ,  STUFF(
                                        (SELECT ', ' + sm.GroupName
                                         FROM [SharingManagement] sm
                                         WHERE sm.PressAgencyHRID = hr.PressAgencyHRID
                                         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),
                                        1, 2, ''
                                    ) AS PermissionGroupName
                               , ROW_NUMBER() OVER (PARTITION BY hr.PressAgencyHRID ORDER BY hr.PressAgencyHRID) AS RowNum
                           from agency_PressAgencyHR hr
                           join [dbo].[SharingManagement] sm on hr.PressAgencyHRID = sm.PressAgencyHRID
                           join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                           join AgencyType agt on agt.Id = pa.Type 
                           where pa.Deleted = @IsNotDeleted and hr.Deleted = @IsNotDeleted and pa.Type is not null
                        AND  ((hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId = {UserId} AND isShared = 1 AND GroupName IS NULL) 
                                        OR 
                                        (hr.PressAgencyHRID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN (
                                                SELECT adm_Role.Name
                                                FROM [adm_EmployeeRole] empRole
                                                JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                                JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                                WHERE emp.EmployeeID = {UserId}
                                            )))
                        ) AS dt 
                        WHERE 
                            dt.RowNum = 1 AND dt.Deleted = @IsNotDeleted";
            }

            if (filter.PressAgencyType != null)
            {
                if (filter.PressAgencyType != PressAgencyType.All)
                {
                    sql += " and dt.PressAgencyType = @PressAgencyType ";
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.Mobile))
                sql += " and dt.Mobile like @Mobile ";

            if (!string.IsNullOrWhiteSpace(filter.FullName))
                sql += " and dt.FullName like @FullName ";

            if (!string.IsNullOrWhiteSpace(filter.TextSearch))
                sql += " and (dt.FullName like @TextSearch or dt.Mobile like @TextSearch or dt.Email like @TextSearch) ";

            if (!string.IsNullOrWhiteSpace(filter.Address))
                sql += " and dt.Address like @Address ";

            if (!string.IsNullOrWhiteSpace(filter.PressAgencyTypeString))
                sql += " and dt.TypeName like @PressAgencyTypeString ";

            if (!string.IsNullOrWhiteSpace(filter.RelatedInformation))
                sql += " and dt.RelatedInformation like @RelatedInformation ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Mobile", BuildLikeFilter(filter.Mobile));
            cmd.Parameters.AddWithValue("@PressAgencyType", filter.PressAgencyType);
            cmd.Parameters.AddWithValue("@Address", BuildLikeFilter(filter.Address));
            cmd.Parameters.AddWithValue("@PressAgencyTypeString", BuildLikeFilter(filter.PressAgencyTypeString));
            cmd.Parameters.AddWithValue("@FullName", BuildLikeFilter(filter.FullName));
            cmd.Parameters.AddWithValue("@TextSearch", BuildLikeFilter(filter.TextSearch));
            cmd.Parameters.AddWithValue("@RelatedInformation", BuildLikeFilter(filter.RelatedInformation));


            using (DataContext context = new DataContext())
            {

                if (filter.PressAgencyType != null)
                {
                    //var lst = base.ExecutePaging<agency_PressAgencyHR>(context, cmd, null, pagingInfo);
                    var lst = context.ExecuteSelect<agency_PressAgencyHR>(cmd);
                    //GetItemByPaging
                    var paramType = GetPressAgencyTypeByID(filter.PressAgencyType.Value);
                    if (paramType != null && (String.Equals(paramType.Code.Trim(), "MB", StringComparison.OrdinalIgnoreCase) || String.Equals(paramType.Code.Trim(), "MBBank", StringComparison.OrdinalIgnoreCase)))
                    {
                        return GetItemByPaging(lst.OrderBy(item =>
                        {
                            if (item.Position.ToLower().Contains("chủ tịch") || item.Position.ToLower().Contains("ct"))
                            {
                                return 0;
                            }
                            return 1;
                        }).ThenBy(item => item.Position).ToList(), pagingInfo);
                    }
                    else
                    {
                        return GetItemByPaging(lst.OrderBy(item => item.FullName).ToList(), pagingInfo);
                    }
                }
                else
                {
                    return base.ExecutePaging<agency_PressAgencyHR>(context, cmd, "FullName asc", pagingInfo);
                }
            }
        }

        #region Press Agency HR History

        public void DeletePressAgencyHRHistory(int? itemID)
        {
            string sql = @"update agency_PressAgencyHRHistory
                                set Deleted = @Deleted
                                where PressAgencyHRHistoryID = @PressAgencyHRHistoryID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyHRHistoryID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_PressAgencyHRHistory> GetListPressAgencyHRHistory_ByPressAgencyHRID(int? pressAgencyHRID)
        {
            string sql = @"select * from agency_PressAgencyHRHistory
                                where PressAgencyHRID = @PressAgencyHRID
                                and Deleted = @IsNotDeleted order by MeetedDTG desc, UpdatedDTG desc";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRHistory>(cmd);
            }
        }

        #endregion

        #region Press Agency HR Alert

        public void DeletePressAgencyHRAlertByPressAgenctyHrID(int? itemID)
        {
            string sql = @"update agency_PressAgencyHRAlert
                                set Deleted = @Deleted
                                where PressAgencyHRID = @PressAgencyHRID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public void DeletePressAgencyHRAlert(int? itemID)
        {
            string sql = @"update agency_PressAgencyHRAlert
                                set Deleted = @Deleted
                                where PressAgencyHRAlertID = @PressAgencyHRAlertID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyHRAlertID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }
        public void UpdatePressAgencyHRAlert_ByPressAgencyHRID(int? pressAgencyHRID)
        {
            string sql = @"UPDATE agency_PressAgencyHRAlert
                            SET Deleted = 1
                            WHERE PressAgencyHRID = @PressAgencyHRID
                            ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }
        public List<agency_PressAgencyHRAlert> GetListPressAgencyHRAlert_ByPressAgencyHRID(int? pressAgencyHRID)
        {
            string sql = @"select * from agency_PressAgencyHRAlert
                                where PressAgencyHRID = @PressAgencyHRID
                                and Deleted = @IsNotDeleted order by AlertDTG desc, UpdatedDTG desc";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRAlert>(cmd);
            }
        }
        public agency_PressAgencyHRAlert GetListPressAgencyHRAlert_ByPressAgencyHRAlert(int? pressAgencyHRlertID)
        {
            string sql = @"select * from agency_PressAgencyHRAlert
                                where PressAgencyHRAlertID = @PressAgencyHRID
                                and Deleted = @IsNotDeleted";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRlertID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRAlert>(cmd).SingleOrDefault();
            }
        }
        #endregion

        #region Press Agency HR Relatives

        public void DeletePressAgencyHRRelatives(int? itemID)
        {
            string sql = @"update agency_PressAgencyHRRelatives
                                set Deleted = @Deleted
                                where PressAgencyHRRelativesID = @PressAgencyHRRelativesID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyHRRelativesID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_PressAgencyHRRelatives> GetListPressAgencyHRRelatives_ByPressAgencyHRID(int? pressAgencyHRID)
        {
            string sql = @"select * from agency_PressAgencyHRRelatives
                                where PressAgencyHRID = @PressAgencyHRID
                                and Deleted = @IsNotDeleted order by UpdatedDTG desc, CreatedDTG desc";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRRelatives>(cmd);
            }
        }

        #endregion

        #endregion

        #region Press Agency History

        public void DeletePressAgencyHistory(int? itemID)
        {
            string sql = @"update agency_PressAgencyHistory
                                set Deleted = @Deleted
                                where PressAgencyHistoryID = @PressAgencyHistoryID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyHistoryID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_PressAgencyHistory> GetListPressAgencyHistory_ByPressAgencyID(int? pressAgencyID, PagingInfo pagingInfo)
        {
            string sql = @"select * from agency_PressAgencyHistory
                                where PressAgencyID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<agency_PressAgencyHistory>(context, cmd, "UpdatedDTG desc, CreatedDTG desc", pagingInfo);
            }
        }

        #endregion

        #region Press Agency Meeting

        public void DeletePressAgencyMeeting(int? itemID)
        {
            string sql = @"update agency_PressAgencyMeeting
                                set Deleted = @Deleted
                                where PressAgencyMeetingID = @PressAgencyMeetingID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PressAgencyMeetingID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_PressAgencyMeeting> GetListPressAgencyMeeting_ByPressAgencyID(int? pressAgencyID, PagingInfo pagingInfo)
        {
            using (DataContext dataContext = new DataContext())
            {
                string sql = @"select * from agency_PressAgencyMeeting
                                where PressAgencyID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
                cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

                using (DataContext context = new DataContext())
                {
                    return base.ExecutePaging<agency_PressAgencyMeeting>(context, cmd, "UpdatedDTG desc, CreatedDTG desc", pagingInfo);
                }
            }
        }

        #endregion

        #region Relations Press Agency

        public void DeleteRelationsPressAgency(int? itemID)
        {
            string sql = @"update agency_RelationsPressAgency
                                set Deleted = @Deleted
                                where RelationsPressAgencyID = @RelationsPressAgencyID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@RelationsPressAgencyID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_RelationsPressAgency> GetListRelationsPressAgency_ByPressAgencyID(int? pressAgencyID, PagingInfo pagingInfo)
        {
            using (DataContext dataContext = new DataContext())
            {
                string sql = @"select * from agency_RelationsPressAgency
                                where PressAgencyID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
                cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

                using (DataContext context = new DataContext())
                {
                    return base.ExecutePaging<agency_RelationsPressAgency>(context, cmd, "UpdatedDTG desc, CreatedDTG desc", pagingInfo);
                }
            }
        }
        public agency_PressAgencyHRRelatives GetListRelationsByRelationsID(int? pressAgencyID)
        {
            using (DataContext dataContext = new DataContext())
            {
                string sql = @"select * from agency_PressAgencyHRRelatives
                                where PressAgencyHRRelativesID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
                cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

                using (DataContext context = new DataContext())
                {
                    return context.ExecuteSelect<agency_PressAgencyHRRelatives>(cmd).FirstOrDefault();
                }
            }
        }
        #endregion

        #region Relationship With MB

        public void DeleteRelationshipWithMB(int? itemID)
        {
            string sql = @"delete agency_RelationshipWithMB
                            where RelationshipWithMBID = @RelationshipWithMBID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@RelationshipWithMBID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<agency_RelationshipWithMB> GetListRelationshipWithMB_ByPressAgencyID(int? pressAgencyID, PagingInfo pagingInfo)
        {
            string sql = @"select * from agency_RelationshipWithMB
                            where PressAgencyID = @PressAgencyID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<agency_RelationshipWithMB>(context, cmd, "RelationshipWithMBID desc", pagingInfo);
            }
        }

        #endregion

        public List<agency_PressAgencyHR> GetAllListPressAgencyHR_ByPressAgencyID(int? pressAgencyID)
        {
            using (DataContext dataContext = new DataContext())
            {
                string sql = @"select * from agency_PressAgencyHR
                                where PressAgencyID = @PressAgencyID
                                and Deleted = @IsNotDeleted";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@PressAgencyID", pressAgencyID);
                cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

                using (DataContext context = new DataContext())
                {
                    return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
                }
            }
        }

        public List<agency_PressAgencyHR> GetAllListPressAgencyHR()
        {

            string sql = @"select 
	                            hr.*,
	                            pa.Name as PressAgencyName,
                                pa.Type as PressAgencyType
                            from agency_PressAgencyHR hr
                            join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                            where hr.Deleted = @IsNotDeleted";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }
        public List<agency_PressAgencyHR> GetListScanPressAgencyHR()
        {

            string sql = @"select 
	                            hr.*,
	                            pa.Name as PressAgencyName,
                                pa.Type as PressAgencyType
                            from agency_PressAgencyHR hr
                            join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                            where hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }
        public List<agency_PressAgencyHR> GetAllListPressAgencyHRAlert()
        {

            string sql = @"select 
	                            hr.*,
	                            pa.Name as PressAgencyName,
                                pa.Type as PressAgencyType
                            from agency_PressAgencyHR hr
                            left join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                            where  hr.Deleted = @IsNotDeleted OR hr.Deleted IS NULL";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHR>(cmd);
            }
        }
        public List<agency_PressAgencyHRRelatives> GetAllListPressAgencyHRRelatives()
        {
            string sql = @"select 
	                        hrRelatives.*,
	                        hr.Position as HRPosition,
	                        hr.FullName as HRFullName,
	                        pa.Name as PressAgencyName
                        from agency_PressAgencyHRRelatives hrRelatives
                        join agency_PressAgencyHR hr on hr.PressAgencyHRID = hrRelatives.PressAgencyHRID and hr.Deleted = @IsNotDeleted
                        join agency_PressAgency pa on pa.PressAgencyID = hr.PressAgencyID and pa.Deleted = @IsNotDeleted
                        where hr.Deleted = @IsNotDeleted AND pa.Deleted = @IsNotDeleted AND hrr.Deleted = @IsNotDeleted AND hrr.[DOB] IS NOT NULL";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRRelatives>(cmd);
            }
        }

        #region Email
        public List<agency_PressAgencyHRAlert> GetAllListLunar()
        {

            string sql = @"select  *
                            from agency_PressAgencyHRAlert
                            where TypeDate = 2 AND  Deleted = @IsNotDeleted";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRAlert>(cmd);
            }
        }
        public List<agency_PressAgencyHRAlert> GetAllListSolar()
        {

            string sql = @"select  *
                            from agency_PressAgencyHRAlert
                            where TypeDate = 1 AND  Deleted = @IsNotDeleted ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgencyHRAlert>(cmd);
            }
        }

        public agency_PressAgency GetAgencyByID(int Id)
        {

            string sql = @"select  *
                            from agency_PressAgency
                            where PressAgencyID = @Id AND Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Id", Id);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<agency_PressAgency>(cmd).SingleOrDefault();
            }
        }
        public agency_PressAgencyHR GetNameAgencyHRByID(int? Id)
        {
            if (Id != null)
            {
                string sql = @"select  *
                            from agency_PressAgencyHR
                            where PressAgencyHRID = @Id AND Deleted = 0";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
                cmd.Parameters.AddWithValue("@Id", Id);

                using (DataContext context = new DataContext())
                {
                    return context.ExecuteSelect<agency_PressAgencyHR>(cmd).SingleOrDefault();
                }
            }
            return null;
        }
        #endregion
    }
}