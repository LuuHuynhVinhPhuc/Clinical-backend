using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class GetPrescriptionByPhoneCommand : IRequest<Result<GetPrescriptionByPhoneResponse>>
    {
        public string PhoneNumber { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPrescriptionByPhoneResponse
    {
        public List<GetPrescriptionDto> Prescriptions { get; set; }
        public PaginationInfo Pagination { get; set; }
    }

    public class GetPrescriptionByPhoneCommandHandler : IRequestHandler<GetPrescriptionByPhoneCommand, Result<GetPrescriptionByPhoneResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrescriptionByPhoneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPrescriptionByPhoneResponse>> Handle(GetPrescriptionByPhoneCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patient.FindWithPhoneNumberAsync(request.PhoneNumber).ConfigureAwait(false);
            if (patient == null)
            {
                return Result.Failure<GetPrescriptionByPhoneResponse>(PrescriptionError.PatientPhoneNotFound(request.PhoneNumber));
            }

            var prescriptions = await _unitOfWork.Prescription.GetByPatientIdAsync(patient.Id, request.PageNumber, request.PageSize).ConfigureAwait(false);
            var totalCount = await _unitOfWork.Prescription.GetTotalCountByPatientIdAsync(patient.Id).ConfigureAwait(false);

            var response = new GetPrescriptionByPhoneResponse
            {
                Prescriptions = _mapper.Map<List<GetPrescriptionDto>>(prescriptions),
                Pagination = new PaginationInfo
                {
                    TotalItems = totalCount,
                    TotalItemsPerPage = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
                }
            };

            return Result.Success(response);
        }
    }
}

