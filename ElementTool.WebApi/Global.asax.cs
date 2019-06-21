using Hangfire;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ElementTool.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        private BackgroundJobServer _backgroundJobServer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            string firebaseUrl = ConfigurationManager.AppSettings["firebaseUrl"];
            string authSecret = ConfigurationManager.AppSettings["authSecret"];

            Hangfire.Firebase.FirebaseStorage firebaseStorage = new Hangfire.Firebase.FirebaseStorage(firebaseUrl, authSecret);
            Hangfire.GlobalConfiguration.Configuration.UseStorage(firebaseStorage);

            JobStorage.Current = firebaseStorage;

            _backgroundJobServer = new BackgroundJobServer();

        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
        protected void Application_End(object sender, EventArgs e)
        {
            _backgroundJobServer.Dispose();
        }
    }
}
