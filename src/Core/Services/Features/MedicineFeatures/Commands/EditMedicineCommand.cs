using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class EditMedicineCommand : IRequest<Result<MedicineEditedResponse>>
    {
        public Guid Id { get; set; } // Added Id to identify the medicine to edit
        public string Name { get; set; }
        public string Company { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
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
                return Result.Failure<MedicineEditedResponse>(MedicineErrors.NotFound(command.Name));
            }

            // Update the existing Medicine entity
            existingMedicine.Name = command.Name;
            existingMedicine.Company = command.Company;
            existingMedicine.Quantity = command.Quantity;
            existingMedicine.Price = command.Price;
            existingMedicine.Status = command.Status;
            existingMedicine.Type = command.Type;

            var response = new MedicineEditedResponse() { Response = "Medicine edited successfully" };

            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Result.Success(response);
        }
    }
}