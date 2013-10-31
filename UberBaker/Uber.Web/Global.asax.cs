﻿using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using StackExchange.Profiling;
using Uber.Data;

namespace Uber.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
            UberContextInitializer initializer = new UberContextInitializer();
			Database.SetInitializer(initializer);

			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			AuthConfig.RegisterAuth();

            // Profiler
            GlobalFilters.Filters.Add(new StackExchange.Profiling.MVCHelpers.ProfilingActionFilter());
            MiniProfilerEF.Initialize();
		}

	    protected void Application_BeginRequest()
	    {
            MiniProfiler.Start();
	    }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
	}
}