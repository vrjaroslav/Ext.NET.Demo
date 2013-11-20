using System;
using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class CustomersService : ICustomersService
    {
        private IBaseRepository<Customer> repository { get; set; }
        private IBaseRepository<Address> addressesRepository { get; set; }

		#region Constructors

		public CustomersService()
		{
			this.repository = new CustomersRepository();
			this.addressesRepository = new AddressesRepository();
		}

        public CustomersService(IBaseRepository<Customer> repository, IBaseRepository<Address> addressesRepository)
		{
            this.repository = repository;
            this.addressesRepository = addressesRepository;
		}

		#endregion

        #region Data methods

        public Customer Save(Customer customer)
        {
            if (customer.IsNew)
            {
                return repository.Add(customer);
            }

            if (customer.BillingAddressId != null)
            {
                customer.BillingAddress.Id = customer.BillingAddressId.Value;
                customer.BillingAddress = addressesRepository.Update(customer.BillingAddress);
            }
            else
                throw new ApplicationException("Customer's BillingAddress' Id was not sent");

            if (customer.ShippingAddressId != null)
            {
                customer.ShippingAddress.Id = customer.ShippingAddressId.Value;
                customer.ShippingAddress = addressesRepository.Update(customer.ShippingAddress);
            }
            else
                throw new ApplicationException("Customer's ShippingAddress' Id was not sent");

            return repository.Update(customer);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<Customer> GetAll()
        {
            return repository.GetAll().ToList();
        }

        #endregion
    }
}
