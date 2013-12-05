using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Services;
using Uber.Web.Helpers;
using Uber.Web.Models;

namespace Uber.Web.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        private ICountriesService service { get; set; }

        public CountriesController()
		{
            this.service = new CountriesService();
		}

        public CountriesController(ICountriesService service)
		{
            this.service = service;
		}

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<CountryModel> data = Mapper.Map<List<Country>, List<CountryModel>>(service.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }
    }
}
