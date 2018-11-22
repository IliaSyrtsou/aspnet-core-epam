using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Web.Configuration;
using Northwind.Web.Middleware;
using Northwind.Repository;
using Northwind.Web.BackgroundTasks;
using Swashbuckle.AspNetCore.Swagger;

namespace Northwind
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Northwind")));

            DIConfiguration.RegisterFilters(services);
            DIConfiguration.RegisterServices(services);
            DIConfiguration.RegisterRepository(services);

            services.AddSingleton<IConfiguration>(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddHostedService<TrackMaxCachedImagesService>();
            services.AddAutoMapper();
            services.AddMvc(options => {
                options.Filters.Add<LoggingActionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseImageCache();
            SwaggerConfiguration.UseSwagger(app);
            app.UseMvc();
            
        }
    }
}
