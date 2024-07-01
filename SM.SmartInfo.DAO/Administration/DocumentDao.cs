//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SM.SmartInfo.DAO.Common;
//using SM.SmartInfo.SharedComponent.Entities;
//using SM.SmartInfo.SharedComponent.Constants;
//using System.Data.SqlClient;
//using SM.SmartInfo.SharedComponent.Params.Administration;
//using SoftMart.Kernel.Exceptions;
//using SM.SmartInfo.Utils;
//using SM.SmartInfo.SharedComponent.EntityInfos;

//namespace SM.SmartInfo.DAO.Administration
//{
//    public class DocumentDao : BaseDao
//    {
//        private string connectionString = ConfigUtils.ConnectionString;
//        #region Modification methods
//        public int InsertDocument(Document item)
//        {
//            string query = @"
//                INSERT INTO [Document] 
//                (DocumentCode, DocumentName, IssuerOrganizationID, StartDate, EndDate, Version, Deleted, CreatedBy, CreatedDTG, UpdatedBy, UpdatedDTG, ReleaseDate) 
//                VALUES 
//                (@DocumentCode, @DocumentName, @IssuerOrganizationID, @StartDate, @EndDate, @Version, @Deleted, @CreatedBy, @CreatedDTG, @UpdatedBy, @UpdatedDTG, @ReleaseDate); 
//                SELECT SCOPE_IDENTITY();";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@DocumentCode", item.DocumentCode ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@DocumentName", item.DocumentName ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@IssuerOrganizationID", item.IssuerOrganizationID.HasValue ? (object)item.IssuerOrganizationID.Value : DBNull.Value);
//                    cmd.Parameters.AddWithValue("@StartDate", item.StartDate.HasValue ? (object)item.StartDate.Value : DBNull.Value);
//                    cmd.Parameters.AddWithValue("@EndDate", item.EndDate.HasValue ? (object)item.EndDate.Value : DBNull.Value);
//                    cmd.Parameters.AddWithValue("@Version", item.Version ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@Deleted", item.Deleted ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG.HasValue ? (object)item.CreatedDTG.Value : DBNull.Value);
//                    cmd.Parameters.AddWithValue("@UpdatedBy", item.UpdatedBy ?? (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@UpdatedDTG", item.UpdatedDTG.HasValue ? (object)item.UpdatedDTG.Value : DBNull.Value);
//                    cmd.Parameters.AddWithValue("@ReleaseDate", item.ReleaseDate.HasValue ? (object)item.ReleaseDate.Value : DBNull.Value);

//                    conn.Open();
//                    object result = cmd.ExecuteScalar();
//                    if (result != null)
//                    {
//                        return Convert.ToInt32(result); 
//                    }
//                    else
//                    {
//                        throw new Exception("Failed to insert Document or retrieve DocumentID.");
//                    }
//                }
//            }
//        }
//        public void UpdateDocument(Document item)
//        {
//            string query = @"
//                    UPDATE [Document] 
//                    SET 
//                        DocumentCode = COALESCE(@DocumentCode, DocumentCode), 
//                        DocumentName = COALESCE(@DocumentName, DocumentName), 
//                        IssuerOrganizationID = COALESCE(@IssuerOrganizationID, IssuerOrganizationID), 
//                        StartDate = COALESCE(@StartDate, StartDate), 
//                        EndDate = COALESCE(@EndDate, EndDate), 
//                        Version = COALESCE(@Version, Version), 
//                        Deleted = COALESCE(@Deleted, Deleted), 
//                        CreatedBy = COALESCE(@CreatedBy, CreatedBy), 
//                        CreatedDTG = COALESCE(@CreatedDTG, CreatedDTG), 
//                        UpdatedBy = COALESCE(@UpdatedBy, UpdatedBy), 
//                        UpdatedDTG = COALESCE(@UpdatedDTG, UpdatedDTG), 
//                        ReleaseDate = COALESCE(@ReleaseDate, ReleaseDate)
//                    WHERE DocumentID = @DocumentID;";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@DocumentID", item.DocumentID);
//                    cmd.Parameters.AddWithValue("@DocumentCode", (object)item.DocumentCode ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@DocumentName", (object)item.DocumentName ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@IssuerOrganizationID", (object)item.IssuerOrganizationID ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@StartDate", (object)item.StartDate ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@EndDate", (object)item.EndDate ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@Version", (object)item.Version ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@Deleted", (object)item.Deleted ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@CreatedBy", (object)item.CreatedBy ?? DBNull.Value);
//                    cmd.Parameters.AddWithValue("@CreatedDTG", (object)item.CreatedDTG ?? DBNull.Value);
//                }
//            }
//        }
//        #endregion

