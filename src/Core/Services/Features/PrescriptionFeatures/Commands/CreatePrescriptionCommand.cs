using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
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
        public DateTime BillDate { get; set; }
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
            //add various check here

            var prescription = new Prescription
            {
                PatientId = command.PatientId,
                FollowUpId = command.FollowUpId,
                Products = _mapper.Map<List<Product>>(command.Products),
                BillDate = DateTime.UtcNow,
                Notes = command.Notes
            };

            _unitOfWork.Prescription.Add(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PrescriptionCreatedResponse { Response = "Prescription created successfully" };
            return Result.Success(response);
        }
    }
}