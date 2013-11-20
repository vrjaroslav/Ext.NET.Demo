using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services
{
    public interface IProductTypesService
    {
        List<ProductType> GetAll();
        ProductType Save(ProductType productType);
        void Delete(int id);
    }
}