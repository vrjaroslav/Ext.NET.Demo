using System.Web.Mvc;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class ProductTypesController : Controller
    {
		private ProductTypesRepository repository { get; set; }

		#region Constructors

		public ProductTypesController()
		{
			repository = new ProductTypesRepository();
		}
		
		public ProductTypesController(IProductTypesRepository repository)
		{
			// TODO Rewite with IoC
			this.repository = new ProductTypesRepository();
		}

		#endregion

		#region Actions

        public ActionResult GetAll()
        {
	        return this.Store(repository.GetAll());
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
