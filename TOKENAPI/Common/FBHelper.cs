using System.Numerics;
using TOKENAPI.Enums;

namespace TOKENAPI.Common
{
    public static class FBHelper
    {
        public const string dfp = "0.###################################################################################################################################################################################################################################################################################################################################################";

        public static decimal FrWei(this double bint, int deci) // FrWei
        {
            bint = bint / Math.Pow(10, deci);
            return decimal.Parse(bint.ToString(dfp));
        }
        public static decimal FrWei(this BigInteger bint, int deci) // FrWei
        {
            double blong = (double)bint / Math.Pow(10, deci);
            return decimal.Parse(blong.ToString(dfp));
        }
        public static double FrWeiDbl(this BigInteger bint, int deci) // FrWei
        {
            double blong = (double)bint / Math.Pow(10, deci);
            return double.Parse(blong.ToString(dfp));
        }
        public static double ToWei(this decimal bint, int deci) // ToWei
        {
            return double.Parse(bint.ToString()) * Math.Pow(10, deci);
        }
        public static BigInteger ToWeiBN(this decimal bint, int deci) // ToWei
        {
            return new BigInteger((double)bint * Math.Pow(10, deci));
        }

        public static ulong ToUnix(this DateTime date)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToUInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }

        public static DateTime ToDate(this double unix)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unix).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime ToDate(this ulong unix)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unix).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime ToDate(this BigInteger unix)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Convert.ToInt64(unix)).ToLocalTime();
            return dtDateTime;
        }
        public static bool EqualI(this string str1, string str2) => (str1??"").Equals(str2,StringComparison.OrdinalIgnoreCase);


        public static async Task<bool> AniAsync<T>(
        this IEnumerable<T> source, Func<T, Task<bool>> func)
            {
                foreach (var element in source)
                {
                    if (await func(element))
                        return true;
                }
                return false;
            }

        public static (string top_dt, ulong top_mk, string end_dt, ulong end_mk) GetTimeFrame(TUnit unit, int sta, int per)
        {
            DateTime now = DateTime.Now;
            ulong top_mk, end_mk;
            string top_dt, end_dt;
            DateTime top, end;

            switch (unit)
            {
                case TUnit.HOUR:
                    top = new DateTime(now.AddHours(sta).Year, now.AddHours(sta).Month, now.AddHours(sta).Day, now.AddHours(sta).Hour, 0, 0);
                    top_mk = top.ToUnix();
                    top_dt = top.ToString("yyyy-MM-dd HH:mm:ss");
                    end = new DateTime(now.AddHours(sta + per).Year, now.AddHours(sta + per).Month, now.AddHours(sta + per).Day, now.AddHours(sta + per).Hour, 0, 0);
                    end_mk = end.ToUnix();
                    end_dt = end.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case TUnit.MINUTE:
                default:

                    top = new DateTime(now.AddMinutes(sta).Year, now.AddMinutes(sta).Month, now.AddMinutes(sta).Day, now.AddMinutes(sta).Hour, now.AddMinutes(sta).Minute, 0);
                    top_mk = top.ToUnix();
                    top_dt = top.ToString("yyyy-MM-dd HH:mm:ss");
                    end = new DateTime(now.AddMinutes(sta + per).Year, now.AddMinutes(sta + per).Month, now.AddMinutes(sta + per).Day, now.AddMinutes(sta + per).Hour, now.AddMinutes(sta + per).Minute, 0);
                    end_mk = end.ToUnix();
                    end_dt = end.ToString("yyyy-MM-dd HH:mm:ss");
                    break;

            }
            return (top_dt, top_mk, end_dt, end_mk);
        }

    }
}
