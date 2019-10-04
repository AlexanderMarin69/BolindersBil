using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.DB
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }
        public bool IsMainContactPerson { get; set; }
    }
}
