using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services
{
    public interface IRolesService
    {
        List<Role> GetAll();
        void Disable(int id);
        Role Save(Role profile, Dictionary<string, List<string>> permissions);
        Role Get(int id);
    }
}