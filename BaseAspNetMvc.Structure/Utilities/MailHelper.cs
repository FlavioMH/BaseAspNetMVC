using log4net;
using System;      
using System.Net;
using System.Net.Mail;   
using System.Threading.Tasks;  

namespace BaseAspNetMvc.Structure.Utilities
{
    public class MailHelper
    {
        /// <summary>
        /// Sends a mail through external provider.
        /// </summary>
        /// <param name="accountUsername">Username with which to authenticate to the external provider.</param>
        /// <param name="accountPassword">Password with which to authenticate to the external provider.</param>
        /// <param name="accountHost">SMTP server responsible for mail sending.</param>
        /// <param name="accountPort">SMTP port number.</param>
        /// <param name="enableSSL">Enable or not SSL.</param>
        /// <param name="from">Mail address from which the mail should be sent.</param>
        /// <param name="fromDisplayName">Name to be displayed as "From" contact.</param>
        /// <param name="to"></param>
        /// <param name="subject">Mail subject.</param>
        /// <param name="body">Mail body.</param>
        /// <param name="isBodyHtml">Mail body is HTML format.</param>
        /// <returns></returns>
        public async Task<bool> SendMessage(string accountUsername, string accountPassword, string accountHost, int accountPort,
            bool enableSSL, string from, string fromDisplayName, string to, string subject, string body, bool isBodyHtml)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(from, from);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isBodyHtml;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = accountUsername,
                        Password = accountPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = accountHost;
                    smtp.Port = accountPort;
                    smtp.EnableSsl = enableSSL;
                    await smtp.SendMailAsync(message);

                    return true;
                }
            }
            catch(Exception e)
            {
                LogManager.GetLogger
                    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error("MailHelper.SendMessage Exception", e);
                return false;  
            }

        }
    }
}
