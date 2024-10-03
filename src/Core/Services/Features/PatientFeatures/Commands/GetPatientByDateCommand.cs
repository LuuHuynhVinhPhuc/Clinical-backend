using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using ClinicalBackend.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByDateCommand : IRequest<Result<GetPatientByDateResponse>>
    {
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }

    public class GetPatientByDateResponse
    {
        public int TotalPatient { get; set; }
        public List<Patient> Patients { get; set; }
    }

    public class GetPatientByDateHandler : IRequestHandler<GetPatientByDateCommand, Result<GetPatientByDateResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientByDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetPatientByDateResponse>> Handle(GetPatientByDateCommand request, CancellationToken cancellationToken)
        {
            // convert to DateTime
            DateTime dateStart = DateTime.Parse(request.DateStart).ToUniversalTime();
            DateTime dateEnd = DateTime.Parse(request.DateEnd).ToUniversalTime();
            
            // check valid date 

            if (dateStart > dateEnd) 
            {
                return Result.Failure<GetPatientByDateResponse>(PatientError.InputDateInvalidFormat);
            }

            // how to get patient list depend on date Start and date End?
            var patients = await _unitOfWork.Patient.GetPatientByDateAsync(dateStart, dateEnd).ConfigureAwait(false);

            return Result.Success(new GetPatientByDateResponse
            {
                TotalPatient = patients.Count(),
                Patients = patients.ToList()
            });
        }
    }
}
