using BolindersBil.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class CreateCarViewModel
    {
        public Vehicle Vehicle { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Bodies { get; set; }
        public IEnumerable<SelectListItem> Dealerships { get; internal set; }
    }
}
