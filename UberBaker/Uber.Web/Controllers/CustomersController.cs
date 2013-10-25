using System.Web.Mvc;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;

namespace Uber.Web.Controllers
{
    public class CustomersController : Controller
    {
		private CustomersRepository repository { get; set; }

		#region Constructors

		public CustomersController()
		{
			repository = new CustomersRepository();
		}

		public CustomersController(ICustomersRepository repository)
		{
			// TODO Rewite with IoC
			this.repository = new CustomersRepository();
		}

		#endregion

		#region Actions

		public ActionResult Index()
        {
            return this.View();
        }

	    public ActionResult Save(Customer customer)
		{
			repository.AddOrUpdate(customer);

			return this.Direct();
		}

		public ActionResult Delete(int id)
		{
			repository.Delete(id);

			return this.Direct();
		}

		public ActionResult GetAll()
		{
			return this.Store(repository.GetAll());
		}

		public ActionResult GetCountries()
		{
			return this.Store(repository.GetCountries());
		}

		#endregion
	}
}
