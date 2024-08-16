using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Queries
{
    public class GetAllMedicinesQuery : IRequest<Result<IEnumerable<Medicine>>>
    {

    }

    public class GetAllMedicinesQueryHandler : IRequestHandler<GetAllMedicinesQuery, Result<IEnumerable<Medicine>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicinesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Medicine>>> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            return Result.Success(medicines);
    }
    }
}