using System.Data;

namespace SM.SmartInfo.DAO.CommonList
{
    public class EmailDao : BaseDao
    {
        public DataTable GetEmailData_ThongBaoSinhNhat(int? notificationID, int? PressAgencyHRID, string content)
        {
            return ExecuteStoreGetDataTable("api_GetEmailDataThongBaoSinhNhat", notificationID, PressAgencyHRID, content);
        }

        public DataTable GetEmailData_ThongBaoNgayKyNiem(int? notificationID, string content)
        {
            return ExecuteStoreGetDataTable("api_GetEmailDataThongBaoNgayKyNiem", notificationID, content);
        }

        public DataTable GetEmailData_ThongBaoNgayThanhLap(int? notificationID, string content)
        {
            return ExecuteStoreGetDataTable("api_GetEmailDataThongBaoNgayThanhLap", notificationID, content);
        }

        public DataTable GetEmailData_ThongBaoNgayTruyenThong(int? notificationID, string content)
        {
            return ExecuteStoreGetDataTable("api_GetEmailDataThongBaoNgayTruyenThong", notificationID, content);
        }
    }
}