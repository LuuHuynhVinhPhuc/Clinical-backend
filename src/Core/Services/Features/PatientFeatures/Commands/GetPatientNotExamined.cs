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
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class GetPatientNotExaminedResponse
    {
        public List<PatientsDto> Patients { get; set; }
        public PaginationInfo Pagination { get; set; }
        
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
            var patients = await _unitOfWork.Patient.GetPatientsNotExamined(request.Page, request.Limit).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Patient.GetPatientsNotExaminedCountAsync().ConfigureAwait(false);

            var res = new GetPatientNotExaminedResponse()
            {
                Patients = _mapper.Map<List<PatientsDto>>(patients),
                Pagination = new PaginationInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.Limit,
                    CurrentPage = request.Page,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.Limit)
                }
            };
            
            return Result.Success(res);
        }
    }
}