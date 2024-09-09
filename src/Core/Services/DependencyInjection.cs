using ClinicalBackend.Services.Authentication;
using ClinicalBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.ConfigureOptions<JwtOptionSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}