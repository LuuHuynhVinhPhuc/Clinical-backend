using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetAllPatientAsync : IRequest<Result<GetAllPatientResponse>>
    {
        // default pagnigation params
        public int Page { get; set; } = 1; 
        public int Limit { get; set; } = 5;  
    }

    public class GetAllPatientResponse()
    {
        public List<Patient> Patient { get; set; }
        public int PageNumber { get; set; }
    }

    // Task
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientAsync, Result<GetAllPatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetAllPatientResponse>>Handle(GetAllPatientAsync request, CancellationToken cancellationToken)
        {
            // get all patient
            var PatientList = await _unitOfWork.Patient.GetAllAsync().ConfigureAwait(false);

            // pagnigation
            //var patientTotal = PatientList.Count();

            var pagnigation = PatientList.OrderByDescending(c => c.CreatedAt).Skip((request.Page - 1) * request.Limit).Take(request.Limit).ToList();

            var res = new GetAllPatientResponse()
            {
                Patient = pagnigation,
                PageNumber = request.Page,
            };

            return Result.Success(res);
        }
    }
}