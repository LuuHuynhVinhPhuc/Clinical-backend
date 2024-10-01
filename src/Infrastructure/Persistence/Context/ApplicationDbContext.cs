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
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.FollowUps)
                .WithOne(f => f.Patient)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete
        }
    }
}