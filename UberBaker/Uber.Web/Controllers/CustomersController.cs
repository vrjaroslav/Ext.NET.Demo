using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    public class CustomersController : Controller
    {
        private IBaseRepository<Customer> repository { get; set; }
        private IBaseRepository<Address> addressesRepository { get; set; }

		#region Constructors

		public CustomersController()
		{
			this.repository = new CustomersRepository();
			this.addressesRepository = new AddressesRepository();
		}

        public CustomersController(IBaseRepository<Customer> repository, IBaseRepository<Address> addressesRepository)
		{
			// TODO Rewite with IoC
			this.repository = new CustomersRepository();
			this.addressesRepository = new AddressesRepository();
		}

		#endregion

		#region Actions

        public ActionResult Save(Customer customer)
		{
			if (customer.IsNew)
			{
				repository.Add(customer);
			}
			else
			{
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

				repository.Update(customer);
			}

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			repository.Delete(id);

			return this.Direct();
		}

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<Customer> dataFromRepo = repository.GetAll().ToList();

            List<CustomerModel> data = Mapper.Map<List<Customer>, List<CustomerModel>>(dataFromRepo);

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

		#endregion
	}
}
