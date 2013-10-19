using System.Web.Mvc;
using Ext.Net;

namespace Uber.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		#region PartialViewsRenderingActions

		public ActionResult RenderProducts(string containerId)
		{
			return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };
		}

		public ActionResult RenderProductTypes(string containerId)
		{
			return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };
		}

		public ActionResult RenderOrders(string containerId)
		{
			return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };
		}

		public ActionResult RenderOrderItems(string containerId)
		{
			return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };
		}

		public ActionResult RenderOrdersChart(string containerId)
		{
			return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };
		}

		public ActionResult RenderCustomers(string containerId)
		{
			return new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };
		}

		#endregion
	}
}
