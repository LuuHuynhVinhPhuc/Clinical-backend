using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class GetPrescriptionByIdCommand : IRequest<Result<GetPrescriptionByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetPrescriptionByIdResponse
    {
        public GetPrescriptionDto Prescription { get; set; }
    }

    public class GetPrescriptionByIdCommandHandler : IRequestHandler<GetPrescriptionByIdCommand, Result<GetPrescriptionByIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrescriptionByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPrescriptionByIdResponse>> Handle(GetPrescriptionByIdCommand command, CancellationToken cancellationToken)
        {
            var prescription = await _unitOfWork.Prescription.GetByIdAsync(command.Id);
            if (prescription == null)
            {
                return Result.Failure<GetPrescriptionByIdResponse>(PrescriptionError.IDNotFound(command.Id));
            }

            var res = new GetPrescriptionByIdResponse
            {
                Prescription = _mapper.Map<GetPrescriptionDto>(prescription)
            };

            return Result.Success(res);
        }
    }
}