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
    public class ImageCarouselCarPageWithInheritance : ViewComponent
    {
        private BolindersBilDatabaseContext ctx;

        public ImageCarouselCarPageWithInheritance(BolindersBilDatabaseContext context)
        {
            ctx = context;
        }

        public IViewComponentResult Invoke(int carId)
        {
            var car = ctx.Vehicles.Where(x => x.Id == carId).FirstOrDefault();
            var VehicleBrandId = car.Brand.Id;

            var ListOfVehicles = 
                ctx.FileUploads
                .Where(x => x.VehicleId == car.Id);

            List<FileUpload> vm = new List<FileUpload>();
            vm = ListOfVehicles.ToList();

            return View(vm);
        }
    }
}
