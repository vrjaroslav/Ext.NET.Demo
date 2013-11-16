using System.Linq;
using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;

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

        public ActionResult ReadData(StoreRequestParameters parameters)
        {
            var data = repository.GetAll().ToList();

            return this.Store(data.SortFilterPaged(parameters), data.Count);
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
