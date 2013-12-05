using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Services.Services;

namespace Uber.Web.Attributes
{
    public class AuthorizeActionAttribute : AuthorizeAttribute
    {
        private string objectType;
        private string[] permissionTypes;

        private IPermissionsService service { get; set; }

		#region Constructors

        public AuthorizeActionAttribute(string objectType, string[] permissionType)
        {
            this.objectType = objectType;
            this.permissionTypes = permissionType;
            this.service = new PermissionsService();
        }

		#endregion

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            foreach (string permissionType in permissionTypes)
            {
                if (service.CheckPermission(httpContext.User.Identity.Name, objectType, permissionType))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Access Error",
                    Message = "You don't have required permissions for object " + this.objectType,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });
                filterContext.Result = new DirectResult();
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Account",
                            action = "Login"
                        })
                    );
            }
        }
    }
}