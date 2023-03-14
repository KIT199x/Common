using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class VietQR
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="soTien">Số tiền chuyển: 100000</param>
        /// <param name="noiDung">Nội dung chuyển khoản: Nguyen Ba Kien Chuyen Khoan</param>
        /// <param name="soTaiKhoan">Số tài khoản : 9704229254607018</param>
        /// <param name="hoVaTen">Họ và tên: NGUYEN BA KIEN</param>
        /// <param name="acqId">BIN thẻ: 970422 tham khảo tại link https://bit.ly/BIN-Bank</param>
        /// <param name="maMau">Mã màu: #000</param>
        /// <param name="imageLogo">Logo giữa QR: định dạng base64</param>
        /// <param name="logoBank">Logo ngân hàng nếu hiển thị tên và số tiền: url logo bank</param>
        /// <param name="template">Mẫu: qr_only</param>
        /// <param name="theme">Chủ đề: qr_only</param>
        /// <returns></returns>
        public static string GenerateQR(string xClientId, string xApiKey, string soTien, string noiDung, string soTaiKhoan, string hoVaTen, string acqId, string maMau, string imageLogo, string logoBank, string template, string theme)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.vietqr.io/v2/generate");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 12000;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("x-client-id", xClientId);
            httpWebRequest.Headers.Add("x-api-key", xApiKey);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                BodyVietQR vietQR = new BodyVietQR();
                vietQR.accountNo = soTaiKhoan;
                vietQR.accountName = hoVaTen;
                vietQR.acqId = acqId;
                vietQR.addInfo = noiDung;
                vietQR.amount = soTien;
                vietQR.colorDark = maMau;
                vietQR.logo = imageLogo;
                vietQR.logoBank = logoBank;
                vietQR.template = template;
                vietQR.theme = theme;
                string json = JsonConvert.SerializeObject(vietQR);

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
                var MyQRWithLogo = myDeserializedClass.data.qrDataURL;
                return MyQRWithLogo;
            }
        }
    }

    public class BodyVietQR
    {
        public string accountName { get; set; }
        public string accountNo { get; set; }
        public string acqId { get; set; }
        public string addInfo { get; set; }
        public string amount { get; set; }
        public string colorDark { get; set; }
        public string logo { get; set; }
        public string template { get; set; }
        public string theme { get; set; }
        public string logoBank { get; set; }
    }
    #region Convert json VietQR to Model
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public string qrDataURL { get; set; }
        public object qrCode { get; set; }
    }

    public class Root
    {
        public string code { get; set; }
        public string desc { get; set; }
        public Data data { get; set; }
    }
    #endregion
}