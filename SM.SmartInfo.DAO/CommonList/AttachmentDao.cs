using System.Linq;
using System.Data.SqlClient;
using SM.SmartInfo.DAO.Common;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SoftMart.Kernel.Entity;
using System;

namespace SM.SmartInfo.DAO.CommonList
{
    public class AttachmentDao : BaseDao
    {
        public void InsertAttachment(adm_Attachment item)
        {
            using (DataContext dataContext = new DataContext())
            {
                dataContext.InsertItem<adm_Attachment>(item);
            }
        }

        public adm_Attachment GetAttachmentByID(int id)
        {
            string query = @"select * from adm_Attachment where AttachmentID = @AttachmentID";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@AttachmentID", id);

            using (DataContext dataContext = new DataContext())
            {
                adm_Attachment res = dataContext.ExecuteSelect<adm_Attachment>(cmd).FirstOrDefault();
                return res;
            }
        }
        public List<adm_Attachment> GetAttachmentByRefIDAndRefTypeJoinCacheECM(int? refID, int? refType)
        {
            string query = @"select att.FileName, att.AttachmentID, att.RefID, att.RefType, att.FileSize, att.ContentType, ecm.CacheECMID, ecm.FileContent from adm_Attachment att join adm_CacheECM ecm on att.AttachmentID = ecm.AttachmentID where att.RefID = @RefID AND att.RefType = @RefType";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandTimeout = 100;
            cmd.Parameters.AddWithValue("@RefID", refID);
            cmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(cmd);
            }
        }

