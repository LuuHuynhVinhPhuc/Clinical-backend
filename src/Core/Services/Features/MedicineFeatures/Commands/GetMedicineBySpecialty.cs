using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineBySpecialtyCommand : IRequest<Result<QueryMedicinesResponse<MedicineDto>>>
    {
        public string Specialty { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetMedicineBySpecialtyCommandHandler : IRequestHandler<GetMedicineBySpecialtyCommand, Result<QueryMedicinesResponse<MedicineDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineBySpecialtyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<QueryMedicinesResponse<MedicineDto>>> Handle(GetMedicineBySpecialtyCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchBySpecialtyAsync(request.Specialty, request.PageNumber, request.PageSize).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Medicines.GetTotalCountBySpecialtyAsync(request.Specialty).ConfigureAwait(false);

            var response = new QueryMedicinesResponse<MedicineDto>
            {
                Medicines = _mapper.Map<List<MedicineDto>>(medicines),
                Pagination = new PaginationInfo
                {
                    TotalItems = totalItems,
                    TotalItemsPerPage = request.PageSize,
                    CurrentPage = request.PageNumber,
                    TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
                }
            };

            return Result.Success(response);
        }
    }
}