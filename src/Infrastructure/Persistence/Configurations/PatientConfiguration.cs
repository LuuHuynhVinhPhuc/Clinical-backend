﻿using ClinicalBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Persistence.Configurations
{
    internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(patient => patient.Id);
            builder.Property(patient => patient.Id).ValueGeneratedOnAdd();
            builder.Property(patient => patient.Name).IsRequired();
            builder.Property(patient => patient.PhoneNumber).IsRequired();
        }
    }
}