using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BolindersBil.web.DB;
using BolindersBil.web.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BolindersBil.web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var conn = Configuration.GetConnectionString("BolindersBilDatabaseContextConnection");

            services.AddDbContext<BolindersBilDatabaseContext>(options => options.UseSqlServer(conn));

            services.AddDbContext<BolindersBilDatabaseContext>(options =>
                   options.UseSqlServer(conn));

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<BolindersBilDatabaseContext>().
                AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });

            //add services for Dependency Injection - Florin!!
            services.AddSingleton<NewsHelper>();

            services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            services.AddMvc();
        }

        



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IIdentitySeeder identitySeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); // felhantering implementation Florin
            }
            else
            {
                app.UseExceptionHandler("/error.html");
                app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
           
            //AuthAppBuilderExtensions.UseAuthentication(app);
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            identitySeeder.CreateAdminAccountIFEmpty();

            
        }
    }
}
