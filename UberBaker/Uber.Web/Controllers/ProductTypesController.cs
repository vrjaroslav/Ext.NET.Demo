using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    public class ProductTypesController : Controller
    {
        private IBaseRepository<ProductType> repository { get; set; }

		#region Constructors

		public ProductTypesController()
		{
			repository = new ProductTypesRepository();
		}

        public ProductTypesController(IBaseRepository<ProductType> repository)
		{
			// TODO Rewite with IoC
			this.repository = new ProductTypesRepository();
		}

		#endregion

		#region Actions

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<ProductType> dataFromRepo = repository.GetAll().ToList();

            List<ProductTypeModel> data = Mapper.Map<List<ProductType>, List<ProductTypeModel>>(dataFromRepo);

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

		public ActionResult Save(ProductType productType)
		{
			repository.AddOrUpdate(productType);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			repository.Delete(id);
			return this.Direct();
		}

		#endregion
    }
}
