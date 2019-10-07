using BolindersBil.web.DB;
using BolindersBil.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private BolindersBilDatabaseContext ctx;

        public VehicleRepository(BolindersBilDatabaseContext context)
        {
            ctx = context;
        }

        public IEnumerable<Vehicle> Vehicles => ctx.Vehicles;
    }
}
