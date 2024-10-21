using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class GetAllPrescriptionCommand : IRequest<Result<GetAllPrescriptionResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllPrescriptionResponse
    {
        public List<GetPrescriptionDto> Prescriptions { get; set; }
        public PaginationInfo Pagination { get; set; }
    }

    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class GetAllPrescriptionCommandHandler : IRequestHandler<GetAllPrescriptionCommand, Result<GetAllPrescriptionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPrescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetAllPrescriptionResponse>> Handle(GetAllPrescriptionCommand request, CancellationToken cancellationToken)
        {
            //add various check here

            var prescriptions = await _unitOfWork.Prescription.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
            var totalItems = await _unitOfWork.Prescription.GetTotalCountAsync().ConfigureAwait(false);

            var response = new GetAllPrescriptionResponse
            {
                Prescriptions = _mapper.Map<List<GetPrescriptionDto>>(prescriptions),
                Pagination = new PaginationInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
                },
            };

            return Result.Success(response);
        }
    }
}