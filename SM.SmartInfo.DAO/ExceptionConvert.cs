using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO
{
    public static class ExceptionConvert
    {
        public static DateTime? TryConvertToGregorianCalendar(int lunarYear, int lunarMonth, int lunarDay)
        {
            try
            {
                ChineseLunisolarCalendar lunarCalendar = new ChineseLunisolarCalendar();
                return lunarCalendar.ToDateTime(lunarYear, lunarMonth, lunarDay, 0, 0, 0, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static int[] TryConvertLunar2Solar(int lunarYear, int lunarMonth, int lunarDay)
        {
            try
            {
                int year = DateTime.Now.Year;
                bool isLeapYear = DateTime.IsLeapYear(year);
                int isLeapYearNumeric = isLeapYear ? 1 : 0;

                int[] solarDate = VietNamCalendar.convertLunar2Solar(lunarDay, lunarMonth, lunarYear, isLeapYearNumeric, 7.0);
                return solarDate;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DateTime? ConvertDateTimeCurrentYear(DateTime? datetime)
        {
            try
            {
                if (datetime.HasValue)
                {
                    int year = DateTime.Now.Year;
                    return new DateTime(year, datetime.Value.Month, datetime.Value.Day);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DateTime? ConvertDateTimeToCurrentYear(DateTime? dateTime, bool isSameYear, int fromYear, int yearTo)
        {
            if (!dateTime.HasValue)
                return null;

            int currentYear = DateTime.Now.Year;
            int month = dateTime.Value.Month;
            int day = dateTime.Value.Day;

            if (isSameYear)
            {
                return new DateTime(currentYear, month, day);
            }
            else if (month == 12)
            {
                return new DateTime(fromYear, month, day);
            }
            else
            {
                return new DateTime(yearTo, month, day);
            }
        }
    }

}
