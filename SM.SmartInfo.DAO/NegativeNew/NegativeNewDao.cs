using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SoftMart.Kernel.Entity;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO.NegativeNew
{
    public class NegativeNewDao : BaseDao
    {
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

        #region Modification methods

        public void InsertNews(SharedComponent.Entities.News item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<SharedComponent.Entities.News>(item);
            }
        }

        public void UpdateNews(SharedComponent.Entities.News item)
        {
            int affectedRows;
            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.UpdateItem<SharedComponent.Entities.News>(item);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(Messages.ItemNotExitOrChanged);
            }
        }

        #endregion

        #region News
        public List<SharedComponent.Entities.News> GetListNews(int? negativeType, int? status, int? typeTime, PagingInfo paging)
        {
            string sql = @"select * from News a
                                where a.Deleted = @IsNotDeleted and a.Type = @Type";

            if (negativeType != null)
                sql += " and a.NegativeType = @NegativeType";

            if (status != null)
                sql += " and a.Status = @Status";

            if (typeTime != null)
            {
                switch (typeTime)
                {
                    case SMX.FilterTime.Week:
                        sql += " AND DATEDIFF(WEEK, a.CreatedDTG, getdate()) = 0";
                        break;
                    case SMX.FilterTime.Month:
                        sql += " AND DATEDIFF(MONTH, a.CreatedDTG, getdate()) = 0";
                        break;
                }
            }

            //sql += @" order by a.Classification, a.IncurredDTG desc, a.NegativeType";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.NegativeNews);
            cmd.Parameters.AddWithValue("@NegativeType", negativeType);
            cmd.Parameters.AddWithValue("@Status", status);

            using (DataContext context = new DataContext())
            {
                //return base.ExecutePaging<SharedComponent.Entities.News>(context, cmd, " Classification, IncurredDTG desc, NegativeType", paging);
                return base.ExecutePaging<SharedComponent.Entities.News>(context, cmd, " CreatedDTG desc", paging);
            }
        }

        public List<SharedComponent.Entities.News> SearchNegativeNews(SharedComponent.Entities.News filterNews, int? typeTime, PagingInfo paging)
        {
            string sql = @"select * from News a
                            where a.Deleted = @IsNotDeleted and a.Type = @Type";

            if (filterNews != null)
            {
                if (filterNews.FromIncurredDTG != null)
                    sql += " and DATEDIFF(DAY, @FromIncurredDTG, a.IncurredDTG) >= 0";

                if (filterNews.ToIncurredDTG != null)
                    sql += " and DATEDIFF(DAY, a.IncurredDTG, @ToIncurredDTG) >= 0";

                if (filterNews.NegativeType != null)
                    sql += " and a.NegativeType = @NegativeType";

                if (filterNews.Classification != null)
                    sql += " and a.Classification = @Classification";

                if (filterNews.Status != null)
                    sql += " and a.Status = @Status";

                if (!string.IsNullOrWhiteSpace(filterNews.SearchText))
                    sql += " and (a.Name like @SearchText " +
                            "or a.RatedLevel like @SearchText " +
                            "or a.PressAgency like @SearchText " +
                            "or a.Resolution like @SearchText " +
                            "or a.ResolutionContent like @SearchText " +
                            "or a.Concluded like @SearchText " +
                            "or a.OtherNote like @SearchText) ";
            }

            if (typeTime != null)
            {
                switch (typeTime)
                {
                    case SMX.FilterTime.Week:
                        sql += " AND DATEDIFF(WEEK, a.IncurredDTG, getdate()) = 0";
                        break;
                    case SMX.FilterTime.Month:
                        sql += " AND DATEDIFF(MONTH, a.IncurredDTG, getdate()) = 0";
                        break;
                }
            }

            //sql += @" order by a.Classification, a.IncurredDTG desc, a.NegativeType";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Status", filterNews.Status);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.NegativeNews);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NegativeType", filterNews.NegativeType);
            cmd.Parameters.AddWithValue("@ToIncurredDTG", filterNews.ToIncurredDTG);
            cmd.Parameters.AddWithValue("@Classification", filterNews.Classification);
            cmd.Parameters.AddWithValue("@FromIncurredDTG", filterNews.FromIncurredDTG);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filterNews.SearchText));

            using (DataContext context = new DataContext())
            {
                //return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
                //return base.ExecutePaging<SharedComponent.Entities.News>(context, cmd, " Classification, IncurredDTG desc, NegativeType", paging);
                return ExecutePaging<SharedComponent.Entities.News>(context, cmd, " NewsID desc", paging);
            }
        }

        public List<SharedComponent.Entities.News> SearchDetailNegativeNews(NegativeNews filterNegativeNews)
        {
            string sql = @"
                        select news.NewsID
                        from News news
                        left join NegativeNews a on a.NewsID = news.NewsID and a.Deleted = 0
                        where news.Deleted = @IsNotDeleted
                        and news.Type = @NegativeNews ";

            if (filterNegativeNews != null)
            {
                if (filterNegativeNews.FromIncurredDTG != null)
                    sql += " and DATEDIFF(DAY, @FromIncurredDTG, a.IncurredDTG) >= 0";

                if (filterNegativeNews.ToIncurredDTG != null)
                    sql += " and DATEDIFF(DAY, a.IncurredDTG, @ToIncurredDTG) >= 0";

                if (filterNegativeNews.Type != null)
                    sql += " and a.Type = @Type";

                if (filterNegativeNews.Status != null)
                    sql += " and a.Status = @Status";

                if (filterNegativeNews.PressAgencyID != null)
                    sql += " and a.PressAgencyID = @PressAgencyID";

                if (!string.IsNullOrWhiteSpace(filterNegativeNews.SearchText))
                    sql += " and (a.OtherChannelName like @SearchText " +
                            "or a.Content like @SearchText " +
                            "or a.Judged like @SearchText " +
                            "or a.MethodHandle like @SearchText " +
                            "or a.Result like @SearchText " +
                            "or a.Url like @SearchText " +
                            "or a.ReporterInformation like @SearchText " +
                            "or a.PressAgencyReview like @SearchText " +
                            "or a.Question like @SearchText " +
                            "or a.QuestionDetail like @SearchText " +
                            "or a.Resolution like @SearchText " +
                            "or a.ResolutionContent like @SearchText " +
                            "or a.Note like @SearchText " +
                            "or a.Name like @SearchText " +
                            "or a.Place like @SearchText " +
                            "or a.Title like @SearchText) ";
            }

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Type", filterNegativeNews.Type);
            cmd.Parameters.AddWithValue("@Status", filterNegativeNews.Status);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NegativeNews", SMX.News.Type.NegativeNews);
            cmd.Parameters.AddWithValue("@ToIncurredDTG", filterNegativeNews.ToIncurredDTG);
            cmd.Parameters.AddWithValue("@PressAgencyID", filterNegativeNews.PressAgencyID);
            cmd.Parameters.AddWithValue("@FromIncurredDTG", filterNegativeNews.FromIncurredDTG);
            cmd.Parameters.AddWithValue("@SearchText", BuildLikeFilter(filterNegativeNews.SearchText));

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
            }
        }

        public List<SharedComponent.Entities.News> GetAllNegativeNews()
        {
            string sql = @"select * from News a
                                where a.Deleted = @IsNotDeleted and a.Type = @Type";

            sql += @" order by a.CreatedDTG desc";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@Type", SMX.News.Type.NegativeNews);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd);
            }
        }

        public SharedComponent.Entities.News GetNews_ByID(int? ID)
        {
            string query = @"select * from News a
                                where a.Deleted=@NotDeleted and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", ID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<SharedComponent.Entities.News>(cmd).FirstOrDefault();
            }
        }

        #endregion

        #region NegativeNews

        public List<NegativeNews> GetListNegativeNew_ByID(int? ID)
        {
            string query = @"select * from  NegativeNews a
                               where a.Deleted=0 and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", ID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<NegativeNews>(cmd);
            }
        }

        public List<NegativeNews> GetListNegativeNew(int? newsID, PagingInfo pagingInfo)
        {
            string sql = @"select a.*,enPress.Name as PressAgencyName from  NegativeNews a
							   left join agency_PressAgency enPress on enPress.PressAgencyID = a.PressAgencyID
                               where a.Deleted=@IsNotDeleted and a.NewsID=@NewsID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IsNotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NewsID", newsID);

            using (DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<NegativeNews>(dataContext, cmd, " UpdatedDTG desc,CreatedDTG desc", pagingInfo);
            }
        }

        public NegativeNews GetNegativeNewsFullInfo_ByID(int? ID)
        {
            string query = @"select a.*, enPress.Name as PressAgencyName 
							   from NegativeNews a
							   left join agency_PressAgency enPress on enPress.PressAgencyID = a.PressAgencyID
							   where a.Deleted = @NotDeleted and a.NegativeNewsID = @NegativeNewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NegativeNewsID", ID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<NegativeNews>(cmd).FirstOrDefault();
            }
        }

        public void DeleteNegativeNewsByNegativeNewsID(int? itemID)
        {
            string sql = @"update NegativeNews
                                set Deleted = @Deleted
                                where NegativeNewsID = @NegativeNewsID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@NegativeNewsID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region NegativeNewsResearched

        public List<NegativeNewsResearched> GetListNegativeNewsResearched_ByNegativeNewsID(int? ID)
        {
            string query = @"select * from  NegativeNewsResearched a
                               where a.Deleted=@NotDeleted and a.NegativeNewsID=@NegativeNewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);
            cmd.Parameters.AddWithValue("@NegativeNewsID", ID);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<NegativeNewsResearched>(cmd);
            }
        }

        public void DeleteNegativeNewsResearched(int? itemID)
        {
            string sql = @"update NegativeNewsResearched
                                set Deleted = @Deleted
                                where NegativeNewsResearchedID = @NegativeNewsResearchedID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);
            cmd.Parameters.AddWithValue("@NegativeNewsResearchedID", itemID);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        #endregion

        #region NewsResearched

        public List<NewsResearched> GetListNewsResearched_ByNewsID(int? ID)
        {
            string query = @"select * from  NewsResearched a
                               where a.Deleted = @NotDeleted and a.NewsID = @NewsID";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@NewsID", ID);
            cmd.Parameters.AddWithValue("@NotDeleted", SMX.smx_IsNotDeleted);

            using (DataContext context = new DataContext())
            {
                return context.ExecuteSelect<NewsResearched>(cmd);
            }
        }

        public void DeleteNewsResearched(int? itemID)
        {
            string sql = @"update NewsResearched
                            set Deleted = @Deleted
                            where NewsResearchedID = @NewsResearchedID";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@NewsResearchedID", itemID);
            cmd.Parameters.AddWithValue("@Deleted", SMX.smx_IsDeleted);

            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }

        #endregion
    }
}