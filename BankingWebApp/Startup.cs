using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankingWebApp.Data;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Http;

namespace BankingWebApp
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
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
            });
            services.AddDNTCaptcha(options =>
         options.UseCookieStorageProvider(SameSiteMode.Strict)
        .WithEncryptionKey("This is my secure key!")
         .InputNames(// This is optional. Change it if you don't like the default names.
                     new DNTCaptchaComponent
                     {
                         CaptchaHiddenInputName = "DNTCaptchaText",
                         CaptchaHiddenTokenName = "DNTCaptchaToken",
                         CaptchaInputName = "DNTCaptchaInputText"
                     })
           .ShowThousandsSeparators(false)
             .Identifier("dntCaptcha")// This is optional. Change it if you don't like its default name.

   );

            services.AddDbContext<BankingWebAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BankingWebAppContext")));
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
            app.UseSession();
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
