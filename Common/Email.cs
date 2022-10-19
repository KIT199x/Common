using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Common
{
    public static class Email
    {
        /// <summary>
        /// Validate Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true = success; false = error</returns>
        public static bool ValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        public static bool SendEmail(string to, string subject, string cc, string bcc, string urlTemplate = "")
        {
            try
            {
                #region Config email sender
                var emailSender = "servicemedia.vn@gmail.com";// Mail gửi thư
                var passwordEmailSender = "Matkhau2022";//Mật khẩu mail gửi thư
                var nameEmail = "Service Send Mail";//Tên mail gửi thư
                var host = "smtp.gmail.com";//Host mail(gmail, hotmail, ...)
                var port = 587;//Port gửi mail của host
                #endregion
                using (MailMessage mail = new MailMessage())
                {
                    string FilePath = urlTemplate;
                    StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();

                    //Thay thế [username] = Tài khoản người dùng  
                    MailText = MailText.Replace("[username]", "KienNB");//Chú ý "KienNB"


                    MailMessage _mailmsg = new MailMessage();

                    //Make TRUE because our body text is html  
                    _mailmsg.IsBodyHtml = true;

                    //Set From Email ID  
                    _mailmsg.From = new MailAddress(emailSender, nameEmail);

                    //Set To Email ID  
                    _mailmsg.To.Add(to);

                    //Set CC Email ID 
                    if (cc != "")
                    {
                        _mailmsg.CC.Add(cc);
                    }

                    //Set BCC Email ID 
                    if (bcc != "")
                    {
                        _mailmsg.Bcc.Add(bcc);
                    }

                    //Set Subject  
                    _mailmsg.Subject = subject;

                    //Set Body Text of Email   
                    _mailmsg.Body = MailText;


                    //Now set your SMTP   
                    SmtpClient _smtp = new SmtpClient();

                    //Set HOST server SMTP detail  
                    _smtp.Host = host;

                    //Set PORT number of SMTP  
                    _smtp.Port = port;

                    //Set SSL --> True / False  
                    _smtp.EnableSsl = true;

                    //Set Sender UserEmailID, Password  
                    NetworkCredential _network = new NetworkCredential(emailSender, passwordEmailSender);
                    _smtp.Credentials = _network;

                    //Send Method will send your MailMessage create above.  
                    _smtp.Send(_mailmsg);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
