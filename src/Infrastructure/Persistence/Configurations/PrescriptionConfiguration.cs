using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicalBackend.Persistence.Configurations
{
    internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.OwnsMany(p => p.Products, product =>
            {
                product.Property(p => p.MedicineId);
                product.Property(p => p.Quantity);
                product.OwnsOne(p => p.Instructions, i =>
                {
                    i.Property(p => p.NumberOfDays);
                    i.Property(p => p.Day);
                    i.Property(p => p.Lunch);
                    i.Property(p => p.Afternoon);
                });
            });
        }
    }
}