using System;
using System.Net;
using System.Web;

namespace Common
{
    public static class Language
    {
        /// <summary>
        /// Dịch ngôn ngữ
        /// </summary>
        /// <param name="word">Từ cần dịch</param>
        /// <param name="from">dịch từ ngôn ngữ vd: en</param>
        /// <param name="to">Ngôn ngữ dịch vd: en</param>
        /// <returns>Ngôn ngữ cần dịch hoặc lỗi nếu có</returns>
        public static string Translate(string word, string from, string to)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={from}&tl={to}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Có lỗi khi dịch ngôn ngữ từ '" + from + "' sang ngôn ngữ '" + to + "'";
            }
        }
    }
}
