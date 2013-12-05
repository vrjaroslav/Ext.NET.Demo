using System.Web.Mvc;
using System.Web.Security;
using Uber.Services.Services;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        private IPermissionsService service { get; set; }

		#region Constructors

        public PermissionsController()
		{
            service = new PermissionsService();
		}

        public PermissionsController(IPermissionsService service)
		{
            this.service = service;
		}

		#endregion

        #region Actions

        public ActionResult AppMenu()
        {
            return this.PartialView();
        }

        public ActionResult LeftAppMenu()
        {
            return this.PartialView(new LeftAppMenuModel
            {
                AllowReadProducts = service.CheckPermission(Membership.GetUser().UserName, "Product", "Read"),
                AllowReadProductTypes = service.CheckPermission(Membership.GetUser().UserName, "ProductType", "Read"),
                AllowReadCustomers = service.CheckPermission(Membership.GetUser().UserName, "Customer", "Read"),
                AllowReadOrders = service.CheckPermission(Membership.GetUser().UserName, "Order", "Read"),
                AllowReadUsers = service.CheckPermission(Membership.GetUser().UserName, "User", "Read"),
                AllowReadRoles = service.CheckPermission(Membership.GetUser().UserName, "Role", "Read"),
            });
        }

        public ActionResult GridPanelTopToolbar(string typeName)
        {
            return this.PartialView(new GridPanelTopToolbarModel
            {
                AddButtonAvailable = service.CheckPermission(Membership.GetUser().UserName, typeName, "Create"),
                DeleteButtonAvailable = service.CheckPermission(Membership.GetUser().UserName, typeName, "Delete"),
                ObjectType = typeName
            });
        }
        
        public ActionResult FormPanelTopToolbar(string typeName)
        {
            return this.PartialView(new FormPanelTopToolbarModel
            {
                SaveButtonAvailable = service.CheckPermission(Membership.GetUser().UserName, typeName, "Create") || service.CheckPermission(Membership.GetUser().UserName, typeName, "Update"),
                ObjectType = typeName
            });
        }

        public ActionResult GetPermissionsCheckboxGroup(int? id, string containerId)
        {
            var result = new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = Ext.Net.RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false,
                Model = service.GetSecurableDictionaryByRoleId(id.Value)
            };

            return result;
        }

        #endregion

    }
}
