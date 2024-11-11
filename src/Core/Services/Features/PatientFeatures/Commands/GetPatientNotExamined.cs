using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientNotExaminedCommand : IRequest<Result<GetPatientNotExaminedResponse>>
    {
        public Guid ID { get; set; }
    }

    public class GetPatientNotExaminedResponse
    {
        public List<PatientsDto> Patient { get; set; }
    }

    // Task
    public class GetPatientNotExaminedHandler : IRequestHandler<GetPatientNotExaminedCommand, Result<GetPatientNotExaminedResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientNotExaminedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPatientNotExaminedResponse>> Handle(GetPatientNotExaminedCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.GetByIdAsync(request.ID).ConfigureAwait(false);

            if (patient == null)
                return Result.Failure<GetPatientNotExaminedResponse>(PatientError.IDNotFound(request.ID));

            var res = new GetPatientNotExaminedResponse() 
            { 
                Patient = _mapper.Map<List<PatientsDto>>(patient) 
            };
            
            return Result.Success(res);
        }
    }
}