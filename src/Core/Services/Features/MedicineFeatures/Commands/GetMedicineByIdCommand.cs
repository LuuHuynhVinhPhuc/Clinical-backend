using ClinicalBackend.Domain.Entities;
using Domain.Interfaces;
using MediatR;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineByIdCommand : IRequest<Result<GetByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdResponse
    {
        public Medicine Medicine { get; set; }
    }


    public class GetMedicineByIdCommandHandler : IRequestHandler<GetMedicineByIdCommand, Result<GetByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMedicineByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetByIdResponse>> Handle(GetMedicineByIdCommand request, CancellationToken cancellationToken)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (medicine == null)
            {
                return Result.Failure<GetByIdResponse>(MedicineErrors.IdNotFound(request.Id));
            }

            var response = new GetByIdResponse {
                Medicine = medicine
            };

            return Result.Success(response);
        }
    }
}