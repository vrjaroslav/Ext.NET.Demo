using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IOrderItemsRepository
	{
		OrderItem Get(int id);

		IQueryable<OrderItem> GetAll();

		OrderItem Add(OrderItem orderItem);

		OrderItem Update(OrderItem orderItem);

		void Delete(int id);
	}
}
