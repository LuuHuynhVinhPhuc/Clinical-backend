using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineByIdCommand : IRequest<Medicine>
    {
        public Guid Id { get; set; }
    }

    public class GetMedicineByIdCommandHandler : IRequestHandler<GetMedicineByIdCommand, Medicine>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Medicine> Handle(GetMedicineByIdCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.GetByIdAsync(request.Id).ConfigureAwait(false);

            return medicines;
        }
    }
}