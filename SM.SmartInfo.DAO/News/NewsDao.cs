using SM.SmartInfo.CacheManager;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO.News
{
    public class NewsDao : BaseDao
    {
        private string ConnectionString = ConfigUtils.ConnectionString;

        public void UpdateSingleNews(SingleNews item)
        {
            string sql = @"UPDATE SingleNews SET Title = @Title, PostedDate = @PostedDate, Summary = @Summary, Link = @Link, 
                            UpdatedDTG = @UpdatedDTG, UpdatedBy = @UpdatedBy, Version = Version + 1,
                            Chanel = @Chanel WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@PostedDate", item.PostedDate);
                cmd.Parameters.AddWithValue("@Summary", item.Summary);
                cmd.Parameters.AddWithValue("@UpdatedDTG", item.UpdatedDTG);
                cmd.Parameters.AddWithValue("@UpdatedBy", item.UpdatedBy);
                cmd.Parameters.AddWithValue("@Chanel", item.Chanel);
                cmd.Parameters.AddWithValue("@Link", item.Link);
                cmd.Parameters.AddWithValue("@Id", item.Id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertHastag(HastagManagement item)
        {
            string sql = @"INSERT INTO HastagManagement (Name, CreatedBy, CreatedDTG) VALUES(@Name, @CreatedBy, @CreatedDTG)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG);
                cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertSingleNews(SingleNews item)
        {
            string sql;

            if (item.CampaignId != null)
            {
                sql = $@"INSERT INTO SingleNews (Title, PostedDate, Summary, Link, CreatedBy, CreatedDTG, Version, Deleted, Chanel, NewsId, CampaignId)
                            VALUES(@Title, @PostedDate, @Summary, @Link, @CreatedBy, @CreatedDTG, @Version, @Deleted, @Chanel, @NewsId, {item.CampaignId})";
            }
            else
            {
                sql = @"INSERT INTO SingleNews (Title, PostedDate, Summary, Link, CreatedBy, CreatedDTG, Version, Deleted, Chanel, NewsId)
                            VALUES(@Title, @PostedDate, @Summary, @Link, @CreatedBy, @CreatedDTG, @Version, @Deleted, @Chanel, @NewsId)";
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@PostedDate", item.PostedDate);
                cmd.Parameters.AddWithValue("@Summary", item.Summary);
                cmd.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG);
                cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);
                cmd.Parameters.AddWithValue("@Chanel", item.Chanel);
                cmd.Parameters.AddWithValue("@Link", item.Link);
                cmd.Parameters.AddWithValue("@NewsId", item.NewsId);
                cmd.Parameters.AddWithValue("@Version", 1);
                cmd.Parameters.AddWithValue("@Deleted", 0);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool IsSingleCamp(int? NewsID)
        {
            using (DataContext context = new DataContext())
            {
                string query = "SELECT COUNT(*) FROM News WHERE NewsID = @NewsID AND isSingleCamp = 1";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@NewsID", NewsID);

                // Thực hiện câu truy vấn để đếm số lượng mục thỏa mãn điều kiện.
                int count = Convert.ToInt32(context.ExecuteScalar(cmd));

                // Nếu count > 0, tức là đã tồn tại ít nhất một mục thỏa mãn điều kiện.
                return count > 0;
            }
        }
        public bool SetIsSingleCamp(int? NewsID, bool isSingleCamp)
        {
            using (DataContext context = new DataContext())
            {
                string query = "UPDATE News SET isSingleCamp = @IsSingleCamp WHERE NewsID = @NewsID";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@IsSingleCamp", isSingleCamp ? 1 : 0); 
                cmd.Parameters.AddWithValue("@NewsID", NewsID);
                int rowsAffected = context.ExecuteNonQuery(cmd); 
                return rowsAffected > 0; 
            }
        }
        public SingleNews GetSingleNewsByNewsId(int? NewsId)
        {
            string sql = @"SELECT TOP 1 * FROM SingleNews WHERE NewsId = @NewsId AND Deleted = 0 ORDER BY Id DESC";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                cmd.Parameters.AddWithValue("@NewsId", NewsId);

                return dataContext.ExecuteSelect<SingleNews>(cmd).FirstOrDefault();
            }
        }

        public int? GetLatestSingleNewsIdByCreator(int? NewsId)
        {
            string sql = @"SELECT Id FROM SingleNews WHERE NewsId = @NewsId AND CreatedBy COLLATE Latin1_General_BIN = @CreatedBy AND Deleted = 0 ORDER BY Id DESC";

            SqlCommand cmd = new SqlCommand(sql);

            using (DataContext dataContext = new DataContext())
            {
                cmd.Parameters.AddWithValue("@CreatedBy", Profiles.MyProfile.UserName);
                cmd.Parameters.AddWithValue("@NewsId", NewsId);

                return dataContext.ExecuteSelect<int>(cmd).FirstOrDefault();
            }
        }

        public void DeleteSingleNews(int? SingleNewsId)
        {
            string sql = @"DELETE FROM SingleNews WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Id", SingleNewsId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<SharedComponent.Entities.News> Get5TinTichCucMoiNhat()
        {
            string query = @"
                select top 5 enNews.*
                from News enNews
                where enNews.Type = @type and enNews.Deleted = 0
                order by CreatedDTG desc ";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@type", SMX.News.Type.News);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(command);
            }
        }

        public List<SharedComponent.Entities.News> Get4TinTieuCucMoiNhat()
        {
            string query = @"
                select top 4 enNews.*
                from News enNews
                where enNews.Type = @type and enNews.Deleted = 0
                order by CreatedDTG desc";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@type", SMX.News.Type.NegativeNews);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(command);
            }
        }

        public List<SharedComponent.Entities.News> SearchNews(string searchText, PagingInfo pagingInfo)
        {
            string sql = @"select * from News a
                        where a.Deleted = @IsNotDeleted and a.Type = @Type ";

            if (!string.IsNullOrWhiteSpace(searchText))
                sql += " and (a.Name like @SearchText " +
                    "or a.RatedLevel like @SearchText " +
                    "or a.Concluded like @SearchText " +
                    "or a.OtherNote like @SearchText " +
                    "or a.Content like @SearchText)";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(searchText));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.News);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<SharedComponent.Entities.News>(context, cmd, "NewsID desc", pagingInfo);
            }
        }

        public List<SharedComponent.Entities.News> SearchNewsForView(SharedComponent.Entities.News filter, SharedComponent.Entities.News quickFilterNews)
        {
            string sql = @"select * from News a
                        where a.Deleted = @IsNotDeleted and a.Type = @Type ";

            SqlCommand cmd = new SqlCommand();
            if (filter != null)
            {
                // Filter
                if (filter.PostingFromDTG != null)
                    sql += " and DATEDIFF(DAY, @PostingFromDTG, a.PostingFromDTG) >= 0";

                if (filter.PostingToDTG != null)
                    sql += " and DATEDIFF(DAY, @PostingToDTG, a.PostingToDTG) >= 0";

                if (filter.NumberOfPublish != null)
                    sql += " and a.NumberOfPublish = @NumberOfPublish";

                if (filter.CatalogID != null)
                    sql += " and a.CatalogID = @CatalogID";

                if (!string.IsNullOrWhiteSpace(filter.SearchText))
                    sql += " and (a.Name like @SearchText " +
                        "or a.Content like @SearchText) ";

                cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.SearchText));
                cmd.Parameters.AddWithValue("@PostingFromDTG", filter.PostingFromDTG);
                cmd.Parameters.AddWithValue("@NumberOfPublish", filter.NumberOfPublish);
                cmd.Parameters.AddWithValue("@CatalogID", filter.CatalogID);
                cmd.Parameters.AddWithValue("@PostingToDTG", filter.PostingToDTG);
            }

            if (quickFilterNews != null)
            {
                // Quick Filter
                if (quickFilterNews.YearCreated != null)
                    sql += " and YEAR(a.CreatedDTG) = @YearCreated ";

                if (quickFilterNews.Category != null)
                    sql += " and a.Category = @Category ";

                cmd.Parameters.AddWithValue("@YearCreated", quickFilterNews.YearCreated);
                cmd.Parameters.AddWithValue("@Category", quickFilterNews.Category);
            }

            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.News);
            cmd.CommandText = sql;

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
            }
        }

        public List<SharedComponent.Entities.News> SearchDetailPositiveNews(PositiveNews filter)
        {
            string sql = @"
                select news.NewsID
                from News news
                left join PositiveNews a on a.NewsID = news.NewsID and a.Deleted = 0
                where news.Deleted = @IsNotDeleted
                and news.Type = @News ";

            SqlCommand cmd = new SqlCommand();
            if (filter != null)
            {
                if (filter.FromPublishDTG != null)
                    sql += " and DATEDIFF(DAY, @FromPublishDTG, a.PublishDTG) >= 0";

                if (filter.ToPublishDTG != null)
                    sql += " and DATEDIFF(DAY, a.PublishDTG, @ToPublishDTG) >= 0";

                if (filter.Type != null)
                    sql += " and a.Type = @Type";

                if (filter.CampaignID != null)
                    sql += " and a.CampaignID = @CampaignID";

                if (filter.PressAgencryID != null)
                    sql += " and a.PressAgencryID = @PressAgencryID";

                if (!string.IsNullOrWhiteSpace(filter.SearchText))
                    sql += " and (a.Title like @SearchText " +
                        "or a.Brief like @SearchText " +
                        "or a.Url like @SearchText) ";

                cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filter.SearchText));
                cmd.Parameters.AddWithValue("@FromPublishDTG", filter.FromPublishDTG);
                cmd.Parameters.AddWithValue("@ToPublishDTG", filter.ToPublishDTG);
                cmd.Parameters.AddWithValue("@Type", filter.Type);
                cmd.Parameters.AddWithValue("@CampaignID", filter.CampaignID);
                cmd.Parameters.AddWithValue("@PressAgencryID", filter.PressAgencryID);
            }

            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@News", SMX.News.Type.News);
            cmd.CommandText = sql;

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
            }
        }

        public List<SharedComponent.Entities.News> BuildTreeListNews()
        {
            string query = @"select
	                            YEAR(a.CreatedDTG) as YearCreated
	                            , a.Category
	                            , count(1) as CategoryCount
                            from News a
                            where a.Deleted = 0
                            and a.Type = @Type
                            and a.Category is not null
                            group by a.Category, YEAR(a.CreatedDTG)
                            order by YEAR(a.CreatedDTG) desc ";

            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@Type", SMX.News.Type.News);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(command);
            }
        }

        public List<SingleNews> GetListSingleNewsByNewsID(int? NewsId, PagingInfo paging)
        {
            string sql = @"SELECT * FROM SingleNews WHERE NewsId = @NewsId AND Deleted = 0 AND CampaignId is null";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@NewsId", NewsId);

            using(DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<SingleNews>(dataContext, cmd, " Id DESC", paging);
            }
        }
        
        public List<SingleNews> GetListSingleNewsByNewsIDAndCampaignID(int? NewsId, int? CampaignId, PagingInfo paging)
        {
            string sql = @"SELECT * FROM SingleNews WHERE NewsId = @NewsId AND CampaignId = @CampaignId AND Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@NewsId", NewsId);
            cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

            using(DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<SingleNews>(dataContext, cmd, " Id DESC", paging);
            }
        }
        
        public List<SingleNews> GetListSingleNewsByNewsIDAndCampaignIDNoPaging(int? NewsId, int? CampaignId)
        {
            string sql = @"SELECT * FROM SingleNews WHERE NewsId = @NewsId AND CampaignId = @CampaignId AND Deleted = 0";

            SqlCommand cmd = new SqlCommand(sql);

            cmd.Parameters.AddWithValue("@NewsId", NewsId);
            cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

            using(DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<SingleNews>(cmd);
            }
        }

        public List<SharedComponent.Entities.News> SearchNegativeNews(string searchText, PagingInfo pagingInfo)
        {
            string sql = @"select * from News a
                        where a.Deleted = @IsNotDeleted and a.Type = @Type ";

            if (!string.IsNullOrWhiteSpace(searchText))
                sql += " and (a.Name like @SearchText " +
                        "or a.RatedLevel like @SearchText " +
                        "or a.PressAgency like @SearchText " +
                        "or a.Resolution like @SearchText " +
                        "or a.ResolutionContent like @SearchText " +
                        "or a.Concluded like @SearchText " +
                        "or a.OtherNote like @SearchText) ";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(searchText));
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.NegativeNews);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<SharedComponent.Entities.News>(context, cmd, "NewsID desc", pagingInfo);
            }
        }

        public List<SharedComponent.Entities.News> GetItemByListID(List<int> lstID)
        {
            string sql = @"select * from News a
                        where a.Deleted = @IsNotDeleted and a.NewsID in ({0}) ";

            sql = string.Format(sql, BuildInCondition(lstID));

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
            }
        }

        #region News tin tuc
        public List<SharedComponent.Entities.News> GetListNews()
        {
            string sql = @"select * from News a
                            where a.Deleted = @IsNotDeleted and a.Type = @Type 
                            order by a.PostingFromDTG desc";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.News);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
            }
        }

        public SharedComponent.Entities.News GetNews_ByID(int? ID)
        {
            string query = @"select a.*, enCata.Name as CatalogName from News a
                                left join CatalogNews enCata on enCata.CatalogNewsID=a.CatalogID
                                where a.Deleted=@NotDeleted and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", ID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd).FirstOrDefault();
            }
        }

        public bool IsNameExists(string name)
        {
            using (DataContext context = new DataContext())
            {
                string query = "SELECT COUNT(*) FROM HastagManagement WHERE Name = @Name";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Name", name);

                // Thực hiện câu truy vấn để đếm số lượng mục có Name trùng khớp với giá trị truyền vào.
                int count = Convert.ToInt32(context.ExecuteScalar(cmd));

                // Nếu count > 0, tức là đã tồn tại một mục có Name trùng khớp.
                return count > 0;
            }
        }

        public List<HastagManagement> GetListHastag(NewsParam param)
        {
            using (DataContext context = new DataContext())
            {
                string query = "SELECT * FROM HastagManagement";

                if (!string.IsNullOrEmpty(param.Hastag))
                {
                    // Nếu param.Hastag tồn tại, thêm điều kiện WHERE vào câu truy vấn
                    query += " WHERE Name LIKE @Hastag";
                }

                SqlCommand cmd = new SqlCommand(query);

                if (!string.IsNullOrEmpty(param.Hastag))
                {
                    // Nếu param.Hastag tồn tại, đặt giá trị tham số @Hastag
                    cmd.Parameters.AddWithValue("@Hastag", "%" + param.Hastag + "%");
                }

                return context.ExecuteSelect<HastagManagement>(cmd);
            }
        }

        #endregion

        #region PositiveNews

        public List<PositiveNews> GetListPositiveNews_ByNewsID(int? ID, PagingInfo pagingInfo)
        {
            string query = @"select a.*,
                                    enPre.Name as PressAgencyName,
                                    enCam.Campaign as CampaignName
                            from PositiveNews a
                            left join CampaignNews enCam on enCam.CampaignNewsID = a.CampaignID
                            left join agency_PressAgency enPre on enPre.PressAgencyID = a.PressAgencryID
                            where a.Deleted=@NotDeleted and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", ID);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<PositiveNews>(context, cmd, "UpdatedDTG desc, CreatedDTG desc", pagingInfo);
            }
        }

        public void DeletePositiveNewsByPositiveNewsID(int? itemID)
        {
            string sql = @"update PositiveNews
                                set Deleted = @Deleted
                                where PositiveNewsID = @PositiveNewsID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@PositiveNewsID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public List<PositiveNews> GetListPositiveNews_ByID(int? ID)
        {
            string query = @"select * from  PositiveNews a
                               where a.Deleted=0 and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", ID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<PositiveNews>(cmd);
            }
        }

        #endregion

        #region CampaignNews

        public List<CampaignNews> GetListCampaignNews_ByNewsID(int? ID, PagingInfo pagingInfo)
        {
            string query = @"select * from  CampaignNews a
                               where a.Deleted=@NotDeleted and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", ID);

            using (DataContext context = new DataContext())
            {
                return base.ExecutePaging<CampaignNews>(context, cmd, "UpdatedDTG desc, CreatedDTG desc", pagingInfo);
            }
        }

        public List<CampaignNews> GetListCampaignNews_ByNewsID(int? newsID)
        {
            string query = @"select * from  CampaignNews a
                               where a.Deleted = @NotDeleted ";

            if (newsID != null)
                query += " and a.NewsID = @NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NewsID", newsID);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<CampaignNews>(cmd);
            }
        }

        public void DeleteCampaignNewsByCampaignNewsID(int? itemID)
        {
            string sql = @"update CampaignNews
                                set Deleted = @Deleted
                                where CampaignNewsID = @CampaignNewsID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@CampaignNewsID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        public void DeleteSingleNewsByCampainIdAndNewsId(int? NewsId, int? CampaignId)
        {
            string sql = @"DELETE FROM SingleNews WHERE NewsId = @NewsId AND CampaignId = @CampaignId";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@NewsId", NewsId);
                cmd.Parameters.AddWithValue("@CampaignId", CampaignId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}