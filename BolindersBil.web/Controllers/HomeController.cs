using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.Infrastructure;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BolindersBil.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsHelper _newsHelper;

       
        public HomeController(NewsHelper newsHelper)
        {
            _newsHelper = newsHelper;
        }

        public IActionResult Index()
        {
            var response = _newsHelper.GetNews();
            var vm = new VehicleListViewModel();
            vm.ArticlesResults = response;

            return View(vm);
        }
    }
}