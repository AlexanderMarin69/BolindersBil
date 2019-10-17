using BolindersBil.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class FilterDataViewModel
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> PriceDataStart { get; internal set; }
        public IEnumerable<SelectListItem> YearDataStart { get; internal set; }
        public IEnumerable<SelectListItem> MileageDataStart { get; internal set; }
        public IEnumerable<SelectListItem> PriceDataEnd { get; internal set; }
        public IEnumerable<SelectListItem> YearDataEnd { get; internal set; }
        public IEnumerable<SelectListItem> MileageDataEnd { get; internal set; }
        public IEnumerable<SelectListItem> FuelData { get; internal set; }
        public IEnumerable<SelectListItem> BodiesData { get; internal set; }
        public string MaxPrice { get; set; }
        public string MinPrice { get; set; }
        public double MaxMileage { get; set; }
        public double MinMileage { get; set; }
        public int MaxYear{ get; set; }
        public int MinYear { get; set; }
        public int SelectedBody { get; set; }
        public string SelectedFuel { get; set; }
        public IEnumerable<Vehicle> Results { get; set; }
        public string SearchString { get; set; }
        public bool NewCar { get; set; }
        public bool OldCar { get; set; }
    }
}
