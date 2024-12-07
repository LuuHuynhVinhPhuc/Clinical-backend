using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;
using Mapster;
using System.Globalization;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class PatientMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientDto>()
                .MapWith(src => new PatientDto(
                                src.Id,
                                src.Name,
                                src.Age,
                                src.Gender,
                                src.DOB,
                                src.Address,
                                src.PhoneNumber));

            config.NewConfig<Patient, PatientsDto>()
                .MapWith(src => new PatientsDto(
                                src.Id,
                                src.Name,
                                src.Age,
                                src.Gender,
                                ConvertToDateOnly(src.DOB),
                                src.Address,
                                src.PhoneNumber,
                                src.CheckStatus));
        }

        private DateOnly ConvertToDateOnly(DateOnly dateTime)
        {
            // Return parsed date if successful, otherwise return original dateTime
            DateOnly res = DateOnly.ParseExact(dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return res;
        }
    }
}