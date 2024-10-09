using ClinicalBackend.Contracts.DTOs.Patient;

namespace ClinicalBackend.Contracts.DTOs.FollowUp
{
    public record struct FollowUpDto(Guid Id, PatientDto? Patient, string Reason, string History,
                                    string Diagnosis, string Summary);
}