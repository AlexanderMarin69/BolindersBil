using BolindersBil.web.Models;
using BolindersBil.web.ViewModels.NewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        //public Dictionary<int, string> Values { get; set; }
        public IEnumerable<Vehicle> VehiclesResults { get; set; }
        public string SearchString { get; set; }
        public Vehicle Vehicle { get; }
        public List<Brand> BrandsInStock { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Dealership> Dealerships { get; set; }
        public List<FileUpload> FileUploads { get; set; }
        public ArticlesResult ArticlesResults { get; set; }
        public bool ShowButton { get; set; }
        public int NextPage { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]

        public string Password { get; set; }
        
    }
}
