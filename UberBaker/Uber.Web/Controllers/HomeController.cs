using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using StackExchange.Profiling;
using StackExchange.Profiling.MVCHelpers;
using PartialViewResult = Ext.Net.MVC.PartialViewResult;

namespace Uber.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            var profiler = MiniProfiler.Current;
            
		    using (profiler.Step("Render Index Page"))
		    {
		        return this.View();
		    }
		}

		#region PartialViewsRenderingActions

        [ProfilingActionFilter]
        public ActionResult RenderTab(string containerId, string tabId)
        {
            var profiler = MiniProfiler.Current;

            using (profiler.Step("Render Tab Page"))
            {

                switch (tabId)
                {
                    case "ProductsPanel":
                        return RenderProducts(containerId);

                    case "ProductTypesPanel":
                        return RenderProductTypes(containerId);

                    case "CustomersPanel":
                        return RenderCustomers(containerId);

                    case "OrdersPanel":
                        return RenderOrders(containerId);

                    case "OrdersChartPanel":
                        return RenderOrdersChart(containerId);

                    case "UsersPanel":
                        return RenderUsers(containerId);
                }
            }

            return null;
	    }

		public ActionResult RenderProducts(string containerId)
		{
			var result = new Ext.Net.MVC.PartialViewResult
			{
                ViewName = "RenderProducts",
				RenderMode = RenderMode.AddTo,
				ContainerId = containerId,
				WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
			};

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

		    return result;
		}

		public ActionResult RenderProductTypes(string containerId)
		{
            var result = new Ext.Net.MVC.PartialViewResult
			{
                ViewName = "RenderProductTypes",
				RenderMode = RenderMode.AddTo,
				ContainerId = containerId,
				WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
			};

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
		}

		public ActionResult RenderOrders(string containerId)
		{
            var result = new Ext.Net.MVC.PartialViewResult
			{
                ViewName = "RenderOrders",
				RenderMode = RenderMode.AddTo,
				ContainerId = containerId,
				WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
			};

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
		}

		public ActionResult RenderOrderItems(string containerId)
		{
			var result = new Ext.Net.MVC.PartialViewResult
			{
                ViewName = "RenderOrderItems",
				RenderMode = RenderMode.AddTo,
				ContainerId = containerId,
				WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
			};

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
		}

		public ActionResult RenderOrdersChart(string containerId)
		{
            var result = new Ext.Net.MVC.PartialViewResult
			{
                ViewName = "RenderOrdersChart",
				RenderMode = RenderMode.AddTo,
				ContainerId = containerId,
				WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
			};

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
		}

		public ActionResult RenderCustomers(string containerId)
		{
            var profiler = MiniProfiler.Current;
            PartialViewResult result;

		    using (profiler.Step("Render Index Page"))
		    {
		        result = new PartialViewResult
		        {
		            ViewName = "RenderCustomers",
		            RenderMode = RenderMode.AddTo,
		            ContainerId = containerId,
		            WrapByScriptTag = false
		            // we load the view via Loader with Script mode therefore script tags is not required
		        };

		        this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();
		    }
		    return result;
		}

		public ActionResult RenderUsers(string containerId)
		{
            var result = new Ext.Net.MVC.PartialViewResult
			{
                ViewName = "RenderUsers",
				RenderMode = RenderMode.AddTo,
				ContainerId = containerId,
				WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
			};

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
		}

		#endregion
	}	
}
