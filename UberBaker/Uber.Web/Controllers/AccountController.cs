using System.Web.Mvc;
using Uber.Web.Models;
using WebMatrix.WebData;

namespace Uber.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
        #region Actions

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

            return View(model);
		}

		public ActionResult LogOff()
		{
			WebSecurity.Logout();

            return this.RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
