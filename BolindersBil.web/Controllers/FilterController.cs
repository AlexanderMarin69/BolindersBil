﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using BolindersBil.web.Repositories;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BolindersBil.web.Controllers
{
    [Route("bilar")]
    public class FilterController : Controller
    {
        private IVehicleRepository repo;


        private BolindersBilDatabaseContext ctx;
        public int PageLimit = 4;

        public FilterController(BolindersBilDatabaseContext context, IVehicleRepository repository)
        {
            ctx = context;
            repo = repository;
        }

        [Route("{state}")]
        [HttpGet]
        public IActionResult Index(string state, int page = 1)
        {
            IEnumerable<Vehicle> result = new List<Vehicle>();

            if (state.Equals("begagnade", StringComparison.InvariantCultureIgnoreCase))
            {
                result = ctx.Vehicles.Include(x => x.Brand).Include(x => x.Dealership).Where(x => x.Used).OrderByDescending(Vehicle => Vehicle.DateAdded).Take(8);
            }
            else if (state.Equals("nya", StringComparison.InvariantCultureIgnoreCase))
            {
                result = ctx.Vehicles.Include(x => x.Brand).Include(x => x.Dealership).Where(x => !x.Used).OrderByDescending(Vehicle => Vehicle.DateAdded).Take(8);
            }
            else if (state.Equals("alla", StringComparison.InvariantCultureIgnoreCase))
            {
                result = ctx.Vehicles.Include(x => x.Brand).Include(x => x.Dealership).Include(x => x.FileUpload).OrderByDescending(Vehicle => Vehicle.DateAdded).Take(8);
            }
            else
            {
                result = ctx.Vehicles.Include(x => x.Brand).Include(x => x.Dealership).Where(x => x.Brand.Name.Equals(state, StringComparison.InvariantCultureIgnoreCase));
                if (!result.Any()) return NotFound();
            }

            var toSkip = (page - 1) * PageLimit;

            var finalResult = result.OrderByDescending(x => x.DateAdded).ToList().Skip(toSkip).Take(PageLimit);
            var vm = GetFilterVm(finalResult);
            vm.Pager = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageLimit,
                TotalItems = result.Count()
            };

            return View(vm);
        }

        [Route("Search")]
        [HttpPost]
        public IActionResult Search(HomeViewModel vm)
        {
            try
            {
                var VehicleSearchResults = ctx.Vehicles.Include(x => x.Brand).Include(x => x.Dealership).Where(x => x.Model.Contains(vm.SearchString) || vm.SearchString == "").ToList();




                var hej = new FilterDataViewModel { Results = VehicleSearchResults };

                var heh = GetFilterVm(VehicleSearchResults);

                return View(nameof(Index), heh);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
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
                }).Distinct(),

                PriceDataEnd = ctx.Vehicles.OrderBy(x => x.Price).Select(x => new SelectListItem
                {
                    Text = x.Price.ToString(),
                    Value = x.Price.ToString()
                }).Distinct(),

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

        [HttpPost]
        public IActionResult FilterAction(FilterDataViewModel vm)
        {

            var result = ctx.Vehicles.Include(x => x.Brand).Include(x => x.Dealership).Where(x => x.Price >= Convert.ToDecimal(vm.MinPrice.Replace(".", ",")) && x.Price <= Convert.ToDecimal(vm.MaxPrice.Replace(".", ",")));
            result = result.Where(x => x.Mileage >= vm.MinMileage && x.Mileage <= vm.MaxMileage);
            result = result.Where(x => x.BodyId == vm.SelectedBody);
            result = result.Where(x => x.Fuel == vm.SelectedFuel);
            result = result.Where(x => x.Year >= vm.MinYear && x.Year <= vm.MaxYear);
            result = result.OrderByDescending(Vehicle => Vehicle.DateAdded);

            if (vm.NewCar == true)
            {
                result = result.Where(x => x.Used == false);

                var finalResult = result.ToList();


                vm = GetFilterVm(finalResult);
            }

            else if (vm.OldCar == true)
            {
                result = result.Where(x => x.Used == true);

                var finalResult = result.ToList();


                vm = GetFilterVm(finalResult);
            }

            else if (vm.OldCar == false || vm.NewCar == false)
            {

                var finalResult = result.ToList();


                vm = GetFilterVm(finalResult);
            }



            return View("index", vm);




            //var finalResult = result.ToList();

            //if (finalResult.Count() == 0)
            //{
            //    finalResult = ctx.Vehicles.OrderBy(x => x.Id).Take(8).ToList();
            //    vm = GetFilterVm(finalResult);
            //    return View("index", vm);
            //}
            //else
            //{
            //    vm = GetFilterVm(finalResult);
            //    return View("index", vm);
            //}
        }
        [Route("bilar")]
        public IActionResult CarFilterResult()
        {
            var carInfo = ctx.Vehicles.OrderBy(x => x.Id).Take(PageLimit);
            var vm = new VehicleResultsViewModel { Vehicles = carInfo };

            return View(vm);
        }

        [Route("{make}-{model}-{registrationNumber}-{id:int}", Name = "CarDetailsPage")]
        public IActionResult CarPage(int id)
        {
            //var carElement = ctx.Brands.Where(x => x.Id.Equals(id));

            var carElement = ctx.Vehicles.Include(x => x.Brand).Include(x => x.FileUpload).FirstOrDefault(x => x.Id.Equals(id));
            var dealershipIdForPhoneNumber = carElement.DealerShipId;

            ViewBag.DealershipSpecificPhoneNumber = ctx.Dealerships.FirstOrDefault(x => x.Id == dealershipIdForPhoneNumber).Phone;


            return View(carElement);
        }


    }
}

