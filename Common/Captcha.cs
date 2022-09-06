using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Captcha
    {
        private class ReCaptchaResponse
        {
            public bool Success;
            public string ChallengeTs;
            public string Hostname;
            public object[] ErrorCodes;
        }
        public static bool ValidCaptcha(string token)
        {
            bool isHuman = true;

            try
            {
                string secretKey = "6Lcx-vUfAAAAAJAVKkyybKlDHLSBlicWEyqUvK8K";
                Uri uri = new Uri("https://www.google.com/recaptcha/api/siteverify" +
                                  $"?secret={secretKey}&response={token}");
                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                ReCaptchaResponse reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(result);
                isHuman = reCaptchaResponse.Success;
            }
            catch (Exception ex)
            {
                return false;
            }
            return isHuman;
        }
    }
}
