using ClinicalBackend.Contracts.DTOs.Medicine;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.MedicineFeatures.Response;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Commands
{
    public class GetMedicineDetailsCommand : IRequest<Result<GetMedicineDeatilsResponse>>
    {
        public string Word { get; set; }
    }

    public class GetMedicineDeatilsResponse
    {
        public List<MedicineDto> Medicines { get; set; }
    }

    public class GetMedicineDetailsCommandHandler : IRequestHandler<GetMedicineDetailsCommand, Result<GetMedicineDeatilsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMedicineDetailsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetMedicineDeatilsResponse>> Handle(GetMedicineDetailsCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _unitOfWork.Medicines.SearchByDetailsAsync(request.Word).ConfigureAwait(false);

            if (!medicines.Any())
            {
                return Result.Failure<GetMedicineDeatilsResponse>(MedicineErrors.NotFound(request.Word));
            }

            var response = new GetMedicineDeatilsResponse
            {
                Medicines = _mapper.Map<List<MedicineDto>>(medicines),
            };

            return Result.Success(response);
        }
    }
}