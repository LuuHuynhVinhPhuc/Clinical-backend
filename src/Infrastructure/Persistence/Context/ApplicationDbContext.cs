using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Reflection;

namespace ClinicalBackend.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Patient> PatientsInfo { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}