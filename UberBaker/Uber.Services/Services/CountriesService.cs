using System.Collections.Generic;
using System.Linq;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Services
{
    public class CountriesService : ICountriesService
    {
        private IBaseRepository<Country> repository { get; set; }

        #region Constructors

        public CountriesService()
		{
			this.repository = new CountriesRepository();
		}

        public CountriesService(IBaseRepository<Country> repository)
		{
			this.repository = repository;
		}

        #endregion

        #region Data methods

        public List<Country> GetAll()
        {
            return repository.GetAll().ToList();
        }
        
        #endregion
    }
}
