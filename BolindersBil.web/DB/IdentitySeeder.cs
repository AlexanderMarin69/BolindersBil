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
      
        public async Task<bool> CreateAdminAccountIFEmpty()
        {
            if (!_context.Users.Any(u => u.UserName == _admin))
            {
                await _userManager.CreateAsync(new IdentityUser
                {
                    UserName = _admin,
                    Email = "admin@example.com",
                    EmailConfirmed = true
                }, _password);
            }

            return true;
        }

    }
}
