using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.Models;
using BolindersBil.web.DB;
using Microsoft.AspNetCore.Mvc;
using BolindersBil.web.ViewModels;

namespace BolindersBil.web.Components
{
    public class RelatedVehiclesWithInheritance : ViewComponent
    {
        private BolindersBilDatabaseContext ctx;

        public RelatedVehiclesWithInheritance(BolindersBilDatabaseContext context)
        {
            ctx = context;
        }

        public IViewComponentResult Invoke(bool renderOptional, RelatedVehiclesViewModel GetVehicleId)
        {
            //GetVehicleId.IdForRelatedCar
            var GetVehicleObject = ctx.Vehicles.Where(x => x.Id == GetVehicleId.IdForRelatedCars).FirstOrDefault();

            var ListOfVehicles = ctx.Vehicles.Where(x => x.Price >= GetVehicleObject.Price && x.Id != GetVehicleId.IdForRelatedCars).Take(3);

            List<Vehicle> vm = new List<Vehicle>();
            vm = ListOfVehicles.ToList();

            if (renderOptional)
                return View("/Views/Shared/Components/RelatedVehiclesWithInheritance/Default.cshtml", vm);

            return View(vm);
        }
    }
}
