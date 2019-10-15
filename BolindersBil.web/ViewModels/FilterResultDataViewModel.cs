using BolindersBil.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class FilterResultDataViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> PriceDataStart { get; internal set; }
        public IEnumerable<SelectListItem> YearDataStart { get; internal set; }
        public IEnumerable<SelectListItem> MileageDataStart { get; internal set; }
        public IEnumerable<SelectListItem> PriceDataEnd { get; internal set; }
        public IEnumerable<SelectListItem> YearDataEnd { get; internal set; }
        public IEnumerable<SelectListItem> MileageDataEnd { get; internal set; }
        public IEnumerable<SelectListItem> FuelData { get; internal set; }
        public IEnumerable<SelectListItem> BodiesData { get; internal set; }

    }
}
