using System.Web.Mvc;
using System.Web.Routing;

namespace Uber.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			Route reportRoute = new Route("{controller}/{action}/{id}", new MvcRouteHandler())
			{
			    Defaults = new RouteValueDictionary {{"controller", "Home"}, {"action", "Index"}, {"id", (string) null}}
			};
		    routes.Add(reportRoute);
		}
	}
}