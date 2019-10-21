using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fyll i ditt namn.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fyll i din email.")]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Fyll i ett ämne.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Skriv ditt meddelande.")]
        public string Message { get; set; }

        public string DealerShipMail { get; set; }
        public string Url { get; set; }
       

    }
}
