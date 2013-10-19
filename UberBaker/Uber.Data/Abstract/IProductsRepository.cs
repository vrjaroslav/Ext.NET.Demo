using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IProductsRepository
	{
		Product Get(int id);

		IQueryable<Product> GetAll();

		Product Add(Product product);

		Product Update(Product product);

		void Delete(int id);

		Product AddOrUpdate(Product product);
	}
}
