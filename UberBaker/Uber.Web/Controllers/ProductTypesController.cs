using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Services;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
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

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<ProductType> dataFromRepo = service.GetAll();

            List<ProductTypeModel> data = Mapper.Map<List<ProductType>, List<ProductTypeModel>>(dataFromRepo);

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

		public ActionResult Save(ProductTypeModel productType)
		{
            service.Save(Mapper.Map<ProductTypeModel, ProductType>(productType));

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
            service.Delete(id);
			return this.Direct();
		}

		#endregion
    }
}
