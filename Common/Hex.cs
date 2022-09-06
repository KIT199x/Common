using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Hex
    {
        /// <summary>
        /// var string2hex=Common.Hex.Text2Hex("STRING");
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string Text2Hex(string hex)
        {
            byte[] ba = Encoding.Default.GetBytes(hex);
            var hexString = BitConverter.ToString(ba);
            hexString = hexString.Replace("-", " ");
            return hexString;
        }
        /// <summary>
        ///  byte[] data=Common.Hex.Hext2Text("HEX");
        ///  string hex2string = Encoding.ASCII.GetString(data);
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] Hext2Text(string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
