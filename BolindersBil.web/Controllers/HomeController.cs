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

namespace BolindersBil.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsHelper _newsHelper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public HomeController(NewsHelper newsHelper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _newsHelper = newsHelper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
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

        //[HttpPost]
        //public IActionResult Link(ContactViewModel model)
        //{
        //    var msg = new MimeMessage();
        //    var MsgBody = new BodyBuilder();


        //    msg.From.Add(new MailboxAddress("Kontakt@Bolindersbil.se"));
        //    msg.To.Add(new MailboxAddress(model.Email));

        //    msg.Subject = "Kolla in bilen från BolindersBil";
        //    MsgBody.HtmlBody = "html body";
        //    msg.Body = MsgBody.ToMessageBody();


        //    var client = new MailKit.Net.Smtp.SmtpClient();

        //    client.Connect("localhost", 25, false);
        //    client.Send(msg);
        //    client.Disconnect(true);

        //    return View();

        //}
    }
}