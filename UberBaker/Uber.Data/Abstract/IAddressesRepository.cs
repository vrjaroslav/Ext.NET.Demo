using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IAddressesRepository
	{
		Address Get(int id);
		IQueryable<Address> GetAll();
		Address Add(Address address);
		Address Update(Address address);
		void Delete(int id);
		Address AddOrUpdate(Address address);
	}
}