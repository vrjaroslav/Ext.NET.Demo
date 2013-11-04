using System.Collections.Generic;
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

        public ActionResult ReadData(StoreRequestParameters parameters)
        {
            var data = repository.GetAll().ToList();

            return this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        #endregion
    }
}
