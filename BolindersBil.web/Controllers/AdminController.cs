using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
           


          return View();

        }
        [HttpPost]
        public async Task<IActionResult> Index(Vehicle car)
        {

            if (ModelState.IsValid)
            {

                ctx.Vehicle.Add(car);
                await ctx.SaveChangesAsync();

            }

            return View();
        }
    }
}