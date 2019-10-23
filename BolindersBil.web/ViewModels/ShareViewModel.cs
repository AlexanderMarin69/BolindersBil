using BolindersBil.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class ShareViewModel
    {

     
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Message { get; set; }
   
    }
}