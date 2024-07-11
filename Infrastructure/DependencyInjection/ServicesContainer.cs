using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.DependencyInjection
{
    public static class ServicesContainer
    {
        public static void AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<UserHubContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserHubConnection"),
                b => b.MigrationsAssembly(typeof(ServicesContainer).Assembly.FullName)),
                ServiceLifetime.Scoped);


        }
    }
}