//        #region Getting methods

//        public Document GetDocumentByID(int? id)
//        {
//            using (DataContext dataContext = new DataContext())
//            {
//                return dataContext.SelectItemByID<Document>(id);
//            }
//        }
//        public Document GetDocumentInfoByID(int DocumentID)
//        {
//            string sql = @"SELECT *
//                        FROM [Document] WHERE Deleted = @isDeleted AND DocumentID = @DocumentID";

//            SqlCommand command = new SqlCommand(sql);
//            command.Parameters.AddWithValue("@DocumentID", DocumentID);
//            command.Parameters.AddWithValue("@isDeleted", SMX.smx_IsNotDeleted);

//            using (DataContext dataContext = new DataContext())
//            {
//                var item = dataContext.ExecuteSelect<Document>(command).FirstOrDefault();
//                return item;
//            }
//        }
//        public List<TargetInfo> GetTargetInDocumentByDocumentID(int DocumentID)
//        {
//            string sql = @"SELECT 
//                            Target.* 
//                        FROM 
//                            [Document]
//                        INNER JOIN 
//                            DocumentTarget ON [Document].DocumentID = DocumentTarget.DocumentID AND [Document].Deleted = 0
//                        INNER JOIN 
//                            Target ON DocumentTarget.TargetID = Target.TargetID AND Target.Deleted = 0
//                        WHERE 
//                            [Document].DocumentID = @DocumentID;";

//            SqlCommand command = new SqlCommand(sql);
//            command.Parameters.AddWithValue("@DocumentID", DocumentID);
//            command.Parameters.AddWithValue("@isDeleted", SMX.smx_IsNotDeleted);

//            using (DataContext dataContext = new DataContext())
//            {
//                var res = dataContext.ExecuteSelect<TargetInfo>(command);
//                return res;
//            }
//        }
//        #endregion

//        #region Method Action
//        public bool IsDocumentCodeDuplicate(int? DocumentID, string DocumentCode)
//        {
//            const string cmdText = @"SELECT COUNT(*)
//                             FROM [Document]
//                             WHERE Deleted = 0 AND DocumentCode = @DocumentCode
//                             AND (@DocumentID IS NULL OR DocumentID <> @DocumentID)";

//            SqlCommand sqlCmd = new SqlCommand(cmdText);
//            sqlCmd.Parameters.AddWithValue("@DocumentID", DocumentID);
//            sqlCmd.Parameters.AddWithValue("@DocumentCode", DocumentCode);

//            using (var dataContext = new DataContext())
//            {
//                int count = dataContext.ExecuteSelect<int>(sqlCmd).FirstOrDefault();
//                return count > 0;
//            }
//        }
//        public void InsertDocumentTarget(List<DocumentTarget> lstItem)
//        {
//            using (var dataContext = new DataContext())
//            {
//                foreach (DocumentTarget item in lstItem)
//                {
//                    dataContext.InsertItem<DocumentTarget>(item);
//                }
//            }
//        }
//        public void DeleteDocumentTarget(int DocumentID)
//        {
//            using (var dataContext = new DataContext())
//            {
//                dataContext.DeleteItemByColumn<DocumentTarget>(DocumentTarget.C_DocumentID, DocumentID);
//            }
//        }
//        #endregion
//    }
//}
