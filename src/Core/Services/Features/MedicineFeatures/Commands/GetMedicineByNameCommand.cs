using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Common;

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

        public GetMedicineByNameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<QueryMedicinesResponse>> Handle(GetMedicineByNameCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchByNameAsync(request.Name).ConfigureAwait(false);
            var totalMedicines = medicines.Count();

            var paginatedMedicines = medicines
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var response = new QueryMedicinesResponse { Medicines = paginatedMedicines };
            return Result.Success(response);
        }
    }
}