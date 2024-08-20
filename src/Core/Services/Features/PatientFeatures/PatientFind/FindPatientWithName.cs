using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.PatientFeatures.Commands;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Services.Features.PatientFeatures.PatientFind
{
    public class FindPatientWithName : IRequest<Result<PatientsInfo>>
    {
        public string Name { get; set; }
    }

    // Task 
    public class FindPatientWithNameHandler : IRequestHandler<FindPatientWithName,Result<PatientsInfo>> 
    {
        private readonly IUnitOfWork _unitOfWork;
        public FindPatientWithNameHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        // Create task
        public async Task<Result<PatientsInfo>> Handle(FindPatientWithName request, CancellationToken cancellationToken)
        {
            // Get all patients (optimize if necessary)
            var allPatients = await _unitOfWork.PatientInfo.GetAllAsync(cancellationToken);

            // Filter patients by name
            var matchingPatients = allPatients.Where(p => p.PatientName.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            // get first elements
            var patient = matchingPatients.FirstOrDefault();

            if (patient == null)
            {
                return Result.Failure<PatientsInfo>(PatientError.NotFound(request.Name));
            }

            return Result.Success(patient);
        }
    }
}
