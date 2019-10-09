using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.DB
{
    public interface IIdentitySeeder
    {
        bool CreateAdminAccountIFEmpty();
    }
}
