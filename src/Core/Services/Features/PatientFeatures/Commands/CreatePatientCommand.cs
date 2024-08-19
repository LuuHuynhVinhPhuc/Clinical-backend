using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    //Entity Command
    public class CreatePatientCommand : IRequest<Result<PatientCreatedResponse>>
    {
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    // Response
    public class PatientCreatedResponse
    {
        public string Response { get; set; }
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
            // check if the patient already exists
            var existingPatient = await _unitOfWork.PatientInfo.GetByCondition(m 
                => m.PatientName == command.PatientName).FirstOrDefaultAsync(cancellationToken);
            if (existingPatient != null) {
                return Result.Failure<PatientCreatedResponse>(PatientError.PatientNameExist); // return alert with exists patient in DB
            }

            // Create a new Patients Entity
            var patient = new PatientsInfo
            {
                PatientName = command.PatientName,
                Age = command.Age,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
            };
            var response = new PatientCreatedResponse() { Response = "Patient created successfully" };

            // Add to respon
            _unitOfWork.PatientInfo.Add(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
