using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByIDCommand : IRequest<Result<GetByIDResponse>>
    {
        public Guid ID { get; set; }
    }

    public class GetByIDResponse
    {
        public PatientsDto Patient { get; set; }
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

            var res = new GetByIDResponse() 
            { 
                Patient = _mapper.Map<PatientsDto>(patient) 
            };
            
            return Result.Success(res);
        }
    }
}