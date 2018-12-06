using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Northwind.Repository;
using Northwind.Services;
using Northwind.Services.Interfaces;
using System.Net.Mail;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Northwind.Web.Configuration
{
    public static class SmtpClientConfiguration
    {
        public static void ConfigureSmtpClient(
                IServiceCollection services, 
                IConfiguration config,
                ILogger logger) {
            services.AddScoped<SmtpClient>((serviceProvider) =>  
            {  
                var host = config.GetValue<String>("Email:Smtp:Host");
                var port = config.GetValue<int>("Email:Smtp:Port");
                var userName = config.GetValue<String>("Email:Smtp:Username");
                var password = config.GetValue<String>("Email:Smtp:Password");

                if(String.IsNullOrWhiteSpace(host) ||
                    port == 0 ||
                    String.IsNullOrWhiteSpace(userName) ||
                    String.IsNullOrWhiteSpace(password))
                {
                    logger.LogError("Smtp configuration is invalid.");    
                }

                return new SmtpClient()
                {  
                    Host = host,  
                    Port = port,  
                    Credentials = new NetworkCredential(userName, password)  
                };  
            });  
        }
    }
}