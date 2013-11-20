using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services
{
    public interface IUserProfilesService
    {
        List<UserProfile> GetAll();
        void Disable(int id);
        UserProfile Save(UserProfile profile);
        UserProfile Get(int id);
        UserProfile GetByUserName(string userName);
    }
}