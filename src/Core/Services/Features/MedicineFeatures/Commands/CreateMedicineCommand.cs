using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class CreateMedicineCommand : IRequest<Result<MedicineCreatedResponse>>
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

    public class MedicineCreatedResponse
    {
        public string Response { get; set; }
    }

    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, Result<MedicineCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MedicineCreatedResponse>> Handle(CreateMedicineCommand command, CancellationToken cancellationToken)
        {
            // Check if the medicine already exists
            var existingMedicine = await _unitOfWork.Medicines.GetByCondition(m => m.Name == command.Name).FirstOrDefaultAsync(cancellationToken);
            if (existingMedicine != null)
            {
                return Result.Failure<MedicineCreatedResponse>(MedicineErrors.MedicineNameExist);
            }

            // Create a new Medicine entity
            var medicine = new Medicine
            {
                Name = command.Name,
                Company = command.Company,
                Quantity = command.Quantity,
                Price = command.Price,
                Status = command.Status,
            };

            var response = new MedicineCreatedResponse() { Response = "Medicine created successfully" };

            // Add the medicine to the repository
            _unitOfWork.Medicines.Add(medicine);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}