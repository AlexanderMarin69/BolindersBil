using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using BolindersBil.web.Repositories;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BolindersBil.web.Controllers
{
    public class FilterController : Controller
    {
        private IVehicleRepository repo;


        private BolindersBilDatabaseContext ctx;
        public int PageLimit = 4;

        public FilterController(BolindersBilDatabaseContext context)
        {
            ctx = context;
        }
  
        public IActionResult Index()
        {
            //år ft, prisft, milft bränsle, växellåda


            //var VehiclesFilterDataPrice = ctx.Vehicles.OrderBy(x => x.Price).Min();
            //var VehiclesFilterDataYear = ctx.Vehicles.OrderBy(x => x.Year).Min();
            //var VehiclesFilterDataMiles = ctx.Vehicles.OrderBy(x => x.Mileage).Min();
            //var VehiclesFilterDataFuel = ctx.Vehicles.OrderBy(x => x.Fuel).Min();
            //var BodiesFilterData = ctx.Bodies.OrderBy(x => x.Id);

            //var vm = new FilterDataViewModel { Vehicles. = vehicleList };

            var vm = new FilterDataViewModel
            {
                Vehicle = new Vehicle(),

                PriceDataStart = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Price.ToString(),
                    Value = x.Id.ToString()
                }),
                

                YearDataStart = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Year.ToString(),
                    Value = x.Id.ToString()
                }),

                MileageDataStart = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Mileage.ToString(),
                    Value = x.Id.ToString(),
                }),

                PriceDataEnd = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Price.ToString(),
                    Value = x.Id.ToString()
                }),

                YearDataEnd = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Year.ToString(),
                    Value = x.Id.ToString()
                }),

                MileageDataEnd = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Mileage.ToString(),
                    Value = x.Id.ToString(),
                }),

                FuelData = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Fuel,
                    Value = x.Id.ToString(),
                }),

                BodiesData = ctx.Bodies.Select(x => new SelectListItem
                {
                    Text = x.BodyName,
                    Value = x.Id.ToString(),
                })
            };

            return View(vm);
        }

        public IActionResult FilterAction(FilterDataViewModel vm)
        {

           var result = ctx.Vehicles.Where(x => x.Price >= vm.MinPrice && x.Price <= vm.MaxPrice);
            result = result.Where(x => x.Mileage >= 100 && x.Mileage <= 900);

            //ctx.Vehicles.Where(vm.PriceDataStart => x.Price >= PriceDataEnd && x.Price <= filter.MaxPrice && x.Milage >= filter.MinKm && x.Milage <= filter.MaxKm) etc










            //var vehicleList = 

            //var resultVm = new FilterResultDataViewModel { Vehicles = vehicleList };




            vm.Results = result;





            return View(vm);
        }



        public IActionResult CarFilterResult()
        {
            var carInfo = ctx.Vehicles.OrderBy(x => x.Id).Take(PageLimit);
            var vm = new VehicleResultsViewModel { Vehicles = carInfo };

            return View(vm);
        }

        //[Route("Filter/CarPage/{id:int}")]
        public IActionResult CarPage(int id)
        {
            //var carElement = ctx.Brands.Where(x => x.Id.Equals(id));


            var carElement = ctx.Vehicles.Include(x => x.Brand).FirstOrDefault(x => x.Id.Equals(id));
           

            return View(carElement);
        }


    }
}

