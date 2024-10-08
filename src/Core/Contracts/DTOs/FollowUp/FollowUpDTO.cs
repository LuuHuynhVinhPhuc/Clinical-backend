using ClinicalBackend.Contracts.DTOs.Patient;
using System;

namespace ClinicalBackend.Contracts.DTOs.FollowUp
{
    public record struct FollowUpDto(Guid id, PatientDto Patient, string Reason, string History,
                                    string Diagnosis, string Summary);
}
