using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using testtask_v1.Models;
using System.Net.Mail;
using System.Net;

namespace testtask_v1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static int queryCount = 0;

        protected string email = "pasha.vrublevskiy20@list.ru";
        protected void Application_Start()
        {
            Database.SetInitializer(new ProductDbInitializer());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            queryCount++;
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            string messageSubject = "Error";
            string messageBody = ex.Message;
            SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("pasha.vrublevskiy20@list.ru", "ExortinvokeR122");
            client.EnableSsl = true;
            client.Timeout = 10000;
            MailAddress from = new MailAddress(email);
            MailAddress to = new MailAddress(email);
            MailMessage mailMsg = new MailMessage(from, to);
            mailMsg.Body = messageBody;
            mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
            mailMsg.Subject = messageSubject;
            mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Send(mailMsg);

        }
    }
}
