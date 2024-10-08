using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;
using Mapster;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class PatientMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.age, src => src.Age)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
        }
    }
}
