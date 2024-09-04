using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using NLog;
using NLog.Web;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            logger.Debug("Application starting");
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            logger.Error(exception, "Unhandled exception occurred");
        }
    }
}
