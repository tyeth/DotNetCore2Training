using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlightMVC.Data;
using FlightMVC.Models;
using FlightMVC.Repositories;
using FlightMVC.Services;
using Microsoft.Extensions.Logging;
using FlightMVC.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace FlightMVC
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
            services.Configure<ConfigData>(Configuration);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));




            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "129bf75f-940f-4deb-bd10-aa8e81ec6bca";// USE SECRET MANAGER INSTEAD AND THIS KEY Configuration["Authentication:Microsoft:ApplicationId"];
                microsoftOptions.ClientSecret = "swtkEEX9[qahZMEZ9438[@+"; // USE SECRET MANAGER INSTEAD AND THIS KEY Configuration["Authentication:Microsoft:Password"];
            });

            // services.AddSingleton<ILoggerFactory, LoggerFactory>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IPassengersRepository, PassengersRepository>();
            services.AddTransient<MyExceptionFilterAttribute>();


            services.AddMemoryCache();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(20));

            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            var policyAdmin = new AuthorizationPolicyBuilder().RequireRole("Admin").Build();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthedUser", policy);
                options.AddPolicy("AdminOnly", policyAdmin);
            });


            services.AddMvc(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(policy));
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
