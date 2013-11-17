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
    public class ProductsController : Controller
    {
        private IBaseRepository<Product> repository { get; set; }

        #region Constructors

        public ProductsController()
        {
            repository = new ProductsRepository();
        }

        public ProductsController(IBaseRepository<Product> repository)
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
            var p = repository.Get(id);
            p.Disabled = true;
            repository.Update(p);

            return this.Direct();
        }

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<Product> dataFromRepo = repository.GetAll().ToList();

            List<ProductModel> data = Mapper.Map<List<Product>, List<ProductModel>>(dataFromRepo);

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        #endregion
    }
}
