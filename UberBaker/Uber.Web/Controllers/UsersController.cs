using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;

namespace Uber.Web.Controllers
{
    public class UsersController : Controller
    {
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
