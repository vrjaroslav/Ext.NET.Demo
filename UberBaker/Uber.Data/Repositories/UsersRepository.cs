using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class UsersRepository : IBaseRepository<User>
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
            return this.DbContext.Users.SingleOrDefault(u => u.Id == id);
		}

        public IQueryable<User> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ?
                this.DbContext.Users :
                this.DbContext.Users.Where(u => !u.Disabled);
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
			User u = Get(id);
            u.Disabled = true;
            
            this.DbContext.SaveChanges();
		}

		public User AddOrUpdate(User user)
		{
			return user.IsNew ? this.Add(user) : this.Update(user);
		}

		#endregion
	}
}
