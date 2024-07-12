using System;
using Service.Helpers.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Service.Helpers
{
    public class SendEmail : ISendEmail
    {
        public void Send(string from, string displayName, string to, string messageBody, string subject)
        {
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(from, displayName);
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Subject = subject;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("mypathacademy.com@gmail.com", "euek fhkv dwys mqgh");
            smtpClient.Send(mailMessage);
        }
        public SendEmail()
        {
        }
    }
}

