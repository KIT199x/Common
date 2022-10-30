using System;
using System.IO;
using System.Net;
using System.Web;

namespace Common
{
    public static class Language
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="translateTo"></param>
        /// <returns></returns>
        public static string Translate(string word, string translateTo = "vi")
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={1}&dt=t&q={0}", HttpUtility.UrlEncode(word), translateTo));
            request.Method = "GET";
            String responseJson = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseJson = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
            return responseJson.ToString().Replace('[', ' ').Replace(']', ' ').Split(',')[0].Replace('"', ' ').Trim();
        }
    }
}
