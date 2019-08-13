using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;

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

            SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(
                "pasha.vrublevskiy20@list.ru",
                "ExortinvokeR122");
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.Send(msg);
            return Task.FromResult(0);
        }
    }
}