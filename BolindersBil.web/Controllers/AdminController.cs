using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BolindersBil.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly BolindersBilDatabaseContext ctx;
        public AdminController(BolindersBilDatabaseContext context)
        {
            ctx = context;

        }
        public IActionResult Index()
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

                return View("index", vm1);
            }
        }
    }
}