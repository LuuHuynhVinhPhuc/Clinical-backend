using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetAllMedicineCommand : IRequest<Result<QueryMedicinesResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllMedicineCommandHandler : IRequestHandler<GetAllMedicineCommand, Result<QueryMedicinesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<QueryMedicinesResponse>> Handle(GetAllMedicineCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync(request.PageNumber, request.PageSize).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Medicines.GetTotalCountAsync().ConfigureAwait(false);

            var response = new QueryMedicinesResponse
            {
                Medicines = medicines.ToList(),
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