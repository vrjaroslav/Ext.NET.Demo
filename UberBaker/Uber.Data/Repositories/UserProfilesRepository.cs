using System.Collections.Generic;
using System.Data;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;

namespace Uber.Data.Repositories
{
    public class UserProfilesRepository : IBaseRepository<UserProfile>
    {
        private UberContext DbContext { get; set; }

		#region Constructors

		public UserProfilesRepository() : this(new UberContext())
		{
		}

        public UserProfilesRepository(UberContext db)
		{
			this.DbContext = db;
		}

		#endregion

		#region Methods

		public UserProfile Get(int id)
		{
            return this.DbContext.Profiles.SingleOrDefault(p => p.Id == id);
		}

        public IQueryable<UserProfile> GetAll(bool includingDisabled = false)
		{
            return includingDisabled ?
                this.DbContext.Profiles.Include("User").Include("User.Role") :
                this.DbContext.Profiles.Include("User").Include("User.Role").Where(p => !p.Disabled);
		}

        public UserProfile Add(UserProfile userProfile)
		{
            this.DbContext.Profiles.Add(userProfile);
            this.DbContext.SaveChanges();
			
            return userProfile;
		}

        public UserProfile Update(UserProfile userProfile)
		{
            this.DbContext.Entry(userProfile).State = EntityState.Modified;
            this.DbContext.SaveChanges();
			
            return userProfile;
		}

		public void Delete(int id)
		{
            UserProfile p = Get(id);
            p.Disabled = true;
            
            this.DbContext.SaveChanges();
		}

        public UserProfile AddOrUpdate(UserProfile userProfile)
		{
			return userProfile.IsNew ? this.Add(userProfile) : this.Update(userProfile);
		}

        #endregion
    }
}
