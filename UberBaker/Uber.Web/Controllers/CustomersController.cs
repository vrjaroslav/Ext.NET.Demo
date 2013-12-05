using System.Collections.Generic;
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
    public class CustomersController : Controller
    {
        private ICustomersService service { get; set; }

		#region Constructors

		public CustomersController()
		{
            this.service = new CustomersService();
		}

        public CustomersController(ICustomersService service)
		{
            this.service = service;
		}

		#endregion

		#region Actions

        [AuthorizeAction("Customer", new[] { "Read" })]
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

        [AuthorizeAction("Customer", new[] { "Create", "Update" })]
        public ActionResult Save(CustomerModel customer)
		{
            service.Save(Mapper.Map<CustomerModel, Customer>(customer));

			return this.Direct();
		}

        [AuthorizeAction("Customer", new[] { "Delete" })]
		public ActionResult Delete(int id)
		{
            service.Delete(id);

			return this.Direct();
		}

        [AuthorizeAction("Customer", new[] { "Read" })]
        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<CustomerModel> data = Mapper.Map<List<Customer>, List<CustomerModel>>(service.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

		#endregion
	}
}
