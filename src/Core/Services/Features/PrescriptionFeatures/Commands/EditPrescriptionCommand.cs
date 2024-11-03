using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class EditPrescriptionCommand : IRequest<Result<PrescriptionEditedResponse>>
    {
        public Guid Id { get; set; }
        public ICollection<PostProductDto> Medicines { get; set; }
        public string Notes { get; set; }
    }

    public class PrescriptionEditedResponse
    {
        public string Response { get; set; }
    }

    public class EditPrescriptionCommandHandler : IRequestHandler<EditPrescriptionCommand, Result<PrescriptionEditedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditPrescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PrescriptionEditedResponse>> Handle(EditPrescriptionCommand command, CancellationToken cancellationToken)
        {
            var prescription = await _unitOfWork.Prescription.GetByIdAsync(command.Id);
            if (prescription == null)
            {
                return Result.Failure<PrescriptionEditedResponse>(PrescriptionError.IDNotFound(command.Id));
            }

            // Return quantities from existing products back to medicine stock
            foreach (var existingProduct in prescription.Products)
            {
                var existingMedicine = await _unitOfWork.Medicines.GetByIdAsync(existingProduct.Medicine.Id).ConfigureAwait(false);
                if (existingMedicine != null)
                {
                    existingMedicine.Stock += existingProduct.Quantity;
                    _unitOfWork.Medicines.Update(existingMedicine);
                }
            }

            float totalCost = 0;
            foreach (var productDto in command.Medicines)
            {
                var medicine = await _unitOfWork.Medicines.GetByIdAsync(productDto.MedicineId).ConfigureAwait(false);
                if (medicine == null)
                {
                    return Result.Failure<PrescriptionEditedResponse>(MedicineErrors.IdNotFound(productDto.MedicineId));
                }

                if (medicine.Stock < productDto.Quantity)
                {
                    return Result.Failure<PrescriptionEditedResponse>(new Error("Medicine.InsufficientStock", $"Insufficient stock for medicine '{medicine.Name}'"));
                }

                bool isDayValid = int.TryParse(productDto.Instructions.Day, out int day);
                bool isLunchValid = int.TryParse(productDto.Instructions.Lunch, out int lunch);
                bool isAfternoonValid = int.TryParse(productDto.Instructions.Afternoon, out int afternoon);

                if (!isDayValid && !isLunchValid && !isAfternoonValid)
                {
                    return Result.Failure<PrescriptionEditedResponse>(new Error("Medicine.InvalidInstructions", "At least one of Day, Lunch, or Afternoon must be a number."));
                }

                medicine.Stock -= productDto.Quantity;
                medicine.Status = "SOLD";
                _unitOfWork.Medicines.Update(medicine);

                totalCost += medicine.Price * productDto.Quantity;
            }

            prescription.Products = _mapper.Map<List<Product>>(command.Medicines);
            prescription.Notes = command.Notes;
            prescription.TotalPrice = totalCost;

            _unitOfWork.Prescription.Update(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PrescriptionEditedResponse { Response = "Prescription edited successfully" };
            return Result.Success(response);
        }
    }
}