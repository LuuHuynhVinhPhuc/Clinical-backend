using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    //Entity Command
    public class CreatePatientCommand : IRequest<Result<PatientCreatedResponse>>
    {
        public string Name { get; set; }

        public DateOnly DOB { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }

    // Response : for return a string value to alert
    public class PatientCreatedResponse
    {
        public required string Response { get; set; }
    }

    // Task
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<PatientCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Async task
        public async Task<Result<PatientCreatedResponse>> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.PatientNameExist);
            }

            // Check if the patient already exists
            var existingPatient = await _unitOfWork.Patient.GetByCondition(m => m.Name == command.Name)
                                                          .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (existingPatient != null)
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.PatientNameExist); // return alert with exists patient in DB
            }

            // Calculate age based on DOB
            int age = CalculateAge(command.DOB);

            // Create a new Patients Entity
            var patient = new Patient
            {
                Name = command.Name,
                DOB = command.DOB,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                Age = age, // Age is stored but not exposed in the command

                ModifiedAt = DateTime.UtcNow, // this element will change when update it
            };
            var response = new PatientCreatedResponse() { Response = "Patient created successfully" };

            // Add to repository
            _unitOfWork.Patient.Add(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

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