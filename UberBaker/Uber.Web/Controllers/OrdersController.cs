using System;
using System.Collections.Generic;
using Ext.Net;
using Ext.Net.MVC;
using System.Linq;
using System.Web.Mvc;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    public class OrdersController : Controller
    {
        private IBaseRepository<Order> repository { get; set; }
        private IBaseRepository<ProductType> productTypesRepository { get; set; }

		#region Constructors

		public OrdersController()
		{
			repository = new OrdersRepository();
            productTypesRepository = new ProductTypesRepository();
		}

        public OrdersController(IBaseRepository<Order> repository, IBaseRepository<ProductType> productTypesRepository)
		{
			// TODO Rewite with IoC
			this.repository = new OrdersRepository();
            this.productTypesRepository = new ProductTypesRepository();
		}

		#endregion

		#region Actions

		public ActionResult Save(Order order)
		{
			repository.AddOrUpdate(order);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
            var o = repository.Get(id);
            o.Disabled = true;
            repository.Update(o);

			return this.Direct();
		}

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            var data = repository.GetAll().ToList();

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        public ActionResult GetChartData(DateTime? startDate, DateTime? endDate)
	    {
            if (startDate == null)
                startDate = DateTime.Now.AddDays(-31).Date;

            if (endDate == null)
                endDate = DateTime.Now.Date;

            var data = repository.GetAll()
                .Where(o => (o.OrderDate > startDate && o.OrderDate < endDate)).GroupBy(o => o.OrderDate)
                .Select(g => new OrderChartDataForMonth { Day = g.Key, OrdersCount = g.Sum(o => o.Quantity) })
                .ToDictionary(o => o.Day.Date, o => o.OrdersCount);

            List<OrderChartDataForMonth> result = new List<OrderChartDataForMonth>();
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
            var data = repository.GetAll()
                .Where(o => (o.OrderDate > startDate && o.OrderDate < endDate))
                .GroupBy(o => o.Product.ProductType)
                .Select(group => new { ProductTypeName = group.Key.Name, OrdersCount = group.Sum(o => o.Quantity) })
                .ToList();

		    return new StoreResult(data);
	    }

		#endregion
    }
}
