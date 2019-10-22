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

        public IViewComponentResult Invoke(int carId)
        {
            //GetVehicleId.IdForRelatedCar
            var car = ctx.Vehicles.Where(x => x.Id == carId).FirstOrDefault();

            //Algoritmen för att räkna ut vilka bilar som visas(max 4 stycken) ska
            //titta på aktuellt bilmärke och hämta andra bilar av samma märke
            //men de får inte vara billigare än aktuell bils pris.
            var ListOfVehicles = ctx.Vehicles.Where(x => x.Price >= car.Price && x.Id != car.Id).Take(4);

            List<Vehicle> vm = new List<Vehicle>();
            vm = ListOfVehicles.ToList();

            //Don't need this, just for demo purpose in pptx
            //if (renderOptional)
            //    return View("/Views/Shared/Components/RelatedVehiclesWithInheritance/Default.cshtml", vm);

            return View(vm);
        }
    }
}
