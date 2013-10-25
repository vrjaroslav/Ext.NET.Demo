using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface ICustomersRepository
	{
		Customer Get(int id);

		IQueryable<Customer> GetAll();

		Customer Add(Customer customer);

		Customer Update(Customer customer);

		void Delete(int id);

		Customer AddOrUpdate(Customer customer);

		IQueryable<Country> GetCountries();
	}
}
