using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface ICountriesRepository
	{
		Country Get(int id);
		IQueryable<Country> GetAll();
		Country Add(Country country);
		Country Update(Country country);
		void Delete(int id);
		Country AddOrUpdate(Country country);
	}
}