using System;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class CustomersRepository : ICustomersRepository
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public CustomersRepository() : this(new UberContext())
		{
		}

		public CustomersRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public Customer Get(int id)
		{
			return DbContext.Customers.SingleOrDefault(c => c.Id == id);
		}

		public IQueryable<Customer> GetAll()
		{
			return DbContext.Customers;
		}

		public Customer Add(Customer customer)
		{
			DbContext.Customers.Add(customer);
			DbContext.SaveChanges();
			return customer;
		}

		public Customer Update(Customer customer)
		{
			DbContext.Entry(customer).State = EntityState.Modified;
			DbContext.SaveChanges();
			return customer;
		}

		public void Delete(int id)
		{
			var c = Get(id);
			DbContext.Customers.Remove(c);
			DbContext.SaveChanges();
		}

		public Customer AddOrUpdate(Customer customer)
		{
			return customer.IsNew ? this.Add(customer) : this.Update(customer);
		}

		#endregion
	}
}
