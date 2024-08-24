using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Constants;
using ClinicalBackend.Services.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{

    public class DeleteMedicineCommand : IRequest<Result<MedicineDeletedResponse>>
    {
        public Guid Id { get; set; } // Added Id to identify the medicine to delete
    }

    public class MedicineDeletedResponse {
        public string Response { get; set; }
    }

    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, Result<MedicineDeletedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MedicineDeletedResponse>> Handle(DeleteMedicineCommand command, CancellationToken cancellationToken)
        {
            // Check if the medicine exists
            var existingMedicine = await _unitOfWork.Medicines.GetByIdAsync(command.Id);
            if (existingMedicine == null)
            {
                return Result.Failure<MedicineDeletedResponse>(MedicineErrors.NotFound(command.Id.ToString()));
            }

            // Remove the existing Medicine entity
            _unitOfWork.Medicines.Remove(existingMedicine);

            var response = new MedicineDeletedResponse() { Response = "Medicine deleted successfully" };

            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(response);
        }
    }
}