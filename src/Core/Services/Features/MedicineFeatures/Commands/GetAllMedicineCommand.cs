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
            var totalMedicines = await _unitOfWork.Medicines.GetAllAsync().ConfigureAwait(false);
            var medicines = totalMedicines
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            if (medicines == null || !medicines.Any())
            {
                return Result.Failure<QueryMedicinesResponse>(new Error("Medicines.NotFound", "No medicines found")); // Return an error if no medicines found
            }

            var response = new QueryMedicinesResponse { Medicines = medicines };
            return Result.Success(response);
        }
    }
}