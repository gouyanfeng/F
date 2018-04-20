using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Utils
    {

        /// <summary>
        /// 日期转成Unix10位时间戳(Unix timestamp)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int ToUnixTimestamp(this DateTime datetime)
        {
            return ((datetime.ToUniversalTime().Ticks - 621355968000000000) / 10000000).TryInt(0);
        }

        /// <summary>
        /// 10位unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        /// <summary>
        /// 转换为short，默认值：short.MinValue
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static short TryShort(this Object strText)
        {
            return TryShort(strText, short.MinValue);
        }

        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short TryShort(this Object strText, short defValue)
        {
            short result = 0;
            return short.TryParse(strText + "", out result) ? result : defValue;
        }
        /// <summary>
        /// 转换为short
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static short? TryShort(this Object strText, short? defValue)
        {
            short result = 0;
            return short.TryParse(strText + "", out result) ? result : defValue;
        }

        /// <summary>
        /// 转换为Int，默认值：int.MinValue
        /// </summary>
        public static int TryInt(this Object strText)
        {
            return TryInt(strText, int.MinValue);
        }

        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int TryInt(this Object strText, int defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(strText + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Int
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int? TryInt(this Object strText, int? defValue)
        {
            int temp = int.MinValue;
            return int.TryParse(strText + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Double，默认值：double.MinValue
        /// </summary>
        public static double TryDouble(this Object strText)
        {
            return TryDouble(strText, double.MinValue);
        }

        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double TryDouble(this Object strText, double defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// 转换为Double
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static double? TryDouble(this Object strText, double? defValue)
        {
            double temp = double.MinValue;
            return double.TryParse(strText + "", out temp) ? temp : defValue;
        }
        /// <summary>
        /// 转换为Decimal，默认值：decimal.MinValue
        /// </summary>
        public static decimal TryDecimal(this Object strText)
        {
            return TryDecimal(strText, decimal.MinValue);
        }

        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal TryDecimal(this Object strText, decimal defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// 转换为Decimal
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static decimal? TryDecimal(this Object strText, decimal? defValue)
        {
            decimal temp = decimal.MinValue;
            return decimal.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// 转换为long，默认值：long.MinValue
        /// </summary>
        public static long TryLong(this Object strText)
        {
            return TryLong(strText, long.MinValue);
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long TryLong(this Object strText, long defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static long? TryLong(this Object strText, long? defValue)
        {
            long temp = long.MinValue;
            return long.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// 转换为Boolean，默认值：false
        /// </summary>
        public static bool TryBool(this Object strText)
        {
            return TryBool(strText, false);
        }

        /// <summary>
        /// 转换为Boolean
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool TryBool(this Object strText, bool defValue)
        {
            bool temp = false;
            return bool.TryParse(strText + "", out temp) ? temp : defValue;
        }

        /// <summary>
        /// 转换为Boolean
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool? TryBool(this Object strText, bool? defValue)
        {
            bool temp = false;
            return bool.TryParse(strText + "", out temp) ? temp : defValue;
        }

        public static string NewGuid32(this Guid guid)
        {

            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
