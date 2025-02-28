using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetAllPatientCommands : IRequest<Result<GetAllPatientResponse>>
    {
        // default pagnigation params
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class GetAllPatientResponse
    {
        public List<PatientsDto> Patients { get; set; }
        public PaginationInfo Pagination { get; set; }
    }

    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    // Task
    public class GetAllPatientHandler : IRequestHandler<GetAllPatientCommands, Result<GetAllPatientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPatientHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetAllPatientResponse>> Handle(GetAllPatientCommands request, CancellationToken cancellationToken)
        {
            // get all patient
            var patients = await _unitOfWork.Patient.GetAllAsync(request.Page, request.Limit).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Patient.GetTotalCountAsync().ConfigureAwait(false);

            var res = new GetAllPatientResponse()
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