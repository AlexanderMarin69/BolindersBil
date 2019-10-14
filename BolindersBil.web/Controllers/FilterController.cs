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


            var vm = GetFilterVm();
            return View(vm);
        }

        private FilterDataViewModel GetFilterVm(IEnumerable<Vehicle> results = null)
        {
            var vm = new FilterDataViewModel
            {
                Vehicle = new Vehicle(),

                PriceDataStart = ctx.Vehicles.OrderBy(x => x.Price).Select(x => new SelectListItem
                {
                    Text = x.Price.ToString(),
                    Value = x.Price.ToString()
                }),


                YearDataStart = ctx.Vehicles.OrderBy(x => x.Year).Select(x => new SelectListItem
                {
                    Text = x.Year.ToString(),
                    Value = x.Year.ToString()
                }),

                MileageDataStart = ctx.Vehicles.OrderBy(x => x.Mileage).Select(x => new SelectListItem
                {
                    Text = x.Mileage.ToString(),
                    Value = x.Mileage.ToString()
                }),

                PriceDataEnd = ctx.Vehicles.OrderBy(x => x.Price).Select(x => new SelectListItem
                {
                    Text = x.Price.ToString(),
                    Value = x.Price.ToString()
                }).OrderBy(x => x.Value),

                YearDataEnd = ctx.Vehicles.OrderBy(x => x.Year).Select(x => new SelectListItem
                {
                    Text = x.Year.ToString(),
                    Value = x.Year.ToString()
                }),

                MileageDataEnd = ctx.Vehicles.OrderBy(x => x.Mileage).Select(x => new SelectListItem
                {
                    Text = x.Mileage.ToString(),
                    Value = x.Mileage.ToString()
                }),

                FuelData = ctx.Vehicles.Select(x => new SelectListItem
                {
                    Text = x.Fuel,
                    Value = x.Fuel,
                }).Distinct().OrderBy(x => x.Value),

                BodiesData = ctx.Bodies.Select(x => new SelectListItem
                {
                    Text = x.BodyName,
                    Value = x.Id.ToString()
                })
            };

            vm.Results = results ?? new List<Vehicle>();

            return vm;
        }

        public IActionResult FilterAction(FilterDataViewModel vm)
        {

            var result = ctx.Vehicles.Where(x => x.Price >= Convert.ToDecimal(vm.MinPrice.Replace(".", ",")) && x.Price <= Convert.ToDecimal(vm.MaxPrice.Replace(".", ",")));
            result = result.Where(x => x.Mileage >= vm.MinMileage && x.Mileage <= vm.MaxMileage);
            result = result.Where(x => x.BodyId == vm.SelectedBody);
            result = result.Where(x => x.Fuel == vm.SelectedFuel);
            result = result.Where(x => x.Year >= vm.MinYear && x.Year <= vm.MaxYear);
            var finalResult = result.ToList();
            vm = GetFilterVm(finalResult);
            return View("index", vm);
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

