using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class EditMedicineCommand : IRequest<Result<MedicineEditedResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? Specialty { get; set; }
        public string? Nutritional { get; set; }
        public string? Dosage { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }

    public class MedicineEditedResponse
    {
        public string Response { get; set; }
    }

    public class EditMedicineCommandHandler : IRequestHandler<EditMedicineCommand, Result<MedicineEditedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MedicineEditedResponse>> Handle(EditMedicineCommand command, CancellationToken cancellationToken)
        {
            // Check if the medicine exists
            var existingMedicine = await _unitOfWork.Medicines.GetByIdAsync(command.Id).ConfigureAwait(false);
            if (existingMedicine == null)
            {
                return Result.Failure<MedicineEditedResponse>(MedicineErrors.IdNotFound(command.Id));
            }

            existingMedicine.Name = command.Name ?? existingMedicine.Name;
            existingMedicine.Specialty = command.Specialty ?? existingMedicine.Specialty;
            existingMedicine.Nutritional = command.Nutritional ?? existingMedicine.Nutritional;
            existingMedicine.Dosage = command.Dosage ?? existingMedicine.Dosage;
            existingMedicine.Company = command.Company ?? existingMedicine.Company;
            existingMedicine.Stock = command.Stock;
            existingMedicine.Price = command.Price;

            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new MedicineEditedResponse() { Response = "Medicine edited successfully" };

            return Result.Success(response);
        }
    }
}