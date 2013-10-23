using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
	public interface IUsersRepository
	{
		User Get(int id);
		IQueryable<User> GetAll();
		User Add(User user);
		User Update(User user);
		void Delete(int id);
		User AddOrUpdate(User user);
	}
}