using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IOrdersRepository
	{
		Order Get(int id);

		IQueryable<Order> GetAll();

		Order Add(Order order);

		Order Update(Order order);

		void Delete(int id);

		Order AddOrUpdate(Order order);
	}
}
