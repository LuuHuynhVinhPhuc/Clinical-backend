using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class UpdatePatientCommands : IRequest<Result<UpdatePatientResponse>>
    {
        public Guid Id { get; set; }
        public string? PatientName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class UpdatePatientResponse
    {
        public string Response { get; set; }
    }

    // Task
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommands, Result<UpdatePatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdatePatientResponse>> Handle(UpdatePatientCommands request, CancellationToken cancellationToken)
        {
            // find with ID 
            var patient = await _unitOfWork.Patient.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (patient == null)
                return Result.Failure<UpdatePatientResponse>(PatientError.NotFoundID(request.Id));

            // save data in Client 
            patient.Name = request.PatientName;
            patient.Age = request.Age;
            patient.Address = request.Address;
            patient.PhoneNumber = request.PhoneNumber;

            // reponse result
            var response = new UpdatePatientResponse() { Response = "Patient updated successfully" };

            // save changes 
            _unitOfWork.Patient.Update(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Result.Success(response);
        }
    }
}