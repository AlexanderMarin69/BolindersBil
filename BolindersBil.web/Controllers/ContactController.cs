using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace BolindersBil.web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    //instansiate new MimeMessage
                    var message = new MimeMessage();
                    //setting to email adress
                    message.To.Add(new MailboxAddress("Bolinders Bil", contactViewModel.DealerShipMail));
                    //setting the from email adress
                    message.From.Add(new MailboxAddress(contactViewModel.Name, contactViewModel.Email));
                    //E-mail subject
                    message.Subject = contactViewModel.Title;
                    //email message body
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = contactViewModel.Message + " Message was sent by: " + contactViewModel.Name + " E-mail: " + contactViewModel.Email
                    };


                    //configure the email
                    //    using (var emailClient = new SmtpClient())
                    //    {
                    //        emailClient.Connect("smtp.gmail.com", 587, false);
                    //        emailClient.Authenticate("emailadress@gmail.com", "password");
                    //        emailClient.Send(message);
                    //        emailClient.Disconnect(true);
                    //    }
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Oops! we have a problem here {ex.Message}";
                }
            }
            return View();
        }
    }
}