using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Repositories;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.web.Controllers
{
    public class FilterController : Controller
    {
        private IVehicleRepository repo;


        private BolindersBilDatabaseContext _context;
        public int PageLimit = 4;

        public FilterController(BolindersBilDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CarFilterResult()
        {
            var carInfo = _context.Vehicles.OrderBy(x => x.Id).Take(PageLimit);
            var vm = new VehicleResultsViewModel { Vehicles = carInfo };

            return View(vm);
        }
        
    }
}

