using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class UpdatePatientCommand : IRequest<Result<UpdatePatientResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string? phoneNumber { get; set; }
        public string Status { get; set; }
    }

    public class UpdatePatientResponse
    {
        public string Response { get; set; }
    }

    // Task
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result<UpdatePatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdatePatientResponse>> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.GetByIdAsync(command.Id).ConfigureAwait(false);

            if (patient == null)
                return Result.Failure<UpdatePatientResponse>(PatientError.IDNotFound(command.Id));

            DateOnly dob = default;

            if (!string.IsNullOrEmpty(command.DOB) && !DateOnly.TryParseExact(command.DOB, "dd-MM-yyyy", out dob))
            {
                return Result.Failure<UpdatePatientResponse>(PatientError.InputDateInvalidFormat);
            }

            int age = CalculateAge(dob);

            if (age < 0)
            {
                return Result.Failure<UpdatePatientResponse>(PatientError.InvalidDOBFormat);
            }

            patient.Name = command.Name;

            if (patient.DOB != dob)
            {
                patient.DOB = dob;
                patient.Age = age;
            }
            
            patient.Address = command.Address;
            patient.PhoneNumber = command.phoneNumber;
            patient.ModifiedAt = DateTime.UtcNow;
            patient.CheckStatus = command.Status;

            // reponse result

            // save changes 
            _unitOfWork.Patient.Update(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new UpdatePatientResponse() { Response = "Patient updated successfully" };

            return Result.Success(response);
        }

        private int CalculateAge(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age)) age--;
            return age;
        }
    }
}