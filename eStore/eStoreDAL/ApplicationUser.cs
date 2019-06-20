using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eStoreDAL
{
    
        /// <summary>
        ///     Author:     Evan Lauersen
        ///     Date:       Created: Feb 27, 2015
        ///     Purpose:    Extends base IndentityUser class with custom columns
        ///                 need to migrate in package manager if db doesn't exist
        ///                   pm> Enable-Migrations -ContextTypeName AppDBContext -EnableAutomaticMigrations
        ///                   pm> Update-database
        ///     Revisions:  none    
        /// </summary>
        public class ApplicationUser : IdentityUser
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public int Age { get; set; }
            public string Address1 { get; set; }
            public string City { get; set; }
            public string Mailcode { get; set; }
            public string Country { get; set; }
            public string CreditcardType { get; set; }
            public string Region { get; set; }
        }
    
}
