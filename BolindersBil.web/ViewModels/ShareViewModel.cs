using BolindersBil.web.Models;
using System;
using System.Collections.Generic;
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
        public string Email { get; set; }
        public string Message { get; set; }
   
    }
}
