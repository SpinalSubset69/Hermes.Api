using Hermes.API.Services;
using Hermes.API.Util;
using Hermes.Core.Interfaces;
using Hermes.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hermes.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ServiceExtensionsMethod(this IServiceCollection services, IConfiguration _config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Profiles of AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            //Services
            services.AddScoped(typeof(ArticleService));
            services.AddScoped(typeof(ReporterService));
            services.AddScoped(typeof(AuthService));

            //Authorization Schema
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Cryptography:SecurityKey").Value)),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true
                   };
               });

            //CORS Policy
            services.AddCors(op =>
            {
                op.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });
            return services;
        }
    }
}
