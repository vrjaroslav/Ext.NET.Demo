using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class ProductTypesRepository : IProductTypesRepository
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public ProductTypesRepository() : this(new UberContext())
		{
		}

		public ProductTypesRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public ProductType Get(int id)
		{
			return DbContext.ProductTypes.SingleOrDefault(p => p.Id == id);
		}

		public IQueryable<ProductType> GetAll()
		{
			return DbContext.ProductTypes;
		}

		public ProductType Add(ProductType productType)
		{
			DbContext.ProductTypes.Add(productType);
			DbContext.SaveChanges();
			return productType;
		}

		public ProductType Update(ProductType productType)
		{
			DbContext.Entry(productType).State = EntityState.Modified;
			DbContext.SaveChanges();
			return productType;
		}

		public void Delete(int id)
		{
			var p = Get(id);
			DbContext.ProductTypes.Remove(p);
		}

		public ProductType AddOrUpdate(ProductType productType)
		{
			return productType.IsNew ? this.Add(productType) : this.Update(productType);
		}

		#endregion
	}
}
