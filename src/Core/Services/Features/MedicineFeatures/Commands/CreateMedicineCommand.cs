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
        public int Stock { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Instructions Instructions { get; set; }
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
            var existingMedicine = await _unitOfWork.Medicines.GetByCondition(m => m.Name == command.Name).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingMedicine != null)
            {
                return Result.Failure<MedicineCreatedResponse>(MedicineErrors.MedicineNameExist);
            }

            // Validate stock
            if (command.Stock <= 0)
            {
                return Result.Failure<MedicineCreatedResponse>(new Error("Medicine.InvalidStock", "Stock must be a positive integer."));
            }

            // Validate price
            if (command.Price <= 0)
            {
                return Result.Failure<MedicineCreatedResponse>(new Error("Medicine.InvalidPrice", "Price must be a positive value."));
            }

            // Validate instructions
            bool isDayValid = int.TryParse(command.Instructions.Day, out int day);
            bool isLunchValid = int.TryParse(command.Instructions.Lunch, out int lunch);
            bool isAfternoonValid = int.TryParse(command.Instructions.Afternoon, out int afternoon);

            if (!isDayValid && !isLunchValid && !isAfternoonValid)
            {
                return Result.Failure<MedicineCreatedResponse>(new Error("Medicine.InvalidInstructions", "At least one of Day, Lunch, or Afternoon must be a number."));
            }

            // Set default values for untyped instructions
            command.Instructions.Day = isDayValid ? command.Instructions.Day : "0";
            command.Instructions.Lunch = isLunchValid ? command.Instructions.Lunch : "0";
            command.Instructions.Afternoon = isAfternoonValid ? command.Instructions.Afternoon : "0";

            var medicine = new Medicine
            {
                Name = command.Name,
                Company = command.Company,
                Stock = command.Stock,
                Price = command.Price,
                Status = command.Status,
                Type = command.Type,
                Instructions = command.Instructions
            };

            // Add the medicine to the repository
            _unitOfWork.Medicines.Add(medicine);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            var response = new MedicineCreatedResponse() { Response = "Medicine created successfully" };

            return Result.Success(response);
        }
    }
}
