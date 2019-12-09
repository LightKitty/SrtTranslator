using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace SrtTranslator
{
    public static class Translater
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        const string translateUrl = "https://translate.google.cn/translate_a/single?client=webapp&sl=auto&tl=zh-CN&hl=zh-CN&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&pc=4&otf=2&ssel=3&tsel=3&kc=4&tk=127250.482507";

        public static string Translate(string text)
        {
            string questUrl = translateUrl + text;
            var get = HttpWebResponseUtility.CreatePostHttpResponse(translateUrl, new Dictionary<string, string>() { { "q", text } }, null, null, Encoding.UTF8, null);
            string qqq = (Encoding.UTF8.GetString(SimpleGet(translateUrl + "&q=hello")));
            string result = Encoding.UTF8.GetString(SimplePost(translateUrl, new Dictionary<string, string>() { { "q", text } }));
            return result;
        }

        /// <summary>
        /// 简易HttpGet请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] SimpleGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.Timeout = 5000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            { //循环度可以避免数据不完整
                int n = stream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
            }
            return buf;
        }

        /// <summary>
        /// 简易HttpPost请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>返回值</returns>
        public static byte[] SimplePost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST"; //设置为post请求
            request.ContentType = "application/x-www-form-urlencoded";

            StringBuilder buffer = new StringBuilder();
            bool flag = false;
            foreach (string key in parameters.Keys)
            {
                if (flag)
                {
                    buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    flag = true;
                }
            }
            byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
            using (Stream requeststream = request.GetRequestStream())
            {
                requeststream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            { //循环度可以避免数据不完整
                int n = stream.Read(buf, offset, count);
                if (n == 0) break;
                count -= n;
                offset += n;
            }
            return buf;
        }
    }
}
