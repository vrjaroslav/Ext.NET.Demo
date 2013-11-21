using System;
using System.Collections.Generic;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using System.Linq;
using System.Web.Mvc;
using Uber.Core;
using Uber.Services;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    public class OrdersController : Controller
    {
        private IOrdersService service { get; set; }

		#region Constructors

		public OrdersController()
		{
            service = new OrdersService();
		}

        public OrdersController(IOrdersService service)
		{
            this.service = service;
		}

		#endregion

		#region Actions

		public ActionResult Save(OrderModel order)
		{
            service.Save(Mapper.Map<OrderModel, Order>(order));

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
            service.Delete(id);

			return this.Direct();
		}

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<OrderModel> data = Mapper.Map<List<Order>, List<OrderModel>>(service.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

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

		#endregion
    }
}
