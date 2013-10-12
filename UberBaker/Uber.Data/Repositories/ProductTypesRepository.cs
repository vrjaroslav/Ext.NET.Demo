using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class ProductTypesRepository : IProductTypesRepository
	{
		private UberContext _db { get; set; }

		public ProductTypesRepository() : this(new UberContext())
		{
		}

		public ProductTypesRepository(UberContext db)
		{
			this._db = db;
		}

		public ProductType Get(int id)
		{
			return _db.ProductTypes.SingleOrDefault(p => p.Id == id);
		}

		public IQueryable<ProductType> GetAll()
		{
			return _db.ProductTypes;
		}

		public ProductType Add(ProductType productType)
		{
			_db.ProductTypes.Add(productType);
			_db.SaveChanges();
			return productType;
		}

		public ProductType Update(ProductType productType)
		{
			_db.Entry(productType).State = EntityState.Modified;
			_db.SaveChanges();
			return productType;
		}

		public void Delete(int id)
		{
			var p = Get(id);
			_db.ProductTypes.Remove(p);
		}
	}
}
