using System;
using System.Collections.Generic;
using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Contracts.DTOs.Patient;

namespace ClinicalBackend.Contracts.DTOs.Prescription
{
    public record struct GetPrescriptionDto(
        Guid Id,
        PatientDto Patient,
        SummaryDto Summary,
        ICollection<GetProductDto> Products,
        string? Notes,
        float TotalPrice,
        DateOnly Revisit,
        DateTime BillDate,
        DateTime CreatedAt
    );

    public record struct PostProductDto(
        Guid MedicineId,
        InstructionsDto Instructions
    );

    public record struct PutProductDto(
        Guid MedicineId,
        string Name,
        InstructionsDto Instructions
    );
    
    public record struct GetProductDto(
        Guid MedicineId,
        string Name,
        int Quantity,
        InstructionsDto Instructions,
        int DailySum
    );
}

