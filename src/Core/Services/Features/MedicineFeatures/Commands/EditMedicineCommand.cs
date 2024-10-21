using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class EditMedicineCommand : IRequest<Result<MedicineEditedResponse>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Instructions Instructions { get; set; }
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
            existingMedicine.Company = command.Company ?? existingMedicine.Company; 
            existingMedicine.Stock = command.Stock;
            existingMedicine.Price = command.Price;
            existingMedicine.Status = command.Status ?? existingMedicine.Status;
            existingMedicine.Type = command.Type ?? existingMedicine.Type;
            existingMedicine.Instructions = new Domain.Entities.Instructions
            {
                Day = command.Instructions.Day,
                Lunch = command.Instructions.Lunch,
                Afternoon = command.Instructions.Afternoon,
                Manual = command.Instructions.Manual
            };

            // Save changes to the repository
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var response = new MedicineEditedResponse() { Response = "Medicine edited successfully" };

            return Result.Success(response);
        }
    }
}