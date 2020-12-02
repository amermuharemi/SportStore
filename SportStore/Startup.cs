using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportStore.Data;
using SportStore.Interfaces;
using SportStore.Models;
using Microsoft.EntityFrameworkCore;

namespace SportStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("ConnString")));
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                        name: "null",
                        template: "{category}/Page{productPage:int}",
                        defaults: new { controller = "Product", action = "List" });

                routes.MapRoute(
                        name: null,
                        template: "Page{productPage:int}",
                        defaults: new {controller = "Product", action = "List", productPage = 1});

                routes.MapRoute(
                        name: null,
                        template: "{category}",
                        defaults: new{controller = "Product",action = "List",productPage = 1});

                routes.MapRoute(
                        name: null,
                        template: "",
                        defaults: new{controller = "Product",action = "List",productPage = 1});


                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            SeedData.EnsurePopulated(app);
            //app.UseEndpoints(endpoints =>
            //{
            //     endpoints.MapRoute("default", "{controller=Home}/{action=Index}");
            //});
        }
    }
}
