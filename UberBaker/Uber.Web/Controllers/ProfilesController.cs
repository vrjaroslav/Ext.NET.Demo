using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Providers;

namespace Uber.Web.Controllers
{
    public class ProfilesController : Controller
    {
        private IProfilesRepository repository { get; set; }

		#region Constructors

		public ProfilesController()
		{
			repository = new ProfilesRepository();
		}

        public ProfilesController(IProfilesRepository repository)
		{
			// TODO Rewite with IoC
			this.repository = new ProfilesRepository();
		}

		#endregion

        #region Actions

        public ActionResult ReadData(StoreRequestParameters parameters)
        {
            var data = repository.GetAll().ToList();

            return this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        public ActionResult Disable(int id)
        {
            var profile = repository.Get(id);
            profile.Disabled = true;
            repository.Update(profile);

            //var u = Membership.GetUser(user.UserName);
            //if (u != null)
            //{
            //    u.IsApproved = false;
            //    Membership.UpdateUser(u);
            //}
            return this.Direct();
        }

        public ActionResult ProfileWindow()
        {
            string userName = Membership.GetUser().UserName;
            var currentUser = repository.GetAll().SingleOrDefault(u => u.User.UserName == userName);
            var result = new Ext.Net.MVC.PartialViewResult
            {
                ViewName = "ProfileWindow",
                RenderMode = RenderMode.Auto,
                Model = currentUser,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };

            return result;
        }

        #endregion

    }
}
