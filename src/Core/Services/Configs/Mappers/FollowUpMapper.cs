using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Domain.Entities;
using Mapster;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class FollowUpMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FollowUp, FollowUpDto>()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.Patient, src => src.Patient)
                .Map(dest => dest.Reason, src => src.Reason)
                .Map(dest => dest.Diagnosis, src => src.Diagnosis)
                .Map(dest => dest.Summary, src => src.Summary);
        }
    }
}
