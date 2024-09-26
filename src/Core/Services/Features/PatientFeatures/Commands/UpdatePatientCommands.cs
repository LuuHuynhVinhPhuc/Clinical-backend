using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class UpdatePatientCommands : IRequest<Result<UpdatePatientResponse>>
    {
        public Guid Id { get; set; }
        public string? name { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string? phoneNumber { get; set; }

        public string CheckStatus { get; set; }
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

        public async Task<Result<UpdatePatientResponse>> Handle(UpdatePatientCommands command, CancellationToken cancellationToken)
        {
            // find with ID 
            var patient = await _unitOfWork.Patient.GetByIdAsync(command.Id).ConfigureAwait(false);

            if (patient == null)
                return Result.Failure<UpdatePatientResponse>(PatientError.IDNotFound(command.Id));

            if (!DateOnly.TryParseExact(command.dob, "dd-MM-yyyy", out DateOnly dob))
            {
                return Result.Failure<UpdatePatientResponse>(PatientError.InputDateInvalidFormat);
            }

            int age = CalculateAge(dob);

            if (age < 0)
            {
                return Result.Failure<UpdatePatientResponse>(PatientError.InvalidDOBFormat);
            }

            // save data in Client 
            patient.Name = command.name;
            patient.DOB = dob;
            patient.Age = age;
            patient.Address = command.address;
            patient.PhoneNumber = command.phoneNumber;
            patient.ModifiedAt = DateTime.UtcNow;
            patient.CheckStatus = command.CheckStatus;

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