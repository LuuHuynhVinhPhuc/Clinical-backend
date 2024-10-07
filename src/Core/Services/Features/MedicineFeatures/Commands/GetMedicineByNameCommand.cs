using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineByNameCommand : IRequest<Result<QueryMedicinesResponse>>
    {
        public string Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetMedicineByNameCommandHandler : IRequestHandler<GetMedicineByNameCommand, Result<QueryMedicinesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineByNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<QueryMedicinesResponse>> Handle(GetMedicineByNameCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchByNameAsync(request.Name, request.PageNumber, request.PageSize).ConfigureAwait(false);
            var totalItems = await _unitOfWork.Medicines.GetTotalCountByNameAsync(request.Name).ConfigureAwait(false);

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