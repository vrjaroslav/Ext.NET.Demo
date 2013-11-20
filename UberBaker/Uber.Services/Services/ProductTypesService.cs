using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class ProductTypesService : IProductTypesService
    {
        private IBaseRepository<ProductType> repository { get; set; }

		#region Constructors

		public ProductTypesService()
		{
			repository = new ProductTypesRepository();
		}

        public ProductTypesService(IBaseRepository<ProductType> repository)
		{
			// TODO Rewite with IoC
			this.repository = new ProductTypesRepository();
		}

		#endregion

        #region Data methods

        public List<ProductType> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public ProductType Save(ProductType productType)
        {
            return repository.AddOrUpdate(productType);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        #endregion
    }
}
