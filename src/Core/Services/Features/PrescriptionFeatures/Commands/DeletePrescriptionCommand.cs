using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class DeletePrescriptionCommand : IRequest<Result<PrescriptionDeletedResponse>>
    {
        public Guid Id { get; set; }
    }

    public class PrescriptionDeletedResponse
    {
        public string Response { get; set; }
    }

    public class DeletePrescriptionCommandHandler : IRequestHandler<DeletePrescriptionCommand, Result<PrescriptionDeletedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePrescriptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PrescriptionDeletedResponse>> Handle(DeletePrescriptionCommand command, CancellationToken cancellationToken)
        {
            var prescription = await _unitOfWork.Prescription.GetByIdAsync(command.Id);
            if (prescription == null)
            {
                return Result.Failure<PrescriptionDeletedResponse>(PrescriptionError.PrescriptionExist);
            }

            _unitOfWork.Prescription.Remove(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PrescriptionDeletedResponse { Response = "Prescription deleted successfully" };
            return Result.Success(response);
        }
    }
}

