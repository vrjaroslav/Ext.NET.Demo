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
			return this.DbContext.Orders.SingleOrDefault(p => p.Id == id);
		}


		public IQueryable<Order> GetAll()
		{
			return this.DbContext.Orders.Include("Customer");
		}

		public Order Add(Order order)
		{
			this.DbContext.Orders.Add(order);
			this.DbContext.SaveChanges();
			
            return order;
		}

		public Order Update(Order order)
		{
			this.DbContext.Entry(order).State = EntityState.Modified;
			this.DbContext.SaveChanges();
			
            return order;
		}

		public void Delete(int id)
		{
			var o = Get(id);
			this.DbContext.Orders.Remove(o);
		}

		public Order AddOrUpdate(Order order)
		{
			return order.IsNew ? this.Add(order) : this.Update(order);
		}

		#endregion
	}
}
