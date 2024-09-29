using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicalBackend.Persistence.Configurations
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PatientID).IsRequired();

            builder.OwnsMany(p => p.PrescriptionDrugs, pd =>
            {
                pd.WithOwner().HasForeignKey("PrescriptionId");
                pd.Property<int>("Id").ValueGeneratedOnAdd();
                pd.HasKey("Id");
            });
        }
    }
}