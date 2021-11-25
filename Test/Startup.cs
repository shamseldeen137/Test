using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data.Models;
using Test.Repo.Implementation;
using Test.Repo.Interface;
using Test.Service;
using Test.Service.Implementation;
using Test.Service.Interface;

namespace Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddEntityFrameworkSqlServer().AddDbContext<DBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
           
            services.AddAutoMapper(typeof(MapperProfile));
            ConfigureService(services);
            ConfigureRepos(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

        }


        void ConfigureService(IServiceCollection services) {
            services.AddScoped<IProductService, ProductService>();


            services.AddScoped<IcategoryService, CategoryService>();
        }
        void ConfigureRepos(IServiceCollection services) {
            services.AddScoped<IProductRepo, ProductRepo>();


            services.AddScoped<ICategoryRepo, CategoryRepo>();
        }
    }
}
