using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Persistence.Configurations
{
    internal class ReExaminationConfiguration : IEntityTypeConfiguration<FollowUp>
    {
        public void Configure(EntityTypeBuilder<FollowUp> builder)
        {
            builder.HasKey(reExamination => reExamination.Id);
            builder.Property(reExamination => reExamination.Id).ValueGeneratedOnAdd();
        }
    }
}
