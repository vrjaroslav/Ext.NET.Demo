using System.Data;
using System.Linq;
using System.Reflection;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private UberContext _db { get; set; }

		public ProductsRepository() : this(new UberContext())
		{
		}

		public ProductsRepository(UberContext db)
		{
			this._db = db;
		}

		public Product Get(int id)
		{
			return _db.Products.SingleOrDefault(p => p.Id == id);
		}

		public IQueryable<Product> GetAll()
		{
			return _db.Products.Include("ProductType");
		}

		public Product Add(Product product)
		{
			_db.Products.Add(product);
			_db.SaveChanges();
			return product;
		}

		public Product Update(Product product)
		{
			_db.Entry(product).State = EntityState.Modified;
			_db.SaveChanges();
			return product;
		}
	
		public void Delete(int id)
		{
			var p = Get(id);

			_db.Products.Remove(p);
			_db.SaveChanges();
		}
	}
}
