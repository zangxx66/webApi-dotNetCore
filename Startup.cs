using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Models;

namespace WebAPI {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<Context> (options => options.UseSqlServer (Configuration.GetConnectionString ("Database")));
            services.AddDistributedMemoryCache();
            services.AddSession (options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddCors (options =>
                options.AddPolicy ("AllowHeaders",
                    p => p.AllowAnyOrigin ().AllowAnyHeader ().AllowAnyMethod ().AllowCredentials ())
            );
            services.AddMvc ();
            services.AddAuthentication (CookieAuthenticationDefaults.AuthenticationScheme).AddCookie ();
            services.AddHsts(options=>{
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("admin.satania.app");
                options.ExcludedHosts.Add("www.satania.app");
            });

            services.AddHttpsRedirection(options=>{
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseSession ();
            app.UseStatusCodePages ("text/plain", "Status code page, status code: {0}");
            app.UseAuthentication ();
            app.UseCors ("AllowHeaders");
            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults : new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}