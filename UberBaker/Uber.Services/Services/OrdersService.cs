using System;
using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class OrdersService : IOrdersService
    {
        private IBaseRepository<Order> repository { get; set; }
        private IBaseRepository<ProductType> productTypesRepository { get; set; }

		#region Constructors

		public OrdersService()
		{
			repository = new OrdersRepository();
            productTypesRepository = new ProductTypesRepository();
		}

        public OrdersService(IBaseRepository<Order> repository, IBaseRepository<ProductType> productTypesRepository)
		{
			this.repository = repository;
            this.productTypesRepository = productTypesRepository;
		}

		#endregion

        #region Data methods

        public Order Save(Order order)
        {
            return repository.AddOrUpdate(order);
        }

        public void Delete(int id)
        {
            var o = repository.Get(id);
            o.Disabled = true;
            repository.Update(o);
        }

        public List<Order> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public IQueryable<Order> GetAllByDate(DateTime startDate, DateTime endDate)
        {
            return repository.GetAll()
                .Where(o => (o.OrderDate > startDate && o.OrderDate < endDate));
        }

        #endregion
    }
}
