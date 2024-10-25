using System;
using System.Collections.Generic;
using ClinicalBackend.Contracts.DTOs.FollowUp;

namespace ClinicalBackend.Contracts.DTOs.Medicine
{
    public record struct MedicineDto(
        Guid Id,
        string? Name,
        string? Company,
        int Stock,
        float Price,
        string? Type,
        string? Status,
        DateTime CreatedAt
    );
}

