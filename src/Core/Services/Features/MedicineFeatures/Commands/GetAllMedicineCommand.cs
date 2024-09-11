using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetAllMedicineCommand : IRequest<List<Medicine>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllMedicineCommandHandler : IRequestHandler<GetAllMedicineCommand, List<Medicine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Medicine>> Handle(GetAllMedicineCommand request, CancellationToken cancellationToken)
        {
            var totalMedicines = await _unitOfWork.Medicines.GetAllAsync();
            var medicines = totalMedicines
                .OrderByDescending(m => m.CreatedAt) // Sort by CreatedAt descending
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return medicines;
        }
    }
}