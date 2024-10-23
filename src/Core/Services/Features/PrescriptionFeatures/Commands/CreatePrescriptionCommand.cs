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
            // Check if the patient exists
            var patient = await _unitOfWork.Patient.GetByIdAsync(command.PatientId).ConfigureAwait(false);
            if (patient == null)
            {
                return Result.Failure<PrescriptionCreatedResponse>(PatientError.IDNotFound(command.PatientId));
            }

            // Check if the follow-up belongs to the patient
            var followUp = await _unitOfWork.FollowUp.GetByIdAsync(command.FollowUpId).ConfigureAwait(false);
            if (followUp == null || followUp.PatientId != command.PatientId)
            {
                return Result.Failure<PrescriptionCreatedResponse>(FollowUpErrors.NotFound(command.FollowUpId.ToString()));
            }

            // Check if each medicine has sufficient stock and update stock
            float totalCost = 0;
            foreach (var productDto in command.Products)
            {
                var medicine = await _unitOfWork.Medicines.GetByIdAsync(productDto.MedicineId).ConfigureAwait(false);
                if (medicine == null)
                {
                    return Result.Failure<PrescriptionCreatedResponse>(MedicineErrors.IdNotFound(productDto.MedicineId));
                }

                if (medicine.Stock < productDto.Quantity)
                {
                    return Result.Failure<PrescriptionCreatedResponse>(new Error("Medicine.InsufficientStock", $"Insufficient stock for medicine '{medicine.Name}'"));
                }

                // Update the stock
                medicine.Stock -= productDto.Quantity;
                _unitOfWork.Medicines.Update(medicine);

                // Calculate the total cost
                totalCost += medicine.Price * productDto.Quantity;
            }

            // Proceed with creating the prescription
            var prescription = new Prescription
            {
                PatientId = command.PatientId,
                FollowUpId = command.FollowUpId,
                Products = _mapper.Map<List<Product>>(command.Products),
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
