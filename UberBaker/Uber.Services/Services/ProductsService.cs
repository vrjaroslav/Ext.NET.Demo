using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class ProductsService : IProductsService
    {
        private IBaseRepository<Product> repository { get; set; }

        #region Constructors

        public ProductsService()
        {
            repository = new ProductsRepository();
        }

        public ProductsService(IBaseRepository<Product> repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Data methods

        public Product Save(Product product)
        {
            return repository.AddOrUpdate(product);
        }

        public void Delete(int id)
        {
            var p = repository.Get(id);
            p.Disabled = true;
            repository.Update(p);
        }

        public List<Product> GetAll()
        {
           return repository.GetAll().ToList();
        }

        #endregion
    }
}
