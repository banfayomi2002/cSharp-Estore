using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;


using System.ComponentModel.DataAnnotations;

// added for Service and DTO classes
using eStoreDAL;

// added for Service and DTO classes

namespace eStoreWebsite.Models
{
    public class UserModel
    {
        private IAuthenticationManager  AuthenticationManager;
        private UserService _dal;
        [Required(ErrorMessage = "UserID is required.")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RepeatPassword is required.")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 99)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email format invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address1 is required.")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Mailcode is required.")]
        [RegularExpression(@"[a-zA-z]\d[a-zA-Z]\s\d[a-zA-Z]\d")]
        public string Mailcode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "CreditcardType is required.")]
        public string CreditcardType { get; set; }
        [Required(ErrorMessage = "Region is required.")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }
        public bool RememberMe { get; set; }



        /// <summary>
        /// constructor should pass new context
        /// <summary>
        public UserModel()
        {
            _dal = new UserService();
        }

        public async Task<bool> LoginAsync(IAuthenticationManager authMgr)
        {
            bool loginOk = false;
            ApplicationUser user = await _dal.RetrieveByNameAsync(UserName, Password, authMgr);
            if (user != null)
            {
                Firstname = user.Firstname;
                Lastname = user.Lastname;
                UserName = user.UserName;
                Email = user.Email;
                Age = Convert.ToInt32(user.Age);
                Address1 = user.Address1;
                City = user.City;
                Mailcode = user.Mailcode;
                Region = user.Region;
                Country = user.Country;
                CreditcardType = user.CreditcardType;
                UserID = user.Id;
                loginOk = true;
            }
            return loginOk;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <returns>dictionary containing a status and userid</returns>
        public async Task<string> Register()
        {
            ApplicationUser user = new ApplicationUser();
            // additional customized fields added to std AspNetUsers Table
            user.UserName = UserName;
            user.Firstname = Firstname;
            user.Lastname = Lastname;
            user.Age = Age;
            user.Address1 = Address1;
            user.City = City;
            user.Mailcode = Mailcode;
            user.Region = Region;
            user.Email = Email;
            user.Country = Country;
            user.CreditcardType = CreditcardType;
            return await _dal.CreateAsync(user, Password);
        }
        
    }
}