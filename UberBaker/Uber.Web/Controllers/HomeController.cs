using System.Web.Mvc;

namespace Uber.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            return this.View();
		}
	}	
}
