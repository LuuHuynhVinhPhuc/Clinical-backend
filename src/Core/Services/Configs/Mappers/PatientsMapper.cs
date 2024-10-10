using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;
using Mapster;
using System.Globalization;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class PatientsMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientsDto>()
                .MapWith(src => new PatientsDto(
                                src.Id,
                                src.Name,
                                src.Age,
                                ConvertToDateOnly(src.DOB),
                                src.Address,
                                src.PhoneNumber,
                                src.CheckStatus,
                                src.FollowUps.Adapt<ICollection<PatientFollowUpDto>>()
                ));
        }

        private DateOnly ConvertToDateOnly(DateOnly dateTime)
        {
            // Return parsed date if successful, otherwise return original dateTime
            DateOnly res = DateOnly.ParseExact(dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return res;
        }
    }
}
