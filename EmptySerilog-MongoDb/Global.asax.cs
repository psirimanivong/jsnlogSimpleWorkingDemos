﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Serilog;
using Serilog.Formatting.Raw;
using Serilog.Sinks.IOFile;
using System.IO;

namespace EmptySerilog_MongoDb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // -----------------

            string logFilePath = Path.Combine(Path.GetTempPath(), "log.txt");

            var log = new LoggerConfiguration()
                            .WriteTo.MongoDB("mongodb://localhost/logs", period: TimeSpan.Zero)
                            .WriteTo.Sink(new FileSink(logFilePath, new RawFormatter(), null))
                            .CreateLogger();

            Log.Logger = log;
        }
    }
}