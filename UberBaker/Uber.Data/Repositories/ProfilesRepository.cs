using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class ProfilesRepository : IBaseRepository<Profile>
    {
        private UberContext DbContext { get; set; }

		#region Constructors

		public ProfilesRepository() : this(new UberContext())
		{
		}

        public ProfilesRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public Profile Get(int id)
		{
            return this.DbContext.Profiles.SingleOrDefault(p => p.Id == id);
		}

        public IQueryable<Profile> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ?
                this.DbContext.Profiles.Include("User").Include("User.Role") :
                this.DbContext.Profiles.Include("User").Include("User.Role").Where(p => !p.Disabled);
		}

        public Profile Add(Profile profile)
		{
            this.DbContext.Profiles.Add(profile);
            this.DbContext.SaveChanges();
			
            return profile;
		}

        public Profile Update(Profile profile)
		{
            this.DbContext.Entry(profile).State = EntityState.Modified;
            this.DbContext.SaveChanges();
			
            return profile;
		}

		public void Delete(int id)
		{
            Profile p = Get(id);
            p.Disabled = true;
            
            this.DbContext.SaveChanges();
		}

        public Profile AddOrUpdate(Profile profile)
		{
			return profile.IsNew ? this.Add(profile) : this.Update(profile);
		}

		#endregion
    }
}
