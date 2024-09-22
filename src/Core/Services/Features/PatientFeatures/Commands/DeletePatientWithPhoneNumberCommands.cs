using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class DeletePatientWithPhoneNumberCommands : IRequest<Result<DeletePatientResponse>>
    {
        public string PhoneNumber { get; set; }
    }

    public class DeletePatientResponse
    {
        public string Response { get; set; }
    }

    // task
    public class DeletePatientWithPhoneNumberHandler : IRequestHandler<DeletePatientWithPhoneNumberCommands, Result<DeletePatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePatientWithPhoneNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DeletePatientResponse>> Handle(DeletePatientWithPhoneNumberCommands request, CancellationToken cancellationToken)
        {
            // find all patient details with Phone number 
            var patient = await _unitOfWork.Patient.FindWithPhoneNumberAsync(request.PhoneNumber).ConfigureAwait(false);
            // check exits
            if (patient == null)
            {
                return Result.Failure<DeletePatientResponse>(PatientError.NotFoundPhone(request.PhoneNumber));
            }

            // remove patient 
            _unitOfWork.Patient.Remove(patient);
            // save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            // return value 
            return Result.Success(new DeletePatientResponse { Response = "Patient deleted successfully" });
        }
    }
}