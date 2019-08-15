using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading;

namespace testtask_v1.Models
{
    public class IdentityEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            
            MailAddress from = new MailAddress(
                        "pasha.vrublevskiy20@list.ru",
                        "Email Confirmation");
            MailAddress to = new MailAddress(message.Destination);
           
            MailMessage msg = new MailMessage(from, to);

            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.mail.ru", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                "pasha.vrublevskiy20@list.ru",
                "ExortinvokeR122"),
                EnableSsl = true,
                Timeout = 10000
            };
            ParameterizedThreadStart start = new ParameterizedThreadStart(
                m => client.Send((MailMessage)m));
            Thread thread = new Thread(start);
            thread.Start(msg);
            return Task.FromResult(0);
            
        }
    }
}