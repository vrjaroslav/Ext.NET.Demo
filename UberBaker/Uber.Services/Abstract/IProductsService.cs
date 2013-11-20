using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services
{
    public interface IProductsService
    {
        Product Save(Product product);
        void Delete(int id);
        List<Product> GetAll();
    }
}