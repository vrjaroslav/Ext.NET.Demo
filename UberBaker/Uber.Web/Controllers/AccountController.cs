using System.Linq;
using Ext.Net;
using Ext.Net.MVC;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Models;
using WebMatrix.WebData;

namespace Uber.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UsersRepository repository { get; set; }

		#region Constructors

		public AccountController()
		{
			repository = new UsersRepository();
		}

		public AccountController(IUsersRepository repository)
		{
			// TODO Rewite with IoC
			this.repository = new UsersRepository();
		}

		#endregion

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
				return this.RedirectToAction("Index", "Home");
			}

            return this.View(model);
		}

		public ActionResult LogOff()
		{
			WebSecurity.Logout();

            return this.RedirectToAction("Index", "Home");
		}

		//
		// GET: /Account/Register

		[AllowAnonymous]
		public ActionResult Register()
		{
            return this.View();
		}

		//
		// POST: /Account/Register

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				// Attempt to register the user
				try
				{
					WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
					WebSecurity.Login(model.UserName, model.Password);

                    return this.RedirectToAction("Index", "Home");
				}
				catch (MembershipCreateUserException e)
				{
                    this.ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
				}
			}

			// If we got this far, something failed, redisplay form
            return this.View(model);
		}

		//
		// POST: /Account/Disassociate

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Disassociate(string provider, string providerUserId)
		{
			string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
			ManageMessageId? message = null;

			// Only disassociate the account if the currently logged in user is the owner
			if (ownerAccount == User.Identity.Name)
			{
				// Use a transaction to prevent the user from deleting their last login credential
				using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
				{
					bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));

					if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
					{
						OAuthWebSecurity.DeleteAccount(provider, providerUserId);
						scope.Complete();
						message = ManageMessageId.RemoveLoginSuccess;
					}
				}
			}

			return RedirectToAction("Manage", new { Message = message });
		}

		//
		// GET: /Account/Manage

		public ActionResult Manage(ManageMessageId? message)
		{
            this.ViewBag.StatusMessage =
				message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
				: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
				: message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
				: "";
            this.ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            this.ViewBag.ReturnUrl = Url.Action("Manage");

            return this.View();
		}

		//
		// POST: /Account/Manage

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Manage(LocalPasswordModel model)
		{
			bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            this.ViewBag.HasLocalPassword = hasLocalAccount;
            this.ViewBag.ReturnUrl = Url.Action("Manage");

			if (hasLocalAccount)
			{
				if (ModelState.IsValid)
				{
					// ChangePassword will throw an exception rather than return false in certain failure scenarios.
					bool changePasswordSucceeded;
					try
					{
						changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
					}
					catch (Exception)
					{
						changePasswordSucceeded = false;
					}

					if (changePasswordSucceeded)
					{
                        return this.RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
					}
					else
					{
                        this.ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
					}
				}
			}
			else
			{
				// User does not have a local password so remove any validation errors caused by a missing
				// OldPassword field
				ModelState state = ModelState["OldPassword"];
				if (state != null)
				{
					state.Errors.Clear();
				}

                if (this.ModelState.IsValid)
				{
					try
					{
						WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);

						return this.RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
					}
					catch (Exception)
					{
						this.ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
					}
				}
			}

			// If we got this far, something failed, redisplay form
			return this.View(model);
		}

        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
		{
			if (this.Url.IsLocalUrl(returnUrl))
			{
				return this.Redirect(returnUrl);
			}
			else
			{
				return this.RedirectToAction("Index", "Home");
			}
		}

		public enum ManageMessageId
		{
			ChangePasswordSuccess,
			SetPasswordSuccess,
			RemoveLoginSuccess,
		}

		internal class ExternalLoginResult : ActionResult
		{
			public ExternalLoginResult(string provider, string returnUrl)
			{
				this.Provider = provider;
				this.ReturnUrl = returnUrl;
			}

			public string Provider { get; private set; }
			public string ReturnUrl { get; private set; }

			public override void ExecuteResult(ControllerContext context)
			{
				OAuthWebSecurity.RequestAuthentication(this.Provider, this.ReturnUrl);
			}
		}

		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}
		#endregion
	}
}
