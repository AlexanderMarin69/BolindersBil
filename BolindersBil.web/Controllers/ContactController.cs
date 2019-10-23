﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;
using BolindersBil.web.ViewModels;
using MailKit.Net.Smtp;
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
        public IActionResult SendForm(ContactViewModel contactViewModel)
        {
            //här ska jag få med namn, tele nr och meddelande.
            var theBody = $"<strong>Namn: </strong> {contactViewModel.Name} <br> " +
                $"<strong>Telefonnummer: </strong> {contactViewModel.PhoneNumber} <br> " +
                $"<strong>Meddelande: </strong> { contactViewModel.Message}";

            var message = new MimeMessage
            {
                //Sender = new MailboxAddress(contactViewModel.Email),
                Subject = contactViewModel.Title,
                //här använder jag min theBody variabel för meddelandet med namn, nummer och meddelande. Samt contenttransferEncodig som gör att t.ex åäö kan användas i mailet.
                Body = new TextPart(TextFormat.Html) { Text = theBody, ContentTransferEncoding = ContentEncoding.QuotedPrintable  }

            };

            

            message.From.Add(new MailboxAddress(contactViewModel.Email));
            message.To.Add(new MailboxAddress(contactViewModel.DealerShipMail));
            
            using (var client = new SmtpClient())
            {
                client.Connect("localhost", 25, false);
                client.Send(message);
                client.Disconnect(true);
            }
            //när man klickar på skicka knappen så returnernar den index filen i -/view/contacts/index.cshtml
            return View("Index");

        }
        
    }
}