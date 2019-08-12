using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using testtask_v1.Models;
using testtask_v1.Binders;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace testtask_v1
{


    public class MvcApplication : System.Web.HttpApplication
    {
        public static int queryCount = 0;
        protected string email = "pasha.vrublevskiy20@list.ru";
        protected void Application_Start()
        {
            App_Start.FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Database.SetInitializer(new ProductDbInitializer());
            Logger.Logger.InitLogger();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(ShoppingCart<Product>), new CartModelBinder());
        }

        protected void Application_BeginRequest()
        {
            queryCount++;
        }


        protected void Application_Error()
        {
            //Exception ex = Server.GetLastError();

            //string messageSubject = "Error";
            //string messageBody = ex.Message;
            //string logMsg = "Error: ";
            //logMsg += Environment.NewLine + "Query Url: " + Request.Url.PathAndQuery;
            //logMsg += Environment.NewLine + "Query headers: " + Environment.NewLine;
            //int i = 0;
            //foreach(var header in Request.Headers)
            //{
            //    logMsg += "    " + i++ + ": " + header + Environment.NewLine; 
            //}
            //logMsg += "Request type: " + Request.RequestType + Environment.NewLine;
            //logMsg += "Exception name: " + ex.GetType() + Environment.NewLine;
            //logMsg += "Stack Trace: " + ex.StackTrace + Environment.NewLine;
            //Logger.Logger.Log.Error(logMsg);
            //Logger.LoggerDb.Log.Error(Request.Url.PathAndQuery, ex);
            //string str = System.Configuration.ConfigurationManager.ConnectionStrings["dblog"].ConnectionString;
            //string str1 = "INSERT INTO Log([Date], [Thread], [Level], [Logger], [Message], [Exception]) VALUES(@log_date, @thread, @log_level, @logger, @message, @exception)";

            //using(SqlConnection conn = new SqlConnection(str))
            //{
            //    conn.Open();
            //    using (SqlCommand comm = new SqlCommand(str1, conn))
            //    {
            //        comm.Parameters.Add("@log_date", SqlDbType.DateTime);
            //        comm.Parameters.Add("@thread", SqlDbType.VarChar);
            //        comm.Parameters.Add("@log_level", SqlDbType.VarChar);
            //        comm.Parameters.Add("@log_level", SqlDbType.VarChar);
            //        comm.Parameters.Add("@log_level", SqlDbType.VarChar);
            //        comm.Parameters.Add("@log_level", SqlDbType.VarChar);
            //    }
            //}
            //SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("", "");
            //client.EnableSsl = true;
            //client.Timeout = 10000;

            //MailAddress from = new MailAddress(email);
            //MailAddress to = new MailAddress(email);
            //MailMessage mailMsg = new MailMessage(from, to);
            //mailMsg.Body = messageBody;
            //mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
            //mailMsg.Subject = messageSubject;
            //mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //client.Send(mailMsg);

        }
    }
}
