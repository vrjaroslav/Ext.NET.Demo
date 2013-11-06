using System.Linq;
using Uber.Core;

namespace Uber.Data.Abstract
{
    public interface IProfilesRepository
    {
        Profile Get(int id);
        IQueryable<Profile> GetAll(bool includingDisabled = false);
        Profile Add(Profile profile);
        Profile Update(Profile profile);
        void Delete(int id);
        Profile AddOrUpdate(Profile profile);
    }
}
