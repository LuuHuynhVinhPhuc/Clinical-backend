using System;
using System.Collections.Generic;
using ClinicalBackend.Contracts.DTOs.FollowUp;

namespace ClinicalBackend.Contracts.DTOs.Medicine
{
    public record struct MedicineDto(
        Guid Id,
        string? Name,
        int Stock,
        float Price,
        string? Type,
        InstructionsDto Instructions,
        DateTime CreatedAt
    );
}

