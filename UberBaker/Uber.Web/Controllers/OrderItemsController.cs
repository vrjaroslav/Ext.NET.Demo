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
    public class OrderItemsController : Controller
    {
        private IOrderItemsRepository _orderItemsRepository { get; set; }

		public OrderItemsController()
		{
			_orderItemsRepository = new OrderItemsRepository();
		}
		
		public OrderItemsController(IOrderItemsRepository orderItemsRepository)
		{
			// TODO Rewite with IoC
			this._orderItemsRepository = new OrderItemsRepository();
		}

		public ActionResult Save(OrderItem orderItem)
		{
			if (orderItem.Id > 0)
				_orderItemsRepository.Update(orderItem);
			else
				_orderItemsRepository.Add(orderItem);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			_orderItemsRepository.Delete(id);
			return this.Direct();
		}

		public ActionResult GetAll()
		{
			return this.Store(_orderItemsRepository.GetAll());
		}

        public ActionResult Index()
        {
            return View();
        }

    }
}
