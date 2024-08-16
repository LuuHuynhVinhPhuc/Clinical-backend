using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Queries
{
    public class GetMedicineQuery : IRequest<Result<Medicine>>
    {
        public int Id { get; set; }
    }

    public class GetMedicineQueryHandler : IRequestHandler<GetMedicineQuery, Result<Medicine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Medicine>> Handle(GetMedicineQuery request, CancellationToken cancellationToken)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.Id);
            if (medicine == null)
            {
                return Result.Failure<Medicine>(MedicineErrors.NotFound(request.Id.ToString()));
            }

            return Result.Success(medicine);
        }
    }
}
