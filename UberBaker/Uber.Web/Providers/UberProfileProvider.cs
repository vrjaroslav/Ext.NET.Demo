using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Profile;
using Uber.Core;
using Uber.Data;

namespace Uber.Web.Providers
{
    public class UberProfileProvider : ProfileProvider
    {
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            // Colletion with properties
            SettingsPropertyValueCollection result = new SettingsPropertyValueCollection();

            if (collection == null || collection.Count < 1 || context == null)
            {
                return result;
            }
            // Getting Username from the Context
            string username = (string)context["UserName"];
            if (String.IsNullOrEmpty(username))
                return result;

            UberContext db = new UberContext();
            
            int userId = db.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault().Id;
            Profile profile = db.Profiles.Where(u => u.UserId == userId).FirstOrDefault();

            if (profile != null)
            {
                foreach (SettingsProperty prop in collection)
                {
                    SettingsPropertyValue svp = new SettingsPropertyValue(prop);
                    svp.PropertyValue = profile.GetType().GetProperty(prop.Name).GetValue(profile, null);
                    result.Add(svp);
                }
            }
            else
            {
                foreach (SettingsProperty prop in collection)
                {
                    SettingsPropertyValue svp = new SettingsPropertyValue(prop);
                    svp.PropertyValue = null;
                    result.Add(svp);
                }
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            // Getting User's name
            string username = (string)context["UserName"];

            if (username == null || username.Length < 1 || collection.Count < 1)
                return;

            UberContext db = new UberContext();
            int userId = db.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault().Id;
            Profile profile = db.Profiles.Where(u => u.UserId == userId).FirstOrDefault();
            if (profile != null)
            {
                foreach (SettingsPropertyValue val in collection)
                {
                    profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                }
                db.Entry(profile).State = EntityState.Modified;
            }
            else
            {
                profile = new Profile();
                foreach (SettingsPropertyValue val in collection)
                {
                    profile.GetType().GetProperty(val.Property.Name).SetValue(profile, val.PropertyValue);
                }
                profile.UserId = userId;
                db.Profiles.Add(profile);
            }
            db.SaveChanges();
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}