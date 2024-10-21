using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicalBackend.Persistence.Configurations
{
    internal class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(medicine => medicine.Id);
            builder.Property(medicine => medicine.Id).ValueGeneratedOnAdd();
            builder.Property(medicine => medicine.Name).IsRequired();
            
            builder.OwnsOne(medicine => medicine.Instructions, instructions =>
            {
                instructions.Property(i => i.Day);
                instructions.Property(i => i.Lunch);
                instructions.Property(i => i.Afternoon);
                instructions.Property(i => i.Manual);
            });
        }
    }
}