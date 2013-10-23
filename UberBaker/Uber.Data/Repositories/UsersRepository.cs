using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private UberContext DbContext { get; set; }

		#region Constructors

		public UsersRepository() : this(new UberContext())
		{
		}

		public UsersRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public User Get(int id)
		{
			return DbContext.Users.SingleOrDefault(c => c.Id == id);
		}

		public IQueryable<User> GetAll()
		{
			return DbContext.Users;
		}

		public User Add(User user)
		{
			DbContext.Users.Add(user);
			DbContext.SaveChanges();
			return user;
		}

		public User Update(User user)
		{
			DbContext.Entry(user).State = EntityState.Modified;
			DbContext.SaveChanges();
			return user;
		}

		public void Delete(int id)
		{
			var c = Get(id);
			DbContext.Users.Remove(c);
			DbContext.SaveChanges();
		}

		public User AddOrUpdate(User user)
		{
			return user.IsNew ? this.Add(user) : this.Update(user);
		}

		#endregion
	}
}
