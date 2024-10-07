using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
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
        public Patient Patient { get; set; }
    }

    // Task 
    public class GetPatientByIDHandler : IRequestHandler<GetPatientByIDCommand, Result<GetByIDResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientByIDHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetByIDResponse>> Handle(GetPatientByIDCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.GetByIdAsync(request.ID).ConfigureAwait(false);

            if (patient == null)
                return Result.Failure<GetByIDResponse>(PatientError.IDNotFound(request.ID));

            var res = new GetByIDResponse() { Patient = patient };
            return Result.Success(res);
        }
    }
}
