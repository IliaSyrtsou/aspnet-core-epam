using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Web.Configuration;
using Northwind.Web.Middleware;
using Northwind.Repository;
using Northwind.Web.BackgroundTasks;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.Models;

namespace Northwind
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config,
            ILoggerFactory loggerFactory)
        {
            Environment = env;
            Configuration = config;
            LoggerFactory = loggerFactory;
        }

        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var logger = LoggerFactory.CreateLogger<Startup>();
            services.AddDbContext<NorthwindDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Northwind")));


            services.AddSingleton<IConfiguration>(Configuration);

            DIConfiguration.RegisterFilters(services);
            DIConfiguration.RegisterServices(services);
            DIConfiguration.RegisterRepository(services);

            services.AddAutoMapper();

            SmtpClientConfiguration.ConfigureSmtpClient(services, Configuration, logger);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHostedService<TrackMaxCachedImagesService>();

            IdentityConfiguration.ConfigureIdentity(services, Configuration);
            services.AddMvc(options => {
                options.Filters.Add<LoggingActionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = "https://localhost:5011";
                    options.RequireHttpsMetadata = false;
                    // options.ClientSecret = new Secret("secret".Sha256()).Value;
                    options.ClientId = "northwind";
                    options.SaveTokens = true;
                });

            SwaggerConfiguration.AddSwaggerGen(services);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:5010").AllowAnyHeader().AllowAnyMethod());
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseImageCache();
            SwaggerConfiguration.UseSwagger(app);
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseMvc();
        }
    }
}
