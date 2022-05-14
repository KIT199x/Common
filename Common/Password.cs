using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Password
    {
        /// <summary>
        /// Tạo mật khẩu ngẫu nhiên
        /// </summary>
        /// <returns>Mật khẩu được tạo tự động</returns>
        public static string CreatePassword()
        {
            int length = 20;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*$+?_&=!%{}/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
