using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Repository;

namespace Northwind.Web.Configuration
{
    public static class IdentityServerConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config) {
            // var connectionString = config.GetConnectionString("Northwind.Identity");
            // var migrationsAssembly = typeof(NorthwindDbContext).GetTypeInfo().Assembly.GetName().Name;

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential();
                // .AddTestUsers(Config.GetUsers())
                // this adds the config data from DB (clients, resources)
                // .AddConfigurationStore(options =>
                // {
                //     options.ConfigureDbContext = builder =>
                //         builder.UseSqlServer(connectionString,
                //             sql => sql.MigrationsAssembly(migrationsAssembly));
                // })
                // // this adds the operational data from DB (codes, tokens, consents)
                // .AddOperationalStore(options =>
                // {
                //     options.ConfigureDbContext = builder =>
                //         builder.UseSqlServer(connectionString,
                //             sql => sql.MigrationsAssembly(migrationsAssembly));

                //     // this enables automatic token cleanup. this is optional.
                //     options.EnableTokenCleanup = true;
                //     options.TokenCleanupInterval = 30;
                // });
        }
    }
}