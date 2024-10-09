using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;
using Mapster;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class FollowUpMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FollowUp, FollowUpDto>()
                .MapWith(src => new FollowUpDto(
                                src.Id,
                                src.Patient.Adapt<PatientDto>(),
                                src.Reason,
                                src.History,
                                src.Diagnosis,
                                src.Summary));

        }
    }
}