using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineDetailsCommand : IRequest<Result<QueryMedicinesResponse<MedicineDto>>>
    {
        public string Word { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetMedicineDetailsCommandHandler : IRequestHandler<GetMedicineDetailsCommand, Result<QueryMedicinesResponse<MedicineDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineDetailsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<QueryMedicinesResponse<MedicineDto>>> Handle(GetMedicineDetailsCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchByDetailsAsync(request.Word).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Medicines.GetCountByDetailAsync(request.Word).ConfigureAwait(false);

            if (!medicines.Any())
            {
                return Result.Failure<QueryMedicinesResponse<MedicineDto>>(MedicineErrors.NotFound(request.Word));
            }

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