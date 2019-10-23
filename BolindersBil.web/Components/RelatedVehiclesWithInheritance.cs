using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.Models;
using BolindersBil.web.DB;
using Microsoft.AspNetCore.Mvc;
using BolindersBil.web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BolindersBil.web.Components
{
    public class RelatedVehiclesWithInheritance : ViewComponent
    {
        private BolindersBilDatabaseContext ctx;

        public RelatedVehiclesWithInheritance(BolindersBilDatabaseContext context)
        {
            ctx = context;
        }

        public IViewComponentResult Invoke(int carId)
        {
            var car = ctx.Vehicles.Where(x => x.Id == carId).FirstOrDefault();
            var VehicleBrandId = car.Brand.Id;

            var ListOfVehicles = ctx.Vehicles
                .Include(b => b.Brand)
                .Where(x => x.Brand.Id == VehicleBrandId 
                && x.Price > Convert.ToDecimal(car.Price)
                && x.Id != carId)
                .Take(4);
            
            List<Vehicle> vm = new List<Vehicle>();
            vm = ListOfVehicles.ToList();

            return View(vm);
        }
    }
}
