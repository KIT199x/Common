using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class Phone
    {
        /// <summary>
        /// Kiểm tra định dạng số điện thoại Việt Nam
        /// </summary>
        /// <param name="number">Số điện thoại</param>
        /// <returns>True/False</returns>
        public static bool IsPhoneNumberVietNam(string number)
        {
            string regexVN = @"(\+84[3|5|7|8|9]|84[3|5|7|8|9]|0[3|5|7|8|9])+([0-9]{8})\b";
            if (number != null)
            {
                return Regex.IsMatch(number, regexVN);
            }
            else
            {
                return false;
            }
        }
    }
}
