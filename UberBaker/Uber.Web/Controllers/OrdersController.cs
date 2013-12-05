using System;
using System.Collections.Generic;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using System.Linq;
using System.Web.Mvc;
using Uber.Core;
using Uber.Services;
using Uber.Web.Attributes;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrdersService service { get; set; }

		#region Constructors

		public OrdersController()
		{
            service = new OrdersService();
		}

        public OrdersController(IOrdersService service, IProductTypesService productTypeService)
		{
            this.service = service;
		}

		#endregion

		#region Actions

        [AuthorizeAction("Order", new[] { "Create", "Update" })]
		public ActionResult Save(OrderModel order)
		{
            service.Save(Mapper.Map<OrderModel, Order>(order));

			return this.Direct();
		}

        [AuthorizeAction("Order", new[] { "Delete" })]
		public ActionResult Delete(int id)
		{
            service.Delete(id);

			return this.Direct();
		}

        [AuthorizeAction("Order", new[] { "Read" })]
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

        public ActionResult Chart(string containerId)
        {
            var result = new Ext.Net.MVC.PartialViewResult("Chart")
            {
                RenderMode = RenderMode.AddTo,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
        }

        [AuthorizeAction("Order", new[] { "Read" })]
        public ActionResult ChartLast31Days()
        {
            return this.PartialView("ChartLast31Days");
        }

        [AuthorizeAction("Order", new[] { "Read" })]
        public ActionResult ChartByTypeLast31Days()
        {
            return this.PartialView("ChartByTypeLast31Days");
        }

        #endregion


        #region Data actions

        [AuthorizeAction("Order", new[] { "Read" })]
        public ActionResult GetChartData(DateTime? startDate, DateTime? endDate)
	    {
            if (startDate == null)
                startDate = DateTime.Now.AddDays(-31).Date;

            if (endDate == null)
                endDate = DateTime.Now.Date;

            var data = service.GetAllByDate(startDate.Value, endDate.Value)
                .ToList()
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new OrderChartDataForMonth { Day = g.Key, OrdersCount = g.Sum(o => o.Quantity) })
                .ToDictionary(o => o.Day, o => o.OrdersCount);

            // TODO Rewrite with AutoMapper
            var result = new List<OrderChartDataForMonth>();
            for (DateTime i = startDate.Value; i < endDate; i = i.AddDays(1))
            {
                result.Add(new OrderChartDataForMonth { Day = i, OrdersCount = data.ContainsKey(i) ? data[i] : 0 });
            }

            return new StoreResult(result);
	    }

        [AuthorizeAction("Order", new[] { "Read" })]
        public ActionResult GetDataLast31DaysByType()
	    {
            DateTime startDate = DateTime.Now.AddDays(-31),
                endDate = DateTime.Now;
            var data = service.GetAllByDate(startDate, endDate)
                .GroupBy(o => o.Product.ProductType)
                .Select(group => new { ProductTypeName = group.Key.Name, OrdersCount = group.Sum(o => o.Quantity) })
                .ToList();

		    return new StoreResult(data);
	    }

        [AuthorizeAction("Order", new[] { "Read" })]
        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<OrderModel> data = Mapper.Map<List<Order>, List<OrderModel>>(service.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }
        
		#endregion
    }
}
