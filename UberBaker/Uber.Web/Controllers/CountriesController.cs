using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    public class CountriesController : Controller
    {
        private IBaseRepository<Country> repository { get; set; }

        public CountriesController()
		{
			this.repository = new CountriesRepository();
		}

        public CountriesController(IBaseRepository<Country> repository)
		{
			// TODO Rewite with IoC
			this.repository = new CountriesRepository();
		}

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<Country> dataFromRepo = repository.GetAll().ToList();

            List<CountryModel> data = Mapper.Map<List<Country>, List<CountryModel>>(dataFromRepo);

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }
    }
}
