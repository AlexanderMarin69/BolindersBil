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
            // Florin: force an exception here of type 500
            //if we change i = 0, we'll have an exception of type 500. See error.html static file
            int i = 1;
            try
            {
                var value = 5 / i;
            }
            catch (Exception ex)
            {
                //TODO: Log this error for easier bug hunt
                throw ex;
            }

            var response = _newsHelper.GetNews();
            var vm = new VehicleListViewModel();
            vm.ArticlesResults = response;

            return View(vm);
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404)
                {
                    var viewName = statusCode.Value.ToString();
                    return View(viewName);
                }
            }
            return View();
        }
    }
}