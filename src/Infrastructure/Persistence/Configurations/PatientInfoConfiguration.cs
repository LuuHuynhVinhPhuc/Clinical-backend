using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicalBackend.Persistence.Configurations
{
    internal class PatientInfoConfiguration : IEntityTypeConfiguration<PatientsInfo>
    {
        public void Configure(EntityTypeBuilder<PatientsInfo> builder)
        {

        }
    }
}
