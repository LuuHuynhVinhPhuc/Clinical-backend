using ClinicalBackend.Contracts.DTOs.Prescription;
using ClinicalBackend.Services.Common;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures.Commands
{
    public class GetPrescriptionByFollowUpCommand : IRequest<Result<GetPrescriptionByFollowUpResponse>>
    {
        public Guid FollowUpId { get; set; }
    }

    public class GetPrescriptionByFollowUpResponse
    {
        public List<GetPrescriptionDto> Prescriptions { get; set; }
    }

    public class GetPrescriptionByFollowUpCommandHandler : IRequestHandler<GetPrescriptionByFollowUpCommand, Result<GetPrescriptionByFollowUpResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrescriptionByFollowUpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetPrescriptionByFollowUpResponse>> Handle(GetPrescriptionByFollowUpCommand request, CancellationToken cancellationToken)
        {
            var followUp = await _unitOfWork.FollowUp.GetByIdAsync(request.FollowUpId).ConfigureAwait(false);

            if (followUp == null)
            {
                return Result.Failure<GetPrescriptionByFollowUpResponse>(PrescriptionError.FollowUpNotFound(request.FollowUpId));
            }

            var prescriptions = await _unitOfWork.Prescription.GetByFollowUpIdAsync(followUp.Id).ConfigureAwait(false);

            var response = new GetPrescriptionByFollowUpResponse
            {
                Prescriptions = _mapper.Map<List<GetPrescriptionDto>>(prescriptions),
            };

            return Result.Success(response);
        }
    }
}