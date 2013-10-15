using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		List<OrderChartData> GetChartDataPerMonth();
	}
}
