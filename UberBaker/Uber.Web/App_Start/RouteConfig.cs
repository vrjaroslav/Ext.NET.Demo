using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Routing;

namespace Uber.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
			
			//routes.Add("Default", new Route("{controller}/{action}/{id}", new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional }), ));

			Route reportRoute = new Route("{controller}/{action}/{id}", new MvcRouteHandler());
			reportRoute.Defaults = new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" }, { "id", (string)null } };
			routes.Add(reportRoute);
		}
	}
}