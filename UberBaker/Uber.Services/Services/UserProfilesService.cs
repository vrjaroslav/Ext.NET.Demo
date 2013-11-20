using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class UserProfilesService : IUserProfilesService
    {
        private IBaseRepository<UserProfile> repository { get; set; }

		#region Constructors

		public UserProfilesService()
		{
			repository = new UserProfilesRepository();
		}

        public UserProfilesService(IBaseRepository<UserProfile> repository)
		{
            this.repository = repository;
		}

		#endregion

        #region Date methods

        public List<UserProfile> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void Disable(int id)
        {
            var profile = repository.Get(id);
            profile.Disabled = true;
            repository.Update(profile);
        }

        public UserProfile Save(UserProfile profile)
        {
            var p = repository.Get(profile.Id);
            p.Email = profile.Email;
            p.LastName = profile.LastName;
            p.FirstName = profile.FirstName;
            repository.AddOrUpdate(p);

            return p;
        }

        public UserProfile Get(int id)
        {
            return repository.Get(id);
        }

        public UserProfile GetByUserName(string userName)
        {
            return repository.GetAll().SingleOrDefault(u => u.User.UserName == userName);
        }

        #endregion

    }
}
