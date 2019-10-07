using Microsoft.EntityFrameworkCore;
using BolindersBil.web.DB;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace BolindersBil.web.DB
{
    public class BolindersBilDatabaseContext : IdentityDbContext<IdentityUser>
    {
  
        public BolindersBilDatabaseContext(DbContextOptions<BolindersBilDatabaseContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<BolindersBil.web.Models.Brand> Brands { get; set; }
        public DbSet<BolindersBil.web.Models.Dealership> Dealerships { get; set; }
        public DbSet<BolindersBil.web.Models.FileUpload> FileUploads { get; set; }
        public DbSet<BolindersBil.web.Models.Vehicle> Vehicles { get; set; }
        
    }
}




