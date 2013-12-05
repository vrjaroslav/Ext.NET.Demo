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
    public class ProductTypesController : Controller
    {
        private IProductTypesService service { get; set; }

		#region Constructors

		public ProductTypesController()
		{
            service = new ProductTypesService();
		}

        public ProductTypesController(IProductTypesService service)
		{
            this.service = service;
		}

		#endregion

		#region Actions

        [AuthorizeAction("ProductType", new[] { "Read" })]
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

        [AuthorizeAction("ProductType", new[] { "Read" })]
        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<ProductType> dataFromRepo = service.GetAll();

            List<ProductTypeModel> data = Mapper.Map<List<ProductType>, List<ProductTypeModel>>(dataFromRepo);

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        [AuthorizeAction("ProductType", new[] { "Create", "Update" })]
		public ActionResult Save(ProductTypeModel productType)
		{
            service.Save(Mapper.Map<ProductTypeModel, ProductType>(productType));

			return this.Direct();
		}

        [AuthorizeAction("ProductType", new[] { "Delete" })]
		public ActionResult Delete(int id)
		{
            service.Delete(id);
			return this.Direct();
		}

		#endregion
    }
}