        public List<adm_Attachment> GetListAttachmentByRefIDAndRefType(int? refID, int? refType)
        {
            string query = @"select * from adm_Attachment where RefID = @RefID AND RefType = @RefType";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@RefID", refID);
            cmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(cmd);
            }
        }

        public adm_Attachment GetAttachmentByIDForImageLibrary(int? id)
        {
            string query = @"select * from adm_Attachment where AttachmentID = @AttachmentID";
            /*string query = @"SELECT 
	                                a.*
                                    , sysDoc.Name as DocumentName
	                                , emp.Name as FullNameCreateBy
                                FROM adm_Attachment a
	                                left join adm_SystemParameter sysDoc on sysDoc.Code = a.RefCode
	                                left join adm_Employee emp on emp.Username = a.CreatedBy and emp.Deleted = 0
                                WHERE AttachmentID = @AttachmentID";*/
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@AttachmentID", id);

            using (DataContext dataContext = new DataContext())
            {
                adm_Attachment res = dataContext.ExecuteSelect<adm_Attachment>(cmd).FirstOrDefault();
                return res;
            }
        }

        public List<adm_Attachment> GetAttachmentsByDefault(int? refID, int? refType, PagingInfo paging = null)
        {
            string cmdText = @"SELECT * FROM [dbo].[adm_Attachment] WHERE RefID = @refID AND RefType = @refType";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@refID", refID);
            sqlCmd.Parameters.AddWithValue("@refType", refType);

            if (paging == null)
            {
                using (DataContext dataContext = new DataContext())
                {
                    return dataContext.ExecuteSelect<adm_Attachment>(sqlCmd);
                }
            }
            else
            {
                using (DataContext dataContext = new DataContext())
                {
                    return ExecutePaging<adm_Attachment>(dataContext, sqlCmd, " AttachmentID desc ", paging);
                }
            }
        }

        public List<adm_Attachment> GetAttachments(int? refID, int? refType)
        {
            string cmdText = @"SELECT 
	                                a.*
                                    , sysDoc.Name as DocumentName
	                                , emp.Name as FullNameCreateBy
                                FROM adm_Attachment a
	                                left join adm_SystemParameter sysDoc on sysDoc.Code = a.RefCode
	                                left join adm_Employee emp on emp.Username = a.CreatedBy and emp.Deleted = 0
                                WHERE a.AttachmentID > 0 AND a.RefID = @RefID AND a.RefType = @RefType";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@RefID", refID);
            sqlCmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(sqlCmd);
            }
        }
        public List<adm_Attachment> GetAttachmentIDByHrAlertID(int? refID, int? refType)
        {
            string cmdText = @"SELECT 
	                                a.*
                                    , sysDoc.Name as DocumentName
	                                , emp.Name as FullNameCreateBy
                                FROM adm_Attachment a
	                                left join adm_SystemParameter sysDoc on sysDoc.Code = a.RefCode
	                                left join adm_Employee emp on emp.Username = a.CreatedBy and emp.Deleted = 0
                                    left join agency_PressAgencyHRAlert hra on a.RefID = hra.PressAgencyHRID
                                WHERE a.AttachmentID > 0 AND hra.PressAgencyHRAlertID = @RefID AND a.RefType = @RefType";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@RefID", refID);
            sqlCmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(sqlCmd);
            }
        }
        public List<adm_Attachment> GetAttachmentIDByHrAlertID(int? refID, int? refType, int? NotificationID, string fullNameHR, string positonHR, string namePressAgency, DateTime? DOB)
        {
            string cmdText = @"SELECT 
	                                a.*
                                    , sysDoc.Name as DocumentName
	                                , emp.Name as FullNameCreateBy
                                FROM adm_Attachment a
	                                left join adm_SystemParameter sysDoc on sysDoc.Code = a.RefCode
	                                left join adm_Employee emp on emp.Username = a.CreatedBy and emp.Deleted = 0
                                    left join agency_PressAgencyHRAlert hra on a.RefID = hra.PressAgencyHRID
                                WHERE a.AttachmentID > 0 AND hra.PressAgencyHRAlertID = @RefID AND a.RefType = @RefType";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@RefID", refID);
            sqlCmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(sqlCmd);
            }
        }
        public List<adm_Attachment> GetAttachmentsImageContact(int? refID, int? refType, int? refTypeOrther, int? refTypeHistory, int PageIndex, int? HistoryRefType = -1)
        {
            PagingInfo paging = new PagingInfo(PageIndex, SMX.smx_PageSize);
            //string cmdText = @"SELECT * FROM [dbo].[adm_Attachment] WHERE RefID = @RefID AND (RefType = @RefType OR RefType = @HistoryRefType)";

            string cmdText = @"SELECT * FROM [dbo].[adm_Attachment]
                                WHERE (RefID = @RefID AND (RefType = @RefType OR RefType = @RefTypeOrther OR RefType = @RefTypeHistory))
                                OR (RefID IN (SELECT PressAgencyHRHistoryID FROM [dbo].[agency_PressAgencyHRHistory] WHERE PressAgencyHRID = @RefID) AND RefType = @HistoryRefType)";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@RefID", refID);
            sqlCmd.Parameters.AddWithValue("@RefType", refType);
            sqlCmd.Parameters.AddWithValue("@RefTypeOrther", refTypeOrther);
            sqlCmd.Parameters.AddWithValue("@RefTypeHistory", refTypeHistory);
            sqlCmd.Parameters.AddWithValue("@HistoryRefType", HistoryRefType);

            using (DataContext dataContext = new DataContext())
            {
                return base.ExecutePaging<adm_Attachment>(dataContext, sqlCmd, " CreatedDTG desc", paging);
            }
        }
        public List<adm_Attachment> GetAttachmentsByListRefID(List<int> lstRefID)
        {
            string cmdText = @"SELECT 
	                                a.*
                                    , sysDoc.Name as DocumentName
	                                , emp.Name as FullNameCreateBy
                                FROM adm_Attachment a
	                                left join adm_SystemParameter sysDoc on sysDoc.Code = a.RefCode
	                                left join adm_Employee emp on emp.Username = a.CreatedBy and emp.Deleted = 0
                                WHERE a.AttachmentID > 0 AND a.RefID in ({0})";

            cmdText = string.Format(cmdText, BuildInCondition(lstRefID));

            SqlCommand sqlCmd = new SqlCommand(cmdText);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(sqlCmd);
            }
        }

        public List<adm_Attachment> GetAttachmentsByRefType(int? refType)
        {
            string cmdText = @"SELECT 
	                                a.*
                                    , sysDoc.Name as DocumentName
	                                , emp.Name as FullNameCreateBy
                                FROM adm_Attachment a
	                                left join adm_SystemParameter sysDoc on sysDoc.Code = a.RefCode
	                                left join adm_Employee emp on emp.Username = a.CreatedBy and emp.Deleted = 0
                                WHERE a.AttachmentID > 0 AND a.RefType = @RefType";

            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.Parameters.AddWithValue("@RefType", refType);

            using (DataContext dataContext = new DataContext())
            {
                return dataContext.ExecuteSelect<adm_Attachment>(sqlCmd);
            }
        }

        public void DeleteAttachment(int? id)
        {
            int affectedRows = 0;

            using (DataContext dataContext = new DataContext())
            {
                affectedRows = dataContext.DeleteItem<adm_Attachment>(id);
            }
            if (affectedRows == 0)
            {
                throw new SMXException(string.Format(Messages.ItemNotExitOrChanged, "adm_Attachment"));
            }
        }

        public void DeleteCacheECM_ByAttachmentID(int? id)
        {
            string query = @"Delete adm_CacheECM Where AttachmentID = @AttachmentID";
            SqlCommand sqlCmd = new SqlCommand(query);
            sqlCmd.Parameters.AddWithValue("@AttachmentID", id);
            using (DataContext context = new DataContext())
            {
                context.ExecuteNonQuery(sqlCmd);
            }
        }
        public adm_CacheECM GetCacheECM_ByAttachmentID(int? id)
        {
            string query = @"select * from adm_CacheECM where AttachmentID = @AttachmentID";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@AttachmentID", id);

            using (DataContext dataContext = new DataContext())
            {
                adm_CacheECM res = dataContext.ExecuteSelect<adm_CacheECM>(cmd).FirstOrDefault();
                return res;
            }
        }

        public adm_CacheECM GetCacheECMByAttachmentID(int? attachmentID)
        {
            using (DataContext context = new DataContext())
            {
                return context.SelectItemByColumnName<adm_CacheECM>(adm_CacheECM.C_AttachmentID, attachmentID).LastOrDefault();
            }
        }

        public void InsertCacheECM(adm_CacheECM item)
        {
            using (DataContext context = new DataContext())
            {
                context.InsertItem<adm_CacheECM>(item);
            }
        }
    }
}