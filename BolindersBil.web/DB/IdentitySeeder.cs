using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.DB
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private const string _admin = "admin";
        private const string _password = "buggeroff";

        private readonly BolindersBilDatabaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeeder(BolindersBilDatabaseContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
      
        public bool CreateAdminAccountIFEmpty()
        {
            if (!_context.Users.Any(u => u.UserName == _admin))
            {
               var result =  _userManager.CreateAsync(new IdentityUser
                {
                    UserName = _admin,
                    Email = "admin@example.com",
                    EmailConfirmed = true
                }, _password);

                var test = result.Status;
            }
            return true;
        }
    }
}
