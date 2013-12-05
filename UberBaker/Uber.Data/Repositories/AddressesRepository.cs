using System.Collections.Generic;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class AddressesRepository : IBaseRepository<Address>
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public AddressesRepository() : this(new UberContext())
		{
		}

		public AddressesRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public Address Get(int id)
		{
            return this.DbContext.Addresses.SingleOrDefault(c => c.Id == id);
		}

        public IQueryable<Address> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ? this.DbContext.Addresses : this.DbContext.Addresses.Where(a => !a.Disabled);
		}

		public Address Add(Address address)
		{
            this.DbContext.Addresses.Add(address);
            this.DbContext.SaveChanges();
			
            return address;
		}

		public Address Update(Address address)
		{
			this.DbContext.Entry(address).State = EntityState.Modified;
			this.DbContext.SaveChanges();
			return address;
		}

		public void Delete(int id)
		{
			var address = Get(id);
            
            this.DbContext.Addresses.Remove(address);
            this.DbContext.SaveChanges();
		}

		public Address AddOrUpdate(Address address)
		{
			return address.IsNew ? this.Add(address) : this.Update(address);
		}

        #endregion
	}
}
