using ClinicalBackend.Domain.Repositories;
using ClinicalBackend.Persistence.Context;
using ClinicalBackend.Persistence.Interceptors;
using ClinicalBackend.Persistence.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IFollowUpRepository, FollowUpRepository>();
            services.AddScoped<AuditableEntitiesInterceptor>();

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
                (sp, options) => options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                        b => b
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                    .AddInterceptors(
                        sp.GetRequiredService<AuditableEntitiesInterceptor>()));

            return services;
        }
    }
}