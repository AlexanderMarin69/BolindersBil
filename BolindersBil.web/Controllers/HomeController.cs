using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.Infrastructure;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
                var vm = new HomeViewModel();
                vm.ArticlesResults = response;

                return View("Index", vm);
           

            
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