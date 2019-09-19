using BolindersBil.web.Models;
using BolindersBil.web.ViewModels.NewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.ViewModels
{
    public class VehicleListViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public List<Brand> BrandsInStock { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public List<FileUpload> FileUploads { get; set; }
        public ArticlesResult ArticlesResults { get; set; }
        public bool ShowButton { get; set; }
        public int NextPage { get; set; }
    }
}
