using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IProductTypesRepository
	{
		ProductType Get(int id);

        IQueryable<ProductType> GetAll(bool includingDisabled = false);

		ProductType Add(ProductType productType);

		ProductType Update(ProductType productType);

		void Delete(int id);

		ProductType AddOrUpdate(ProductType productType);
	}
}
