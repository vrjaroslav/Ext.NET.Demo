using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Services;
using Uber.Web.Attributes;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private IRolesService service { get; set; }

		#region Constructors

		public RolesController()
		{
            service = new RolesService();
		}

        public RolesController(IRolesService service)
		{
            this.service = service;
		}

		#endregion

        #region Actions

        [AuthorizeAction("Role", new[] { "Read" })]
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

        [AuthorizeAction("Role", new[] { "Read" })]
        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<RoleModel> data = Mapper.Map<List<Role>, List<RoleModel>>(service.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        [AuthorizeAction("Role", new[] { "Delete" })]
        public ActionResult Disable(int id)
        {
            service.Disable(id);
            return this.Direct();
        }

        [AuthorizeAction("Role", new[] { "Create", "Update" })]
        public ActionResult Save(RoleModel profile, List<PermissionModel> permissions)
        {
            Dictionary<string, List<string>> r = new Dictionary<string,List<string>>();
            foreach (var item in permissions.Where(item => item.PermissionType != null))
            {
                if (r.ContainsKey(item.ObjectType))
                {
                    r[item.ObjectType].Add(item.PermissionType);
                }
                else
                {
                    r.Add(item.ObjectType, new List<string> { item.PermissionType });
                }
            }

            service.Save(Mapper.Map<RoleModel, Role>(profile), r);

            X.MessageBox.Alert("Success", "Role has been updated").Show();

            return this.Direct();
        }

        #endregion
    }
}
