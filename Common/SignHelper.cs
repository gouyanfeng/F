using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SignHelper
    {


        /// <summary>
        /// 微信支付 统一下单  签名生成
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string SignWeixinPay(IDictionary<string, object> parameters, string appKey)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, object> sortedParams = new SortedDictionary<string, object>(parameters);
            IEnumerator<KeyValuePair<string, object>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数作为JSON串
            StringBuilder query = new StringBuilder();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                object value = dem.Current.Value;
                query.Append(key);
                query.Append("=");
                query.Append(value);
                query.Append("&");
            }
            query = query.Remove(query.Length - 1, 1);
            query.Append("&key=" + appKey);

            return SecurityHelper.EncodeMD5(query.ToString(), false);

        }





        /// <summary>
        /// 支付宝支付服务端签名生成 [REA]
        /// </summary>
        /// <param name="parameters">业务参数</param>
        /// <param name="privateKeyPem">REA私钥</param>
        /// <returns></returns>
        public static string SignAlipayPay(IDictionary<string, string> parameters, string privateKeyPem)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数作为JSON串
            StringBuilder query = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                object value = dem.Current.Value;
                query.Append(key);
                query.Append("=");
                query.Append("\"" + value + "\"");
                query.Append("&");
            }
            var content = query.Remove(query.Length - 1, 1);

            var result = RSAFromPkcs8.sign(content.ToString(), privateKeyPem, "utf-8");
            content.Append("&sign_type=\"RSA\"&sign=\"" + result + "\"");
            return content.ToString();

        }
    }
}
