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

namespace BolindersBil.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly BolindersBilDatabaseContext ctx;


        // Florin implemented - ok
        private IVehicleRepository repo;


        // Florin implemented - ok
        public AdminController(BolindersBilDatabaseContext context, IVehicleRepository repository)
        {
            ctx = context;
            repo = repository;

        }


        // Florin implemented - ok
        public IActionResult Index()
        {
            return View(nameof(Index), repo.Vehicles);
        }


        // Florin implemented - ok
        [HttpGet]
        public IActionResult Edit(int productId)
        {
            var vehicle = repo.Vehicles.FirstOrDefault(x => x.Id.Equals(productId));
            var vm = new EditVehicleViewModel
            {
                Brands = ctx.Brands.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

                Dealerships = ctx.Dealerships.Select(x => new SelectListItem
                {
                    Text = x.City,
                    Value = x.Id.ToString(),
                }),

                Vehicle = vehicle
            };
            return View(vm);
        }


        // Florin implemented - ok
        [HttpPost]
        public IActionResult Edit(EditVehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Vehicle.DateUpdated = DateTime.Now;
                repo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }


        // Florin implemented - Ok
        [HttpPost]
        public IActionResult Delete(int vehicleId)
        {
            var deleted = repo.DeleteVehicle(vehicleId);
            if (deleted != null)
            {
                //product was found and deleted
            }
            else
            {
                //TODO
                //product was not found - show error
            }
            return RedirectToAction(nameof(Index));
        }


        // Alex & Florin implemented
        public IActionResult Create()
        {
            var vm = new CreateCarViewModel
            {
                Vehicle = new Vehicle(),
                
                Brands = ctx.Brands.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

                 Dealerships = ctx.Dealerships.Select(x => new SelectListItem
                 {
                     Text = x.City,
                     Value = x.Id.ToString(),
                 })
            };

                return View(vm);
        }

        // Alex & Florin implemented
        [HttpPost]
        public async Task<IActionResult> CreateNewCar(CreateCarViewModel vm)
        {
            
            if (ModelState.IsValid)
            {
                vm.Vehicle.DateAdded = DateTime.Now;

                ctx.Vehicles.Add(vm.Vehicle);

                await ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var vm1 = new CreateCarViewModel
                {
                    Vehicle = new Vehicle(),

                    Brands = ctx.Brands.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),

                    Dealerships = ctx.Dealerships.Select(x => new SelectListItem
                    {
                        Text = x.City,
                        Value = x.Id.ToString(),
                    })
                };

                return View(nameof(Create), vm1);
            }
        }
    }
}