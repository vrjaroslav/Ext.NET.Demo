using System.Web.Mvc;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class ProductTypesController : Controller
    {
		private ProductTypesRepository _productsTypesRepository { get; set; }

		public ProductTypesController()
		{
			_productsTypesRepository = new ProductTypesRepository();
		}
		
		public ProductTypesController(IProductTypesRepository productsTypesRepository)
		{
			// TODO Rewite with IoC
			this._productsTypesRepository = new ProductTypesRepository();
		}

        public ActionResult GetAll()
        {
	        return this.Store(_productsTypesRepository.GetAll());
        } 

		public ActionResult Save(ProductType productType)
		{
			if (productType.Id > 0)
				_productsTypesRepository.Update(productType);
			else
				_productsTypesRepository.Add(productType);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			_productsTypesRepository.Delete(id);
			return this.Direct();
		}

		public ActionResult Index()
		{
			return View();
		}
    }
}
