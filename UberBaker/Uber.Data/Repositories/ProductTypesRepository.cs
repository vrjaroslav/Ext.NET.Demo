using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class ProductTypesRepository : IBaseRepository<ProductType>
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
            return this.DbContext.ProductTypes.SingleOrDefault(p => p.Id == id);
		}

        public IQueryable<ProductType> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ? this.DbContext.ProductTypes : this.DbContext.ProductTypes.Where(p => !p.Disabled);
		}

		public ProductType Add(ProductType productType)
		{
            this.DbContext.ProductTypes.Add(productType);
            this.DbContext.SaveChanges();
			
            return productType;
		}

		public ProductType Update(ProductType productType)
		{
            this.DbContext.Entry(productType).State = EntityState.Modified;
            this.DbContext.SaveChanges();
			
            return productType;
		}

		public void Delete(int id)
		{
			ProductType pt = Get(id);
            pt.Disabled = true;

            var products = DbContext.Products.Where(p => p.ProductTypeId == id);
            foreach (var p in products)
            {
                p.ProductTypeId = null;
            }

            this.DbContext.SaveChanges();
		}

		public ProductType AddOrUpdate(ProductType productType)
		{
			return productType.IsNew ? this.Add(productType) : this.Update(productType);
		}

		#endregion
	}
}
