using Hermes.API.Services;
using Hermes.API.Util;
using Hermes.Core.Interfaces;
using Hermes.Infrastructure.Repositories;

namespace Hermes.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ServiceExtensionsMethod(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Profiles of AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            //Services
            services.AddScoped(typeof(ArticleService));
            services.AddScoped(typeof(ReporterService));
            services.AddScoped(typeof(AuthService));

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
