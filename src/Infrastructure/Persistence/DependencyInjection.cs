using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using ClinicalBackend.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using ClinicalBackend.Persistence.Repositories;
using ClinicalBackend.Domain.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPatientInfoRepository, PatientRepository>();
            services.AddScoped<IFollowUpRepository, FollowUpRepository>();

            return services;
        }
    }
}
