using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Northwind.Repository;
using Northwind.Services;
using Northwind.Entities;
using Northwind.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.Configuration;

namespace Northwind.Web.Configuration
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(IServiceCollection services, IConfiguration config) {
            services.AddDbContext<NorthwindIdentityDbContext>(options =>
                    options.UseSqlServer(
                        config.GetConnectionString("Northwind")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<NorthwindIdentityDbContext>();
            // services.AddIdentity<IdentityUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
