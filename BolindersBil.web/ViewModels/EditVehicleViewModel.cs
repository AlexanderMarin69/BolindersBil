using BolindersBil.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class EditVehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        //public List<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Dealerships { get; internal set; }
        public List<FileUpload> FileUpload { get; set; }



        //public Vehicle Vehicle { get; set; }
        //public List<SelectListItem> Brands { get; set; }
        //public List<SelectListItem> DealerShips { get; set; }
        //public List<FileUpload> FileUpload { get; set; }
    }
}
