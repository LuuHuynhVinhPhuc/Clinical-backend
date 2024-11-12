using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using ClinicalBackend.Services.Features.MedicineFeatures;
using ClinicalBackend.Services.Features.PatientFeatures;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class CreatePrescriptionCommand : IRequest<Result<PrescriptionCreatedResponse>>
    {
        public Guid PatientId { get; set; }
        public Guid FollowUpId { get; set; }
        public ICollection<PostProductDto> Products { get; set; }
        public string? RevisitDate { get; set; }
        public string Notes { get; set; }
    }

    public class PrescriptionCreatedResponse
    {
        public string Response { get; set; }
    }

    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, Result<PrescriptionCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePrescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PrescriptionCreatedResponse>> Handle(CreatePrescriptionCommand command, CancellationToken cancellationToken)
        {
            if (command.Products == null || command.Products.Count == 0)
            {
                return Result.Failure<PrescriptionCreatedResponse>(new Error("Prescription.NoProducts", "No products were provided for the prescription"));
            }

            var patient = await _unitOfWork.Patient.GetByIdAsync(command.PatientId).ConfigureAwait(false);
            if (patient == null)
            {
                return Result.Failure<PrescriptionCreatedResponse>(PatientError.IDNotFound(command.PatientId));
            }

            var followUp = await _unitOfWork.FollowUp.GetByIdAsync(command.FollowUpId).ConfigureAwait(false);
            if (followUp == null || followUp.PatientId != command.PatientId)
            {
                return Result.Failure<PrescriptionCreatedResponse>(FollowUpErrors.NotFound(command.FollowUpId.ToString()));
            }

            // Check if each medicine has sufficient stock and update stock
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
                    return Result.Failure<PrescriptionCreatedResponse>(MedicineErrors.IdNotFound(productDto.MedicineId));
                }

                var medicineByName = await _unitOfWork.Medicines.GetByNameAsync(productDto.Name).ConfigureAwait(false);
                if (medicineByName == null || medicineByName.Id != medicine.Id) 
                {
                    return Result.Failure<PrescriptionCreatedResponse>(PrescriptionError.InvalidProductName(productDto.Name));
                }

                if (medicine.Stock < Quantity)
                {
                    return Result.Failure<PrescriptionCreatedResponse>(new Error("Medicine.InsufficientStock", $"Insufficient stock for medicine '{medicine.Name}'"));
                }

                medicine.Stock -= Quantity;
                medicine.Status = "SOLD";
                _unitOfWork.Medicines.Update(medicine);

                totalCost += medicine.Price * Quantity;
            }

            if (!DateOnly.TryParseExact(command.RevisitDate, "dd-MM-yyyy", out DateOnly revisitDate)) // check date time format
            {
                return Result.Failure<PrescriptionCreatedResponse>(PrescriptionError.InputDateInvalidFormat);
            }

            // Proceed with creating the prescription
            var prescription = new Prescription
            {
                PatientId = command.PatientId,
                FollowUpId = command.FollowUpId,
                Products = _mapper.Map<List<Product>>(command.Products),
                RevisitDate = revisitDate,
                BillDate = DateTime.UtcNow,
                Notes = command.Notes,
                TotalPrice = totalCost
            };

            _unitOfWork.Prescription.Add(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PrescriptionCreatedResponse { Response = "Prescription created successfully" };
            return Result.Success(response);
        }
    }
}