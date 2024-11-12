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
        public ICollection<PostProductDto> Products { get; set; }
        public string? RevisitDate { get; set; }
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
            foreach (var productDto in command.Products)
            {
                var Quantity = 
                    Convert.ToInt32(productDto.Instructions.NumberOfDays) * 
                    (Convert.ToInt32(productDto.Instructions.Day)
                    + Convert.ToInt32(productDto.Instructions.Lunch)
                    + Convert.ToInt32(productDto.Instructions.Afternoon));

                var medicine = await _unitOfWork.Medicines.GetByIdAsync(productDto.MedicineId).ConfigureAwait(false);
                if (medicine == null)
                {
                    return Result.Failure<PrescriptionEditedResponse>(MedicineErrors.IdNotFound(productDto.MedicineId));
                }

                if (medicine.Stock < Quantity)
                {
                    return Result.Failure<PrescriptionEditedResponse>(new Error("Medicine.InsufficientStock", $"Insufficient stock for medicine '{medicine.Name}'"));
                }

                medicine.Stock -= Quantity;
                medicine.Status = "SOLD";
                _unitOfWork.Medicines.Update(medicine);

                totalCost += medicine.Price * Quantity;
            }

            if (!DateOnly.TryParseExact(command.RevisitDate, "dd-MM-yyyy", out DateOnly revisitDate))
            {
                return Result.Failure<PrescriptionEditedResponse>(PrescriptionError.InputDateInvalidFormat);
            }

            prescription.Products = _mapper.Map<List<Product>>(command.Products);
            prescription.Notes = command.Notes;
            prescription.RevisitDate = revisitDate;
            prescription.TotalPrice = totalCost;

            _unitOfWork.Prescription.Update(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PrescriptionEditedResponse { Response = "Prescription edited successfully" };
            return Result.Success(response);
        }
    }
}