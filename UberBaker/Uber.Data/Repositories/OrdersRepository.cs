using System.Collections.Generic;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public OrdersRepository() : this(new UberContext())
		{
		}

		public OrdersRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public Order Get(int id)
		{
			return DbContext.Orders.SingleOrDefault(p => p.Id == id);
		}


		public IQueryable<Order> GetAll()
		{
			return DbContext.Orders.Include("Customer");
		}

		public Order Add(Order order)
		{
			DbContext.Orders.Add(order);
			DbContext.SaveChanges();
			return order;
		}

		public Order Update(Order order)
		{
			DbContext.Entry(order).State = EntityState.Modified;
			DbContext.SaveChanges();
			return order;
		}

		public void Delete(int id)
		{
			var o = Get(id);
			DbContext.Orders.Remove(o);
		}

		public Order AddOrUpdate(Order order)
		{
			return order.IsNew ? this.Add(order) : this.Update(order);
		}

		#endregion
	}
}
