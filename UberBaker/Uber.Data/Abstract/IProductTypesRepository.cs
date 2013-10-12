using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IProductTypesRepository
	{
		ProductType Get(int id);

		IQueryable<ProductType> GetAll();

		ProductType Add(ProductType productType);

		ProductType Update(ProductType productType);

		void Delete(int id);
	}
}
