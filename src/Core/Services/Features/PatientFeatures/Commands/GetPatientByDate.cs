
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ClinicalBackend.Domain.Entities;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByDateCommand : IRequest<Result<GetPatientByDateResponse>>
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
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

            var patients = await _unitOfWork.Patient.GetPatientByDateAsync(request.DateStart, request.DateEnd).ConfigureAwait(false);

            if (patients == null || !patients.Any())
                return Result.Failure<GetPatientByDateResponse>(PatientError.NoPatientFoundForDate(request.DateStart));

            var totalPatient = patients.Count();

            var response = new GetPatientByDateResponse
            {
                TotalPatient = totalPatient,  // Count total patient
                Patients = patients.ToList()
            };

            return Result.Success(response);
        }
    }
}
