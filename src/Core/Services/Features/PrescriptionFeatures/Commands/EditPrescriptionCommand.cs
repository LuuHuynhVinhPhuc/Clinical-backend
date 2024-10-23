using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class EditPrescriptionCommand : IRequest<Result<PrescriptionEditedResponse>>
    {
        public Guid Id { get; set; }
        public ICollection<Product> Medicines { get; set; }
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

            prescription.Products = _mapper.Map<List<Product>>(command.Medicines);
            prescription.Notes = command.Notes;

            _unitOfWork.Prescription.Update(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PrescriptionEditedResponse { Response = "Prescription edited successfully" };
            return Result.Success(response);
        }
    }
}

