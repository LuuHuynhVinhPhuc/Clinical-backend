using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByIDCommand : IRequest<Result<GetByIDResponse>>
    {
        public Guid ID { get; set; }
    }

    public class GetByIDResponse
    {
        public List<Patient> Patient { get; set; }
    }

    // Task 
    public class GetPatientByIDHandler : IRequestHandler<GetPatientByIDCommand, Result<GetByIDResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPatientByIDHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetByIDResponse>> Handle(GetPatientByIDCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.GetByIdAsync(request.ID).ConfigureAwait(false);

            if (patient == null)
                return Result.Failure<GetByIDResponse>(PatientError.IDNotFound(request.ID));

            var res = new GetByIDResponse() { Patient = new List<Patient> { patient } };
            return Result.Success(res);
        }
    }
}
