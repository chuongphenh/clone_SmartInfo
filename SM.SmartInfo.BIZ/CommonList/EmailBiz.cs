using System;
using System.Data;
using System.Collections.Generic;
using SM.SmartInfo.DAO.CommonList;
//using SM.SmartInfo.Service.Notification;
//using SM.SmartInfo.Service.Notification.BIZ;

namespace SM.SmartInfo.BIZ
{
    class EmailBiz
    {
        private const string ColumnEmail = "Email";

        private const string ColumnRefID = "RefID";

        private static EmailDao _dao = new EmailDao();

        //public static void SendEmailSinhNhat(int? notificationID, int? PressAgencyHRID, string content)
        //{
        //    DataTable data = _dao.GetEmailData_ThongBaoSinhNhat(notificationID, PressAgencyHRID, content);
        //    string emailTemplate = TemplateCode.Email_ThongBaoSinhNhat;

        //    SendSingleEmail(emailTemplate, data);
        //}

        //private static void SendSingleEmail(string emailTemplate, DataTable data)
        //{
        //    try
        //    {
        //        if (data == null && data.Rows.Count == 0)
        //        {
        //            Utils.LogManager.WebLogger.LogDebug(string.Format("Khong co du lieu cho email {0}", emailTemplate));
        //            return;
        //        }

        //        List<string> lstEmail = new List<string>();
        //        string refID = string.Empty;
        //        foreach (DataRow row in data.Rows)
        //        {
        //            string[] arrEmail = row[ColumnEmail].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //            foreach (string email in arrEmail)
        //            {
        //                if (!string.IsNullOrWhiteSpace(email) &&
        //                    !lstEmail.Exists(c => string.Equals(c, email, StringComparison.OrdinalIgnoreCase)))
        //                    lstEmail.Add(email);
        //            }

        //            refID = row[ColumnRefID].ToString();
        //        }

        //        if (lstEmail.Count > 0)
        //        {
        //            NotifyBiz notifyBiz = new NotifyBiz();
        //            notifyBiz.RequestNotify(emailTemplate, data.Rows[0], lstEmail, refID);
        //        }
        //    }
        //    catch (Exception ex) { }
        //}
    }
}