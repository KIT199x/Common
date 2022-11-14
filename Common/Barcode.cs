using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZXing.QrCode;

namespace Common
{
    public static class Barcode
    {
        public static string Translate(string _data)
        {
            var width = 450;
            var height = 100;
            var barcodeWriterPixelData = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    PureBarcode = true,
                    Margin = 0
                }
            };
            var pixelData = barcodeWriterPixelData.Write(_data);
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb))
            {
                using (var memoryStream = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                    try
                    {
                        Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(memoryStream.ToArray()));
                }
            }
        }
    }
}
