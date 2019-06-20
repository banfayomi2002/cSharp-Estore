using eStoreDAL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStoreWebsite
{

    public partial class Startup
    {
        //For information on Configuring authentication, 

        //please visit http://go.
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppDbContext.Create);
            //Enable the application to use a cookie to store 

            //information for the sig
            //and to use a cookie to temporarily store information 

            //about a user logging
            //Configure the sign in cookie

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login"),
                LogoutPath = new PathString("/Login/Logoff"),
                ExpireTimeSpan = System.TimeSpan.FromMinutes(2)
            });
        }
    }
}
