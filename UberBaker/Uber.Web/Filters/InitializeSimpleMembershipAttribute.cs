using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using Uber.Data;
using WebMatrix.WebData;
using Uber.Web.Models;

namespace Uber.Web.Filters
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
	{
		private static SimpleMembershipInitializer _initializer;
		private static object _initializerLock = new object();
		private static bool _isInitialized;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Ensure ASP.NET Simple Membership is initialized only once per app start
			LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
		}

		private class SimpleMembershipInitializer
		{
			public SimpleMembershipInitializer()
			{
				Database.SetInitializer<UberContext>(null);

				try
				{
					using (var context = new UberContext())
					{
						if (!context.Database.Exists())
						{
							// Create the SimpleMembership database without Entity Framework migration schema
							((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
						}
					}

					WebSecurity.InitializeDatabaseConnection("UberContext", "Users", "Id", "UserName", autoCreateTables: true);

					if (!WebSecurity.UserExists("admin"))
					{
						WebSecurity.CreateUserAndAccount("admin", "qwerty", new { FirstName = "Administrator", LastName = "" });
					}
					if (!WebSecurity.UserExists("manager"))
					{
						WebSecurity.CreateUserAndAccount("manager", "qwerty", new { FirstName = "Manager", LastName = "" });
					}
					if (!WebSecurity.UserExists("user"))
					{
						WebSecurity.CreateUserAndAccount("user", "qwerty", new { FirstName = "User", LastName = "" });
					}
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
				}
			}
		}
	}
}
