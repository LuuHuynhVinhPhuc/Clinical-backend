using System;
using System.Collections.Generic;
using ClinicalBackend.Contracts.DTOs.FollowUp;

namespace ClinicalBackend.Contracts.DTOs.Patient
{
    public record struct PatientDto(Guid Id, int age, DateOnly DOB, string Address, string PhoneNumber);
}

