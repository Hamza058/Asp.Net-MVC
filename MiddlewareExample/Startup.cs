using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiddlewareExample.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExample
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
            services.AddControllersWithViews();
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

            #region Map ve Run Kullanýmý
            //app.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Before 1. Middleware\n");
            //        await next();
            //        await context.Response.WriteAsync("After 1. Middleware\n");
            //    });

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Before 2. Middleware\n");
            //    await next();
            //    await context.Response.WriteAsync("After 2. Middleware\n");
            //});

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Terminal 3. Middleware\n");
            //}); 
            #endregion

            #region Map Methodu Kullanýmý
            //app.Map("/ornek", app =>
            //{
            //    app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Ornek url icin middleware");
            //    });
            //}); 
            #endregion

            #region MapWhen Methodu Kullanýmý
            //app.MapWhen(context => context.Request.Query.ContainsKey("name"), app =>
            //{
            //    app.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Before 1. Middleware\n");
            //        await next();
            //        await context.Response.WriteAsync("After 1. Middleware\n");
            //    });

            //    app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Terminal 3. Middleware\n");
            //    });
            //}); 
            #endregion

            app.UseMiddleware<WhiteIpAddressControlMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
