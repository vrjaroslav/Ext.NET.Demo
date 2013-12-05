using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Web.Attributes;

namespace Uber.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        [AuthorizeAction("User", new[] { "Read" })]
        public ActionResult Index(string containerId)
        {
            var result = new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false
            };

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
        }
    }
}
