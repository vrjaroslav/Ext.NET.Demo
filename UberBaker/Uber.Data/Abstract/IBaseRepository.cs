using System.Collections.Generic;
using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
    public interface IBaseRepository<T> where T : BaseItem
    {
        T Get(int id);

        IQueryable<T> GetAll(bool includingDisabled = false);

        T Add(T item);

        T Update(T item);

        void Delete(int id);

        T AddOrUpdate(T item);
    }
}
