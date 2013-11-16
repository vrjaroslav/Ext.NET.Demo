using System.Linq;
using System.Web.Mvc;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;

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
            var data = repository.GetAll().ToList();

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }
    }
}
