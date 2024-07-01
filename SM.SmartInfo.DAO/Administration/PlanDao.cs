using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.SmartInfo.DAO.Common;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using System.Data.SqlClient;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.DAO.Administration
{
    public class PlanDao : BaseDao
    {
        private string connectionString = ConfigUtils.ConnectionString;
        #region Modification methods
        public int InsertDocument(Document item)
        {
            string query = @"
        INSERT INTO [Document] 
        (DocumentCode, DocumentName, IssuerOrganizationID, StartDate, EndDate, Version, Deleted, CreatedBy, CreatedDTG, UpdatedBy, UpdatedDTG, ReleaseDate) 
        VALUES 
        (@DocumentCode, @DocumentName, @IssuerOrganizationID, @StartDate, @EndDate, @Version, @Deleted, @CreatedBy, @CreatedDTG, @UpdatedBy, @UpdatedDTG, @ReleaseDate); SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DocumentCode", item.DocumentCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DocumentName", item.DocumentName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IssuerOrganizationID", item.IssuerOrganizationID.HasValue ? (object)item.IssuerOrganizationID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartDate", item.StartDate.HasValue ? (object)item.StartDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", item.EndDate.HasValue ? (object)item.EndDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Version", item.Version ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Deleted", item.Deleted ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", item.CreatedBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedDTG", item.CreatedDTG.HasValue ? (object)item.CreatedDTG.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", item.UpdatedBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedDTG", item.UpdatedDTG.HasValue ? (object)item.UpdatedDTG.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReleaseDate", item.ReleaseDate.HasValue ? (object)item.ReleaseDate.Value : DBNull.Value);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result); // Convert the result to int and return it.
                    }
                    else
                    {
                        throw new Exception("Failed to insert Document or retrieve DocumentID.");
                    }
                }
            }
        }

        public void UpdatePlan(Document item)
        {
            string query = @"
                            UPDATE [Plan] 
                            SET 
                                PlanCode = COALESCE(@PlanCode, PlanCode), 
                                Name = COALESCE(@Name, Name), 
                                OrganizationName = COALESCE(@OrganizationName, OrganizationName), 
                                StartDate = COALESCE(@StartDate, StartDate), 
                                EndDate = COALESCE(@EndDate, EndDate), 
                                ReportCycle = COALESCE(@ReportCycle, ReportCycle), 
                                ReportCycleType = COALESCE(@ReportCycleType, ReportCycleType), 
                                Description = COALESCE(@Description, Description), 
                                Version = COALESCE(@Version, Version), 
                                Deleted = COALESCE(@Deleted, Deleted), 
                                CreatedBy = COALESCE(@CreatedBy, CreatedBy), 
                                CreatedDTG = COALESCE(@CreatedDTG, CreatedDTG), 
                                UpdatedBy = COALESCE(@UpdatedBy, UpdatedBy), 
                                UpdatedDTG = COALESCE(@UpdatedDTG, UpdatedDTG)
                            WHERE PlanID = @PlanID;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", item.PlanID);
                    cmd.Parameters.AddWithValue("@PlanCode", (object)item.PlanCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Name", (object)item.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OrganizationName", (object)item.OrganizationName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartDate", (object)item.StartDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", (object)item.EndDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReportCycle", (object)item.ReportCycle ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReportCycleType", (object)item.ReportCycleType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)item.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Version", (object)item.Version ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Deleted", (object)item.Deleted ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", (object)item.CreatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedDTG", (object)item.CreatedDTG ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", (object)item.UpdatedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedDTG", (object)item.UpdatedDTG ?? DBNull.Value);

                    conn.Open();
                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows == 0)
                    {
                        throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "Plan"));
                    }
                }
            }
        }

        #endregion

        #region Getting methods

        public Document GetPlanByID(int? id)
        {
            using (DataContext dataContext = new DataContext())
            {
                return dataContext.SelectItemByID<Document>(id);
            }
        }
        public Document GetPlanInfoByID(int planID)
        {
            string sql = @"SELECT *
                        FROM [Plan] WHERE Deleted = @isDeleted AND PlanID = @planID";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@planID", planID);
            command.Parameters.AddWithValue("@isDeleted", SMX.smx_IsNotDeleted);

            using (DataContext dataContext = new DataContext())
            {
                var item = dataContext.ExecuteSelect<Document>(command).FirstOrDefault();
                return item;
            }
        }
        public List<TargetInfo> GetTargetInPlanByPlanID(int planID)
        {
            string sql = @"SELECT 
                            Target.* 
                        FROM 
                            [Plan]
                        INNER JOIN 
                            PlanTarget ON [Plan].PlanID = PlanTarget.PlanID AND [Plan].Deleted = 0
                        INNER JOIN 
                            Target ON PlanTarget.TargetID = Target.TargetID AND Target.Deleted = 0
                        WHERE 
                            [Plan].PlanID = @planID;";

            SqlCommand command = new SqlCommand(sql);
            command.Parameters.AddWithValue("@planID", planID);
            command.Parameters.AddWithValue("@isDeleted", SMX.smx_IsNotDeleted);

            using (DataContext dataContext = new DataContext())
            {
                var res = dataContext.ExecuteSelect<TargetInfo>(command);
                return res;
            }
        }
        #endregion

        #region Method Action
        public bool IsPlanCodeDuplicate(int? planID, string planCode)
        {
            const string cmdText = @"SELECT COUNT(*)
                             FROM [Plan]
                             WHERE Deleted = 0 AND PlanCode = @planCode
                             AND (@PlanID IS NULL OR PlanID <> @PlanID)";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@PlanID", planID);
            sqlCmd.Parameters.AddWithValue("@planCode", planCode);

            using (var dataContext = new DataContext())
            {
                int count = dataContext.ExecuteSelect<int>(sqlCmd).FirstOrDefault();
                return count > 0;
            }
        }
        public void InsertPlanTarget(List<PlanTarget> lstItem)
        {
            using (var dataContext = new DataContext())
            {
                foreach (PlanTarget item in lstItem)
                {
                    dataContext.InsertItem<PlanTarget>(item);
                }
            }
        }
        public void DeletePlanTarget(int planID)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.DeleteItemByColumn<PlanTarget>(PlanTarget.C_PlanID, planID);
            }
        }
        #endregion
    }
}
