using BolindersBil.web.DB;
using BolindersBil.web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.web.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private BolindersBilDatabaseContext ctx;

        public VehicleRepository(BolindersBilDatabaseContext context)
        {
            ctx = context;
        }

        //public IEnumerable<Vehicle> Vehicles => ctx.Vehicles;

        public IEnumerable<Vehicle> Vehicles => ctx.Vehicles.Include(f => f.FileUpload);


        // Florin implemented - ok
        public void SaveVehicle(Vehicle v)
        {
            if (v.Id == 0)
            {
                ctx.Vehicles.Add(v);
            }
            else
            {
                var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(v.Id));
                if (ctxVehicle != null)
                {
                    var ctxBody = ctx.Bodies.FirstOrDefault(x => x.Id.Equals(v.BodyId));
                    if (ctxVehicle != null)
                    {
                        var ctxBrand = ctx.Brands.FirstOrDefault(x => x.Id.Equals(v.BrandId));
                        if (ctxBrand != null)
                        {
                            var ctxDealership = ctx.Dealerships.FirstOrDefault(x => x.Id.Equals(v.DealerShipId));
                            if (ctxDealership != null)
                            {
                                ctxVehicle.Model = v.Model;
                                //ctxVehicle.ModelDescription = v.ModelDescription;
                                ctxVehicle.RegistrationNumber = v.RegistrationNumber;
                                ctxVehicle.Year = v.Year;
                                ctxVehicle.Mileage = v.Mileage;
                                ctxVehicle.Price = v.Price;
                                ctxVehicle.Body = v.Body;
                                ctxVehicle.Color = v.Color;
                                ctxVehicle.Transmission = v.Transmission;
                                ctxVehicle.Fuel = v.Fuel;
                                ctxVehicle.Horsepower = v.Horsepower;
                                ctxVehicle.Used = v.Used;
                                ctxVehicle.Lease = v.Lease;
                                ctxVehicle.ImageUrl = v.ImageUrl;
                                ctxVehicle.Attributes = v.Attributes;
                                ctxVehicle.DateUpdated = DateTime.Now;
                                ctxVehicle.Body = ctxBody;
                                ctxVehicle.Dealership = ctxDealership;
                                ctxVehicle.Brand = ctxBrand;
                            }
                        }
                    }
                }
                    ctx.SaveChanges();
                }
            }




            // Florin implemented
            public Vehicle DeleteVehicle(int vehicleId)
            {
                var ctxVehicle = ctx.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
                var regno = ctxVehicle.RegistrationNumber;

                if (ctxVehicle != null)
                {
                    ctx.Vehicles.Remove(ctxVehicle);
                    ctx.SaveChanges();



                }
                return ctxVehicle;
            }
        }
    }
