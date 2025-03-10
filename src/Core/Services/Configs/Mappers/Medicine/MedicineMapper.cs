using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Domain.Entities;
using Mapster;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class MedicineMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Medicine, MedicineDto>()
                .MapWith(src => new MedicineDto(
                                src.Id,
                                src.Name,
                                src.Company,
                                src.Specialty,
                                src.Nutritional,
                                src.Dosage,
                                src.Stock,
                                src.Price,
                                src.Status,
                                src.CreatedAt));

            config.NewConfig<Medicine, MedicineByDateDto>()
                .MapWith(src => new MedicineByDateDto(
                                src.Id,
                                src.Name,
                                src.Company,
                                src.Stock,
                                src.Price,
                                src.CreatedAt));

            config.NewConfig<Medicine, Top10MedicineDto>()
                .MapWith(src => new Top10MedicineDto(
                                src.Id,
                                src.Name,
                                src.Company,
                                src.Stock));
        }
    }
}