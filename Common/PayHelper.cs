using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common
{
    public class PayHelper
    {
        /// <summary>
        /// 微信APP支付服务端
        /// </summary>
        /// <returns></returns>
        public string Weixin()
        {
            //1.接收业务参数

            Dictionary<string, object> dic = new Dictionary<string, object>();

            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            string appid = "wx49add75c836ce986";
            string mch_id = "1245408302";
            string appKey = "RJEmdA6KBJXUephhkejHxPyvntUf5JLc";
            string nonce_str = Guid.NewGuid().NewGuid32();
            string body = "测试";
            string out_trade_no = "0000000000000001x";
            string total_fee = "100";
            string notify_url = "ccccccccccc";//回调地址
            string trade_type = "APP";
            string spbill_create_ip = "127.0.0.1";
            string timeStamp = DateTime.Now.ToUnixTimestamp().ToString();
            string package = "Sign=WXPay";

            dic.Add("appid", appid);
            dic.Add("mch_id", mch_id);
            dic.Add("nonce_str", nonce_str);
            dic.Add("body", body);
            dic.Add("out_trade_no", out_trade_no);
            dic.Add("total_fee", total_fee);//单位分
            dic.Add("notify_url", notify_url);
            dic.Add("trade_type", trade_type);
            dic.Add("spbill_create_ip", spbill_create_ip);


            //2.拿业务参数+appKey 进行签名
            var sign = SignHelper.SignWeixinPay(dic, appKey);

            var xml = new System.Text.StringBuilder();
            xml.Append("<xml>");
            xml.Append("<appid>" + appid + "</appid>");
            xml.Append("<mch_id>" + mch_id + "</mch_id>");
            xml.Append("<nonce_str>" + nonce_str + "</nonce_str>");
            xml.Append("<body>" + body + "</body>");
            xml.Append("<out_trade_no>" + out_trade_no + "</out_trade_no>");
            xml.Append("<total_fee>" + total_fee + "</total_fee>");
            xml.Append("<notify_url>" + notify_url + "</notify_url>");
            xml.Append("<trade_type>" + trade_type + "</trade_type>");
            xml.Append("<spbill_create_ip>" + spbill_create_ip + "</spbill_create_ip>");
            xml.Append("<sign>" + sign + "</sign>");
            xml.Append("</xml>");


            //3.将签名和其他参数拼成的XML进行统一下单
            var response = WebRequestHelper.Request(url, xml.ToString());

            string result = string.Empty;
            var xmlresponse = XDocument.Parse(response);
            var return_code = xmlresponse.Root.Element("return_code");
            if (return_code != null)
            {
                if (return_code.Value == "SUCCESS")
                {
                    if (xmlresponse.Root.Element("result_code").Value == "SUCCESS")
                    {
                        //4.拿到prepay_id后进行第二次签名 
                        var prepayid = xmlresponse.Root.Element("prepay_id").Value;
                        dic = new Dictionary<string, object>();
                        dic.Add("appid", appid);
                        dic.Add("partnerid", mch_id);
                        dic.Add("noncestr", nonce_str);
                        dic.Add("prepayid", prepayid);
                        dic.Add("package", package);
                        dic.Add("timestamp", timeStamp);
                        sign = SignHelper.SignWeixinPay(dic, appKey);

                        //5.将签名和其他参数返回客户端
                        result = new { appid = appid, partnerid = mch_id, noncestr = nonce_str, prepayid = prepayid, package = package, timestamp = timeStamp, sign = sign }.ToString();
                    }
                    else
                    {
                        result = xmlresponse.Root.Element("err_code_des").Value;
                    }
                }
                else
                {
                    result = xmlresponse.Root.Element("return_msg").Value;
                }
            }
            else
            {
                result = "微信服务器请求异常";
            }
            return result;
        }


        /// <summary>
        /// 支付宝APP支付服务端 RSA加密
        /// </summary>
        /// <returns></returns>
        public string Alipay()
        {
            string service = "mobile.securitypay.pay";
            string partner = "2088511305329300";
            string seller_id = "admin@mmiyou.com";
            string _input_charset = "utf-8";
            string notify_url = "xxxxxxxxxxxxx";
            string out_trade_no = "xxxxxxxxxx";
            string subject = "xxxxxxxxxxxxx";
            string payment_type = "1";
            decimal total_fee = 0.01M;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("service", service);
            dic.Add("partner", partner);
            dic.Add("_input_charset", _input_charset);
            dic.Add("notify_url", notify_url);
            dic.Add("out_trade_no", out_trade_no);
            dic.Add("subject", subject);
            dic.Add("payment_type", payment_type);
            dic.Add("seller_id", seller_id);
            dic.Add("total_fee", total_fee.ToString());

            var privateKeyPem = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBALHv4R4wuXRgFjAUxfy/Bf3MG5K7eQYf6wdAbAOcSHinYxaghZVi0hEU/m2tea8aGwhhZ9n+RMdHaVc3GOEXVbqxH+9eYCaqpeZTCQHEv1fPAxLI1zw3wosfuIbeZnbKzJrMJpuGPx6VIShLFQXatMNMVXIe6XR/yDNRODHeWa1xAgMBAAECgYAUPPJf2q7dO6iKY3J5Ysitqy9fqw8C0VKMCpm+d2IOxwsQIcXPzeOSqc6ebuKuNcvpUhrbwn3UJJ0onE2qHFtVpeC5zCilQ6DI1w+b8+IFgosm2wsCFiwWVkD6O4PWYDfC77NQCNG/7UZwmyUVEwBkWp5+z5ymwjRJP4Pt7iReuQJBAOie8R6tikK3sfPX33s20ipycuIwL49l7lwCx4XpINRbfR8kEGDnbt1qRmzyvmaL8eI0aHme/Nuf691i6eTANC8CQQDD0fzGp4DaS1RRKgY2GHG0M/w1/eah98LOT7qocKHcZ4dWHHKwPKqZeqWkg/Ij80N4dKaGd+0ReQGaiF6jurBfAkAShzZlXyU4EzGVXohqY/9xahREd9oR7eiCY6ZIwDzhLThWFqVHUqe98w0Q+HS57kOh5NK9fTBeveonhA1lLvn5AkEAoKDc6Bu479gBBz8grzkn9Dj8d1GZ4Vy6eMAgDlGuBH0MIA/f6D8rdhCMVcJCW0kOHH4bp4wydDW9ogShFU9rowJAHvuHqjpaQRGNoaij2H9ltSCjKhlK9sBcHBtx2T+/RWe21Lf0SfbSRbQH1dv+gd+xZFEi6mTmMRj0usyt+Rv/2A==";

            var result = SignHelper.SignAlipayPay(dic, privateKeyPem);
            return result;
        }
    }
}
