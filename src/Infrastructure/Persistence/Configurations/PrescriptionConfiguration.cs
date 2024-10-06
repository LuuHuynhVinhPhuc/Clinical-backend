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

            builder.HasOne(x => x.FollowUp).WithMany().HasForeignKey(x => x.FollowUpID);
            builder.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientID);

            builder.OwnsMany(p => p.PrescriptionDrugs, pd =>
            {
                pd.WithOwner().HasForeignKey(pd => pd.PrescriptionID);
                pd.Property(x => x.Id).ValueGeneratedOnAdd();
                pd.HasKey(x => x.Id);
                pd.HasOne(x => x.Medicine).WithMany().HasForeignKey(x => x.MedicineID);
            });
        }
    }
}