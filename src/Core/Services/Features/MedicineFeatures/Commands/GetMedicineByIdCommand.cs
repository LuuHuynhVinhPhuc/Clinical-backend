using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineByIdCommand : IRequest<Result<QueryMedicinesResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetMedicineByIdCommandHandler : IRequestHandler<GetMedicineByIdCommand, Result<QueryMedicinesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<QueryMedicinesResponse>> Handle(GetMedicineByIdCommand request, CancellationToken cancellationToken)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (medicine == null)
            {
                return Result.Failure<QueryMedicinesResponse>(MedicineErrors.IdNotFound(request.Id));
            }

            var response = new QueryMedicinesResponse { Medicines = new List<Medicine> { medicine } };
            return Result.Success(response);
        }
    }
}