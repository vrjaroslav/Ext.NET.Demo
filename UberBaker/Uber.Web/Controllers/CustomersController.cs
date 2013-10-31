using System;
using System.Web.Mvc;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class CustomersController : Controller
    {
		private ICustomersRepository repository { get; set; }
		private IAddressesRepository addressesRepository { get; set; }
		private ICountriesRepository countriesRepository { get; set; }

		#region Constructors

		public CustomersController()
		{
			this.repository = new CustomersRepository();
			this.addressesRepository = new AddressesRepository();
			this.countriesRepository = new CountriesRepository();
		}

		public CustomersController(ICustomersRepository repository, IAddressesRepository addressesRepository, ICountriesRepository countriesRepository)
		{
			// TODO Rewite with IoC
			this.repository = new CustomersRepository();
			this.addressesRepository = new AddressesRepository();
			this.countriesRepository = new CountriesRepository();
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

		public ActionResult GetAll()
		{
			return this.Store(repository.GetAll());
		}

		public ActionResult GetCountries()
		{
			return this.Store(repository.GetCountries());
		}

		#endregion
	}
}
