using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;
using ecommerce.Models;
using ecommerce.Models.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            //var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            //builder.AddUserSecrets<Startup>();
            //Configuration = builder.Build();
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication()
            //    .AddMicrosoftAccount(microsoftOptions =>
            //    {
            //        microsoftOptions.ClientId = Configuration["OAUTH:Authentication:Microsoft:ApplicationId"];
            //        microsoftOptions.ClientSecret = Configuration["OAUTH:Authentication:Microsoft:Password"];
            //    })
            //    .AddGoogle(googleOptions =>
            //    {
            //        googleOptions.ClientId = Configuration["OAUTH:Authentication:Google:ClientId"];
            //        googleOptions.ClientSecret = Configuration["OAUTH:Authentication:Google:ClientSecret"];
            //    });

            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(ApplicationRoles.Admin));
                options.AddPolicy("HasFavAnimal", policy => policy.RequireClaim("FavAnimal"));
            });


            //Everytime you see IInventory create an instance of DevECOMRepo
            //Registering our Dependency Injection
            services.AddScoped<IInventory, DevInventory>();
            services.AddScoped<IBasket, DevBasket>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IOrder, DevOrder>();

            //services.AddDbContext<EcomDbContext>(options =>
            //options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(Configuration["ConnectionStrings:UserConnection"]));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:SquirrelConnection"]));

            services.AddDbContext<EcomDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:ProductionConnection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();


            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
