using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SM.SmartInfo.Utils
{
    public class ConvertSolarLunar
    {

        /*private static int INT(double d)
        {
            return (int)Math.Floor(d);
        }

        private static int MOD(int x, int y)
        {
            int z = x - (int)(y * Math.Floor((double)x / y));
            if (z == 0)
            {
                z = y;
            }
            return z;
        }

        private static double UniversalToJD(int D, int M, int Y)
        {
            double JD;
            if (Y > 1582 || (Y == 1582 && M > 10) || (Y == 1582 && M == 10 && D > 14))
            {
                JD = 367 * Y - INT(7 * (Y + INT((M + 9) / 12)) / 4) - INT(3 * (INT((Y + (M - 9) / 7) / 100) + 1) / 4) + INT(275 * M / 9) + D + 1721028.5;
            }
            else
            {
                JD = 367 * Y - INT(7 * (Y + 5001 + INT((M - 9) / 7)) / 4) + INT(275 * M / 9) + D + 1729776.5;
            }
            return JD;
        }

        private static int[] UniversalFromJD(double JD)
        {
            int Z, A, alpha, B, C, D, E, dd, mm, yyyy;
            double F;
            Z = INT(JD + 0.5);
            F = (JD + 0.5) - Z;
            if (Z < 2299161)
            {
                A = Z;
            }
            else
            {
                alpha = INT((Z - 1867216.25) / 36524.25);
                A = Z + 1 + alpha - INT(alpha / 4);
            }
            B = A + 1524;
            C = INT((B - 122.1) / 365.25);
            D = INT(365.25 * C);
            E = INT((B - D) / 30.6001);
            dd = INT(B - D - INT(30.6001 * E) + F);
            if (E < 14)
            {
                mm = E - 1;
            }
            else
            {
                mm = E - 13;
            }
            if (mm < 3)
            {
                yyyy = C - 4715;
            }
            else
            {
                yyyy = C - 4716;
            }
            return new int[] { dd, mm, yyyy };
        }

        private static int getLocalTimeZone()
        {
            TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            return offset.Hours;
        }

        private static int[] LocalFromJD(double JD)
        {
            return UniversalFromJD(JD + getLocalTimeZone() / 24.0);
        }

        private static double LocalToJD(int D, int M, int Y)
        {
            return UniversalToJD(D, M, Y) - getLocalTimeZone() / 24.0;
        }

        private static double NewMoon(int k)
        {
            double T = k / 1236.85; // Time in Julian centuries from 1900 January 0.5
            double T2 = T * T;
            double T3 = T2 * T;
            double dr = Math.PI / 180;
            double Jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * T2 - 0.000000155 * T3;
            Jd1 = Jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * T2) * dr); // Mean new moon
            double M = 359.2242 + 29.10535608 * k - 0.0000333 * T2 - 0.00000347 * T3; // Sun's mean anomaly
            double Mpr = 306.0253 + 385.81691806 * k + 0.0107306 * T2 + 0.00001236 * T3; // Moon's mean anomaly
            double F = 21.2964 + 390.67050646 * k - 0.0016528 * T2 - 0.00000239 * T3; // Moon's argument of latitude
            double C1 = (0.1734 - 0.000393 * T) * Math.Sin(M * dr) + 0.0021 * Math.Sin(2 * dr * M);
            C1 = C1 - 0.4068 * Math.Sin(Mpr * dr) + 0.0161 * Math.Sin(dr * 2 * Mpr);
            C1 = C1 - 0.0004 * Math.Sin(dr * 3 * Mpr);
            C1 = C1 + 0.0104 * Math.Sin(dr * 2 * F) - 0.0051 * Math.Sin(dr * (M + Mpr));
            C1 = C1 - 0.0074 * Math.Sin(dr * (M - Mpr)) + 0.0004 * Math.Sin(dr * (2 * F + M));
            C1 = C1 - 0.0004 * Math.Sin(dr * (2 * F - M)) - 0.0006 * Math.Sin(dr * (2 * F + Mpr));
            C1 = C1 + 0.0010 * Math.Sin(dr * (2 * F - Mpr)) + 0.0005 * Math.Sin(dr * (2 * Mpr + M));
            double deltat;
            if (T < -11)
            {
                deltat = 0.001 + 0.000839 * T + 0.0002261 * T2 - 0.00000845 * T3 - 0.000000081 * T * T3;
            }
            else
            {
                deltat = -0.000278 + 0.000265 * T + 0.000262 * T2;
            }
            double JdNew = Jd1 + C1 - deltat;
            return JdNew;
        }

        private static double SunLongitude(double jdn)
        {
            double T = (jdn - 2451545.0) / 36525; // Time in Julian centuries from 2000-01-01 12:00:00 GMT
            double T2 = T * T;
            double dr = Math.PI / 180; // degree to radian
            double M = 357.52910 + 35999.05030 * T - 0.0001559 * T2 - 0.00000048 * T * T2; // mean anomaly, degree
            double L0 = 280.46645 + 36000.76983 * T + 0.0003032 * T2; // mean longitude, degree
            double DL = (1.914600 - 0.004817 * T - 0.000014 * T2) * Math.Sin(dr * M);
            DL = DL + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * M) + 0.000290 * Math.Sin(dr * 3 * M);
            double L = L0 + DL; // true longitude, degree
            L = L * dr;
            L = L - Math.PI * 2 * (INT(L / (Math.PI * 2))); // Normalize to (0, 2*PI)
            return L;
        }

        private static int[] LunarMonth11(int Y)
        {
            double off = LocalToJD(31, 12, Y) - 2415021.076998695;
            int k = INT(off / 29.530588853);
            double jd = NewMoon(k);
            int[] ret = LocalFromJD(jd);
            double sunLong = SunLongitude(LocalToJD(ret[0], ret[1], ret[2])); // sun longitude at local midnight
            if (sunLong > 3 * Math.PI / 2)
            {
                jd = NewMoon(k - 1);
            }
            return LocalFromJD(jd);
        }
        private static double[] SUNLONG_MAJOR = new double[] {
            0, Math.PI / 6, 2 * Math.PI / 6, 3 * Math.PI / 6, 4 * Math.PI / 6, 5 * Math.PI / 6,
            Math.PI, 7 * Math.PI / 6, 8 * Math.PI / 6, 9 * Math.PI / 6, 10 * Math.PI / 6, 11 * Math.PI / 6};

        static void InitLeapYear(int[][] ret)
        {
            double[] sunLongitudes = new double[ret.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                int[] a = ret[i];
                double jdAtMonthBegin = LocalToJD(a[0], a[1], a[2]);
                sunLongitudes[i] = SunLongitude(jdAtMonthBegin);
            }
            bool found = false;
            for (int i = 0; i < ret.Length; i++)
            {
                if (found)
                {
                    ret[i][3] = MOD(i + 10, 12);
                    continue;
                }
                double sl1 = sunLongitudes[i];
                double sl2 = sunLongitudes[i + 1];
                bool hasMajorTerm = Math.Floor(sl1 / Math.PI * 6) != Math.Floor(sl2 / Math.PI * 6);
                if (!hasMajorTerm)
                {
                    found = true;
                    ret[i][4] = 1;
                    ret[i][3] = MOD(i + 10, 12);
                }
            }
        }


        private static int[][] LunarYear(int Y)
        {
            int[][] ret = null;
            int[] month11A = LunarMonth11(Y - 1);
            double jdMonth11A = LocalToJD(month11A[0], month11A[1], month11A[2]);
            int k = (int)Math.Floor(0.5 + (jdMonth11A - 2415021.076998695) / 29.530588853);
            int[] month11B = LunarMonth11(Y);
            double off = LocalToJD(month11B[0], month11B[1], month11B[2]) - jdMonth11A;
            bool leap = off > 365.0;
            if (!leap)
            {
                ret = new int[13][];
            }
            else
            {
                ret = new int[14][];
            }
            ret[0] = new int[] { month11A[0], month11A[1], month11A[2], 0, 0 };
            ret[ret.Length - 1] = new int[] { month11B[0], month11B[1], month11B[2], 0, 0 };
            for (int i = 1; i < ret.Length - 1; i++)
            {
                double nm = NewMoon(k + i);
                int[] a = LocalFromJD(nm);
                ret[i] = new int[] { a[0], a[1], a[2], 0, 0 };
            }
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i][3] = MOD(i + 11, 12);
            }
            if (leap)
            {
                InitLeapYear(ret);
            }
            return ret;
        }

        private static int[] Solar2Lunar(int D, int M, int Y)
        {
            int yy = Y;
            int[][] ly = LunarYear(Y); // Please cache the result of this computation for later use!!!
            int[] month11 = ly[ly.Length - 1];
            double jdToday = LocalToJD(D, M, Y);
            double jdMonth11 = LocalToJD(month11[0], month11[1], month11[2]);
            if (jdToday >= jdMonth11)
            {
                ly = LunarYear(Y + 1);
                yy = Y + 1;
            }
            int i = ly.Length - 1;
            while (jdToday < LocalToJD(ly[i][0], ly[i][1], ly[i][2]))
            {
                i--;
            }
            int dd = (int)(jdToday - LocalToJD(ly[i][0], ly[i][1], ly[i][2])) + 1;
            int mm = ly[i][3];
            if (mm >= 11)
            {
                yy--;
            }
            return new int[] { dd, mm, yy };
        }
        public static DateTime lunarConverter(DateTime date)
        {
            int[] datetime = Solar2Lunar(date.Day, date.Month, date.Year);
            return new DateTime(datetime[2], datetime[1], datetime[0]);
        }*/

        public class Lunar
        {
            public bool isleap;
            public int lunarDay;
            public int lunarMonth;
            public int lunarYear;
        }



        public class Solar
        {
            public int solarDay;
            public int solarMonth;
            public int solarYear;




        }

        public class LunarSolarConverter
        {
            public static int[] lunar_month_days =
            {
            1887, 0x1694, 0x16aa, 0x4ad5, 0xab6, 0xc4b7, 0x4ae, 0xa56, 0xb52a,
            0x1d2a, 0xd54, 0x75aa, 0x156a, 0x1096d, 0x95c, 0x14ae, 0xaa4d, 0x1a4c, 0x1b2a, 0x8d55, 0xad4, 0x135a, 0x495d,
            0x95c, 0xd49b, 0x149a, 0x1a4a, 0xbaa5, 0x16a8, 0x1ad4, 0x52da, 0x12b6, 0xe937, 0x92e, 0x1496, 0xb64b, 0xd4a,
            0xda8, 0x95b5, 0x56c, 0x12ae, 0x492f, 0x92e, 0xcc96, 0x1a94, 0x1d4a, 0xada9, 0xb5a, 0x56c, 0x726e, 0x125c,
            0xf92d, 0x192a, 0x1a94, 0xdb4a, 0x16aa, 0xad4, 0x955b, 0x4ba, 0x125a, 0x592b, 0x152a, 0xf695, 0xd94, 0x16aa,
            0xaab5, 0x9b4, 0x14b6, 0x6a57, 0xa56, 0x1152a, 0x1d2a, 0xd54, 0xd5aa, 0x156a, 0x96c, 0x94ae, 0x14ae, 0xa4c,
            0x7d26, 0x1b2a, 0xeb55, 0xad4, 0x12da, 0xa95d, 0x95a, 0x149a, 0x9a4d, 0x1a4a, 0x11aa5, 0x16a8, 0x16d4,
            0xd2da, 0x12b6, 0x936, 0x9497, 0x1496, 0x1564b, 0xd4a, 0xda8, 0xd5b4, 0x156c, 0x12ae, 0xa92f, 0x92e, 0xc96,
            0x6d4a, 0x1d4a, 0x10d65, 0xb58, 0x156c, 0xb26d, 0x125c, 0x192c, 0x9a95, 0x1a94, 0x1b4a, 0x4b55, 0xad4,
            0xf55b, 0x4ba, 0x125a, 0xb92b, 0x152a, 0x1694, 0x96aa, 0x15aa, 0x12ab5, 0x974, 0x14b6, 0xca57, 0xa56, 0x1526,
            0x8e95, 0xd54, 0x15aa, 0x49b5, 0x96c, 0xd4ae, 0x149c, 0x1a4c, 0xbd26, 0x1aa6, 0xb54, 0x6d6a, 0x12da, 0x1695d,
            0x95a, 0x149a, 0xda4b, 0x1a4a, 0x1aa4, 0xbb54, 0x16b4, 0xada, 0x495b, 0x936, 0xf497, 0x1496, 0x154a, 0xb6a5,
            0xda4, 0x15b4, 0x6ab6, 0x126e, 0x1092f, 0x92e, 0xc96, 0xcd4a, 0x1d4a, 0xd64, 0x956c, 0x155c, 0x125c, 0x792e,
            0x192c, 0xfa95, 0x1a94, 0x1b4a, 0xab55, 0xad4, 0x14da, 0x8a5d, 0xa5a, 0x1152b, 0x152a, 0x1694, 0xd6aa,
            0x15aa, 0xab4, 0x94ba, 0x14b6, 0xa56, 0x7527, 0xd26, 0xee53, 0xd54, 0x15aa, 0xa9b5, 0x96c, 0x14ae, 0x8a4e,
            0x1a4c, 0x11d26, 0x1aa4, 0x1b54, 0xcd6a, 0xada, 0x95c, 0x949d, 0x149a, 0x1a2a, 0x5b25, 0x1aa4, 0xfb52,
            0x16b4, 0xaba, 0xa95b, 0x936, 0x1496, 0x9a4b, 0x154a, 0x136a5, 0xda4, 0x15ac
        };



            public static int[] solar_1_1 =
            {
            1887, 0xec04c, 0xec23f, 0xec435, 0xec649, 0xec83e, 0xeca51, 0xecc46, 0xece3a,
            0xed04d, 0xed242, 0xed436, 0xed64a, 0xed83f, 0xeda53, 0xedc48, 0xede3d, 0xee050, 0xee244, 0xee439, 0xee64d,
            0xee842, 0xeea36, 0xeec4a, 0xeee3e, 0xef052, 0xef246, 0xef43a, 0xef64e, 0xef843, 0xefa37, 0xefc4b, 0xefe41,
            0xf0054, 0xf0248, 0xf043c, 0xf0650, 0xf0845, 0xf0a38, 0xf0c4d, 0xf0e42, 0xf1037, 0xf124a, 0xf143e, 0xf1651,
            0xf1846, 0xf1a3a, 0xf1c4e, 0xf1e44, 0xf2038, 0xf224b, 0xf243f, 0xf2653, 0xf2848, 0xf2a3b, 0xf2c4f, 0xf2e45,
            0xf3039, 0xf324d, 0xf3442, 0xf3636, 0xf384a, 0xf3a3d, 0xf3c51, 0xf3e46, 0xf403b, 0xf424e, 0xf4443, 0xf4638,
            0xf484c, 0xf4a3f, 0xf4c52, 0xf4e48, 0xf503c, 0xf524f, 0xf5445, 0xf5639, 0xf584d, 0xf5a42, 0xf5c35, 0xf5e49,
            0xf603e, 0xf6251, 0xf6446, 0xf663b, 0xf684f, 0xf6a43, 0xf6c37, 0xf6e4b, 0xf703f, 0xf7252, 0xf7447, 0xf763c,
            0xf7850, 0xf7a45, 0xf7c39, 0xf7e4d, 0xf8042, 0xf8254, 0xf8449, 0xf863d, 0xf8851, 0xf8a46, 0xf8c3b, 0xf8e4f,
            0xf9044, 0xf9237, 0xf944a, 0xf963f, 0xf9853, 0xf9a47, 0xf9c3c, 0xf9e50, 0xfa045, 0xfa238, 0xfa44c, 0xfa641,
            0xfa836, 0xfaa49, 0xfac3d, 0xfae52, 0xfb047, 0xfb23a, 0xfb44e, 0xfb643, 0xfb837, 0xfba4a, 0xfbc3f, 0xfbe53,
            0xfc048, 0xfc23c, 0xfc450, 0xfc645, 0xfc839, 0xfca4c, 0xfcc41, 0xfce36, 0xfd04a, 0xfd23d, 0xfd451, 0xfd646,
            0xfd83a, 0xfda4d, 0xfdc43, 0xfde37, 0xfe04b, 0xfe23f, 0xfe453, 0xfe648, 0xfe83c, 0xfea4f, 0xfec44, 0xfee38,
            0xff04c, 0xff241, 0xff436, 0xff64a, 0xff83e, 0xffa51, 0xffc46, 0xffe3a, 0x10004e, 0x100242, 0x100437,
            0x10064b, 0x100841, 0x100a53, 0x100c48, 0x100e3c, 0x10104f, 0x101244, 0x101438, 0x10164c, 0x101842, 0x101a35,
            0x101c49, 0x101e3d, 0x102051, 0x102245, 0x10243a, 0x10264e, 0x102843, 0x102a37, 0x102c4b, 0x102e3f, 0x103053,
            0x103247, 0x10343b, 0x10364f, 0x103845, 0x103a38, 0x103c4c, 0x103e42, 0x104036, 0x104249, 0x10443d, 0x104651,
            0x104846, 0x104a3a, 0x104c4e, 0x104e43, 0x105038, 0x10524a, 0x10543e, 0x105652, 0x105847, 0x105a3b, 0x105c4f,
            0x105e45, 0x106039, 0x10624c, 0x106441, 0x106635, 0x106849, 0x106a3d, 0x106c51, 0x106e47, 0x10703c, 0x10724f,
            0x107444, 0x107638, 0x10784c, 0x107a3f, 0x107c53, 0x107e48
        };



            public static int GetBitInt(int data, int length, int shift)
            {
                return (data & (((1 << length) - 1) << shift)) >> shift;
            }



            //WARNING: Dates before Oct. 1582 are inaccurate 
            public static long SolarToInt(int y, int m, int d)
            {
                m = (m + 9) % 12;
                y = y - m / 10;
                return 365 * y + y / 4 - y / 100 + y / 400 + (m * 306 + 5) / 10 + (d - 1);
            }



            public static Solar SolarFromInt(long g)
            {
                long y = (10000 * g + 14780) / 3652425;
                long ddd = g - (365 * y + y / 4 - y / 100 + y / 400);
                if (ddd < 0)
                {
                    y--;
                    ddd = g - (365 * y + y / 4 - y / 100 + y / 400);
                }
                long mi = (100 * ddd + 52) / 3060;
                long mm = (mi + 2) % 12 + 1;
                y = y + (mi + 2) / 12;
                long dd = ddd - (mi * 306 + 5) / 10 + 1;
                var solar = new Solar();
                solar.solarYear = (int)y;
                solar.solarMonth = (int)mm;
                solar.solarDay = (int)dd;
                return solar;
            }



            private static bool IsLeapYear(int y)
            {
                return ((y % 4 == 0 && y % 100 != 0) || (y % 400 == 0));
            }



            public static Solar LunarToSolar(Lunar lunar)
            {
                int days = lunar_month_days[lunar.lunarYear - lunar_month_days[0]];
                int leap = GetBitInt(days, 4, 13);
                int offset = 0;
                int loopend = leap;
                if (!lunar.isleap)
                {
                    if (lunar.lunarMonth <= leap || leap == 0)
                    {
                        loopend = lunar.lunarMonth - 1;
                    }
                    else
                    {
                        loopend = lunar.lunarMonth;
                    }
                }
                for (int i = 0; i < loopend; i++)
                {
                    offset += GetBitInt(days, 1, 12 - i) == 1 ? 30 : 29;
                }
                offset += lunar.lunarDay;



                int solar11 = solar_1_1[lunar.lunarYear - solar_1_1[0]];



                int y = GetBitInt(solar11, 12, 9);
                int m = GetBitInt(solar11, 4, 5);
                int d = GetBitInt(solar11, 5, 0);



                return SolarFromInt(SolarToInt(y, m, d) + offset - 1);
            }



            public static Lunar SolarToLunar(Solar solar)
            {
                var lunar = new Lunar();
                int index = solar.solarYear - solar_1_1[0];
                int data = (solar.solarYear << 9) | (solar.solarMonth << 5) | (solar.solarDay);
                int solar11 = 0;
                if (solar_1_1[index] > data)
                {
                    index--;
                }
                solar11 = solar_1_1[index];
                int y = GetBitInt(solar11, 12, 9);
                int m = GetBitInt(solar11, 4, 5);
                int d = GetBitInt(solar11, 5, 0);
                long offset = SolarToInt(solar.solarYear, solar.solarMonth, solar.solarDay) - SolarToInt(y, m, d);



                int days = lunar_month_days[index];
                int leap = GetBitInt(days, 4, 13);



                int lunarY = index + solar_1_1[0];
                int lunarM = 1;
                int lunarD = 1;
                offset += 1;



                for (int i = 0; i < 13; i++)
                {
                    int dm = GetBitInt(days, 1, 12 - i) == 1 ? 30 : 29;
                    if (offset > dm)
                    {
                        lunarM++;
                        offset -= dm;
                    }
                    else
                    {
                        break;
                    }
                }
                lunarD = (int)(offset);
                lunar.lunarYear = lunarY;
                lunar.lunarMonth = lunarM;
                lunar.isleap = false;
                if (leap != 0 && lunarM > leap)
                {
                    lunar.lunarMonth = lunarM - 1;
                    if (lunarM == leap + 1)
                    {
                        lunar.isleap = true;
                    }
                }
                lunar.lunarDay = lunarD;
                return lunar;
            }
        }

        public static DateTime lunarConverter(DateTime sDate)
        {
            var solarDate = new Solar()
            {
                solarDay = sDate.Day,
                solarMonth = sDate.Month,
                solarYear = sDate.Year
            };
            var lunarDate = LunarSolarConverter.SolarToLunar(solarDate);
            return new DateTime(lunarDate.lunarYear, lunarDate.lunarMonth, lunarDate.lunarDay);
        }

        public static DateTime solarConverter(DateTime lDate)
        {
            var lunarDate = new Lunar()
            {
                isleap = false,
                lunarDay = lDate.Day,
                lunarMonth = lDate.Month,
                lunarYear = lDate.Year
            };
            var solarDate = LunarSolarConverter.LunarToSolar(lunarDate);
            return new DateTime(solarDate.solarYear, solarDate.solarMonth, solarDate.solarDay);
        }
    }
}
