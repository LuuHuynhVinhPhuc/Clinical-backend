using ClinicalBackend.Contracts.DTOs.Patient;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;
using System.Globalization;

namespace ClinicalBackend.Services.Features.PatientFeatures.Commands
{
    public class GetPatientByDateCommand : IRequest<Result<GetPatientByDateResponse>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        // default pagnigation params
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 5;
    }

    public class GetPatientByDateResponse
    {
        public List<PatientsDto> Patients { get; set; }
        public PaginationsInfo Pagination { get; set; }
    }

    public class PaginationsInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class GetPatientByDateHandler : IRequestHandler<GetPatientByDateCommand, Result<GetPatientByDateResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientByDateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPatientByDateResponse>> Handle(GetPatientByDateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime dateStart = DateTime.ParseExact(request.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.ToUniversalTime();
                DateTime dateEnd = DateTime.ParseExact(request.EndDate, "dd-MM-yyyy", CultureInfo.InvariantCulture).Date.AddDays(1).AddTicks(-1).ToUniversalTime();


                // check valid date

                if (dateStart > dateEnd)
                {
                    return Result.Failure<GetPatientByDateResponse>(PatientError.InputDateInvalidFormat);
                }
                
                var totalItems = await _unitOfWork.Patient.GetTotalCountByDateAsync(dateStart, dateEnd).ConfigureAwait(false);

                // how to get patient list depend on date Start and date End?
                var patients = await _unitOfWork.Patient.GetPatientByDateAsync(dateStart, dateEnd, request.Page, request.Limit).ConfigureAwait(false);

                return Result.Success(new GetPatientByDateResponse
                {
                    Patients = _mapper.Map<List<PatientsDto>>(patients),
                    Pagination = new PaginationsInfo
                    {
                        TotalItems = totalItems,
                        TotalItemsPerPage = request.Page,
                        CurrentPage = request.Limit,
                        TotalPages = (int)Math.Ceiling((double)totalItems / request.Limit)
                    }
                });
            }
            catch (FormatException)
            {
                return Result.Failure<GetPatientByDateResponse>(PatientError.InputDateInvalidFormat);
            }
        }
    }
}