using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using Uber.Web.Models;
using Uber.Web.Providers;
using WebMatrix.WebData;

namespace Uber.Web
{
	public static class AuthConfig
	{
		public static void RegisterAuth()
		{
            //WebSecurity.InitializeDatabaseConnection("UberContext", "Users", "Id", "UserName", autoCreateTables: false);

            //MembershipUser admin = ((UberMembershipProvider)Membership.Provider).CreateUser("admin", "demo");
            //MembershipUser manager = ((UberMembershipProvider)Membership.Provider).CreateUser("manager", "demo");
            //MembershipUser user = ((UberMembershipProvider)Membership.Provider).CreateUser("user", "demo");
			// To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
			// you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

			//OAuthWebSecurity.RegisterMicrosoftClient(
			//    clientId: "",
			//    clientSecret: "");

			//OAuthWebSecurity.RegisterTwitterClient(
			//    consumerKey: "",
			//    consumerSecret: "");

			//OAuthWebSecurity.RegisterFacebookClient(
			//    appId: "",
			//    appSecret: "");

			//OAuthWebSecurity.RegisterGoogleClient();
		}
	}
}
