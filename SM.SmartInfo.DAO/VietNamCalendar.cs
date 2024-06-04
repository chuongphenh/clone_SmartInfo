using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.DAO
{
    public class VietNamCalendar
    {
        public static readonly double PI = Math.PI;
        public static int jdFromDate(int dd, int mm, int yy)
        {
            int a = (14 - mm) / 12;
            int y = yy + 4800 - a;
            int m = mm + 12 * a - 3;
            int jd = dd + (153 * m + 2) / 5 + 365 * y + y / 4 - y / 100 + y / 400 - 32045;
            if (jd < 2299161)
            {
                jd = dd + (153 * m + 2) / 5 + 365 * y + y / 4 - 32083;
            }
            return jd;
        }

        public static int[] jdToDate(int jd)
        {
            int a, b, c;
            if (jd > 2299160)
            {
                a = jd + 32044;
                b = (4 * a + 3) / 146097;
                c = a - (b * 146097) / 4;
            }
            else
            {
                b = 0;
                c = jd + 32082;
            }
            int d = (4 * c + 3) / 1461;
            int e = c - (1461 * d) / 4;
            int m = (5 * e + 2) / 153;
            int day = e - (153 * m + 2) / 5 + 1;
            int month = m + 3 - 12 * (m / 10);
            int year = b * 100 + d - 4800 + m / 10;
            return new int[] { day, month, year };
        }

        public static double SunLongitude(double jdn)
        {
            return SunLongitudeAA98(jdn);
        }

        public static double SunLongitudeAA98(double jdn)
        {
            double T = (jdn - 2451545.0) / 36525;
            double T2 = T * T;
            double dr = PI / 180;
            double M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2;
            double L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2;
            double DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
            DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
            double L = L0 + DL;
            L = L - 360 * (INT(L / 360));
            return L;
        }

        public static double NewMoon(int k)
        {
            return NewMoonAA98(k);
        }

        public static double NewMoonAA98(int k)
        {
            double T = k / 1236.85;
            double T2 = T * T;
            double T3 = T2 * T;
            double dr = PI / 180;
            double Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
            Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr);
            double M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3;
            double Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3;
            double F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3;
            double C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
            C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
            C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
            C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (M + Mpr));
            C1 = C1 - 0.0074 * Math.Sin(dr * (M - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + M));
            C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - M)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
            C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
            double deltat = (T < -11) ? (0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3) : (-0.000278 + 0.000265 * T + 0.000262 * T2);
            double JdNew = Jd1 + C1 - deltat;
            return JdNew;
        }

        public static int INT(double d)
        {
            return (int)Math.Floor(d);
        }

        public static double getSunLongitude(int dayNumber, double timeZone)
        {
            return SunLongitude(dayNumber - 0.5 - timeZone / 24);
        }

        public static int getNewMoonDay(int k, double timeZone)
        {
            double jd = NewMoon(k);
            return INT(jd + 0.5 + timeZone / 24);
        }

        public static int getLunarMonth11(int yy, double timeZone)
        {
            double off = jdFromDate(31, 12, yy) - 2415021.076998695;
            int k = INT(off / 29.530588853);
            int nm = getNewMoonDay(k, timeZone);
            int sunLong = INT(getSunLongitude(nm, timeZone) / 30);
            if (sunLong >= 9)
            {
                nm = getNewMoonDay(k - 1, timeZone);
            }
            return nm;
        }

        public static int getLeapMonthOffset(int a11, double timeZone)
        {
            int k = INT(0.5 + (a11 - 2415021.076998695) / 29.530588853);
            int last;
            int i = 1;
            int arc = INT(getSunLongitude(getNewMoonDay(k + i, timeZone), timeZone) / 30);
            do
            {
                last = arc;
                i++;
                arc = INT(getSunLongitude(getNewMoonDay(k + i, timeZone), timeZone) / 30);
            } while (arc != last && i < 14);
            return i - 1;
        }

        public static int[] convertSolar2Lunar(int dd, int mm, int yy, double timeZone)
        {
            int lunarDay, lunarMonth, lunarYear, lunarLeap;
            int dayNumber = jdFromDate(dd, mm, yy);
            int k = INT((dayNumber - 2415021.076998695) / 29.530588853);
            int monthStart = getNewMoonDay(k + 1, timeZone);
            if (monthStart > dayNumber)
            {
                monthStart = getNewMoonDay(k, timeZone);
            }
            int a11 = getLunarMonth11(yy, timeZone);
            int b11 = a11;
            if (a11 >= monthStart)
            {
                lunarYear = yy;
                a11 = getLunarMonth11(yy - 1, timeZone);
            }
            else
            {
                lunarYear = yy + 1;
                b11 = getLunarMonth11(yy + 1, timeZone);
            }
            lunarDay = dayNumber - monthStart + 1;
            int diff = INT((monthStart - a11) / 29);
            lunarLeap = 0;
            lunarMonth = diff + 11;
            if (b11 - a11 > 365)
            {
                int leapMonthDiff = getLeapMonthOffset(a11, timeZone);
                if (diff >= leapMonthDiff)
                {
                    lunarMonth = diff + 10;
                    if (diff == leapMonthDiff)
                    {
                        lunarLeap = 1;
                    }
                }
            }
            if (lunarMonth > 12)
            {
                lunarMonth = lunarMonth - 12;
            }
            if (lunarMonth >= 11 && diff < 4)
            {
                lunarYear -= 1;
            }
            return new int[] { lunarDay, lunarMonth, lunarYear, lunarLeap };
        }

        public static int[] convertLunar2Solar(int lunarDay, int lunarMonth, int lunarYear, int lunarLeap, double timeZone)
        {
            int a11, b11;
            if (lunarMonth < 11)
            {
                a11 = getLunarMonth11(lunarYear - 1, timeZone);
                b11 = getLunarMonth11(lunarYear, timeZone);
            }
            else
            {
                a11 = getLunarMonth11(lunarYear, timeZone);
                b11 = getLunarMonth11(lunarYear + 1, timeZone);
            }
            int k = INT(0.5 + (a11 - 2415021.076998695) / 29.530588853);
            int off = lunarMonth - 11;
            if (off < 0)
            {
                off += 12;
            }
            if (b11 - a11 > 365)
            {
                int leapOff = getLeapMonthOffset(a11, timeZone);
                int leapMonth = leapOff - 2;
                if (leapMonth < 0)
                {
                    leapMonth += 12;
                }
                if (lunarLeap != 0 && lunarMonth != leapMonth)
                {
                    Console.WriteLine("Invalid input!");
                    return new int[] { 0, 0, 0 };
                }
                else if (lunarLeap != 0 || off >= leapOff)
                {
                    off += 1;
                }
            }
            int monthStart = getNewMoonDay(k + off, timeZone);
            return jdToDate(monthStart + lunarDay - 1);
        }

        //public static void Main(string[] args)
        //{
        //    double TZ = 7.0;
        //    int start = jdFromDate(20, 04, 2023);
        //    int step = 15;
        //    int count = -1;
        //    while (count++ < 240)
        //    {
        //        int jd = start + step * count;
        //        int[] s = jdToDate(jd);
        //        int[] l = convertSolar2Lunar(s[0], s[1], s[2], TZ);
        //        int[] s2 = convertLunar2Solar(l[0], l[1], l[2], l[3], TZ);
        //        if (s[0] == s2[0] && s[1] == s2[1] && s[2] == s2[2])
        //        {
        //            Console.WriteLine("OK! " + s[0] + "/" + s[1] + "/" + s[2] + " -> " + l[0] + "/" + l[1] + "/" + l[2] + (l[3] == 0 ? "" : " leap"));
        //        }
        //        else
        //        {
        //            Console.WriteLine("ERROR! " + s[0] + "/" + s[1] + "/" + s[2] + " -> " + l[0] + "/" + l[1] + "/" + l[2] + (l[3] == 0 ? "" : " leap"));
        //        }
        //    }
        //}

        //public static void Main(string[] args)
        //{
        //    double TZ = 7.0;
        //    int start = jdFromDate(1, 1, 2024);
        //    int step = 15;
        //    int count = -1;
        //    while (count++ < 240)
        //    {
        //        // Chuyển đổi từ ngày dương sang ngày âm
        //        int jd = start + step * count;
        //        int[] s = jdToDate(jd);
        //        int[] l = convertSolar2Lunar(s[0], s[1], s[2], TZ);

        //        // Hiển thị kết quả chuyển đổi từ ngày dương sang ngày âm
        //        Console.WriteLine("Dương sang Âm:");
        //        Console.WriteLine("Input: " + s[0] + "/" + s[1] + "/" + s[2]);
        //        Console.WriteLine("Output: " + l[0] + "/" + l[1] + "/" + l[2] + (l[3] == 0 ? "" : " nhảy"));

        //        // Chuyển đổi từ ngày âm sang ngày dương
        //        int[] s2 = convertLunar2Solar(l[0], l[1], l[2], l[3], TZ);

        //        // Hiển thị kết quả chuyển đổi từ ngày âm sang ngày dương
        //        Console.WriteLine("Âm sang Dương:");
        //        Console.WriteLine("Input: " + l[0] + "/" + l[1] + "/" + l[2] + (l[3] == 0 ? "" : " nhảy"));
        //        Console.WriteLine("Output: " + s2[0] + "/" + s2[1] + "/" + s2[2]);
        //    }
        //}

    }
}
