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
    public class ProductsController : Controller
    {
        private IProductsService productService { get; set; }

        #region Constructors

        public ProductsController()
        {
            productService = new ProductsService();
        }

        public ProductsController(IProductsService service)
        {
            this.productService = service;
        }

        #endregion

        #region Actions

        public ActionResult Index(string containerId)
        {
            var result = new Ext.Net.MVC.PartialViewResult
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
        }

        public ActionResult Save(ProductModel product)
        {
            productService.Save(Mapper.Map<ProductModel, Product>(product));

            return this.Direct();
        }

        public ActionResult Delete(int id)
        {
            productService.Delete(id);

            return this.Direct();
        }

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<ProductModel> data = Mapper.Map<List<Product>, List<ProductModel>>(productService.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        #endregion
    }
}
