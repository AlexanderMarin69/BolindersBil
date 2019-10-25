using System.Net.Mail;
using System.Threading.Tasks;
using BolindersBil.web.Infrastructure;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using BolindersBil.web.DB;
using BolindersBil.web.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BolindersBil.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsHelper _newsHelper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly BolindersBilDatabaseContext ctx;

        private IVehicleRepository repo; 

        public HomeController(NewsHelper newsHelper, IVehicleRepository repository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BolindersBilDatabaseContext ctx)
        {
            _newsHelper = newsHelper;
            _userManager = userManager;
            _signInManager = signInManager;
            repo = repository;
            this.ctx = ctx;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            var response = _newsHelper.GetNews();
            var vm = new HomeViewModel();
            var uniqueVehicleBrands = ctx.Vehicles.Include(x => x.Brand).Select(x => x.Brand).Distinct();
            vm.BrandsInStock = uniqueVehicleBrands.ToList();
            vm.ArticlesResults = response;

            return View("Index", vm);

        }

        public IActionResult Search(FilterDataViewModel vm)
        {

            var VehicleSearchResults = repo.Vehicles.Where(x => x.Model.Contains(vm.SearchString) || vm.SearchString == "").ToList();
            
            vm.Results = VehicleSearchResults;
            
            
            return RedirectToAction("Filter", "Index", vm.Results);
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

        [AllowAnonymous]
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
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

 
    }
}