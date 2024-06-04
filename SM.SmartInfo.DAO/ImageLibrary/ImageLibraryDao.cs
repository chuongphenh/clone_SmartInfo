using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;


namespace SM.SmartInfo.DAO.ImageLibrary
{
    public class ImageLibraryDao : BaseDao
    {
        private string ConnectionString = ConfigUtils.ConnectionString;
        private CommonList.AttachmentDao _dao = new CommonList.AttachmentDao();

        public void PermanentlyDelete(ImageLibraryParam param)
        {
            foreach (var item in param.listDeleteImg)
            {
                string sql = @"DELETE FROM [dbo].[ImgCatalogManagement] WHERE UserId = @UserId AND ImgId = @ImgId AND isDeleted = 1 ";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ImgId", item);
                    command.Parameters.AddWithValue("@UserId", param.CurrentUserId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RevertDeletedItem(ImageLibraryParam param)
        {
            foreach(var item in param.listDeleteImg)
            {
                string sql = @"UPDATE [dbo].[ImgCatalogManagement] SET isDeleted = 0 WHERE UserId = @UserId AND ImgId = @ImgId AND isDeleted = 1 ";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ImgId", item);
                    command.Parameters.AddWithValue("@UserId", param.CurrentUserId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ViewDeletedItem(ImageLibraryParam param)
        {
            string sql = @"SELECT a.* FROM [dbo].[ImgCatalogManagement] icm JOIN [dbo].[adm_Attachment] a ON icm.ImgId = a.AttachmentID WHERE isDeleted = 1 AND UserId = @userId";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@userId", param.CurrentUserId);

            using (DataContext dataContext = new DataContext())
            {
                param.listAttachment = ExecutePaging<adm_Attachment>(dataContext, cmd, " CreatedDTG ", param.PagingInfo);
            }
        }

        public bool isAdmin()
        {
            return Profiles.MyProfile.Roles.Any(x => x.Name.ToLower().Contains("qtht"));
        }

        public void GetRootImageByFilter(ImageLibraryParam param)
        {
            string sql;

            if (isAdmin())
            {
                sql = @"SELECT a.*
                        FROM adm_Attachment a
                        LEFT JOIN ImgCatalogManagement m ON a.AttachmentID = m.ImgId 
                        WHERE ContentType LIKE '%image%' AND a.refType = @refType ";
                if (param.year != null)
                {
                    sql += $" AND YEAR(a.CreatedDTG) = {param.year}";
                }
                if (param.postedDTG != null)
                {
                    sql += $" AND (CONVERT(varchar(10), a.CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
                }
                if (param.dateFrom != null && param.dateTo != null)
                {
                    if (param.postedDTG != null)
                    {
                        sql += " OR";
                    }
                    else
                    {
                        sql += " AND";
                    }
                    sql += $" (a.CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND a.CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
                }
                sql += " OR a.AttachmentID IN (SELECT ImgId FROM [dbo].[ImgCatalogManagement] WHERE refType = @refType)";
            }
            else
            {
                sql = $@"Select r.* FROM (SELECT a.* FROM [dbo].[adm_Attachment] a INNER JOIN [dbo].[SharingManagement] sm 
                            ON a.RefID = sm.PressAgencyHRID WHERE a.RefType = 3 AND sm.UserId = {param.CurrentUserId}";
                if (param.year != null)
                {
                    sql += $" AND YEAR(a.CreatedDTG) = {param.year}";
                }
                if (param.postedDTG != null)
                {
                    sql += $" AND (CONVERT(varchar(10), a.CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
                }
                if (param.dateFrom != null && param.dateTo != null)
                {
                    if (param.postedDTG != null)
                    {
                        sql += " OR";
                    }
                    else
                    {
                        sql += " AND";
                    }
                    sql += $" (a.CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND a.CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
                }
                sql += @"UNION
                         SELECT a.* FROM [dbo].[adm_Attachment] a WHERE a.RefType <> 3) AS r Where r.refType = @refType";
                if (param.year != null)
                {
                    sql += $" AND YEAR(a.CreatedDTG) = {param.year}";
                }
                if (param.postedDTG != null)
                {
                    sql += $" AND (CONVERT(varchar(10), a.CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
                }
                if (param.dateFrom != null && param.dateTo != null)
                {
                    if (param.postedDTG != null)
                    {
                        sql += " OR";
                    }
                    else
                    {
                        sql += " AND";
                    }
                    sql += $" (a.CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND a.CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
                }
            }

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@refType", param.refType);
            
            using (DataContext dataContext = new DataContext())
            {
                param.listAttachment = ExecutePaging<adm_Attachment>(dataContext, cmd, " CreatedDTG DESC ", param.PagingInfo);
            }
        }

        public void GetRefTypeById(ImageLibraryParam param)
        {
            string sql = @"SELECT RefType FROM [dbo].[ImageCatalog] WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Id", param.Id);

            using (DataContext dataContext = new DataContext())
            {
                param.refType = (int)dataContext.ExecuteScalar(cmd);
            }
        }
        
        public void AddImageToNode(ImageLibraryParam param)
        {

            string all = @"SELECT * FROM [dbo].[ImgCatalogManagement]";

            SqlCommand cmd = new SqlCommand(all);

            List<ImgCatalogManagement> list = new List<ImgCatalogManagement>();

            using (DataContext dataContext = new DataContext())
            {
                list = dataContext.ExecuteSelect<ImgCatalogManagement>(cmd);
            }

            foreach (var item in param.listInsertImg)
            {
                if (list.Any(x => x.ImgId.Equals(item) && x.CatalogId.Equals(param.Id) && x.refType.Equals(param.refType)))
                {
                    continue;
                }

                string sql = @"INSERT INTO [dbo].[ImgCatalogManagement] (ImgId, CatalogId, CreatedDTG, UserId, isDeleted, refType)
                            VALUES (@ImgId, @CatalogId, @CreatedDTG, @UserId, @isDeleted, @refType)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ImgId", item);
                    command.Parameters.AddWithValue("@CatalogId", param.Id);
                    command.Parameters.AddWithValue("@CreatedDTG", DateTime.Now);
                    command.Parameters.AddWithValue("@UserId", param.CurrentUserId);
                    command.Parameters.AddWithValue("@isDeleted", 0);
                    command.Parameters.AddWithValue("@refType", param.refType);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            
        }

        public void GetListEditableNode(ImageLibraryParam param)
        {
            string sql = @"SELECT * FROM [dbo].[ImageCatalog] WHERE ParentId is not null OR refType > 12";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.listImageCatalog = dataContext.ExecuteSelect<ImageCatalog>(cmd);
            }
        }

        public void DeleteSelectedImg(ImageLibraryParam param)
        {
            foreach(var item in param.listDeleteImg)
            {
                string sql = @"DELETE FROM [dbo].[ImgCatalogManagement] WHERE ImgId = @Id AND CatalogId = @catalogId AND UserId = @userId"; 

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Id", item);
                    command.Parameters.AddWithValue("@catalogId", param.Id);
                    command.Parameters.AddWithValue("@userId", param.CurrentUserId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteSelectedImgOriginal(ImageLibraryParam param)
        {
            foreach (var item in param.listDeleteImg)
            {
                _dao.DeleteAttachment(item);
                _dao.DeleteCacheECM_ByAttachmentID(item);
            }
        }
       

        public void AddNewNode(ImageLibraryParam param)
        {
            if(param.ImageCatalog.ParentId != null)
            {
                string sql = @"INSERT INTO [dbo].[ImageCatalog] (CatalogName, ParentId, CreatedBy, CreatedDTG, CreatedUserId, refType)
                    VALUES (@CatalogName, @ParentId, @CreatedBy, @CreatedDTG, @CreatedUserId, @refType)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@CatalogName", param.ImageCatalog.CatalogName);
                    command.Parameters.AddWithValue("@ParentId", param.ImageCatalog.ParentId);
                    command.Parameters.AddWithValue("@CreatedBy", param.ImageCatalog.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedDTG", param.ImageCatalog.CreatedDTG);
                    command.Parameters.AddWithValue("@CreatedUserId", param.ImageCatalog.CreatedUserId);
                    command.Parameters.AddWithValue("@refType", param.ImageCatalog.refType);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                string sql = @"INSERT INTO [dbo].[ImageCatalog] (CatalogName, CreatedBy, CreatedDTG, CreatedUserId, refType)
                    VALUES (@CatalogName, @CreatedBy, @CreatedDTG, @CreatedUserId, @refType)";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@CatalogName", param.ImageCatalog.CatalogName);
                    command.Parameters.AddWithValue("@CreatedBy", param.ImageCatalog.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedDTG", param.ImageCatalog.CreatedDTG);
                    command.Parameters.AddWithValue("@CreatedUserId", param.ImageCatalog.CreatedUserId);
                    command.Parameters.AddWithValue("@refType", param.ImageCatalog.refType);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            
        }

        public void GetListImageCatalog(ImageLibraryParam param)
        {
            string sql = @"SELECT * FROM [dbo].[ImageCatalog]";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.listImageCatalog = dataContext.ExecuteSelect<ImageCatalog>(cmd);
            }
        }

        public void EditNoteName(ImageLibraryParam param)
        {
            string sql = @"UPDATE [dbo].[ImageCatalog] SET CatalogName = @CatalogName WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", param.Id);
                command.Parameters.AddWithValue("@CatalogName", param.CatalogName);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteSelectedNode(ImageLibraryParam param)
        {
            string sql = @"DELETE FROM [dbo].[ImageCatalog] WHERE Id = @Id OR ParentId = @Id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", param.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void GetImageCatalogById(ImageLibraryParam param)
        {
            string sql = @"SELECT * FROM [dbo].[ImageCatalog] WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Id", param.Id);

            using(DataContext dataContext = new DataContext())
            {
                param.ImageCatalog = dataContext.ExecuteSelect<ImageCatalog>(cmd).FirstOrDefault();
            }
        }

        public void GetListImage(ImageLibraryParam param)
        {
            string sql;
            if (isAdmin())
            {
                sql = @"SELECT * FROM [dbo].[adm_Attachment] WHERE ContentType LIKE '%image%'";
                if (param.year != null)
                {
                    sql += $" AND YEAR(CreatedDTG) = {param.year}";
                }
                if (param.postedDTG != null)
                {
                    sql += $" AND (CONVERT(varchar(10), CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
                }
                if (param.dateFrom != null && param.dateTo != null)
                {
                    if (param.postedDTG != null)
                    {
                        sql += " OR";
                    }
                    else
                    {
                        sql += " AND";
                    }
                    sql += $" (CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
                }
            }
            else
            {
                sql = $@"SELECT a.* FROM [dbo].[adm_Attachment] a INNER JOIN [dbo].[SharingManagement] sm 
                            ON a.RefID = sm.PressAgencyHRID WHERE a.RefType = 3 AND sm.UserId = {param.CurrentUserId}";
                if (param.year != null)
                {
                    sql += $" AND YEAR(a.CreatedDTG) = {param.year}";
                }
                if (param.postedDTG != null)
                {
                    sql += $" AND (CONVERT(varchar(10), a.CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
                }
                if (param.dateFrom != null && param.dateTo != null)
                {
                    if (param.postedDTG != null)
                    {
                        sql += " OR";
                    }
                    else
                    {
                        sql += " AND";
                    }
                    sql += $" (a.CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND a.CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
                }
                sql +=@"UNION
                        SELECT a.* FROM [dbo].[adm_Attachment] a WHERE a.RefType <> 3";
                if (param.year != null)
                {
                    sql += $" AND YEAR(a.CreatedDTG) = {param.year}";
                }
                if (param.postedDTG != null)
                {
                    sql += $" AND (CONVERT(varchar(10), a.CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
                }
                if (param.dateFrom != null && param.dateTo != null)
                {
                    if (param.postedDTG != null)
                    {
                        sql += " OR";
                    }
                    else
                    {
                        sql += " AND";
                    }
                    sql += $" (a.CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND a.CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
                }
            }

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.listAttachment = ExecutePaging<adm_Attachment>(dataContext, cmd, " CreatedDTG DESC ", param.PagingInfo);
            }
        }

        public void GetImageByFilter(ImageLibraryParam param)
        {
            string sql = @"SELECT a.* FROM [dbo].[adm_Attachment] a join [dbo].[ImgCatalogManagement] icm on a.AttachmentID = icm.ImgId
                        WHERE icm.CatalogId = @CatalogId AND icm.UserId = @userId AND icm.isDeleted = 0 AND a.ContentType LIKE '%image%'";

            if (param.year != null)
            {
                sql += $" AND YEAR(a.CreatedDTG) = {param.year}";
            }
            if (param.postedDTG != null)
            {
                sql += $" AND (CONVERT(varchar(10), a.CreatedDTG, 101) = '{Convert.ToDateTime(param.postedDTG).ToString("MM/dd/yyyy")}')";
            }
            if (param.dateFrom != null && param.dateTo != null)
            {
                if (param.postedDTG != null)
                {
                    sql += " OR";
                }
                else
                {
                    sql += " AND";
                }
                sql += $" (a.CreatedDTG >= '{Convert.ToDateTime(param.dateFrom).Date}' AND a.CreatedDTG <= '{Convert.ToDateTime(param.dateTo).Date}')";
            }

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@CatalogId", param.Id);
            cmd.Parameters.AddWithValue("@userId", param.CurrentUserId);

            using (DataContext dataContext = new DataContext())
            {
                param.listAttachment = ExecutePaging<adm_Attachment>(dataContext, cmd, " CreatedDTG DESC ", param.PagingInfo);
            }
        }
        
        public void GetListPostedYears(ImageLibraryParam param)
        {
            string sql = @"SELECT DISTINCT YEAR(CreatedDTG) AS distinct_years
                            FROM [dbo].[adm_Attachment]
                            ORDER BY distinct_years";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                param.listPostedYears = dataContext.ExecuteSelect<int>(cmd);
            }
        }
    }
}
