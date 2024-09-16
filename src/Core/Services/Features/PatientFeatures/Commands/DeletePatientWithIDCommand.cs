using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class DeletePatientWithIDCommand : IRequest<Result<DeletePatientWithIDResponse>>
    {
        public Guid ID { get; set; }
    }

    public class DeletePatientWithIDResponse
    {
        public string response { get; set; }
    }
    // task 
    public class DeletewithIDHandler : IRequestHandler<DeletePatientWithIDCommand, Result<DeletePatientWithIDResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletewithIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DeletePatientWithIDResponse>> Handle(DeletePatientWithIDCommand request, CancellationToken cancellationToken)
        {
            // find all patient 
            var patient = await _unitOfWork.Patient.GetByIdAsync(request.ID).ConfigureAwait(false);
            if (patient == null)
                return Result.Failure<DeletePatientWithIDResponse>(PatientError.NotFoundID(request.ID));

            // remove patient when it exits
            _unitOfWork.Patient.Remove(patient);
            // save change 
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var res = new DeletePatientWithIDResponse() { response = "Patient deleted successfully" };

            return Result.Success(res);
        }
    }
}