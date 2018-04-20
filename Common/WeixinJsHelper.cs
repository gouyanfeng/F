using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common
{


    /// <summary>
    ///  微信JSSDK
    /// </summary>
    public class WeixinJsHelper
    {

        public string AppId { get; private set; }
        public int Timestamp { get; private set; }
        public string NonceStr { get; private set; }
        public string Signature { get; private set; }


        /// <summary>
        ///      WeixinJsHelper v = new WeixinJsHelper("wx6beb9fd2", "94a5d1f1a11ca97e82", "ccc");
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secrect"></param>
        /// <param name="url"></param>
        public WeixinJsHelper(string appid, string secrect, string url)
        {

            AppId = appid;
            Timestamp = DateTime.Now.ToUnixTimestamp();
            NonceStr = Guid.NewGuid().ToString();

            var key = appid + secrect;
            var value = HttpRuntime.Cache.Get(key);
            if (value != null)
            {
                Signature = GetSignature(value.ToString(), NonceStr, Timestamp, url);
            }
            var token = GetAccessToken(appid, secrect);
            var jsTicket = GetTickect(token.access_token);


            //暂时用系统缓存
            HttpRuntime.Cache
                .Add(key,
                jsTicket.ticket,
                null,
                DateTime.Now.AddSeconds(7000),
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default, null);

            Signature = GetSignature(jsTicket.ticket, NonceStr, Timestamp, url);

        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secrect"></param>
        /// <returns></returns>
        private dynamic GetAccessToken(string appid, string secrect)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}", "client_credential", appid, secrect);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var token = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            return token;
        }

        /// <summary>
        /// 获取jsapi_ticket
        /// jsapi_ticket是公众号用于调用微信JS接口的临时票据。
        /// 正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。
        /// 由于获取jsapi_ticket的api调用次数非常有限，频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，开发者必须在自己的服务全局缓存jsapi_ticket 。
        /// </summary>
        /// <param name="access_token">BasicAPI获取的access_token,也可以通过TokenHelper获取</param>
        /// <returns></returns>
        private dynamic GetTickect(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var jsTicket = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            return jsTicket;
        }

        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        private string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url)
        {
            var string1Builder = new StringBuilder();
            string1Builder.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
                          .Append("noncestr=").Append(noncestr).Append("&")
                          .Append("timestamp=").Append(timestamp).Append("&")
                          .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            return Sha1(string1Builder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgStr"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private string Sha1(string orgStr, string encode = "UTF-8")
        {
            var sha1 = new SHA1Managed();
            var sha1bytes = System.Text.Encoding.GetEncoding(encode).GetBytes(orgStr);
            byte[] resultHash = sha1.ComputeHash(sha1bytes);
            string sha1String = BitConverter.ToString(resultHash).ToLower();
            sha1String = sha1String.Replace("-", "");
            return sha1String;
        }
    }
}
