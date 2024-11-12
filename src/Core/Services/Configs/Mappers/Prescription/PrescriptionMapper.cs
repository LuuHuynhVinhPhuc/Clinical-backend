using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Domain.Entities;
using Mapster;

namespace ClinicalBackend.Services.Configs.Mappers
{
    public class PrescriptionMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Prescription, GetPrescriptionDto>()
                .MapWith(src => new GetPrescriptionDto(
                    src.Id,
                    src.Patient.Adapt<PatientDto>(),
                    src.FollowUp.Adapt<SummaryDto>(),
                    src.Products.Adapt<ICollection<GetProductDto>>(),
                    src.Notes,
                    src.TotalPrice,
                    src.RevisitDate,
                    src.BillDate,
                    src.CreatedAt
                ));



            config.NewConfig<Product, GetProductDto>()
                .MapWith(src => new GetProductDto(
                    src.Medicine.Id,
                    src.Medicine.Name,
                    Convert.ToInt32(src.Instructions.NumberOfDays) * 
                    (Convert.ToInt32(src.Instructions.Day)
                    + Convert.ToInt32(src.Instructions.Lunch)
                    + Convert.ToInt32(src.Instructions.Afternoon)),
                    src.Instructions.Adapt<InstructionsDto>(),
                    Convert.ToInt32(src.Instructions.Day)
                    + Convert.ToInt32(src.Instructions.Lunch)
                    + Convert.ToInt32(src.Instructions.Afternoon)
                ));
        }
    }
}