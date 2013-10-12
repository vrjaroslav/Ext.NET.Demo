using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class OrdersController : Controller
    {
        private IOrdersRepository _ordersRepository { get; set; }

		public OrdersController()
		{
			_ordersRepository = new OrdersRepository();
		}
		
		public OrdersController(IOrdersRepository _ordersRepository)
		{
			// TODO Rewite with IoC
			this._ordersRepository = new OrdersRepository();
		}

		public ActionResult Save(Order order)
		{
			if (order.Id > 0)
				_ordersRepository.Update(order);
			else
				_ordersRepository.Add(order);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			_ordersRepository.Delete(id);
			return this.Direct();
		}

		public ActionResult GetAll()
		{
			return this.Store(_ordersRepository.GetAll());
		}

        public ActionResult Index()
        {
            return View();
        }

    }
}
