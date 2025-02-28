using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;
using MapsterMapper;
using ClinicalBackend.Contracts.DTOs.Patient;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByContactInfoCommand : IRequest<Result<GetByContactInfoResponse>>
    {
        public string ContactInfo { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }

    public class GetByContactInfoResponse
    {
        public List<PatientsDto> Patients { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
    // Task
    public class GetPatientByContactInfoHandler : IRequestHandler<GetPatientByContactInfoCommand, Result<GetByContactInfoResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientByContactInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetByContactInfoResponse>> Handle(GetPatientByContactInfoCommand request, CancellationToken cancellationToken)
        {
            var patients = await _unitOfWork.Patient.GetByContactInfo(request.ContactInfo, request.Page, request.Limit).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Patient.GetCountByContactInfo(request.ContactInfo).ConfigureAwait(false);
            
            // check exist
            if (patients == null)
                return Result.Failure<GetByContactInfoResponse>(PatientError.PhoneNotFound(request.ContactInfo.ToString()));

            var res = new GetByContactInfoResponse 
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