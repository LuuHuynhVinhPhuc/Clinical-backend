using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineCommand : IRequest<List<Medicine>>
    {
        public string Name { get; set; }
    }

    public class GetMedicineCommandHandler : IRequestHandler<GetMedicineCommand, List<Medicine>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Medicine>> Handle(GetMedicineCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchByNameAsync(request.Name);

            return medicines ?? new List<Medicine>(); // Return an empty list if no medicines found
        }
    }
}