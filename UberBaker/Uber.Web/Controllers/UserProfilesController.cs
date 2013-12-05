using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Ext.Net;
using Ext.Net.MVC;
using Uber.Core;
using Uber.Services;
using Uber.Web.Helpers;
using Uber.Web.Models;
using Uber.Web.Providers;

namespace Uber.Web.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private IUserProfilesService service { get; set; }

		#region Constructors

		public UserProfilesController()
		{
            service = new UserProfilesService();
		}

        public UserProfilesController(IUserProfilesService service)
		{
            this.service = service;
		}

		#endregion

        #region Actions

        public ActionResult ReadData(StoreRequestParameters parameters, bool getAll = false)
        {
            List<UserProfileModel> data = Mapper.Map<List<UserProfile>, List<UserProfileModel>>(service.GetAll());

            return getAll ? this.Store(data, data.Count) : this.Store(data.SortFilterPaged(parameters), data.Count);
        }

        public ActionResult Disable(int id)
        {
            service.Disable(id);
            return this.Direct();
        }

        public ActionResult Save(UserProfileModel profile)
        {
            service.Save(Mapper.Map<UserProfileModel, UserProfile>(profile));

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
                service.Get(profile.Id.Value) :
                service.GetByUserName(currentUserName);

            if (p == null)
            {
                X.MessageBox.Alert("Error", "Requried profile was found").Show();
                return this.Direct();
            }

            if (((UberMembershipProvider)Membership.Provider).ChangePassword(p.User.UserName, profile.OldPassword,
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

        public ActionResult CurrentProfilePanel(string containerId)
        {
            string userName = Membership.GetUser().UserName;
            var currentUser = service.GetByUserName(userName);
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
