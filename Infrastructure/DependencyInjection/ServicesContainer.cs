using Application.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services.TokenServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            services.AddScoped<JwtTokenGenerator>();

            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
