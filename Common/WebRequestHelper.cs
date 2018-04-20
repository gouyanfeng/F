using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class WebRequestHelper
    {


        public enum Method { GET, POST };

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">POST数据</param>
        /// <returns></returns>
        public static string Request(string url, string postData = null)
        {
            return Request(Method.POST, url, System.Text.Encoding.UTF8, null, postData, null);
        }
        public static string Request(Method method, string url, Encoding encoding, string contentType = null, string postData = null, Cookie cookie = null, int timeOut = 120000)
        {
            HttpWebRequest webRequest = null;

            string responseData = "";
            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            webRequest.Timeout = 120000;
            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;
            if (cookie != null)
            {
                webRequest.CookieContainer = new CookieContainer();
                webRequest.CookieContainer.Add(cookie);
            }
            if (method == Method.POST)
            {
                if (string.IsNullOrEmpty(contentType))
                {
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                }
                else
                {
                    webRequest.ContentType = contentType;
                }

                if (!string.IsNullOrEmpty(postData))
                {
                    Stream stream = null;
                    StreamWriter requestWriter = null;
                    try
                    {
                        stream = webRequest.GetRequestStream();
                        requestWriter = new StreamWriter(stream);
                        requestWriter.Write(postData);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        if (requestWriter != null)
                        {
                            requestWriter.Dispose();
                            requestWriter = null;
                        }
                        if (stream != null)
                        {
                            stream.Dispose();
                            stream = null;
                        }
                    }
                }
            }

            responseData = WebResponseGetEncoding(webRequest, encoding);
            webRequest = null;

            return responseData;
        }
        private static string WebResponseGetEncoding(HttpWebRequest webRequest, Encoding encoding)
        {
            using (var responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream(), encoding))
            {
                return responseReader.ReadToEnd();
            }
        }
    }

}
