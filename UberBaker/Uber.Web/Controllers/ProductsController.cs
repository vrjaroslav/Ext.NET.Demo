using System.Web.Mvc;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class ProductsController : Controller
    {
		private ProductsRepository repository { get; set; }

		#region Constructors

		public ProductsController()
		{
			repository = new ProductsRepository();
		}
		
		public ProductsController(IProductsRepository repository)
		{
			// TODO Rewite with IoC
			this.repository = new ProductsRepository();
		}

		#endregion

		#region Actions

		public ActionResult Save(Product product)
		{
			repository.AddOrUpdate(product);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			repository.Delete(id);

			return this.Direct();
		}

		public ActionResult GetAll()
		{
			return this.Store(repository.GetAll());
		}

        public ActionResult Index()
        {
            return this.View();
        }

		#endregion
    }
}
