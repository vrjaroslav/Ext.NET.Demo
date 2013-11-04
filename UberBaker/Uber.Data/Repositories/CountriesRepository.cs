using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class CountriesRepository : ICountriesRepository
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public CountriesRepository() : this(new UberContext())
		{
		}

		public CountriesRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public Country Get(int id)
		{
            return this.DbContext.Countries.SingleOrDefault(c => c.Id == id);
		}

        public IQueryable<Country> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ? this.DbContext.Countries : this.DbContext.Countries.Where(c => !c.Disabled);
		}

		public Country Add(Country country)
		{
            this.DbContext.Countries.Add(country);
            this.DbContext.SaveChanges();
			
            return country;
		}

		public Country Update(Country country)
		{
			this.DbContext.Entry(country).State = EntityState.Modified;
			this.DbContext.SaveChanges();
			return country;
		}

		public void Delete(int id)
		{
			var country = Get(id);
            
            this.DbContext.Countries.Remove(country);
            this.DbContext.SaveChanges();
		}

		public Country AddOrUpdate(Country country)
		{
			return country.IsNew ? this.Add(country) : this.Update(country);
		}

		#endregion
	}
}
