using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]

        public string Password { get; set; }
    }
}


