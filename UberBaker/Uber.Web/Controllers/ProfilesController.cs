using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Data.Abstract;
using Uber.Data.Repositories;
using Uber.Web.Helpers;
using Uber.Web.Models;
using Uber.Web.Providers;

namespace Uber.Web.Controllers
{
    public class ProfilesController : Controller
    {
        private IBaseRepository<Profile> repository { get; set; }

		#region Constructors

		public ProfilesController()
		{
			repository = new ProfilesRepository();
		}

        public ProfilesController(IBaseRepository<Profile> repository)
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

            return this.Direct();
        }

        public ActionResult Save(ProfileUpdateModel profile)
        {
            var p = repository.Get(profile.Id);
            p.Email = profile.Email;
            p.LastName = profile.LastName;
            p.FirstName = profile.FirstName;
            repository.AddOrUpdate(p);
            X.MessageBox.Alert("Success", "Your profile has been updated").Show();

            return this.Direct();
        }

        public ActionResult UpdatePassword(LocalPasswordModel profile)
        {
            if (profile.ConfirmPassword != profile.NewPassword) 
            {
                X.MessageBox.Alert("Error", "Passwords are not equal").Show();
                return this.Direct();
            }

            var currentUserName = Membership.GetUser().UserName;
            var p = profile.Id.HasValue ? 
                repository.Get(profile.Id.Value) :
                repository.GetAll().SingleOrDefault(u => u.User.UserName == currentUserName);

            if (p == null)
            {
                X.MessageBox.Alert("Error", "Requried profile was found").Show();
                return this.Direct();
            }

            if (((UberMembershipProvider) Membership.Provider).ChangePassword(p.User.UserName, profile.OldPassword,
                profile.NewPassword))
            {
                X.MessageBox.Alert("Success", "Your password has been changed").Show();
                X.GetCmp<FieldSet>("UserProfilePassChangeFieldset").Collapse();
                X.GetCmp<FormPanel>("UserProfilePassChangeForm").Reset();
            }
            else
            {
                X.MessageBox.Alert("Error", "You entered the wrong old password").Show();
            }
            

            return this.Direct();
        }

        public ActionResult ProfilePanel(string containerId)
        {
            string userName = Membership.GetUser().UserName;
            var currentUser = repository.GetAll().SingleOrDefault(u => u.User.UserName == userName);
            var result = new Ext.Net.MVC.PartialViewResult
            {
                ViewName = "ProfilePanel",
                RenderMode = RenderMode.AddTo,
                Model = currentUser,
                ContainerId = containerId,
                WrapByScriptTag = false // we load the view via Loader with Script mode therefore script tags is not required
            };

            this.GetCmp<TabPanel>(containerId).SetLastTabAsActive();

            return result;
        }

        #endregion

    }
}
