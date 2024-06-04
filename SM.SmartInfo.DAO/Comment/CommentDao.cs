using System.Data.SqlClient;
using SM.SmartInfo.DAO.Common;
using System.Collections.Generic;

namespace SM.SmartInfo.DAO.Comment
{
    public class CommentDao : BaseDao
    {
        public List<SharedComponent.Entities.Comment> GetAllCommentByRefIDAndRefType(int? refID, int? refType, int? typeNoti)
        {
            //string cmdText = @"select
            //                        *
            //                    from Comment cmt
            //                    where cmt.CommentID > 0 and cmt.RefID = @RefID and cmt.RefType = @RefType";
            string cmdText = @"select
                                    *
                                from Comment 
                                where CommentID > 0 and RefID = @RefID ";
            switch (refType)
            {
                case 10:
                    cmdText += @" AND (RefType BETWEEN 10 AND 19) ";
                    break;
                case 20:
                    cmdText += @" AND (RefType BETWEEN 20 AND 29) ";
                    break;
                default:
                    cmdText += $@" AND (RefType = @refType) ";
                    break;
            }
            cmdText += @" order by CommentedDTG desc";

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Parameters.AddWithValue("@RefID", refID);
            cmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                var lst = dataContext.ExecuteSelect<SharedComponent.Entities.Comment>(cmd);
                foreach(var item in lst)
                {
                    item.TypeNoti = typeNoti;
                }
                return lst;
            }
        }

        public bool CheckDeleteComment(int? cmtID, int? version)
        {
            string sql = @"select count(1) from Comment
                            where CommentID = @CommentID AND [Version] = @Version";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@CommentID", cmtID);
            cmd.Parameters.AddWithValue("@Version", version);
            using (DataContext context = new DataContext())
            {
                var result = (int?)context.ExecuteScalar(cmd);
                return result > 0;
            }
        }

        public void DeleteCommentByID(int? cmtID, int? version, int? commentedByID, int? rate)
        {
            string sql = @"delete Comment
 where CommentID = @CommentID 
	AND [Version] = @Version
	AND CommentedByID = @CommentedByID
	AND Rate = @Rate";

            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@CommentID", cmtID);
            cmd.Parameters.AddWithValue("@Version", version);
            cmd.Parameters.AddWithValue("@CommentedByID", commentedByID);
            cmd.Parameters.AddWithValue("@Rate", rate);
            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(cmd);
            }
        }
    }
}