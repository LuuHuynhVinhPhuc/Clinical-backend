using System;
using System.Collections.Generic;
using ClinicalBackend.Contracts.DTOs.FollowUp;

namespace ClinicalBackend.Contracts.DTOs.Patient
{
    public record struct PatientsDto(
        Guid Id,
        string? Name,
        int Age,
        string? Gender,
        DateOnly DOB,
        string? Address,
        string? PhoneNumber,
        string Status
        //ICollection<PatientFollowUpDto>? FollowUps
    );
}

