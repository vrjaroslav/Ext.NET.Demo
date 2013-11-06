using System;
using System.Linq;
using System.Web.Security;
using Uber.Core;
using Uber.Data;

namespace Uber.Web.Providers
{
    public class UberRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string userName)
        {
            string[] role = { };
            using (UberContext db = new UberContext())
            {
                try
                {
                    // Getting user
                    User user = (from u in db.Users
                                 where u.UserName == userName
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // Getting role
                        Role userRole = db.Roles.Find(user.RoleId);

                        if (userRole != null)
                        {
                            role = new string[] { userRole.Name };
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            }
            return role;
        }

        public override void CreateRole(string roleName)
        {
            Role newRole = new Role() { Name = roleName };
            UberContext db = new UberContext();
            db.Roles.Add(newRole);
            db.SaveChanges();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Finding user
            using (UberContext _db = new UberContext())
            {
                try
                {
                    // Getting user
                    User user = (from u in _db.Users
                                 where u.UserName == username
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        Role userRole = _db.Roles.Find(user.RoleId);

                        //сравниваем
                        if (userRole != null && userRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
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

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}