using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class SecurityHelper
    {
        private static readonly Encoding Encoder = Encoding.UTF8;

        #region MD5加密
        /// <summary>
        /// 将字符串加密为MD5
        /// </summary>
        /// <returns></returns>
        private static string EncodeMD5(string text)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(text));
            md5.Clear();
            var sTemp = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
                sTemp.Append(result[i].ToString("x").PadLeft(2, '0'));
            return sTemp.ToString().ToLower();
        }

        /// <summary>
        /// 将字符串加密为MD5
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <param name="isUpper">是否要返回大写的MD5串</param>
        /// <returns></returns>
        public static string EncodeMD5(string text, bool isUpper)
        {
            if (isUpper)
            {
                return EncodeMD5(text);
            }

            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            //二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }
            return result.ToString();
        }

        #endregion

    }
}
