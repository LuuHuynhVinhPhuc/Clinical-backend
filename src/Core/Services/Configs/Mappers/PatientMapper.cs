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
                .MapWith(src => new PatientDto(
                                src.Id,
                                src.Age,
                                src.DOB,
                                src.Address,
                                src.PhoneNumber));

        }
    }
}
