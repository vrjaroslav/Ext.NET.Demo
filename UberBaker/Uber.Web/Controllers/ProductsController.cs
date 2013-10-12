using System.Linq;
using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class ProductsController : Controller
    {
		private ProductsRepository _productsRepository { get; set; }

		public ProductsController()
		{
			_productsRepository = new ProductsRepository();
		}
		
		public ProductsController(IProductsRepository productsRepository)
		{
			// TODO Rewite with IoC
			this._productsRepository = new ProductsRepository();
		}

		public ActionResult Save(Product product)
		{
			if (product.Id > 0)
				_productsRepository.Update(product);
			else
				_productsRepository.Add(product);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			_productsRepository.Delete(id);
			return this.Direct();
		}

		public ActionResult GetAll()
		{
			return this.Store(_productsRepository.GetAll());
		}

        public ActionResult Index()
        {
            return View();
        }

		//public ActionResult HandleChanges(StoreDataHandler handler)
		//{
		//	ChangeRecords<Product> products = handler.BatchObjectData<Product>();
		//	var store = this.GetCmp<Store>(this.Request.Params["storeId"]);

		//	foreach (Product created in products.Created)
		//	{
		//		_productsRepository.Add(created);

		//		var record = store.GetByInternalId(created.PhantomId);
		//		record.CreateVariable = true;
		//		record.SetId(created.Id);
		//		record.Commit();
		//		created.PhantomId = null;
		//	}

		//	foreach (Product deleted in products.Deleted)
		//	{
		//		_productsRepository.Delete(deleted.Id);
		//		store.CommitRemoving(deleted.Id);
		//	}

		//	foreach (Product updated in products.Updated)
		//	{
		//		_productsRepository.Update(updated);

		//		var record = store.GetById(updated.Id);
		//		record.Commit();
		//	}

		//	return this.Direct();
		//}

    }
}
