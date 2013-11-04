﻿using Ext.Net;
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
        private IOrdersRepository repository { get; set; }

		#region Constructors

		public OrdersController()
		{
			repository = new OrdersRepository();
		}
		
		public OrdersController(IOrdersRepository repository)
		{
			// TODO Rewite with IoC
			this.repository = new OrdersRepository();
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

        public ActionResult ReadData(StoreRequestParameters parameters)
        {
            var data = repository.GetAll().ToList();

            return this.Store(data.SortFilterPaged(parameters), data.Count);
        }

		public ActionResult GetChartDataForCurrentMonth(int month = 10)
	    {
			var data = repository.GetAll().Where(o => o.OrderDate.Month == month).Select(o => new OrderChartDataForMonth { Day = o.OrderDate.Day, OrdersCount = o.Quantity }).ToList();

		    return new StoreResult(data);
	    }

		#endregion
    }
}
