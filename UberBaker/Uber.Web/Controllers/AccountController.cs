using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Web.Attributes;
using Uber.Web.Models;
using WebMatrix.WebData;

namespace Uber.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
        #region Actions

        [AuthorizeAction("User", new[] { "Read" })]
        public ActionResult Index()
		{
            return this.View();
		}

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
            this.ViewBag.ReturnUrl = returnUrl;

            return this.View(new LoginModel
            {
                UserName = "admin",
                Password = "demo"
            });
		}

		//
		// POST: /Account/Login

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
			{
                return RedirectToAction("Index", "Home");
			}

            X.Msg.Alert("Authentification Error", "Provided wrong login or password").Show();

            return this.Direct();
		}

		public ActionResult LogOff()
		{
			WebSecurity.Logout();

            return this.RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
