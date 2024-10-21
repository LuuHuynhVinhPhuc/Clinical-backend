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
        int Quantity
    );
    
    public record struct GetProductDto(
        string Name,
        int Quantity,
        InstructionsDto Instructions
    );
}

