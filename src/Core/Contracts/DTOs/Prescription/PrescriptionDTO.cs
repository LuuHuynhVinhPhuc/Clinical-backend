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
        DateTime Revisit,
        DateTime BillDate,
        DateTime CreatedAt
    );

    public record struct PostPrescriptionDto(
        Guid PatientId,
        Guid FollowUpId,
        ICollection<PostProductDto> Products,
        string? Notes,
        DateTime BillDate
    );

    public record struct PostProductDto(
        Guid MedicineId,
        int Quantity,
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

