using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineCommand : IRequest<(List<Medicine>, int)>
    {
        public string Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetMedicineCommandHandler : IRequestHandler<GetMedicineCommand, (List<Medicine>, int)>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(List<Medicine>, int)> Handle(GetMedicineCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchByNameAsync(request.Name);
            var totalMedicines = medicines.Count();

            var paginatedMedicines = medicines
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return (paginatedMedicines, totalMedicines);
        }
    }
}