using BolindersBil.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class ShareViewModel
    {

        public Vehicle Vehicle { get; set; }

        public string Email { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }

    }
}
