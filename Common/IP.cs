using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class IP
    {
        /// <summary>
        /// Get IP Public
        /// </summary>
        /// <returns>IP</returns>
        public static IPAddress GetIpPublic()
        {
            List<string> urls = new List<string>()
            {
                "https://ipv4.icanhazip.com",
                "https://api.ipify.org",
                "https://ipinfo.io/ip",
                "https://checkip.amazonaws.com",
                "https://wtfismyip.com/text",
                "http://icanhazip.com"
            };
            foreach (var url in urls)
            {
                try
                {
                    return IPAddress.Parse(new WebClient().DownloadString(url).Replace("\\r\\n", "").Replace("\\n", "").Trim());
                }
                catch
                {
                    return IPAddress.Parse("0.0.0.0");
                }
            }
            return IPAddress.Parse("0.0.0.0");
        }
        /// <summary>
        /// Get IP Local
        /// </summary>
        /// <returns>IP</returns>
        public static IPAddress GetIpLocal()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IPAddress.Parse(ip.ToString());
                    }
                }
            }
            catch (Exception)
            {
                return IPAddress.Parse("0.0.0.0");
            }
            return IPAddress.Parse("0.0.0.0");
        }
    }
}
