using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using BolindersBil.web.Repositories;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BolindersBil.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly BolindersBilDatabaseContext ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

      
        private IVehicleRepository repo;


       
        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BolindersBilDatabaseContext context, IVehicleRepository repository)
        {
            ctx = context;
            repo = repository;
            _userManager = userManager;
            _signInManager = signInManager;

        }



        [AllowAnonymous]
        public IActionResult Index()
        {
          
            if (User.Identity.IsAuthenticated)
            {
                var vehicleList = ctx.Vehicles.OrderBy(x => x.Id);
                var vm = new HomeViewModel { Vehicles = vehicleList };

                return View(vm);
            }
            else
            {

            return View();

            }
        }

      
        public async Task<IActionResult> Login(HomeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vm.Password, false, false)).Succeeded)
                    {

                    }

                    
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
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

                Bodies = ctx.Bodies.Select(x => new SelectListItem
                {
                    Text = x.BodyName,
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

                Bodies = ctx.Bodies.Select(x => new SelectListItem
                {
                    Text = x.BodyName,
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
                return RedirectToAction("index", "Admin");
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

                    Bodies = ctx.Bodies.Select(x => new SelectListItem
                    {
                        Text = x.BodyName,
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