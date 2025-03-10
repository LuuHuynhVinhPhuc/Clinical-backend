﻿using ClinicalBackend.Domain.Entities;
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

        public string Gender { get; set; }

        public string DOB { get; set; }

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

            // Check phone number
            var phone = await _unitOfWork.Patient.GetByCondition(m => m.PhoneNumber == command.PhoneNumber).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (phone != null) // check existing number
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.PhoneAlreadyExisted(command.PhoneNumber));
            }

            if (!DateOnly.TryParseExact(command.DOB, "dd-MM-yyyy", out DateOnly dob)) // check date time format
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.InputDateInvalidFormat);
            }

            int age = CalculateAge(dob);

            if (age < 0)
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.InvalidDOBFormat);
            }

            // Create a new Patients Entity
            var patient = new Patient
            {
                Name = command.Name,
                Gender = command.Gender,
                DOB = dob, // Convert to DateOnly,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                Age = age,
                CheckStatus = "not_examined",
            };

            // Add to repository
            _unitOfWork.Patient.Add(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var response = new PatientCreatedResponse() { Response = "Patient created successfully" };

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