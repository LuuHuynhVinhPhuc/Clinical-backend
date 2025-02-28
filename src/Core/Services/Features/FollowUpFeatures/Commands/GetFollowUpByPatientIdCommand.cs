using ClinicalBackend.Contracts.DTOs.FollowUp;
using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Features.FollowUps;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace ClinicalBackend.Services.Features.FollowUpFeatures.Commands
{
    public class GetFollowUpByPatientIdCommand : IRequest<Result<GetFollowUpByPatientIdResponse>>
    {
        public Guid PatientId { get; set; }
    }

    public class GetFollowUpByPatientIdResponse
    {
        public List<FollowUpDto> FollowUps { get; set; }
    }

    // task
    public class GetFollowUpByPatientIdHandler : IRequestHandler<GetFollowUpByPatientIdCommand, Result<GetFollowUpByPatientIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFollowUpByPatientIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetFollowUpByPatientIdResponse>> Handle(GetFollowUpByPatientIdCommand request, CancellationToken cancellationToken)
        {
            var followUps = await _unitOfWork.FollowUp
                            .GetByPatientIdAsync(request.PatientId)
                            .ConfigureAwait(false);

            if (_unitOfWork.FollowUp == null)
            {
                throw new InvalidOperationException("UnitOfWork or FollowUp repository is not initialized.");
            }

            if (followUps == null)
                return Result.Failure<GetFollowUpByPatientIdResponse>(error: FollowUpErrors.FollowUpNotExists(request.PatientId));

            // return a list
            var res = new GetFollowUpByPatientIdResponse
            {
                FollowUps = _mapper.Map<List<FollowUpDto>>(followUps)
            };

            return Result.Success(res);
        }
    }
}