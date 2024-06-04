using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace SM.SmartInfo.DAO.Notification
{
    public class NotificationDao : BaseDao
    {
        private string ConnectionString = ConfigUtils.ConnectionString;
        public void DeleteNotificationByHRAlertID(int alertID)
        {
            string sql = @"UPDATE ntf_Notification 
                        SET isDeleted = 1 
                        WHERE NotificationID = (
                            SELECT TOP(1) NotificationID 
                            FROM ntf_Notification 
                            WHERE AlertID = @AlertID 
                            ORDER BY CreatedDTG DESC 
                        );
                        ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AlertID", alertID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteNotificationByNameLike(string content, string isDeleted)
        {
            string sql = $@"UPDATE ntf_Notification SET isDeleted = @isDeleted WHERE Content = N'{content}'";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@isDeleted", isDeleted);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteNotificationByPressAgencyID(int pressAgencyHRID)
        {
            string sql = @"UPDATE ntf_Notification
                            SET ntf_Notification.isDeleted = 1
                            WHERE EXISTS (
                                SELECT 1
                                FROM agency_PressAgencyHRAlert
                                WHERE ntf_Notification.AlertID = agency_PressAgencyHRAlert.PressAgencyHRAlertID
                                AND agency_PressAgencyHRAlert.PressAgencyHRID = @PressAgencyHRID
                            );

                            UPDATE agency_PressAgencyHRAlert
                            SET agency_PressAgencyHRAlert.Deleted = 1
                            WHERE agency_PressAgencyHRAlert.PressAgencyHRID = @PressAgencyHRID;";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@PressAgencyHRID", pressAgencyHRID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool checkExistingNotificationByAlertId(int? id)
        {
            string sql = @"SELECT * FROM ntf_Notification WHERE AlertID = @alertID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@alertID", id);

            using (DataContext context = new DataContext())
            {
                if (context.ExecuteSelect<ntf_Notification>(cmd).Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void UpdateNotification(ntf_Notification param)
        {
            string sql = "";
            if (param.lunarDay != null && param.lunarMonth != null && param.lunarYear != null)
            {
                sql += @"UPDATE ntf_Notification SET UpdateDTG = @UpdateDate, DoDTG = @DoDTG, Content = @Content,
                            Type = @Type, Note = @Note, Comment = @Comment, lunarDay = @lunarDay, lunarMonth = @lunarMonth, lunarYear = @lunarYear WHERE AlertID = @alertID";
            }
            else
            {
                sql += @"UPDATE ntf_Notification SET UpdateDTG = @UpdateDate, DoDTG = @DoDTG, Content = @Content,
                            Type = @Type, Note = @Note, Comment = @Comment WHERE AlertID = @alertID";
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UpdateDate", param.UpdateDTG);
                command.Parameters.AddWithValue("@DoDTG", param.DoDTG);
                command.Parameters.AddWithValue("@Content", param.Content);
                command.Parameters.AddWithValue("@Type", param.Type);
                command.Parameters.AddWithValue("@Note", param.Note);
                command.Parameters.AddWithValue("@Comment", param.Comment == null ? "" : param.Comment);
                if (param.lunarDay != null && param.lunarMonth != null && param.lunarYear != null)
                {
                    command.Parameters.AddWithValue("@lunarDay", param.lunarDay);
                    command.Parameters.AddWithValue("@lunarMonth", param.lunarMonth);
                    command.Parameters.AddWithValue("@lunarYear", param.lunarYear);
                }
                command.Parameters.AddWithValue("@alertID", param.AlertID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void InsertNewNotificationPushHistory(ntf_NotificationPushHistory newNotiPushHistory)
        {
            string insertNotificationSql = @"
            INSERT INTO [ntf_NotificationPushHistory] 
            ([Content], [CreatedDTG], [CreatedBy], [RefData], [Title], [EmployeeID], [IsRead], [Status], [ClientMessageID], [Error], [DeviceID], [TransactionId])
            VALUES (@Content, @CreatedDTG, @CreatedBy, @RefData, @Title, @EmployeeID, @IsRead, @Status, @ClientMessageID, @Error, @DeviceID, @TransactionId);";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(insertNotificationSql, connection);
                command.Parameters.AddWithValue("@Content", (object)newNotiPushHistory.Content ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedDTG", DateTime.Now);
                command.Parameters.AddWithValue("@CreatedBy", (object)newNotiPushHistory.UserFullName ?? DBNull.Value);
                command.Parameters.AddWithValue("@RefData", (object)newNotiPushHistory.RefData ?? DBNull.Value);
                command.Parameters.AddWithValue("@Title", (object)newNotiPushHistory.Title ?? DBNull.Value);
                command.Parameters.AddWithValue("@EmployeeID", (object)newNotiPushHistory.EmployeeID ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsRead", (object)newNotiPushHistory.IsRead ?? DBNull.Value);
                command.Parameters.AddWithValue("@Status", (object)newNotiPushHistory.Status ?? DBNull.Value);
                command.Parameters.AddWithValue("@ClientMessageID", (object)newNotiPushHistory.ClientMessageID ?? DBNull.Value);
                command.Parameters.AddWithValue("@Error", (object)newNotiPushHistory.Error ?? DBNull.Value);
                command.Parameters.AddWithValue("@DeviceID", (object)newNotiPushHistory.DeviceID ?? DBNull.Value);
                command.Parameters.AddWithValue("@TransactionId", (object)newNotiPushHistory.TransactionId ?? DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
        //--------------------
        public int InsertNewNotificationAndGetID(ntf_Notification newNoti)
        {
            // Insert new notification
            string insertNotificationSql = @"
                    INSERT INTO ntf_Notification (CreatedDTG, DoDTG, Content, Type, Note, Comment, AlertID, isDeleted)
                    VALUES (@CreateDate, @DoDTG, @Content, @Type, @Note, @Comment, @AlertID, @isDeleted);
                    SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(insertNotificationSql, connection);
                command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                command.Parameters.AddWithValue("@DoDTG", newNoti.DoDTG);
                command.Parameters.AddWithValue("@Content", newNoti.Content);
                command.Parameters.AddWithValue("@Type", newNoti.Type);
                command.Parameters.AddWithValue("@Note", newNoti.Note);
                command.Parameters.AddWithValue("@Comment", newNoti.Comment == null ? "" : newNoti.Comment);
                command.Parameters.AddWithValue("@AlertID", newNoti.AlertID == null ? -1 : newNoti.AlertID);
                command.Parameters.AddWithValue("@isDeleted", newNoti.isDeleted);

                // Execute the INSERT command and get the inserted NotificationID
                int newNotificationID = Convert.ToInt32(command.ExecuteScalar());

                return newNotificationID;
            }
        }

        //--------------------
        public void InsertNewNotification(ntf_Notification param)
        {
            string sql = "";
            if (param.lunarDay != null && param.lunarMonth != null && param.lunarYear != null)
            {
                sql += @"INSERT INTO ntf_Notification
                    (CreatedDTG, DoDTG, Content, Type, Note, Comment, AlertID, isDeleted, lunarDay, lunarMonth, lunarYear)
                    VALUES (@CreateDate, @DoDTG, @Content, @Type, @Note, @Comment, @AlertID, @isDeleted, @lunarDay, @lunarMonth, @lunarYear)";
            }
            else
            {
                sql += @"INSERT INTO ntf_Notification
                    (CreatedDTG, DoDTG, Content, Type, Note, Comment, AlertID, isDeleted)
                    VALUES (@CreateDate, @DoDTG, @Content, @Type, @Note, @Comment, @AlertID, @isDeleted)";
            }


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                command.Parameters.AddWithValue("@DoDTG", param.DoDTG);
                command.Parameters.AddWithValue("@Content", param.Content);
                command.Parameters.AddWithValue("@Type", param.Type);
                command.Parameters.AddWithValue("@Note", param.Note);
                command.Parameters.AddWithValue("@Comment", param.Comment == null ? "" : param.Comment);
                command.Parameters.AddWithValue("@AlertID", param.AlertID == null ? -1 : param.AlertID);
                command.Parameters.AddWithValue("@isDeleted", param.isDeleted);
                if (param.lunarDay != null && param.lunarMonth != null && param.lunarYear != null)
                {
                    command.Parameters.AddWithValue("@lunarDay", param.lunarDay);
                    command.Parameters.AddWithValue("@lunarMonth", param.lunarMonth);
                    command.Parameters.AddWithValue("@lunarYear", param.lunarYear);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Dùng khi tìm kiếm trên nav
        public List<ntf_Notification> SearchNotification(string searchText, PagingInfo pagingInfo, int userId)
        {
            string cmdText;
            if (isAdmin())
            {
                cmdText = @"WITH Events AS (
                                SELECT 
                                        hrr.[PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hrr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRRelatives] hrr
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                UNION ALL
                                SELECT 
                                        [PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ông/bà ' + [FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHR] hr
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                UNION ALL
                                SELECT 
                                        pahra.[PressAgencyHRID] as NotificationID,
                                        pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                        hr.Position + N' báo ' + pa.[Name] AS Note, 
                                        pahra.[AlertDTG] as DoDTG,
                                        1 AS [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL
                UNION ALL
                                SELECT [SystemParameterID] as NotificationID
                                          ,[Name] as Content
                                          ,[Description] as Note
                                          ,[Ext4] as DoDTG
                                          ,[Ext1i] as Type,
										  null as lunarDay,
										  null as lunarMonth,
										  null as lunarYear
                                  FROM [adm_SystemParameter] 
                                  WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                  AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                UNION ALL
                                SELECT 
                                        pa.[PressAgencyID] as NotificationID,
                                        CASE 
                                                WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                        END AS Content,
                                        at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                        pa.[EstablishedDTG] AS DoDTG,
                                        4 AS Type,
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        agency_PressAgency pa
                                JOIN 
                                        AgencyType at ON pa.[Type] = at.[Id]
                                WHERE 
                                        pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                UNION ALL
                               SELECT 
                                    pahra.[PressAgencyHRID] as NotificationID,
                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                    [AlertDTG] as DoDTG,
                                    5 AS [Type],
									lunarDay,
									lunarMonth,
									lunarYear
                                FROM 
                                    [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                    pahra.[Deleted] = 0 AND pahra.[TypeDate] = 2 AND [AlertDTG] IS NOT NULL
                )
                SELECT * FROM Events  where 1 = 1 ";
            }
            else
            {
                cmdText = $@"WITH BirthdayEvents AS (
                            SELECT 
                                                                hrr.[PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hrr.[DOB] as DoDTG,
                                                                1 as [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHRRelatives] hrr
                                                        JOIN 
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                                        UNION ALL
                                                        SELECT 
                                                                [PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ông/bà ' + [FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hr.[DOB] as DoDTG,
                                                                1 as [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHR] hr
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                                        UNION ALL
                                                        SELECT 
                                                                pahra.[PressAgencyHRID] as NotificationID,
                                                                pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                                                hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                                pahra.[AlertDTG] as DoDTG,
                                                                1 AS [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHRAlert] pahra 
                                                        JOIN 
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL

                                                    UNION ALL
                                                               SELECT 
                                                                    pahra.[PressAgencyHRID] as NotificationID,
                                                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                                    [AlertDTG] as DoDTG,
                                                                    5 AS [Type],
									                                lunarDay,
									                                lunarMonth,
									                                lunarYear
                                                                FROM 
                                                                    [agency_PressAgencyHRAlert] pahra 
                                                                JOIN 
                                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                                JOIN 
                                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                                WHERE 
                                                                    pahra.Deleted = 0 AND pahra.TypeDate = 2 AND [AlertDTG] IS NOT NULL
                        ),

                         SharedBirthdayEvents AS (
                                    SELECT BirthdayEvents.*
                                    FROM BirthdayEvents
                                     WHERE EXISTS (
                            SELECT 1 
                            FROM [dbo].[SharingManagement] sm 
                            WHERE 
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId = {Profiles.MyProfile.EmployeeID} AND isShared = 1 AND GroupName IS NULL) 
                                OR 
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN (
                                        SELECT adm_Role.Name
                                        FROM [adm_EmployeeRole] empRole
                                        JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                        JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                        WHERE emp.EmployeeID = {Profiles.MyProfile.EmployeeID}
                                    )
				         )))
                        ,
                        OtherEvents AS (
                            SELECT [SystemParameterID] as NotificationID
                                                                  ,[Name] as Content
                                                                  ,[Description] as Note
                                                                  ,[Ext4] as DoDTG
                                                                  ,[Ext1i] as Type,
										                          null as lunarDay,
										                          null as lunarMonth,
										                          null as lunarYear
                                                          FROM [adm_SystemParameter] 
                                                          WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                                          AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                                        UNION ALL
                                                        SELECT 
                                                                pa.[PressAgencyID] as NotificationID,
                                                                CASE 
                                                                        WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                                        ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                                                END AS Content,
                                                                at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                                                pa.[EstablishedDTG] AS DoDTG,
                                                                4 AS Type,
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                agency_PressAgency pa
                                                        JOIN 
                                                                AgencyType at ON pa.[Type] = at.[Id]
                                                        WHERE 
                                                                pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL     
                        )

                        SELECT * 
                        FROM (
                            SELECT * FROM SharedBirthdayEvents
                            UNION ALL
                            SELECT * FROM OtherEvents
                        ) AS EventAll where 1 = 1 ";
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                cmdText += $@" and (Content LIKE N'%{searchText.Trim()}%' OR Note LIKE N'%{searchText.Trim()}%') ";
            }

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@userId", userId);

            using (DataContext context = new DataContext())
            {
                var lst = context.ExecuteSelect<ntf_Notification>(cmdText);

                lst.ForEach(notification =>
                {
                    if (notification.Type == 5)
                    {
                        int[] solarDate = ExceptionConvert.TryConvertLunar2Solar(DateTime.Now.Year, notification.lunarMonth ?? DateTime.Now.Month, notification.lunarDay ?? DateTime.Now.Day);
                        notification.DateConvert = $"{solarDate[1]:00}{solarDate[0]:00}";
                    }
                    else
                    {
                        notification.DateConvert = notification.DoDTG?.ToString("MMdd") ?? "";
                    }
                });

                lst = lst.OrderBy(notification =>
                {
                    int notificationDate = int.Parse(notification.DateConvert);
                    int nowDate = int.Parse(DateTime.Now.ToString("MMdd"));
                    int difference = Math.Abs(notificationDate - nowDate);
                    return difference;
                }).ToList();

                return GetItemByPaging(lst, pagingInfo);
            }
        }
        // Dùng khi tìm kiếm theo tuần, tháng, tất cả
        public List<ntf_Notification> SearchNotification(ntf_Notification filter, int? typeTime, PagingInfo pagingInfo, int userId)
        {
            string cmdText;
            if (isAdmin())
            {
                cmdText = @"WITH Events AS (
                                SELECT 
                                        hrr.[PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hrr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRRelatives] hrr
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                UNION ALL
                                SELECT 
                                        [PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ông/bà ' + [FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHR] hr
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                UNION ALL
                                SELECT 
                                        pahra.[PressAgencyHRID] as NotificationID,
                                        pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                        hr.Position + N' báo ' + pa.[Name] AS Note, 
                                        pahra.[AlertDTG] as DoDTG,
                                        1 AS [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL
                UNION ALL
                                SELECT [SystemParameterID] as NotificationID
                                          ,[Name] as Content
                                          ,[Description] as Note
                                          ,[Ext4] as DoDTG
                                          ,[Ext1i] as Type,
										  null as lunarDay,
										  null as lunarMonth,
										  null as lunarYear
                                  FROM [adm_SystemParameter] 
                                  WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                  AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                UNION ALL
                                SELECT 
                                        pa.[PressAgencyID] as NotificationID,
                                        CASE 
                                                WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                        END AS Content,
                                        at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                        pa.[EstablishedDTG] AS DoDTG,
                                        4 AS Type,
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        agency_PressAgency pa
                                JOIN 
                                        AgencyType at ON pa.[Type] = at.[Id]
                                WHERE 
                                        pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                UNION ALL
                               SELECT 
                                    pahra.[PressAgencyHRID] as NotificationID,
                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                    [AlertDTG] as DoDTG,
                                    5 AS [Type],
									lunarDay,
									lunarMonth,
									lunarYear
                                FROM 
                                    [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                    pahra.[Deleted] = 0 AND pahra.[TypeDate] = 2 AND [AlertDTG] IS NOT NULL
                )
                SELECT * FROM Events  where 1 = 1 ";
            }
            else
            {
                cmdText = $@"WITH BirthdayEvents AS (
                            SELECT 
                                                                hrr.[PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hrr.[DOB] as DoDTG,
                                                                1 as [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHRRelatives] hrr
                                                        JOIN 
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                                        UNION ALL
                                                        SELECT 
                                                                [PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ông/bà ' + [FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hr.[DOB] as DoDTG,
                                                                1 as [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHR] hr
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                                        UNION ALL
                                                        SELECT 
                                                                pahra.[PressAgencyHRID] as NotificationID,
                                                                pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                                                hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                                pahra.[AlertDTG] as DoDTG,
                                                                1 AS [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHRAlert] pahra 
                                                        JOIN 
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL

                                                    UNION ALL
                                                               SELECT 
                                                                    pahra.[PressAgencyHRID] as NotificationID,
                                                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                                    [AlertDTG] as DoDTG,
                                                                    5 AS [Type],
									                                lunarDay,
									                                lunarMonth,
									                                lunarYear
                                                                FROM 
                                                                    [agency_PressAgencyHRAlert] pahra 
                                                                JOIN 
                                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                                JOIN 
                                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                                WHERE 
                                                                    pahra.Deleted = 0 AND pahra.TypeDate = 2 AND [AlertDTG] IS NOT NULL
                        ),

                         SharedBirthdayEvents AS (
                                    SELECT BirthdayEvents.*
                                    FROM BirthdayEvents
                                     WHERE EXISTS (
                            SELECT 1 
                            FROM [dbo].[SharingManagement] sm 
                            WHERE 
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId = {userId} AND isShared = 1 AND GroupName IS NULL) 
                                OR 
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN (
                                        SELECT adm_Role.Name
                                        FROM [adm_EmployeeRole] empRole
                                        JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                        JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                        WHERE emp.EmployeeID = {userId}
                                    )
				         )))
                        ,
                        OtherEvents AS (
                            SELECT [SystemParameterID] as NotificationID
                                                                  ,[Name] as Content
                                                                  ,[Description] as Note
                                                                  ,[Ext4] as DoDTG
                                                                  ,[Ext1i] as Type,
										                          null as lunarDay,
										                          null as lunarMonth,
										                          null as lunarYear
                                                          FROM [adm_SystemParameter] 
                                                          WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                                          AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                                        UNION ALL
                                                        SELECT 
                                                                pa.[PressAgencyID] as NotificationID,
                                                                CASE 
                                                                        WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                                        ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                                                END AS Content,
                                                                at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                                                pa.[EstablishedDTG] AS DoDTG,
                                                                4 AS Type,
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                agency_PressAgency pa
                                                        JOIN 
                                                                AgencyType at ON pa.[Type] = at.[Id]
                                                        WHERE 
                                                                pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL     
                        )

                        SELECT * 
                        FROM (
                            SELECT * FROM SharedBirthdayEvents
                            UNION ALL
                            SELECT * FROM OtherEvents
                        ) AS EventAll where 1 = 1 ";
            }


            //if (!string.IsNullOrWhiteSpace(filter.TextSearch))
            //    cmdText += $@"Where (Content like N'%{filter.TextSearch.Trim()}%' 
            //                        or Note like N'%{filter.TextSearch.Trim()}%' 
            //                        or FORMAT(DoDTG, 'dd/MM/yyyy') like N'%{filter.TextSearch.Trim()}%'
            //                        )";

            //if (filter.FromDoDTG != null || filter.ToDoDTG != null || !string.IsNullOrEmpty(filter.TextSearch))
            //{
            //    cmdText += " where 1 = 1 ";
            //}
            if (filter.FromDoDTG != null && filter.ToDoDTG != null)
            {
                int[] lunarDateFrom = VietNamCalendar.convertSolar2Lunar(filter.FromDoDTG.Value.Day, filter.FromDoDTG.Value.Month, filter.FromDoDTG.Value.Year, 7.0);
                int[] lunarDateTo = VietNamCalendar.convertSolar2Lunar(filter.ToDoDTG.Value.Day, filter.ToDoDTG.Value.Month, filter.ToDoDTG.Value.Year, 7.0);

                cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{filter.FromDoDTG.Value.Date}') OR (MONTH(DoDTG) = MONTH('{filter.FromDoDTG.Value.Date}') AND DAY(DoDTG) >= DAY('{filter.FromDoDTG.Value.Date}'))) AND (MONTH(DoDTG) < MONTH('{filter.ToDoDTG.Value.Date}') OR (MONTH(DoDTG) = MONTH('{filter.ToDoDTG.Value.Date}') AND " +
                    $"DAY(DoDTG) <= DAY('{filter.ToDoDTG.Value.Date}')))) " +
                    $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFrom[1]} OR " +
                    $"(lunarMonth = {lunarDateFrom[1]} AND " +
                    $"lunarDay >= {lunarDateFrom[0]})) AND " +
                    $"(lunarMonth < {lunarDateTo[1]} OR " +
                    $"(lunarMonth = {lunarDateTo[1]} AND" +
                    $" lunarDay <=  {lunarDateTo[0]}))))  ";
            }
            if (filter.FromDoDTG != null && filter.ToDoDTG == null)
            {
                int[] lunarDateFrom = VietNamCalendar.convertSolar2Lunar(filter.FromDoDTG.Value.Day, filter.FromDoDTG.Value.Month, filter.FromDoDTG.Value.Year, 7.0);

                cmdText += $" and  ((Type <> 5 AND (MONTH(DoDTG) > MONTH('{filter.FromDoDTG.Value.Date}') OR(MONTH(DoDTG) = MONTH('{filter.FromDoDTG.Value.Date}') AND DAY(DoDTG) >= DAY('{filter.FromDoDTG.Value.Date}'))))" +
                    $"OR (Type = 5 AND (lunarMonth > {lunarDateFrom[1]} OR " +
                    $"(lunarMonth = {lunarDateFrom[1]} AND " +
                    $"lunarDay >= {lunarDateFrom[0]})))) ";
            }
            if (filter.FromDoDTG == null && filter.ToDoDTG != null)
            {
                int[] lunarDateTo = VietNamCalendar.convertSolar2Lunar(filter.ToDoDTG.Value.Day, filter.ToDoDTG.Value.Month, filter.ToDoDTG.Value.Year, 7.0);

                cmdText += $" and ((Type <> 5 AND (MONTH(DoDTG) < MONTH('{filter.ToDoDTG.Value.Date}') OR (MONTH(DoDTG) = MONTH('{filter.ToDoDTG.Value.Date}') AND DAY(DoDTG) <= DAY('{filter.ToDoDTG.Value.Date}'))))" +
                    $" OR (Type = 5 AND (lunarMonth < {lunarDateTo[1]} OR " +
                    $"(lunarMonth = {lunarDateTo[1]} AND " +
                    $"lunarDay <= {lunarDateTo[0]})))) ";
            }
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                cmdText += $@" and (Content LIKE N'%{filter.TextSearch.Trim()}%' OR Note LIKE N'%{filter.TextSearch.Trim()}%') ";
            }
           
            if (typeTime != null && string.IsNullOrWhiteSpace(filter.TextSearch) && filter.FromDoDTG == null && filter.ToDoDTG == null)
            {
                var currentDate = DateTime.Now;
                switch (typeTime)
                {
                    case SMX.FilterTime.Week:
                        DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
                        DateTime endOfWeek = startOfWeek.AddDays(6);

                        int[] lunarDateFrom = VietNamCalendar.convertSolar2Lunar(startOfWeek.Day, startOfWeek.Month, startOfWeek.Year, 7.0);
                        int[] lunarDateTo = VietNamCalendar.convertSolar2Lunar(endOfWeek.Day, endOfWeek.Month, endOfWeek.Year, 7.0);

                        cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{startOfWeek.Date}') OR (MONTH(DoDTG) = MONTH('{startOfWeek.Date}') AND DAY(DoDTG) >= DAY('{startOfWeek.Date}'))) AND (MONTH(DoDTG) < MONTH('{endOfWeek.Date}') OR (MONTH(DoDTG) = MONTH('{endOfWeek.Date}') AND " +
                    $"DAY(DoDTG) <= DAY('{endOfWeek.Date}')))) " +
                    $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFrom[1]} OR " +
                    $"(lunarMonth = {lunarDateFrom[1]} AND " +
                    $"lunarDay >= {lunarDateFrom[0]})) AND " +
                    $"(lunarMonth < {lunarDateTo[1]} OR " +
                    $"(lunarMonth = {lunarDateTo[1]} AND" +
                    $" lunarDay <=  {lunarDateTo[0]}))))  ";
                        break;
                    case SMX.FilterTime.Month:
                        DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                        DateTime endOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).AddDays(-1);

                        int[] lunarDateFromm = VietNamCalendar.convertSolar2Lunar(startOfMonth.Day, startOfMonth.Month, startOfMonth.Year, 7.0);
                        int[] lunarDateToo = VietNamCalendar.convertSolar2Lunar(endOfMonth.Day, endOfMonth.Month, endOfMonth.Year, 7.0);

                        cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{startOfMonth.Date}') OR (MONTH(DoDTG) = MONTH('{startOfMonth.Date}') AND DAY(DoDTG) >= DAY('{startOfMonth.Date}'))) AND (MONTH(DoDTG) < MONTH('{endOfMonth.Date}') OR (MONTH(DoDTG) = MONTH('{endOfMonth.Date}') AND " +
                     $"DAY(DoDTG) <= DAY('{endOfMonth.Date}')))) " +
                     $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFromm[1]} OR " +
                     $"(lunarMonth = {lunarDateFromm[1]} AND " +
                     $"lunarDay >= {lunarDateFromm[0]})) AND " +
                     $"(lunarMonth < {lunarDateToo[1]} OR " +
                     $"(lunarMonth = {lunarDateToo[1]} AND" +
                     $" lunarDay <=  {lunarDateToo[0]}))))  ";
                        break;
                }
            }
            else
            {
                switch (typeTime)
                {
                    case SMX.FilterTime.Week:
                        DateTime startOfWeekk = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);
                        DateTime endOfWeek = startOfWeekk.AddDays(6);
                        int[] lunarDateFrom = VietNamCalendar.convertSolar2Lunar(startOfWeekk.Day, startOfWeekk.Month, startOfWeekk.Year, 7.0);
                        int[] lunarDateTo = VietNamCalendar.convertSolar2Lunar(endOfWeek.Day, endOfWeek.Month, endOfWeek.Year, 7.0);

                        cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{startOfWeekk.Date}') OR (MONTH(DoDTG) = MONTH('{startOfWeekk.Date}') AND DAY(DoDTG) >= DAY('{startOfWeekk.Date}'))) AND (MONTH(DoDTG) < MONTH('{endOfWeek.Date}') OR (MONTH(DoDTG) = MONTH('{endOfWeek.Date}') AND " +
                     $"DAY(DoDTG) <= DAY('{endOfWeek.Date}')))) " +
                     $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFrom[1]} OR " +
                     $"(lunarMonth = {lunarDateFrom[1]} AND " +
                     $"lunarDay >= {lunarDateFrom[0]})) AND " +
                     $"(lunarMonth < {lunarDateTo[1]} OR " +
                     $"(lunarMonth = {lunarDateTo[1]} AND" +
                     $" lunarDay <=  {lunarDateTo[0]}))))  ";
                        break;
                    case SMX.FilterTime.Month:
                        DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        DateTime endOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                        int[] lunarDateFromm = VietNamCalendar.convertSolar2Lunar(startOfMonth.Day, startOfMonth.Month, startOfMonth.Year, 7.0);
                        int[] lunarDateToo = VietNamCalendar.convertSolar2Lunar(endOfMonth.Day, endOfMonth.Month, endOfMonth.Year, 7.0);

                        cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{startOfMonth.Date}') OR (MONTH(DoDTG) = MONTH('{startOfMonth.Date}') AND DAY(DoDTG) >= DAY('{startOfMonth.Date}'))) AND (MONTH(DoDTG) < MONTH('{endOfMonth.Date}') OR (MONTH(DoDTG) = MONTH('{endOfMonth.Date}') AND " +
                     $"DAY(DoDTG) <= DAY('{endOfMonth.Date}')))) " +
                     $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFromm[1]} OR " +
                     $"(lunarMonth = {lunarDateFromm[1]} AND " +
                     $"lunarDay >= {lunarDateFromm[0]})) AND " +
                     $"(lunarMonth < {lunarDateToo[1]} OR " +
                     $"(lunarMonth = {lunarDateToo[1]} AND" +
                     $" lunarDay <=  {lunarDateToo[0]}))))  ";
                        break;
                }
            }
            SqlCommand command = new SqlCommand(cmdText);
            //command.Parameters.AddWithValue("@textSearch", BuildLikeFilter(filter.TextSearch));
            //command.Parameters["@textSearch"].SqlDbType = SqlDbType.NVarChar;

            using (DataContext dataContext = new DataContext())
            {
                var lst = dataContext.ExecuteSelect<ntf_Notification>(cmdText);

                lst.ForEach(notification =>
                {
                    if (notification.Type == 5)
                    {
                        int[] solarDate = ExceptionConvert.TryConvertLunar2Solar(DateTime.Now.Year, notification.lunarMonth ?? DateTime.Now.Month, notification.lunarDay ?? DateTime.Now.Day);
                        notification.DateConvert = $"{solarDate[1]:00}{solarDate[0]:00}";
                    }
                    else
                    {
                        notification.DateConvert = notification.DoDTG?.ToString("MMdd") ?? "";
                    }
                });

                lst = lst.OrderBy(notification =>
                {
                    int notificationDate = int.Parse(notification.DateConvert);
                    int nowDate = int.Parse(DateTime.Now.ToString("MMdd"));
                    int difference = Math.Abs(notificationDate - nowDate);
                    return difference;
                }).ToList();

                return GetItemByPaging(lst, pagingInfo);
            }
        }
        public List<ntf_NotificationPushHistory> GetNotificationPushHistory()
        {
            string cmdText = @"SELECT [ID]
                              ,[Content]
                              ,[CreatedDTG]
                              ,[CreatedBy]
                              ,[RefData]
                              ,[Title]
                              ,[EmployeeID]
                              ,[IsRead]
                              ,[Status]
                              ,[ClientMessageID]
                              ,[Error]
                              ,[DeviceID]
                              ,[TransactionId]
                          FROM [ntf_NotificationPushHistory] 
                          ORDER BY 
                                CreatedDTG DESC";

            SqlCommand cmd = new SqlCommand(cmdText);
            //cmd.Parameters.AddWithValue("@NumberOfSend", numberOfSend);
            //cmd.Parameters.AddWithValue("@SinhNhat", SMX.Notification.CauHinhGuiThongBao.SinhNhat);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_NotificationPushHistory>(cmd);
            }
        }
        // Danh sách thông báo ở màn trang chủ
        public List<ntf_Notification> GetAllListNotificationForDefault(int userId)
        {
            DateTime currentDate = DateTime.Now.Date;
            string cmdText;
            LogManager.WebLogger.LogError(Profiles.MyProfile.Roles.ToString(), null);
            DateTime next10Days = currentDate.AddDays(10);
            int[] lunarDateFrom = VietNamCalendar.convertSolar2Lunar(currentDate.Day, currentDate.Month, currentDate.Year, 7.0);
            int[] lunarDateTo = VietNamCalendar.convertSolar2Lunar(next10Days.Day, next10Days.Month, next10Days.Year, 7.0);
            if (isAdmin())
            {
                #region Code cũ
                #region code cũ
                //cmdText = @"select noti.*,  CASE
                //                WHEN noti.Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)
                //                ELSE noti.DoDTG
                //            END AS ConvertedDoDTG
                //                from ntf_Notification noti
                //                where (noti.isDeleted = 0 OR noti.isDeleted IS NULL) 
                //                 AND (
                //                        (noti.Type = 5 AND dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)  <= DATEADD(day, 10, GETDATE()))
                //                        OR 
                //                        (noti.Type != 5 AND noti.DoDTG <= DATEADD(day, 10, GETDATE()))
                //                    )
                //                order by 
                //               ConvertedDoDTG desc";
                //==================
                //cmdText = @"   WITH DistinctNotifications AS (
                //                SELECT 
                //                    noti.*,  
                //                    ROW_NUMBER() OVER (PARTITION BY noti.Content, noti.Note ORDER BY 
                //                        CASE
                //                            WHEN noti.Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)
                //                            ELSE noti.DoDTG
                //                        END DESC,
                //                        NotificationID DESC 
                //                    ) AS RowNum
                //                FROM 
                //                    ntf_Notification noti
                //                WHERE 
                //                    (noti.isDeleted = 0 OR noti.isDeleted IS NULL) 
                //                    AND (
                //                        (noti.Type = 5 AND dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0) <= DATEADD(day, 10, GETDATE()))
                //                        OR 
                //                        (noti.Type != 5 AND noti.DoDTG <= DATEADD(day, 10, GETDATE()))
                //                    )
                //            )
                //            SELECT 
                //                NotificationID,
                //                CreatedDTG,
                //                DoDTG,
                //                Content,
                //                Type,
                //                Note,
                //                Comment,
                //                AlertID,
                //                UpdateDTG,
                //                isDeleted,
                //                CreatedBy,
                //                ConvertedDoDTG
                //            FROM 
                //                (
                //                    SELECT 
                //                        *,
                //                        CASE
                //                            WHEN Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(DoDTG), '-', DAY(DoDTG)), 0)
                //                            ELSE DoDTG
                //                        END AS ConvertedDoDTG
                //                    FROM DistinctNotifications
                //                ) AS subquery
                //            WHERE 
                //                RowNum = 1
                //            ORDER BY 
                //                ConvertedDoDTG DESC, NotificationID;";
                #endregion
                #endregion
                cmdText = @"WITH Events AS (
                                SELECT 
                                        hrr.[PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hrr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRRelatives] hrr
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                UNION ALL
                                SELECT 
                                        [PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ông/bà ' + [FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHR] hr
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                UNION ALL
                                SELECT 
                                        pahra.[PressAgencyHRID] as NotificationID,
                                        pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                        hr.Position + N' báo ' + pa.[Name] AS Note, 
                                        pahra.[AlertDTG] as DoDTG,
                                        1 AS [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL
                UNION ALL
                                SELECT [SystemParameterID] as NotificationID
                                          ,[Name] as Content
                                          ,[Description] as Note
                                          ,[Ext4] as DoDTG
                                          ,[Ext1i] as Type,
										  null as lunarDay,
										  null as lunarMonth,
										  null as lunarYear
                                  FROM [adm_SystemParameter] 
                                  WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                  AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                UNION ALL
                                SELECT 
                                        pa.[PressAgencyID] as NotificationID,
                                        CASE 
                                                WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                        END AS Content,
                                        at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                        pa.[EstablishedDTG] AS DoDTG,
                                        4 AS Type,
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        agency_PressAgency pa
                                JOIN 
                                        AgencyType at ON pa.[Type] = at.[Id]
                                WHERE 
                                        pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                UNION ALL
                               SELECT 
                                    pahra.[PressAgencyHRID] as NotificationID,
                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                    [AlertDTG] as DoDTG,
                                    5 AS [Type],
									lunarDay,
									lunarMonth,
									lunarYear
                                FROM 
                                    [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                    pahra.[Deleted] = 0 AND pahra.[TypeDate] = 2 AND [AlertDTG] IS NOT NULL
                )
                SELECT * FROM Events Where 1 = 1 ";
            }
            else
            {
                #region code cũ
                //cmdText = @"select noti.*,  CASE
                //                WHEN noti.Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)
                //                ELSE noti.DoDTG
                //            END AS ConvertedDoDTG from [dbo].[ntf_Notification] noti left join 
                //            (select paa.* from [dbo].[agency_PressAgencyHRAlert] paa join [dbo].[SharingManagement] sm on paa.PressAgencyHRID = sm.PressAgencyHRID 
                //            where sm.UserId = @userId and sm.isShared = 1 and paa.Deleted = 0) 
                //            as hrShare on noti.AlertID = hrShare.PressAgencyHRAlertID
                //            where noti.AlertID = hrShare.PressAgencyHRAlertID or AlertID = 0 or noti.AlertID is null and noti.isDeleted = 0  
                //            AND (
                //                (noti.Type = 5 AND dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)  <= DATEADD(day, 10, GETDATE()))
                //                OR 
                //                (noti.Type != 5 AND noti.DoDTG <= DATEADD(day, 10, GETDATE()))
                //            )
                //            group by noti.NotificationID, noti.CreatedDTG, noti.DoDTG, noti.Content, noti.Type, noti.Note, noti.Comment, noti.AlertID, noti.UpdateDTG, noti.isDeleted, noti.CreatedBy
                //            order by
                //                ConvertedDoDTG desc";
                //cmdText = @"WITH DistinctNotifications AS (
                //            SELECT 
                //                noti.*,  
                //                CASE
                //                    WHEN noti.Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)
                //                    ELSE noti.DoDTG
                //                END AS ConvertedDoDTG
                //            FROM 
                //                [dbo].[ntf_Notification] noti
                //            LEFT JOIN (
                //                SELECT paa.*
                //                FROM [dbo].[agency_PressAgencyHRAlert] paa
                //                JOIN [dbo].[SharingManagement] sm ON paa.PressAgencyHRID = sm.PressAgencyHRID 
                //                WHERE sm.UserId = @userId AND sm.isShared = 1 AND paa.Deleted = 0
                //            ) AS hrShare ON noti.AlertID = hrShare.PressAgencyHRAlertID
                //            WHERE 
                //                (noti.AlertID = hrShare.PressAgencyHRAlertID OR noti.AlertID = 0 OR noti.AlertID IS NULL OR noti.AlertID = -1) 
                //                AND (noti.isDeleted = 0  or  noti.isDeleted is null)
                //                AND (
                //                    (noti.Type = 5 AND dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0) <= DATEADD(day, 10, GETDATE()))
                //                    OR 
                //                    (noti.Type != 5 AND noti.DoDTG <= DATEADD(day, 10, GETDATE()))
                //                )
                //        )
                //        SELECT 
                //            NotificationID,
                //            CreatedDTG,
                //            DoDTG,
                //            Content,
                //            Type,
                //            Note,
                //            Comment,
                //            AlertID,
                //            UpdateDTG,
                //            isDeleted,
                //            CreatedBy,
                //            MAX(ConvertedDoDTG) AS ConvertedDoDTG 
                //        FROM 
                //            DistinctNotifications
                //        GROUP BY 
                //            NotificationID, CreatedDTG, DoDTG, Content, Type, Note, Comment, AlertID, UpdateDTG, isDeleted, CreatedBy
                //        ORDER BY 
                //            MAX(ConvertedDoDTG) DESC;";
                #endregion
                cmdText = $@"WITH BirthdayEvents AS (
                            SELECT 
                                                                hrr.[PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hrr.[DOB] as DoDTG,
                                                                1 as [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHRRelatives] hrr
                                                        JOIN 
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                                        UNION ALL
                                                        SELECT 
                                                                [PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ông/bà ' + [FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hr.[DOB] as DoDTG,
                                                                1 as [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHR] hr
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                                        UNION ALL
                                                        SELECT 
                                                                pahra.[PressAgencyHRID] as NotificationID,
                                                                pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                                                hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                                pahra.[AlertDTG] as DoDTG,
                                                                1 AS [Type],
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                [agency_PressAgencyHRAlert] pahra 
                                                        JOIN 
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                        JOIN 
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE 
                                                                pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL

                                                    UNION ALL
                                                               SELECT 
                                                                    pahra.[PressAgencyHRID] as NotificationID,
                                                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                                    [AlertDTG] as DoDTG,
                                                                    5 AS [Type],
									                                lunarDay,
									                                lunarMonth,
									                                lunarYear
                                                                FROM 
                                                                    [agency_PressAgencyHRAlert] pahra 
                                                                JOIN 
                                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                                JOIN 
                                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                                WHERE 
                                                                    pahra.Deleted = 0 AND pahra.TypeDate = 2 AND [AlertDTG] IS NOT NULL
                        ),

                         SharedBirthdayEvents AS (
                                    SELECT BirthdayEvents.*
                                    FROM BirthdayEvents
                                     WHERE EXISTS (
                            SELECT 1 
                            FROM [dbo].[SharingManagement] sm 
                            WHERE 
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId = {userId} AND isShared = 1 AND GroupName IS NULL) 
                                OR 
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN (
                                        SELECT adm_Role.Name
                                        FROM [adm_EmployeeRole] empRole
                                        JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                        JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                        WHERE emp.EmployeeID = {userId}
                                    )
				         )))
                        ,
                        OtherEvents AS (
                            SELECT [SystemParameterID] as NotificationID
                                                                  ,[Name] as Content
                                                                  ,[Description] as Note
                                                                  ,[Ext4] as DoDTG
                                                                  ,[Ext1i] as Type,
										                          null as lunarDay,
										                          null as lunarMonth,
										                          null as lunarYear
                                                          FROM [adm_SystemParameter] 
                                                          WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                                          AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                                        UNION ALL
                                                        SELECT 
                                                                pa.[PressAgencyID] as NotificationID,
                                                                CASE 
                                                                        WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                                        ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                                                END AS Content,
                                                                at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                                                pa.[EstablishedDTG] AS DoDTG,
                                                                4 AS Type,
										                        null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM 
                                                                agency_PressAgency pa
                                                        JOIN 
                                                                AgencyType at ON pa.[Type] = at.[Id]
                                                        WHERE 
                                                                pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL     
                        )

                        SELECT * 
                        FROM (
                            SELECT * FROM SharedBirthdayEvents
                            UNION ALL
                            SELECT * FROM OtherEvents
                        ) AS EventAll where 1 = 1 "; 
            }
            cmdText += $@" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{currentDate}') OR (MONTH(DoDTG) = MONTH('{currentDate}') AND DAY(DoDTG) >= DAY('{currentDate}'))) AND (MONTH(DoDTG) < MONTH('{next10Days}') OR (MONTH(DoDTG) = MONTH('{next10Days}') AND " +
                   $"DAY(DoDTG) <= DAY('{next10Days}')))) " +
                   $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFrom[1]} OR " +
                   $"(lunarMonth = {lunarDateFrom[1]} AND " +
                   $"lunarDay >= {lunarDateFrom[0]})) AND " +
                   $"(lunarMonth < {lunarDateTo[1]} OR " +
                   $"(lunarMonth = {lunarDateTo[1]} AND" +
                   $" lunarDay <=  {lunarDateTo[0]})))) ";

            SqlCommand cmd = new SqlCommand(cmdText);
            using (DataContext dataContext = new DataContext())
            {
                var lst = dataContext.ExecuteSelect<ntf_Notification>(cmd);

                lst.ForEach(notification =>
                {
                    if (notification.Type == 5)
                    {
                        int[] solarDate = ExceptionConvert.TryConvertLunar2Solar(DateTime.Now.Year, notification.lunarMonth ?? DateTime.Now.Month, notification.lunarDay ?? DateTime.Now.Day);
                        notification.DateConvert = $"{solarDate[1]:00}{solarDate[0]:00}";
                    }
                    else
                    {
                        notification.DateConvert = notification.DoDTG?.ToString("MMdd") ?? "";
                    }
                });

                lst = lst.OrderBy(notification =>
                {
                    int notificationDate = int.Parse(notification.DateConvert);
                    int nowDate = int.Parse(DateTime.Now.ToString("MMdd"));
                    int difference = Math.Abs(notificationDate - nowDate);
                    return difference;
                }).ToList();

                return lst;
            }
        }

        public List<ntf_Notification> GetAllListNotification(int? typeTime, int userId)
        {
            string cmdText;
            if (isAdmin())
            {
                cmdText = @"SELECT 
                                            hrr.[PressAgencyHRID] as ID,
                                            N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                            hr.[Position] + N' báo ' + pa.[Name] as Note,
                                            hrr.[DOB] as DoDTG,
                                            1 as [Type], NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                    FROM 
                                            [agency_PressAgencyHRRelatives] hrr
                                    JOIN 
                                            [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                    JOIN 
                                            [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                    WHERE 
                                            hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                    UNION ALL
                                    SELECT 
                                            [PressAgencyHRID] as ID,
                                            N'Sinh nhật ông/bà ' + [FullName] as Content,
                                            hr.[Position] + N' báo ' + pa.[Name] as Note,
                                            hr.[DOB] as DoDTG,
                                            1 as [Type], NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                    FROM 
                                            [agency_PressAgencyHR] hr
                                    JOIN 
                                            [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                    WHERE 
                                            hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                    UNION ALL
                                    SELECT 
                                            pahra.[PressAgencyHRID] as ID,
                                            pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                            hr.Position + N' báo ' + pa.[Name] AS Note, 
                                            pahra.[AlertDTG] as DoDTG,
                                            1 AS [Type], NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                    FROM 
                                            [agency_PressAgencyHRAlert] pahra 
                                    JOIN 
                                            [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                    JOIN 
                                            [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                    WHERE 
                                            pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL
                    UNION ALL
                                    SELECT [SystemParameterID] as ID
                                              ,[Name] as Content
                                              ,[Description] as Note
                                              ,[Ext4] as DoDTG
                                              ,[Ext1i] as Type, NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                      FROM [adm_SystemParameter] 
                                      WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                      AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                    UNION ALL
                                    SELECT 
                                            pa.[PressAgencyID] as ID,
                                            CASE 
                                                    WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                    ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                            END AS Content,
                                            at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                            pa.[EstablishedDTG] AS DoDTG,
                                            4 AS Type, NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                    FROM 
                                            agency_PressAgency pa
                                    JOIN 
                                            AgencyType at ON pa.[Type] = at.[Id]
                                    WHERE 
                                            pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                    UNION ALL
                    SELECT 
                        pahra.[PressAgencyHRID] as ID,
                        pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)' AS Content,
                        hr.Position + N' báo ' + pa.[Name] AS Note, 
                        [AlertDTG] as DoDTG,
                            5 AS [Type], lunarDay, lunarMonth, lunarYear
                    FROM 
                        [agency_PressAgencyHRAlert] pahra 
                    JOIN 
                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                    JOIN 
                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                    WHERE 
                        pahra.Deleted = 0 AND pahra.TypeDate = 2 ";
            }
            else
            {
                //cmdText = @"select noti.* from [dbo].[ntf_Notification] noti left join 
                //            (select paa.* from [dbo].[agency_PressAgencyHRAlert] paa join [dbo].[SharingManagement] sm on paa.PressAgencyHRID = sm.PressAgencyHRID 
                //            where sm.UserId = @userId and sm.isShared = 1 and paa.Deleted = 0) 
                //            as abc on noti.AlertID = abc.PressAgencyHRAlertID
                //            where noti.AlertID = abc.PressAgencyHRAlertID or AlertID = 0 or noti.AlertID is null and (noti.isDeleted = 0 OR noti.isDeleted IS NULL) 
                //            group by noti.NotificationID, noti.CreatedDTG, noti.DoDTG, noti.Content, noti.Type, noti.Note, noti.Comment, noti.AlertID, noti.UpdateDTG, noti.isDeleted, noti.CreatedBy ";
                cmdText = $@"WITH BirthdayEvents AS(
                            SELECT
                                                                hrr.[PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hrr.[DOB] as DoDTG,
                                                                1 as [Type],
                                                                null as lunarDay,
                                                                null as lunarMonth,
                                                                null as lunarYear
                                                        FROM
                                                                [agency_PressAgencyHRRelatives] hrr
                                                        JOIN
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                                        JOIN
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                                        UNION ALL
                                                        SELECT
                                                                [PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ông/bà ' + [FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hr.[DOB] as DoDTG,
                                                                1 as [Type],
                                                                null as lunarDay,
                                                                null as lunarMonth,
                                                                null as lunarYear
                                                        FROM
                                                                [agency_PressAgencyHR] hr
                                                        JOIN
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                                        UNION ALL
                                                        SELECT
                                                                pahra.[PressAgencyHRID] as NotificationID,
                                                                pahra.[Content] + N' của ông/bà ' + hr.[FullName] + N' (Dương lịch)' AS Content,
                                                                hr.Position + N' báo ' + pa.[Name] AS Note,
                                                                pahra.[AlertDTG] as DoDTG,
                                                                1 AS[Type],
                                                                null as lunarDay,
                                                                null as lunarMonth,
                                                                null as lunarYear
                                                        FROM
                                                                [agency_PressAgencyHRAlert] pahra
                                                        JOIN
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                        JOIN
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE
                                                                pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL

                                                    UNION ALL
                                                               SELECT
                                                                    pahra.[PressAgencyHRID] as NotificationID,
                                                                    ISNULL(pahra.[Content] + N' của ông/bà ' + hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' + hr.[FullName]) AS Content,
                                                                    hr.Position + N' báo ' + pa.[Name] AS Note,
                                                                    [AlertDTG] as DoDTG,
                                                                    5 AS[Type],
                                                                    lunarDay,
                                                                    lunarMonth,
                                                                    lunarYear
                                                                FROM
                                                                    [agency_PressAgencyHRAlert] pahra
                                                                JOIN
                                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                                JOIN
                                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                                WHERE
                                                                    pahra.Deleted = 0 AND pahra.TypeDate = 2 AND[AlertDTG] IS NOT NULL
                        ),

                         SharedBirthdayEvents AS(
                                    SELECT BirthdayEvents.*
                                    FROM BirthdayEvents
                                     WHERE EXISTS(
                            SELECT 1
                            FROM[dbo].[SharingManagement] sm
                            WHERE
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId = { userId}
                AND isShared = 1 AND GroupName IS NULL) 
                                OR
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN(
                                        SELECT adm_Role.Name
                                        FROM[adm_EmployeeRole] empRole
                                        JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                        JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                        WHERE emp.EmployeeID = { userId}
                                    )
				         )))
                        ,
                        OtherEvents AS(
                            SELECT[SystemParameterID] as NotificationID
                                                                  ,[Name] as Content
                                                                  ,[Description] as Note
                                                                  ,[Ext4] as DoDTG
                                                                  ,[Ext1i] as Type,
										                          null as lunarDay,
										                          null as lunarMonth,
										                          null as lunarYear
                                                          FROM[adm_SystemParameter]
                                                          WHERE[FeatureID] = 1231 AND[Ext1i] in (2, 3)
                                                          AND[Deleted] = 0 AND Status = 1 AND[Ext4] IS NOT NULL
                                      UNION ALL
                                                      SELECT
                                                                pa.[PressAgencyID] as NotificationID,
                                                                CASE
                                                                        WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' + pa.[Name]
                                                                        ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                                                END AS Content,
                                                                at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                                                pa.[EstablishedDTG] AS DoDTG,
                                                                4 AS Type,

                                                                null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM
                                                                agency_PressAgency pa
                                                        JOIN
                                                                AgencyType at ON pa.[Type] = at.[Id]
                                                        WHERE
                                                                pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                        )

                        SELECT*
                        FROM(
                            SELECT * FROM SharedBirthdayEvents
                            UNION ALL
                            SELECT * FROM OtherEvents
                        ) AS EventAll  Where 1 = 1 ";
            }

            if (typeTime != null)
            {
                switch (typeTime)
                {
                    case SMX.FilterTime.Week:
                        cmdText += " and  DATEDIFF(WEEK, CAST(CAST(YEAR(getdate()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME), getdate()) = 0";
                        break;
                    case SMX.FilterTime.Month:
                        cmdText += " and DATEDIFF(MONTH, CAST(CAST(YEAR(getdate()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME), getdate()) = 0";
                        break;
                }
            }
            //cmdText += " order by noti.DoDTG desc, noti.NotificationID desc";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@userId", userId);
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd);
            }
        }

        public bool isAdmin()
        {
            return Profiles.MyProfile.Roles.Any(x => x.Name.ToLower().Contains("qtht"));
        }

        // xử lý ở đây, thông báo màn hình thông báo, và tìm kiếm
        public List<ntf_Notification> GetAllListNotification(int? typeTime, PagingInfo pagingInfo, int userId)
        {
            DateTime currentDate = DateTime.Now.Date;
            string cmdText;
            LogManager.WebLogger.LogError(Profiles.MyProfile.Roles.ToString(), null);
           
            ChineseLunisolarCalendar lunarCalendar = new ChineseLunisolarCalendar();
           
            if (isAdmin())
            {
                #region old code
                //cmdText = @"select noti.*,  CASE
                //                WHEN noti.Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)
                //                ELSE noti.DoDTG
                //            END AS ConvertedDoDTG
                //                from ntf_Notification noti
                //                where noti.isDeleted = 0 OR noti.isDeleted IS NULL ";
                #endregion
                cmdText = @"WITH Events AS (
                                            SELECT 
                                                    hrr.[PressAgencyHRID] as NotificationID,
                                                    N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                                    hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                    hrr.[DOB] as DoDTG,
                                                    1 as [Type], NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                            FROM 
                                                    [agency_PressAgencyHRRelatives] hrr
                                            JOIN 
                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                            JOIN 
                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                            WHERE 
                                                    hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                            UNION ALL
                                            SELECT 
                                                    [PressAgencyHRID] as NotificationID,
                                                    N'Sinh nhật ông/bà ' + [FullName] as Content,
                                                    hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                    hr.[DOB] as DoDTG,
                                                    1 as [Type], NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                            FROM 
                                                    [agency_PressAgencyHR] hr
                                            JOIN 
                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                            WHERE 
                                                    hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                            UNION ALL
                                            SELECT 
                                                    pahra.[PressAgencyHRID] as NotificationID,
                                                    pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                                    pahra.[AlertDTG] as DoDTG,
                                                    1 AS [Type], NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                            FROM 
                                                    [agency_PressAgencyHRAlert] pahra 
                                            JOIN 
                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                            JOIN 
                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                            WHERE 
                                                    pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL
                            UNION ALL
                                            SELECT [SystemParameterID] as NotificationID
                                                      ,[Name] as Content
                                                      ,[Description] as Note
                                                      ,[Ext4] as DoDTG
                                                      ,[Ext1i] as Type, NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                              FROM [adm_SystemParameter] 
                                              WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                              AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                            UNION ALL
                                            SELECT 
                                                    pa.[PressAgencyID] as NotificationID,
                                                    CASE 
                                                            WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                            ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                                    END AS Content,
                                                    at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                                    pa.[EstablishedDTG] AS DoDTG,
                                                    4 AS Type, NULL as lunarDay, NULL as lunarMonth, NULL as lunarYear
                                            FROM 
                                                    agency_PressAgency pa
                                            JOIN 
                                                    AgencyType at ON pa.[Type] = at.[Id]
                                            WHERE 
                                                    pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                            UNION ALL
                            SELECT 
                                pahra.[PressAgencyHRID] as NotificationID,
                                pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)' AS Content,
                                hr.Position + N' báo ' + pa.[Name] AS Note, 
                                [AlertDTG] as DoDTG,
                                    5 AS [Type], lunarDay, lunarMonth, lunarYear
                            FROM 
                                [agency_PressAgencyHRAlert] pahra 
                            JOIN 
                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                            JOIN 
                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                            WHERE 
                                pahra.Deleted = 0 AND pahra.TypeDate = 2
                            )
                            SELECT * FROM Events  ";
            }
            else
            {
                #region old code
                //cmdText = @"select TOP (1000) noti.*, CASE
                //                WHEN noti.Type = 5 THEN dbo.fn_ConvertLunarToSolarDateTime(CONCAT(YEAR(GETDATE()), '-', MONTH(noti.DoDTG), '-', DAY(noti.DoDTG)), 0)
                //                ELSE noti.DoDTG
                //            END AS ConvertedDoDTG from [dbo].[ntf_Notification] noti left join 
                //            (select paa.* from [dbo].[agency_PressAgencyHRAlert] paa join [dbo].[SharingManagement] sm on paa.PressAgencyHRID = sm.PressAgencyHRID 
                //            where sm.UserId = @userId and sm.isShared = 1 and paa.Deleted = 0) 
                //            as abc on noti.AlertID = abc.PressAgencyHRAlertID
                //            where (noti.AlertID = abc.PressAgencyHRAlertID or AlertID = 0 or noti.AlertID is null OR noti.isDeleted = -1) and (noti.isDeleted = 0 OR noti.isDeleted IS NULL)";
                #endregion
                cmdText = $@"WITH BirthdayEvents AS(
                            SELECT
                                                                hrr.[PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hrr.[DOB] as DoDTG,
                                                                1 as [Type],
                                                                null as lunarDay,
                                                                null as lunarMonth,
                                                                null as lunarYear
                                                        FROM
                                                                [agency_PressAgencyHRRelatives] hrr
                                                        JOIN
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                                        JOIN
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                                        UNION ALL
                                                        SELECT
                                                                [PressAgencyHRID] as NotificationID,
                                                                N'Sinh nhật ông/bà ' + [FullName] as Content,
                                                                hr.[Position] + N' báo ' + pa.[Name] as Note,
                                                                hr.[DOB] as DoDTG,
                                                                1 as [Type],
                                                                null as lunarDay,
                                                                null as lunarMonth,
                                                                null as lunarYear
                                                        FROM
                                                                [agency_PressAgencyHR] hr
                                                        JOIN
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE
                                                                hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                                        UNION ALL
                                                        SELECT
                                                                pahra.[PressAgencyHRID] as NotificationID,
                                                                pahra.[Content] + N' của ông/bà ' + hr.[FullName] + N' (Dương lịch)' AS Content,
                                                                hr.Position + N' báo ' + pa.[Name] AS Note,
                                                                pahra.[AlertDTG] as DoDTG,
                                                                1 AS[Type],
                                                                null as lunarDay,
                                                                null as lunarMonth,
                                                                null as lunarYear
                                                        FROM
                                                                [agency_PressAgencyHRAlert] pahra
                                                        JOIN
                                                                [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                        JOIN
                                                                [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                        WHERE
                                                                pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL

                                                    UNION ALL
                                                               SELECT
                                                                    pahra.[PressAgencyHRID] as NotificationID,
                                                                    ISNULL(pahra.[Content] + N' của ông/bà ' + hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' + hr.[FullName]) AS Content,
                                                                    hr.Position + N' báo ' + pa.[Name] AS Note,
                                                                    [AlertDTG] as DoDTG,
                                                                    5 AS[Type],
                                                                    lunarDay,
                                                                    lunarMonth,
                                                                    lunarYear
                                                                FROM
                                                                    [agency_PressAgencyHRAlert] pahra
                                                                JOIN
                                                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                                                JOIN
                                                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                                                WHERE
                                                                    pahra.Deleted = 0 AND pahra.TypeDate = 2 AND[AlertDTG] IS NOT NULL
                        ),

                         SharedBirthdayEvents AS(
                                    SELECT BirthdayEvents.*
                                    FROM BirthdayEvents
                                     WHERE EXISTS(
                            SELECT 1
                            FROM[dbo].[SharingManagement] sm
                            WHERE
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId = {userId}
                AND isShared = 1 AND GroupName IS NULL) 
                                OR
                                (BirthdayEvents.NotificationID = sm.PressAgencyHRID AND sm.UserId IS NULL AND sm.UserEmail IS NULL AND GroupName IN(
                                        SELECT adm_Role.Name
                                        FROM[adm_EmployeeRole] empRole
                                        JOIN adm_Employee emp ON emp.EmployeeID = empRole.EmployeeID
                                        JOIN adm_Role ON adm_Role.RoleID = empRole.RoleID
                                        WHERE emp.EmployeeID = { userId}
                                    )
				         )))
                        ,
                        OtherEvents AS(
                            SELECT[SystemParameterID] as NotificationID
                                                                  ,[Name] as Content
                                                                  ,[Description] as Note
                                                                  ,[Ext4] as DoDTG
                                                                  ,[Ext1i] as Type,
										                          null as lunarDay,
										                          null as lunarMonth,
										                          null as lunarYear
                                                          FROM[adm_SystemParameter]
                                                          WHERE[FeatureID] = 1231 AND[Ext1i] in (2, 3)
                                                          AND[Deleted] = 0 AND Status = 1 AND[Ext4] IS NOT NULL
                                      UNION ALL
                                                      SELECT
                                                                pa.[PressAgencyID] as NotificationID,
                                                                CASE
                                                                        WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' + pa.[Name]
                                                                        ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                                                END AS Content,
                                                                at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                                                pa.[EstablishedDTG] AS DoDTG,
                                                                4 AS Type,

                                                                null as lunarDay,
										                        null as lunarMonth,
										                        null as lunarYear
                                                        FROM
                                                                agency_PressAgency pa
                                                        JOIN
                                                                AgencyType at ON pa.[Type] = at.[Id]
                                                        WHERE
                                                                pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
                        )

                        SELECT*
                        FROM(
                            SELECT * FROM SharedBirthdayEvents
                            UNION ALL
                            SELECT * FROM OtherEvents
                        ) AS EventAll";
            }
            cmdText += $@" WHERE 1 = 1 ";
            if (typeTime != null)
            {
                switch (typeTime)
                {
                    case SMX.FilterTime.Week:
                        DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
                        DateTime endOfWeek = startOfWeek.AddDays(6);

                        int[] lunarDateFrom = VietNamCalendar.convertSolar2Lunar(startOfWeek.Day, startOfWeek.Month, startOfWeek.Year, 7.0);
                        int[] lunarDateTo = VietNamCalendar.convertSolar2Lunar(endOfWeek.Day, endOfWeek.Month, endOfWeek.Year, 7.0);

                        cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{startOfWeek.Date}') OR (MONTH(DoDTG) = MONTH('{startOfWeek.Date}') AND DAY(DoDTG) >= DAY('{startOfWeek.Date}'))) AND (MONTH(DoDTG) < MONTH('{endOfWeek.Date}') OR (MONTH(DoDTG) = MONTH('{endOfWeek.Date}') AND " +
                    $"DAY(DoDTG) <= DAY('{endOfWeek.Date}')))) " +
                    $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFrom[1]} OR " +
                    $"(lunarMonth = {lunarDateFrom[1]} AND " +
                    $"lunarDay >= {lunarDateFrom[0]})) AND " +
                    $"(lunarMonth < {lunarDateTo[1]} OR " +
                    $"(lunarMonth = {lunarDateTo[1]} AND" +
                    $" lunarDay <=  {lunarDateTo[0]}))))  ";
                        break;
                    case SMX.FilterTime.Month:
                        DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                        DateTime endOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1).AddDays(-1);

                        int[] lunarDateFromm = VietNamCalendar.convertSolar2Lunar(startOfMonth.Day, startOfMonth.Month, startOfMonth.Year, 7.0);
                        int[] lunarDateToo = VietNamCalendar.convertSolar2Lunar(endOfMonth.Day, endOfMonth.Month, endOfMonth.Year, 7.0);

                        cmdText += $" and ((TYPE <> 5 AND (MONTH(DoDTG) > MONTH('{startOfMonth.Date}') OR (MONTH(DoDTG) = MONTH('{startOfMonth.Date}') AND DAY(DoDTG) >= DAY('{startOfMonth.Date}'))) AND (MONTH(DoDTG) < MONTH('{endOfMonth.Date}') OR (MONTH(DoDTG) = MONTH('{endOfMonth.Date}') AND " +
                     $"DAY(DoDTG) <= DAY('{endOfMonth.Date}')))) " +
                     $"OR (TYPE = 5 AND (lunarMonth > {lunarDateFromm[1]} OR " +
                     $"(lunarMonth = {lunarDateFromm[1]} AND " +
                     $"lunarDay >= {lunarDateFromm[0]})) AND " +
                     $"(lunarMonth < {lunarDateToo[1]} OR " +
                     $"(lunarMonth = {lunarDateToo[1]} AND" +
                     $" lunarDay <=  {lunarDateToo[0]}))))  ";
                        break;
                }
            }

            SqlCommand cmd = new SqlCommand(cmdText);
            //cmd.Parameters.AddWithValue("@userId", userId);

            using (DataContext dataContext = new DataContext())
            {
                var lst = dataContext.ExecuteSelect<ntf_Notification>(cmd);
                lst.ForEach(notification =>
                {
                    if (notification.Type == 5)
                    {
                        notification.DateConvert = lunarCalendar.ToDateTime(DateTime.Now.Year, notification.lunarMonth ?? 0, notification.lunarDay ?? 0, 0, 0, 0, 0).ToString("MMdd");
                    }
                    else
                    {
                        notification.DateConvert = notification.DoDTG?.ToString("MMdd") ?? "";
                    }
                });

                lst = lst.OrderBy(notification =>
                {
                    int notificationDate = int.Parse(notification.DateConvert);
                    int nowDate = int.Parse(DateTime.Now.ToString("MMdd"));
                    int difference = Math.Abs(notificationDate - nowDate);
                    return difference;
                }).ToList();
                return GetItemByPaging(lst, pagingInfo);
            }
        }
        public ntf_Notification GetNotificationByID(int? ID, int Type)
        {
            string cmdText;
            cmdText = @"WITH Events AS (
                                SELECT 
                                        hrr.[PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ' + hrr.[Relationship] + ' ' + hrr.[FullName] + N' của ông/bà ' + hr.[FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hrr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRRelatives] hrr
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = hrr.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND hrr.Deleted = 0 AND hrr.[DOB] IS NOT NULL

                                UNION ALL
                                SELECT 
                                        [PressAgencyHRID] as NotificationID,
                                        N'Sinh nhật ông/bà ' + [FullName] as Content,
                                        hr.[Position] + N' báo ' + pa.[Name] as Note,
                                        hr.[DOB] as DoDTG,
                                        1 as [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHR] hr
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        hr.Deleted = 0 AND pa.Deleted = 0 AND  hr.[DOB] IS NOT NULL
                                UNION ALL
                                SELECT 
                                        pahra.[PressAgencyHRID] as NotificationID,
                                        pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + N' (Dương lịch)' AS Content,
                                        hr.Position + N' báo ' + pa.[Name] AS Note, 
                                        pahra.[AlertDTG] as DoDTG,
                                        1 AS [Type],
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                        [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                        [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                        pahra.Deleted = 0 AND pahra.TypeDate = 1 AND pahra.[AlertDTG] IS NOT NULL
                UNION ALL
                                SELECT [SystemParameterID] as NotificationID
                                          ,[Name] as Content
                                          ,[Description] as Note
                                          ,[Ext4] as DoDTG
                                          ,[Ext1i] as Type,
										  null as lunarDay,
										  null as lunarMonth,
										  null as lunarYear
                                  FROM [adm_SystemParameter] 
                                  WHERE [FeatureID] = 1231 AND [Ext1i] in (2,3) 
                                  AND [Deleted] = 0 AND Status = 1 AND [Ext4] IS NOT NULL
                UNION ALL
                                SELECT 
                                        pa.[PressAgencyID] as NotificationID,
                                        CASE 
                                                WHEN DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) = 0 THEN N'Ngày thành lập ' +  pa.[Name]
                                                ELSE N'Kỷ niệm ' + CAST(DATEDIFF(YEAR, pa.EstablishedDTG, GETDATE()) AS NVARCHAR(10)) + N' năm ngày thành lập'
                                        END AS Content,
                                        at.[TypeName] + ' - ' + pa.[Name] AS Note,
                                        pa.[EstablishedDTG] AS DoDTG,
                                        4 AS Type,
										null as lunarDay,
										null as lunarMonth,
										null as lunarYear
                                FROM 
                                        agency_PressAgency pa
                                JOIN 
                                        AgencyType at ON pa.[Type] = at.[Id]
                                WHERE 
                                        pa.[Deleted] = 0 AND pa.[EstablishedDTG] IS NOT NULL
            UNION ALL
                               SELECT 
                                    pahra.[PressAgencyHRID] as NotificationID,
                                    ISNULL(pahra.[Content] + N' của ông/bà ' +  hr.[FullName] + ' (' + CONVERT(NVARCHAR, pahra.[lunarDay]) + '/' + CONVERT(NVARCHAR, pahra.[lunarMonth]) + '/' + CONVERT(NVARCHAR, pahra.[lunarYear]) + N' - Âm lịch)', pahra.[Content] + N' của ông/bà ' +  hr.[FullName]) AS Content,
                                    hr.Position + N' báo ' + pa.[Name] AS Note, 
                                    [AlertDTG] as DoDTG,
                                    5 AS [Type],
									lunarDay,
									lunarMonth,
									lunarYear
                                FROM 
                                    [agency_PressAgencyHRAlert] pahra 
                                JOIN 
                                    [agency_PressAgencyHR] hr ON hr.[PressAgencyHRID] = pahra.[PressAgencyHRID]
                                JOIN 
                                    [agency_PressAgency] pa ON hr.[PressAgencyID] = pa.[PressAgencyID]
                                WHERE 
                                    pahra.[Deleted] = 0 AND pahra.[TypeDate] = 2 AND [AlertDTG] IS NOT NULL
             )
             SELECT * FROM Events Where NotificationID = @NotificationID AND Type = @Type";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NotificationID", ID);
            cmd.Parameters.AddWithValue("@Type", Type);


            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd).FirstOrDefault();
            }
        }
        #region Email
        public bool IsRecordCurrentDate()
        {
            string checkDuplicateSql = $@"
                                        SELECT TOP(1) *
                                        FROM ntf_NotificationHistory
                                        WHERE CONVERT(DATE, SendDTG) = CONVERT(DATE, GETDATE());";
            SqlCommand cmdDublicate = new SqlCommand(checkDuplicateSql);
            using (DataContext context = new DataContext())
            {
                var record = context.ExecuteSelect<ntf_NotificationHistory>(cmdDublicate).SingleOrDefault();
                if (record != null)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckDuplicateRecordForYear(int? alertID)
        {
            if(alertID != null)
            {
                string sql = @"SELECT * FROM ntf_Notification WHERE AlertID = @hrID AND (isDeleted = 0 OR isDeleted IS NULL)   ORDER BY CreatedDTG DESC";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@hrID", alertID);

                using (DataContext context = new DataContext())
                {
                    var scanNoti = context.ExecuteSelect<ntf_Notification>(cmd).SingleOrDefault();
                    if (scanNoti != null)
                    {
                        return CheckDuplicateRecord(scanNoti);
                    }
                }
                return false;
            }
            return false;
        }
        public bool CheckDuplicateRecord(ntf_Notification item)
        {
            string checkDuplicateSql = $@"
                            SELECT 1
                            FROM ntf_Notification
                            WHERE YEAR(CreatedDTG) = YEAR('{item.CreatedDTG}')
                            AND Type = @Type
                            AND AlertID = @AlertID AND (isDeleted = @isDeleted OR isDeleted IS NULL)";
            SqlCommand cmdDublicate = new SqlCommand(checkDuplicateSql);
            cmdDublicate.Parameters.AddWithValue("@Type", item.Type);
            cmdDublicate.Parameters.AddWithValue("@AlertID", item.AlertID);
            cmdDublicate.Parameters.AddWithValue("@isDeleted", item.isDeleted);

            using (DataContext context = new DataContext())
            {
                var record = context.ExecuteSelect<ntf_Notification>(cmdDublicate).SingleOrDefault();
                if (record != null)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Email
        public List<ntf_Notification> GetListThongBaoLunarChuaGuiDu(int? numberOfSend)
        {
            string cmdText = @"select 
	                                noti.*,
	                                notiHis.NumberOfSend
                                from ntf_Notification noti
                                outer apply 
	                                (select count(notiH.NotificationHistoryID) as NumberOfSend
	                                from ntf_NotificationHistory notiH
	                                where notiH.RefID = noti.NotificationID and notiH.RefType = noti.Type) notiHis
                                where DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) >= 0 
                                AND DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) <= @NumberOfSend 
                                and notiHis.NumberOfSend <= @NumberOfSend 
                                and noti.Type = @lunar 
                                AND (noti.isDeleted = 0 OR noti.isDeleted IS NULL)";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NumberOfSend", numberOfSend);
            cmd.Parameters.AddWithValue("@lunar", SMX.Notification.CauHinhGuiThongBao.NgayGio);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd);
            }
        }
        public List<ntf_Notification> GetListThongBaoSinhNhatChuaGuiDu(int? numberOfSend)
        {
            string cmdText = @"select 
	                                noti.*,
	                                notiHis.NumberOfSend
                                from ntf_Notification noti
                                outer apply 
	                                (select count(notiH.NotificationHistoryID) as NumberOfSend
	                                from ntf_NotificationHistory notiH
	                                where notiH.RefID = noti.NotificationID and notiH.RefType = noti.Type) notiHis
                                where DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) >= 0 
                                AND DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) <= @NumberOfSend 
                                and notiHis.NumberOfSend <= @NumberOfSend 
                                and noti.Type = @SinhNhat 
                                AND (noti.isDeleted = 0 OR noti.isDeleted IS NULL)";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NumberOfSend", numberOfSend);
            cmd.Parameters.AddWithValue("@SinhNhat", SMX.Notification.CauHinhGuiThongBao.SinhNhat);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd);
            }
        }

        public List<ntf_Notification> GetListThongBaoNgayKyNiemChuaGuiDu(int? numberOfSend)
        {
            string cmdText = @"select 
	                            noti.*,
	                            notiHis.NumberOfSend
                            from ntf_Notification noti
                            outer apply 
	                                (select count(notiH.NotificationHistoryID) as NumberOfSend
	                                from ntf_NotificationHistory notiH
	                                where notiH.RefID = noti.NotificationID and notiH.RefType = noti.Type) notiHis
                                where DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) >= 0 
                                and DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) <= @NumberOfSend
                                and notiHis.NumberOfSend <= @NumberOfSend and noti.Type = @KyNiem AND (noti.isDeleted = 0 OR noti.isDeleted IS NULL)";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NumberOfSend", numberOfSend);
            cmd.Parameters.AddWithValue("@KyNiem", SMX.Notification.CauHinhGuiThongBao.KyNiem);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd);
            }
        }

        public List<ntf_Notification> GetListThongBaoNgayThanhLapChuaGuiDu(int? numberOfSend)
        {
            string cmdText = @"select 
	                            noti.*,
	                            notiHis.NumberOfSend
                            from ntf_Notification noti
                            outer apply 
	                            (select count(notiH.NotificationHistoryID) as NumberOfSend
	                            from ntf_NotificationHistory notiH
	                            where notiH.RefID = noti.NotificationID and notiH.RefType = noti.Type) notiHis
                            where DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) >= 0 
                            and  DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) <= @NumberOfSend 
                            and notiHis.NumberOfSend <= @NumberOfSend and noti.Type = @NgayThanhLap AND (noti.isDeleted = 0 OR noti.isDeleted IS NULL)";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NumberOfSend", numberOfSend);
            cmd.Parameters.AddWithValue("@NgayThanhLap", SMX.Notification.CauHinhGuiThongBao.NgayThanhLap);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd);
            }
        }

        public List<ntf_Notification> GetListThongBaoNgayTruyenThongChuaGuiDu(int? numberOfSend)
        {
            string cmdText = @"select 
	                            noti.*,
	                            notiHis.NumberOfSend
                            from ntf_Notification noti
                            outer apply 
	                            (select count(notiH.NotificationHistoryID) as NumberOfSend
	                            from ntf_NotificationHistory notiH
	                            where notiH.RefID = noti.NotificationID and notiH.RefType = noti.Type) notiHis
                            where DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) >= 0 
                             AND DATEDIFF(DAY, GETDATE(), CAST(CAST(YEAR(GETDATE()) AS varchar) + '-' + CAST(MONTH(noti.DoDTG) AS varchar) + '-' + CAST(DAY(noti.DoDTG) AS varchar) AS DATETIME)) <= @NumberOfSend 
                            and notiHis.NumberOfSend <= @NumberOfSend and noti.Type = @TruyenThong AND (noti.isDeleted = 0 OR noti.isDeleted IS NULL)";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@NumberOfSend", numberOfSend);
            cmd.Parameters.AddWithValue("@TruyenThong", SMX.Notification.CauHinhGuiThongBao.TruyenThong);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<ntf_Notification>(cmd);
            }
        }
    }
}