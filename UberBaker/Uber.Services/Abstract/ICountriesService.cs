using System.Collections.Generic;
using Uber.Core;

namespace Uber.Services
{
    public interface ICountriesService
    {
        List<Country> GetAll();
    }
}