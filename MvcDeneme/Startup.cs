using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MvcDeneme.Filters;
using MvcDeneme.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MvcDeneme
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
            services.AddScoped<NotFoundFilter>();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllersWithViews();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(//Bu �ekilde tan�mlama yapt���m�zda QueryStringden daha d�zg�n ge�er.
                    //name: "productpages",   //E�er yapmasayd�k Product/Delete/Delete/10 olarak ge�erdi
                    //pattern: "{controller=product}/{action=delete}/{delete?}");
                /*
                 * Burada son k�s�mda tan�mlanan delete id parametresi yerine tan�mlanm��t�r.
                 * controller ve action tan�mlamasakta olur
                 * ? i�areti null de�er alabilir anlam�na gelir. 
                 * Bireden fazla de�er al�rsa /{} yap�s�yla devam edebiliriz.
                */
            });
        }
    }
}
