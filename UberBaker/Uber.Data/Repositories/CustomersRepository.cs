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
            return this.DbContext.Customers.SingleOrDefault(c => c.Id == id);
		}

		public IQueryable<Customer> GetAll()
		{
            return this.DbContext.Customers
                .Include("BillingAddress")
                .Include("ShippingAddress");
		}

		public Customer Add(Customer customer)
		{
            this.DbContext.Customers.Add(customer);
            this.DbContext.SaveChanges();
			
            return customer;
		}

		public Customer Update(Customer customer)
		{
			this.DbContext.Entry(customer).State = EntityState.Modified;
			this.DbContext.SaveChanges();
			return customer;
		}

		public void Delete(int id)
		{
			var c = Get(id);
            
            this.DbContext.Customers.Remove(c);
            this.DbContext.SaveChanges();
		}

		public Customer AddOrUpdate(Customer customer)
		{
			return customer.IsNew ? this.Add(customer) : this.Update(customer);
		}

		public IQueryable<Country> GetCountries()
		{
			return DbContext.Countries;
		}

		#endregion
	}
}
