using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class OrderItemsRepository : IOrderItemsRepository
	{
		private UberContext _db { get; set; }

		public OrderItemsRepository() : this(new UberContext())
		{
		}

		public OrderItemsRepository(UberContext db)
		{
			this._db = db;
		}

		public OrderItem Get(int id)
		{
			return _db.OrderItems.SingleOrDefault(p => p.Id == id);
		}

		public IQueryable<OrderItem> GetAll()
		{
			return _db.OrderItems;
		}

		public OrderItem Add(OrderItem orderItem)
		{
			_db.OrderItems.Add(orderItem);
			_db.SaveChanges();
			return orderItem;
		}

		public OrderItem Update(OrderItem orderItem)
		{
			_db.Entry(orderItem).State = EntityState.Modified;
			_db.SaveChanges();
			return orderItem;
		}

		public void Delete(int id)
		{
			var o = Get(id);
			_db.OrderItems.Remove(o);
		}
	}
}
