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
                                src.Stock,
                                src.Price,
                                src.Type,
                                src.Status,
                                src.CreatedAt));
        }
    }
}