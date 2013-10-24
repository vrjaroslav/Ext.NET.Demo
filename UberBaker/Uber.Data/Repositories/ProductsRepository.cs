using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public ProductsRepository() : this(new UberContext())
		{
		}

		public ProductsRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public Product Get(int id)
		{
            return this.DbContext.Products.SingleOrDefault(p => p.Id == id);
		}

		public IQueryable<Product> GetAll()
		{
            return this.DbContext.Products.Include("ProductType");
		}

		public Product Add(Product product)
		{
            this.DbContext.Products.Add(product);
            this.DbContext.SaveChanges();
			
            return product;
		}

		public Product Update(Product product)
		{
            this.DbContext.Entry(product).State = EntityState.Modified;
            this.DbContext.SaveChanges();
			
            return product;
		}
	
		public void Delete(int id)
		{
			var p = Get(id);
            
            this.DbContext.Products.Remove(p);
            this.DbContext.SaveChanges();
		}

		public Product AddOrUpdate(Product product)
		{
			return product.IsNew ? this.Add(product) : this.Update(product);
		}

		#endregion
	}
}
