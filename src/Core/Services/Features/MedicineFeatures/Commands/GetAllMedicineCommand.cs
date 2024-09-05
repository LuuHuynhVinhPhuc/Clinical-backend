using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetAllMedicineCommand : IRequest<List<Medicine>>
    {
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
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            if (medicines == null || !medicines.Any())
            {
                return new List<Medicine>(); // Return an empty list if no medicines found
            }

            return medicines.ToList();
        }
    }
}