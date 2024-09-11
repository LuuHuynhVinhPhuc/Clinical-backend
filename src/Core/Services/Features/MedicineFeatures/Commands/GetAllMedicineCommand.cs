using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetAllMedicineCommand : IRequest<(List<Medicine>, int)>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllMedicineCommandHandler : IRequestHandler<GetAllMedicineCommand, (List<Medicine>, int)>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(List<Medicine>, int)> Handle(GetAllMedicineCommand request, CancellationToken cancellationToken)
        {
            var totalMedicines = await _unitOfWork.Medicines.GetAllAsync().ConfigureAwait(false);
            var medicines = totalMedicines
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
            if (medicines == null || !medicines.Any())
            {
                return (new List<Medicine>(), 0); // Return an empty list and 0 as the total count if no medicines found
            }

            return (medicines, totalMedicines.Count());
        }
    }
}