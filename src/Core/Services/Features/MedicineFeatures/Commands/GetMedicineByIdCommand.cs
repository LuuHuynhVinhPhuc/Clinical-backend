using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Domain.Entities;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineByIdCommand : IRequest<Result<GetByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdResponse
    {
        public MedicineDto Medicine { get; set; }
    }

    public class GetMedicineByIdCommandHandler : IRequestHandler<GetMedicineByIdCommand, Result<GetByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetByIdResponse>> Handle(GetMedicineByIdCommand request, CancellationToken cancellationToken)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.Id).ConfigureAwait(false);

            if (medicine == null)
            {
                return Result.Failure<GetByIdResponse>(MedicineErrors.IdNotFound(request.Id));
            }

            var response = new GetByIdResponse
            {
                Medicine = _mapper.Map<MedicineDto>(medicine)
            };

            return Result.Success(response);
        }
    }
}