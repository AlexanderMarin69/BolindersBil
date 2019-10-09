using BolindersBil.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Repositories
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> Vehicles { get; }

        // Florin implemented - ok
        void SaveVehicle(Vehicle v);

        // Florin implemented - ok
        Vehicle DeleteVehicle(int vehicleId);
    }
}
