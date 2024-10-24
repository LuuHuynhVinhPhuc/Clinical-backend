using System;
using System.Collections.Generic;
using ClinicalBackend.Contracts.DTOs.FollowUp;

namespace ClinicalBackend.Contracts.DTOs.Prescription
{
    public record struct InstructionsDto(
        string Day,
        string Lunch,
        string Afternoon, 
        string Manual
    );
}

