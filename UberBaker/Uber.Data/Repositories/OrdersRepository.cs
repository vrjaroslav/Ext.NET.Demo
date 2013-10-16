using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private UberContext _db { get; set; }

		public OrdersRepository() : this(new UberContext())
		{
		}

		public OrdersRepository(UberContext db)
		{
			this._db = db;
		}

		public Order Get(int id)
		{
			return _db.Orders.SingleOrDefault(p => p.Id == id);
		}


		public IQueryable<Order> GetAll()
		{
			return _db.Orders.Include("OrderItems");
		}

		public Order Add(Order order)
		{
			_db.Orders.Add(order);
			_db.SaveChanges();
			return order;
		}

		public Order Update(Order order)
		{
			_db.Entry(order).State = EntityState.Modified;
			_db.SaveChanges();
			return order;
		}

		public void Delete(int id)
		{
			var o = Get(id);
			_db.Orders.Remove(o);
		}

		public List<OrderChartDataForMonth> GetChartDataForMonth(int month)
		{
			var data = _db.Orders.Where(o => o.OrderDate.Month == month).Select(o => new OrderChartDataForMonth { Day = o.OrderDate.Day, OrdersCount = o.OrderItems.Count }).ToList();
			return data;
		}
	}
}
