using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// added for Task
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;




// added for Service and DTO classes
using eStoreDAL;




public class UserService
{
    /// <summary>
    /// Create Asp.Identity based user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="pw"></param>
    /// <returns>dictionary containing status of create, plus userid</returns>
    public async Task<string> CreateAsync(ApplicationUser user, string pw)
    {
        UserManager<ApplicationUser> usrMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AppDbContext()));
        var retVal = "";

        // required fields in AspNetUsers table that aren't loaded from registration
        user.AccessFailedCount = 3;
        user.LockoutEnabled = false;
        user.TwoFactorEnabled = false;
        user.PhoneNumberConfirmed = false;
        user.EmailConfirmed = false;

        IdentityResult result = await usrMgr.CreateAsync(user, pw);

        if (result.Succeeded)
        {
            retVal = user.Id;
        }
        else
        {
            retVal = result.Errors.FirstOrDefault();
        }

        return retVal;
    }

    public async Task<ApplicationUser> RetrieveByNameAsync(string u, string p, IAuthenticationManager authMgr)
    {
   UserManager<ApplicationUser> appUsrMgr =
      new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AppDbContext()));
     SignInManager<ApplicationUser, string> sinMgr = 
     new SignInManager<ApplicationUser, string>(appUsrMgr, authMgr);
     var result = await sinMgr.PasswordSignInAsync(u, p, true, false);

       switch(result)
       {
       case SignInStatus.Success:
              var user = await appUsrMgr.FindByNameAsync(u);
              return user;
              case SignInStatus.Failure:
              default:
              return null;
       }
   }

}

