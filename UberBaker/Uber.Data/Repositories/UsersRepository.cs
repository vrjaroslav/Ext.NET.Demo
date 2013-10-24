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
            return this.DbContext.Users.SingleOrDefault(c => c.Id == id);
		}

		public IQueryable<User> GetAll()
		{
            return this.DbContext.Users;
		}

		public User Add(User user)
		{
            this.DbContext.Users.Add(user);
            this.DbContext.SaveChanges();
			
            return user;
		}

		public User Update(User user)
		{
            this.DbContext.Entry(user).State = EntityState.Modified;
            this.DbContext.SaveChanges();
			
            return user;
		}

		public void Delete(int id)
		{
			var c = Get(id);

            this.DbContext.Users.Remove(c);
            this.DbContext.SaveChanges();
		}

		public User AddOrUpdate(User user)
		{
			return user.IsNew ? this.Add(user) : this.Update(user);
		}

		#endregion
	}
}
