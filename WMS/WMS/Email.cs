using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace WMS
{
    class Email
    {
        public static bool Sent(string EmailAdress, string Message)
        {
            MailAddress from, to;
            from = new MailAddress("18721711827@163.com", "系统管理员");
            to = new MailAddress(EmailAdress, "user");
            NetworkCredential account = new NetworkCredential("18721711827@163.com", "19961018cym");
            MailMessage mail = new MailMessage()
            {
                From = from,
                Subject = "您正注册仓储管理系统",
                Body = "验证码为：" + Message,
                BodyEncoding = Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
            };
            mail.To.Add(to);
            SmtpClient client = new SmtpClient("smtp.163.com")
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = account
            };

            client.Send(mail);
            return true;
        }
    }
}