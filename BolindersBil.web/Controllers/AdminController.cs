using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly BolindersBilDatabaseContext _context;
        public AdminController(BolindersBilDatabaseContext context)
        {
            _context = context;

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

                _context.Vehicle.Add(car);
                await _context.SaveChangesAsync();

            }

            return View();
        }
    }
}