﻿using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    //Entity Command
    public class CreatePatientCommand : IRequest<Result<PatientCreatedResponse>>
    {
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    // Response : for return a string value to alert 
    public class PatientCreatedResponse
    {
        public required string Response { get; set; }
    }

    // Task
    public class CreatePatientCommandHander : IRequestHandler<CreatePatientCommand, Result<PatientCreatedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePatientCommandHander(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Async task
        public async Task<Result<PatientCreatedResponse>> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            // Check if the patient already exists
            var existingPatient = await _unitOfWork.Patient.GetByCondition(m => m.Name == command.Name)
                                                          .FirstOrDefaultAsync(cancellationToken);

            if (existingPatient != null)
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.PatientNameExist); // return alert with exists patient in DB
            }

            // Validate DOB format
            DateTime dob;
            if (!DateTime.TryParse(command.DOB, out dob))
            {
                return Result.Failure<PatientCreatedResponse>(PatientError.InvalidDOBFormat);
            }

            // Calculate age from DOB (for internal use only)
            int age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age)) age--;

            // Create a new Patients Entity
            var patient = new Patient
            {
                Name = command.Name,
                DOB = command.DOB,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                CreatedAt = command.CreatedAt,
                Age = age, // Age is stored but not exposed in the command
            };
            var response = new PatientCreatedResponse() { Response = "Patient created successfully" };

            // Add to repository
            _unitOfWork.Patient.Add(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
