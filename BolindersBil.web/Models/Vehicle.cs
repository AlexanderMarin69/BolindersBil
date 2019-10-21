using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste välja en bilmodell!")]
        public string Model { get; set; }
        //[Required(ErrorMessage = "Du måste välja modellbeskrivning!")]
        //public string ModelDescription { get; set; }
        [Required(ErrorMessage = "Fordonet måste ha registeringsnummer!")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Fordonet måste ha en årsmodell!")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Fordonet måste ha kilometertal!")]
        public double Mileage { get; set; }
        [Required(ErrorMessage = "Fordonet måste ha ett pris!")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Fordonet måste ha en färg!")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Du måste välja växellåda!")]
        public string Transmission { get; set; }
        [Required(ErrorMessage = "Du måste välja bränsle!")]
        public string Fuel { get; set; }
        [Required(ErrorMessage = "Du måste välja antalet hästkrafter!")]
        public string Horsepower { get; set; }
        public bool Used { get; set; }
        public string ImageUrl { get; set; }
        public bool Lease { get; set; }
        public string Attributes { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int DealerShipId { get; set; }
        public virtual Dealership Dealership { get; set; }
        public int FileUploadId { get; set; }
        public virtual List<FileUpload> FileUpload { get; set; }
        public int BodyId { get; set; }
        public virtual Body Body { get; set; }

    }
}
