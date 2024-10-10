namespace ClinicalBackend.Contracts.DTOs.FollowUp
{
    public record struct PatientFollowUpDto
    (
        Guid Id, 
        string Reason, 
        string History,
        string Diagnosis, 
        string Summary
    );
}